/**
 * @license Copyright (c) 2003-2017, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	//config.language = 'zh-cn';
	// config.uiColor = '#AADC6E';
    config.baseHref = "http://" + location.host;
    config.filebrowserImageUploadUrl = '/AdminCP/Upload/CKUploadImage?action=uploadimg';
    config.filebrowserUploadUrl = '/AdminCP/Upload/CKUploadFile?action=uploadfile';
    config.font_names = '宋体/宋体;黑体/黑体;楷体/楷体;幼圆/幼圆;微软雅黑/微软雅黑;' + config.font_names;
    config.allowedContent = true;
    config.extraPlugins = 'html5video,lineheight,widget,widgetselection,clipboard,lineutils,emoji';
    config.line_height = "0;1em;1.1em;1.2em;1.3em;1.4em;1.5em;1.6em;1.7em;1.8em;1.9em;2em;2.5em;3em;3.5em;4em;5em";
    config.toolbar_Common = [
        { name: 'document', groups: ['mode', 'document', 'doctools'], items: ['Source', '-', 'NewPage', 'Preview', 'Print', '-', 'Templates'] },
        { name: 'clipboard', groups: ['clipboard', 'undo'], items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
        { name: 'editing', groups: ['find', 'selection', 'spellchecker'], items: ['Find', 'Replace', '-', 'SelectAll', '-', 'Scayt'] },
        { name: 'links', items: ['Link', 'Unlink', 'Anchor'] },
        '/',
        { name: 'basicstyles', groups: ['basicstyles', 'cleanup'], items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat'] },
        { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi'], items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', 'CreateDiv', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl'] },

        { name: 'insert', items: ['Image', 'Flash', 'html5video', 'Table', 'HorizontalRule', 'Smiley','emoji', 'SpecialChar', 'Iframe',] },
        '/',
        { name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize', 'lineheight'] },
        { name: 'colors', items: ['TextColor', 'BGColor'] },
        { name: 'tools', items: ['Maximize', 'ShowBlocks'] },
        { name: 'others', items: ['-'] },
        { name: 'about', items: ['About'] }
    ];
};
