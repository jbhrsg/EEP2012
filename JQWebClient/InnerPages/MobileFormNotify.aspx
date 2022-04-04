<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MobileFormNotify.aspx.cs" Inherits="InnerPages_MobileFormSubmit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="jquery.mobile-1.3.2/jquery.mobile-1.3.2.min.css" rel="stylesheet" />
    <link href="../js/themes/infolight.mobile.css" rel="stylesheet" />
    <link href="../js/jquery.mobile-modify.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../js/jquery.json.js"></script>
    <script type="text/javascript" src="jquery.mobile-1.3.2/jquery.mobile-1.3.2.min.js"></script>
    <script type="text/javascript" src="../js/jquery.infolight.mobile.js"></script>
    <script type="text/javascript" src="../js/jquery.infolight.mobile-wf.js"></script>
    <script type="text/javascript" src="../MobileMainFlowPage.js"></script>--%>
</head>
<body>
    <form id="form1" runat="server">
        <JQMobileTools:JQScriptManager runat="server" ID="JQScriptManager"  LocalScript="true"/>
        <div id="MobileFormNotify" class="info-flow" data-overlay-theme="b" data-role="page">
            <div data-theme="b" data-role="content" style="margin: 0 auto;min-width: 100px; max-width: 500px">
                <div data-role="collapsible" data-theme="b" data-content-theme="c">
                    <h2 id="h2User_FromNotify">Choose a user...</h2>
                    <ul id="ulUsers_FormNotify" data-role="listview" data-filter="true" data-inset="true">
                        <%--                <li><a><input type="checkbox" id="checkbox-0">0</a></li>
                <li><a><input type="checkbox" id="checkbox-1">1</a></li>
                <li><a><input type="checkbox" id="checkbox-2">2</a></li>
                <li><a><input type="checkbox" id="checkbox-3">3</a></li>
                <li><a><input type="checkbox" id="checkbox-4">4</a></li>
                <li><a><input type="checkbox" id="checkbox-5">5</a></li>
                <li><a><input type="checkbox" id="checkbox-6">6</a></li>
                <li><a><input type="checkbox" id="checkbox-7">7</a></li>
                <li><a><input type="checkbox" id="checkbox-8">8</a></li>
                <li><a><input type="checkbox" id="checkbox-9">9</a></li>--%>
                    </ul>
                </div>
                <div data-role="collapsible" data-theme="b" data-content-theme="c">
                    <h2 id="h2Group_FromNotify">Choose a role...</h2>
                    <ul id="ulRoles_FormNotify" data-role="listview" data-filter="true" data-inset="true">
                    </ul>
                </div>
                <textarea data-mini="true" cols="40" rows="15" name="textarea-1" id="txtMessage_FormNotify"></textarea>
                <label id="result_FormNotify" style="color: red"></label>
                <fieldset class="ui-grid-a" data-theme="b">
                    <div class="ui-block-a">
                        <a data-mini="true" id="btnOk_FromNotify" data-role="button" data-theme="b" onclick="btnOk_FromNotifyClick()">OK</a>
                    </div>
                    <div class="ui-block-b">
                        <a data-mini="true" id="btnCancel_FromNotify" data-role="button" data-theme="b" onclick="commentGoBack()">Close</a>
                    </div>
                </fieldset>
            </div>
        </div>
    </form>
</body>
</html>
