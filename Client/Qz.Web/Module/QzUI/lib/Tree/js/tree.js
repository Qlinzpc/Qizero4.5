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
        title: '',
        show: false,
        parent: '',
        ajax: {
            url: '/Common/Build',
            data: ''
        },
        select: {
            max: {
                cnt: 5,
                msg: "'当前选中 ' + tree.key.length + '个,已超过最大值 ' + tree.select.max.cnt + '个'"
            }
        },
        callback: function () { },
        initCallback: function () { },
        createCallback: function () { },
        shrinkCallback: function () { },
        spreadCallback: function () { },
        checkCallback: function () { },
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
        key: [],
        value: []
    };

    // 初始化 
    Tree.prototype.init = function () {

        this.config.parent = this.config.parent || $api.dom('body');

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
            show: this.config.show,
            mask: false,
            icon: 'fa-tree',
            close:false,
            parent: this.config.id,
            callback: this.config.callback
        });

        var html = [],
            data = {},
            treeType = 'child',
            fname;

        html.push('<li data-id="0" class="active" data-sub-menu="0" onclick=""><i class="fa fa-gps-home"></i><span class="title">首页</span></li>');
        html.push('<li data-id="1" data-sub-menu="1" data-open="0" onclick=""><i class="fa fa-manage"></i><span class="title">系统管理</span><i class="fa fa-open"></i><ul style="" class="dn"> <li data-id="1" onclick="" data-sub-menu="0"><i class="fa fa-sys-app"></i><span class="title">系统应用</span></li> <li data-id="1" class="" onclick=""><i class="fa fa-sys-log"></i><span class="title">系统日志</span></li> <li data-id="1" class="" onclick=""><i class="fa fa-data-code"></i><span class="title">数据字典</span></li> <li data-id="1" class="" onclick=""><i class="fa fa-db-manage"></i><span class="title">数据库管理</span></li></ul></li>');
        html.push('<li data-id="2" data-sub-menu="1" data-open="0" onclick=""><i class="fa fa-sys-setting"></i><span class="title">系统设置</span><i class="fa fa-open"></i><ul style=" " class="dn"> <li data-id="1" onclick="" data-sub-menu="0"><i class="fa fa-module-manage"></i><span class="title">模块管理</span></li> <li data-id="1" class="" onclick=""><i class="fa fa-company-manage"></i><span class="title">公司管理</span></li> <li data-id="1" class="" onclick="" data-sub-menu="0"><i class="fa fa-dept-manage"></i><span class="title">部门管理</span></li> <li data-id="1" class="" onclick=""><i class="fa fa-role-manage"></i><span class="title">角色管理</span></li><li data-id="1" class="" onclick=""><i class="fa fa-user-manage"></i><span class="title">用户管理</span></li></ul></li>');

        //for (var i = 0; i < rs.Obj.Data.length; i++) {
        //    data = rs.Obj.Data[i];
        //    if (type == 'build') {
        //        if (data.ParentId == '0') {
        //            treeType = 'root';
        //            fname = data.Name;
        //        } else {
        //            fname = $api.data(obj, 'fname') + data.Name;
        //        }
        //        html.push('<li data-id="' + data.Id + '" data-code="' + data.Code + '" data-parentId="' + data.ParentId + '" data-fname="' + fname + '" data-type="' + treeType + '" onclick="tree.check(this)"><i class="fa fa-spread" onclick="tree.spread(this)"></i><span class="txt">' + data.Name + '</span></li>');
        //    } else
        //        html.push('<li data-id="' + data.Key + '" onclick="box.itemClick(this);">' + data.Value + '</li>');
        //}

        this.box.body('<ul>' + html.join('') + '</ul>');

        $("li", this.dom).bind('click', function () {

            // 阻止事件冒泡
            var e = event || window.event; if (e && e.stopPropagation) { e.stopPropagation(); } else { e.cancelBubble = true; }

            var _this = $(this),
                subMenu =  _this.data('sub-menu');

            if (subMenu && subMenu != '0') {

                var _open = $('li[data-open=1]');
                if (_open.length >= 1 && _this.attr('data-open') == '0') {
                    $('ul:eq(0)', _open).slideUp('500');
                    $('.fa-open', _open).css({ transform: 'rotate(0deg)', transition: '0.5s' });
                    _open.attr('data-open', '0');
                }

                if (_this.attr('data-open') != '0') {
                    $('ul:eq(0)', _this).slideUp('500');
                    $('.fa-open', _this).css({ transform: 'rotate(0deg)', transition: '0.5s' });
                    _this.attr('data-open', '0');
                } else {
                    $('ul:eq(0)', _this).slideDown('500');
                    $('.fa-open', _this).css({ transform: 'rotate(90deg)', transition: '0.5s' });
                    _this.attr('data-open', '1');
                }
                
                return;
            }

            $('.active', this.dom).removeClass('active');
            _this.addClass('active');

        });

    };

    // 收缩 
    Tree.prototype.shrink = function () { };

    // 展开 
    Tree.prototype.spread = function () { };

    // 选中 不选中 
    Tree.prototype.check = function () { };

    // Message 提示信息 
    Tree.prototype.msg = function () { };

    o.Tree = Tree;

})(window, jQuery);

