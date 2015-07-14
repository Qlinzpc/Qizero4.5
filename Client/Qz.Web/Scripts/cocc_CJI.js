/*
 * astcc_CJI(cocc client javascript interface)
 * Version 1.0 基于jquery(http://code.jquery.com/jquery-1.4.2.min.js)
 * 
 * Copyright (c) 2007-2010 Inc.All Rights Reserved
 *
 * @Document:
 * http://cn.cocc.org
 * http://wiki.cocc.org/doku.php
 *
 * @Contact:
 * cocc <support@cocc.org>
 * 
 */

// var cocc_ip = '180.166.124.116';
if (typeof (cocc_ip) != 'undefined') {
    if (cocc_ip == '') {
        alert("Please defined cocc_ip");
    }
} else {
    alert("Please defined cocc_ip");
}

//登录
function loginCJI(orgidentity, usertype, user, pwdtype, password, callbackFuc) {
    $.getJSON("http://" + cocc_ip + "/setevent/loginCJI?callback=?", {
        usertype: usertype,
        user: user,
        orgidentity: orgidentity,
        pwdtype: pwdtype,
        password: password
    }, function (json) {
        try {
            if (typeof (callbackFuc) != 'undefined' && callbackFuc != '') {
                callbackFuc(json);
            }
        } catch (e) {
            //alert('loginCJI error!');
        }
    });
}

//登出
function logoutCJI(orgidentity, usertype, user, pwdtype, password, callbackFuc) {
    $.getJSON("http://" + cocc_ip + "/setevent/logoutCJI?callback=?", {
        usertype: usertype,
        user: user,
        orgidentity: orgidentity,
        pwdtype: pwdtype,
        password: password
    }, function (json) {
        try {
            if (typeof (callbackFuc) != 'undefined' && callbackFuc != '') {
                callbackFuc(json);
            }
        } catch (e) {
            //alert('logoutCJI error!');
        }
    });
}


//队列接口(分机示忙，闲)
function queueActionCJI(type, usertype, user, orgidentity, list, pwdtype, password, deviceexten, pushevent, callbackFuc, intparam) {
    $.getJSON("http://" + cocc_ip + "/setevent/queueActionCJI?callback=?", {
        type: type,
        usertype: usertype,
        user: user,
        orgidentity: orgidentity,
        list: list,
        pwdtype: pwdtype,
        password: password,
        deviceexten: deviceexten,
        pushevent: pushevent,
        intparam: intparam
    }, function (json) {
        try {
            if (typeof (callbackFuc) != 'undefined' && callbackFuc != '') {
                callbackFuc(json);
            }
        } catch (e) {
            //alert('queueActionCJI error!');
        }
    });
}


//(暂停/继续)服务
function queuePauseCJI(type, usertype, user, orgidentity, pwdtype, password, pause_reason, pushevent, callbackFuc, dnd, intparam) {
    $.getJSON("http://" + cocc_ip + "/setevent/queuePauseCJI?callback=?", {
        type: type,
        usertype: usertype,
        user: user,
        orgidentity: orgidentity,
        pwdtype: pwdtype,
        password: password,
        pause_reason: pause_reason,
        pushevent: pushevent,
        dnd: dnd,
        intparam: intparam
    }, function (json) {
        try {
            if (typeof (callbackFuc) != 'undefined' && callbackFuc != '') {
                callbackFuc(json);
            }
        } catch (e) {
            //alert('queuePauseCJI error!');
        }
    });
}


//切换事后模式
function acwActionCJI(type, usertype, user, orgidentity, pwdtype, password, agent_group_id, pushevent, callbackFuc) {
    $.getJSON("http://" + cocc_ip + "/setevent/acwActionCJI?callback=?", {
        type: type,
        usertype: usertype,
        user: user,
        orgidentity: orgidentity,
        pwdtype: pwdtype,
        password: password,
        agent_group_id: agent_group_id,
        pushevent: pushevent
    }, function (json) {
        try {
            if (typeof (callbackFuc) != 'undefined' && callbackFuc != '') {
                callbackFuc(json);
            }
        } catch (e) {
            //alert('acwActionCJI error!');
        }
    });
}


