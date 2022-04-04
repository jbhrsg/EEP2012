<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MobileFlowPreview.aspx.cs" Inherits="InnerPages_ChangePassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <JQMobileTools:JQScriptManager ID="JQScriptManager1" runat="server" />
    <div id="MobileFlowPreview" class="info-flow" data-overlay-theme="b" data-role="page">
        <div id="info" data-role="content" data-theme="b" class="indexbg ui-page ui-page-theme-b ui-page-active" data-url="MobileMainBackground" tabindex="0" style="min-height: 958px;">
            <a data-mini="true" data-inline="true" data-icon="back" data-role="button" data-iconpos="notext" data-theme="b" data-rel="back">Back</a>
            <form class="ui-filterable">
                <div style="overflow-x: scroll; overflow-y: hidden">
                    <img id="imgPreview" />
                </div>
            </form>
        </div>
    </div>
</body>
</html>
