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
        parent: false,
        timer: 0,
        pos: 'center',
        footer: '',
        closeCallback: function () { },
        callback: function () { }
    },

    btns = {
        yes: '<a class="btn-a p-3-13" data-id="yes">是</a>',
        no: '<a class="btn-a p-3-13" data-id="no">否</a>',
        ok: '<a class="btn-a" data-id="ok">确   定</a>',
        cancel: '<a class="btn-a" data-id="cancel">取  消</a>',
        submit: '<a class="btn-a" data-id="submit">提  交</a>'
    };

    /**
     * Dialog 对话框 
     * @param {Object} opt 
     */
    function Dialog(opt) {
    }

    // 属性  
    Dialog.prototype = {
        box: {},
        dom: {},
        config: {}
    };

    // 创建 
    Dialog.prototype._create = function (opt) {
        this.config = $api.extend({}, DEFAULTS, opt);

        // Box 
        this.box = new Box(this.config);
        this.dom = this.box.dom;

        var _body = $api.dom(this.box.dom, '.box-body'),
            _footer = $api.dom(this.box.dom,'.box-footer'),
            _this  = this;

        $api.html(_body, '<div class="dialog-content">' + this.config.content + '</div>');

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

        return this;
    };

    // 警告框 
    Dialog.prototype.alert = function (opt, callback) {
        var _this = this,
            _footer = null;

        opt.footer = btns.ok ;

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
        this._create(opt);
        opt.footer = '';

        return this;
    };
    // 询问框 
    Dialog.prototype.confirm = function (opt, callback) {
        var _this = this,
            _footer = null;

        opt.footer = btns.ok + '' + btns.cancel;

        this._create(opt);

        _footer = $api.dom(this.dom, '.dialog-footer');

        if(_footer){
            $api.addEvt($api.dom(_footer, 'a[data-id=ok]'), 'click', function () {
                _this.box.close();
                if (typeof callback === 'function') callback.call(this, true, _this);
            });

            $api.addEvt($api.dom(_footer, 'a[data-id=cancel]'), 'click', function () {
                _this.box.close();
                if (typeof callback === 'function') callback.call(this, false, _this );
            });
        }

        return this;
    };

    o.Dialog = Dialog;

})(window);