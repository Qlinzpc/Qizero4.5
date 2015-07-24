/**
 * QzUI.js.lib.menu.js 
 * @Author ZPC 
 * @Date 2015-4-16 11:23:52 
 * @Version 1.0 
 * 
 */
(function (o, $) {

    "use strict";

    var DEFAULTS = {
        id: 'meun',
        show: false,
        data: {},
        title: {
            txt: 'Module 导航',
            icon: 'icon-nav',
            dom: ''
        },
        setting: {
            theme: 'accordion',
            multiple: false
        },
        toggle: {
            click: function () { }
        },
        parent: '',
        page: {},
        ajax: {
            url: '',
            data: ''
        },
        callback: function () { },
        initCallback: function () { },
        createCallback: function () { },
        shrinkCallback: function () { },
        spreadCallback: function () { },
        selectCallback: function () { },
        xCallback: function () { }
    };

    function Menu(opt) {
        this.config = $.extend({}, DEFAULTS, opt);

        this.create();

        this.init();
    }

    Menu.prototype = {
        dom: '',
        page: {},
        config: {},
        toggle: {
            dom: null
        },
        box: {}
    };

    // 创建 
    Menu.prototype.create = function () {

        var _menu = this,
            _target;

        this.config.parent = this.config.parent || $api.dom('body');

        if (typeof this.config.id === 'string') {
            _target = $api.byId(this.config.id) || $api.dom(this.config.id);
        }

        if (!_target) {
            $api.append(this.config.parent, '<div class="menu" id="' + this.config.id + '"></div>');

            _target = $api.byId(this.config.id) || $api.dom(this.config.id);
        }

        this.dom = _target;

        this.box = new Box({
            id: 'box-' + this.config.id,
            title: this.config.title,
            header: false,
            show: this.config.show,
            mask: false,
            close: false,
            parent: this.config.id,
            callback: this.config.callback
        });

        if (typeof this.config.createCallback === 'function') this.config.createCallback(this);
    };

    // 初始化 
    Menu.prototype.init = function () {

        var _menu = this;

        _menu.page = this.config.page;

        switch (this.config.setting.theme) {
            case 'windows':
                $api.prepend(this.config.title.dom, '<div class="menu-title"><i class="icon {icon}"></i><span>{txt}</span></div>'.format(this.config.title));

                break;
            case 'accordion':
            default:
                $api.prepend(this.config.title.dom, '<div class="menu-title"><i class="icon {icon}"></i><span>{txt}</span><i class="icon icon-sys-menu"></i></div>'.format(this.config.title));

                this.toggle.dom = $api.dom(this.config.title.dom, '.icon-sys-menu');
                $api.addEvt(this.toggle.dom, 'click', function () {
                    $api.trigger(_menu.config.toggle.click());
                });
                break;
        }

        this.bind(this.box.body(), this.config.data);

        if (typeof this.config.initCallback === 'function') this.config.initCallback(this);

        $api.dom(this.dom, 'li:first-child').click();

    };

    // 绑定 
    Menu.prototype.bind = function (target, data) {
        var html = [];

        if (data && $api.isArray(data)) {
            data.forEach(function (_) {

                _.Badge = (_.SubMenu > 0 ? '<i class="icon icon-open"></i><span class="badge">' + _.SubMenu + '<span>' : "");

                html.push('<li data-id="{Id}" data-open="0" data-sub-menu="{SubMenu}" data-url="{URL}"><i class="icon icon-{Icon}"></i><span class="title">{Name}</span>{Badge}</li>'.format(_));
            });
        }

        $api.append(target, '<ul>' + html.join('') + '</ul>');
        $api.data(target, 'bind', '1');

        this.event($api.domAll(target, 'li'));
    };

    // 事件  
    Menu.prototype.event = function (target) {

        var _menu = this;

        $(target).click(function (e) {

            // 阻止事件冒泡前， 触发页面全局事件 
            // _menu.page.global.click();

            // 阻止事件冒泡
            $api.stop.event(e);

            var _this = $(this),
                subMenu = _this.data('sub-menu');

            if (subMenu && subMenu != '0') {

                var _open = $('li[data-open=1]');

                if (_open.length >= 1 && _this.attr('data-open') == '0' && !_menu.config.setting.multiple) {
                    _menu.shrink.call(_open);
                }

                if (_this.attr('data-open') != '0') {
                    // 收缩  
                    _menu.shrink.call(_this);

                    if (typeof _menu.config.shrinkCallback === 'function') _menu.config.shrinkCallback(_menu, _this.get(0));
                } else {
                    // 展开 
                    _menu.spread.call(_this);

                    if (typeof _menu.config.spreadCallback === 'function') _menu.config.spreadCallback(_menu, _this.get(0));
                }

                return;
            }

            // 选中 
            _menu.select(_this);
        });
    };

    // 收缩 
    Menu.prototype.shrink = function () {
        $('ul:eq(0)', this).slideUp('500');
        $('.icon-open', this).css({ transform: 'rotate(0deg)', transition: '0.5s' });
        this.attr('data-open', '0');
    };

    // 展开 
    Menu.prototype.spread = function () {
        $('ul:eq(0)', this).slideDown('500');
        $('.icon-open', this).css({ transform: 'rotate(90deg)', transition: '0.5s' });
        this.attr('data-open', '1');
    };

    // 获取或设置 Menu 选中 
    Menu.prototype.select = function (_this) {

        if (_this) {

            $('.active', this.dom).removeClass('active');
            _this.addClass('active');

            if (typeof this.config.selectCallback === 'function') this.config.selectCallback(this, _this);

        } else {

            _this = $('.active', this.dom);
        }
        return _this;
    };

    // Message 提示信息 
    Menu.prototype.msg = function () { };

    o.Menu = Menu;

})(window, jQuery);
