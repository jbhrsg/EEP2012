<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FlowEmail.aspx.cs" Inherits="FlowEmail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="js/themes/default/easyui.css" rel="stylesheet" />
    <link href="js/themes/icon.css" rel="stylesheet" />
    <script src="js/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="js/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="js/jquery.json.js" type="text/javascript"></script>
    <script src="js/jquery.infolight.js" type="text/javascript"></script>
    <script src="js/jquery.infolight-wf.js" type="text/javascript"></script>
    <script src="MainPage.js" type="text/javascript"></script>
    <script src="MainPage_Flow.js" type="text/javascript"></script>
    <script src="WorkflowBoot.js" type="text/javascript"></script>
    <script>
        $(document).ready(function () {
            var param = $.parseJSON($('#parameter').val());
            var type = param.type;
            var listID = param.listID;
            var flowPath = param.flowPath;
            $.ajax({
                type: "POST",
                dataType: 'json',
                url: 'handler/SystemHandle_Flow.ashx',
                data: { listID: listID, Type: 'ToDoList' },
                cache: false,
                async: true,
                success: function (data) {
                    for (var i = 0; i < data.rows.length; i++) {
                        if (data.rows[i].FLOWPATH == flowPath) {
                            if (type == "reject") {
                                doReject('', undefined, '', data.rows[i], true);
                            }
                            else {
                                var winTitle = $.sysmsg('getValue', 'SLTools/SLMainFlowPage2/GridButton');
                                var winTitles = winTitle.split(';');;
                                var title = winTitles[1];
                                var href = 'InnerPages/FormApprove.html';
                                if (type == 'return') {
                                    href = 'InnerPages/FormReturn.html';
                                    title = winTitles[2];
                                }

                                $('#panel').panel({
                                    title: title,
                                    href: href,
                                    width: 550,
                                    onLoad: function () {
                                        $('#btnCancel_FromApprove, #btnCancel_FromReturn').unbind().bind('click', function () {
                                            window.close();
                                        });
                                        if (type == 'return') {
                                            formReturnLoaded(data.rows[i], '', '');
                                        }
                                        else {
                                            formApproveLoaded(data.rows[i], '', '');
                                        }
                                    }
                                });
                            }
                            return;
                        }
                    }

                    var msg = $.sysmsg('getValue', 'FLRuntime/FLInstance/FLSetpIsApprovedOrReturned');
                    $('<p style="color:#ff0000">' + msg + '</p>').appendTo('#panel');

                }
            });



        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="panel" class="easyui-panel" style="padding: 10px; background: #fafafa;">
        </div>
        <asp:HiddenField runat="server" ID="parameter" />
    </form>
</body>
</html>
