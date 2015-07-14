/**
 * common 
 * 
 */
(function (o) {

    var common = new function () {

        // ajax 默认参数 
        var defaults = {
            url: "",
            method: "post",
            data: null,
            async: true,
            type: "json",
            timeout: 15000,
            cache: false,
            isLoad: true,
            loadId: "",
            errMsg: "#msg",
            callback: function (rs) { },
            success: function (rs) { }
        },

            _istip = 0;

        // 请求参数
        this.request = function () {
            var href = decodeURI(window.location.href).split('#')[0];
            if (href.indexOf('?') == -1) return "";

            var params = href.split('?')[1];
            if (params === "") return "";

            return common.parse(params);
        };

        // 字符串解析为 数组对象[] 
        this.parse = function (str) {
            str = str || '';
            if (str == '') return [];
            var obj = [];

            if (str.indexOf('&') <= -1) {
                obj[str.split('=')[0]] = str.split('=')[1];
                return obj;
            }

            var arr = str.split('&'),
                len = arr.length,
                i = 0;

            for (i = 0; i < len; i++) {
                if (arr[i] != "") {
                    obj[arr[i].split('=')[0]] = arr[i].split('=')[1];
                }
            }

            return obj;
        };

        // ajax 操作 
        this.ajax = function (opts) {

            var opt = $api.extend({}, defaults, opts);

            new Ajax()._init({
                method: opt.method,
                url: opt.url,
                data: opt.data,
                type: opt.type,
                timeout: opt.timeout,
                success: function (r) {
                    var rs = r;
                    if (r && r.d) { rs = r.d; }

                    if (opt.success && typeof opt.success === 'function') opt.success(rs);
                    if (opt.callback && typeof opt.callback === 'function') opt.callback(rs);
                },
                error: function (err) {
                   
                    common.tip(err.message);
                    if (opt.isLoad) common.load.hide(opt.loadId);

                    if (opt.error && typeof opt.error === 'function') opt.error(err);
                },
                before: function () {
                    if (opt.isLoad) common.load.show(opt.loadId);
                },
                complete: function () {
                    if (opt.isLoad) common.load.hide(opt.loadId);
                },
                async: opt.async,
                cache: opt.cache
            });
        };

        // 异步加载 js , css 
        this.loadJC = function (src, type) {
            var obj,
                tag;

            if (src.indexOf('?') != -1) {
                obj = common.parse(src.split('?')[1]);
            }
            type = type || "";
            switch (type.toLowerCase()) {
                case "css":
                    tag = document.createElement("link");
                    tag.href = src;
                    tag.rel = 'stylesheet';
                    $api.append($api.dom('head'),tag);
                    break;
                default:
                    tag = document.createElement("script");
                    tag.src = src;
                    $api.append($api.dom('body'), tag);
                    break;
            }

            if (obj && obj.jcallback) {

                tag.onload(function () {
                    eval(obj.jcallback + '()');
                });
            }
        };

        // 页面加载完成 
        this.load = {
            complete: function (callback) {
                // 加载事件 
                document.onreadystatechange = function () {
                    if (document.readyState == "complete") {
                        if (typeof callback === "function") callback();
                    }
                };
            },
            show: function (obj) {
                common.loading.show();
            },
            hide: function (obj) {
                common.loading.hide();
            }
        };

        // tip 提示框 
        this.tip = function (msg, timer) {
            if (!msg) return;

            var _this,
                $body = $api.dom('body');

            timer = timer || 2000;
            $api.append($body, '<div class="tip animated"></div>');
            _this = $api.domAll('.tip')[_istip++];
            msg = $api.stringify(msg);

            $api.text(_this, msg);
            $api.css(_this, 'display:block;opacity:1;');
            $api.css(_this, 'left:' + (($body.offsetWidth - _this.offsetWidth) / 2) + 'px;top:' + (20 + (_istip - 1) * (_this.offsetHeight + 3)) + 'px');
            var t, t1 = setTimeout(function () {
                // 开始执行动画 
                $api.addCls(_this, 'rotateOutDownRight');
                clearTimeout(t1);
                // 动画 2s 后执行 
                t = setTimeout(function () {
                    $api.remove(_this);
                    --_istip;
                    clearTimeout(t);
                }, 2000);
            }, timer + (2000 * (_istip - 1)) - 100);
        };

        // loadding 加载框 
        this.loading = new function () {
            this.show = function (type) {
                type = type || 0; // loading 类型 ( 0.全局 loading 1.底部 loading ) 
                if (type != 0) return;
                var _this = $api.dom('.loading'),
                    $body = $api.dom('body');

                if (!_this) {
                    $api.append($body, '<div class="loading"><div class="loading1"></div><div class="loading2"></div><div></div></div>');
                    _this = $api.dom('.loading');
                }

                // 遮蔽层
                $api.remove($api.dom('.l-mask'));
                $api.append($api.dom('body'), '<div class="l-mask animated fadeIn" style="display:block;"></div>');
                $api.show(_this);
                $api.css(_this, 'left:' + (($body.offsetWidth - _this.offsetWidth) / 2) + 'px');
            };
            this.hide = function (type) {
                type = type || 0; // loading 类型 ( 0.全局 loading 1.底部 loading ) 
                if (type != 0) return;
                var _this = $api.dom('.loading');
                // 遮蔽层
                $api.remove($api.dom('.l-mask'));
                $api.hide(_this);
            };
        };

        // top 置顶 
        this.top = new function () {
            this.target = {};

            // 初始化 
            this.init = function (that) {
                var _this = $api.dom('.top'),
                    $body = $api.dom('body');

                this.target = that || $api.dom('.content');
                if (!_this) {
                    $api.append($body, '<div class="top" onclick="common.top.click();"><i class="fa fa-top"></i></div>');
                    _this = $api.dom('.top');
                }

                if (this.target.scrollTop > 1) {
                    $api.show(_this);
                    return;
                }
                $api.hide(_this);
            };

            // click 
            this.click = function () {

                var that = this.target,
                    timer = setInterval(function () {
                        that.scrollTop -= that.scrollTop / 5;
                        if (that.scrollTop <= 0) {
                            that.scrollTop = 0;
                            clearInterval(timer);
                        }
                    }, 20);
            };
        };

        // 翻译 
        // txt 待翻译字符
        // callback 回调函数 
        this.translate = function (txt, callback) {
            var r = 'zh';

            if (/[^\u0000-\u00FF]/.test(txt)) r = 'en';

            // 翻译 
            // q 待翻译字符 
            // r  翻译结果 zh ( 为中文 ) , en ( 为英文 )
            common.ajax({
                method: 'get',
                type: 'html',
                url: 'http://192.169.55.240:1010/Common/Translate',
                data: 'q=' + txt + '&r=' + r ,
                callback: function (rs) {

                    rs = $api.parse(rs.html);

                    if (typeof callback === 'function') callback.call(rs, rs.trans_result[0].dst);
                }
            });

        };
    };

    o.common = common;

})(window);

