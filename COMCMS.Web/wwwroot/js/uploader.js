$(function () {
    //初始化绑定默认的属性
    $.upLoadDefaults = $.upLoadDefaults || {};
    $.upLoadDefaults.property = {
        multiple: false, //是否多文件
        notwater: false, //是否加水印
        thumbnail: false, //是否生成缩略图
        sendurl: null, //发送地址
        filetypes: "jpg,jpge,png,gif", //文件类型
        filesize: "2048", //文件大小
        btntext: "浏览...", //上传按钮的文字
        swf: null //SWF上传控件相对地址
    };
    //初始化上传控件
    $.fn.InitUploader = function (p) {
        var fun = function (parentObj) {
            p = $.extend({}, $.upLoadDefaults.property, p || {});
            var btnObj = $('<div class="upload-btn">' + p.btntext + '</div>').appendTo(parentObj);
            //初始化属性
            p.sendurl += "?action=UpLoadFile";
            if (p.notwater) {
                p.sendurl += "&notwater=1";
            }
            if (p.thumbnail) {
                p.sendurl += "&IsThumbnail=1";
            }

            //初始化WebUploader
            var uploader = WebUploader.create({
                auto: true, //自动上传
                swf: p.swf, //SWF路径
                server: p.sendurl, //上传地址
                pick: {
                    id: btnObj,
                    multiple: p.multiple
                },
                accept: {
                    //title: 'Images',
                    extensions: p.filetypes,
                    //mimeTypes: 'image/*'
                },
                formData: {
                    //'DelFilePath': '' //定义参数
                },
                fileVal: 'Filedata', //上传域的名称
                fileSingleSizeLimit: p.filesize * 1024, //文件大小
                compress: false,
                noCompressIfLarger: false,
                compressSize: 0
            });

            //当validate不通过时，会以派送错误事件的形式通知
            uploader.on('error', function (type) {
                switch (type) {
                    case 'Q_EXCEED_NUM_LIMIT':
                        alert("错误：上传文件数量过多！");
                        break;
                    case 'Q_EXCEED_SIZE_LIMIT':
                        alert("错误：文件总大小超出限制！");
                        break;
                    case 'F_EXCEED_SIZE':
                        alert("错误：文件大小超出限制！");
                        break;
                    case 'Q_TYPE_DENIED':
                        alert("错误：禁止上传该类型文件！");
                        break;
                    case 'F_DUPLICATE':
                        alert("错误：请勿重复上传该文件！");
                        break;
                    default:
                        alert('错误代码：' + type);
                        break;
                }
            });

            //当有文件添加进来的时候
            uploader.on('fileQueued', function (file) {
                //如果是单文件上传，把旧的文件地址传过去
                if (!p.multiple) {
                    uploader.options.formData.DelFilePath = parentObj.siblings(".upload-path").val();
                }
                //防止重复创建
                if (parentObj.children(".upload-progress").length == 0) {
                    //创建进度条
                    //var fileProgressObj = $('<div class="upload-progress"></div>').appendTo(parentObj);
                    //var progressText = $('<span class="txt">正在上传，请稍候...</span>').appendTo(fileProgressObj);
                    //var progressBar = $('<span class="bar"><b></b></span>').appendTo(fileProgressObj);
                    //var progressCancel = $('<a class="close" title="取消上传">关闭</a>').appendTo(fileProgressObj);
                    //使用loadding
                    layer.load(0, {shade: [0.2, '#000']});
                    //绑定点击事件
                    //progressCancel.click(function () {
                    //    uploader.cancelFile(file);
                    //    fileProgressObj.remove();
                    //});
                }
            });

            //文件上传过程中创建进度条实时显示
            uploader.on('uploadProgress', function (file, percentage) {
                //var progressObj = parentObj.children(".upload-progress");
                //progressObj.children(".txt").html(file.name);
                //progressObj.find(".bar b").width(percentage * 100 + "%");
            });

            //当文件上传出错时触发
            uploader.on('uploadError', function (file, reason) {
                uploader.removeFile(file); //从队列中移除
                alert(file.name + "上传失败，错误代码：" + reason);
            });

            //当文件上传成功时触发
            uploader.on('uploadSuccess', function (file, data) {
                if (data.status == 0) {
                    layer.closeAll();
                    alert(data.msg);
                    var progressObj = parentObj.children(".upload-progress");
                    progressObj.children(".txt").html(data.msg);
                }
                if (data.status == 1) {
                    layer.closeAll();
                    //如果是单文件上传，则赋值相应的表单
                    if (!p.multiple) {
                        //parentObj.siblings(".upload-name").val(data.name);
                        parentObj.siblings(".upload-path").val(data.path);
                        //处理vue的问题
                        var listname = parentObj.siblings(".upload-path").data("listname");
                        var itemKey = parentObj.siblings(".upload-path").data("key");
                        var itemIndex = parentObj.siblings(".upload-path").data("index");
                        //console.log("当前：",listname, itemKey, itemIndex);
                        if (listname && itemKey && app) {
                            app[listname][itemIndex][itemKey] = data.path;
                        }
                        //parentObj.siblings(".upload-size").val(data.size);
                    } else {
                        addImage(parentObj, data.path, data.thumb);
                    }
                    var progressObj = parentObj.children(".upload-progress");
                    //progressObj.children(".txt").html("上传成功：" + file.name);
                }
                uploader.removeFile(file); //从队列中移除
            });

            //不管成功或者失败，文件上传完成时触发
            uploader.on('uploadComplete', function (file) {
                var progressObj = parentObj.children(".upload-progress");
                progressObj.children(".txt").html("上传完成");
                //如果队列为空，则移除进度条
                if (uploader.getStats().queueNum == 0) {
                    progressObj.remove();
                }
            });
        };
        return $(this).each(function () {
            fun($(this));
        });
    }
});

//添加图片相册
function addImage(targetObj, originalSrc, thumbSrc) {
    //插入到相册UL里面
	//验证是否是图片
	var newLi;
	if(CheckImageType(originalSrc)){
		newLi = $('<li class="addbutton">'
		+ '<input type="hidden" class="upopladfile" name="hid_photo_name" value="' + originalSrc + '" />'
		+ '<img class="img_size"  src="' + thumbSrc + '" bigsrc="' + originalSrc + '" />'
		+ '</li>');
	} else{
		newLi = $('<li class="addbutton">'
		+ '<input type="hidden" class="upopladfile" name="hid_photo_name" value="' + originalSrc + '" />'
		+ '<img class="img_size"  src="/templates/default/images/video.jpg" />'
		+ '</li>');
	}
    newLi.appendTo('.img_ul');
}

//验证图片格式
function CheckImageType(imagesrc) {
    var filename = imagesrc;
    if (filename.lastIndexOf(".") != -1) {
        var fileType = (filename.substring(filename.lastIndexOf(".") + 1, filename.length)).toLowerCase();
        var suppotFile = new Array();
        suppotFile[0] = "jpeg";
        suppotFile[1] = "jpg";
        suppotFile[2] = "gif";
        suppotFile[3] = "png";
        suppotFile[4] = "bmp";
        for (var i = 0; i < suppotFile.length; i++) {
            if (suppotFile[i] == fileType) {
                return true;
            } else {
                continue;
            }
        }
        return false;

    } else {
        return false;
    }
}