var tree = new function () {
    // 选中数据 key 数组
    this.key = [];
    // 选中数据 value 数组
    this.value = [];
    // ajax 请求参数
    this.ajax = { url: '/Common/Build', data: '' };
    // 选中最大数量 
    this.select = { max: { cnt: 5, msg: "'当前选中 ' + tree.key.length + '个,已超过最大值 ' + tree.select.max.cnt + '个'" } };
    // 扩展 callback
    this.spreadCallback = function (obj) {
        var id = $api.data(obj, 'id');
        var check = $api.data(obj, 'check') == '0';
        tree.ajax.data = "estateId=" + $api.val('estateId') + "&parentId=" + id + "&userId=" + req.userId;
        // 异步绑定 下级栋阁
        search.bind.data(tree.ajax.url, tree.ajax.data, 'build', obj, true, check);
    };

    // 收缩
    this.shrink = function ($this) {
        // 阻止事件冒泡
        var e = event || window.event; if (e && e.stopPropagation) { e.stopPropagation(); } else { e.cancelBubble = true; }

        $api.addRmCls($this, 'fa-shrink', 'fa-spread');
        $api.addRmAttr($this, 'onclick', 'tree.spread(this)');

        var $p = $this.parentNode;
        var cnt = $api.data($p, 'children');

        if (cnt >= 1) $api.hide($p.children[$p.children.length - 1]);
    };
    // 展开
    this.spread = function ($this) {
        // 阻止事件冒泡
        var e = event || window.event; if (e && e.stopPropagation) { e.stopPropagation(); } else { e.cancelBubble = true; }

        $api.addRmCls($this, 'fa-spread', 'fa-shrink');
        $api.addRmAttr($this, 'onclick', 'tree.shrink(this)');

        var $p = $this.parentNode;
        var cnt = $api.data($p, 'children');
        if (cnt >= 1) {
            $api.show($p.children[$p.children.length - 1]);
            return;
        }
        tree.spreadCallback($p);
    };

    // 选中 不选中
    this.check = function ($this) {
        // 阻止事件冒泡
        var e = event || window.event; if (e && e.stopPropagation) { e.stopPropagation(); } else { e.cancelBubble = true; }

        // 是否已经选中
        var check = $api.data($this, 'check') == '0';

        // 递归同步子元素
        function syncChildren(chArr) {
            for (var i = 0; i < chArr.children.length ; i++) {
                // 子元素 tree.key 不操作数据
                _check(chArr.children[i], check, 1);
                // chArr.children[i] 是否有子元素
                cnt = $api.data(chArr.children[i], 'children') || 0;
                if (cnt >= 1) {
                    chSubArr = chArr.children[i].children;
                    syncChildren(chSubArr[chSubArr.length - 1]);
                }
            }
        }
        // 子元素个数
        var cnt = $api.data($this, 'children') || 0;
        if (cnt >= 1) {
            // 获取所有的子元素
            var chArr = $this.children[$this.children.length - 1], chSubArr;
            // 递归同步子元素 将所有的子元素 同父元素操作一致
            syncChildren(chArr);

            // 删除 tree.key 所有子元素
            tree.key.removeLike($api.data($this, 'code'));
            // 删除 tree.value 所有子元素
            tree.value.removeLike($api.data($this, 'fname'));
        }
        // 元素控制
        _check($this, check);

        // 获取元素 类型
        var type = $api.data($this, 'type') || '';
        // 是根元素
        if (type == 'root') { return false; }
        // 获得当前的父元素
        var $p = $this.parentNode.parentNode, levelArr;

        // 递归同步父元素不选中
        function syncUnChkParent($p, check) {
            _check($p, check);
            // 获得当前操作元素的同级元素
            levelArr = $p.children[$p.children.length - 1].children;
            for (var i = 0; i < levelArr.length; i++) {
                if ($api.hasCls(levelArr[i], 'select')) {
                    // 所有选中的同级元素 , 添加到 tree.key 中
                    tree.key.remove($api.data(levelArr[i], 'code'));
                    tree.key.push($api.data(levelArr[i], 'code'));
                    // 所有选中的同级元素 , 添加到 tree.value 中
                    tree.value.remove($api.data(levelArr[i], 'fname'));
                    tree.value.push($api.data(levelArr[i], 'fname'));
                }
            }
            type = $api.data($p, 'type') || '';
            // 不是根元素
            if (type != 'root') {
                // 递归同步父元素
                syncUnChkParent($p.parentNode.parentNode, check);
            }
        }
        // 如果当前状态为选中 ， 即更改元素为不选中状态 ， 更改父元素为不选中状态
        if (check) {
            syncUnChkParent($p, check);
            return false;
        }

        // 获取当前元素所有的子元素
        var all = $this.parentNode.children, allCheck = true, subAll;
        // 递归同步父元素
        syncParent($p, check, all);
        // 递归同步父元素选中
        function syncParent($p, check, all) {
            allCheck = true;
            // 判断当前子元素是否都为选中状态
            for (var i = 0; i < all.length; i++) {
                if ($api.hasCls(all[i], 'unselect') || all[i].className == '') {
                    allCheck = false;
                    break;
                }
            }
            // 1. 如果当前子元素 都为选中状态
            // 2. 更改父元素选中状态
            // 3. tree.key 选中数据数组, 移除所有子元素, 保留父元素
            if (allCheck) {
                _check($p, check);
                // tree.key 移除所有子元素
                for (var i = 0; i < all.length; i++) {
                    // 去掉不选中 code
                    tree.key.remove($api.data(all[i], 'code'));
                    tree.value.remove($api.data(all[i], 'fname'));
                }
                type = $api.data($p, 'type') || '';
                // 不是根元素
                if (type != 'root') {
                    subAll = $p.parentNode.parentNode.children;
                    // 递归同步父元素
                    syncParent($p.parentNode.parentNode, check, subAll[subAll.length - 1].children);
                }
            }
        }

        return false;
    };
    // 选中 不选中 元素控制
    function _check($this, isCheck, isOpr) {
        isCheck = isCheck || false;
        isOpr = isOpr || 0;
        if (isCheck) {
            // 当前状态为选中 ， 即更改元素为不选中状态
            $api.rmChild($this, '.fa-check');
            $api.data($this, 'check', '1');
            $api.css($this.children[0], 'color:#149b70;');
            $api.addRmCls($this, 'select', 'unselect');
            // 去掉不选中 code
            if (isOpr == 0) {
                tree.key.remove($api.data($this, 'code'));
                tree.value.remove($api.data($this, 'fname'));
            }
        } else {
            // 当前状态为不选中 ， 即更改元素为选中状态
            $api.addRmCls($this, 'unselect', 'select');
            $api.rmChild($this, '.fa-uncheck');
            $api.data($this, 'check', '0');
            $api.after($api.dom($this, '.txt'), '<i class="fa fa-check"></i>');
            $api.css($this.children[0], 'color:#fff;');
            // 添加选中 code
            if (isOpr == 0) {
                tree.key.push($api.data($this, 'code'));
                tree.value.push($api.data($this, 'fname'));
            }
        }
    };
};
