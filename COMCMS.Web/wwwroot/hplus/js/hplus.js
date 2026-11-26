// COMCMS Horizon Admin Shell Script (Bootstrap 5 + MetisMenu v3)

$(document).ready(function () {
    // MetisMenu v3 initialization
    if ($.fn.metisMenu && $('#side-menu').length) {
        $('#side-menu').metisMenu();
    }

    // Restore sidebar collapsed state on desktop
    if (localStorageSupport() && $(window).width() >= 992) {
        if (localStorage.getItem('collapse_menu') === 'on') {
            $('body').addClass('mini-navbar');
        }
    } else if ($(window).width() < 992) {
        $('body').addClass('mini-navbar');
    }

    // Sidebar toggle button
    $('.navbar-minimalize').on('click', function (e) {
        e.preventDefault();
        $('body').toggleClass('mini-navbar');
        if (localStorageSupport() && $(window).width() >= 992) {
            localStorage.setItem('collapse_menu', $('body').hasClass('mini-navbar') ? 'on' : 'off');
        }
    });

    // Mobile overlay backdrop click closes sidebar
    $('#sidebarBackdrop').on('click', function () {
        $('body').addClass('mini-navbar');
    });

    // Clicking a top-level menu while collapsed expands sidebar cleanly
    $('#side-menu > li > a').on('click', function () {
        if ($('body').hasClass('mini-navbar') && $(window).width() >= 992) {
            $('body').removeClass('mini-navbar');
            if (localStorageSupport()) {
                localStorage.setItem('collapse_menu', 'off');
            }
        }
    });

    // On mobile, selecting a leaf menu closes the slide-over drawer
    $('#side-menu').on('click', 'a.J_menuItem', function () {
        if ($(window).width() < 992) {
            $('body').addClass('mini-navbar');
        }
    });

    // iOS iframe scroll helper
    if (/(iPhone|iPad|iPod|iOS)/i.test(navigator.userAgent)) {
        $('#content-main').css('overflow-y', 'auto');
    }
});

$(window).on('resize', function () {
    if ($(window).width() < 992) {
        $('body').addClass('mini-navbar');
    }
});

function localStorageSupport() {
    try {
        return ('localStorage' in window) && window.localStorage !== null;
    } catch (e) {
        return false;
    }
}

var toplayerindex = null;

// 打开弹窗（兼容所有子页面 top.showDialog 调用）
function showDialog(id, title, url, width, height) {
    var w = '82%', h = '84%';
    if (width > 0) w = width + 'px';
    if (height > 0) h = height + 'px';
    var windowWidth = $(window).width();
    if (windowWidth <= 768) {
        w = '100%';
        h = '100%';
    }
    toplayerindex = layer.open({
        type: 2,
        title: title,
        id: id,
        shadeClose: false,
        shade: 0.55,
        maxmin: true,
        area: [w, h],
        content: url,
        cancel: function () {
            toplayerindex = null;
        }
    });
}

// 刷新当前激活选项卡 iframe
function refreshCurrentTab() {
    var current = $('.page-tabs-content .active').attr('data-id');
    var target = $('.J_iframe[data-id="' + current + '"]');
    if (!target.length) return;

    var url = target.attr('src');
    var loading = layer.load(2);
    target.one('load', function () {
        layer.close(loading);
    }).attr('src', url);
}
