<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MobileFormComment.aspx.cs" Inherits="InnerPages_MobileFormSubmit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <JQMobileTools:JQScriptManager runat="server" ID="JQScriptManager" JQueryMobileVersion="1.4.2" SubFolder="false" LocalScript="true" />
        <div id="MobileFormComment" data-role="page" data-theme="b" class="info-flow">
            <div data-role="header" data-theme="b">
                <h1>EEP2012</h1>
                <a class="refresh" data-mini="true" data-inline="true" data-icon="refresh" data-role="button"
                    data-iconpos="notext" data-theme="b">Refresh</a>
                <a class="goBack" data-mini="true"
                    data-inline="true" data-icon="back" data-role="button" data-iconpos="notext" href="#" role="button"
                    data-theme="b" onclick="commentGoBack()">goBack</a>
            </div>
            <div data-role="content">
                <ul id="ulComment" data-role="listview" data-inset="true">
                </ul>
            </div>
        </div>
    </form>
</body>
</html>
