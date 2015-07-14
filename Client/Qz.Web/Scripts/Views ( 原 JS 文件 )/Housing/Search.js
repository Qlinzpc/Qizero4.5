
/**
 * 表单 
 */
var form = new function () {
    // 初始化 
    this.init = function () {
        var aClose = $api.domAll('.txt-flag'),
            len = aClose.length,
            target = {},
            left = 0,
            top = 0,
            i = 0;

        for (i = 0; i < len; i++) {
            target = $api.byId($api.data(aClose[i], 'target'));
            $api.addCls(target.parentNode, 'pos-r');
            left = target.offsetLeft + target.clientWidth - aClose[i].clientWidth - 5;
            top = target.offsetTop + target.clientHeight - aClose[i].clientHeight - aClose[i].clientHeight / 2 + 1;
            $api.css(aClose[i], 'left:' + left + 'px;top:' + top + 'px;width:' + aClose[i].clientWidth + 'px');
        }
    };
    // 验证
    this.validate = function (id) {
        var fArr = $api.byId(id),
            _this;

        for (var i = 0; i < fArr.length; i++) {
            _this = fArr[i];
            if (_this.localName == 'textarea' || (_this.localName == 'input' && _this.type != 'button' && _this.type != 'submit' && _this.type != 'reset')) {
                if (!_this.validity.valid) {
                    if ("float" == $api.data(_this, 'validate-type')) {
                        if (!/^[0-9]+([.]{1}[0-9]+){0,1}$/.test(_this.value)) {
                            common.tip('只能为整数或小数');
                            _this.focus();
                            return false;
                        }
                        continue;
                    }
                    common.tip(_this.validationMessage);
                    _this.focus();
                    return false;
                }
            }
        }

        return true;
    };
    // 条件
    this.condition = function (id) {
        var json = [],
            fArr = $api.byId(id),
            _this;

        for (var i = 0; i < fArr.length; i++) {
            _this = fArr[i];
            if (_this.localName == 'textarea' || _this.localName == 'select' || (_this.localName == 'input' && _this.type != 'button' && _this.type != 'submit' && _this.type != 'reset')) {
                json.push('"' + _this.name + '":"' + $api.val(_this) + '"');
            }
        }

        json = json.join(',');
        return eval('({' + json + '})');
    };
};

/**
 * box 操作
 */
var box = new function () {

    this.target = '',
    this.key = '',
    this.value = '',
    this.arr = [],
    this.title = '',

    this.opts = { show: false, oper: '<span class="btn-ok" onclick="box.ok()">确 定</span>' };

    // 创建 box 
    this.create = function () {
        var that;
        typeof box.target === 'string' && (that = $api.byId(box.target) || $api.dom(box.target));
        if (!that) {
            $api.append($api.dom('body'), '<div class="ui-select" id="' + box.target + '"> <div class="ui-header"> <span class="ui-title"> ' + (box.opts.show ? box.opts.oper : '<i class="fa fa-win"></i>' + box.title) + ' </span> <span class="ui-close" onclick="box.close()">×</span> </div> <div class="ui-content"></div> </div>');
            that = $api.byId(box.target) || $api.dom(box.target);
        }

        return that;
    };
    // 数据项 点击事件
    this.itemClick = function ($this) {
        if (box.arr.length <= 0) return;
        $api.removeCls(box.arr, 'select');
        $api.text(box.arr, '', 'this');
        $api.addCls($this, 'select');
        var txt = $api.trim($api.text($this));
        $api.html($this, '<i class="fa fa-select"></i>' + txt);

        // 获得当前操作的数据
        search.obj.key = $api.data($this, 'id');
        search.obj.value = txt;

        setTimeout(function () { box.ok(); }, 500);
    };

    // 清空 
    this.clear = function () { };
    // 确定 
    this.ok = function () {
        if (search.obj.value == '' && tree.key.length == 0) {
            return;
        }
        if (search.obj.value == '') {
            if (tree.key.length > tree.select.max.cnt) {
                common.tip(eval(tree.select.max.msg));
                return;
            }
            search.obj = { key: tree.key.join(','), value: tree.value.join(',') };
            tree.key = [];
            tree.value = [];
        }

        $api.remove($api.dom('.l-mask'));
        $api.hide(box.target);
        $api.val(box.key, search.obj.key);
        $api.val(box.value, search.obj.value);

        search.obj = { key: '', value: '' };
    };

    // 打开
    this.open = function () {
        setTimeout(function () {
            // 遮蔽层
            $api.remove($api.dom('.l-mask'));
            $api.append($api.dom('body'), '<div class="l-mask" style="display:block;"></div>');
        }, 100);

        $api.show(box.target);
    };
    // 关闭 
    this.close = function () {
        $api.remove($api.dom('.l-mask'));
        $api.hide(box.target);
    };
};

