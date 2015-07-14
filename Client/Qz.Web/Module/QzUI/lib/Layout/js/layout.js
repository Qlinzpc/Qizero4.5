/**
 * QzUI.js.lib.layout.js 
 * @Author ZPC 
 * @Date 2015-4-16 11:23:52 
 * @Version 1.0 
 * 
 */
(function (o) {

    "use strict";

    // 默认配置测试 
    var DEFAULTS = {
        id: '.page-container',
        main: '',
        nav: '',
        tab:'',
        taskbar: '',
        header: '',
        footer: '',
        parent: '',
        callback: function () { },
        initCallback: function () { }
    },

    // 获取或设置 Target HTML DOM 
    getset = function (_target, _value) {

        if (_value) {
            _target.html(_value);
        }

        return _target;
    };

    // Layout 布局 
    function Layout(opt) {
        var _this = this;

        this.config = $.extend({}, DEFAULTS, opt);

        this.create();

        this.init();

        $(window).resize(function () {
            _this.init();
        });
    }

    // Layout 属性
    Layout.prototype = {
        dom: {},
        config: {},
        // 页面任务栏
        _taskbar: {},
        // 页面内容
        _content: {},
        // 页面导航
        _nav: {},
        // 页面选项卡 
        _tab: {},
        // 页面主内容
        _main: {},
        // 页面头部
        _header: {},
        // 页面底部
        _footer: {}
    };

    // 创建 Layout 布局
    Layout.prototype.create = function ()
    {

        var _html = '<div class="page-layout" id="' + this.config.id + '"><div class="header"></div><div class="taskbar"></div><div class="layout-panel"><div class="nav"></div><div class="main"></div></div><div class="footer"></div></div>';

        return this;
    };

    // 初始化 Layout 布局 
    Layout.prototype.init = function () {

        var _height = $(window).height();

        this.dom = $(this.config.id),

        this._taskbar = $('.page-taskbar:eq(0)', this.dom),
        this._tab = $('.page-tab:eq(0)', this.dom),

        this._content = $('.page-content:eq(0)', this.dom),
        this._nav = $('.page-nav:eq(0)', this.dom),
        this._main = $('.page-main:eq(0)', this.dom),

        this._header = $('.page-header:eq(0)', this.dom),
        this._footer = $('.page-footer:eq(0)', this.dom);

        this._content.height(_height - this._header.outerHeight() - this._taskbar.outerHeight() - this._footer.outerHeight());

        if (typeof this.config.initCallback === 'function') this.config.initCallback(this);
    };

    // 获取或设置 页面内容 Content Dom 
    Layout.prototype.content = function (_value) {

        return getset(this._content, _value);
    };

    // 获取或设置 页面主内容 Main Dom 
    Layout.prototype.main = function (_value) {

        return getset(this._main, _value);
    };

    // 获取或设置 页面导航 Nav Dom 
    Layout.prototype.nav = function (_value) {

        return getset(this._nav, _value);
    };

    // 获取或设置 页面任务栏 Taskbar Dom 
    Layout.prototype.taskbar = function (_value) {

        return getset(this._taskbar, _value);
    };

    // 获取或设置 页面选项卡 Tab Dom 
    Layout.prototype.tab = function (_value) {

        return getset(this._tab, _value);
    };

    // 获取或设置 页面头部 Header Dom 
    Layout.prototype.header = function (_value) {

        return getset(this._header, _value);
    };

    // 获取或设置 页面底部 Footer Dom 
    Layout.prototype.footer = function (_value) {

        return getset(this._footer, _value);
    };

    // 全局事件 
    Layout.prototype.global = {
        // 点击 
        click: function (callback)
        {
            if(callback){
                $('body').unbind('click').bind('click', function ()
                {
                    if (typeof callback === 'function') callback();
                });

                return;
            }

            $('body').click();
        },

        // 右键 
        contextmenu: function (callback)
        {

        }
    };

    o.Layout = Layout;

})(window);