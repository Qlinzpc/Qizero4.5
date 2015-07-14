/*
 * coccNginxHttpPush()
 * Version 1.0 脱离cocc WEB框架时,坐席获取个人话务事件的方法
 * 
 * 适用于客户自行开发呼叫中心系统 或 在已有系统内嵌套cocc时，即时获取事件。
 *
 * Copyright (c) 2007-2013 Inc.All Rights Reserved
 *
 * 
 */

//检查所需变量是否存在 
function coccCheckVariable() {

    //cocc服务器IP
    if (typeof (cocc_ip) != 'undefined') {
        if (cocc_ip == '') {
            alert("Please defined cocc_ip");
            return;
        }
    } else {
        alert("Please defined cocc_ip");
        return;
    }

    //cocc坐席所属团队ID
    if (typeof (cocc_identity) != 'undefined') {
        if (cocc_identity == '') {
            alert("Please defined cocc_identity");
            return;
        }
    } else {
        alert("Please defined cocc_identity");
        return;
    }

    //cocc密码
    if (typeof (cocc_pwd) != 'undefined') {
        if (cocc_pwd == '') {
            alert("Please defined cocc_pwd");
            return;
        }
    } else {
        alert("Please defined cocc_pwd");
        return;
    }

}


//连接cocc服务器的方法
function coccNginxHttpPush() {
    if (document.getElementById("cocc_NginxHttpPushFrame") != null) {
        document.getElementById("cocc_NginxHttpPushFrame").parentNode.removeChild(document.getElementById("cocc_NginxHttpPushFrame"));
    }

    coccCheckVariable();

    var iframe = document.createElement("iframe");
    iframe.src = 'javascript:;';
    iframe.id = 'cocc_NginxHttpPushFrame';
    iframe.name = 'cocc_NginxHttpPushFrame';
    document.body.appendChild(iframe);

    var cocc_NginxHttpPushFrameHtml = '<html><head><meta content="text/html; charset=UTF-8" http-equiv="Content-Type"/>';
    cocc_NginxHttpPushFrameHtml += '<script type="text/javascript">';
    cocc_NginxHttpPushFrameHtml += 'var teamidentity = "init";';
    cocc_NginxHttpPushFrameHtml += 'var agentno = "init";';
    cocc_NginxHttpPushFrameHtml += 'var pwd = "init";';
    cocc_NginxHttpPushFrameHtml += 'var script;';

    //cocc获得新事件时，响应此函数。
    cocc_NginxHttpPushFrameHtml += 'function coccEvents(json) {';
    cocc_NginxHttpPushFrameHtml += '		var curetag;';
    cocc_NginxHttpPushFrameHtml += '		var cursince;';
    cocc_NginxHttpPushFrameHtml += '		for(var i=0;i<json.length;i++){';
    cocc_NginxHttpPushFrameHtml += '			if(json[i] == null){';
    cocc_NginxHttpPushFrameHtml += '			}else{';
    cocc_NginxHttpPushFrameHtml += '				eval ("var jsondata = " + ""+json[i].text+"");';
    //cocc_NginxHttpPushFrameHtml += '				if( jsondata.event == "ringing" || jsondata.event == "answer" || jsondata.event == "hangup" || jsondata.event == "hangupacw" || jsondata.event == "join" || jsondata.event == "incoming" || jsondata.event == "onhold" || jsondata.event == "resume" || jsondata.event == "paused" || jsondata.event == "login" || jsondata.event == "logoff" || jsondata.event == "nopaused" || jsondata.event == "acwoff" || jsondata.event == "acwring" || jsondata.event == "acwanswer" || jsondata.event == "workwaydialout" || jsondata.event == "workwayall" || jsondata.event == "workwaydialin"){';	
    cocc_NginxHttpPushFrameHtml += '					try{window.parent.sonAccept(""+jsondata.pushstr+"");}catch (e){console.log(e);}';
    //cocc_NginxHttpPushFrameHtml += '				}';
    cocc_NginxHttpPushFrameHtml += '				curetag=json[i].tag;';
    cocc_NginxHttpPushFrameHtml += '				cursince=json[i].time;';
    cocc_NginxHttpPushFrameHtml += '			}';
    cocc_NginxHttpPushFrameHtml += '		}';
    cocc_NginxHttpPushFrameHtml += '		document.getElementsByTagName("head")[0].removeChild(script);';
    cocc_NginxHttpPushFrameHtml += '		anhpPublicInterface(curetag,cursince);';
    cocc_NginxHttpPushFrameHtml += '}';

    //连接cocc服务器的方法
    cocc_NginxHttpPushFrameHtml += 'function anhpPublicInterface(etag,since) {';
    cocc_NginxHttpPushFrameHtml += '		var myDate = new Date();';
    cocc_NginxHttpPushFrameHtml += '		var urlrandom = myDate.getTime();';
    cocc_NginxHttpPushFrameHtml += '		if(agentno != ""){';
    cocc_NginxHttpPushFrameHtml += '			var url = "http://' + cocc_ip + '/publicapi/agentpull/"+teamidentity+"-"+agentno+"-"+pwd+"?_="+urlrandom+"&callback=coccEvents&etag="+etag+"&since="+since+"";';
    cocc_NginxHttpPushFrameHtml += '		}else{';
    cocc_NginxHttpPushFrameHtml += '			var url = "http://' + cocc_ip + '/publicapi/agentpull/"+teamidentity+"-"+pwd+"?_="+urlrandom+"&callback=coccEvents&etag="+etag+"&since="+since+"";';
    cocc_NginxHttpPushFrameHtml += '		}';
    cocc_NginxHttpPushFrameHtml += '		script = document.createElement("script");';
    cocc_NginxHttpPushFrameHtml += '		script.setAttribute("async","async");';
    cocc_NginxHttpPushFrameHtml += '		script.setAttribute("src", url);';
    cocc_NginxHttpPushFrameHtml += '		script.setAttribute("id", "scriptid_"+urlrandom);';
    cocc_NginxHttpPushFrameHtml += '		document.getElementsByTagName("head")[0].appendChild(script);';
    cocc_NginxHttpPushFrameHtml += '		listenscript("scriptid_"+urlrandom,etag,since);';
    cocc_NginxHttpPushFrameHtml += '}';

    cocc_NginxHttpPushFrameHtml += 'function listenscript(scriptid,orietag,orisince){';
    cocc_NginxHttpPushFrameHtml += '		var scriptobj = document.getElementById(scriptid);';
    cocc_NginxHttpPushFrameHtml += '		window.setTimeout(function(){';
    cocc_NginxHttpPushFrameHtml += '			if(scriptobj.parentNode){';
    cocc_NginxHttpPushFrameHtml += '				scriptobj.parentNode.removeChild(scriptobj);';
    cocc_NginxHttpPushFrameHtml += '				anhpPublicInterface(orietag,orisince);';
    cocc_NginxHttpPushFrameHtml += '			}';
    cocc_NginxHttpPushFrameHtml += '		}, 61000);';
    cocc_NginxHttpPushFrameHtml += '}';

    cocc_NginxHttpPushFrameHtml += 'function initHttpPush(initlast_modified,initidentity,initagentno,initpwd) {';
    cocc_NginxHttpPushFrameHtml += '		teamidentity  = initidentity;';
    cocc_NginxHttpPushFrameHtml += '		agentno = initagentno;';
    cocc_NginxHttpPushFrameHtml += '		pwd = initpwd;';
    cocc_NginxHttpPushFrameHtml += '		anhpPublicInterface(0,initlast_modified);';
    cocc_NginxHttpPushFrameHtml += '}';

    cocc_NginxHttpPushFrameHtml += '</script>';
    cocc_NginxHttpPushFrameHtml += '</head></html>';
    document.getElementById("cocc_NginxHttpPushFrame").contentWindow.document.write(cocc_NginxHttpPushFrameHtml);
    document.getElementById("cocc_NginxHttpPushFrame").contentWindow.document.close();

    if (iframe.attachEvent) {
        try {
            document.getElementById("cocc_NginxHttpPushFrame").contentWindow.initHttpPush(cocc_lastmodified, cocc_identity, cocc_agentno, cocc_pwd);
        } catch (e) {
            iframe.attachEvent("onload", function () {
                document.getElementById("cocc_NginxHttpPushFrame").contentWindow.initHttpPush(cocc_lastmodified, cocc_identity, cocc_agentno, cocc_pwd);
            });
        }
    } else {
        if (navigator.userAgent.indexOf('Firefox') >= 0) {
            iframe.onload = function () {
                document.getElementById("cocc_NginxHttpPushFrame").contentWindow.initHttpPush(cocc_lastmodified, cocc_identity, cocc_agentno, cocc_pwd);
            };
        } else {
            try {
                document.getElementById("cocc_NginxHttpPushFrame").contentWindow.initHttpPush(cocc_lastmodified, cocc_identity, cocc_agentno, cocc_pwd);
            } catch (e) {
                iframe.onload = function () {
                    document.getElementById("cocc_NginxHttpPushFrame").contentWindow.initHttpPush(cocc_lastmodified, cocc_identity, cocc_agentno, cocc_pwd);
                };
            }
        }
    }

    document.getElementById("cocc_NginxHttpPushFrame").style.display = 'none';
}
