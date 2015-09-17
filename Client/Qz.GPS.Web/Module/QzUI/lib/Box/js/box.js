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
        lock: false,
        min: false,
        max: false,
        close: true,
        css: null,
        parent: '',
        header: true,
        footer: false,
        callback: function () { },
        closeCallback: function () { }
    },
        containers = [];

    /**
     * Box 
     * @param {Object} opt 
     */
    function Box(opt) {

        this.config = $api.extend({}, DEFAULTS, opt);

        if ($api.isElement(this.config.parent)) this.parent = this.config.parent;

        this.parent = this.parent || ($api.dom(this.config.parent || 'body') || $api.byId(this.config.parent));

        this.creaet();

        // 判断是否已初始化
        if (containers.indexOf(this.config.id, 1) === -1) {
            this.init();
            containers.push(this.config.id);
        } else {
            this.min();
        }
    }

    // 属性 
    Box.prototype = {
        mask: {},
        overlay: {},
        dom: {},
        config: {},
        parent: null,
        _lock: false,
        _min: false,
        _max: false,
        _copies: {},
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
            $api.append(this.parent, '<div class="box" id="{id}"><div class="box-header"><div><i class="icon {icon}"></i><span class="box-title">{title}</span></div><div><i class="icon icon-box-lock"></i><i class="icon icon-box-min"></i><i class="icon icon-box-max"></i><i class="icon icon-box-close"></i></div></div><div class="box-body"></div><div class="box-footer">{footer}</div></div>'.format(this.config));

            _target = $api.byId(this.config.id) || $api.dom(this.config.id);

            this._body = $api.dom(_target, '.box-body');
        }

        // 显示 
        if (this.config.show) {
            this.open();
        }

        this.dom = _target;

        // 显示 header 
        if (this.config.header) {
            this._header = $api.dom(this.dom, '.box-header');
            $api.show(this._header);
        } else {
            $api.remove($api.dom(this.dom, '.box-header'));
        }

        // 显示 footer 
        if (this.config.footer) {
            this._footer = $api.dom(this.dom, '.box-footer');
            $api.show(this._footer);
        } else {
            $api.remove($api.dom(this.dom, '.box-footer'));
        }

        if (typeof this.config.callback === 'function') this.config.callback(this);
        return this;
    };

    // 初始化 
    Box.prototype.init = function () {
        var _box = this;

        if (this.config.css) {
            $(this.dom).css(this.config.css);
        }

        if (this.config.close) {
            // 关闭 
            $api.addEvt($api.dom(this.dom, '.icon-box-close'), 'click', function () {
                _box.close.call(_box);
            });
        } else {
            $api.remove($api.dom(this.dom, '.icon-box-close'));
        }

        if (this.config.lock) {
            // 关闭 
            $api.addEvt($api.dom(this.dom, '.icon-box-lock'), 'click', function () {
                _box.lock.call(_box);
            });
        } else {
            $api.remove($api.dom(this.dom, '.icon-box-lock'));
        }

        if (this.config.min) {
            // 关闭 
            $api.addEvt($api.dom(this.dom, '.icon-box-min'), 'click', function () {
                _box.min.call(_box);
            });
        } else {
            $api.remove($api.dom(this.dom, '.icon-box-min'));
        }

        if (this.config.max) {
            // 关闭 
            $api.addEvt($api.dom(this.dom, '.icon-box-max'), 'click', function () {
                _box.max.call(_box);
            });
        } else {
            $api.remove($api.dom(this.dom, '.icon-box-max'));
        }


    };

    // 初始化 Drag 
    Box.prototype.initdrag = function () {
        if ($api.dom(this._header, 'div:first-child')) {
            $api.css($api.dom(this._header, 'div:first-child'), 'width: {0}px'.format(this.dom.clientWidth - $api.dom(this._header, 'div:last-child').clientWidth - 1));
        }
    };

    // 打开 
    Box.prototype.open = function () {
        var _this = this;

        $api.show(this.parent);
        $api.show(this.config.id);

        if (this.config.mask) {
            // 遮蔽层 
            $api.append(_this.parent, '<div class="l-mask animated fadeIn" style="display:block;" data-box-id="' + _this.config.id + '"></div>');

            _this.mask = $api.dom("div.l-mask[data-box-id='" + _this.config.id + "']");

            $api.css(_this.mask, 'z-index:' + (++o.Index));
        }

        if (this.config.overlay) {
            // 覆盖层 
            $api.append($api.dom('body'), '<div class="overlay" style="display:block;" data-box-id="' + _this.config.id + '"></div>');

            _this.overlay = $api.dom('.overlay');

            $api.css(_this.overlay, 'z-index:' + (++o.Index));

            $api.addEvt(_this.overlay, 'click', function () {
                _this.close.call(_this);
            });

        }

        $api.css(this.config.id, 'height:auto;transform: scale(1,1):1;z-index:' + (++o.Index));

        return this;
    };

    Box.prototype.lock = function () {
        this._lock = !this._lock;

    };

    Box.prototype.min = function () {

        if (this._min) {
            $api.css(this.dom, 'top:{top};left:{left};transform: scale(1,1);width:{width};height:{height}'.format(this._copies));
        } else {
            this._copies = {
                top: this.dom.style.top,
                left: this.dom.style.left,
                width: ((this.dom.clientWidth + 2) + 'px') || 'auto',
                height: this.dom.style.height || 'auto'
            };

            var pos = $api.offset($api.dom('.taskbar .{0}'.format(this.config.icon)));
            console.log(pos);

            $api.css(this.dom, 'top:{0}px;left:{1}px;transform: scale(0,0);min-width:0px;width:0px;height:0px'.format(pos.t - 60, pos.l));
        }

        this._min = !this._min;
    };

    Box.prototype.max = function () {

        if (this._max) {
            $api.css(this.dom, 'top:{top};left:{left};width:{width};height:{height}'.format(this._copies));
        } else {
            this._copies = {
                top: this.dom.style.top,
                left: this.dom.style.left,
                width: ((this.dom.clientWidth + 2) + 'px') || 'auto',
                height: this.dom.style.height || 'auto'
            };

            $api.css(this.dom, 'top:0;left:0;width:{0}px;height:{1}px'.format(this.config.parent.clientWidth, this.config.parent.clientHeight));
        }

        this._max = !this._max;
    };

    // 关闭 
    Box.prototype.close = function (stop) {
        if (arguments.length == 0 || stop) {
            // 阻止事件冒泡 
            $api.stop.event();
        }

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
    Box.prototype.header = function (html) {
        if (html) {
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
    Box.prototype.footer = function (html) {
        if (html) {
            $api.append(this._footer, html);
        }

        return this._footer;
    };

    o.Box = Box;
    o.BoxContainers = containers;

})(window);
