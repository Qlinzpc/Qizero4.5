﻿/**
 * QzUI.js.lib.ajax.js Ajax 
 * 
 * @Author ZPC 
 * @Date 2015-4-16 11:24:20 
 * @Version 1.0 
 * 
 */
(function (o)
{

    'use strict';

    // 默认配置 
    var DEFAULTS = {
        contentType: "application/x-www-form-urlencoded",
        url: "",
        method: "post",
        data: null,
        async: true,
        type: "json",
        timeout: 15000,
        cache: true,
        success: function() {},
        error: function() {},
        before: function() {},
        complete: function() {}
    };

    // url 参数 append 
    var append = function(url, param) {
        // 不存在 '?' 字符 
        if (!/\?/.test(url)) {
            return url + "?" + param;
        } else {
            // 最后一个字符为 '&'  
            if (url.lastIndexOf("&") === url.length - 1) {
                return url + param;
            } else {
                return url + "&" + param;
            }
        }
    };

    // 解析 
    var parse = function(obj) {
        if (typeof obj === 'string') return obj;

        var arr = [],
            i,
            j;

        if (obj instanceof Array) {
            for (i = 0; i < obj.length; i++) {
                arr.push((obj[i].name || i) + "=" + obj[i]);
            }
        } else {
            for (j in obj) {
                if (obj.hasOwnProperty(j)) {
                    arr.push(j + "=" + obj[j]);
                }
            }
        }

        return arr.join("&");
    };

    // 合并配置 
    var extend = function(rs, dft, src) {
        for (var i in dft) {
            if (dft.hasOwnProperty(i)) {
                if (i in src) {
                    rs[i] = src[i];
                    continue;
                }

                rs[i] = dft[i];
            }
        }
        return rs;
    };

    // Ajax Error 
    var AjaxError = function(msg) {
        this.name = "AJAX 错误";
        this.message = msg || "未知错误";
    };

    // Ajax 构造函数 
    function Ajax()
    {
        return this;
    }

    // 属性 
    Ajax.prototype = {
        config: {},
        xhr: {}
    };

    // 创建 
    Ajax.prototype.create = function ()
    {
        if (!window.XMLHttpRequest) {
            var xhr = this.xhr;
            window.XMLHttpRequest = function ()
            {
                if (window.ActiveXObject)
                {
                    try
                    {
                        xhr = new ActiveXObject("Msxml2.XMLHTTP");
                    } catch (e)
                    {
                        xhr = new ActiveXObject("Microsoft.XMLHTTP");
                    }
                }
            };
        }
        this.xhr = new XMLHttpRequest();

        return this;
    };
    // 设置 
    Ajax.prototype._setting = function ()
    {
        this.config.data = parse(this.config.data);

        if ("get" === this.config.method.toLocaleLowerCase()) {
            this.config.url = append(this.config.url, this.config.data);
        }

        if (!this.config.cache) {
            this.config.url = append(this.config.url, "v=" + (new Date).getTime());
        }

    };
    Ajax.prototype._open = function ()
    {
        try
        {
            this.xhr.open(this.config.method, this.config.url, this.config.async);
            if (/post/i.test(this.config.method))
            {
                this.xhr.setRequestHeader("Content-Type", this.config.contentType);
            }
        } catch (e)
        {
            throw new AjaxError(e.message);
        }
        return this;
    };
    Ajax.prototype._send = function ()
    {
        try
        {
            this.xhr.send(this.config.data);

        } catch (e)
        {
            throw new AjaxError(e.message);
        }
        return this;
    };
    Ajax.prototype._ok = function ()
    {
        try
        {
            return !this.xhr.status && location.protocol === "file:" || this.xhr.status >= 200 && this.xhr.status < 300 || this.xhr.status === 304 || navigator.userAgent.indexOf("Safari") >= 0 && this.xhr.status === undef;
        } catch (e)
        {
        }
        return false;
    };
    Ajax.prototype._data = function ()
    {
        var response = this.xhr.getResponseHeader("Content-Type");

        if (/xml/i.test(response))
        {
            response = "xml";
        }

        if (/JavaScript/i.test(response))
        {
            response = "js";
        }

        if (/html/i.test(response))
        {
            response = "html";
        }

        if (/json/i.test(response))
        {
            response = "json";
        }

        var type = this.config.type || response;

        if (type !== response)
        {
            return new AjaxError("数据类型错误, 请重新操作 !");
        }

        switch (type)
        {
            case "xml":
                return this.xhr.responseXML.documentElement;
            case "js":
                return eval("(" + this.xhr.responseText + ")");
            case "json":
                return eval('(' + this.xhr.responseText + ')');
            case "html":
                return { "html": this.xhr.responseText };
            default:
                return this.xhr.responseText;
        }
    };
    Ajax.prototype._init = function (opt)
    {
        this.config = extend({}, DEFAULTS, opt);

        this.create();

        this._setting();

        try
        {
            var _timeout = false,
                _this = this._open(),
                _timer;

            _timer = setTimeout(function ()
            {
                _timeout = true;
                _this.xhr.abort();
                _this.config.error(new AjaxError("请求超时, 请检查你的网络 !"));

            }, this.config.timeout);

            if (typeof this.config.before === 'function') this.config.before(this);

            this.xhr.onreadystatechange = function ()
            {
                if (_this.xhr.readyState === 4 && !_timeout)
                {
                    try
                    {
                        if (_this._ok.call(_this))
                        {
                            var data = _this._data.call(_this);

                            if (data.message && data.message.indexOf('请重新操作') !== -1)
                            {
                                if (typeof _this.config.error === "function") _this.config.error(data);
                                return;
                            }

                            if (typeof _this.config.success === "function") _this.config.success(data);
                        }
                        else
                        {
                            if (typeof _this.config.error === "function") _this.config.error(new AjaxError("请求失败, 请联系管理员!"));
                        }

                    } catch (er)
                    {
                        console.log(er.stack);
                        if (typeof _this.config.error === "function") _this.config.error(new AjaxError(er.message));

                    } finally
                    {
                        if (typeof _this.config.complete === "function") _this.config.complete();

                        _this.xhr = null;

                        clearTimeout(_timer);
                    }
                }
            };

            this._send();
        } catch (e)
        {
            console.log(e);
            if (typeof this.config.error === 'function') this.config.error(new AjaxError(e.message));
        } finally
        {
            return this;
        }

    };
    // post 请求 
    Ajax.prototype.post = function (opt)
    {
        opt.method = 'post';
        this._init(opt);

        return this;
    };
    // get 请求 
    Ajax.prototype.get = function (opt)
    {
        opt.method = 'get';
        this._init(opt);

        return this;
    };

    o.Ajax = Ajax;

})(window);

