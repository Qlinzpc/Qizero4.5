/**
 * 表单操作
 */
var form = new function () {
    // 验证手机号码
    function IsMobile(text) {
        var _emp = /^\s*|\s*$/g;
        text = text.replace(_emp, "");
        var _d = /^1[3578][01379]\d{8}$/g;                                                       // 电信手机号码
        var _l = /^1[34578][012456]\d{8}$/g;                                                    // 联通手机号码
        var _y = /^(134[012345678]\d{7}|1[34578][012356789]\d{8})$/g;  // 移动手机号码
        if (_d.test(text)) {
            return 3;
        } else if (_l.test(text)) {
            return 2;
        } else if (_y.test(text)) {
            return 1;
        }

        return 0;
    }

    // 验证
    this.validate = function (id, callback) {

        var _this = $api.dom("#" + id),
            arr = $api.domAll(_this, "input,select,textarea"),
            len = arr.length,
            i = 0,
            oData = {},
            msg = '',
            _type,
            _val;

        for (i = 0; i < len; i++) {
            oData = arr[i];
            _val = $api.trim($api.val(oData));

            if (oData.required && _val == '') {

                msg = $api.data(oData, 'required-msg') || "该项不能为空 !";

                oData.focus();
                break;
            }

            _type = $api.data(oData, 'type') || '';

            if (_val && _type) {
                switch (_type) {
                    case 'int':
                        if (! /^\d*$/.test(_val)) {
                            msg = "只能为数字 !";

                            oData.focus();
                        }
                        break;
                    case 'tel':

                        if (_val.length != 11) {
                            msg = "只能为 11位手机号码 !";
                        } else if (IsMobile(_val) == 0) {
                            msg = "手机号码错误, 请输入正确的手机号码 !";
                        }

                        if (msg) {
                            oData.focus();
                        }
                        break;
                }
            }
        }

        if (typeof callback === 'function') callback(msg);

        return msg == '';
    };

    /**
     * 序列化成 JSON 字符串
     * @param {String} id
     * @returns {String}
     */
    this.serialize = function (id) {

        var _this = $api.dom("#" + id),
            arr = $api.domAll(_this, "input,select,textarea"),
            len = arr.length,
            rs = [],
            i = 0,
            oData = {},
            _type,
            _val;

        for (i = 0; i < len; i++) {

            oData = arr[i];
            _type = $api.attr(oData, 'type');

            if (_type == 'button' || _type == 'submit' || _type == 'reset') continue;

            _type = $api.data(oData, 'type') || '',
            _val = $api.trim($api.val(oData));

            if (_type && (_type === 'int' || _type === 'float') && _val === '') _val = 0;

            rs.push('\"' + $api.attr(oData, 'name') + '\":\"' + _val + '\"');
        }

        return "{" + rs.join(',') + "}";
    };
    // 设置数据 
    this.setdata = function (id, obj) {
        if (!obj) return;

        var _this = $api.dom("#" + id),
            arr = $api.domAll(_this, "input,select,textarea"),
            len = arr.length,
            i = 0,
            oData = {},
            _key,
            _type;

        for (i = 0; i < len; i++) {

            oData = arr[i];
            _type = $api.attr(oData, 'type');

            if (_type == 'button' || _type == 'submit' || _type == 'reset') continue;

            _key = $api.attr(oData, 'name');
            $api.val(oData, obj[_key]);
        }

    };
    // 重置
    this.reset = function (id) {

        var _this = $api.dom("#" + id),
          arr = $api.domAll(_this, "input,select,textarea"),
          len = arr.length,
          rs = [],
          i = 0,
          _name = '',
          oData = {};

        for (i = 0; i < len; i++) {
            oData = arr[i];
            _name = oData.tagName;

            if (/input|textarea/gi.test(_name)) {
                oData.value = '';
            }
            if (/select/gi.test(_name)) {
                if (oData.options.length) oData.options[0].selected = true;
            }
        }
    };
    // 提交
    this.submit = function (ajax) {
        // ajax post 请求
        common.ajax({
            url: ajax.url,
            data: ajax.data,
            loadId: '#dialog',
            errMsg: '#errmsg',
            callback: function (rs) {
                ajax.callback(rs);
            }
        });
    };
};
