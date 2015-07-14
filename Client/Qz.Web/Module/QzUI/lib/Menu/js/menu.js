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
        title: {
            txt: 'Module 导航',
            dom: ''
        },
        setting: {
            theme: 'accordion',
            multiple: false
        },
        parent: '',
        page: {},
        ajax: {
            url: '/Common/Build',
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
        page:{},
        config: {},
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

                break;
            case 'accordion':
            default:
                $api.prepend(this.config.title.dom, '<div class="menu-title"><i class="icon icon-nav"></i><span>' + this.config.title.txt + '</span><i class="icon icon-sys-menu"></i></div>');
                break;
        }

        var html = [],
            data = {},
            menuType = 'child',
            fname;

        html.push('<li data-id="0" data-sub-menu="0" data-url="/" onclick=""><i class="icon icon-home"></i><span class="title">首页</span></li>');
        html.push('<li data-id="1" data-sub-menu="1" data-open="0" onclick=""><i class="icon icon-manage"></i><span class="title">系统管理</span><i class="icon icon-open"></i><ul style="" class="dn"> <li data-id="1" onclick="" data-sub-menu="0"  data-url="/Application"><i class="icon icon-sys-app"></i><span class="title">系统应用</span></li> <li data-id="1" class="" onclick="" data-url="/SysLog"><i class="icon icon-sys-log"></i><span class="title">系统日志</span></li> <li data-id="1" class="" onclick="" data-url="/DataCode"><i class="icon icon-data-code"></i><span class="title">数据字典</span></li> <li data-id="1" class="" onclick="" data-url="/DbManage"><i class="icon icon-db-manage"></i><span class="title">数据库管理</span></li></ul></li>');
        html.push('<li data-id="2" data-sub-menu="1" data-open="0" onclick=""><i class="icon icon-sys-setting"></i><span class="title">系统设置</span><i class="icon icon-open"></i><ul style=" " class="dn"> <li data-id="1" onclick="" data-sub-menu="0" data-url="/Module"><i class="icon icon-module-manage"></i><span class="title">模块管理</span></li> <li data-id="1" class="" onclick="" data-url="/Company"><i class="icon icon-company-manage"></i><span class="title">公司管理</span></li> <li data-id="1" class="" onclick="" data-sub-menu="0" data-url="/Department"><i class="icon icon-dept-manage"></i><span class="title">部门管理</span></li> <li data-id="1" class="" onclick="" data-url="/Role"><i class="icon icon-role-manage"></i><span class="title">角色管理</span></li><li data-id="1" class="" onclick="" data-url="/User"><i class="icon icon-user-manage"></i><span class="title">用户管理</span></li></ul></li>');

        //for (var i = 0; i < rs.Obj.Data.length; i++) {
        //    data = rs.Obj.Data[i];
        //    if (type == 'build') {
        //        if (data.ParentId == '0') {
        //            menuType = 'root';
        //            fname = data.Name;
        //        } else {
        //            fname = $api.data(obj, 'fname') + data.Name;
        //        }
        //        html.push('<li data-id="' + data.Id + '" data-code="' + data.Code + '" data-parentId="' + data.ParentId + '" data-fname="' + fname + '" data-type="' + menuType + '" onclick="menu.select(this)"><i class="icon icon-spread" onclick="menu.spread(this)"></i><span class="txt">' + data.Name + '</span></li>');
        //    } else
        //        html.push('<li data-id="' + data.Key + '" onclick="box.itemClick(this);">' + data.Value + '</li>');
        //}

        this.box.body('<ul>' + html.join('') + '</ul>');

        $("li", this.dom).bind('click', function () {

            // 阻止事件冒泡前， 触发页面全局事件 
            _menu.page.global.click();

            // 阻止事件冒泡
            var e = event || window.event; if (e && e.stopPropagation) { e.stopPropagation(); } else { e.cancelBubble = true; }

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

                    if (typeof _menu.config.shrinkCallback === 'function') _menu.config.shrinkCallback(_menu, _this);
                } else {
                    // 展开 
                    _menu.spread.call(_this);

                    if (typeof _menu.config.spreadCallback === 'function') _menu.config.spreadCallback(_menu, _this);
                }

                return;
            }

            // 选中 
            _menu.select(_this);
        });

        if (typeof this.config.initCallback === 'function') this.config.initCallback(this);

        $('li:eq(0)', this.dom).click();
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
