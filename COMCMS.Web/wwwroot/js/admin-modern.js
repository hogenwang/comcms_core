(function (window, document) {
    'use strict';

    var THEME_KEY = 'comcms_admin_theme';

    function normalizeTheme(theme) {
        return theme === 'dark' ? 'dark' : 'light';
    }

    function getSavedTheme() {
        try {
            var val = window.localStorage.getItem(THEME_KEY);
            if (val === 'dark' || val === 'light') return val;
        } catch (e) { }
        return 'light';
    }

    function applyThemeToDocument(doc, theme) {
        if (!doc || !doc.documentElement) return;
        var mode = normalizeTheme(theme);
        doc.documentElement.setAttribute('data-theme', mode);
        doc.documentElement.setAttribute('data-bs-theme', mode);
    }

    function updateThemeToggleButtons(theme) {
        var isDark = theme === 'dark';
        var buttons = document.querySelectorAll('[data-admin-theme-toggle]');
        for (var i = 0; i < buttons.length; i++) {
            var btn = buttons[i];
            btn.setAttribute('aria-pressed', isDark ? 'true' : 'false');
            btn.setAttribute('title', isDark ? '切换为浅色风格' : '切换为深色风格');
            var icon = btn.querySelector('i');
            var label = btn.querySelector('span');
            if (icon) {
                icon.className = isDark ? 'fa fa-sun-o' : 'fa fa-moon-o';
            }
            if (label) {
                label.textContent = isDark ? '浅色风格' : '深色风格';
            }
        }
    }

    function syncChildIframes(theme) {
        var iframes = document.querySelectorAll('iframe');
        for (var i = 0; i < iframes.length; i++) {
            try {
                var childWin = iframes[i].contentWindow;
                if (childWin && childWin.document) {
                    applyThemeToDocument(childWin.document, theme);
                    if (childWin.COMCMSTheme && typeof childWin.COMCMSTheme.applyOnly === 'function') {
                        childWin.COMCMSTheme.applyOnly(theme);
                    }
                }
            } catch (e) { }
        }
    }

    function setTheme(theme) {
        var mode = normalizeTheme(theme);
        try {
            window.localStorage.setItem(THEME_KEY, mode);
        } catch (e) { }
        applyThemeToDocument(document, mode);
        updateThemeToggleButtons(mode);
        syncChildIframes(mode);
        return mode;
    }

    function applyOnly(theme) {
        var mode = normalizeTheme(theme);
        applyThemeToDocument(document, mode);
        updateThemeToggleButtons(mode);
        syncChildIframes(mode);
    }

    function toggleTheme() {
        var current = document.documentElement.getAttribute('data-theme') || getSavedTheme();
        return setTheme(current === 'dark' ? 'light' : 'dark');
    }

    // Apply immediately before DOM paint to prevent theme flicker
    var initialTheme = getSavedTheme();
    applyThemeToDocument(document, initialTheme);

    window.COMCMSTheme = {
        get: getSavedTheme,
        set: setTheme,
        applyOnly: applyOnly,
        toggle: toggleTheme
    };

    window.addEventListener('storage', function (e) {
        if (e && e.key === THEME_KEY && e.newValue) {
            applyOnly(e.newValue);
        }
    });

    // jQuery-level Bootstrap 5 + Modern Plugin Bridges
    function initJQueryBridges() {
        var $ = window.jQuery;
        if (!$) return;

        // 1. Bootstrap-Table v1.22.6 default Font Awesome icons (replaces BS3 Glyphicons)
        if ($.fn.bootstrapTable && $.fn.bootstrapTable.defaults) {
            $.extend($.fn.bootstrapTable.defaults, {
                iconsPrefix: 'fa',
                icons: {
                    paginationSwitchDown: 'fa-caret-square-o-down',
                    paginationSwitchUp: 'fa-caret-square-o-up',
                    refresh: 'fa-refresh',
                    toggleOff: 'fa-toggle-off',
                    toggleOn: 'fa-toggle-on',
                    columns: 'fa-th-list',
                    detailOpen: 'fa-plus',
                    detailClose: 'fa-minus',
                    fullscreen: 'fa-arrows-alt',
                    search: 'fa-search',
                    clearSearch: 'fa-trash-o'
                }
            });
        }

        // 2. TreeGrid default Font Awesome expander icons
        if ($.fn.treegrid && $.fn.treegrid.defaults) {
            $.extend($.fn.treegrid.defaults, {
                expanderExpandedClass: 'fa fa-angle-down',
                expanderCollapsedClass: 'fa fa-angle-right'
            });
        }

        // 3. Lightweight iCheck compatibility shim (uses modern native CSS checkboxes/radios)
        if (!$.fn.iCheck) {
            $.fn.iCheck = function (action) {
                return this.each(function () {
                    var $el = $(this);
                    var $inputs = $el.is('input') ? $el : $el.find('input[type="checkbox"], input[type="radio"]');
                    if (typeof action === 'string') {
                        if (action === 'check') $inputs.prop('checked', true).trigger('change');
                        else if (action === 'uncheck') $inputs.prop('checked', false).trigger('change');
                        else if (action === 'toggle') $inputs.each(function () { this.checked = !this.checked; }).trigger('change');
                        else if (action === 'disable') $inputs.prop('disabled', true);
                        else if (action === 'enable') $inputs.prop('disabled', false);
                        return;
                    }
                    $inputs.off('.admICheck').on('change.admICheck', function () {
                        var $inp = $(this);
                        $inp.trigger('ifChanged');
                        $inp.trigger(this.checked ? 'ifChecked' : 'ifUnchecked');
                    });
                });
            };
        }

        // 4. Lightweight native colorpicker replacement for $("#TitleColor").colorpicker()
        $.fn.colorpicker = function () {
            return this.each(function () {
                var $input = $(this);
                if ($input.data('admColorInit')) return;
                $input.data('admColorInit', true);

                var initialVal = $.trim($input.val() || '');
                var hexPattern = /^#[0-9a-fA-F]{6}$/;
                var pickerVal = hexPattern.test(initialVal) ? initialVal : '#0f172a';

                var $wrap = $('<div class="adm-color-field"></div>');
                var $swatch = $('<input type="color" class="adm-color-swatch" title="选择颜色" />').val(pickerVal);

                $input.before($wrap);
                $wrap.append($swatch).append($input);

                $swatch.on('input change', function () {
                    $input.val($(this).val()).trigger('change');
                });

                $input.on('input change', function () {
                    var v = $.trim($(this).val() || '');
                    if (hexPattern.test(v)) {
                        $swatch.val(v);
                    }
                });
            });
        };

        // 5. DOM-ready theme toggle wiring & BS3 -> BS5 data-toggle / nav-tabs bridge
        $(function () {
            updateThemeToggleButtons(getSavedTheme());

            $(document).on('click', '[data-admin-theme-toggle]', function (e) {
                e.preventDefault();
                toggleTheme();
            });

            // Upgrade any residual data-toggle / data-dismiss attributes dynamically
            $('[data-toggle]').each(function () {
                var $el = $(this);
                var val = $el.attr('data-toggle');
                if (val && !$el.attr('data-bs-toggle')) {
                    $el.attr('data-bs-toggle', val);
                }
            });

            $('[data-dismiss]').each(function () {
                var $el = $(this);
                var val = $el.attr('data-dismiss');
                if (val && !$el.attr('data-bs-dismiss')) {
                    $el.attr('data-bs-dismiss', val);
                }
            });

            // Ensure BS3-style .nav-tabs > li.active > a stays synced with BS5 Tab API
            $('.nav-tabs').each(function () {
                var $nav = $(this);
                $nav.find('> li.active > a').addClass('active');
                $nav.on('click', '> li > a[data-bs-toggle="tab"], > li > a[data-toggle="tab"]', function (e) {
                    e.preventDefault();
                    var $link = $(this);
                    var target = $link.attr('href') || $link.attr('data-bs-target');
                    $link.closest('ul').find('> li').removeClass('active');
                    $link.closest('ul').find('> li > a').removeClass('active');
                    $link.addClass('active').closest('li').addClass('active');
                    if (target && target.charAt(0) === '#') {
                        var $pane = $(target);
                        if ($pane.length) {
                            $pane.siblings('.tab-pane').removeClass('active show');
                            $pane.addClass('active show');
                        }
                    }
                });
            });

            // Sync theme into newly loaded iframes
            $(document).on('load', 'iframe', function () {
                syncChildIframes(getSavedTheme());
            });
        });
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', function () {
            updateThemeToggleButtons(getSavedTheme());
            initJQueryBridges();
        });
    } else {
        updateThemeToggleButtons(getSavedTheme());
        initJQueryBridges();
    }
})(window, document);
