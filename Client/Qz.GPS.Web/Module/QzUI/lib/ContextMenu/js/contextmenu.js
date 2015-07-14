/**
 * QzUI.js.lib.contextmenu.js 
 * @Author ZPC 
 * @Date 2015-4-16 11:23:52 
 * @Version 1.0 
 * 
 */
(function (o, $)
{

    "use strict";

    var DEFAULTS = {
        id: 'context-menu',
        show: false,
        setting: {

        },
        parent: '',
        page: {},
        callback: function () { },
        initCallback: function () { },
        createCallback: function () { },
        addCallback: function () { },
        removeCallback: function () { },
        selectCallback: function () { },
        xCallback: function () { }
    },

        A = '';

    // ContextMenu 菜单 
    function ContextMenu(opt)
    {

        this.config = $.extend({}, DEFAULTS, opt);

        this.create();

        this.init();
    }

    // ContextMenu 属性 
    ContextMenu.prototype = {
        config: {},
        dom: {},
        current: {},
        page: {},
        children: [],
        container: [],
        contextmenu: null // 右键菜单 
    };

    // 创建 ContextMenu 菜单 
    ContextMenu.prototype.create = function ()
    {

        var _target;

        this.config.parent = this.config.parent || $api.dom('body');

        if (typeof this.config.id === 'string')
        {
            _target = $api.byId(this.config.id) || $api.dom(this.config.id);
        }

        if (!_target)
        {
            var _html = '<ul class="tab" id="' + this.config.id + '"></ul>';

            $api.append(this.config.parent, _html);

            _target = $api.byId(this.config.id) || $api.dom(this.config.id);
        }

        this.dom = _target;

        if (typeof this.config.createCallback === 'function') this.config.createCallback(this);

        if (!this.contextmenu)
        {

            this.contextmenu = $('<ul class="context-menu"><li data-name="refresh"><i class="icon icon-refresh"></i>刷新</li><li data-name="refresh" class="disabled"><i class="icon icon-close"></i>打印</li><li class="split-line"></li><li data-name="close"><i class="icon icon-close"></i>关闭</li><li data-name="closeAll"><i class="icon icon-refresh"></i>关闭所有</li><li data-name="closeAllOther"><i class="icon icon-refresh"></i>除此之外全部关闭</li></ul>');
        }

        $(this.config.parent).append(this.contextmenu);
    };

    // 初始化 ContextMenu 菜单 
    ContextMenu.prototype.init = function ()
    {
        var _contextMenu = this;

        this.page = this.config.page;

        $('li', this.contextmenu).click(function ()
        {
            // 阻止事件冒泡
            $api.stop.event();

            var _ = $(this);
            var _current = _contextMenu.current.get(0);

            if (/disabled/i.test(_.attr('class'))) return;

            log($(this).html());

            switch (_.data('name'))
            {
                case "refresh":

                    break;
                case "close":

                    _contextMenu.remove(_current);
                    break;
                case "closeAll":

                    _contextMenu.remove(_current, true, [_contextMenu.children[0]]);
                    break;
                case "closeAllOther":

                    _contextMenu.remove(_current, true, [_contextMenu.children[0], _current]);
                    break;
            }

            log(_.data('name'));

            _contextMenu.contextmenu.hide();
        });

    };

    // 指定 Sub ContextMenu 绑定事件 
    ContextMenu.prototype.bindEvent = function (_this)
    {

        var _contextMenu = this;

        $(_this).bind('click', function ()
        {

            if ($api.hasCls(this, 'select')) return;

            log(this);

            _contextMenu.select(this);
        });

        $('.icon-tab-close', _this).bind('click', function ()
        {
            // 阻止事件冒泡前， 触发页面全局事件 
            _contextMenu.page.global.click();

            // 阻止事件冒泡
            $api.stop.event();

            log(this.parentNode);

            _contextMenu.close(this.parentNode);
        });

        // 屏蔽系统右键菜单  
        $(_this).bind('contextmenu', function (e)
        {
            return false;
        });

        $(_this).mousedown(function (e)
        {
            var _ = $(this),
                offset = _.offset();

            // 1 = 鼠标左键  2 = 鼠标中键  3 = 鼠标右键 
            switch (e.which)
            {
                case 1:

                    log('鼠标左键');
                    break;
                case 2:


                    log('鼠标中键');
                    break;
                case 3:

                    _contextMenu.contextmenu.css({
                        left: e.pageX + 5,
                        top: e.pageY + 5,
                        display: 'block'
                    });

                    _contextMenu.current = _;

                    log('鼠标右键');

                    // 页面全局点击 事件 
                    _contextMenu.page.global.click(function ()
                    {
                        // 隐藏右键菜单 
                        _contextMenu.contextmenu.hide();
                    });

                    return false;
                default:

                    break;
            }
        });

    };

    // 获取或设置 Sub ContextMenu 选中 
    ContextMenu.prototype.select = function (_this)
    {

        if (_this)
        {
            $(this.container[this.children.indexOf($('li[class=select]', this.dom).get(0))]).addClass('dn');
            $('li[class=select]', this.dom).removeClass('select');

            $(_this).addClass('select');
            $(this.container[this.children.indexOf(_this)]).removeClass('dn');

            if (typeof this.config.selectCallback === 'function') this.config.selectCallback(this, _this);
        } else
        {

            _this = $('li[class=select]', this.dom);
        }

        this.current = $(_this);

        return _this;
    };

    // 添加 Sub ContextMenu  
    ContextMenu.prototype.add = function (_html, _url)
    {

        var _this = $('<li class></li>');

        if (_html)
        {

            if (this.children.length >= 1)
            {
                _html += '<i class="icon icon-tab-close"></i>';
            }

            _this.html(_html);
        }

        var _ = _this.get(0);

        if (this.exists(_, true))
        {

            return _this;
        }

        var _container = $("<div></div>");
        _container.html(_url).addClass('tab-content dn');

        // 向 主内容 追加 Container 容器 
        this.page.main().append(_container);

        this.children.push(_);
        this.container.push(_container.get(0));

        $(this.dom).append(_);
        this.select(_);

        // 绑定事件 
        this.bindEvent(_);

        return _this;
    };

    // 关闭指定 Sub ContextMenu  
    ContextMenu.prototype.close = function (_this)
    {

        log('close: ' + _this);

        this.remove(_this);

    };

    // 移除指定 Sub ContextMenu  
    // _all 是否移除所有 
    // _filter 移除中过滤的 Sub ContextMenu 
    ContextMenu.prototype.remove = function (_this, _all, _filter)
    {
        var _exists = false,
            _select,
            _container;

        if (_filter && $.isArray(_filter))
        {
            _filter.forEach(function (_)
            {
                // 菜单是否存在 过滤 _filter 中 
                if (_this == _)
                {
                    _exists = true;
                    return;
                }
            });
        }

        // 不存在， 则移除
        if (!_exists)
        {
            // 菜单对应 容器
            _container = this.container[this.children.indexOf(_this)];

            // 从 container 数组（集合）中移除 容器 
            this.container.remove(_container);
            // 从 children 数组（集合）中移除 Sub ContextMenu 
            this.children.remove(_this);

            // 获得最后一个 菜单 
            _select = this.children[this.children.length - 1];

            // 选中最后一个 
            this.select(_select);

            // 移除菜单 
            $(_this).remove();
            // 移除容器 
            $(_container).remove();
        }

        if (!_select)
        {
            _select = this.children[this.children.length - 1];
            if (_this == _select && this.children.length > 2)
            {
                _select = this.children[this.children.length - 2];
            }
        }

        // 递归移除当前 Sub ContextMenu 
        if (_all)
        {
            if (this.children.length <= 0 || (_filter && $.isArray(_filter) && this.children.length <= _filter.length)) return;

            this.remove(_select, _all, _filter);
        }

    };

    // 判断是否已存在 Sub ContextMenu 
    ContextMenu.prototype.exists = function (_this, _select)
    {

        var _t = $(_this).text(),
            _exists = false;

        this.children.forEach(function (_)
        {

            if ($(_).text() == _t)
            {
                _exists = true;
                _this = _;
                return;
            }
        });

        if (_select)
        {
            this.select(_this);
        }

        return _exists;
    };

    o.ContextMenu = ContextMenu;

})(window, jQuery);

