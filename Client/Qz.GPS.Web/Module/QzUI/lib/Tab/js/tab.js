/**
 * QzUI.js.lib.tab.js 
 * @Author ZPC 
 * @Date 2015-4-16 11:23:52 
 * @Version 1.0 
 * 
 */
(function (o, $)
{

    "use strict";

    var DEFAULTS = {
        id: 'tab',
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

    // Tab 选项卡 
    function Tab(opt)
    {
        this.config = $.extend({}, DEFAULTS, opt);

        this.childrens = [];
        this.containers = [];

        this.create();

        this.init();
    }

    Tab.prototype = {
        config: {},
        dom: {},
        current: {},
        page: {},
        childrens: [],
        containers: [],
        contextmenu: null // 右键菜单 
    };

    // 创建 Tab 选项卡 
    Tab.prototype.create = function ()
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

    // 初始化 Tab 选项卡 
    Tab.prototype.init = function ()
    {
        var _tab = this;

        this.page = this.config.page;

        $('li', this.contextmenu).click(function ()
        {
            // 阻止事件冒泡
            $api.stop.event();

            var _current = _tab.current.get(0),
                _ = $(this),
                _content,
                _type;

            if (/disabled/i.test(_.attr('class'))) return;

            switch (_.data('name'))
            {
                case "refresh":

                    _tab.reload(_tab.current);
                    break;
                case "close":

                    _tab.remove(_current);
                    break;
                case "closeAll":

                    _tab.remove(_current, true, [_tab.childrens[0]]);
                    break;
                case "closeAllOther":

                    _tab.remove(_current, true, [_tab.childrens[0], _current]);
                    break;
            }

            _tab.contextmenu.hide();
        });

        this.childrens = [];
        this.containers = [];

        if (typeof this.config.initCallback === 'function') this.config.initCallback.call(this);
    };

    // 指定 Sub Tab 绑定事件 
    Tab.prototype.bindEvent = function (_this)
    {

        var _tab = this;

        $(_this).bind('click', function ()
        {

            if ($api.hasCls(this, 'select')) return;

            _tab.select(this);
        });

        $('.icon-tab-close', _this).bind('click', function ()
        {
            // 阻止事件冒泡前， 触发页面全局事件 
            _tab.page && _tab.page.global.click();

            // 阻止事件冒泡
            $api.stop.event();

            _tab.close(this.parentNode);
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

                    //log('鼠标左键');
                    break;
                case 2:


                    //log('鼠标中键');
                    break;
                case 3:

                    _tab.contextmenu.css({
                        left: e.pageX + 5,
                        top: e.pageY + 5,
                        display: 'block'
                    });

                    _tab.current = _;

                    //log('鼠标右键');

                    // 页面全局点击 事件 
                    _tab.page && _tab.page.global.click(function ()
                    {
                        // 隐藏右键菜单 
                        _tab.contextmenu.hide();
                    });

                    return false;
                default:

                    break;
            }
        });

    };

    // 获取或设置 Sub Tab 选中 
    Tab.prototype.select = function (_this)
    {

        if (_this)
        {
            $(this.containers[this.childrens.indexOf($('li[class=select]', this.dom).get(0))]).addClass('dn');
            $('li[class=select]', this.dom).removeClass('select');

            $(_this).addClass('select');
            $(this.containers[this.childrens.indexOf(_this)]).removeClass('dn');

            if (typeof this.config.selectCallback === 'function') this.config.selectCallback(this, _this);
        } else
        {

            _this = $('li[class=select]', this.dom);
        }

        this.current = $(_this);

        return _this;
    };

    // 添加 Sub Tab  
    // _html Sub Tab 选项卡 HTML 
    // _content 选项卡容器 'ajax | content'
    // _type 'ajax | content' 
    Tab.prototype.add = function (_html, _content, _type)
    {
        var _this = $('<li class></li>'),
            _tab = this,
            _ = _this.get(0);

        _this.attr('data-content', _type == 'ajax' ? JSON.stringify(_content) : _content);
        _this.attr('data-type', _type);

        if (_html)
        {
            if (this.childrens.length >= 1)
            {
                _html += '<i class="icon icon-tab-close"></i>';
            }

            _this.html(_html);
        }

        if (this.exists(_, true))
        {
            return _this;
        }

        var _container = $("<div></div>");
        _container.addClass('tab-content dn');

        // 向 主内容 追加 Container 容器 
        this.page && this.page.main().append(_container);

        this.childrens.push(_);
        this.containers.push(_container.get(0));

        $(this.dom).append(_);
        this.select(_);

        // 绑定事件 
        this.bindEvent(_);

        // 设置内容 
        _tab.content(_, _content, _type);

        return _this;
    };

    // 关闭指定 Sub Tab  
    Tab.prototype.close = function (_this)
    {
        // log('close: ' + _this);
        this.remove(_this);
    };

    // 移除指定 Sub Tab  
    // _all 是否移除所有 
    // _filter 移除中过滤的 Sub Tab 
    Tab.prototype.remove = function (_this, _all, _filter)
    {
        var _exists = false,
            _select,
            _container;

        if (_filter && $.isArray(_filter))
        {
            _filter.forEach(function (_)
            {
                // 选项卡是否存在 过滤 _filter 中 
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
            // 选项卡对应 容器
            _container = this.containers[this.childrens.indexOf(_this)];

            // 从 container 数组（集合）中移除 容器 
            this.containers.remove(_container);
            // 从 children 数组（集合）中移除 Sub Tab 
            this.childrens.remove(_this);

            // 获得最后一个 选项卡 
            _select = this.childrens[this.childrens.length - 1];

            // 选中最后一个 
            this.select(_select);

            // 移除选项卡 
            $(_this).remove();
            // 移除容器 
            $(_container).remove();

            $api.trigger(this.config.removeCallback, _container, _this);
        }

        if (!_select)
        {
            _select = this.childrens[this.childrens.length - 1];
            if (_this == _select && this.childrens.length > 2)
            {
                _select = this.childrens[this.childrens.length - 2];
            }
        }

        // 递归移除当前 Sub Tab 
        if (_all)
        {
            if (this.childrens.length <= 0 || (_filter && $.isArray(_filter) && this.childrens.length <= _filter.length)) return;

            this.remove(_select, _all, _filter);
        }

    };

    // 刷新 Tab Container 
    Tab.prototype.reload = function (_this) {

        _this = _this || this.current;

        var _type = _this.data('type');
        var _content = _this.data('content');

        this.content(_this.get(0), _content, _type);
    };

    // 判断是否已存在 Sub Tab 
    Tab.prototype.exists = function (_this, _select)
    {

        var _t = $(_this).text(),
            _exists = false;

        this.childrens.forEach(function (_)
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

    // 设置或获取 Sub Tab Container DOM 
    Tab.prototype.content = function (_this, _content, _type)
    {
        // 选项卡对应 容器
        var _container = this.containers[this.childrens.indexOf(_this)],
            _ajax;

        _type = _type || 'ajax';

        switch (_type)
        {
            case 'ajax':

                _ajax = _content;

                if (!_ajax) return;

                common.ajax({
                    url: _ajax.url,
                    data: _ajax.data || null,
                    type: 'html',
                    success: function (rs)
                    {
                        $(_container).html(rs.html);
                    },
                    error: function (er)
                    {
                        console.log(JSON.stringify(er));
                    }
                });

                break;
            case 'content':

                $(_container).html(_content);
                break;
            default:
                log('Tab Container 容器, _type 参数错误 ！' + _type);
                break;
        }

    };

    o.Tab = Tab;

})(window, jQuery);