//结束事后
function acwOffCJI(usertype, user, orgidentity, pwdtype, password, pushevent, callbackFuc) {
    $.getJSON("http://" + cocc_ip + "/setevent/acwOffCJI?callback=?", {
        usertype: usertype,
        user: user,
        orgidentity: orgidentity,
        pwdtype: pwdtype,
        password: password,
        pushevent: pushevent
    }, function (json) {
        try {
            if (typeof (callbackFuc) != 'undefined' && callbackFuc != '') {
                callbackFuc(json);
            }
        } catch (e) {
            //alert('acwOffCJI error!');
        }
    });
}


//切换工作模式
function workwayActionCJI(status, usertype, user, orgidentity, pwdtype, password, agent_group_id, pushevent, callbackFuc) {
    $.getJSON("http://" + cocc_ip + "/setevent/workwayActionCJI?callback=?", {
        status: status,
        usertype: usertype,
        user: user,
        orgidentity: orgidentity,
        pwdtype: pwdtype,
        password: password,
        agent_group_id: agent_group_id,
        pushevent: pushevent
    }, function (json) {
        try {
            if (typeof (callbackFuc) != 'undefined' && callbackFuc != '') {
                callbackFuc(json);
            }
        } catch (e) {
            //alert('workwayActionCJI error!');
        }
    });
}


//呼叫接口
function makeCallCJI(targetdn, targettype, agentgroupid, usertype, user, orgidentity, pwdtype, password, modeltype, model_id, userdata, callbackFuc, agentexten) {
    $.getJSON("http://" + cocc_ip + "/setevent/makeCallCJI?callback=?", {
        targetdn: targetdn,
        targettype: targettype,
        agentgroupid: agentgroupid,
        usertype: usertype,
        user: user,
        orgidentity: orgidentity,
        pwdtype: pwdtype,
        password: password,
        modeltype: modeltype,
        model_id: model_id,
        userdata: userdata,
        agentexten: agentexten
    }, function (json) {
        try {
            if (typeof (callbackFuc) != 'undefined' && callbackFuc != '') {
                callbackFuc(json);
            }
        } catch (e) {
            //alert('makeCallCJI error!');
        }
    });
}

//咨询接口
function consultCJI(targetdn, agentgroupid, consulttype, pwdtype, password, usertype, user, orgidentity, callbackFuc) {
    $.getJSON("http://" + cocc_ip + "/setevent/consultCJI?callback=?", {
        targetdn: targetdn,
        agentgroupid: agentgroupid,
        consulttype: consulttype,
        pwdtype: pwdtype,
        password: password,
        usertype: usertype,
        user: user,
        orgidentity: orgidentity
    }, function (json) {
        try {
            if (typeof (callbackFuc) != 'undefined' && callbackFuc != '') {
                callbackFuc(json);
            }
        } catch (e) {
            //alert('consultCJI error!');
        }
    });
}

//转接接口
function transferCJI(pwdtype, password, usertype, user, orgidentity, callbackFuc) {
    $.getJSON("http://" + cocc_ip + "/setevent/transferCJI?callback=?", {
        pwdtype: pwdtype,
        password: password,
        usertype: usertype,
        user: user,
        orgidentity: orgidentity
    }, function (json) {
        try {
            if (typeof (callbackFuc) != 'undefined' && callbackFuc != '') {
                callbackFuc(json);
            }
        } catch (e) {
            //alert('transferCJI error!');
        }
    });
}

//接回接口
function callReturnCJI(pwdtype, password, usertype, user, orgidentity, callbackFuc) {
    $.getJSON("http://" + cocc_ip + "/setevent/callReturnCJI?callback=?", {
        pwdtype: pwdtype,
        password: password,
        usertype: usertype,
        user: user,
        orgidentity: orgidentity
    }, function (json) {
        try {
            if (typeof (callbackFuc) != 'undefined' && callbackFuc != '') {
                callbackFuc(json);
            }
        } catch (e) {
            //alert('callReturnCJI error!');
        }
    });
}

//会议接口
function conferenceCJI(pwdtype, password, usertype, user, orgidentity, callbackFuc) {
    $.getJSON("http://" + cocc_ip + "/setevent/conferenceCJI?callback=?", {
        pwdtype: pwdtype,
        password: password,
        usertype: usertype,
        user: user,
        orgidentity: orgidentity
    }, function (json) {
        try {
            if (typeof (callbackFuc) != 'undefined' && callbackFuc != '') {
                callbackFuc(json);
            }
        } catch (e) {
            //alert('conferenceCJI error!');
        }
    });
}

