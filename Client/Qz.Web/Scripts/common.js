(function (win) {
    /**
     * Desc: 公共函数
     * Author: ZPC
     * AddDate: 2015-3-5 13:40:36 
     */
    var common = new function () {
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

        // ajax 请求 
        this.ajax = {
            post: function (opts) {
                $ajax.post({
                    contentType: opts.contentType || "application/x-www-form-urlencoded",
                    url: opts.url || "",
                    data: opts.data || "",
                    type: opts.type || "json",
                    timeout: opts.timeout || 10000,
                    async: opts.async || true,
                    success: function (rs) {
                        opts.callback(rs);
                        common.loading.hide(opts.load && opts.load.type);
                    },
                    error: function (er) {
                        common.loading.hide(opts.load && opts.load.type);
                        if (typeof er === 'object' && er.message.indexOf('请求超时') != -1) {
                            $api.html($api.dom('.content'), '<div class="tac"><i class="fa fa-wifi" onclick="common.reload()"></i><p onclick="common.reload()">请求超时, 点击重新加载 !</p></div>');
                            return;
                        }
                        if (opts.error && typeof opts.error == 'function') opts.error(er); else common.tip(er.message);
                    },
                    before: function () {
                        common.loading.show(opts.load && opts.load.type);
                    },
                    complete: function () { },
                    showStatus: function () { }
                });
            },


            get: function (opts, t) {

                var _ajax = new Ajax();
                if (t) {
                    _ajax.post({
                        url: opts.url || "",
                        data: opts.data || "",
                        type: opts.type || "json",
                        timeout: opts.timeout || 15000,
                        async: opts.async || true,
                        success: function (rs) {
                            if (typeof opts.callback === 'function') opts.callback(rs);
                            common.loading.hide(opts.load && opts.load.type);
                        },
                        error: function (er) {
                            common.loading.hide(opts.load && opts.load.type);
                            if (typeof er === 'object' && er.message.indexOf('请求超时') != -1) {
                                $api.html($api.dom('.content'), '<div class="tac"><i class="fa fa-wifi" onclick="common.reload()"></i><p onclick="common.reload()">请求超时, 点击重新加载 !</p></div>');
                                return;
                            }
                            if (opts.error && typeof opts.error == 'function') opts.error(er); else common.tip(er.message);
                        },
                        before: function (o) {
                            common.loading.show(opts.load && opts.load.type);
                        },
                        complete: function () { }
                    });

                    return;
                }
                _ajax.get({
                    url: opts.url || "",
                    data: opts.data || "",
                    type: opts.type || "json",
                    timeout: opts.timeout || 15000,
                    async: opts.async || true,
                    success: function (rs) {
                        if (typeof opts.callback === 'function') opts.callback(rs);
                        common.loading.hide(opts.load && opts.load.type);
                    },
                    error: function (er) {
                        common.loading.hide(opts.load && opts.load.type);
                        if (typeof er === 'object' && er.message.indexOf('请求超时') != -1) {
                            $api.html($api.dom('.content'), '<div class="tac"><i class="fa fa-wifi" onclick="common.reload()"></i><p onclick="common.reload()">请求超时, 点击重新加载 !</p></div>');
                            return;
                        }
                        if (opts.error && typeof opts.error == 'function') opts.error(er); else common.tip(er.message);
                    },
                    before: function (o) {
                        common.loading.show(opts.load && opts.load.type);
                    },
                    complete: function () { }
                });
            }

        };

        // tip 提示框 
        this.tip = function (msg, timer) {
            var _this,//= $api.dom('.tip'); 
                $body = $api.dom('body');

            timer = timer || 2000;
            $api.append($body, '<div class="tip animated"></div>');
            _this = $api.domAll('.tip')[_istip++];
            msg = $api.stringify(msg);
            console.log(msg);
            $api.text(_this, msg);
            $api.css(_this, 'display:block;opacity:1;');
            $api.css(_this, 'left:' + (($body.offsetWidth - _this.offsetWidth) / 2) + 'px;width:' + (_this.offsetWidth - 5) + 'px;bottom:' + (10 + (_istip - 1) * 5) + '%');
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

        // 异步加载 js , css 
        this.loadJC = function (src, type) {
            var obj, tag;
            if (src.indexOf('?') != -1) {
                obj = common.parse(src.split('?')[1]);
            }
            type = type || "";
            switch (type.toLowerCase()) {
                case "css":
                    tag = document.createElement("style");
                    tag.src = src;
                    document.head.appendChild(tag);
                    break;
                default:
                    tag = document.createElement("script");
                    tag.src = src;
                    document.body.appendChild(tag);
                    break;
            }

            if (obj && obj.jcallback) {
                eval(obj.jcallback + '()');
            }
        };

        // 重新加载 
        this.reload = function () {
            window.location.reload();
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
            }
        };

        var _istip = 0;
    };

    win.common = common;
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
