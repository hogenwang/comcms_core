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
    // You can use the "CKFinder" class to render CKFinder in a page:
    var finder = new CKFinder();
    finder.basePath = '/static/editor/ckeditor/ckfinder_net/'; // The path for the installation of CKFinder (default = "/ckfinder/").
    finder.selectActionFunction = SetFileField;
    finder.popup();
}
function SetFileField(fileUrl) {
    document.getElementById(dName).value = decodeURI(fileUrl);
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