<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ToOtherSystem.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>填寫專案評鑑表</title>
    <script>        
        //=======================================【Ready】=========================================
        $(function () {
            if (parent != window) {
                $.ajax({
                    type: "post",
                    data: "Method=GetHref",
                    cache: false,
                    async: true,
                    success: function (jsonStr) {
                        var json = $.parseJSON(jsonStr);
                        if (json.IsOK) parent.location.href = json.TheResult;
                        else $.messager.alert('訊息', json.Msg, 'error');
                    },
                    error: function (data) { $.messager.alert('訊息', '執行錯誤', 'error'); }
                });
            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
    </form>
</body>
</html>
