/**
 * 轻量级 AJAX 
 * @author ZPC 2015-3-13 14:03:21 
 * @version 1.0 
 */
(function (window, undef) {
    var doc = window.document, _obj = "object", _str = "string";
    var ajax = {
        init: function (opt) {
            this[0] = this.create();
            this[1] = {
                contentType: opt.contentType || "application/x-www-form-urlencoded",
                url: opt.url || "",
                method: opt.method || "GET",
                data: opt.data || null,
                async: opt.async || true,
                type: opt.type || "json",
                timeout: opt.timeout || 10000,
                cache: opt.cache || false,
                success: opt.success || function () { },
                error: opt.error || function () { },
                complete: opt.complete || function () { },
                before: opt.before || function () { },
                showStatus: opt.showStatus || function () { }
            };
            Setting(this[1]);
            try {
                var isTimeout = false, cur = this.open();
                var timer = setTimeout(function () {
                    isTimeout = true;
                    // cur.stop();
                    cur[1].error(new AjaxError("请求超时, 请检查你的网络 !"));
                }, cur[1].timeout);

                cur[1].before(this);
                this[0].onreadystatechange = function () {
                    cur[1].showStatus(cur[0].readyState);
                    if (cur[0].readyState == 4 && !isTimeout) {

                        try {
                            if (IsOK(cur[0])) {
                                var t = HttpData(cur[0], cur[1].type);
                                if (t.message && t.message.indexOf('请重新操作') != -1) {
                                    cur[1].error(t);
                                    return;
                                }
                                cur[1].success(t);
                            }
                            else {
                                cur[1].error(new AjaxError("请求未成功完成"));
                            }

                        } catch (et) {
                            cur[1].error(new AjaxError(et.message));
                        } finally {
                            cur[1].complete();
                            cur[0] = null;
                            clearTimeout(timer);
                        }
                    }
                };

                this.send();
            } catch (e) {
                this[1].error(new AjaxError(e.message));
            } finally {
                return this;
            }
        },
        create: function () {
            if (window.XMLHttpRequest == undef) {
                window.XMLHttpRequest = function () {
                    if (window.ActiveXObject) {
                        try {
                            return new ActiveXObject("Msxml2.XMLHTTP");
                        } catch (e) {
                            return new ActiveXObject("Microsoft.XMLHTTP");
                        }
                    }
                };
            }
            return new XMLHttpRequest();
        },
        open: function () {
            try {
                this[0].open(this[1].method, this[1].url, this[1].async);
                if (/POST/i.test(this[1].method)) {
                    this[0].setRequestHeader( "Content-Type", this[1].contentType );
                    // this[0].setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
                    // if (this[0].overrideMimeType) this[0].setRequestHeader("Connection", "close");
                }
            } catch (e) {
                throw new AjaxError(e.message)
            }
            return this;
        },
        send: function () {
            try {
                this[0].send(this[1].data);
            } catch (e) {
                throw new AjaxError(e.message);
            }
            return this;
        },
        stop: function () {
            try { this[0].abort(); } catch (e) { throw new AjaxError(e.message) }
            return this;
        },
        get: function (opts) {
            return this.init(opts);
        },
        post: function (opts) {
            opts.method = "POST";
            return this.init(opts);
        },
        parseToQueryString: function (obj) {
            var s, i, j;
            if (typeof obj === _str) return obj;
            if (s = [], obj instanceof Array)
                for (i = 0; i < obj.length; i++)
                    s.push(obj[i].name || i + "=" + obj[i]);
            else
                for (j in obj) s.push(j + "=" + obj[j]);
            return s.join("&");
        },
        parseToObject: function (str) {
            var set, item, i; if (typeof str === _obj) return str; for (set = {}, str = str.split("&"), i = 0; i < str.length; i++) str[i].indexOf("=") > 0 && (item = str[i].split("="), set[item[0]] = item[1]); return set;
        }
    };

    function Setting(p) {
        p.data && (p.data = ajax.parseToQueryString(p.data));
        //p.method.toUpperCase() == "POST" && ( p.data = JSON.stringify(p.data) );
        p.method.toUpperCase() == "GET" && p.data && (p.url = Append(p.url, p.data));
        p.cache || (p.url = Append(p.url, "v=" + (new Date).getTime()));
    };
    function IsOK(r) {
        try {
            return !r.status && location.protocol == "file:" || r.status >= 200 && r.status < 300 || r.status == 304 || navigator.userAgent.indexOf("Safari") >= 0 && r.status == undef
        } catch (e) {
        }
        return !1;
    }
    function AjaxError(msg) {
        this.name = "AJAX 错误"; this.message = msg || "未知错误";
    }
    function HttpData(r, type) {
        var res, ct, t = r.getResponseHeader("Content-Type");
        if (/xml/i.test(t)) t = "xml";
        if (/javascript/i.test(t)) t = "js";
        if (/css/i.test(t)) t = "css";
        if (/html/i.test(t)) t = "html";
        if (/json/i.test(t)) t = "json";

        res = type || t;
        if (res != t) { return new AjaxError("数据获取错误, 请重新操作 !"); }
        switch (res) {
            case "xml":
                return r.responseXML.documentElement;
            case "js":
                return r.responseText;
            case "json":
                return eval('(' + r.responseText + ')');
            case "html":
                return {"html" : r.responseText};
            default:
                return r.responseText
        }
    }
    function Append(url, param) {
        if (url.indexOf("?") < 0) { return url + "?" + param; }
        else {
            if (/\?$/.test(url)) { return url + param; }
            else { return url + "&" + param; }
        }
    }

    window.$ajax = ajax;
})(window);
