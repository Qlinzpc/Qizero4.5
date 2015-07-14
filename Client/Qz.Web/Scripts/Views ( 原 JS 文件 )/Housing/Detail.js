
/*
 * 详情
 */
var detail = new function () {
    this.ajax = { url: "/Housing/DetailData", data: { UserId: req.userId, HousingId: req.id, source: (req.source || 0) } };
    this.data = { Lng: '114.031024', Lat: '22.541168' };

    // 加载
    this.load = function () {
        // 绑定事件
        bind.event();
        // 绑定数据
        bind.data('detail');

        // 创建微信 
        weix.create();
    };

    // 绑定
    var bind = {
        // 事件
        event: function () {
            // 展开 ( 收缩 ) 更多详细消息
            $api.addEvt('btn-more', 'click', function () {
                var txt = $api.text(this);
                var arr = $api.domAll('.more');
                var i = arr.length;
                if (txt == '查看更多详细消息>>') {
                    $api.text(this, '隐藏更多详细消息>>');
                } else {
                    $api.text(this, '查看更多详细消息>>');
                }
                var t = setInterval(function () {
                    $api.toggleCls(arr[--i], 'dn');
                    if (i <= 0) {
                        clearInterval(t);
                        return;
                    }
                }, 50);

            });
            // 选项卡
            $api.addEvt($api.domAll('.nav .btn'), 'click', function () {
                var target = $api.data(this, 'target');
                var arrNav = $api.domAll('.nav .btn');
                var arrInfo = $api.domAll('.info');

                $api.removeCls(arrNav, 'select');
                $api.addCls(this, 'select');

                $api.addCls(arrInfo, 'dn');
                $api.removeCls(target, 'dn');

                if (target != 'tel-info') _ishidetel = 0;
                detail.img.isShow = false;
                // 房源图片
                if (target == 'pic-info') {
                    detail.img.isShow = true;
                    if (_isformat == 0) {
                        formatPic(target); // 格式化图片 
                        detail.img.load();  // 加载图片 
                    }
                }
                // 电话号码
                if (target == 'tel-info' && _ishidetel == 0) {
                    recordTel(($api.val("showTel") == "1"));  // 查看电话记录
                }
            });
            // 滚动条
            $api.addEvt($api.dom('.content'), 'scroll', function () {
                // 置顶 
                common.top.init(this);

                if (detail.img.isShow && detail.img.getUploadLen() > 0) {
                    var obj = $api.dom('#pic-info');

                    if (this.scrollTop >= obj.offsetTop * 0.40) {
                        $api.show("upload");
                    } else {
                        $api.hide("upload");
                    }
                }

            });

            // 选择图片 
            $api.addEvt($api.domAll('#pic-info .fa-picture'), 'click', detail.img.choose);
            // 图片上传 
            $api.addEvt('upload', 'click', detail.img.upload);
            // 图片预览 
            $api.addEvt($api.domAll('#pic-info img'), 'click', detail.img.preview);
        },
        // 数据
        data: function (id) {
            common.ajax.post({
                url: detail.ajax.url,
                data: detail.ajax.data,
                callback: function (rs) {
                    if (rs.Status != 0) {
                        common.tip(rs.Message);
                        return;
                    }
                    detail.data = rs.Obj;
                    var arr = $api.domAll(id, 'span[data-bind]'), _this, bname;
                    for (var i = 0; i < arr.length; i++) {
                        _this = arr[i];
                        bname = $api.data(_this, 'bind');
                        if (bname == "Id") { $api.text(_this, req.id); continue; }
                        $api.text(_this, detail.data[bname] || "");
                    }

                    // 创建地图
                    map.create();
                }
            });
        }
    }

    // 图片 
    this.img = new function () {
        var arr = { uploadData: [], serverData: [] }, len = arr.uploadData.length;
        // 默认图片大小 
        this.fontSize = '150px',
        // 房源图片 选项卡状态, 是否处于显示状态 
        this.isShow = false,
        // 是否已打开选择图片 
        this.isOpen = false;

        // 预览图片集合 
        this.asrc = ['http://isz.zlhome.com/upLoad/HouseInfos/2015/3/17/0e86533f-3942-43ed-b18a-30fe30d0090a/3.jpg',
'http://isz.zlhome.com/upLoad/HouseInfos/2014/12/19/62604e45-2c47-49c4-9a72-bd8ec274cec5/3.jpg',
'http://isz.zlhome.com/upLoad/HouseInfos/2015/1/4/a1aad13e-70c4-436d-babc-7aac98430bcc/3.jpg',
'http://isz.zlhome.com/upLoad/HouseInfos/2015/3/18/4b84bcdb-6aa3-4b2e-9b28-f61645d3f0b2/3.jpg',
'http://isz.zlhome.com/upLoad/HouseInfos/2015/3/21/8bdc72dd-d37b-487f-8529-e281a7baf2f2/3.jpg',
'http://isz.zlhome.com/upLoad/HouseInfos/2014/11/26/25ec8906-5860-41b7-a326-ed349c46ab56/3.jpg',
'http://isz.zlhome.com/upLoad/HouseInfos/2014/11/26/35967000-2e54-4856-b5e5-03cdb3256bda/3.jpg'];

        // 图片加载 
        this.load = function () {
            var ajax = { url: "/Housing/PictureData", data: '{ "UserId": "' + req.userId + '","HousingId": "' + req.id + '" }' };
            common.ajax.post({
                contentType: "application/json",
                url: ajax.url,
                data: ajax.data,
                callback: function (rs) {
                    if (rs.Status != 0) { common.tip(rs.Message); return; }

                    var aData = rs.Obj.Data, oData, dlen = aData.length, i = 0, _this, _par, _h;
                    for (i = 0; i < dlen; i++) {
                        oData = aData[i];
                        // 获得当前对象 和 当前对象的父元素 
                        _this = detail.img.get(oData.Type), _par = _this.parentNode, _h = _par.clientHeight;
                        // 显示图片 
                        $api.html(_par, '<img style="height:' + (_h - 50) + 'px" data-type="' + oData.Type + '" src="' + $api.val('serverImgUrl') + oData.Url + $api.val('showImg') + '" onclick="detail.img.preview.apply(this)" /><span>' + $api.trim($api.text(_par)) + '</span>');
                        // 将上传成功的图片 url 添加到 '当前图片集合 asrc' 中 
                        detail.img.asrc.push($api.val('serverImgUrl') + oData.Url + $api.val('previewImg'));
                    }

                    var t = setTimeout(function () {
                        weix.config();        // 配置微信 API 
                        clearTimeout(t);
                    }, 500);
                }
            });



        };

        // 选择图片 
        this.choose = function () {
            if (detail.img.isOpen) { common.tip('打开中, 请稍后 !'); return; }

            var _this = this, par = this.parentNode, _cnt, _w = par.clientWidth, _h = par.clientHeight, _t = $api.data(_this, 'type');
            // 检测 weixin api 是否启用 
            wx.checkJsApi({
                jsApiList: ['chooseImage'],
                success: function (res) {

                    common.tip('打开中, 请稍后 !');
                    detail.img.isOpen = true; // 正在打开 

                    wx.chooseImage({
                        success: function (res) {
                            cnt = res.localIds.length;
                            if (cnt > 1) {
                                common.tip('只能选择 1 张图片, 已选择 ' + cnt + ' 张图片 !', 3500);
                                return;
                            }
                            // 先删除之前选择的图片 
                            removeUpload(_t);
                            // 显示已选择的图片 
                            $api.html(par, '<img style="height:' + (_h - 50) + 'px" data-type="' + _t + '" src="' + res.localIds[0] + '" onclick="detail.img.choose.apply(this)" /><span>' + $api.trim($api.text(par)) + '</span><i class="fa fa-del animated slideInDown" onclick="detail.img.del(this,' + _t + ');"></i>');

                            // 保存需要上传的 localId 
                            arr.uploadData.push({ id: res.localIds[0], type: _t });
                            len = arr.uploadData.length;

                            // 显示上传按钮操作 
                            showUpload(len);

                            detail.img.isOpen = false;
                        },
                        fail: function (res) {
                            common.tip(JSON.stringify(res));
                            detail.img.isOpen = false;
                        }
                    });
                },
                fail: function (res) {
                    common.tip(JSON.stringify(res));
                    detail.img.isOpen = false;
                }
            });

        };

        // 图片上传 
        this.upload = function () {
            var i = 0, _upload;
            arr.serverData = [];

            uploadImg();
            function uploadImg() {
                // 获得需上传的 uploadData 
                _upload = arr.uploadData[i];
                // 上传至微信服务器 
                wx.uploadImage({
                    localId: _upload.id,
                    success: function (res) {
                        i++;
                        try {
                            common.tip("已上传 " + i + " / " + len);
                            // 保存微信服务器 serverId 
                            arr.serverData.push('{ "Type": "' + _upload.type + '", "MediaId": "' + res.serverId + '" }');

                            if (i < len) {
                                uploadImg();
                            }
                            else if (i == len) {
                                var ajax = {
                                    url: "/Housing/PictureUpload",
                                    data: '{ "UserId":"' + req.userId + '","HousingId":"' + req.id + '","Data": [' + arr.serverData.join(",") + '] }'
                                };

                                // 上传至服务器 
                                common.ajax.post({
                                    contentType: "application/json",
                                    url: ajax.url,
                                    data: ajax.data,
                                    timeout: 20000,
                                    callback: function (rs) {
                                        var asuccess, afail, osuccess, ofail, slen, flen, par, type, obj, _this, slen = flen = 0;
                                        if (rs.Status == 2) { common.tip(rs.Message); return; }

                                        arr.uploadData = [];
                                        showUpload(0);
                                        common.tip(rs.Message, 3500);

                                        ///* 上传成功后, 取消删除标志, 加上 [ 已成功 ] 标志 */
                                        //// 1. 获得上传成功的图片 
                                        //asuccess = rs.Obj.SuccessData, slen = asuccess.length;

                                        //detail.img.asrc = [];
                                        //for (i = 0; i < slen; i++) {
                                        //    // 2.0 获得上传成功的 数据 
                                        //    obj = asuccess[i];
                                        //    // 2.1 移除之前上传的 uploadData 
                                        //    removeUpload(obj.Type);
                                        //    // 2.2 获得当前对象 和 当前对象的父元素 
                                        //    _this = detail.img.get(obj.Type),par = _this.parentNode; 
                                        //    // 2.3 取消删除标志 
                                        //    $api.remove( $api.dom( par, '.fa-del' ) );
                                        //    // 2.4 将 选择图片事件 替换为 图片预览事件, 加上 [ 已成功 ] 标志 
                                        //    $api.html(par, $api.html(par).replace(".choose.",".preview.") + '<i class="fa fa-success"></i>' ); 
                                        //    // 2.5 将上传成功的图片 url 添加到 '当前图片集合 asrc' 中 
                                        //    detail.img.asrc.push(obj.Url);
                                        //}

                                        ///* 上传失败的图片, 加上 [ 失败标志 ] 标志 */
                                        //// 1. 获得上传失败的图片 
                                        //afail = rs.Obj.FailData, flen = afail.length;

                                        //for (i = 0; i < flen; i++) {
                                        //    par = detail.img.get(afail[i]).parentNode ;
                                        //    // 2. 加上 [ 失败标志 ] 标志 
                                        //    $api.html(par, $api.html(par) + '<i class="fa fa-fail animated slideInDown"></i>'); 
                                        //}

                                        //// 显示 上传图片 操作 
                                        //showUpload(len);
                                        //common.tip('图片上传成功 '+slen+' 张, 失败 '+flen+' 张 !' + detail.img.asrc.length);
                                    },
                                    error: function (er) {
                                        common.tip(er);
                                    }
                                });
                            } else {
                                common.tip('上传失败, 请重新上传 !');
                            }
                        } catch (e) {
                            common.tip(e.message);
                        }
                    },
                    fail: function (res) {
                        common.tip(JSON.stringify(res));
                    }
                });
            }
        };

        // 图片预览
        this.preview = function () {
            wx.previewImage({
                current: $api.attr(this, 'src'),
                urls: detail.img.asrc
            });
        };

        // 删除已选择图片 
        this.del = function (target, type) {
            var par = target.parentNode, item, index;
            // 显示默认图片 
            $api.html(par, '<i data-type="' + type + '" style="font-size:' + detail.img.fontSize + '" class="fa fa-picture" onclick="detail.img.choose.call(this)"></i><span>' + $api.trim($api.text(par)) + '</span>');

            // 移除当前上传的 uploadData 
            removeUpload(type);

            // 显示上传按钮操作 
            showUpload(len);
            common.tip("已成功删除 !");
        };

        // 根据 data-type 获得对象
        this.get = function (type) {
            var par = $api.dom("#pic-info");
            return ($api.dom(par, 'li i[data-type="' + type + '"]') || $api.dom(par, 'li img[data-type="' + type + '"]'));
        };

        // 获得 upload 数量 
        this.getUploadLen = function () {
            return arr.uploadData.length;
        };

        // 显示 上传图片 操作 
        function showUpload(cnt) {
            cnt = cnt || 0; cnt > 0 && ($api.show('upload'), $api.text('upload', '上传图片 ' + cnt)); cnt == 0 && ($api.hide('upload'));
        };
        // 根据类型移除 uploadData 
        function removeUpload(type) {
            var index = -1;
            // 移除当前上传的 uploadData 
            for (var i = 0; i < len; i++) {
                item = arr.uploadData[i];
                if (item.type == type) {
                    index = i;
                    break;
                }
            }
            index != -1 && (arr.uploadData.splice(index, 1), len = arr.uploadData.length);
        };
    };

    var _isformat = 0, _ishidetel = 0;
    // 格式化图片
    function formatPic(target) {
        var oul = $api.dom(target, 'ul');
        var w = (oul.clientWidth - 9) / 2 / 1.13 - 1;
        detail.img.fontSize = w + 'px';
        $api.css($api.domAll(oul, '.fa-picture'), 'font-size:' + detail.img.fontSize);
        _isformat = 1; // 已格式化图片
    };
    // 查看电话 
    function recordTel(isrecord) {
        _ishidetel = 1;
        if (isrecord) {

            var ajax = { url: "/Housing/DetailTelData", data: '{ "UserId": "' + req.userId + '","HousingId": "' + req.id + '","Source": "' + req.source + '","Type": "2" }' };
            common.ajax.post({
                contentType: "application/json",
                url: ajax.url,
                data: ajax.data,
                callback: function (rs) {
                    if (rs.Status != 0) { common.tip(rs.Message); return; }

                    common.tip("查看电话已记录成功 !");

                    $api.text('tel-number', rs.Obj.OwnerTel);
                    $api.data('tel', 'tel', rs.Obj.OwnerTel);
                }
            });

        }
    };
};