//挂断接口
function hangupCJI(uniqueid, targetagent, target, pwdtype, password, usertype, user, orgidentity, callbackFuc) {
    $.getJSON("http://" + cocc_ip + "/setevent/hangupCJI?callback=?", {
        uniqueid: uniqueid,
        targetagent: targetagent,
        target: target,
        pwdtype: pwdtype,
        password: password,
        usertype: usertype,
        user: user,
        orgidentity: orgidentity
    }, function (json) {
        try {
            if (typeof (callbackFuc) != 'undefined' && callbackFuc != '') {
                callbackFuc(json);
            }
        } catch (e) {
            //alert('hangupCJI error!');
        }
    });
}


//强插接口
function intrudeCJI(target, phonenumber, pwdtype, password, usertype, user, orgidentity, callbackFuc) {
    $.getJSON("http://" + cocc_ip + "/setevent/intrudeCJI?callback=?", {
        target: target,
        phonenumber: phonenumber,
        pwdtype: pwdtype,
        password: password,
        usertype: usertype,
        user: user,
        orgidentity: orgidentity
    }, function (json) {
        try {
            if (typeof (callbackFuc) != 'undefined' && callbackFuc != '') {
                callbackFuc(json);
            }
        } catch (e) {
            //alert('intrudeCJI error!');
        }
    });
}

//监听接口
function silentMonitorCJI(target, phonenumber, pwdtype, password, usertype, user, orgidentity, callbackFuc) {
    $.getJSON("http://" + cocc_ip + "/setevent/silentMonitorCJI?callback=?", {
        target: target,
        phonenumber: phonenumber,
        pwdtype: pwdtype,
        password: password,
        usertype: usertype,
        user: user,
        orgidentity: orgidentity
    }, function (json) {
        try {
            if (typeof (callbackFuc) != 'undefined' && callbackFuc != '') {
                callbackFuc(json);
            }
        } catch (e) {
            //alert('silentMonitorCJI error!');
        }
    });
}

//强拆接口
function forcedReleaseCJI(target, phonenumber, pwdtype, password, usertype, user, orgidentity, callbackFuc) {
    $.getJSON("http://" + cocc_ip + "/setevent/forcedReleaseCJI?callback=?", {
        target: target,
        phonenumber: phonenumber,
        pwdtype: pwdtype,
        password: password,
        usertype: usertype,
        user: user,
        orgidentity: orgidentity
    }, function (json) {
        try {
            if (typeof (callbackFuc) != 'undefined' && callbackFuc != '') {
                callbackFuc(json);
            }
        } catch (e) {
            //alert('forcedReleaseCJI error!');
        }
    });
}

//密语接口
function whisperCJI(target, phonenumber, pwdtype, password, usertype, user, orgidentity, callbackFuc) {
    $.getJSON("http://" + cocc_ip + "/setevent/whisperCJI?callback=?", {
        target: target,
        phonenumber: phonenumber,
        pwdtype: pwdtype,
        password: password,
        usertype: usertype,
        user: user,
        orgidentity: orgidentity
    }, function (json) {
        try {
            if (typeof (callbackFuc) != 'undefined' && callbackFuc != '') {
                callbackFuc(json);
            }
        } catch (e) {
            //alert('whisperCJI error!');
        }
    });
}

//通话暂停接口
function holdCJI(silence, orgidentity, usertype, user, pwdtype, password, callbackFuc) {
    $.getJSON("http://" + cocc_ip + "/setevent/holdCJI?callback=?", {
        silence: silence,
        orgidentity: orgidentity,
        usertype: usertype,
        user: user,
        pwdtype: pwdtype,
        password: password
    }, function (json) {
        try {
            if (typeof (callbackFuc) != 'undefined' && callbackFuc != '') {
                callbackFuc(json);
            }
        } catch (e) {
            //alert('holdCJI error!');
        }
    });
}

//通话继续接口
function resumeCJI(orgidentity, usertype, user, pwdtype, password, callbackFuc) {
    $.getJSON("http://" + cocc_ip + "/setevent/resumeCJI?callback=?", {
        orgidentity: orgidentity,
        usertype: usertype,
        user: user,
        pwdtype: pwdtype,
        password: password
    }, function (json) {
        try {
            if (typeof (callbackFuc) != 'undefined' && callbackFuc != '') {
                callbackFuc(json);
            }
        } catch (e) {
            //alert('resumeCJI error!');
        }
    });
}

