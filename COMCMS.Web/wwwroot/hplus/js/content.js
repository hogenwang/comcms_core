var $parentNode = window.parent ? window.parent.document : document;

function $childNode(name) {
    return window.frames[name];
}

$(function () {
    // Bootstrap 5 native Tooltip / Popover initialization (safe fallback)
    if (window.bootstrap) {
        document.querySelectorAll('[data-bs-toggle="tooltip"], [data-toggle="tooltip"]').forEach(function (el) {
            try { new window.bootstrap.Tooltip(el); } catch (e) { }
        });
        document.querySelectorAll('[data-bs-toggle="popover"], [data-toggle="popover"]').forEach(function (el) {
            try { new window.bootstrap.Popover(el); } catch (e) { }
        });
    }

    // 折叠 ibox
    $('.collapse-link').on('click', function () {
        var ibox = $(this).closest('div.ibox');
        var button = $(this).find('i');
        var content = ibox.find('div.ibox-content');
        content.slideToggle(200);
        button.toggleClass('fa-chevron-up').toggleClass('fa-chevron-down');
        ibox.toggleClass('border-bottom');
    });

    // 关闭 ibox
    $('.close-link').on('click', function () {
        $(this).closest('div.ibox').remove();
    });

    // 判断当前页面是否在 iframe 中
    if (top === window) {
        var gohome = '<div class="gohome"><a href="/AdminCP/Index/Main" title="返回首页"><i class="fa fa-home"></i></a></div>';
        $('body').append(gohome);
    }
});

function animationHover(element, animation) {
    element = $(element);
    element.hover(
        function () {
            element.addClass('animated ' + animation);
        },
        function () {
            window.setTimeout(function () {
                element.removeClass('animated ' + animation);
            }, 2000);
        });
}
