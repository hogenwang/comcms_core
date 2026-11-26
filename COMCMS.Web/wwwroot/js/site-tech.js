(function (window, document, $) {
    'use strict';

    function showSiteToast(type, message) {
        var host = document.getElementById('techToastHost');
        if (!host) {
            host = document.createElement('div');
            host.id = 'techToastHost';
            host.className = 'tech-toast-host';
            document.body.appendChild(host);
        }
        host.textContent = '';

        var toast = document.createElement('div');
        toast.className = 'tech-toast is-' + (type || 'info');

        var icon = document.createElement('i');
        icon.className = 'fa ' + (type === 'success' ? 'fa-check-circle' : (type === 'error' ? 'fa-exclamation-circle' : 'fa-info-circle'));
        icon.style.color = type === 'success' ? '#34d399' : (type === 'error' ? '#fb7185' : '#38bdf8');

        var span = document.createElement('span');
        span.textContent = message || '';

        toast.appendChild(icon);
        toast.appendChild(span);
        host.appendChild(toast);

        window.clearTimeout(host._timer);
        host._timer = window.setTimeout(function () {
            host.textContent = '';
        }, 3000);
    }

    function submitGuestbookForm(formEl, payload) {
        var $form = $(formEl);
        var $btn = $form.find('button[type="submit"]');
        var origHtml = $btn.html();
        $btn.prop('disabled', true).html('<i class="fa fa-spinner fa-spin"></i> 提交中...');

        $.ajax({
            type: 'POST',
            url: '/Server/DoPostMessage',
            data: payload,
            dataType: 'json',
            success: function (data) {
                $btn.prop('disabled', false).html(origHtml);
                if (data && data.status === 'success') {
                    showSiteToast('success', data.message || '留言提交成功，感谢您的反馈！');
                    formEl.reset();
                    if (typeof window.getNewCode === 'function') {
                        window.getNewCode();
                    }
                } else {
                    showSiteToast('error', (data && data.message) || '提交失败，请检查输入信息。');
                }
            },
            error: function (xhr) {
                $btn.prop('disabled', false).html(origHtml);
                if (xhr && xhr.status === 429) {
                    showSiteToast('error', '提交过于频繁，请稍后再试。');
                } else {
                    showSiteToast('error', '提交失败，请稍后重试。');
                }
            }
        });
    }

    window.doPostFooterMessage = function () {
        var form = document.getElementById('footerMessageForm');
        if (!form) return false;
        var name = $.trim($('#name').val() || '');
        var phone = $.trim($('#phone').val() || '');
        var content = $.trim($('#content').val() || '');
        var token = $(form).find("input[name='__RequestVerificationToken']").first().val() || '';

        if (!name) { showSiteToast('error', '请输入您的姓名'); return false; }
        if (!phone) { showSiteToast('error', '请输入您的联系电话'); return false; }
        if (!content) { showSiteToast('error', '请填写留言内容'); return false; }

        submitGuestbookForm(form, {
            name: name,
            phone: phone,
            content: content,
            __RequestVerificationToken: token
        });
        return false;
    };

    window.doPostMessage = function () {
        var form = document.getElementById('msgForm');
        if (!form) return false;
        var name = $.trim($('#name_txt').val() || '');
        var phone = $.trim($('#tel_txt').val() || $('#phone_txt').val() || '');
        var content = $.trim($('#content_txt').val() || '');
        var code = $.trim($('#tel_code').val() || '');
        var token = $(form).find("input[name='__RequestVerificationToken']").first().val() || '';

        if (!name) { showSiteToast('error', '请输入您的姓名'); return false; }
        if (!phone) { showSiteToast('error', '请输入您的联系电话'); return false; }
        if (!content) { showSiteToast('error', '请填写留言内容'); return false; }

        submitGuestbookForm(form, {
            name: name,
            phone: phone,
            content: content,
            code: code,
            __RequestVerificationToken: token
        });
        return false;
    };

    $(function () {
        var $header = $('#siteHeader');
        function updateHeaderScroll() {
            $header.toggleClass('is-scrolled', $(window).scrollTop() > 24);
        }
        updateHeaderScroll();
        $(window).on('scroll', updateHeaderScroll);

        $('#mobileNavToggle').on('click', function () {
            var $wrap = $('#siteNavWrap');
            var open = $wrap.toggleClass('is-open').hasClass('is-open');
            $(this).attr('aria-expanded', open ? 'true' : 'false');
            $(this).find('i').attr('class', open ? 'fa fa-times' : 'fa fa-bars');
        });

        // News tabs
        $('.tech-tab-btn').on('click', function () {
            var target = $(this).data('tab');
            $(this).addClass('is-active').siblings('.tech-tab-btn').removeClass('is-active');
            $('.tech-news-panel').removeClass('is-active');
            $('#' + target).addClass('is-active');
        });

        // Hero showcase swiper
        if ($('.tech-hero-slider .swiper-slide').length > 1 && typeof window.Swiper === 'function') {
            new window.Swiper('.tech-hero-slider .swiper-container', {
                loop: true,
                autoplay: 4500,
                observer: true,
                observeParents: true,
                nextButton: '.tech-slider-next',
                prevButton: '.tech-slider-prev'
            });
        }
    });

    window.showSiteToast = showSiteToast;
})(window, document, window.jQuery);
