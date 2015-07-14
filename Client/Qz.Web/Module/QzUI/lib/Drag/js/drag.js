/**
 * QzUI.js.lib.drag.js 
 * @Author ZPC 
 * @Date 2015-4-16 11:23:52 
 * @Version 1.0 
 * 
 */
(function (o, $) {
    "use strict";

    var DEFAULTS = {
        drag: '.drag',
        target: '.drag',
        parent: 'body',
        proxy: false,
        drawable: true,
        dragX: true,
        dragY: true,
        callback: function () { }
    },
        index;

    // Drag 拖拽 
    function Drag(opt) {

        this.config = $.extend({}, DEFAULTS, opt);

        this.init();

        var
            _div = null,
            _this = this,
            mousedown = function (e) {
                e = e || event;
                var pos = _this.target.position();

                _this.startX = e.pageX - pos.left;
                _this.startY = e.pageY - pos.top;

                if (!index) index = parseInt(_this.target.css('z-index'));

                _this.target.css('z-index', (++index));

                if (_this.config.proxy) {
                    _div = $('<div></div>');
                    var _t = _this.target.get(0);

                    _div.css({
                        width: _t.offsetWidth,
                        height: _t.offsetHeight,
                        top: _t.offsetTop + 'px',
                        left: _t.offsetLeft + 'px',
                        position: 'absolute',
                        cursor: 'move',
                        'z-index': ++index,
                        background: '#ededed',
                        filter: 'alpha(opacity=50)',
                        opacity: 0.5,
                        border: '1px solid #ccc'
                    }).addClass('proxy');

                    _this.parent.append(_div);
                }

                _this.doc.bind('mousemove', mousemove);
                _this.doc.bind('mouseup', mouseup);
            },
            mousemove = function (e) {
                e = e || event;

                var moveX = e.pageX - _this.startX,
                    moveY = e.pageY - _this.startY,
                    minX,
                    maxX,
                    minY,
                    maxY;

                if (_this.area) {

                    minX = _this.area[0];
                    maxX = _this.area[1] - _this.target.outerWidth();
                    minY = _this.area[2];
                    maxY = _this.area[3] - _this.target.outerHeight();

                    $api.css(_this.parent, 'border: 0;transition: 0.5s');

                    // X 轴比最小值还要小 
                    if (moveX < minX) {
                        log('X 轴比最小值还要小 ');

                        //$api.css(_this.parent, 'border-left: 1px solid red;transition: 0.5s');
                    }
                    // X 轴比最大值还要大 
                    if (moveX > maxX) {
                        log('X 轴比最大值还要大 ');

                        //$api.css(_this.parent, 'border-right: 1px solid red;transition: 0.5s');
                    }
                    // Y 轴比最小值还要小 
                    if (moveY < minY) {
                        log('Y 轴比最小值还要小 ');

                        //$api.css(_this.parent, 'border-top: 1px solid red;transition: 0.5s');
                    }
                    // Y 轴比最大值还要大 
                    if (moveY > maxY) {
                        log('Y 轴比最大值还要大 ');

                        //$api.css(_this.parent, 'border-bottom: 1px solid red;transition: 0.5s');
                    }

                    // 与最小值比取 max , 与最大值比取 min 
                    moveX = Math.min(Math.max(moveX, minX), maxX);
                    moveY = Math.min(Math.max(moveY, minY), maxY);
                }

                if (_this.config.drawable) {
                    _this.config.dragX && ((_div || _this.target).css('left', moveX));
                    _this.config.dragY && ((_div || _this.target).css('top', moveY));

                    if (typeof _this.config.callback === 'function') _this.config.callback.call(_this, { x: moveX, y: moveY });
                }
            },
            mouseup = function (e) {

                if (_div) {
                    var _d = _div.get(0);

                    (_this.target).css({
                        'left': _d.offsetLeft ,
                        'top': _d.offsetTop ,
                        'transition': '0.8s'
                    });

                    _div.remove();
                    _div = null;
                }

                _this.doc.unbind('mousemove', mousemove);
                _this.doc.unbind('mouseup', mouseup);
            };

        _this.dom.bind('mousedown', mousedown);

    }

    // Drag 拖拽属性 
    Drag.prototype = {
        config: {},
        dom: {},
        target: {},
        parent: {},
        area: [0, 0, 0, 0],
        doc: {},
        startX: 0,
        startY: 0
    };

    // 初始化 
    Drag.prototype.init = function () {

        this.dom = $(this.config.drag);
        this.target = $(this.config.target);
        this.parent = $(this.config.parent);
        this.doc = $(document);

        var maxX = this.parent.outerWidth(),
            maxY = this.parent.outerHeight();

        this.area = [0, maxX, 0, maxY];

        this.dom.css('cursor', 'move');
        this.target.css('position', 'absolute');
        this.parent.css('position', 'relative');

    };

    o.Drag = Drag;
    o.Index = index;

})(window, jQuery);
