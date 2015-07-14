// 获得请求参数
var req = common.request();
// 加载后执行
function init() {
    $api.val('userId', req.userId);
    // 设置 .content 高度
    var h = window.innerHeight - $api.dom('.header').clientHeight - $api.dom('.footer').clientHeight;
    $api.css($api.dom('.content'), 'height:' + h + 'px;overflow-y: scroll;');

    // 内网访问 标识
    req.interior = $api.val("isInteriorRequest");
};
// 跳转
function goHref(url) { url = url || ""; if (url == "") { window.history.go(-1); return; } window.location.href = url + '?userId=' + req.userId; }

// 加载事件
common.load.complete(function () {
    // 页面加载完成 初始化
    init();
    // 验证 UserId
    if (!req.userId) { common.tip('用户访问异常, 请联系管理员 !'); return; }

    // 判断内网访问 标识
    if (req.interior == "True") {
        var ajax = { url: $api.val("interiorRequestValidate"), data: { "userId": req.userId } };
        common.ajax.post({
            type: "xml",
            url: ajax.url,
            data: ajax.data,
            callback: function (rs) {
                if (rs.childNodes[0].data === 'true') {
                    var t = setTimeout(function () {
                        clearTimeout(t);
                        // 加载 回调
                        if (typeof common.loadCallback === "function") common.loadCallback();
                    }, 100);
                    return;
                }
                common.tip('请在指定网络下访问 !');
                $api.html('.content', '');
            },
            error: function (er) {
                // if (typeof common.loadCallback === "function") common.loadCallback();

                common.tip('请在指定网络下访问 !');
                $api.html('.content', '');
            }
        });
        return;
    }

    // 加载 回调
    if (typeof common.loadCallback === "function") common.loadCallback();
});
