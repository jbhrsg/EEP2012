<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkflowPage.aspx.cs" Inherits="WorkflowPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link id="easyuiTheme" href="../js/themes/default/easyui.css" rel="stylesheet" />
    <link href="../js/themes/icon.css" rel="stylesheet" />
    <script src="../js/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../js/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../js/jquery.json.js" type="text/javascript"></script>
    <script src="../js/jquery.infolight.js" type="text/javascript"></script>
    <script src="../js/jquery.infolight-wf.js" type="text/javascript"></script>
    <script src="../MainPage.js" type="text/javascript"></script>
    <script src="../MainPage_Flow.js" type="text/javascript"></script>
    <script src="../WorkflowBoot.js" type="text/javascript"></script>
    <script>
        var flowDeleteText = "是否要删除这笔工作流";
        var flowRejectText = "是否要作废这笔工作流";
        $(document).ready(function () {
            //renderMain();
            renderMain_Flow();

            $.ajax({
                type: "POST",
                url: '../handler/SystemHandle.ashx?type=SetTheme',
                cache: false,
                async: false,
                success: function (data) {
                    $.changeTheme(data, "../");
                },
                error: function (data) {
                    //data.responseText = '';
                    //obj = "[{\"" + textField + "\":\"\"}]";
                }
            });

            //$(window).bind('beforeunload', function (e) {
            //    var x = window.event.clientX;
            //    var y = window.event.clientY;
            //    //alert(window.outerWidth + ":" + x);
            //    if (y < 0) {
            //        logout();
            //    }
            //});
        });

        function setlanguage(currentLang) {
            $.sysmsg('getValues', [
                'JQWebClient/mainpagelinkbutton'
                , 'Srvtools/AnyQuery/DeleteSure'
                , 'FLClientControls/FLNavigator/NavText'
                , 'FLClientControls/FLNavigator/FlowRejectConfirm'
                , 'FLClientControls/FLNavigator/FlowPauseConfirm'
            ]);
            var localstring = $.sysmsg('getValue', 'JQWebClient/mainpagelinkbutton');
            var local = localstring.split(',');
            var homeloacl = local[0];
            var menuslocal = local[1];
            var aboutlocal = local[2];
            var logoutlocal = local[3];
            var changePasswordlocal = local[4];
            $('#homeMenuButton').text(homeloacl);
            $('#btn-mmMenus').text(menuslocal);
            $('#aboutMenuButton').text(aboutlocal);
            $('#aboutMenuButton').text(aboutlocal);
            $('#logOutMenuButton').text(logoutlocal);
            $('#changePasswordMenuButton').text(changePasswordlocal);
            //var oid = $(this).attr("id");
            //var buckle = $(this).children("buckle").text();

            var DeleteSure = $.sysmsg('getValue', 'FLClientControls/FLNavigator/FlowDeleteConfirm');
            var NavText = $.sysmsg('getValue', 'FLClientControls/FLNavigator/NavText');
            var NavTexts = NavText.split(";");
            flowDeleteText = String.format(DeleteSure, NavTexts[20]);
            flowRejectText = $.sysmsg('getValue', 'FLClientControls/FLNavigator/FlowRejectConfirm');
        }
    </script>
