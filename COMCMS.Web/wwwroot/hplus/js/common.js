//顶部打开dialog
function showDialog(id, title, url, width, height) {
    top.showDialog(id, title, url, width, height)
}
//删除数据
//function doDel(url, id) {
//    top.doDel(url, id);
//}
//刷新网页
function refresh() {

    window.location = window.location;
}
//
dName = "";
function BrowseServer(id) {
    dName = id;
    var input = document.createElement("input");
    input.type = "file";
    input.accept = "image/jpeg,image/png,image/gif,image/bmp,.jpg,.jpeg,.png,.gif,.bmp";
    input.hidden = true;
    input.addEventListener("change", async function () {
        if (!input.files || !input.files.length) return;
        var formData = new FormData();
        formData.append("upload", input.files[0], input.files[0].name);
        var loading = window.layer ? layer.load(1) : null;
        try {
            var token = document.querySelector("input[name='__RequestVerificationToken']");
            var response = await fetch("/AdminCP/Upload/JoditUploadImage", {
                method: "POST",
                credentials: "same-origin",
                headers: { "RequestVerificationToken": token ? token.value : "" },
                body: formData
            });
            var payload = await response.json();
            if (!response.ok || !payload.success) {
                var messages = payload.data && payload.data.messages;
                throw new Error(messages && messages.length ? messages[0] : "图片上传失败。");
            }
            SetFileField(payload.data.files[0]);
        } catch (error) {
            if (window.layer) layer.msg(error.message || "图片上传失败。");
        } finally {
            if (loading !== null) layer.close(loading);
            input.remove();
        }
    });
    document.body.appendChild(input);
    input.click();
}
function SetFileField(fileUrl) {
    var field = document.getElementById(dName);
    if (!field) return;
    field.value = decodeURI(fileUrl);
    if (window.jQuery) $(field).trigger("change");
}
//显示隐藏新增行
function showAddRow() {
    $("#addnew").toggleClass('hidden');
}
//显示编辑
function showEdit(id) {
    $("#row_" + id + " .rt-editpanel").hide();
    $("#row_" + id + " .rt-savepanel").removeClass('hidden');
}
//取消编辑
function cancelEdit(id) {
    $("#row_" + id + " .rt-editpanel").show();
    $("#row_" + id + " .rt-savepanel").addClass('hidden');
}

/*********
验证是否是数字
**********/
function isInt(str) {
    //如果为空，则通过校验
    if (str == "")
        return false;
    if (/^(\-?)(\d+)$/.test(str))
        return true;
    else
        return false;
}
