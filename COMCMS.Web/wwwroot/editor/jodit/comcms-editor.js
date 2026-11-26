(function (window, document) {
    "use strict";

    var instances = new Map();
    var imageExtensions = ["jpg", "jpeg", "png", "gif", "bmp", "webp"];
    var maxImageSize = 10 * 1024 * 1024;
    var maxUploadSize = 50 * 1024 * 1024;
    var activeBrowserCallback = null;
    var activeBrowserEditor = null;

    function csrfToken() {
        var input = document.querySelector("input[name='__RequestVerificationToken']");
        return input ? input.value : "";
    }

    function errorMessage(payload, fallback) {
        if (payload && payload.data && payload.data.messages && payload.data.messages.length) {
            return payload.data.messages[0];
        }
        return payload && payload.message ? payload.message : fallback;
    }

    function isImageFile(file) {
        if (!file || !file.name) return false;
        var ext = file.name.split(".").pop().toLowerCase();
        return imageExtensions.indexOf(ext) !== -1;
    }

    function openAttachBrowser(editor, callback, onlyImages) {
        activeBrowserCallback = typeof callback === "function" ? callback : null;
        activeBrowserEditor = editor || null;

        try {
            if (editor && editor.events) {
                editor.events.fire("closeAllPopups");
            }
        } catch (_) { }

        var query = "?mode=jodit" + (onlyImages ? "&type=image" : "");
        var url = "/AdminCP/Attach/Index" + query;
        var title = onlyImages ? "选择图片 - 附件管理器" : "选择文件 - 附件管理器";

        if (window.layer && typeof window.layer.open === "function") {
            window.layer.open({
                type: 2,
                title: title,
                shadeClose: true,
                shade: 0.35,
                maxmin: true,
                area: ["92%", "88%"],
                content: url,
                end: function () {
                    activeBrowserCallback = null;
                    activeBrowserEditor = null;
                }
            });
            return;
        }

        window.open(url, "comcmsAttachBrowser", "width=1120,height=720,scrollbars=yes,resizable=yes");
    }

    function completeBrowserSelection(items) {
        if (!items || !items.length) return;

        var normalized = {
            baseurl: "",
            files: items.map(function (item) { return item.url; }),
            isImages: items.map(function (item) { return Boolean(item.isImage); }),
            items: items.map(function (item) { return { file: item.url, isImage: Boolean(item.isImage) }; })
        };

        var cb = activeBrowserCallback;
        var ed = activeBrowserEditor;
        activeBrowserCallback = null;
        activeBrowserEditor = null;

        if (typeof cb === "function") {
            cb(normalized);
            return;
        }

        // Fallback direct insert if callback was cleared
        if (ed && ed.s) {
            items.forEach(function (item) {
                if (item.isImage) {
                    ed.s.insertImage(item.url);
                } else {
                    var link = document.createElement("a");
                    link.href = item.url;
                    link.target = "_blank";
                    link.rel = "noopener";
                    link.textContent = item.name || item.url;
                    ed.s.insertHTML(link.outerHTML);
                }
            });
        }
    }

    function makeOptions(element, options) {
        var height = element.id === "KindInfo" ? 380 : 560;

        return Object.assign({
            language: "zh_cn",
            height: height,
            minHeight: 320,
            maxHeight: 960,
            // Display full Jodit v4 fat toolbar across 2 clean rows (no buttons hidden behind "...")
            toolbarAdaptive: false,
            toolbarSticky: false,
            showCharsCounter: true,
            showWordsCounter: true,
            showXPathInStatusbar: false,
            enableDragAndDropFileToEditor: true,
            imageDefaultWidth: null,
            // Enables the "Browse" tab in Image / File / Image-Properties dialogs
            filebrowser: {
                ajax: {
                    url: "/AdminCP/Attach/Index"
                }
            },
            uploader: {
                url: "/AdminCP/Upload/JoditUpload",
                method: "POST",
                withCredentials: true,
                headers: { "RequestVerificationToken": csrfToken() },
                filesVariableName: function () { return "upload"; },
                imagesExtensions: imageExtensions,
                prepareData: function (formData) {
                    var token = csrfToken();
                    if (token && formData && typeof formData.set === "function") {
                        formData.set("__RequestVerificationToken", token);
                    }
                    return formData;
                },
                isSuccess: function (response) { return Boolean(response && response.success); },
                getMessage: function (response) { return errorMessage(response, "文件上传失败。"); },
                process: function (response) { return response.data; },
                beforeUpload: function (files) {
                    if (!files || files.length === 0) return false;
                    for (var i = 0; i < files.length; i += 1) {
                        var file = files[i];
                        var limit = isImageFile(file) ? maxImageSize : maxUploadSize;
                        if (file.size > limit) {
                            this.jodit.message.error(isImageFile(file) ? "图片不能超过 10 MiB。" : "文件不能超过 50 MiB。");
                            return false;
                        }
                    }
                }
            }
        }, options || {});
    }

    function bindAttachFileBrowser(editor) {
        if (!editor) return;
        // Override lazy filebrowser getter / open method so clicking "Browse" always launches COMCMS Attach Manager
        var fb = editor.filebrowser;
        if (fb && typeof fb.open === "function") {
            fb.open = function (callback, onlyImages) {
                openAttachBrowser(editor, callback, Boolean(onlyImages));
                return Promise.resolve();
            };
        }
    }

    function create(target, options) {
        if (!window.Jodit) throw new Error("Jodit 编辑器资源未加载。");
        var element = typeof target === "string" ? document.getElementById(target) : target;
        if (!element) throw new Error("找不到编辑器字段：" + target);
        if (instances.has(element.id)) return instances.get(element.id);

        var editor = window.Jodit.make(element, makeOptions(element, options));
        bindAttachFileBrowser(editor);
        instances.set(element.id, editor);
        return editor;
    }

    function syncAll() {
        instances.forEach(function (editor) {
            if (!editor.isDestructed) editor.synchronizeValues();
        });
    }

    window.COMCMSEditor = {
        create: create,
        syncAll: syncAll,
        instances: instances,
        openAttachBrowser: openAttachBrowser,
        completeBrowserSelection: completeBrowserSelection
    };
})(window, document);
