$(function () {
    // 计算元素集合的总宽度
    function calSumWidth(elements) {
        var width = 0;
        $(elements).each(function () {
            width += $(this).outerWidth(true);
        });
        return width;
    }

    // 规范化路由地址便于匹配菜单与选项卡
    function normalizeMenuUrl(url) {
        if (!url) return '';
        var normalized = $.trim(String(url)).toLowerCase();
        var hashIdx = normalized.indexOf('#');
        if (hashIdx > -1) normalized = normalized.substring(0, hashIdx);
        if (normalized.length > 1 && normalized.charAt(normalized.length - 1) === '/') {
            normalized = normalized.slice(0, -1);
        }
        return normalized;
    }

    // 联动左侧 #side-menu（metisMenu v3）：展开对应父菜单、折叠其他分支并高亮当前菜单项
    function syncSideMenu(targetUrl) {
        var $sideMenu = $('#side-menu');
        if ($sideMenu.length === 0) return;

        var normTarget = normalizeMenuUrl(targetUrl);

        // 移除残留焦点，防止已切换走的菜单项因 :focus 继续显示白色高亮
        $sideMenu.find('a:focus').blur();

        // 查找匹配的可见菜单项
        var $matched = $();
        if (normTarget) {
            $sideMenu.find('a.J_menuItem').each(function () {
                var $a = $(this);
                if ($a.closest('li[style*="display:none"], li[style*="display: none"]').length > 0) {
                    return;
                }
                if (normalizeMenuUrl($a.attr('href')) === normTarget) {
                    $matched = $a;
                    return false;
                }
            });
        }

        // 先清理所有叶子项和父项的选中状态
        $sideMenu.find('li').not('.nav-header').removeClass('active mm-active');
        $sideMenu.find('a.J_menuItem').removeClass('active');

        if ($matched.length === 0) {
            // 若当前激活页不在左侧菜单（例如下拉菜单里的“修改密码”），收起所有展开的子菜单
            $sideMenu.find('ul.mm-collapse.mm-show').removeClass('mm-show').css('height', '');
            $sideMenu.find('a[aria-expanded="true"]').attr('aria-expanded', 'false');
            return;
        }

        var $leafLi = $matched.closest('li');
        var $ancestorLis = $matched.parentsUntil('#side-menu', 'li');
        var $ancestorUls = $matched.parentsUntil('#side-menu', 'ul');

        // 收起非当前分支的其他展开菜单（与 metisMenu 手风琴模式一致）
        $sideMenu.find('ul.mm-collapse.mm-show').not($ancestorUls).each(function () {
            $(this).removeClass('mm-show').css('height', '');
            $(this).siblings('a').attr('aria-expanded', 'false');
        });

        // 展开当前菜单所在的所有父级菜单
        $ancestorUls.each(function () {
            var $ul = $(this);
            $ul.addClass('mm-collapse mm-show').css('height', '');
            $ul.siblings('a').attr('aria-expanded', 'true');
        });

        // 高亮父级与当前叶子菜单项
        $ancestorLis.addClass('mm-active active');
        $leafLi.addClass('mm-active active');
        $matched.addClass('active');

        // 若侧边栏存在滚动条，自动滚动到当前高亮菜单项可见区域
        var $scrollContainer = $('.sidebar-collapse');
        if ($scrollContainer.length > 0 && $leafLi.length > 0 && $leafLi.is(':visible')) {
            var containerTop = $scrollContainer.offset().top;
            var containerHeight = $scrollContainer.height();
            var itemTop = $leafLi.offset().top;
            if (itemTop < containerTop + 20 || itemTop > containerTop + containerHeight - 40) {
                var targetScroll = $scrollContainer.scrollTop() + (itemTop - containerTop) - containerHeight / 2 + 20;
                $scrollContainer.stop(true).animate({ scrollTop: Math.max(0, targetScroll) }, 180);
            }
        }
    }

    // 滚动到指定选项卡
    function scrollToTab(element) {
        if (!element || $(element).length === 0) return;
        var marginLeftVal = calSumWidth($(element).prevAll()), marginRightVal = calSumWidth($(element).nextAll());
        var tabOuterWidth = calSumWidth($(".content-tabs").children().not(".J_menuTabs"));
        var visibleWidth = $(".content-tabs").outerWidth(true) - tabOuterWidth;
        var scrollVal = 0;
        if ($(".page-tabs-content").outerWidth() < visibleWidth) {
            scrollVal = 0;
        } else if (marginRightVal <= (visibleWidth - $(element).outerWidth(true) - $(element).next().outerWidth(true))) {
            if ((visibleWidth - $(element).next().outerWidth(true)) > marginRightVal) {
                scrollVal = marginLeftVal;
                var tabElement = element;
                while ((scrollVal - $(tabElement).outerWidth()) > ($(".page-tabs-content").outerWidth() - visibleWidth)) {
                    scrollVal -= $(tabElement).prev().outerWidth();
                    tabElement = $(tabElement).prev();
                }
            }
        } else if (marginLeftVal > (visibleWidth - $(element).outerWidth(true) - $(element).prev().outerWidth(true))) {
            scrollVal = marginLeftVal - $(element).prev().outerWidth(true);
        }
        $('.page-tabs-content').animate({
            marginLeft: 0 - scrollVal + 'px'
        }, "fast");
    }

    // 查看左侧隐藏的选项卡
    function scrollTabLeft() {
        var marginLeftVal = Math.abs(parseInt($('.page-tabs-content').css('margin-left'), 10) || 0);
        var tabOuterWidth = calSumWidth($(".content-tabs").children().not(".J_menuTabs"));
        var visibleWidth = $(".content-tabs").outerWidth(true) - tabOuterWidth;
        var scrollVal = 0;
        if ($(".page-tabs-content").width() < visibleWidth) {
            return false;
        } else {
            var tabElement = $(".J_menuTab:first");
            var offsetVal = 0;
            while ((offsetVal + $(tabElement).outerWidth(true)) <= marginLeftVal) {
                offsetVal += $(tabElement).outerWidth(true);
                tabElement = $(tabElement).next();
            }
            offsetVal = 0;
            if (calSumWidth($(tabElement).prevAll()) > visibleWidth) {
                while ((offsetVal + $(tabElement).outerWidth(true)) < visibleWidth && tabElement.length > 0) {
                    offsetVal += $(tabElement).outerWidth(true);
                    tabElement = $(tabElement).prev();
                }
                scrollVal = calSumWidth($(tabElement).prevAll());
            }
        }
        $('.page-tabs-content').animate({
            marginLeft: 0 - scrollVal + 'px'
        }, "fast");
    }

    // 查看右侧隐藏的选项卡
    function scrollTabRight() {
        var marginLeftVal = Math.abs(parseInt($('.page-tabs-content').css('margin-left'), 10) || 0);
        var tabOuterWidth = calSumWidth($(".content-tabs").children().not(".J_menuTabs"));
        var visibleWidth = $(".content-tabs").outerWidth(true) - tabOuterWidth;
        var scrollVal = 0;
        if ($(".page-tabs-content").width() < visibleWidth) {
            return false;
        } else {
            var tabElement = $(".J_menuTab:first");
            var offsetVal = 0;
            while ((offsetVal + $(tabElement).outerWidth(true)) <= marginLeftVal) {
                offsetVal += $(tabElement).outerWidth(true);
                tabElement = $(tabElement).next();
            }
            offsetVal = 0;
            while ((offsetVal + $(tabElement).outerWidth(true)) < visibleWidth && tabElement.length > 0) {
                offsetVal += $(tabElement).outerWidth(true);
                tabElement = $(tabElement).next();
            }
            scrollVal = calSumWidth($(tabElement).prevAll());
            if (scrollVal > 0) {
                $('.page-tabs-content').animate({
                    marginLeft: 0 - scrollVal + 'px'
                }, "fast");
            }
        }
    }

    // 通过遍历给菜单项加上 data-index 属性
    $(".J_menuItem").each(function (index) {
        if (!$(this).attr('data-index')) {
            $(this).attr('data-index', index);
        }
    });

    function menuItem(event) {
        event.preventDefault();

        var dataUrl = $(this).attr('href'),
            dataIndex = $(this).data('index'),
            menuName = $.trim($(this).text()),
            flag = true;
        if (dataUrl === undefined || $.trim(dataUrl).length === 0) return false;

        // 选项卡菜单已存在
        $('.J_menuTab').each(function () {
            if (normalizeMenuUrl($(this).data('id')) === normalizeMenuUrl(dataUrl)) {
                if (!$(this).hasClass('active')) {
                    $(this).addClass('active').siblings('.J_menuTab').removeClass('active');
                    scrollToTab(this);
                    $('.J_mainContent .J_iframe').each(function () {
                        if (normalizeMenuUrl($(this).data('id')) === normalizeMenuUrl(dataUrl)) {
                            $(this).show().siblings('.J_iframe').hide();
                            return false;
                        }
                    });
                }
                flag = false;
                return false;
            }
        });

        // 选项卡菜单不存在
        if (flag) {
            var $tab = $('<a href="javascript:;" class="active J_menuTab"></a>')
                .attr('data-id', dataUrl)
                .text(menuName + ' ')
                .append('<i class="fa fa-times-circle"></i>');
            $('.J_menuTab').removeClass('active');

            var loading = layer.load();
            var iframe = $('<iframe>', {
                'class': 'J_iframe',
                name: 'iframe' + dataIndex,
                width: '100%',
                height: '100%',
                src: dataUrl,
                frameborder: '0',
                'data-id': dataUrl,
                seamless: 'seamless'
            }).one('load', function () {
                layer.close(loading);
            });

            $('.J_mainContent').find('iframe.J_iframe').hide();
            $('.J_mainContent').append(iframe);
            $('.J_menuTabs .page-tabs-content').append($tab);
            scrollToTab($('.J_menuTab.active'));
        }

        // 同步左侧菜单激活状态
        syncSideMenu(dataUrl);
        return false;
    }

    $('.J_menuItem').on('click', menuItem);

    // 关闭选项卡菜单
    function closeTab() {
        var $closingTab = $(this).parents('.J_menuTab');
        var closeTabId = $closingTab.data('id');
        var currentWidth = $closingTab.width();
        var nextActiveId = null;

        if ($closingTab.hasClass('active')) {
            var $nextTab = $closingTab.next('.J_menuTab');
            var $prevTab = $closingTab.prev('.J_menuTab');

            if ($nextTab.length) {
                nextActiveId = $nextTab.data('id');
                $nextTab.addClass('active');

                $('.J_mainContent .J_iframe').each(function () {
                    if ($(this).data('id') == nextActiveId) {
                        $(this).show().siblings('.J_iframe').hide();
                        return false;
                    }
                });

                var marginLeftVal = parseInt($('.page-tabs-content').css('margin-left'), 10) || 0;
                if (marginLeftVal < 0) {
                    $('.page-tabs-content').animate({
                        marginLeft: (marginLeftVal + currentWidth) + 'px'
                    }, "fast");
                }
            } else if ($prevTab.length) {
                nextActiveId = $prevTab.data('id');
                $prevTab.addClass('active');

                $('.J_mainContent .J_iframe').each(function () {
                    if ($(this).data('id') == nextActiveId) {
                        $(this).show().siblings('.J_iframe').hide();
                        return false;
                    }
                });
            }

            $closingTab.remove();
            $('.J_mainContent .J_iframe').each(function () {
                if ($(this).data('id') == closeTabId) {
                    $(this).remove();
                    return false;
                }
            });

            if (nextActiveId !== null) {
                scrollToTab($('.J_menuTab.active'));
                syncSideMenu(nextActiveId);
            }
        } else {
            $closingTab.remove();
            $('.J_mainContent .J_iframe').each(function () {
                if ($(this).data('id') == closeTabId) {
                    $(this).remove();
                    return false;
                }
            });
            scrollToTab($('.J_menuTab.active'));
        }
        return false;
    }

    $('.J_menuTabs').on('click', '.J_menuTab i', closeTab);

    // 关闭其他选项卡
    function closeOtherTabs() {
        $('.page-tabs-content').children("[data-id]").not(":first").not(".active").each(function () {
            $('.J_iframe[data-id="' + $(this).data('id') + '"]').remove();
            $(this).remove();
        });
        $('.page-tabs-content').css("margin-left", "0");
        syncSideMenu($('.J_menuTab.active').data('id'));
    }
    $('.J_tabCloseOther').on('click', closeOtherTabs);

    // 滚动到已激活的选项卡
    function showActiveTab() {
        var $active = $('.J_menuTab.active');
        scrollToTab($active);
        syncSideMenu($active.data('id'));
    }
    $('.J_tabShowActive').on('click', showActiveTab);

    // 点击选项卡菜单
    function activeTab() {
        var currentId = $(this).data('id');
        if (!$(this).hasClass('active')) {
            $('.J_mainContent .J_iframe').each(function () {
                if ($(this).data('id') == currentId) {
                    $(this).show().siblings('.J_iframe').hide();
                    return false;
                }
            });
            $(this).addClass('active').siblings('.J_menuTab').removeClass('active');
            scrollToTab(this);
        }
        // 联动同步左侧导航菜单激活项
        syncSideMenu(currentId);
    }

    $('.J_menuTabs').on('click', '.J_menuTab', activeTab);

    // 双击刷新 iframe
    function refreshTab() {
        var target = $('.J_iframe[data-id="' + $(this).data('id') + '"]');
        var url = target.attr('src');
        var loading = layer.load();
        target.one('load', function () {
            layer.close(loading);
        }).attr('src', url);
    }

    $('.J_menuTabs').on('dblclick', '.J_menuTab', refreshTab);

    $('.J_tabLeft').on('click', scrollTabLeft);
    $('.J_tabRight').on('click', scrollTabRight);

    // 关闭全部
    $('.J_tabCloseAll').on('click', function () {
        $('.page-tabs-content').children("[data-id]").not(":first").each(function () {
            $('.J_iframe[data-id="' + $(this).data('id') + '"]').remove();
            $(this).remove();
        });
        var firstId = null;
        $('.page-tabs-content').children("[data-id]:first").each(function () {
            firstId = $(this).data('id');
            $('.J_iframe[data-id="' + firstId + '"]').show();
            $(this).addClass("active");
        });
        $('.page-tabs-content').css("margin-left", "0");
        if (firstId) {
            syncSideMenu(firstId);
        }
    });

    // 暴露全局方法，便于 iframe 内跳转后也能同步侧边栏
    window.syncAdminSideMenu = syncSideMenu;

    // 初始加载时同步默认激活的“首页”菜单
    var initialTabId = $('.J_menuTab.active').data('id');
    if (initialTabId) {
        syncSideMenu(initialTabId);
    }
});
