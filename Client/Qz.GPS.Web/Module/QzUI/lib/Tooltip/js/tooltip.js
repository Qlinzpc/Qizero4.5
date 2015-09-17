/**
 * QzUI.js.lib.Tooltip.js Tooltip 
 * 
 * @Author ZPC 
 * @Date 2015-7-22 11:04:08 
 * @Version 1.0 
 * 
 */
(function (o) {

    'use strict';

    var defaults = {
        parent: 'body'

    };

    function Tooltip(opts) {

        this.config = $api.extend({}, defaults, opts);

    }

    Tooltip.prototype = {
        dom: null,
        config: null,
        parent: null,
        all: []
    }

    Tooltip.prototype.init = function (p) {

        var _this = this,
            pos;

        this.config.parent = p || this.config.parent;

        this.all = this.getTips();

        
        //$api.addEvt(this.all, 'mouseenter', function () {
        //    console.log(this);
        //});

        $(this.all).hover(function () {
            $api.append(_this.config.parent, '<div class="tooltip"></div>');
            _this.dom = $api.dom('.tooltip');
            if (!_this.dom) {
                _this.config.parent = 'body';
                $api.append(_this.config.parent, '<div class="tooltip"></div>');
                _this.dom = $api.dom('.tooltip');
            }

            pos = $api.offset(this);

            $api.css(_this.dom, 'top: {0}px;left: {1}px;'.format(pos.t + pos.h , pos.l));
            $api.html(_this.dom, $api.attr(this, 'tooltip'));
            $(_this.dom).show().animate({ 'top': '{0}px'.format(pos.t + pos.h + 5), 'left': '{0}px'.format(pos.l + 5) });

        }, function () {
            $(_this.dom).remove();

        });

    };

    Tooltip.prototype.show = function () {

    };

    Tooltip.prototype.getTips = function () {

        var alls = $api.domAll(this.config.parent, '*'),
            tips = [];

        alls.forEach(function (_) {
            if (_.attributes.hasOwnProperty('tooltip') && _.attributes.tooltip)
                tips.push(_);
        });

        return tips;
    };

    o.Tooltip = Tooltip;
    o.tooltip = new Tooltip({});

})(window);