//获取团队坐席状态
function teamStatusCJI(orgidentity, usertype, user, pwdtype, password, status, callbackFuc) {
    $.getJSON("http://" + cocc_ip + "/setevent/teamStatusCJI?callback=?", {
        status: status,
        orgidentity: orgidentity,
        usertype: usertype,
        user: user,
        pwdtype: pwdtype,
        password: password
    }, function (json) {
        try {
            if (typeof (callbackFuc) != 'undefined' && callbackFuc != '') {
                callbackFuc(json);
            }
        } catch (e) {
            //alert('teamStatusCJI error!');
        }
    });
}

//获取坐席组状态
function agentgroupStatusCJI(orgidentity, usertype, user, pwdtype, password, agent_group_id, status, callbackFuc) {
    $.getJSON("http://" + cocc_ip + "/setevent/agentgroupStatusCJI?callback=?", {
        agent_group_id: agent_group_id,
        status: status,
        orgidentity: orgidentity,
        usertype: usertype,
        user: user,
        pwdtype: pwdtype,
        password: password
    }, function (json) {
        try {
            if (typeof (callbackFuc) != 'undefined' && callbackFuc != '') {
                callbackFuc(json);
            }
        } catch (e) {
            //alert('agentgroupStatusCJI error!');
        }
    });
}

//获取坐席状态接口
function agentStatusCJI(orgidentity, usertype, user, pwdtype, password, callbackFuc) {
    $.getJSON("http://" + cocc_ip + "/setevent/agentStatusCJI?callback=?", {
        orgidentity: orgidentity,
        usertype: usertype,
        user: user,
        pwdtype: pwdtype,
        password: password
    }, function (json) {
        try {
            if (typeof (callbackFuc) != 'undefined' && callbackFuc != '') {
                callbackFuc(json);
            }
        } catch (e) {
            //alert('agentStatusCJI error!');
        }
    });
}

//预拨号接口
function dialerListCJI(orgidentity, usertype, user, pwdtype, password, campaignid, phonenum, priority, dialtime, callbackFuc) {
    $.getJSON("http://" + cocc_ip + "/setevent/dialerListCJI?callback=?", {
        orgidentity: orgidentity,
        usertype: usertype,
        user: user,
        pwdtype: pwdtype,
        password: password,
        campaignid: campaignid,
        phonenum: phonenum,
        priority: priority,
        dialtime: dialtime
    }, function (json) {
        try {
            if (typeof (callbackFuc) != 'undefined' && callbackFuc != '') {
                callbackFuc(json);
            }
        } catch (e) {
            //alert('agentStatusCJI error!');
        }
    });
}

//数据导入接口
function importCJI(orgidentity, usertype, user, pwdtype, password, modeltype, model_id, source, context, source_user, source_pwd, exetime, delrow, phone_field, priority_field, dialtime_field, emptyagent, resetstatus, dupway, dupdiallist, changepackage, callbackFuc) {
    $.getJSON("http://" + cocc_ip + "/setevent/importCJI?callback=?", {
        orgidentity: orgidentity,
        usertype: usertype,
        user: user,
        pwdtype: pwdtype,
        password: password,
        modeltype: modeltype,
        model_id: model_id,
        source: source,
        context: context,
        source_user: source_user,
        source_pwd: source_pwd,
        exetime: exetime,
        delrow: delrow,
        phone_field: phone_field,
        priority_field: priority_field,
        dialtime_field: dialtime_field,
        emptyagent: emptyagent,
        resetstatus: resetstatus,
        dupway: dupway,
        dupdiallist: dupdiallist,
        changepackage: changepackage
    }, function (json) {
        try {
            if (typeof (callbackFuc) != 'undefined' && callbackFuc != '') {
                callbackFuc(json);
            }
        } catch (e) {
            //alert('agentStatusCJI error!');
        }
    });
}

//获取录音存放地址
function getMonitorCJI(sessionid, calldate, callbackFuc) {
    $.getJSON("http://" + cocc_ip + "/setevent/getMonitorCJI?callback=?", {
        sessionid: sessionid,
        calldate: calldate
    }, function (json) {
        try {
            if (typeof (callbackFuc) != 'undefined' && callbackFuc != '') {
                callbackFuc(json);
            }
        } catch (e) {
            //alert('getMonitorCJI error!');
        }
    });
}