/**
 * 树
 */
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

/**
 * 盘源搜索 
 */
var search = new function () {
    this.obj = { key: '', value: '' };
    this.build = { parentId: 0 };

    this.select = {
        usage: {},
        city: {},
        area: {},
        trade: {}
    };

    // 加载 
    this.load = function () {
        // 清除 
        search.clear();
        // 绑定事件 
        search.bind.event();
        // 初始化 表单 
        form.init();

        // 开始初始化 下拉框 
        // 用途  
        search.select.usage = new Select({
            title: '用途列表',
            show: false,
            isdft: false,
            target: 'usage-list',
            key: 'usage',
            value: 'selectUsage',
            url: '/Common/Usage',
            data: 'id=' + req.userId,
            change: function () {
                var _this = this;

                // console.log(_this);
                search.clear();
            },
            callback: function () {
                // 点击重置后 BUG 
                $api.attr('selectUsage', 'value', $api.val('selectUsage'));
            }
        });
        
        // 交易类型 
        search.select.trade = new Select({
            title: '交易类型',
            show: false,
            target: 'trade-list',
            key: 'tradeType',
            value: 'selectTrade',
            html: '<li data-id="0" class="select"><i class="fa fa-select"></i>全部</li><li data-id="1">出售</li><li data-id="2">出租</li>',
            change: function () {
                var _this = this;

                // console.log(_this);

                var val = $api.val('tradeType');
                if (val == 0 || val == 1) {
                    $api.text('unit', '万元');
                    return;
                }
                $api.text('unit', '元/月');
            }
        });
        // 结束初始化 下拉框 
    };

    // 清除 楼盘, 栋阁 值
    this.clear = function (type) {
        switch (type) {
            case "build":
                $api.val('build', '');
                $api.val('code', '');
                break;
            case "all":
            default:
                $api.val('estate', '');
                $api.val('estateId', '0');
                $api.val('build', '');
                $api.val('code', '');
                break;
        }
    }

    // 绑定
    this.bind = {
        // 事件
        event: function () {
            $api.addEvt(window, 'resize', function () {
                form.init();
            });
            // 高级 基本查询切换
            $api.addEvt('btn-adv', 'click', function () {
                var txt = $api.text(this);
                var arr = $api.domAll('.advanced');
                var i = arr.length;
                if (txt == '基本查询>>') {
                    $api.text(this, '高级查询>>');
                } else {
                    $api.text(this, '基本查询>>');
                }
                var t = setInterval(function () {
                    $api.toggleCls(arr[--i], 'dn');
                    if (i <= 0) {
                        clearInterval(t);
                        return;
                    }
                }, 100);

            });
            // 楼盘名称失焦
            $api.addEvt('estate', 'blur', function () {
                if ($api.trim($api.val(this)) == "") search.clear();
            });
            // 栋阁名称失焦
            $api.addEvt('build', 'blur', function () {
                if ($api.trim($api.val(this)) == "") search.clear('build');
            });
            // 栋阁名称点击 
            $api.addEvt('build', 'click', function () {
                $api.dom(this.parentNode, '.fa-query').click();
            });
            // 查询
            $api.addEvt('query', 'click', function () {
                if (form.validate('search')) {
                    $api.setStorage('hSearch', form.condition('search'));

                    window.location.href = '/Housing/List?userId=' + req.userId;
                }
            });
            // 重置
            $api.addEvt('reset', 'click', function () { $api.byId('search').reset(); });
            // 楼盘 栋阁查询
            $api.addEvt($api.domAll('.fa-query'), 'click', function () {
                // 根据 url 加载列表数据
                var url = $api.data(this, 'url');
                var type = $api.data(this, 'type');
                var data = "";
                switch (type) {
                    case "estate":
                        data = "userId=" + req.userId + "&usageId=" + $api.val('usage') + "&val=" + $api.val('estate');
                        box.title = "楼盘列表";
                        break;
                    case "build":
                        data = "userId=" + req.userId + "&estateId=" + $api.val('estateId') + "&parentId=" + search.build.parentId;
                        if ($api.val('estateId') == '' || $api.val('estateId') == '0') {
                            common.tip('请先选择楼盘');
                            return;
                        }
                        box.title = "";
                        box.opts.show = true;
                        break;
                }

                // 获取当前操作 目标元素标识
                box.target = $api.data(this, 'target');
                box.key = $api.data(this, 'key');
                box.value = $api.data(this, 'value');

                // 创建 box 
                var obj = box.create();

                search.bind.data(url, data, type, $api.dom(obj, '.ui-content'));
            });

            // 用途  
            $api.addEvt('selectUsage', 'click', function (e) {
                search.select.usage.show();
            });
            // 城区  
            $api.addEvt('selectCity', 'click', function () {
                if (search.select.city.show) {
                    search.select.city.show();
                    return;
                }
                // 城区 
                search.select.city = new Select({
                    title: '城区列表',
                    target: 'city-list',
                    select: '0',
                    key: 'cityId',
                    value: 'selectCity',
                    url: '/Common/City',
                    data: 'userId=' + req.userId,
                    change: function () {
                        var _this = this;

                        // console.log(_this);
                        if (search.select.area.clear) { search.select.area.clear(); }

                        $api.val('areaId', 0);
                        $api.val('selectArea', '全部');
                    }
                });
            });
            // 片区 
            $api.addEvt('selectArea', 'click', function () {
                var cityId = $api.val('cityId');
                if (cityId == 0) {
                    common.tip('请先选择城区 !');
                    return;
                }

                if (search.select.area.show && search.select.area.box.arr.length > 0) {
                    search.select.area.show();
                    return;
                }
                // 片区 
                search.select.area = new Select({
                    title: '片区列表',
                    target: 'area-list',
                    select: '0',
                    key: 'areaId',
                    value: 'selectArea',
                    url: '/Common/Area',
                    data: 'userId=' + req.userId + '&cityId=' + cityId,
                    change: function () {
                        //var _this = this;

                        //console.log(_this);
                    }
                });
            });
            // 交易类型 
            $api.addEvt('selectTrade', 'click', function () {
                search.select.trade.show();
            });

        },

        // 绑定 楼盘, 栋阁 数据
        data: function (url, data, type, obj, isChildTree, check) {
            // ajax 请求获取数据
            common.ajax.post({
                url: url,
                data: data,
                callback: function (rs) {
                    if (rs.Status != 0) {
                        common.tip(rs.Message);
                        return;
                    }

                    var html = [], data = {}, treeType = 'child', fname;
                    for (var i = 0; i < rs.Obj.Data.length; i++) {
                        data = rs.Obj.Data[i];
                        if (type == 'build') {
                            if (data.ParentId == '0') {
                                treeType = 'root';
                                fname = data.Name;
                            } else {
                                fname = $api.data(obj, 'fname') + data.Name;
                            }
                            html.push('<li data-id="' + data.Id + '" data-code="' + data.Code + '" data-parentId="' + data.ParentId + '" data-fname="' + fname + '" data-type="' + treeType + '" onclick="tree.check(this)"><i class="fa fa-spread" onclick="tree.spread(this)"></i><span class="txt">' + data.Name + '</span></li>');
                        } else
                            html.push('<li data-id="' + data.Key + '" onclick="box.itemClick(this);">' + data.Value + '</li>');
                    }

                    // 如果是子栋阁加载
                    if (isChildTree) {
                        $api.append(obj, '<ul class="p-d-l-5">' + html.join('') + '</ul>');
                        $api.data(obj, 'children', rs.Obj.Data.length);
                        if (check) {
                            $api.data(obj, 'check', '1');
                            tree.check(obj);
                            $api.data(obj, 'check', '0');
                        }
                    } else {
                        $api.html(obj, '<ul class="scroll container">' + html.join('') + '</ul>');
                    }
                    // 显示查询列表
                    box.open();
                    // 遮蔽层
                    $api.remove($api.dom('.l-mask'));
                    $api.append($api.dom('body'), '<div class="l-mask" style="display:block;"></div>');

                    var arr = $api.domAll($api.byId(box.target), 'li');
                    if (arr.length == 0) {
                        box.arr = [];
                        return;
                    }
                    box.arr = arr;
                }
            });
        }
    }

};

/**
 * 加载 回调函数 
 */
common.loadCallback = function () {
    // 加载 
    search.load();
};
