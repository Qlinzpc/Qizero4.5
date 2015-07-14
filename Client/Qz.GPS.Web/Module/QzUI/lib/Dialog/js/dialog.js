/**
 * QzUI.js.lib.dialog.js  Dialog 框  
 * 
 * @Author ZPC 
 * @Date 2015-4-16 11:23:52 
 * @Version 1.0 
 * 
 */
(function (o) {

    "use strict";

    // 默认配置 
    var DEFAULTS = {
        id: 'dialog-' + new Date().getTime(),
        title: '系统提示',
        content: '',
        show: false,
        mask: true,
        parent: '',
        drag: false,
        timer: 0,
        css: {},
        pos: '',
        footer: '',
        closeCallback: function () { },
        callback: function () { }
    },

    btns = {
        yes: '<a class="btn btn-b p-3-13" data-id="yes">是</a>',
        no: '<a class="btn btn-b p-3-13" data-id="no">否</a>',
        ok: '<a class="btn btn-b" data-id="ok">确   定</a>',
        cancel: '<a class="btn btn-b" data-id="cancel">取  消</a>',
        submit: '<a class="btn btn-b" data-id="submit">提  交</a>'
    };

    /**
     * Dialog 对话框 
     * @param {Object} opt 
     */
    function Dialog() { }

    // 属性  
    Dialog.prototype = {
        box: {},
        dom: {},
        drag: {},
        config: {}
    };

    // 创建 
    Dialog.prototype._create = function (opt) {

        opt.closeCallback = function () {
            $api.remove(this.dom);

            $api.trigger(opt.winCloseCallback);
        };

        this.config = $api.extend({}, DEFAULTS, opt);
        this.config.css['width'] = 'auto';
        this.config.css['min-width'] = '250px';

        // Box 
        this.box = new Box(this.config);
        this.dom = this.box.dom;

        var _body = $api.dom(this.box.dom, '.box-body'),
            _footer = $api.dom(this.box.dom, '.box-footer'),
            _this = this;

        // $api.html(_body, '<div class="dialog-content">' + this.config.content + '</div>');
        $(_body).html('<div class="dialog-content">' + this.config.content + '</div>');

        // 假设显示 footer 
        if (this.config.footer) {
            $api.html(_footer, '<div class="dialog-footer">' + this.config.footer + '</div>');
        }

        // 假设自动关闭
        if (this.config.timer) {

            var timer = setTimeout(function () {
                // 关闭 
                _this.box.close();
                // 清除定时器 
                clearTimeout(timer);
            }, this.config.timer * 1000);
        }

        // 设置位置
        this.position();

        if (this.config.drag) {

            new Drag({
                drag: this.box.header().children[0],
                target: this.box.dom,
                parent: this.box.parent,
                proxy: true
            });

            this.box.init();
        }

        return this;
    };

    // 警告框 
    Dialog.prototype.alert = function (opt, callback) {
        var _this = this,
            _footer = null;

        opt.footer = btns.ok;
        opt.show = true;
        opt.css || (opt.css = {});
        opt.css['max-width'] = '300px';

        this._create(opt);
        _footer = $api.dom(this.dom, '.dialog-footer');

        if (_footer) {
            $api.addEvt($api.dom(_footer, 'a[data-id=ok]'), 'click', function () {
                _this.box.close();
                if (typeof callback === 'function') callback.call(this, _this);
            });
        }

        return this;
    };

    // 消息框 
    Dialog.prototype.msg = function (opt) {
        opt.footer = '';
        opt.show = true;
        opt.css || (opt.css = {});
        opt.css['max-width'] = '300px';

        this._create(opt);

        return this;
    };

    // 询问框 
    Dialog.prototype.confirm = function (opt, callback) {
        var _this = this,
            _footer = null;

        opt.css || (opt.css = {});
        opt.css['max-width'] = '300px';
        opt.footer = btns.ok + '' + btns.cancel;
        opt.show = true;
        this._create(opt);

        _footer = $api.dom(this.dom, '.dialog-footer');

        if (_footer) {
            $api.addEvt($api.dom(_footer, 'a[data-id=ok]'), 'click', function () {
                _this.box.close();
                if (typeof callback === 'function') callback.call(this, true, _this);
            });

            $api.addEvt($api.dom(_footer, 'a[data-id=cancel]'), 'click', function () {
                _this.box.close();
                if (typeof callback === 'function') callback.call(this, false, _this);
            });
        }

        return this;
    };

    // 窗口  
    Dialog.prototype.window = function (opt) {
        opt.footer = '';
        opt.show = true;
        
        this._create(opt);

        return this;
    };

    // 设置位置 
    Dialog.prototype.position = function () {

        var _this = this.box.dom,
            css = { top: 0, left: 0 },
            pWidth = 0,
            pHeight = 0,
            width = 0,
            height = 0;

        width = _this.offsetWidth;
        height = _this.offsetHeight;
        pWidth = this.box.parent.offsetWidth;
        pHeight = this.box.parent.offsetHeight;

        switch (this.config.pos) {
            case "top":
                break;
            case "top-center":
                css.left = (pWidth - width) / 2;
                break;
            case "right":
                css.left = pWidth - width;
                break;
            case "right-center":
                css.top = (pHeight - height) / 2;
                css.left = pWidth - width;
                break;
            case "bottom":
                css.top = pHeight - height;
                css.left = pWidth - width;
                break;
            case "bottom-center":
                css.top = pHeight - height;
                css.left = (pWidth - width) / 2;
                break;
            case "left":
                css.top = pHeight - height;
                break;
            case "left-center":
                css.top = (pHeight - height) / 2;
                break;
            case "center":
                css.top = (pHeight - height) / 2;
                css.left = (pWidth - width) / 2;
                break;
            default:
                css.left = (pWidth - width) / 2;
                css.top = 130;
                if (this.config.parent) css.top = 50;
                break;
        }

        $api.css(_this, 'top: ' + css.top + 'px;left: ' + css.left + 'px;');
    };

    o.Dialog = Dialog;
    o.dialog = new Dialog();

})(window);