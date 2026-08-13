(function (window, document) {
    "use strict";

    var instances = new Map();
    var imageExtensions = ["jpg", "jpeg", "png", "gif", "bmp"];
    var maxImageSize = 10 * 1024 * 1024;
    var maxUploadSize = 50 * 1024 * 1024;

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

    async function upload(url, file) {
        var formData = new FormData();
        formData.append("upload", file, file.name);
        var response = await fetch(url, {
            method: "POST",
            credentials: "same-origin",
            headers: { "RequestVerificationToken": csrfToken() },
            body: formData
        });
        var payload;
        try {
            payload = await response.json();
        } catch (_) {
            throw new Error("服务器返回了无法识别的上传结果。");
        }
        if (!response.ok || !payload.success) {
            throw new Error(errorMessage(payload, "上传失败，请稍后重试。"));
        }
        return payload.data.files[0];
    }

    function chooseFile(accept, callback) {
        var input = document.createElement("input");
        input.type = "file";
        input.accept = accept;
        input.hidden = true;
        input.addEventListener("change", function () {
            if (input.files && input.files.length) callback(input.files[0]);
            input.remove();
        });
        window.addEventListener("focus", function () {
            window.setTimeout(function () {
                if (!input.files || !input.files.length) input.remove();
            }, 0);
        }, { once: true });
        document.body.appendChild(input);
        input.click();
    }

    async function uploadWithLock(editor, url, file) {
        if (!editor.lock("comcms-upload")) return null;
        try {
            return await upload(url, file);
        } catch (error) {
            editor.message.error(error && error.message ? error.message : "上传失败，请稍后重试。");
            return null;
        } finally {
            editor.unlock();
        }
    }

    function uploadAttachment(editor) {
        chooseFile(".pdf,.txt,.csv,.doc,.docx,.xls,.xlsx,.ppt,.pptx,.zip,.rar,.7z,.mp3,.wav", function (file) {
            if (file.size > maxUploadSize) {
                editor.message.error("附件不能超过 50 MiB。");
                return;
            }
            (async function () {
                var url = await uploadWithLock(editor, "/AdminCP/Upload/JoditUploadFile", file);
                if (!url) return;
                var link = document.createElement("a");
                link.href = url;
                link.target = "_blank";
                link.rel = "noopener";
                link.textContent = file.name;
                editor.s.insertHTML(link.outerHTML);
                editor.message.info("附件上传成功。");
            })();
        });
    }

    function uploadVideo(editor) {
        chooseFile("video/mp4,video/webm,video/ogg,.mp4,.webm,.ogv,.ogg", function (file) {
            if (file.size > maxUploadSize) {
                editor.message.error("视频不能超过 50 MiB。");
                return;
            }
            (async function () {
                var url = await uploadWithLock(editor, "/AdminCP/Upload/JoditUploadVideo", file);
                if (!url) return;
                var video = document.createElement("video");
                video.controls = true;
                video.preload = "metadata";
                video.setAttribute("playsinline", "");
                video.src = url;
                video.style.maxWidth = "100%";
                video.style.height = "auto";
                editor.s.insertHTML(video.outerHTML + "<p><br></p>");
                editor.message.info("视频上传成功。");
            })();
        });
    }

    function makeOptions(element, options) {
        var uploadAttachmentButton = {
            name: "comcmsUploadAttachment",
            icon: "file",
            tooltip: "上传附件",
            exec: function (editor) { uploadAttachment(editor); }
        };
        var uploadVideoButton = {
            name: "comcmsUploadVideo",
            icon: "video",
            tooltip: "上传视频",
            exec: function (editor) { uploadVideo(editor); }
        };
        var token = csrfToken();
        var height = element.id === "KindInfo" ? 360 : 520;

        return Object.assign({
            language: "zh_cn",
            height: height,
            minHeight: 280,
            maxHeight: 900,
            toolbarAdaptive: true,
            toolbarSticky: false,
            showCharsCounter: true,
            showWordsCounter: true,
            showXPathInStatusbar: false,
            enableDragAndDropFileToEditor: true,
            buttons: [
                "source", "|", "bold", "italic", "underline", "strikethrough", "eraser", "|",
                "ul", "ol", "outdent", "indent", "|", "font", "fontsize", "brush", "paragraph", "lineHeight", "|",
                "image", uploadAttachmentButton, uploadVideoButton, "table", "link", "hr", "symbols", "|",
                "left", "center", "right", "justify", "|", "undo", "redo", "find", "fullsize", "preview"
            ],
            uploader: {
                url: "/AdminCP/Upload/JoditUploadImage",
                method: "POST",
                withCredentials: true,
                headers: { "RequestVerificationToken": token },
                filesVariableName: function () { return "upload"; },
                imagesExtensions: imageExtensions,
                isSuccess: function (response) { return Boolean(response && response.success); },
                getMessage: function (response) { return errorMessage(response, "图片上传失败。"); },
                process: function (response) { return response.data; },
                beforeUpload: function (files) {
                    if (files.length !== 1) {
                        this.jodit.message.error("请一次上传一张图片。");
                        return false;
                    }
                    for (var i = 0; i < files.length; i += 1) {
                        if (files[i].size > maxImageSize) {
                            this.jodit.message.error("图片不能超过 10 MiB。");
                            return false;
                        }
                    }
                }
            }
        }, options || {});
    }

    function create(target, options) {
        if (!window.Jodit) throw new Error("Jodit 编辑器资源未加载。");
        var element = typeof target === "string" ? document.getElementById(target) : target;
        if (!element) throw new Error("找不到编辑器字段：" + target);
        if (instances.has(element.id)) return instances.get(element.id);

        var editor = window.Jodit.make(element, makeOptions(element, options));
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
        instances: instances
    };
})(window, document);
