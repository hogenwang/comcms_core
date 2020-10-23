//所有异步提交 formId 不需要加上#  last:2020-07-15  by Hogen Wang
//依赖jquery jquery.form jquery.validate jquery.metadata
function DoPost(formId, rules, messages) {
    //提交地址
    var $myform = $("#" + formId);
    var url = $myform.attr("action");
    var btn;
    if ($myform.find("input[type='submit']").length > 0) {
        btn = $myform.find("input[type='submit']");
    } else if ($myform.find("button[type='submit']").length > 0) {
        btn = $myform.find("button[type='submit']");
    }
    //alert(queryString);
    var v = $myform.validate({
        rules: rules,
        messages: messages,
        submitHandler: function (form) {
            //loadding效果
            var loading = layer.load(0, {
                shade: [0.2, '#000'] //0.1透明度的背景
            });

            btn.attr("disabled", "disabled");
            //执行loadding 并且ajax提交
            //如果是有CKEditor的话，需要update，否则取不到值
            if (window.CKEDITOR) {
                for (instance in CKEDITOR.instances)
                    CKEDITOR.instances[instance].updateElement();
            }
            var queryString = $myform.formSerialize();
            $.ajax({
                type: "POST",
                url: url,
                data: queryString,
                dataType: "JSON",
                success: function (data) {
                    if (data.status == "success") {
                        layer.close(loading);
                        //alert(data.message);
                        //window.location.href= data.returnUrl;
                        layer.msg(data.message, {
                            time: 1000 //2秒关闭（如果不配置，默认是3秒）
                        }, function () {
                            //alert(data.returnUrl);
                            if (data.returnUrl) {
                                if (data.returnUrl == "close") {
                                    var dindex = parent.layer.getFrameIndex(window.name);
                                    if (dindex) {
                                        parent.layer.close(dindex);
                                        var current = top.$(".page-tabs-content .active").attr("data-id");
                                        var target = top.$('.J_iframe[data-id="' + current + '"]');
                                        if (target[0].contentWindow.reloadlist) {
                                            target[0].contentWindow.reloadlist();
                                        }
                                        else if (target[0].contentWindow.doSearch) {
                                            target[0].contentWindow.doSearch();
                                        }
                                        else if (target[0].contentWindow.refresh) {
                                            target[0].contentWindow.refresh();
                                        }
                                        else {
                                            top.refreshCurrentTab();
                                        }
                                    } else {
                                        window.location.href = window.location.href;
                                    }
                                }
                                else if (data.returnUrl == "closeandreload") {
                                    var dindex = parent.layer.getFrameIndex(window.name);
                                    if (dindex) {
                                        //top.refreshCurrentTab();
                                        //刷新页面列表 如果有就刷新，没有就刷新整个界面
                                        var current = top.$(".page-tabs-content .active").attr("data-id");
                                        var target = top.$('.J_iframe[data-id="' + current + '"]');
                                        if (target[0].contentWindow.reloadlist) {
                                            target[0].contentWindow.reloadlist();
                                        }
                                        else if (target[0].contentWindow.doSearch) {
                                            target[0].contentWindow.doSearch();
                                        }
                                        else if (target[0].contentWindow.refresh) {
                                            target[0].contentWindow.refresh();
                                        }
                                        else {
                                            top.refreshCurrentTab();
                                        }
                                        parent.layer.close(dindex);
                                    }
                                }
                                else {
                                    window.location.href = data.returnUrl;
                                }
                            }
                            else {
                                window.location.href = window.location.href;
                            }

                        });
                    } else {
                        layer.close(loading);
                        layer.alert(data.message, { icon: 2 });
                        btn.removeAttr("disabled");
                    }
                },
                error: function () {
                    layer.close(loading);
                    layer.alert('执行错误，请联系管理员！', { icon: 2 });
                    btn.removeAttr("disabled");
                }
            });
        }
    });
    return false;
}
//后台异步删除
function doDel(url, id) {
    //console.log('找到记录：' + $("#tb_departments").length, window.location.href);
    top.layer.confirm('确认要删除当前记录？此操作不可恢复！', {
        icon: 3,
        title: '系统提示',
        btn: ['确定', '取消 '] //按钮
    }, function () {
        var loading = top.layer.load(0, {
            shade: [0.2, '#000'] //0.1透明度的背景
        });
        $.ajax({
            type: "POST",
            url: url,
            data: 'id=' + id,
            dataType: "JSON",
            success: function (data) {
                if (data.status == "success") {
                    top.layer.close(loading);
                    top.layer.msg(data.message, {
                        time: 1000 //2秒关闭（如果不配置，默认是3秒）
                    }, function () {
                        if ($("#tb_departments").length > 0) {
                            $("#tb_departments").bootstrapTable('refresh');
                        } else {
                            window.location.href = window.location.href;
                            //var dindex = parent.layer.getFrameIndex(window.name);
                            //if (dindex) {
                            //    layer.close(dindex);
                            //    return;
                            //}
                            //if (top.toplayerindex) {
                            //    //window.location.href = window.location.href;
                            //} else {
                            //    top.refreshCurrentTab();//刷新本tab
                            //}
                        }

                    });
                } else {
                    top.layer.close(loading);
                    top.layer.alert(data.message, { icon: 2 });

                }
            },
            error: function () {
                top.layer.close(loading);
                top.layer.alert('执行错误，请联系管理员！', { icon: 2 });
            }
        });
    }, function () {
    });
}
//执行添加
function doAdd(formid, url) {
    var strQuery = $("#" + formid + " .addnew").fieldSerialize();

    var loading = layer.load(0, {
        shade: [0.2, '#000'] //0.1透明度的背景
    });
    $.ajax({
        type: "POST",
        url: url,
        data: strQuery,
        dataType: "JSON",
        success: function (data) {
            if (data.status == "success") {
                layer.close(loading);
                layer.msg(data.message, {
                    time: 1000 //2秒关闭（如果不配置，默认是3秒）
                }, function () {
                    window.location.href = window.location.href;
                });
            } else {
                layer.close(loading);
                layer.alert(data.message, { icon: 2 });

            }
        },
        error: function () {
            layer.close(loading);
            layer.alert('执行错误，请联系管理员！', { icon: 2 });
        }
    });
}
//执行异步编辑
function doEdit(formid, url, id) {
    var strQuery = $("#" + formid + " #row_" + id + " .rt-edit").fieldSerialize();
    var loading = layer.load(0, {
        shade: [0.2, '#000'] //0.1透明度的背景
    });
    $.ajax({
        type: "POST",
        url: url,
        data: strQuery,
        dataType: "JSON",
        success: function (data) {
            if (data.status == "success") {
                layer.close(loading);
                layer.msg(data.message, {
                    time: 1000 //2秒关闭（如果不配置，默认是3秒）
                }, function () {
                    window.location.href = window.location.href;
                });
            } else {
                layer.close(loading);
                layer.alert(data.message, { icon: 2 });
            }
        },
        error: function () {
            layer.close(loading);
            layer.alert('执行错误，请联系管理员！', { icon: 2 });
        }
    });
}

