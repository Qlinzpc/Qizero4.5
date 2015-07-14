/**
 * QzUI.js.lib.tree.js 
 * @Author ZPC 
 * @Date 2015-4-16 11:23:52 
 * @Version 1.0 
 * 
 */
(function (o, $) {

    "use strict";

    var DEFAULTS = {
        id: '',
        show: false,
        data: null,
        extend: '',
        title: '',
        parent: '',
        mutil: false,
        callback: function () { },
        initCallback: function () { },
        createCallback: function () { },
        shrinkCallback: function () { },
        spreadCallback: function () { },
        checkCallback: function () { },
        selectCallback: function(){ },
        xCallback: function () { }
    };

    function Tree(opt) {
        this.config = $.extend({}, DEFAULTS, opt);

        this.init();

        this.create();
    }

    Tree.prototype = {
        dom: '',
        config: {},
        box: {},
        parent: null,
        arr: [],
        checks: [],
        allchecks: [],
        key: [],
        value: []
    };

    // 初始化 
    Tree.prototype.init = function () {

        this.arr = [];
        this.checks = [];
        this.allchecks = [];

        if ($api.isElement(this.config.parent)) this.parent = this.config.parent;

        this.parent = this.parent || (this.config.parent || $api.dom('body'));

        $api.trigger(this.config.initCallback, null, this);
    };

    // 创建 
    Tree.prototype.create = function () {

        var _tree = this,
            _target;

        if (typeof this.config.id === 'string') {
            _target = $api.byId(this.config.id) || $api.dom(this.config.id);
        }

        if (!_target) {
            $api.append(this.config.parent, '<div class="tree" id="' + this.config.id + '"></div>');

            _target = $api.byId(this.config.id) || $api.dom(this.config.id);
        }

        this.dom = _target;

        this.box = new Box({
            id: 'box-' + this.config.id,
            title: this.config.title,
            header: !!this.config.title,
            show: this.config.show,
            mask: false,
            icon: '',
            close:false,
            parent: this.config.id,
            callback: this.config.callback
        });

        this.bind(this.box.body(), this.config.data);

        $api.trigger(this.config.createCallback, null , this);

    };

    // 绑定 
    Tree.prototype.bind = function (target, data) {
        var html = [],
            _lis = null,
            _tree = this;

        if (data && $api.isArray(data)) {
            data.forEach(function (_) {
                _.Type =_.Type || '';

                if (_.ParentId == -1) {
                    _.Id = 0;
                    _.Type = 'root';
                }

                switch (_tree.config.extend) {
                    case 'checkbox':
                        _.Extend = '<i class="icon icon-cbox-un-check" data-type="checkbox" data-check="0"></i>';
                        // _.Extend = '<i class="icon icon-cbox-check" data-type="checkbox"></i>';
                        break;
                    default:
                        _.Extend = _tree.config.extend;
                        break;
                }

                html.push('<li data-id="{Id}" data-code="{Code}" data-parentId="{ParentId}" data-sub-count="{SubCount}" data-children="0" data-type="{Type}" data-bind="0"><i class="icon icon-spread" data-trigger data-open="0"></i>{Extend}<span class="txt">{Name}</span></li>'.format(_));

            });
        }

        $api.append(target, '<ul>' + html.join('') + '</ul>');

        _lis = $api.domAll(target, 'li');

        this.arr.push({ target: target, childrens: _lis });

        // 树节点点击事件 
        $(_lis).bind('click', function () {

            // 阻止事件冒泡
            $api.stop.event();

            $('.active', _tree.dom).removeClass('active');
            $api.addCls(this, 'active');

            $api.trigger(_tree.config.selectCallback, this, _tree);
        });

        // 树触发 点击事件
        $("li i[data-trigger]", target).bind('click', function () {

            // 阻止事件冒泡
            $api.stop.event();

            var _open = $api.data(this, 'open');

            if (_open === "1") {
                // 收缩 
                _tree.shrink(this);
                $api.data(this, 'open', '0');
            } else {
                // 展开 
                _tree.spread(this);
                $api.data(this, 'open', '1');
            }

        });

        // checkbox 点击事件
        $("li i[data-type='checkbox']", target).bind('click', function () {

            // 阻止事件冒泡
            $api.stop.event();

            _tree.check.call(this, _tree);

            $api.trigger(_tree.config.checkCallback, [$api.data(this, 'check') === '1', _tree], this);
        });

    };

    // 收缩 
    Tree.prototype.shrink = function (_) { 
        // 阻止事件冒泡
        $api.stop.event();

        $api.addRmCls(_, 'icon-shrink', 'icon-spread');

        var _parent = _.parentNode;
        var cnt = $api.data(_parent, 'children');
        if (cnt >= 1) {
            $('ul:eq(0)', _parent).hide();
        }

        $api.trigger(this.config.shrinkCallback, _parent, this);
    };

    // 展开 
    Tree.prototype.spread = function (_) {
        // 阻止事件冒泡
        $api.stop.event();

        $api.addRmCls(_, 'icon-spread', 'icon-shrink');

        var _parent = _.parentNode;
        var cnt = $api.data(_parent, 'children');
        if (cnt >= 1) {
            $('ul:eq(0)', _parent).show();
            return;
        }

        $api.trigger(this.config.spreadCallback, _parent, this);
    };

    // 选中 不选中 
    Tree.prototype.check = function (_tree) {

        var _this = this,

            _parent = _this.parentNode,

            _c = $api.data(_this, 'check'),

            // 是否已经选中
            isCheck = (_c === '0'),

            // 选中 不选中 
            _check = function (_this, _, _un) {
                if (_) {
                    // 选中  
                    $api.rmCls(_this, 'icon-cbox-check');
                    $api.addRmCls(_this, 'icon-cbox-un-check', 'icon-cbox-' + (!!_un ? 'square' : 'check'));
                    $api.data(_this, 'check', (!!_un ? '2' : '1'));

                    _tree.allchecks.remove(_this.parentNode);
                    _tree.allchecks.push(_this.parentNode);
                } else {
                    // 取消选中 
                    $api.rmCls(_this, 'icon-cbox-square');
                    $api.addRmCls(_this, 'icon-cbox-check', 'icon-cbox-un-check');
                    $api.data(_this, 'check', '0');

                    _tree.allchecks.remove(_this.parentNode);
                }

                if (!!_un || !_){
                    // 将取消选中的元素从 checks 中删除 
                    _tree.checks.remove(_this.parentNode);
                } else {
                    // 将选中的元素放入 checks 中 
                    _tree.checks.push(_this.parentNode);
                }
            },

            type = null,

            checkCnt = 0,

            all = [],

            subAll = [],

            // 递归同步子元素 
            syncChildren = function (chArr) {
                for (var i = 0; i < chArr.children.length ; i++) {
                    // 子元素 tree.key 不操作数据
                    _check($api.dom(chArr.children[i], "i[data-type='checkbox']"), isCheck);
                    // 移除父元素中 checks 子元素 
                    if (isCheck && parseInt($api.data(chArr.children[i], 'parentid')) > 0) _tree.checks.remove(chArr.children[i]);

                    // chArr.children[i] 是否有子元素
                    cnt = $api.data(chArr.children[i], 'children') || 0;
                    if (cnt >= 1) {
                        chSubArr = chArr.children[i].children;
                        syncChildren(chSubArr[chSubArr.length - 1]);
                    }
                }
            },

            // 递归同步父元素选中 
            syncParent = function ($p, isCheck, all) {
                checkCnt = 0;

                // 判断当前子元素是否都为选中状态
                for (var i = 0; i < all.length; i++) {
                    if ($api.data($api.dom(all[i], "i[data-type='checkbox']"), 'check') === '1') {
                        checkCnt++;
                    }
                }
                // 1. 如果当前子元素 都为选中状态
                // 2. 更改父元素选中状态
                // 3. tree.key 选中数据数组, 移除所有子元素, 保留父元素
                if (checkCnt === all.length) {
                    _check($api.dom($p, "i[data-type='checkbox']"), isCheck);

                    if (isCheck) {
                        // 如果父元素选中, 则移除父元素中 checks 子元素 
                        var pid = $api.data($p, 'id'), 
                            childrens = [];

                        if(parseInt(pid) > 0){
                            childrens = $api.domAll($p, "li[data-parentid='{0}']".format(pid));

                            childrens.forEach(function (c) {
                                _tree.checks.remove(c);
                            });
                        }
                    } 
                } else if (checkCnt === 0 && $api.domAll($p, "li i[data-check='2']").length <= 1) {
                    _check($api.dom($p, "i[data-type='checkbox']"), false);
                } else {
                    syncParentCheck($p, true);
                    return;
                }

                type = $api.data($p, 'type') || '';
                // 不是根元素
                if (type != 'root') {
                    subAll = $p.parentNode.parentNode.children;
                    // 递归同步父元素
                    syncParent($p.parentNode.parentNode, isCheck, subAll[subAll.length - 1].children);
                }
            },

            // 递归同步父元素选中 不选中 
            syncParentCheck = function (_this, isCheck) {

                _check($api.dom(_this, "i[data-type='checkbox']"), isCheck, true);

                type = $api.data(_this, 'type') || '';
                // 不是根元素 
                if (type != 'root') {
                    // 递归同步父元素
                    syncParentCheck(_this.parentNode.parentNode, isCheck);
                }
            };

        _check(_this, isCheck);

        // 子元素个数
        var cnt = $api.data(_parent, 'children') || 0;
        if (cnt >= 1) {
            // 获取所有的子元素
            var chArr = _parent.children[_parent.children.length - 1],
                chSubArr;

            // 递归同步子元素 将所有的子元素 同父元素操作一致
            syncChildren(chArr);
        }

        // 获取元素 类型
        type = $api.data(_parent, 'type') || '';
        // 是根元素
        if (type == 'root') { return false; }
        // 获得当前的父元素
        var $p = _parent.parentNode.parentNode;

        // 如果当前状态为选中 ， 即更改元素为不选中状态 ， 更改父元素为不选中状态
        if (isCheck) {
            syncParentCheck($p, isCheck);
        }

        // 获取当前元素所有的子元素 
        all = _parent.parentNode.children;

        // 递归同步父元素
        syncParent($p, isCheck, all);
        return false;
    };

    Tree.prototype.checkbox = function(){

    };

    // Message 提示信息 
    Tree.prototype.msg = function () { };

    o.Tree = Tree;

})(window, jQuery);
