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
        id: '',
        title: '',
        show: false,
        mask: true,
        parent: '',
        footer: '',
        callback: function () { },
        closeCallback: function () { }
    },
        // 遮蔽层 
        index = 0;

    // 属性 
    Box.prototype = {
        mask: {},
        dom: {},
        config: {}
    };

    /**
     * Box 
     * @param {Object} opt 
     */
    function Box(opt) {

        this.config = $api.extend({}, DEFAULTS, opt);

        this.config.parent = this.config.parent || $api.dom('body');

        this.creaet();
    }

    // 创建 Box 
    Box.prototype.creaet = function () {
        var _box = this,
            _target;

        if (typeof this.config.id === 'string') {
            _target = $api.byId(this.config.id) || $api.dom(this.config.id);
        }

        if (!_target) {
            $api.append(this.config.parent, '<div class="box" id="' + this.config.id + '"><div class="box-header"><span class="box-title"> <i class="icon-win"></i>' + this.config.title + '</span><span class="box-close">×</span></div><div class="box-body"></div><div class="box-footer">' + this.config.footer + '</div></div>');

            _target = $api.byId(this.config.id) || $api.dom(this.config.id);

            // 关闭 
            $api.addEvt($api.dom(_target, '.box-close'), 'click', function () {
                _box.close.call(_box);
            });

            // 显示 footer 
            if (this.config.footer) {
                $api.show($api.dom(_target, '.box-footer'));
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
    // 打开 
    Box.prototype.open = function () {
        var _this = this;

        if (this.config.mask) {
            // setTimeout(function () {
                // 遮蔽层 
                $api.append(_this.config.parent, '<div class="l-mask animated fadeIn" style="display:block;" data-box-id="'+_this.config.id+'"></div>');

                _this.mask = $api.dom(".l-mask[data-box-id='" + _this.config.id + "']");

                $api.css('.l-mask', 'z-index:' + (index - 10));

            // }, 100);
        }

        $api.show(this.config.id);

        index = index || parseInt($api.cssVal(this.config.id, 'z-index'));
        $api.css(this.config.id, 'z-index:' + (index += 21));

        return this;
    };
    // 关闭 
    Box.prototype.close = function () {
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

    o.Box = Box;

})(window);
