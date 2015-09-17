(function (o) {

    "use strict";

    var defaults = {
        container: '.page-container',
        header: '.page-header',
        context: '.page-context',
        footer: '.page-footer'
    };

    // 布局 
    function Layout(opt) {
        this.config = $api.extend({}, defaults, opt);
    }
    // 属性 
    Layout.prototype = {
        config: {},
        body: {},
        container: {},
        header: {},
        footer: {},
        context: {}
    };
    // 初始化 
    Layout.prototype.init = function () {

        this.container = $api.dom(this.config.container);
        this.header = $api.dom(this.config.header);
        this.context = $api.dom(this.config.context);
        this.footer = $api.dom(this.config.footer);
        this.body = $api.dom('body');

        $api.css(this.context, 'height: {0}px'.format(this.body.clientHeight - (this.header ? this.header.clientHeight : 0) - (this.footer ? this.footer.clientHeight : 0)));
    };

    o.Layout = Layout;

})(window);