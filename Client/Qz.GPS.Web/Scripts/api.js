/*
 * APICloud JavaScript Library
 * Copyright (c) 2014 apicloud.com
 * 修改: 
 *      1. css addEvt 批量处理 
 * 		2. isNodeList 
 *		3. isElement 
 */
(function (window) {

    var u = {};
    var isAndroid = (/android/gi).test(navigator.appVersion);
    var uzStorage = function () {
        var ls = window.localStorage;
        //if (isAndroid) {
        //    // ls = os.localStorage();
        //}
        return ls;
    };

    /**
     * @description 移动端事件  
     * @author 2015-4-14 14:35:07 
     */
    (function (o) {
        "use strict";

        /**
         * touch 事件 
         * 
         * @param {Object} el DOM 元素 
         * @param {String} type 事件类型 'tap 
         *                   swipeUp swipeRight swipeDown swipeLeft 
         *                   longSwipeUp longSwipeRight longSwipeDown longSwipeLeft' 
         * @param {Function} func 回调函数  
         */
        var touchEvents = function (el, type, func) {
            this.longTime = 400; //用于设置长点击阀值
            this.el = el || document;
            this.func = func || function () { };
            this.type = type || 'tap';
            this.mouseData = {
                sTime: 0,
                eTime: 0,
                sX: 0,
                eX: 0,
                sY: 0,
                eY: 0
            };
            this.addEvent();

        };
        touchEvents.prototype = {
            constructor: touchEvents,
            addEvent: function () {
                var scope = this;
                this.startFn = function (e) {
                    scope.touchStart.call(scope, e);
                };
                this.moveFn = function (e) {
                    scope.touchMove.call(scope, e);
                };
                this.endFn = function (e) {
                    scope.touchEnd.call(scope, e);
                };
                this.el.addEventListener('touchstart', this.startFn);
                this.el.addEventListener('touchmove', this.moveFn);
                this.el.addEventListener('touchend', this.endFn);
            },
            removeEvent: function () {
                this.el.removeEventListener('touchstart', this.touchStart);
                this.el.removeEventListener('touchmove', this.touchMove);
                this.el.removeEventListener('touchend', this.touchEnd);
            },
            touchStart: function (e) {
                var pos = e.changedTouches[0];
                this.mouseData.sTime = new Date().getTime();
                this.mouseData.sX = pos.pageX;
                this.mouseData.sY = pos.pageY;
            },
            touchMove: function (e) {
                e.preventDefault();
                return false;
            },
            touchEnd: function (e) {
                var pos = e.changedTouches[0];
                this.mouseData.eTime = new Date().getTime();
                this.mouseData.eX = pos.pageX;
                this.mouseData.eY = pos.pageY;
                this.onTouchEnd();
            },
            onTouchEnd: function (e) {
                if (this.type == this._getDir()) {
                    this.func.call(this.el, this);
                }
            },
            _getDir: function () {
                //时间间隔，间隔小于100都认为是快速，大于400的认为是慢速
                var timeLag = this.mouseData.eTime - this.mouseData.sTime;
                var dir = 'swipe';
                if (timeLag > this.longTime) dir = 'longSwipe';
                if (this.mouseData.sX == this.mouseData.eX && this.mouseData.sY == this.mouseData.eY) {
                    dir = 'tap';
                    if (timeLag > this.longTime) dir = 'longTap';
                } else {
                    if (Math.abs(this.mouseData.eY - this.mouseData.sY) > Math.abs(this.mouseData.eX - this.mouseData.sX)) {
                        dir = this._getUDDir(dir);
                    } else {
                        dir = this._getLRDir(dir);
                    }
                }

                console.log('间隔：' + timeLag + ', 方向：' + dir);
                return dir;
            },
            //单独用于计算上下的
            _getUDDir: function (dir) {
                if (this.mouseData.eY - this.mouseData.sY > 0) dir += 'Down';
                if (this.mouseData.eY - this.mouseData.sY < 0) dir += 'Up';
                return dir;
            },
            //计算左右
            _getLRDir: function (dir) {
                if (this.mouseData.eX - this.mouseData.sX > 0) dir += 'Right';
                if (this.mouseData.eX - this.mouseData.sX < 0) dir += 'Left';
                return dir;
            }
        };

        o.touch = touchEvents;
    })(u);

    function parseArguments(url, data, fnSuc, dataType) {
        if (typeof (data) == 'function') {
            dataType = fnSuc;
            fnSuc = data;
            data = undefined;
        }
        if (typeof (fnSuc) != 'function') {
            dataType = fnSuc;
            fnSuc = undefined;
        }
        return {
            url: url,
            data: data,
            fnSuc: fnSuc,
            dataType: dataType
        };
    }
    u.trim = function (str) {
        if (String.prototype.trim) {
            return str == null ? "" : String.prototype.trim.call(str);
        } else {
            return str.replace(/(^\s*)|(\s*$)/g, "");
        }
    };
    u.trimAll = function (str) {
        return str.replace(/\s*/g, '');
    };
    u.isArray = function (obj) {
        if (Array.isArray) {
            return Array.isArray(obj);
        } else {
            return (obj instanceof Array) || (Object.prototype.toString.call(obj) === '[object Array]');
        }
    };
    u.addEvt = function (el, name, fn, useCapture) {
        useCapture = useCapture || false;
        if (typeof el === 'object' && u.isNodeList(el)) {
            var i = el.length;
            while (i--) {
                el[i].addEventListener(name, fn, useCapture);
            }
        } else {
            if (typeof el === 'string') el = u.byId(el) || u.dom(el);
            if (el.addEventListener) el.addEventListener(name, fn, useCapture);
        }
    };
    u.addTouchEvt = function (el, name, fn) {

        if (typeof el === 'object' && u.isNodeList(el)) {
            var i = el.length;
            while (i--) {
                new u.touch(el[i], name, fn);
            }
        } else {
            if (typeof el === 'string') el = u.byId(el) || u.dom(el);
            new u.touch(el, name, fn);

        }
    };

    u.rmEvt = function (el, name, fn, useCapture) {
        useCapture = useCapture || false;
        if (typeof el === 'object' && u.isNodeList(el)) {
            var i = el.length;
            while (i--) {
                el[i].removeEventListener(name, fn, useCapture);
            }
        } else {
            if (typeof el === 'string') el = u.byId(el) || u.dom(el);
            if (el.removeEventListener) el.removeEventListener(name, fn, useCapture);
        }
    };
    u.addRmEvt = function (el, rmEvt, addEvt, fn, useCapture) {
        u.rmEvt(el, rmEvt);
        u.addEvt(el, addEvt, fn, useCapture);
    };
    u.show = function (el) {
        if (typeof el === 'string') el = u.byId(el) || u.dom(el);
        u.css(el, 'display:block;');
    };
    u.hide = function (el) {
        if (typeof el === 'string') el = u.byId(el) || u.dom(el);
        u.css(el, 'display:none;');
    };
    u.one = function (el, name, fn, useCapture) {
        useCapture = useCapture || false;
        var that = this;
        var cb = function () {
            fn && fn();
            that.rmEvt(el, name, cb, useCapture);
        };
        that.addEvt(el, name, cb, useCapture);
    };
    u.dom = function (el, selector) {
        if (arguments.length === 1 && typeof arguments[0] == 'string') {
            if (document.querySelector) {
                return document.querySelector(arguments[0]);
            }
        } else if (arguments.length === 2) {
            if (typeof el === 'string') el = u.byId(el);
            if (el.querySelector) {
                return el.querySelector(selector);
            }
        }
    };
    u.domAll = function (el, selector) {

        if (arguments.length === 1 && typeof arguments[0] == 'string') {
            if (document.querySelectorAll) {
                return document.querySelectorAll(arguments[0]);
            }
        } else if (arguments.length === 2) {
            if (typeof el === 'string') el = u.byId(el) || u.dom(el);
            if (el.querySelectorAll) {
                return el.querySelectorAll(selector);
            }
        }
    };
    u.byId = function (id) {
        return document.getElementById(id);
    };
    u.first = function (el, selector) {
        if (arguments.length === 1) {
            return el.children[0];
        }
        if (arguments.length === 2) {
            return this.dom(el, selector + ':first-child');
        }
    };
    u.last = function (el, selector) {
        if (arguments.length === 1) {
            var children = el.children;
            return children[children.length - 1];
        }
        if (arguments.length === 2) {
            return this.dom(el, selector + ':last-child');
        }
    };
    u.eq = function (el, index) {
        return this.dom(el, ':nth-child(' + index + ')');
    };
    u.not = function (el, selector) {
        return this.domAll(el, ':not(' + selector + ')');
    };
    u.prev = function (el) {
        var node = el.previousSibling;
        if (node.nodeType && node.nodeType === 3) {
            node = node.previousSibling;
            return node;
        }
    };
    u.next = function (el) {
        var node = el.nextSibling;
        if (node.nodeType && node.nodeType === 3) {
            node = node.nextSibling;
            return node;
        }
    };
    u.closest = function (el, selector) {
        var doms, targetDom;
        var isSame = function (doms, el) {
            var i = 0, len = doms.length;
            for (i; i < len; i++) {
                if (doms[i].isEqualNode(el)) {
                    return doms[i];
                }
            }
            return false;
        };
        var traversal = function (el, selector) {
            doms = u.domAll(el.parentNode, selector);
            targetDom = isSame(doms, el);
            while (!targetDom) {
                el = el.parentNode;
                if (el != null && el.nodeType == el.DOCUMENT_NODE) {
                    return false;
                }
                traversal(el, selector);
            }

            return targetDom;
        };

        return traversal(el, selector);
    };
    u.remove = function (el) {
        if (el && el.parentNode) {
            el.parentNode.removeChild(el);
        }
    };
    u.hasEle = function (el, selector) {
        return u.dom(el, selector) != null;
    };
    u.rmChild = function (el, ch) {
        if (typeof arguments[0] === 'string') {
            el = u.byId(el) || u.dom(el);
        }
        if (typeof arguments[1] === 'string') {
            ch = u.domAll(el, ch);
        }
        if (el.children && ch != null && typeof ch === 'object') {
            if (ch.length >= 1) {
                var i = ch.length;
                while (i--) {
                    try { el.removeChild(ch[i]); } catch (e) { }
                }
            } else {
                try { el.removeChild(ch); } catch (e) { }
            }
        }
    };
    u.data = function (el, name, value) {
        if (typeof arguments[0] == 'string') {
            el = u.byId(el) || u.dom(el);
        }
        if (arguments.length == 2) {
            return u.attr(el, 'data-' + name);
        } else if (arguments.length == 3) {
            u.attr(el, 'data-' + name, value);
            return el;
        }
    };
    u.attr = function (el, name, value) {
        if (el === undefined) return;
        if (typeof arguments[0] == 'string') {
            el = u.byId(el) || u.dom(el);
        }
        if (arguments.length == 2) {
            return el.getAttribute(name);
        } else if (arguments.length == 3) {
            el.setAttribute(name, value);
            return el;
        }
    };
    u.removeAttr = function (el, name) {
        if (arguments.length === 2) {
            el.removeAttribute(name);
        }
    };
    u.addRmAttr = function (el, attr, val) {
        if (typeof arguments[0] == 'string') {
            el = u.byId(el) || u.dom(el);
        }
        el.removeAttribute(attr);
        el.setAttribute(attr, val);
    };
    u.addRmCls = function (el, oldCls, cls) {
        if (typeof arguments[0] == 'string') {
            el = u.byId(el) || u.dom(el);
        }

        if (el != null) {
            u.rmCls(el, oldCls);
            u.addCls(el, cls);
        }
    };
    u.hasCls = function (el, cls) {
        var arr = el.className.split(' ');
        for (var i = 0; i < arr.length; i++) {
            if (u.trim(arr[i]) == cls) return true;
        }
        return false;
    };
    u.addCls = function (el, cls) {
        var preCls, newCls, acls = cls.split(' '), len = acls.length > 1 && acls.length, i = 0;
        if (typeof el === 'object' && u.isNodeList(el)) {
            i = el.length;
            while (i--) {
                if (u.hasCls(el[i], cls)) { u.rmCls(el[i], cls); }
                preCls = el[i].className;
                newCls = u.trim(preCls) + ' ' + cls;
                el[i].className = newCls;
            }
        } else {
            if (typeof arguments[0] == 'string') {
                el = u.byId(el) || u.dom(el);
            }

            if (len > 1) {
                for (i = 0; i < len; i++) {
                    if (u.hasCls(el, acls[i])) { u.rmCls(el, acls[i]); }
                }
            } else {
                if (u.hasCls(el, cls)) { u.rmCls(el, cls); }
            }

            preCls = el.className;
            newCls = u.trim(preCls) + ' ' + cls;
            el.className = newCls;
        }

        return el;
    };
    u.rmCls = function (el, cls) {
        var preCls, newCls;
        if (typeof el === 'object' && u.isNodeList(el)) {
            var i = el.length;
            while (i--) {
                preCls = el[i].className;
                newCls = preCls.replace(cls, '');
                el[i].className = u.trim(newCls);
            }
        } else {
            if (typeof arguments[0] == 'string') {
                el = u.byId(el) || u.dom(el);
            }
            preCls = el.className;
            newCls = preCls.replace(cls, '');
            el.className = u.trim(newCls);
        }
        return el;
    };
    u.toggleCls = function (el, cls) {
        if (u.hasCls(el, cls)) {
            u.rmCls(el, cls);
        } else {
            u.addCls(el, cls);
        }
        return el;
    };
    u.val = function (el, val) {
        if (typeof arguments[0] == 'string') {
            el = u.byId(el) || u.dom(el);
        }

        if (arguments.length === 1) {
            switch (el.tagName) {
                case 'SELECT':
                    var value = el.options[el.selectedIndex].value;
                    return value;
                case 'INPUT':
                    if (el.type == 'number' && u.trim(el.value) == '') {
                        return '0';
                    }
                    return el.value;
                case 'TEXTAREA':
                    return el.value;
            }
        }
        if (arguments.length === 2) {
            switch (el.tagName) {
                case 'SELECT':
                    el.options[el.selectedIndex].value = val;
                    return el;
                case 'INPUT':
                    el.value = val;
                    return el;
                case 'TEXTAREA':
                    el.value = val;
                    return el;
            }
        }

    };
    u.prepend = function (el, html) {
        el.insertAdjacentHTML('afterbegin', html);
        return el;
    };
    u.append = function (el, html) {
        if (u.isElement(html)) {
            return el.appendChild(html);
        }
        el.insertAdjacentHTML('beforeend', html);
        return el;
    };
    u.before = function (el, html) {
        el.insertAdjacentHTML('beforebegin', html);
        return el;
    };
    u.after = function (el, html) {
        el.insertAdjacentHTML('afterend', html);
        return el;
    };
    u.html = function (el, html) {
        if (typeof arguments[0] == 'string') {
            el = u.byId(el) || u.dom(el);
        }
        if (arguments.length === 1) {
            return el.innerHTML;
        } else if (arguments.length === 2) {
            el.innerHTML = html;
            return el;
        }
    };
    u.text = function (el, txt, t) {
        if (typeof arguments[0] == 'string') {
            el = u.byId(el) || u.dom(el);
        }
        if (arguments.length === 1) {
            return el.textContent;
        } else if (arguments.length >= 2) {
            if (typeof el === 'object' && u.isNodeList(el)) {
                var i = el.length;
                while (i--) {
                    if (txt === '' && t === 'this') el[i].textContent = u.trim(el[i].textContent);
                    else el[i].textContent = txt;
                }
            } else {
                if (txt === '' && t === 'this') el.textContent = u.trim(el.textContent);
                else el.textContent = txt;
            }
            return el;
        }
    };
    u.offset = function (el) {
        var sl, st;
        if (document.documentElement) {
            sl = document.documentElement.scrollLeft;
            st = document.documentElement.scrollTop;
        } else {
            sl = document.body.scrollLeft;
            st = document.body.scrollTop;
        }
        var rect = el.getBoundingClientRect();
        return {
            l: rect.left + sl,
            t: rect.top + st,
            w: el.offsetWidth,
            h: el.offsetHeight
        };
    };
    u.css = function (el, css) {
        if (typeof arguments[0] == 'string') {
            el = u.byId(el) || u.dom(el);
        }

        if (el != null && typeof el === 'object' && typeof css === 'string' && css.indexOf(':') > 0) {
            if (u.isNodeList(el)) {
                var i = el.length;
                while (i--) {
                    el[i].style.cssText += ';' + css;
                }
            } else {
                el.style && (el.style.cssText += ';' + css);
            }
        }
    };
    u.cssVal = function (el, prop) {
        if (arguments.length === 2) {
            if (typeof arguments[0] == 'string') {
                el = u.byId(el) || u.dom(el);
            }

            var computedStyle = window.getComputedStyle(el, null);
            return computedStyle.getPropertyValue(prop);
        }
    };
    u.stringify = function (json) {
        if (typeof json === 'string') return json;
        if (typeof json === 'object') {
            if (JSON) {
                return JSON.stringify(json);
            }
            var s, i, j;
            if (s = [], json instanceof Array)
                for (i = 0; i < json.length; i++)
                    s.push('"' + (json[i].name || i) + '":"' + json[i] + '"');
            else
                for (j in json) s.push('"' + j + '":"' + json[j] + '"');
            return s && "{" + s.join(",") + "}";
        }
    };
    u.parse = function (str) {
        if (typeof json === 'object') return str;
        if (typeof str === 'string') {
            if (JSON) {
                return JSON.parse(str);
            }
            str = str.substring(1, str.length - 1);
            var set, item, i;
            for (set = {}, str = str.split(","), i = 0; i < str.length; i++)
                str[i].indexOf(":") > 0 && (item = str[i].split(":"), set[item[0].replace('"', '').replace('"', '')] = item[1].replace('"', '').replace('"', ''));
            return set;
        }
    };
    u.setStorage = function (key, value) {
        if (arguments.length === 2) {
            var v = value;
            if (typeof v == 'object') {
                v = u.stringify(v);
                v = 'obj-' + v;
            } else {
                v = 'str-' + v;
            }
            var ls = uzStorage();
            if (ls) {
                ls.setItem(key, v);
            }
        }
    };
    u.getStorage = function (key) {
        var ls = uzStorage();
        if (ls) {
            var v = ls.getItem(key);
            if (!v) { return; }
            if (v.indexOf('obj-') === 0) {
                v = v.slice(4);
                return u.parse(v);
            } else if (v.indexOf('str-') === 0) {
                return v.slice(4);
            }
        }
    };
    u.rmStorage = function (key) {
        var ls = uzStorage();
        if (ls && key) {
            ls.removeItem(key);
        }
    };
    u.clearStorage = function () {
        var ls = uzStorage();
        if (ls) {
            ls.clear();
        }
    };
    /*by zpc */
    u.isNodeList = function (obj) {
        return Object.prototype.toString.call(obj) === '[object NodeList]';
    };
    u.isElement = function (obj, name) {
        var str = Object.prototype.toString.call(obj);
        name = name || '';
        if (name) {
            return (typeof name === 'string') && (/element/i.test(str)) && (str.toLocaleLowerCase().indexOf(name.toLocaleLowerCase()) != -1);
        } else {
            return /element/i.test(str);
        }
    };
    //u.extend = function (des, src, override) {
    //    if (src instanceof Array) {
    //        for (var i = 0, len = src.length; i < len; i++)
    //            u.extend(des, src[i], override);
    //    }
    //    for (var i in src) {
    //        if (override || !(i in des)) {
    //            des[i] = src[i];
    //        }
    //    }
    //    return des;
    //};
    u.extend = function (rs, dft, src) {

        for (var i in dft) {
            if (i in src) {
                rs[i] = src[i];
                continue;
            }

            rs[i] = dft[i];
        }
        return rs;
    };

    /*by king*/
    u.fixIos7Bar = function (el) {
        var strDM = api.systemType;
        if (strDM == 'ios') {
            var strSV = api.systemVersion;
            var numSV = parseInt(strSV, 10);
            var fullScreen = api.fullScreen;
            var iOS7StatusBarAppearance = api.iOS7StatusBarAppearance;
            if (numSV >= 7 && !fullScreen && iOS7StatusBarAppearance) {
                el.style.paddingTop = '20px';
            }
        }
    };
    u.toast = function (title, text, time) {
        var opts = {};
        var show = function (opts, time) {
            api.showProgress(opts);
            setTimeout(function () {
                api.hideProgress();
            }, time);
        };
        if (arguments.length === 1) {
            time = time || 500;
            if (typeof title === 'number') {
                time = title;
            } else {
                opts.title = title + '';
            }
            show(opts, time);
        } else if (arguments.length === 2) {
            time = time || 500;

            if (typeof text === "number") {
                var tmp = text;
                time = tmp;
                text = null;
            }
            if (title) {
                opts.title = title;
            }
            if (text) {
                opts.text = text;
            }
            show(opts, time);
        }
        if (title) {
            opts.title = title;
        }
        if (text) {
            opts.text = text;
        }
        time = time || 500;
        show(opts, time);
    };
    u.post = function (/*url,data,fnSuc,dataType*/) {
        var argsToJson = parseArguments.apply(null, arguments);
        var json = {};
        var fnSuc = argsToJson.fnSuc;
        argsToJson.url && (json.url = argsToJson.url);
        argsToJson.data && (json.data = argsToJson.data);
        if (argsToJson.dataType) {
            var type = toLowerCase(argsToJson.dataType);
            if (type == 'text' || type == 'json') {
                json.dataType = type;
            }
        } else {
            json.dataType = 'json';
        }
        json.method = 'post';
        api.ajax(json,
            function (ret, err) {
                if (ret) {
                    fnSuc && fnSuc(ret);
                }
            }
        );
    };
    u.get = function (/*url,fnSuc,dataType*/) {
        var argsToJson = parseArguments.apply(null, arguments);
        var json = {};
        var fnSuc = argsToJson.fnSuc;
        argsToJson.url && (json.url = argsToJson.url);
        //argsToJson.data && (json.data = argsToJson.data);
        if (argsToJson.dataType) {
            var type = toLowerCase(argsToJson.dataType);
            if (type == 'text' || type == 'json') {
                json.dataType = type;
            }
        } else {
            json.dataType = 'text';
        }
        json.method = 'get';
        api.ajax(json,
            function (ret, err) {
                if (ret) {
                    fnSuc && fnSuc(ret);
                }
            }
        );
    };

    window.$api = u;

})(window);


