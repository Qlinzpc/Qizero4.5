﻿/**
 * QzUI.js.base.js 
 * @Author ZPC 
 * @Date 2015-4-16 11:25:06 
 * @Version 1.0 
 * 修改: 
 *      1. css addEvt 批量处理 
 * 		2. isNodeList 
 *		3. isElement 
 */
(function (o) {

    var u = {};
    var isAndroid = (/android/gi).test(navigator.appVersion);
    var uzStorage = function () {
        var ls = o.localStorage;
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
    u.isNodeList = function (obj) {
        return (obj instanceof NodeList) || (Object.prototype.toString.call(obj) === '[object NodeList]');
    };
    u.trigger = function (fun, data, call) {

        if (typeof fun === 'function') {
            if (u.isArray(data)) {
                if (call) {
                    fun.apply(call, data);
                } else {
                    fun.apply(this, data);
                }
            } else {
                if (call) {
                    fun.call(call, data);
                } else {
                    fun.call(this, data);
                }
            }
        }
    };

    u.browser = function() {
        var ua = navigator.userAgent;
        if (/chrome/gi.test(ua)) {
            return 'chrome';
        }

        if (/firefox/gi.test(ua)) {
            return 'firefox';
        }

        if (/explorer/gi.test(navigator.appName)) {
            return 'ie';
        }

        return ua;
    };

    u._browser = {
        chrome: 'chrome',
        firefox: 'firefox',
        ie: 'explorer'
    };

    u.stop = {
        event: function (e) {
            try {
                e = e || window.event;
                
                //如果提供了事件对象，则这是一个非IE浏览器 
                if (e && e.stopPropagation) {
                    //因此它支持W3C的stopPropagation()方法 
                    e.stopPropagation();
                } else {
                    //否则，我们需要使用IE的方式来取消事件冒泡 
                    window.event.cancelBubble = true;
                }
            } catch (er) {
                console.log(er);
            }
            
        }
    };
    u.foreach = function (obj, callback) {

        if (typeof callback === 'function') {
            var len = obj.length,
                i = 0;

            for (i = 0; i < len; i++) {

                callback(obj[i]);
            }
        }
    };
    u.addEvt = function (el, name, fn, useCapture) {
        useCapture = useCapture || false;
        if (typeof el === 'object' && u.isNodeList(el)) {
            var i = el.length;
            while (i--) {

                u.addEvt(el[i], name, fn, useCapture);
            }
        } else {
            if (typeof el === 'string') el = u.byId(el) || u.dom(el);

            el.addEventListener ? el.addEventListener(name, fn, useCapture) :
            el.attachEvent ? el.attachEvent("on" + name, fn) :
            el['on' + name] = fn;

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

                el[i].removeEventListener ? el.removeEventListener(name, fn, false) :
			    el[i].detachEvent ? el.detachEvent("on" + name, fn) :
			    el[i]['on' + type] = null;
            }
        } else {
            if (typeof el === 'string') el = u.byId(el) || u.dom(el);

            el.removeEventListener ? el.removeEventListener(name, fn, false) :
			el.detachEvent ? el.detachEvent("on" + name, fn) :
			el['on' + type] = null;
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
        if (!el) return;

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
                    switch (el.type) {
                        case 'number':
                            if (u.trim(el.value) == '') {
                                return '0';
                            }
                            return el.value;
                        case 'checkbox':
                            
                            return el.checked ? 0 : 1;
                        default:
                            return el.value;
                    }
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

        if (typeof arguments[0] === 'string') {
            el = u.byId(el) || u.dom(el);
        }

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
    u.cssVal = function (el, attribute) {
        if (arguments.length === 2) {
            if (typeof arguments[0] == 'string') {
                el = u.byId(el) || u.dom(el);
            }

            return el.currentStyle ? el.currentStyle[attribute] : document.defaultView.getComputedStyle(el, null)[attribute];
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

        if (!str) return {};

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

    o.$api = u;

})(window);

Function.prototype.fExtends = function (base) {
    for (var p in base.prototype) this.prototype[p] = base.prototype[p];
};

(function (o) {

    function Base() { }

    o.Base = Base;

})(window);

/**
 * PageBase 类继承于 Base 基类 
 */
(function (o) {

    function PageBase() { }

    PageBase.prototype = {
        id: '',
        name: '',
        dom: null,
        tbl: null,
        btns: null
    }

    PageBase.prototype.initbase = function () {
        // 若Id 为空
        if (!$api.trim(this.id)) {
            common.tip('PageBase 类, initbase 函数初始化失败, Id 为空 !', 5 * 1000);
            return false;
        }

        this.dom = $api.dom('#{0}'.format(this.id));

        // 若不存在 dom 
        if (!this.dom) {
            common.tip('PageBase 类, initbase 函数初始化失败, Id:{0} 不存在!'.format(this.id), 5 * 1000);
            return false;
        }

        if (o.gps[this.id]){

            this.tbl = o.gps[this.id]['table'] || {
                dom: $api.dom(this.dom, '.tbl'),
                select: { row: null }
            };

            this.btns = o.gps[this.id]['buttons'];
        } else {
            this.tbl = {};
            this.btns = {};
        }

        return true;
    }

    o.PageBase = PageBase;

})(window);

/*
    function getType(o)
    {
        var _t;
        return ((_t = typeof(o)) == "object" ? o==null && "null" || Object.prototype.toString.call(o).slice(8,-1):_t).toLowerCase();
    }
    function extend(destination,source)
    {
        for(var p in source)
        {
            if(getType(source[p])=="array"||getType(source[p])=="object")
            {
                destination[p]=getType(source[p])=="array"?[]:{};
                arguments.callee(destination[p],source[p]);
            }
            else
            {
                destination[p]=source[p];
            }
        }
    }
    var test={a:"ss",b:"dd",c:{d:"css",e:"cdd"}};
    var test1={};
    extend(test1,test);
    test1.c.d="change"; //改变test1的c属性对象的d属性 
    alert(test.c.d);
 */