/**
 * 数组 Array indexOf 函数 
 * Desc: 获得指定元素位置   
 */
Array.prototype.indexOf = function (val, is) {
    is = is || 0;
    for (var i = 0; i < this.length; i++) {
        if (is == 0 && this[i] == val) return i;
        if (is != 0 && this[i].indexOf(val) > -1) return i;
    }
    return -1;
};
/**
 * 数组 Array remove 函数 
 * Desc: 删除指定元素  
 */
Array.prototype.remove = function (val) {
    var index = this.indexOf(val);
    if (index > -1) {
        this.splice(index, 1);
    }
};
/**
 * 数组 Array remove 函数 
 * Desc: 删除指定 like 子元素 
 */
Array.prototype.removeLike = function (val) {
    var index = this.indexOf(val, 1);
    if (index > -1) {
        this.splice(index, 1);
        this.removeLike(val);
    }
};
/**
 * 数组 Array forEach 函数 
 * Desc: 循环每一个 Array 子元素 
 */
Array.prototype.forEach = function (callback) {

    if (typeof callback === 'function') {
        var len = this.length,
            i = 0,
            obj = null;

        for (i = 0; i < len; i++) {

            obj = this[i];

            callback(obj);
        }
    }
}

var log = function (msg) {
    try {
        console.log(msg);

    } catch (e) {

        alert(msg);
    }
};