/**
 * Box 
 * @author ZPC 2015-4-14 17:26:01  
 * @version 1.0 
 */
(function (o) {

    "use strict";

    var DEFAULTS = {
        oper: '<span class="btn-ok">确 定</span>',
        show: false,
        target: '',
        key: '',
        value: '',
        title: ''
    };

    // 属性 
    Box.prototype = {
        arr: {},
        dom: {},
        opts: {},
        old: {},
        obj: {
            key: '',
            value: ''
        }
    };

    /**
     * @constructor Select
     * @param {Object} opt 
     */
    function Box(opt) {

        this.opts = $api.extend({}, DEFAULTS, opt);

        this.creaet();
    }

    // 创建 Box 
    Box.prototype.creaet = function () {
        var _box = this,
            _target;

        if (typeof this.opts.target === 'string') {
            _target = $api.byId(this.opts.target) || $api.dom(this.opts.target);
        }

        if (!_target) {
            $api.append($api.dom('body'), '<div class="ui-select" id="' + this.opts.target + '"> <div class="ui-header"> <span class="ui-title"> ' + (this.opts.show ? this.opts.oper : '<i class="fa fa-win"></i>' + this.opts.title) + ' </span> <span class="ui-close">×</span> </div> <div class="ui-content"></div> </div>');
            _target = $api.byId(this.opts.target) || $api.dom(this.opts.target);

            if ($api.dom(_target, '.btn-ok')) {
                // 确定 
                $api.addEvt($api.dom(_target, '.btn-ok'), 'click', function () {
                    _box.ok.call(_box);
                });
            }

            // 关闭 
            $api.addEvt($api.dom(_target, '.ui-close'), 'click', function () {
                _box.close.call(_box);
            });

        }

        this.dom = _target;

        return this;
    };
    // 打开 
    Box.prototype.open = function () {
        setTimeout(function () {
            // 遮蔽层
            $api.remove($api.dom('.l-mask'));
            $api.append($api.dom('body'), '<div class="l-mask animated fadeIn" style="display:block;"></div>');
            $api.css('.l-mask', 'z-index:' + (parseInt($api.cssVal('.l-mask', 'z-index')) + 11));

        }, 100);

        $api.show(this.opts.target);
        $api.css(this.opts.target, 'z-index:' + (parseInt($api.cssVal(this.opts.target, 'z-index')) + 11));

        return this;
    };
    // 关闭 
    Box.prototype.close = function () {
        $api.remove($api.dom('.l-mask'));
        $api.hide(this.opts.target);
        $api.css(this.opts.target, 'z-index:' + (parseInt($api.cssVal(this.opts.target, 'z-index')) - 11));

        return this;
    };
    // 选中 
    Box.prototype.check = function (_target, callback) {
        var _this = this;
        if (this.arr.length <= 0) return;

        // 保存当前选中的 dom 
        this.old = $api.domAll(_target.parentNode, '.select');

        $api.rmCls(this.old, 'select');
        $api.text(this.old, '', 'this');
        $api.addCls(_target, 'select');
        var txt = $api.trim($api.text(_target));
        $api.html(_target, '<i class="fa fa-select"></i>' + txt);

        // 获得当前操作的数据
        this.obj.key = $api.data(_target, 'id');
        this.obj.value = txt;

        var timer = setTimeout(function () {
            _this.ok();
            if (typeof callback === 'function') {
                callback();
            }

            clearTimeout(timer);
        }, 500);

        return this;
    };
    // 确定 
    Box.prototype.ok = function () {

        if (this.obj.value == '') {
            return;
        }

        // 关闭 
        this.close();

        $api.val(this.opts.key, this.obj.key);
        $api.val(this.opts.value, this.obj.value);

        this.obj = { key: '', value: '' };

        return this;
    };

    o.Box = Box;

})(window);

