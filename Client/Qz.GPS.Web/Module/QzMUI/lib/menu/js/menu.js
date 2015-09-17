(function (o) {

    "use strict";

    var defaults = {
        id: '.menu',
        pid: '.page-container',
        mask: true,
        position: 'menu-left',
        trigger: '',
        initCallback: function () { },
        closeCallback: function () { },
        openCallback: function () { },
        toggleCallback: function () { }
    },
        timer = null;

    // 侧滑菜单 
    function Menu(opt) {
        this.config = $api.extend({}, defaults, opt);
    }

    // 属性 
    Menu.prototype = {
        config: {},
        parent: {},
        mask: {},
        dom: {}
    };

    // 初始化 
    Menu.prototype.init = function (obj) {
        var menu = this;

        this.dom = $api.dom(this.config.id);
        this.parent = $api.dom(this.config.pid);
        if (this.config.mask) {
            $api.after(this.dom, '<div class="menu-mask animated fadeIn" data-menu></div>');
            this.mask = $api.dom('.menu-mask[data-menu]');
            $api.hide(this.mask);
            $api.addEvt(this.mask, 'click', function () {
                menu.close();
            });
        }

        $api.addCls(this.dom, this.config.position);

        if (obj && $api.isArray(obj.menu)) {
            var html = [];

            obj.menu.forEach(function (_menu) {
                console.log(_menu);
                if (_menu.hasOwnProperty('split')) {
                    if(_menu['split']){
                        html.push('<li class="split"></li>'.format(_menu));
                    }
                    return;
                }
                if (_menu.hasOwnProperty('active')) {
                    if (_menu['active']){
                        html.push('<li class="active"><a href="{url}"><i class="icon icon-{icon}"></i>{name}</a></li>'.format(_menu));
                    }
                    return;
                }
                html.push('<li><a href="{url}"><i class="icon icon-{icon}"></i>{name}</a></li>'.format(_menu));
            });

            $api.html(this.dom, '<ul>{0}</ul>'.format(html.join('')));
        }

        if (this.config.trigger) {

            $api.addEvt(this.config.trigger, 'click', function () {

                menu.toggle();
            });
        }

        $api.trigger(this.config.initCallback);
    };
    // 打开 
    Menu.prototype.open = function () {

        $api.addCls(this.dom, 'menu-open');
        if (this.config.mask) {
            $api.show(this.mask);
            $api.addCls(this.mask, 'menu-mask-open');
        }

        $api.trigger(this.config.openCallback);
    };
    // 关闭 
    Menu.prototype.close = function () {
        var menu = this;

        $api.rmCls(this.dom, 'menu-open');
        if (this.config.mask) {
            $api.rmCls(this.mask, 'menu-mask-open');
        }

        timer = setTimeout(function () {

            $api.hide(menu.mask);
            clearTimeout(timer);
            $api.trigger(menu.config.closeCallback, '1');
        }, 400);

        $api.trigger(this.config.closeCallback, '0');
    };
    // 切换，如果打开状态则关闭，如果关闭状态则打开  
    Menu.prototype.toggle = function () {
        if (this.status() === 'open') {
            this.close();
        } else {
            this.open();
        }

        $api.trigger(this.config.toggleCallback);
    };
    // 菜单状态 
    Menu.prototype.status = function () {
        return $api.hasCls(this.dom, 'menu-open') ? 'open' : 'close';
    }

    o.Menu = Menu;

})(window);