
var asrc = ['http://isz.zlhome.com/upLoad/HouseInfos/2015/3/17/0e86533f-3942-43ed-b18a-30fe30d0090a/1.jpg',
        'http://isz.zlhome.com/upLoad/HouseInfos/12014/12/19/62604e45-2c47-49c4-9a72-bd8ec274cec5/1.jpg',
        'http://isz.zlhomecom/upLoad/HouseInfos/2015/1/4/a1aad13e-70c4-436d-babc-7aac98430bcc/1.jpg',
        'http://isz.zlhome.com/upLoad/HouseInfos/12015/3/18/4b84bcdb-6aa3-4b2e-9b28-f61645d3f0b2/1.jpg',
        'http://isz.zlhome.com/upLoad/HouseInfos/12015/3/21/8bdc72dd-d37b-487f-8529-e281a7baf2f2/1.jpg',
        'http://isz.zlhome.com/upLoad/HouseInfos/12014/11/26/25ec8906-5860-41b7-a326-ed349c46ab56/1.jpg',
        'http://isz.zlhome.com/upLoad/HouseInfos/12014/11/26/35967000-2e54-4856-b5e5-03cdb3256bda/1.jpg'];

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
            len = fArr.length,
            i = 0,
            _this;

        for (i = 0; i < len; i++) {
            _this = fArr[i];
            if (this.check(_this)) {
                if (!_this.validity.valid) {
                    if ("float" === $api.data(_this, 'validate-type')) {
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
    this.condition = function (id, fArr) {
        fArr = fArr || $api.byId(id);

        var len = fArr.length,
            i = 0,
            json = [],
            _this;

        for (i = 0; i < len; i++) {
            _this = fArr[i];
            if (this.check(_this)) {
                json.push('"' + _this.name + '":"' + $api.val(_this) + '"');
            }
        }

        json = json.join(',');
        return eval('({' + json + '})');
    };
    // 清空 
    this.clear = function (id, name) {
        var fArr = $api.domAll($api.byId(id), 'div[data-form-clear] input'),
            obj = form.clear.get(name),
            i = 0,
            len = fArr.length,
            _this;

        for (i = 0; i < len; i++) {
            _this = fArr[i];
            if (this.check(_this)) {
                $api.val(_this, obj[_this.name]);
            }
        }

        // 初始化 '交易类型'
        tools.filter.select.trade.init();
    };
    // 保存清空默认值 
    this.clear.set = function (id, name) {
        var fArr = $api.domAll($api.byId(id), 'div[data-form-clear] input');

        var condition = form.condition(id, fArr);

        $api.setStorage(name, condition);
    };
    // 获得清空默认值 
    this.clear.get = function (id) {
        return $api.getStorage(id);
    };

    // 检查是否满足要求 
    this.check = function (_this) {
        return (_this.localName === 'textarea' || _this.localName === 'select' || (_this.localName === 'input' && _this.type !== 'button' && _this.type !== 'submit' && _this.type !== 'reset'));
    };
};

/**
 * tools 操作 
 */
var tools = new function () {

    // 绑定 
    var bind = {
        // 事件 
        event: function () {
            // 点击 tools
            $api.addEvt(tools.arr, 'click', function (e) {
                tools.show.call(this);
            });
            // 点击遮蔽层 
            $api.addEvt('.l-sub-mask', 'click', function () {
                tools.hide();
                $api.hide(this);
            });
        }
    };

    // 初始化 
    this.init = function () {
        this.usage = this.base;
        this.usage.name = 'usage';
        // 用途 
        this.base.init.call(this.usage);

        this.sort = this.base;
        this.sort.name = 'sort';
        // 排序 
        this.base.init.call(this.sort);
        // 筛选 
        this.filter.init();

        // 绑定事件 
        bind.event();
    };

    // 当前操作 
    this.current = { dom: {} },
    this.arr = $api.domAll('.btn-nav'),
    this.aParent = $api.domAll('.tools > ul > li');

    this.base = {
        arr: [],
        old: {},
        name: '',
        init: function () {
            var _this = this;

            _this.arr = $api.domAll(_this.name + '-list', 'li');

            $api.addEvt(_this.arr, 'click', function () {
                var obj = { key: '', value: '' },
                    txt;

                // 保存当前选中的 dom 
                _this.old = $api.domAll(this.parentNode, '.select');

                $api.removeCls(_this.old, 'select');
                $api.text(_this.old, '', 'this');
                $api.addCls(this, 'select');
                txt = $api.trim($api.text(this));
                $api.html(this, '<i class="fa fa-select"></i>' + txt);

                // 获得数据
                obj.key = $api.data(this, 'id');
                obj.value = txt;

                if (typeof _this.changeCallback === 'function') _this.changeCallback(obj);

                tools.close();
            });
        }
    };

    // 用途 
    this.usage = {};
    // 排序 
    this.sort = {};
    // 筛选 
    this.filter = {
        select: {
            trade: {}
        },
        init: function () {
            var _this = this;
            // 交易类型 
            _this.select.trade = new Select({
                title: '交易类型',
                show: false,
                target: 'trade-list',
                key: 'tradeType',
                value: 'selectTrade',
                html: '<li data-id="0" class="select"><i class="fa fa-select"></i>全部</li><li data-id="1">出售</li><li data-id="2">出租</li>',
                change: function () {
                    var val = $api.val('tradeType');

                    if (val == 0 || val == 1) {
                        $api.text('unit', '万元');
                        return;
                    }
                    $api.text('unit', '元/月');
                }
            });
            // 改变交易类型 
            $api.addEvt('selectTrade', 'click', function () {
                _this.select.trade.show();
            });
            // 搜索 
            $api.addEvt('btnSearch', 'click', function () {

                if (form.validate(page.tools.form.id)) {
                    $api.setStorage(page.tools.form.saveName, form.condition(page.tools.form.id));
                    // 加载数据 
                    hlist.load.data(0);

                    tools.close();
                }

            });
            // 清除条件 
            $api.addEvt('btnClear', 'click', function () {

                form.clear(page.tools.form.id, page.tools.form.clearName);
            });

            // 保存 清除条件 的默认值 
            form.clear.set(page.tools.form.id, page.tools.form.clearName);
        }
    };

    // 显示 
    this.show = function () {
        var _select = $api.dom(this.parentNode, '.ui-item');

        tools.hide();

        $api.css(this, 'color:#149b70');
        $api.css($api.dom(this, '.fa'), 'color:#149b70');

        $api.show(_select);
        $api.css('.l-sub-mask', 'height:' + $api.dom(".content").clientHeight + 'px');
        $api.show('.l-sub-mask');

        if (_select.id == 'filter' && $api.data(_select, 'status') == '0') {
            // 初始化 表单 
            form.init();
            $api.data(_select, 'status', '1');
        }
    };
    // 隐藏 
    this.hide = function (animated) {
        var i = 0,
            _select,
            _parent;

        for (i = 0; i < this.aParent.length; i++) {
            _parent = this.aParent[i];
            _select = $api.dom(_parent, '.ui-item');

            $api.css($api.dom(_parent, '.btn-nav'), 'color:#333');
            $api.css($api.dom(_parent, '.fa'), 'color:#808080');

            $api.hide(_select);
        }
    };
    // 关闭 
    this.close = function () {
        setTimeout(function () {
            // 点击遮蔽层  
            $api.dom('.l-sub-mask').click();
        }, 500);
    };

};

/*
 * 盘源 
 */
var hlist = new function () {

    "use strict";

    var
        // 是否正在加载中 
        isLoad = 0,
        // 绑定
        bind = {
            // 事件
            event: function () {
                $api.addEvt($api.dom('.content'), 'scroll', function () {
                    // 置顶
                    common.top.init(this);
                    if (isLoad != 0) return;
                    if (this.scrollTop >= (this.scrollHeight - this.clientHeight - 30)) {
                        $api.show('msg');
                        $api.html($api.byId('msg'), '<img src="/Content/images/load.gif" alt="" />正在加载请稍后 ...');
                        isLoad = 1;
                        hlist.ajax.data.UseageId = $api.val('usage');
                        // 绑定数据
                        bind.data(page.list, 1);
                    }
                });
                $api.addEvt('usage', 'change', function () {
                    isLoad = 1;
                    hlist.ajax.data.UseageId = $api.val('usage');
                    $api.html(page.list, '<tbody></tbody>');
                    $api.html($api.byId('msg'), '');
                    hlist.ajax.data.Page = 0;
                    // 绑定数据
                    bind.data(page.list);
                });
                // 页面改变大小 
                $api.addEvt(window, 'resize', function () {
                    var aul = $api.domAll(page.list, 'ul');
                    // 格式化 样式 
                    formatStyle(aul);
                });
            },
            // 条件
            condition: function () {
            },
            // 数据
            data: function (id, type) {
                // 下一页 
                hlist.ajax.data.Page = parseInt(hlist.ajax.data.Page) + 1;
                common.ajax.post({
                    url: hlist.ajax.url,
                    data: hlist.ajax.data,
                    load: { type: type },
                    error: function () {
                        isLoad = 0;
                    },
                    callback: function (rs) {
                        // 判断数据状态 
                        if (rs.Status != 0) {
                            // 数据绑定失败 
                            isLoad = 2;
                            if (rs.Status == 1) {
                                $api.show('msg'); $api.html($api.byId('msg'), '<i class="fa fa-null"></i>系统内暂无数据 !');
                                return;
                            }

                            common.tip(rs.Message);
                            return;
                        }

                        var len = rs.Obj.Data.length,
                            gpId = 'gp' + rs.Obj.Data[0].Id,
                            obj = $api.byId(id),
                            fs = $api.dom('.content').clientWidth * 0.33 * 0.88,
                            ahtml = [],
                            data = {},
                            unit = "",
                            j = 0,
                            i;

                        // 绑定数据 , gpId 当前绑定的一组 ul 
                        for (i = 0; i < len; i++) {
                            data = rs.Obj.Data[i];
                            unit = data.TradeType == 1 ? "万元" : "元/月";

                            // (data.ImgSrc == null ? '' : data.ImgSrc) 
                            if (j >= asrc.length) { j = 0; }

                            ahtml.push('<ul class="item ' + gpId + '" id="' + data.Id + '">\
        <li><img src="' + asrc[j] + '" onclick="hlist.open(' + data.Id + ')" class="dn" /><i class="fa fa-picture" onclick="hlist.open(' + data.Id + ')" style="font-size:' + (fs > 150 ? 150 : fs) + 'px"></i></li>\
        <li>\
            <p class="housing-title cb" onclick="hlist.open(' + data.Id + ')">' + data.Name + '房</p>\
            <p>' + data.CityName + ' ' + data.AreaName + '</p>\
            <p>' + data.Room + '房 ' + data.Hall + '厅 ' + data.Acreage + '㎡ ' + ((data.Direct == null || data.Direct == "") ? "" : data.Direct) + '</p>\
            <p class="pos-a" style="right: 5px;top: 23px;"><span class="price">' + data.Price + '</span> ' + unit + '</p>\
        </li>\
    </ul>');
                            j++;
                        }
                        $api.append(obj, ahtml.join(''));

                        // 根据 gpId 获得当前 ul 数组 
                        var imgIsLoad = 0,
                            aul = $api.domAll('.' + gpId),
                            aimg = getImgs(aul), // 获得当前所有的图片 
                            len = aimg.length, // 图片长度 
                            j = 0,
                            _w = 0; // 图片宽度 

                        for (i = 0; i < len; i++) {
                            // 图片加载成功 
                            aimg[i].onload = function () {

                                $api.removeCls(this, 'dn');
                                $api.remove($api.dom(this.parentNode, '.fa-picture'));

                                if (imgIsLoad == 0) {
                                    // 根据加载成功的第一张图片的高度 计算 li 的高度 height , 行高 line-height ; 
                                    calcStyle(aul, (this.clientHeight));
                                    // 已加载过 
                                    imgIsLoad = 1;
                                    // 图片宽度 
                                    _w = this.clientWidth;
                                }
                            };
                            // 图片加载出错 
                            aimg[i].onerror = function () {
                                //// 把错误的图片 替换为默认样式 
                                //var p = this.parentNode,
                                //    w = ((this.clientWidth || _w) / 1.17);

                                ////$api.html(p, '<i class="fa fa-picture" onclick="' + $api.attr(this, 'onclick') + '" style="font-size:' + (w || 95) + 'px;"></i>');

                                //j++;
                                //// 如果全部图片都出错 
                                //if (j == len && imgIsLoad == 0) {
                                //    // 计算 li 的高度 height , 行高 line-height ; 
                                //    calcStyle(aul, ($api.dom('.fa-picture').clientHeight - 1));
                                //}
                            };
                        }

                        // 数据绑定成功  
                        isLoad = 0;
                    }
                });
            }
        };

    // 格式化 样式 
    function formatStyle(aul) {
        var aimg = getImgs(aul), // 获得图片数组对象 
            len = aimg.length,
            i = 0,
            img = null;

        if (len > 0) {

            for (i = 0; i < len; i++) {
                if (aimg[i].className != 'dn') {
                    img = aimg[i];
                    break;
                }
            }

            if (img) {
                calcStyle(aul, img.clientHeight);

                var w = (img.clientWidth / 1.17);
                $api.css($api.domAll('.fa-picture'), 'font-size:' + w + 'px');
            }

            return;
        }
    }
    // 根据 ul 数组对象, 获得子级 img 数组对象 
    function getImgs(aul) {
        var aimg = [],
            j = aul.length,
            oimg;

        while (true) {
            j--;
            oimg = $api.dom(aul[j], 'li:nth-child(1) img');
            if (oimg != null) aimg.push(oimg); // 只添加 img 
            if (j <= 0) break;
        }
        return aimg;
    }
    // 设置样式 
    function calcStyle(aul, height) {
        var j = aul.length,
            h = parseInt(height / 3);

        while (true) {
            j--;
            $api.css($api.dom(aul[j], 'li:nth-child(2)'), 'height:' + h + 'px;line-height:' + h + 'px');
            $api.css($api.last(aul[j], 'li:nth-child(2) p'), 'top:' + (h + 1) + 'px');
            $api.css(aul[j], 'height:' + (h * 3 + 5) + 'px');
            if (j <= 0) break;
        }
    }

    this.ajax = { url: page.ajax.url, data: page.ajax.data };
    // 加载
    this.load = function () {
        // tools 初始化 
        tools.init();
        // 用途改变 
        tools.usage.changeCallback = function (obj) {
            $api.val('usage', obj.Key);

            // 加载数据 
            hlist.load.data();
        };
        // 排序改变 
        tools.sort.changeCallback = function (obj) {
            $api.val('orderBy', obj.Key);

            // 加载数据 
            hlist.load.data();
        };

        // 绑定事件
        bind.event();
        // 绑定条件
        bind.condition();

        // 加载数据 
        this.load.data();

        // 设置元素宽度 
        $api.css($api.domAll('.ui-item'), 'width:' + ($api.offset($api.dom('.content')).w) + 'px');
    };
    // 加载数据 
    this.load.data = function () {

        hlist.ajax.data.UseageId = $api.val('usage');
        hlist.ajax.data.Page = 0;
        isLoad = 1;

        $api.html($api.byId(page.list), '');
        // 绑定数据
        bind.data(page.list);
    };
    // 打开详情
    this.open = function (id) {
        window.location.href = '/Housing/Detail?id=' + id + '&userId=' + req.userId + '&source=' + page.source;
    };
};

/*
 * 加载 回调函数 
 */
common.loadCallback = function () {
    // 加载 我的盘源 
    hlist.load();
};
