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
        close: true,
        css:null,
        parent: '',
        header: true,
        footer: false,
        callback: function () { },
        closeCallback: function () { }
    },

    // 遮蔽层 
    index = 0;

    /**
     * Box 
     * @param {Object} opt 
     */
    function Box(opt) {

        this.config = $api.extend({}, DEFAULTS, opt);

        this.parent = $api.dom(this.config.parent || 'body') || $api.byId(this.config.parent);

        this.creaet();

        this.init();
    }

    // 属性 
    Box.prototype = {
        mask: {},
        dom: {},
        config: {},
        parent:{},
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
            }

            // 显示 footer 
            if (this.config.footer)
            {
                this._footer = $api.dom(_target, '.box-footer');
                $api.show(this._footer);
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

    };

    // 打开 
    Box.prototype.open = function () {
        var _this = this;

        $api.show(this.config.id);

        index = index || parseInt($api.cssVal(this.config.id, 'z-index'));
        $api.css(this.config.id, 'z-index:' + (index += 21));

        if (this.config.mask)
        {
            // 遮蔽层 
            $api.append(_this.parent, '<div class="l-mask animated fadeIn" style="display:block;" data-box-id="' + _this.config.id + '"></div>');

            _this.mask = $api.dom(".l-mask[data-box-id='" + _this.config.id + "']");

            $api.css('.l-mask', 'z-index:' + (index - 10));

        }

        return this;
    };
    // 关闭 
    Box.prototype.close = function () {

        // 阻止事件冒泡
        var e = event || window.event; if (e && e.stopPropagation) { e.stopPropagation(); } else { e.cancelBubble = true; }

        var _this = this;

        if (this.config.mask) {
            $api.addCls(this.mask, 'fadeOut');

            setTimeout(function () {
                // 遮蔽层
                $api.remove(_this.mask);

            }, 800);
        }

        $api.hide(this.config.id);
        $api.css(this.config.id, 'z-index:' + (index - 21));

        if (typeof this.config.closeCallback === 'function') this.config.closeCallback(this);
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