/**
 * Select 下拉框  
 * @author ZPC 2015-4-14 17:14:10 
 * @version 1.0 
 */
(function (o) {

    "use strict";

    var DEFAULTS = {
        title: '数据列表',
        show: true,
        isdft: true,
        defaults: '',
        target: '',
        key: '',
        value: '',
        url: '',
        data: '',
        html: '',
        select: '',
        change: function () { },
        callback: function () { }
    },

    // 绑定事件 
    bind = {
        event: function (obj) {
            $api.addEvt(obj.box.arr, 'click', function () {
                obj.change(this);
            });
        }
    };

    /**
     * @constructor Select
     * @param {Object} opt 
     */
    function Select(opt) {
        this.opts = $api.extend({}, DEFAULTS, opt);

        this.create();
    }

    // 属性 
    Select.prototype = {
        dom: {},
        box: {},
        opts: {}
    };

    // 创建 Select 
    Select.prototype.create = function () {
        var arr = [], _this = this;

        // 1. 创建 Box 
        _this.box = new Box({
            title: _this.opts.title,
            target: _this.opts.target,
            key: _this.opts.key,
            value: _this.opts.value
        });

        // 2.0 已存在 
        if (_this.box.arr.length > 0) {
            if (_this.opts.show) {
                _this.show();
            }

            if (typeof _this.opts.callback === 'function') _this.opts.callback();
            return _this;
        }

        // 2.1 显示指定 HTML 
        if (_this.opts.html) {
            $api.html($api.dom(_this.box.dom, '.ui-content'), '<ul class="scroll container">' + _this.opts.html + '</ul>');
            if (_this.opts.show) {
                _this.show();
            }

            arr = $api.domAll(_this.box.dom, 'li');
            if (arr.length == 0) {
                _this.box.arr = [];
                return _this;
            }
            _this.box.arr = arr;
            bind.event(_this);

            if (typeof _this.opts.callback === 'function') _this.opts.callback();
            return _this;
        }

        // 3. ajax 请求 
        common.ajax.post({
            url: this.opts.url,
            data: this.opts.data,
            callback: function (rs) {
                if (rs.Status != 0) {
                    common.tip(rs.Message);

                    if (typeof _this.opts.callback === 'function') _this.opts.callback();
                    return _this;
                }

                var i = 0,
                    html = [],
                    data = {},
                    len = rs.Obj.Data.length;

                // 显示默认 HTML 
                if (_this.opts.isdft) {
                    if (_this.opts.select == '0') {
                        html.push('<li data-id="0" class="select"><i class="fa fa-select"></i>全部</li>');
                        $api.val(_this.opts.key, 0);
                        $api.val(_this.opts.value, '全部');
                    } else {
                        html.push('<li data-id="0">全部</li>');
                    }
                }
                for (i = 0; i < len; i++) {
                    data = rs.Obj.Data[i];

                    if (!_this.opts.isdft && _this.opts.select == '' && i == 0) {
                        html.push('<li data-id="' + data.Key + '" class="select"><i class="fa fa-select"></i>' + data.Value + '</li>');
                        $api.val(_this.opts.key, data.Key);
                        $api.val(_this.opts.value, data.Value);
                        continue;
                    }
                    if (data.Key == _this.opts.select) {
                        html.push('<li data-id="' + data.Key + '" class="select"><i class="fa fa-select"></i>' + data.Value + '</li>');
                        continue;
                    }
                    html.push('<li data-id="' + data.Key + '">' + data.Value + '</li>');
                }

                $api.html($api.dom(_this.box.dom, '.ui-content'), '<ul class="scroll container">' + html.join('') + '</ul>');

                if (_this.opts.show) _this.show();

                var arr = $api.domAll(_this.box.dom, 'li');
                if (arr.length == 0) {
                    _this.box.arr = [];

                    if (typeof _this.opts.callback === 'function') _this.opts.callback();
                    return _this;
                }
                _this.box.arr = arr;
                bind.event(_this);
                if (typeof _this.opts.callback === 'function') _this.opts.callback();
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

        this.box.check(_target, function () {
            _this.opts.change.call(_target, _this);
        });

        return this;
    };
    // 清除 
    Select.prototype.clear = function () {
        $api.html($api.dom(this.box.dom, '.ui-content'), '');
        this.box.arr = [];

        return this;
    };
    // 初始化 
    Select.prototype.init = function () {

        $api.rmCls(this.box.arr, 'select');
        $api.text(this.box.arr, '', 'this');
        $api.addCls(this.box.arr[0], 'select');
        var txt = $api.trim($api.text(this.box.arr[0]));
        $api.html(this.box.arr[0], '<i class="fa fa-select"></i>' + txt);

    };

    o.Select = Select;

})(window);

/**
 * Dialog 框  
 * @author ZPC 2015-4-14 17:14:10 
 * @version 1.0 
 */
(function (o) {

    "use strict";

    var DEFAULTS = {

    };

    function Dialog(opt) {
        this.opts = $api.extend({}, DEFAULTS, opt);

        this._create();
    }

    // 属性  
    Dialog.prototype = {
        box: {},
        dom: {},
        opts: {}
    };

    // 创建 
    Dialog.prototype._create = function () {

        return this;
    };

    // 警告框 
    Dialog.prototype.alert = function () {

        return this;
    };

    // 消息框 
    Dialog.prototype.msg = function () {

        return this;
    };
    // 询问框 
    Dialog.prototype.confirm = function () {

        return this;
    };

    o.Dialog = Dialog;

})(window);
