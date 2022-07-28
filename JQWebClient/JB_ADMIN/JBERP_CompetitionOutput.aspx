<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_CompetitionOutput.aspx.cs" Inherits="JB_ADMIN_JBERP_CompetitionOutput" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="tt" class="easyui-tabs" style="width: 100%; height: 100%;">
                    <div title="Tab1" style="overflow: auto;padding: 20px"><%-- data-options="closable:true" style="overflow: auto; padding: 20px; display: none;"--%>
                        <iframe src='http://localhost:4848/single/?appid=C%3A%5CUsers%5CAllen%5CDocuments%5CQlik%5CSense%5CApps%5C2017competition1.qvf&sheet=yrCWJZ&opt=currsel&select=clearall' style='border:none;width:800px;height:500px;'></iframe>
                    </div>
        </div>
    </div>
    </form>
</body>
</html>
