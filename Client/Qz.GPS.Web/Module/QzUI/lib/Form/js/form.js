/**
 * QzUI.js.lib.form.js  Form 表单 
 * 
 * @Author ZPC 
 * @Date 2015-7-1 14:54:04 
 * @Version 1.0 
 * 
 */
(function (o) {

    "use strict";

    var DEFAULTS = {
        id: '#form',
        isValidate: true

    },
        IsMobile = function (text) {
            var _emp = /^\s*|\s*$/g;
            text = text.replace(_emp, "");
            // 电信手机号码
            var _d = /^1[3578][01379]\d{8}$/g;
            // 联通手机号码
            var _l = /^1[34578][012456]\d{8}$/g;
            // 移动手机号码
            var _y = /^(134[012345678]\d{7}|1[34578][012356789]\d{8})$/g;  
            if (_d.test(text)) {
                return 3;
            } else if (_l.test(text)) {
                return 2;
            } else if (_y.test(text)) {
                return 1;
            }

            return 0;
        };

    function Form(opt) {

        this.config = $api.extend({}, DEFAULTS, opt);

        this.init();
    }

    Form.prototype = {
        dom: null,
        config: null,
        arr: []
    };

    Form.prototype.init = function () {

        this.dom = $api.dom(this.config.id);
        this.arr = $api.domAll(this.dom, "input,select,textarea");

    };

    /**
     * 验证 
     */
    Form.prototype.validate = function (callback) {

        var msg = '',
            _type,
            _val;

        this.arr.forEach(function (_) {

            _val = $api.trim($api.val(_));

            if(_.required && _val === '')
            {
                msg = $api.data(_, 'required-msg') || "该项不能为空 !";

                _.focus();

                return false;
            }

            _type = $api.data(_, 'type') || '';

            if (_val && _type) {
                switch (_type) {
                    case 'int':
                        if (! /^\d*$/.test(_val)) {
                            msg = "只能为数字 !";

                            _.focus();
                        }
                        return false;
                    case 'tel':
                        if (_val.length != 11) {
                            msg = "只能为 11位手机号码 !";
                        } else if (IsMobile(_val) == 0) {
                            msg = "手机号码错误, 请输入正确的手机号码 !";
                        }

                        if (msg) {
                            _.focus();
                        }
                        return false;
                }
            }
        });

        $api.trigger(callback, msg);

        return msg == '';
    };

    /**
     * 序列化成 JSON 字符串 
     */
    Form.prototype.serialize = function (obj) {
        var rs = [],
            _type,
            _val;

        this.arr.forEach(function (_) {
            _type = $api.attr(_, 'type');

            if (_type !== 'button' || _type !== 'submit' || _type !== 'reset') {

                _val = $api.trim($api.val(_));

                if (_type && (_type === 'int' || _type === 'float') && _val === '') _val = 0;

                rs.push('\"' + $api.attr(_, 'name') + '\":\"' + _val + '\"');
            }
        });

        obj = obj || '';

        if (typeof obj === 'object') {
            return $api.parse("{" + rs.join(',') + "}");
        }

        return "{" + rs.join(',') + "}";
    };

    /**
     * 设置数据 
     */
    Form.prototype.setdata = function (obj) {
        if (!obj) return;

        var _key,
            _type;

        this.arr.forEach(function (_) {

            _type = $api.attr(_, 'type');

            if (_type !== 'button' || _type !== 'submit' || _type !== 'reset') {
                _key = $api.attr(_, 'name');
                $api.val(_, obj[_key]);
            }

        });

    };

    /**
     * 重置  
     */
    Form.prototype.reset = function () {
        var _name = '';

        this.arr.forEach(function (_) {
            _name = _.tagName;

            if (/input|textarea/gi.test(_name)) {
                _.value = '';
            }
            if (/select/gi.test(_name)) {
                if (_.options.length) _.options[0].selected = true;
            }

        });

    };

    /**
     * 提交  
     */
    Form.prototype.submit = function (ajax) {
        common.ajax({
            url: ajax.url,
            data: ajax.data,
            callback: function (rs) {
                ajax.callback(rs);
            }
        });
    };

    /**
     * 设置 Form DOM Id 
     */
    Form.prototype.setId = function (id) {
        this.config.id = id;
    }

    o.Form = Form;

})(window);