//队列中客户数量
function queueCustomerNumCJI(orgidentity, queuenumber, prio, callbackFuc) {
    $.getJSON("http://" + cocc_ip + "/setevent/queueCustomerNumCJI?callback=?", {
        orgidentity: orgidentity,
        queuenumber: queuenumber,
        prio: prio
    }, function (json) {
        try {
            if (typeof (callbackFuc) != 'undefined' && callbackFuc != '') {
                callbackFuc(json);
            }
        } catch (e) {
            //alert('queueCustomerNumCJI error!');
        }
    });
}

//获取单一坐席实时数据
function agentRealtimeCJI(orgidentity, usertype, user, pwdtype, password, callbackFuc) {
    $.getJSON("http://" + cocc_ip + "/setevent/agentRealtimeCJI?callback=?", {
        orgidentity: orgidentity,
        usertype: usertype,
        user: user,
        pwdtype: pwdtype,
        password: password
    }, function (json) {
        try {
            if (typeof (callbackFuc) != 'undefined' && callbackFuc != '') {
                callbackFuc(json);
            }
        } catch (e) {
            //alert('agentRealtimeCJI error!');
        }
    });
}

//发送DTMF
function dtmfCJI(orgidentity, usertype, user, pwdtype, password, dtmf, callbackFuc) {
    $.getJSON("http://" + cocc_ip + "/setevent/dtmfCJI?callback=?", {
        orgidentity: orgidentity,
        usertype: usertype,
        user: user,
        pwdtype: pwdtype,
        password: password,
        dtmf: dtmf
    }, function (json) {
        try {
            if (typeof (callbackFuc) != 'undefined' && callbackFuc != '') {
                callbackFuc(json);
            }
        } catch (e) {
            //alert('dtmfCJI error!');
        }
    });
}

//设置随路数据
function setvarCJI(orgidentity, usertype, user, pwdtype, password, varname, varvalue, callbackFuc) {
    $.getJSON("http://" + cocc_ip + "/setevent/setvarCJI?callback=?", {
        orgidentity: orgidentity,
        usertype: usertype,
        user: user,
        pwdtype: pwdtype,
        password: password,
        varname: varname,
        varvalue: varvalue
    }, function (json) {
        try {
            if (typeof (callbackFuc) != 'undefined' && callbackFuc != '') {
                callbackFuc(json);
            }
        } catch (e) {
            //alert('setvarCJI error!');
        }
    });
}

//坐席转IVR
function agenttoivrCJI(orgidentity, usertype, user, pwdtype, password, ivrexten, ivrflow, transfer, callbackFuc) {
    $.getJSON("http://" + cocc_ip + "/setevent/agenttoivrCJI?callback=?", {
        orgidentity: orgidentity,
        usertype: usertype,
        user: user,
        pwdtype: pwdtype,
        password: password,
        ivrexten: ivrexten,
        ivrflow: ivrflow,
        transfer: transfer
    }, function (json) {
        try {
            if (typeof (callbackFuc) != 'undefined' && callbackFuc != '') {
                callbackFuc(json);
            }
        } catch (e) {
            //alert('setvarCJI error!');
        }
    });
}


//分机拨号
function devicecallCJI(orgidentity, exten, targetdn, callerid, callbackFuc) {
    $.getJSON("http://" + cocc_ip + "/setevent/devicecallCJI?callback=?", {
        orgidentity: orgidentity,
        exten: exten,
        targetdn: targetdn,
        callerid: callerid
    }, function (json) {
        try {
            if (typeof (callbackFuc) != 'undefined' && callbackFuc != '') {
                callbackFuc(json);
            }
        } catch (e) {
            //alert('devicecallCJI error!');
        }
    });
}


//设置分机
function setdeviceCJI(orgidentity, exten, user, pwdtype, password, callbackFuc) {
    $.getJSON("http://" + cocc_ip + "/setevent/setdeviceCJI?callback=?", {
        orgidentity: orgidentity,
        exten: exten,
        user: user,
        pwdtype: pwdtype,
        password: password
    }, function (json) {
        try {
            if (typeof (callbackFuc) != 'undefined' && callbackFuc != '') {
                callbackFuc(json);
            }
        } catch (e) {
            //alert('setdeviceCJI error!');
        }
    });
}