</head>
<body>
    <div id="tabsWorkFlow" class="easyui-tabs" data-options="fit:true,plain:true">
        <div title="111">
            <div class="easyui-layout" data-options="fit:true">
                <div data-options="region:'north',split:false,border:false" style="height: 35px">
                    <table style="height: 100%; width: 100%">
                        <tr>
                            <td style="width: 180px">
                                <select id="ddlToDoListFilter" class="easyui-combobox" data-options="valueField:'id',textField:'text'" style="width: 150px;" />
                            </td>
                            <td style="width: 80px">
                                <a id="btnRefresh_Inbox" href="#" class="easyui-linkbutton">Refresh</a>
                            </td>
                            <td style="width: 80px">
                                <a id="btnQuery_Inbox" href="#" class="easyui-linkbutton">Query</a>
                            </td>
                            <td style="width: 120px">
                                <a id="btnApproveAll_Inbox" href="#" class="easyui-linkbutton">Approve All</a>
                            </td>
                            <td style="width: 120px">
                                <a id="btnReturnAll_Inbox" href="#" class="easyui-linkbutton">Return All</a>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </div>
                <div data-options="region:'center',border:false">
                    <table id="dgInbox" class="easyui-datagrid" title="收件代办" style="width: auto" fit="true" pagination="false" rownumbers="true" singleselect="false"></table>
                </div>
            </div>
        </div>
        <div title="222">
            <div class="easyui-layout" data-options="fit:true">
                <div data-options="region:'north',split:false,border:false" style="height: 35px">
                    <table style="height: 100%; width: 100%">
                        <tr>
                            <td style="width: 180px">
                                <select id="ddlToDoHisFilter" class="easyui-combobox" data-options="valueField:'id',textField:'text'" style="width: 150px;" />
                            </td>
                            <td style="width: 80px">
                                <input id="chkSubmitted" type="checkbox" onclick="chkSubmittedChanged(this)" /><label id="lSubmitted">Submitted</label>
                            </td>
                            <td style="width: 80px">
                                <a id="btnRefresh_Outbox" href="#" class="easyui-linkbutton">Refresh</a>
                            </td>
                            <td style="width: 80px">
                                <a id="btnQuery_Outbox" href="#" class="easyui-linkbutton">Query</a>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </div>
                <div data-options="region:'center',border:false">
                    <div id="divOutbox" style="width: 100%; height: 100%;">
                        <table id="dgOutbox" class="easyui-datagrid" title="送件经办" fit="true" pagination="false" rownumbers="true" singleselect="true"></table>
                    </div>
                    <div id="divFlowRunOver" style="width: 100%; height: 100%;">
                        <table id="dgFlowRunOver" class="easyui-datagrid" title="已结案" fit="true" pagination="false" rownumbers="true" singleselect="true"></table>
                    </div>
                </div>
            </div>
        </div>
        <div title="333">
            <div class="easyui-layout" data-options="fit:true">
                <div data-options="region:'north',split:false,border:false" style="height: 35px">
                    <table style="height: 100%; width: 100%">
                        <tr>
                            <td style="width: 80px">
                                <a id="btnRefresh_Notify" href="#" class="easyui-linkbutton">Refresh</a>
                            </td>
                            <td style="width: 120px">
                                <a id="btnDeleteAll_Notify" href="#" class="easyui-linkbutton">Delete All</a>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </div>
                <div data-options="region:'center',border:false">
                    <table id="dgNotify" class="easyui-datagrid" title="通知事项" style="width: auto" fit="true" pagination="false" rownumbers="true" singleselect="true"></table>
                </div>
            </div>
        </div>
        <div title="444">
            <div class="easyui-layout" data-options="fit:true">
                <div data-options="region:'north',split:false,border:false" style="height: 35px">
                    <table style="height: 100%; width: 100%">
                        <tr>
                            <td style="width: 100px">
                                <label id="lOvertimeColumn_Delay"></label>
                            </td>
                            <td style="width: 80px">
                                <select id="sOvertimeColumn_Delay" class="easyui-combobox" style="width: 80px;">
                                    <option value="0">0</option>
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                    <option value="6">6</option>
                                    <option value="7">7</option>
                                    <option value="8">8</option>
                                    <option value="9">9</option>
                                    <option value="10">10</option>
                                </select>
                            </td>
                            <td style="width: 80px">
                                <a id="btnRefresh_Delay" href="#" class="easyui-linkbutton">Refresh</a>
                            </td>
                            <td>
                                <input id="cbNotOvertime_Delay" type="checkbox" onclick="cbNotOvertimeChanged(this)" /><label id="lNotOvertime_Delay">NotOvertime</label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div data-options="region:'center',border:false">
                    <table id="dgDelay" class="easyui-datagrid" title="逾时事项" style="width: auto" fit="true" pagination="false" rownumbers="true" singleselect="true"></table>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
