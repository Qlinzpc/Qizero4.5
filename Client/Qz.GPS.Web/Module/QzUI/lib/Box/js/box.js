/**
 * QzUI.js.lib.box.js Box 
 * 
 * @Author ZPC 
 * @Date 2015-4-16 11:23:52 
 * @Version 1.0 
 * 
 */
(function (o) {

    "use strict";
    
    var DEFAULTS = {
        id: 'box',
        title: '',
        show: false,
        mask: true,
        icon: 'icon-win',
        overlay: false,
        close: true,
        css: null,
        parent: '',
        header: true,
        footer: false,
        callback: function () { },
        closeCallback: function () { }
    };

    /**
     * Box 
     * @param {Object} opt 
     */
    function Box(opt) {

        this.config = $api.extend({}, DEFAULTS, opt);

        if ($api.isElement(this.config.parent)) this.parent = this.config.parent;

        this.parent = this.parent || ($api.dom(this.config.parent || 'body') || $api.byId(this.config.parent));

        this.creaet();

        this.init();
    }

    // 属性 
    Box.prototype = {
        mask: {},
        overlay:{},
        dom: {},
        config: {},
        parent:null,
        _header: {},
        _body: {},
        _footer: {}
    };

    // 创建 Box 
    Box.prototype.creaet = function () {
        var _box = this,
            _target;

        if (typeof this.config.id === 'string') {
            _target = $api.byId(this.config.id) || $api.dom(this.config.id);
        }

        if (!_target) {
            $api.append(this.parent, '<div class="box" id="' + this.config.id + '"><div class="box-header"><div><i class="icon ' + this.config.icon + '"></i><span class="box-title">' + this.config.title + '</span></div><div><i class="icon icon-box-close"></i></div></div><div class="box-body"></div><div class="box-footer">' + this.config.footer + '</div></div>');

            _target = $api.byId(this.config.id) || $api.dom(this.config.id);

            if (this.config.close) {
                // 关闭 
                $api.addEvt($api.dom(_target, '.icon-box-close'), 'click', function () {
                    _box.close.call(_box);
                });
            } else {
                $api.remove($api.dom(_target, '.icon-box-close'));
            }

            this._body = $api.dom(_target, '.box-body');

            // 显示 header 
            if (this.config.header) {
                this._header = $api.dom(_target, '.box-header');
                $api.show(this._header);
            } else {
                $api.remove($api.dom(_target, '.box-header'));
            }

            // 显示 footer 
            if (this.config.footer)
            {
                this._footer = $api.dom(_target, '.box-footer');
                $api.show(this._footer);
            } else {
                $api.remove($api.dom(_target, '.box-footer'));
            }
        }

        // 显示 
        if (this.config.show) {
            this.open();
        }

        this.dom = _target;
        if (typeof this.config.callback === 'function') this.config.callback(this);

        return this;
    };

    // 初始化 
    Box.prototype.init = function () {

        if (this.config.css) {
            $(this.dom).css(this.config.css);
        }

        if ($api.dom(this._header, 'div:first-child')) {
            $api.css($api.dom(this._header, 'div:first-child'), 'width: {0}px'.format(this.dom.clientWidth - $api.dom(this._header, 'div:last-child').clientWidth - 1));
        }

    };

    // 打开 
    Box.prototype.open = function () {
        var _this = this;
        
        $api.show(this.parent);
        $api.show(this.config.id);

        if (this.config.mask)
        {
            // 遮蔽层 
            $api.append(_this.parent, '<div class="l-mask animated fadeIn" style="display:block;" data-box-id="' + _this.config.id + '"></div>');

            _this.mask = $api.dom("div.l-mask[data-box-id='" + _this.config.id + "']");

            $api.css(_this.mask, 'z-index:' + (++o.Index));
        }

        if (this.config.overlay)
        {
            // 覆盖层 
            $api.append($api.dom('body'), '<div class="overlay" style="display:block;" data-box-id="' + _this.config.id + '"></div>');

            _this.overlay = $api.dom('.overlay');

            $api.css(_this.overlay, 'z-index:' + (++o.Index));

            $api.addEvt(_this.overlay, 'click', function () {
                _this.close.call(_this);
            });

        }

        $api.css(this.config.id, 'z-index:' + (++o.Index));

        return this;
    };
    // 关闭 
    Box.prototype.close = function () {

        // 阻止事件冒泡
        $api.stop.event();

        var _this = this;

        if (this.config.mask) {
            $api.addCls(this.mask, 'fadeOut');

            setTimeout(function () {
                // 遮蔽层
                $api.remove(_this.mask);

            }, 700);
        }

        if (this.config.overlay) {
            // 覆盖层 
            _this.overlay = $api.dom('.overlay');
            $api.remove(_this.overlay);
        }

        $api.hide(this.config.id);

        if (typeof this.config.closeCallback === 'function') this.config.closeCallback.call(this);
        return this;
    };

    // 获取或设置 Box Header Dom 
    Box.prototype.header = function (html)
    {
        if (html)
        {
            $api.append(this._header, html);
        }

        return this._header;
    };

    // 获取或设置 Box Body Dom 
    Box.prototype.body = function (html) {

        if (html) {
            $api.append(this._body, html);
        }

        return this._body;
    };

    // 获取或设置 Box Footer Dom 
    Box.prototype.footer = function (html)
    {
        if (html)
        {
            $api.append(this._footer, html);
        }

        return this._footer;
    };

    o.Box = Box;

})(window);
