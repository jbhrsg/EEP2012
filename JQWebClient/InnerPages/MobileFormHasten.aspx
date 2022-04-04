<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MobileFormHasten.aspx.cs" Inherits="InnerPages_MobileFormSubmit" %>

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
        <div id="MobileFormHasten" class="info-flow" data-overlay-theme="b" data-role="page">
            <div data-theme="b" data-role="content" style="margin: 0 auto;min-width: 100px; max-width: 500px">
                <label data-mini="true" id="label_FormHasten" for="txtMessage_FormHasten">
                    通知讯息1
                </label>
                <textarea data-mini="true" cols="40" rows="15" name="textarea-1" id="txtMessage_FormHasten"></textarea>
                <label id="result_FormHasten" style="color: red"></label>
                <fieldset class="ui-grid-a" data-theme="b">
                    <div class="ui-block-a">
                        <a data-mini="true" id="btnOk_FormHasten" data-role="button" data-theme="b" onclick="btnOk_FormHastenClick()">OK</a>
                    </div>
                    <div class="ui-block-b">
                        <a data-mini="true" id="btnCancel_FormHasten" data-role="button" data-theme="b" data-rel="back">Close</a>
                    </div>
                </fieldset>
            </div>
        </div>
    </form>
</body>
</html>