//后台异步提示操作
function doAction(url, id, tiptitle) {
    layer.confirm(tiptitle, {
        icon: 3,
        title: '系统提示',
        btn: ['确定', '取消 '] //按钮
    }, function () {
        var loading = layer.load(0, {
            shade: [0.2, '#000'] //0.1透明度的背景
        });
        $.ajax({
            type: "POST",
            url: url,
            data: 'id=' + id,
            dataType: "JSON",
            success: function (data) {
                if (data.status == "success") {
                    layer.close(loading);
                    layer.msg(data.message, {
                        time: 1000 //2秒关闭（如果不配置，默认是3秒）
                    }, function () {
                        var index = parent.layer.getFrameIndex(window.name); //获取窗口索引
                        //window.location.href = window.location.href;
                        if (index) {
                            window.location.href = window.location.href;
                        } else {
                            //判断是否是列表
                            var current = top.$(".page-tabs-content .active").attr("data-id");
                            var target = top.$('.J_iframe[data-id="' + current + '"]');
                            if (target[0].contentWindow.doSearch) {
                                target[0].contentWindow.doSearch();
                            } else {
                                refreshCurrentTab();
                            }
                            //refreshCurrentTab();//刷新本tab
                        }
                    });
                } else {
                    layer.close(loading);
                    layer.alert(data.message, { icon: 2 });

                }
            },
            error: function () {
                layer.close(loading);
                layer.alert('执行错误，请联系管理员！', { icon: 2 });
            }
        });
    }, function () {
    });
}

