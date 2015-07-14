/**
 * QzUI.js.lib.select.js  Select 下拉框  
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
        id: '',       // Select ID 
        title: '数据列表', // 标题 
        show: false,  // 是否显示
        mask: true,   // 是否显示遮蔽层 
        isdft: true,    // 是否显示默认 '全部' 
        select: '',       // 指定选中值 
        url: '',     // 数据 ajax url 地址 
        data: '',  // ajax data 
        key: '',    // Key 隐藏值 
        value: '', // Value 显示值 
        html: '',  // 指定显示 html 
        change: function () { },    // 项改变事件 
        callback: function () { }    // 创建完毕 回调函数 
    },

    // 绑定事件 
    bind = {
        event: function (obj) {
            $api.addEvt(obj.arr, 'click', function () {
                obj.change(this);
            });
        }
    };

    /**
     * Select 下拉框 
     * @param {Object} opt 
     */
    function Select(opt) {
        this.config = $api.extend({}, DEFAULTS, opt);

        this.create();
    }

    // 属性 
    Select.prototype = {
        arr: {},
        old: {},
        dom: {},
        box: {},
        config: {},
        obj: {
            key: '',
            value: ''
        }
    };

    // 创建 Select 
    Select.prototype.create = function () {
        var arr = [],
            _this = this;

        // 1. 创建 Box 
        _this.box = new Box(this.config);

        // 2.0 已存在 
        if (_this.arr.length > 0) {
            if (_this.config.show) {
                _this.show();
            }

            if (typeof _this.config.callback === 'function') _this.config.callback();
            return _this;
        }

        // 2.1 显示指定 HTML 
        if (_this.config.html) {
            $api.html($api.dom(_this.box.dom, '.box-body'), '<ul class="ui-select scroll">' + _this.config.html + '</ul>');
            if (_this.config.show) {
                _this.show();
            }

            arr = $api.domAll(_this.box.dom, 'li');
            if (arr.length == 0) {
                _this.arr = [];
                return _this;
            }
            _this.box.arr = arr;
            _this.dom = $api.dom(_this.box.dom,'ul');
            bind.event(_this);

            if (typeof _this.config.callback === 'function') _this.config.callback();
            return _this;
        }

        // 3. ajax 请求 
        common.ajax.post({
            url: _this.config.url,
            data: _this.config.data,
            callback: function (rs) {
                if (rs.Status != 0) {
                    common.tip(rs.Message);

                    if (typeof _this.config.callback === 'function') _this.config.callback();
                    return _this;
                }

                var i = 0,
                    html = [],
                    data = {},
                    len = rs.Obj.Data.length;

                // 显示默认 HTML 
                if (_this.config.isdft) {
                    if (_this.config.select == '0') {
                        html.push('<li data-id="0" class="select"><i class="icon-select"></i>全部</li>');
                        $api.val(_this.config.key, 0);
                        $api.val(_this.config.value, '全部');
                    } else {
                        html.push('<li data-id="0">全部</li>');
                    }
                }
                for (i = 0; i < len; i++) {
                    data = rs.Obj.Data[i];

                    if (!_this.config.isdft && _this.config.select == '' && i == 0) {
                        html.push('<li data-id="' + data.Key + '" class="select"><i class="icon-select"></i>' + data.Value + '</li>');
                        $api.val(_this.config.key, data.Key);
                        $api.val(_this.config.value, data.Value);
                        continue;
                    }
                    if (data.Key == _this.config.select) {
                        html.push('<li data-id="' + data.Key + '" class="select"><i class="icon-select"></i>' + data.Value + '</li>');
                        continue;
                    }
                    html.push('<li data-id="' + data.Key + '">' + data.Value + '</li>');
                }

                $api.html($api.dom(_this.box.dom, '.box-body'), '<ul class="ui-select scroll">' + html.join('') + '</ul>');

                if (_this.config.show) _this.show();

                arr = $api.domAll(_this.box.dom, 'li');
                if (arr.length == 0) {
                    _this.arr = [];

                    if (typeof _this.config.callback === 'function') _this.config.callback();
                    return _this;
                }

                _this.arr = arr;
                _this.dom = $api.dom(_this.box.dom, 'ul');
                bind.event(_this);
                if (typeof _this.config.callback === 'function') _this.config.callback();
            }
        });

        return this;
    };
    // 显示 
    Select.prototype.show = function () {
        // 显示 box 
        this.box.open();

        return this;
    };
    // 隐藏 
    Select.prototype.hide = function () {
        // 隐藏 box 
        this.box.close();

        return this;
    };
    // 改变事件 
    Select.prototype.change = function (_target) {
        var _this = this;

        this.check(_target, function () {
            // 关闭 
            _this.hide();
            if (typeof _this.config.change === 'function') _this.config.change.call(_target, _this);
        });

        return this;
    };
    // 选中 
    Select.prototype.check = function (_target, callback) {
        if (this.arr.length <= 0) return;

        var _this = this,
            txt = $api.trim($api.text(_target)),
            timer;

        // 保存当前选中的 dom 
        this.old = $api.domAll(this.dom, '.select');

        $api.removeCls(this.old, 'select');
        $api.text(this.old, '', 'this');
        $api.addCls(_target, 'select');
        $api.html(_target, '<i class="icon-select"></i>' + txt);

        // 获得当前操作的数据
        this.obj.key = $api.data(_target, 'id');
        this.obj.value = txt;

        $api.val(this.config.key, this.obj.key);
        $api.val(this.config.value, this.obj.value);

        timer = setTimeout(function () {
            if (typeof callback === 'function') {
                callback();
            }

            clearTimeout(timer);
        }, 500);

        return this;
    };
    // 清除 
    Select.prototype.clear = function () {
        $api.html($api.dom(this.box.dom, '.box-body'), '');
        this.arr = [];

        return this;
    };
    // 初始化 
    Select.prototype.init = function () {

        this.check(this.arr[0]);
    };

    o.Select = Select;

})(window);