/*
 * 地图
 */
var map = new function () {
    // 动态加载 百度地图 api js
    this.create = function () {
        var t = setTimeout(function () {
            // common.loadJC('http://api.map.baidu.com/api?type=quick&ak=XgAuf1cEQMD810eWwx6R2Ta3&v=1.0&callback=map.init');
            clearTimeout(t);
        }, 200);
    };
    // 加载完毕执行
    this.init = function () {
        // 百度地图API功能
        var _map = new BMap.Map("map");
        _map.centerAndZoom(new BMap.Point(detail.data.Lng, detail.data.Lat), 18);
        _map.addControl(new BMap.ZoomControl());  //添加地图缩放控件
        var marker = new BMap.Marker(new BMap.Point(detail.data.Lng, detail.data.Lat));  //创建标注
        _map.addOverlay(marker); // 将标注添加到地图中
        _map.addEventListener("click", function () {

        });
        common.tip('baidu map api 加载完毕');
        var t = setInterval(function () {
            if ($api.dom('.anchorBL')) {
                _map.disableDragging();

                $api.hide('.anchorBL');
                clearInterval(t);
            }
        }, 500);
    };
};

/*
 * 微信 API
 */
var weix = new function () {
    // 动态加载 微信 api js
    this.create = function () {
        common.loadJC('http://res.wx.qq.com/open/js/jweixin-1.0.0.js?jcallback=weix.init');
    };
    // 初始化 
    this.init = function () {
        wx.ready(function () {

        });
        wx.error(function (res) {
            alert(res.errMsg);
        });
    };
    // 配置 
    this.config = function () {
        var ajax = { url: "/Common/WeiXinAPI", data: { userId: req.userId } };
        common.ajax.post({
            url: ajax.url,
            data: ajax.data,
            callback: function (rs) {
                var appId = rs.appId;
                var timestamp = rs.timestamp;
                var nonceStr = rs.nonceStr;
                var signature = rs.signature;
                // 微信api config 配置
                wx.config({
                    debug: false,
                    appId: appId,
                    timestamp: timestamp,
                    nonceStr: nonceStr,
                    signature: signature,
                    jsApiList: [
                      'checkJsApi',
                      'chooseImage',
                      'previewImage',
                      'uploadImage',
                      'downloadImage',
                      'openLocation',
                      'getLocation',
                      'hideOptionMenu',
                      'showOptionMenu'
                    ]
                });
            }
        });
    };
};

/*
 * 加载 回调函数
 */
common.loadCallback = function () {
    // 加载 盘源详情
    detail.load();
};