function DoAdminLogin(formId, rules, messages) {
    //提交地址
    var $myform = $("#" + formId);
    var url = $myform.attr("action");
    var rvtoken = $("input[name='__RequestVerificationToken']").val();
    var btn;
    if ($myform.find("input[type='submit']").length > 0) {
        btn = $myform.find("input[type='submit']");
    } else if ($myform.find("button[type='submit']").length > 0) {
        btn = $myform.find("button[type='submit']");
    }
    //alert(queryString);
    var v = $myform.validate({
        rules: rules,
        messages: messages,
        submitHandler: function (form) {
            //loadding效果
            var loading = layer.load(0, {
                shade: [0.2, '#000'] //0.1透明度的背景
            });

            btn.attr("disabled", "disabled");
            //执行loadding 并且ajax提交

            var password = $("#password").val();
            var username = $("#username").val();
            password = encMe(password, encrypt_key, 1, 0);
            username = encMe(username, encrypt_key, 1, 0);
            var queryString = $myform.formSerialize();
            $.ajax({
                type: "POST",
                url: url,
                data: "username=" + username + "&password=" + password + "&__RequestVerificationToken=" + rvtoken,
                dataType: "JSON",
                success: function (data) {
                    if (data.status == "success") {
                        layer.close(loading);
                        //alert(data.message);
                        //window.location.href= data.returnUrl;
                        layer.msg(data.message, {
                            time: 1000 //2秒关闭（如果不配置，默认是3秒）
                        }, function () {
                            window.location.href = data.returnUrl;
                        });
                    } else {
                        layer.close(loading);
                        if (data.other == "reload") {
                            layer.msg('页面访问超时，正在刷新页面，请重新登录！', function () {
                                //关闭后的操作
                                setTimeout(function () {
                                    window.location.href = window.location.href;
                                },1500)
                            });
                        } else {
                            layer.alert(data.message, { icon: 2 });
                        }
                        
                        btn.removeAttr("disabled");
                    }
                },
                error: function () {
                    layer.close(loading);
                    layer.alert('执行错误，请联系管理员！', { icon: 2 });
                    btn.removeAttr("disabled");
                }
            });
        }
    });
    return false;
}


//后台列表执行批量操作
function doBatchAction(url, title) {
    var list = $('#tb_departments').bootstrapTable('getSelections');
    var ids = [];
    if (!list || list.length < 1) {
        layer.alert("请至少选择一条记录！", { icon: 2 });
        return false;
    }
    list.map(function (item, index) {
        if (item.id) {
            ids.push(item.id);
        }
        if (item.Id) {
            ids.push(item.Id);
        }
    })

    layer.confirm('确认要执行批量' + title + '吗？', {
        icon: 3,
        title: '系统提示',
        btn: ['确定', '取消 '] //按钮
    }, function () {
        var loading = layer.load(0, {
            shade: [0.2, '#000'] //0.1透明度的背景
        });
        $.ajax({
            type: "POST",
            url: url,
            data: { ids: JSON.stringify(ids) },
            dataType: "JSON",
            success: function (data) {
                if (data.status == "success") {
                    layer.close(loading);
                    layer.msg(data.message, {
                        time: 1000 //2秒关闭（如果不配置，默认是3秒）
                    }, function () {
                        //执行重新载入
                        if (doSearch) {
                            doSearch();
                        } else {
                            refreshCurrentTab();
                        }
                    });
                } else {
                    layer.close(loading);
                    layer.alert(data.message, { icon: 2 });

                }
            },
            error: function () {
                layer.close(loading);
                layer.alert('执行错误，请联系管理员！', { icon: 2 });
            }
        });
    }, function () {
    });
}