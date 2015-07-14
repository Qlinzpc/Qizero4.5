window.onload = function () {
    var req = common.request();

    var allDiv = $api.domAll(".container", "div");
    var allTr = $api.domAll(".container", "tr");
    var win = window;
    var h = win.innerHeight / allTr.length - 5;
    $api.addEvt(win, 'resize', function () {
        $api.css(allDiv, "height:" + (this.innerHeight / allTr.length - 5) + "px");
    });
    $api.css(allDiv, "height:" + h + "px");

    $api.addEvt(allDiv, 'mouseover', function () { $api.css(this, "opacity:0.8"); });
    $api.addEvt(allDiv, 'mouseout', function () { $api.css(this, "opacity:1"); });
    $api.addEvt(allDiv, 'click', function () {
        var txt = $api.trimAll($api.text(this));
        if (txt == "") return;
        var url = $api.attr(this, "data-url");
        if (url == "") return;
        window.location.href = url + "?userId=" + req.userId;
    });

    var sort = 0;
    for (var i in data.home) {
        sort = data.home[i].sort;
        if (sort != undefined) {
            $api.css(allDiv[sort], "background:" + data.home[i].bdColor);
            $api.attr(allDiv[sort], "id", data.home[i].id);
            $api.attr(allDiv[sort], "data-url", data.home[i].url);
            if (data.home[i].icon != "") {
                $api.html(allDiv[sort], '<i class="fa fa-' + data.home[i].icon + '"></i><span class="title">' + data.home[i].title + '</span>');
            }
            else {
                $api.text(allDiv[sort], data.home[i].title);
            }
        }
    }

}

document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
    WeixinJSBridge.call('hideOptionMenu');
    WeixinJSBridge.call('hideToolbar');
});