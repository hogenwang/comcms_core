/*!
 * COMCMS Native HTML5/XHR2 Uploader
 * Zero third-party dependencies. Supports images, attachments, videos,
 * real-time progress, drag-and-drop, cancellation, live preview updates, and CSRF tokens.
 */
(function (window, $) {
    'use strict';

    var IMAGE_EXTS = { jpg: true, jpeg: true, png: true, gif: true, bmp: true, webp: true };

    var defaults = {
        multiple: false,
        notwater: false,
        thumbnail: false,
        sendurl: '/AdminCP/Upload/UploadImage',
        filetypes: 'gif,jpg,jpeg,png,bmp',
        filesize: 10240, // KB (10 MiB default)
        btntext: '上传',
        icon: 'fa-cloud-upload',
        fileField: 'Filedata',
        onSuccess: null,
        onError: null
    };

    function notifyError(message) {
        var text = message || '上传失败，请稍后重试。';
        if (window.layer && typeof window.layer.msg === 'function') {
            window.layer.msg(text, { icon: 2, time: 3000 });
        } else {
            window.alert(text);
        }
    }

    function notifySuccess(message) {
        if (window.layer && typeof window.layer.msg === 'function') {
            window.layer.msg(message || '上传成功', { icon: 1, time: 1500 });
        }
    }

    function getAntiforgeryToken(container) {
        var $form = $(container).closest('form');
        var token = $form.find("input[name='__RequestVerificationToken']").first().val();
        if (!token) {
            token = $("input[name='__RequestVerificationToken']").first().val();
        }
        return token || '';
    }

    function normalizeExtensions(filetypes) {
        if (!filetypes) return [];
        return String(filetypes)
            .split(',')
            .map(function (ext) { return ext.trim().toLowerCase().replace(/^\./, ''); })
            .filter(function (ext) { return ext.length > 0; });
    }

    function getExtension(fileName) {
        if (!fileName || fileName.lastIndexOf('.') === -1) return '';
        return fileName.substring(fileName.lastIndexOf('.') + 1).toLowerCase();
    }

    function isImagePath(path) {
        if (!path) return false;
        var clean = String(path).split('?')[0].split('#')[0];
        return !!IMAGE_EXTS[getExtension(clean)];
    }

    function buildAcceptAttribute(exts) {
        if (!exts || exts.length === 0) return '';
        return exts.map(function (ext) { return '.' + ext; }).join(',');
    }

    function buildUploadUrl(opts) {
        var base = opts.sendurl || defaults.sendurl;
        var sep = base.indexOf('?') === -1 ? '?' : '&';
        var query = ['action=UpLoadFile'];
        if (opts.notwater) query.push('notwater=1');
        if (opts.thumbnail) query.push('IsThumbnail=1');
        return base + sep + query.join('&');
    }

    function updatePreview($parent, uploadedPath) {
        if (!uploadedPath || !isImagePath(uploadedPath)) return;
        var $preview = $parent.siblings('.my-preview');
        if ($preview.length === 0) return;

        var $img = $preview.find('img').first();
        if ($img.length > 0) {
            $img.attr('src', uploadedPath);
        }
    }

    function appendAlbumItem(originalSrc, thumbSrc) {
        var $ul = $('.img_ul');
        if ($ul.length === 0) return;

        var $li = $('<li class="addbutton"></li>');
        $('<input type="hidden" class="upopladfile" name="hid_photo_name" />').val(originalSrc).appendTo($li);
        var $img = $('<img class="img_size" />');
        if (isImagePath(originalSrc)) {
            $img.attr('src', thumbSrc || originalSrc).attr('bigsrc', originalSrc);
        } else {
            $img.attr('src', '/templates/default/images/video.jpg');
        }
        $img.appendTo($li);
        $ul.append($li);
    }

    function mountUploader(element, rawOptions) {
        if (!element || element.getAttribute('data-comcms-uploader') === '1') {
            return;
        }
        element.setAttribute('data-comcms-uploader', '1');

        var $parent = $(element);
        var opts = $.extend({}, defaults, rawOptions || {});
        var allowedExts = normalizeExtensions(opts.filetypes);
        var maxBytes = (parseInt(opts.filesize, 10) || 10240) * 1024;
        var uploadUrl = buildUploadUrl(opts);

        var buttonText = opts.btntext;
        if (!buttonText || buttonText === '浏览...') {
            buttonText = '上传';
        }

        $parent.empty();

        var $wrap = $('<div class="comcms-uploader"></div>');
        var $fileInput = $('<input type="file" class="comcms-uploader-input" />');
        if (opts.multiple) {
            $fileInput.prop('multiple', true);
        }
        var acceptAttr = buildAcceptAttribute(allowedExts);
        if (acceptAttr) {
            $fileInput.attr('accept', acceptAttr);
        }

        var $btn = $('<button type="button" class="comcms-upload-btn"></button>');
        var $progressBar = $('<span class="comcms-upload-progress-bar"></span>');
        var $label = $('<span class="comcms-upload-label"></span>');
        var $icon = $('<i class="fa"></i>').addClass(opts.icon || 'fa-cloud-upload');
        var $text = $('<span></span>').text(buttonText);
        var $cancel = $('<span class="comcms-upload-cancel" title="取消上传">&times;</span>');

        $label.append($icon).append($text);
        $btn.append($progressBar).append($label).append($cancel);
        $wrap.append($btn).append($fileInput);
        $parent.append($wrap);

        var activeXhr = null;

        function setIdleState() {
            activeXhr = null;
            $btn.removeClass('is-uploading').prop('disabled', false);
            $progressBar.css('width', '0%');
            $icon.attr('class', 'fa ' + (opts.icon || 'fa-cloud-upload'));
            $text.text(buttonText);
            $fileInput.val('');
        }

        function setUploadingState(percent) {
            $btn.addClass('is-uploading');
            $icon.attr('class', 'fa fa-spinner fa-spin');
            var pct = Math.max(0, Math.min(100, Math.round(percent || 0)));
            $progressBar.css('width', pct + '%');
            $text.text(pct + '%');
        }

        function validateFile(file) {
            if (!file) return '未选择任何文件。';
            var ext = getExtension(file.name);
            if (allowedExts.length > 0 && allowedExts.indexOf(ext) === -1) {
                return '不支持的文件类型（仅支持：' + allowedExts.join(', ') + '）。';
            }
            if (file.size <= 0) {
                return '上传文件不能为空。';
            }
            if (maxBytes > 0 && file.size > maxBytes) {
                var mb = (maxBytes / (1024 * 1024)).toFixed(1).replace(/\.0$/, '');
                return '文件大小超出限制（最大允许 ' + mb + ' MB）。';
            }
            return null;
        }

        function uploadQueue(files, index) {
            if (index >= files.length) {
                setIdleState();
                return;
            }
            var file = files[index];
            var validationError = validateFile(file);
            if (validationError) {
                notifyError(validationError);
                if (typeof opts.onError === 'function') opts.onError(validationError, file);
                uploadQueue(files, index + 1);
                return;
            }

            var formData = new FormData();
            formData.append(opts.fileField || 'Filedata', file);
            var token = getAntiforgeryToken(element);
            if (token) {
                formData.append('__RequestVerificationToken', token);
            }
            if (!opts.multiple) {
                var oldPath = $parent.siblings('.upload-path').val();
                if (oldPath) {
                    formData.append('DelFilePath', oldPath);
                }
            }

            var xhr = new XMLHttpRequest();
            activeXhr = xhr;
            setUploadingState(0);

            xhr.upload.onprogress = function (evt) {
                if (evt.lengthComputable && evt.total > 0) {
                    setUploadingState((evt.loaded / evt.total) * 100);
                }
            };

            xhr.onreadystatechange = function () {
                if (xhr.readyState !== 4) return;
                if (xhr.status === 0) {
                    // Aborted by user
                    setIdleState();
                    return;
                }

                var data = null;
                try {
                    data = JSON.parse(xhr.responseText);
                } catch (e) {
                    data = null;
                }

                if (xhr.status >= 200 && xhr.status < 300 && data && data.status == 1) {
                    if (!opts.multiple) {
                        var $pathInput = $parent.siblings('.upload-path');
                        $pathInput.val(data.path).trigger('input').trigger('change');
                        updatePreview($parent, data.path);

                        var listname = $pathInput.data('listname');
                        var itemKey = $pathInput.data('key');
                        var itemIndex = $pathInput.data('index');
                        if (listname && itemKey && typeof window.app !== 'undefined' && window.app && window.app[listname] && window.app[listname][itemIndex]) {
                            window.app[listname][itemIndex][itemKey] = data.path;
                        }
                    } else {
                        appendAlbumItem(data.path, data.thumb);
                    }
                    notifySuccess(data.msg || '上传成功');
                    if (typeof opts.onSuccess === 'function') {
                        opts.onSuccess(data, file);
                    }
                } else {
                    var errMsg = (data && (data.msg || data.message)) || ('上传失败（HTTP ' + xhr.status + '）');
                    notifyError(errMsg);
                    if (typeof opts.onError === 'function') {
                        opts.onError(errMsg, file);
                    }
                }

                uploadQueue(files, index + 1);
            };

            xhr.onerror = function () {
                var errMsg = '网络异常，上传失败，请重试。';
                notifyError(errMsg);
                if (typeof opts.onError === 'function') opts.onError(errMsg, file);
                setIdleState();
            };

            xhr.open('POST', uploadUrl, true);
            xhr.setRequestHeader('X-Requested-With', 'XMLHttpRequest');
            if (token) {
                xhr.setRequestHeader('RequestVerificationToken', token);
            }
            xhr.send(formData);
        }

        $btn.on('click', function (e) {
            if ($(e.target).closest('.comcms-upload-cancel').length > 0) {
                return;
            }
            if (activeXhr) return;
            $fileInput.trigger('click');
        });

        $cancel.on('click', function (e) {
            e.preventDefault();
            e.stopPropagation();
            if (activeXhr) {
                activeXhr.abort();
                notifyError('已取消上传');
            }
        });

        $fileInput.on('change', function () {
            var files = this.files ? Array.prototype.slice.call(this.files) : [];
            if (files.length === 0) return;
            if (!opts.multiple && files.length > 1) {
                files = [files[0]];
            }
            uploadQueue(files, 0);
        });

        // Support drag-and-drop onto the enclosing .input-group
        var $dropZone = $parent.closest('.input-group');
        if ($dropZone.length > 0 && !$dropZone.data('comcmsDropBound')) {
            $dropZone.data('comcmsDropBound', true);
            $dropZone.on('dragenter dragover', function (e) {
                e.preventDefault();
                e.stopPropagation();
                $dropZone.addClass('comcms-upload-dragover');
            });
            $dropZone.on('dragleave drop', function (e) {
                e.preventDefault();
                e.stopPropagation();
                $dropZone.removeClass('comcms-upload-dragover');
            });
            $dropZone.on('drop', function (e) {
                if (activeXhr) return;
                var dt = e.originalEvent && e.originalEvent.dataTransfer;
                if (!dt || !dt.files || dt.files.length === 0) return;
                var dropped = Array.prototype.slice.call(dt.files);
                if (!opts.multiple && dropped.length > 1) {
                    dropped = [dropped[0]];
                }
                uploadQueue(dropped, 0);
            });
        }
    }

    window.COMCMSUploader = {
        defaults: defaults,
        init: function (target, options) {
            $(target).each(function () {
                mountUploader(this, options);
            });
        }
    };

    $.fn.comcmsUploader = function (options) {
        return this.each(function () {
            mountUploader(this, options);
        });
    };

    // Compatibility alias for any legacy script calling InitUploader
    $.fn.InitUploader = $.fn.comcmsUploader;
})(window, window.jQuery);
