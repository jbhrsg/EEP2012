<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MobileMainFlowPageNotify.aspx.cs" Inherits="MobileMainFlowPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head runat="server">
    <title></title>
   <%-- <meta name="viewport" content="initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <link href="jquery.mobile-1.3.2/jquery.mobile-1.3.2.min.css" rel="stylesheet" />
    <link href="js/themes/infolight.mobile.css" rel="stylesheet" />
    <link href="js/jquery.mobile-modify.css" rel="stylesheet" />
    <script type="text/javascript" src="js/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="js/jquery.json.js"></script>
    <script type="text/javascript" src="jquery.mobile-1.3.2/jquery.mobile-1.3.2.min.js"></script>
    <script type="text/javascript" src="js/jquery.infolight.mobile.js"></script>
    <script type="text/javascript" src="js/jquery.infolight.mobile-wf.js"></script>
    <script type="text/javascript" src="MobileMainFlowPage.js"></script>
    <link href="css/jquery.swipeButton.css" rel="stylesheet" />
    <script type="text/javascript" src="js/plugins/jquery.swipeButton.js"></script>--%>
</head>
<body>
    <form id="form1" runat="server">
        <JQMobileTools:JQScriptManager runat="server" ID="JQScriptManager" SubFolder="false" LocalScript="true"/>
        <div id="pNotify" data-role="page" data-theme="b">
            <div data-role="header" data-theme="b">
                <fieldset class="ui-grid-c">
                    <div class="ui-block-a">
                        <a id="aInbox_Notify" data-role="button" data-theme="b" onclick="gotoInbox()">待办事项</a>
                    </div>
                    <div class="ui-block-b">
                        <a id="aOutbox_Notify" data-role="button" data-theme="b" onclick="gotoOutbox()">经办事项</a>
                    </div>
                    <div class="ui-block-c">
                        <a id="aNotify_Notify" data-role="button" data-theme="b" onclick="gotoNotify()">通知</a>
                    </div>
                    <div class="ui-block-d">
                        <a id="aDelay_Notify" data-role="button" data-theme="b" onclick="gotoDelay()">逾时事项</a>
                    </div>
                </fieldset>
            </div>
            <div id="divNotify_Notify" class="info-flow" data-role="content">
                <ul id="ulNotify_Notify" data-role="listview">
                    <%--<li id="ulNotifyTitle" data-role="list-divider">通知<span class="ui-li-count">2</span></li>
                                    <li><a href="index.html">
                    <h2>jQuery Team</h2>
                    <p><strong>Boston Conference Planning</strong></p>
                    <p>In preparation for the upcoming conference in Boston, we need to start gathering a list of sponsors and speakers.</p>
                    <p class="ui-li-aside"><strong>9:18</strong>AM</p>
                </a></li>--%>
                </ul>
                 <div data-role="popup" id="popupMenu_Notify" data-theme="d">
                    <ul data-role="listview" data-inset="true" style="min-width: 120px;" data-theme="b">
                    <li><a class="open" href="#" rel='external'>Open</a></li>
                    <li><a class="delete" href="#">Delete</a></li>
                </ul>
                </div>
            </div>
            <div data-role="footer" data-theme="b" class="transparent-bg">
                <fieldset class="ui-grid-a">
                    <div class="ui-block-a">
                        <a id="aMenu_Notify" class="menu" data-mini="true" data-role="button" data-theme="b" onclick="gotoMenu()">菜单</a>
                    </div>
                    <div class="ui-block-b">
                        <a id="aWorkFlow_Notify" class="flow" data-mini="true" data-role="button" data-theme="b" onclick="gotoInbox()">個人工作</a>
                    </div>
                </fieldset>
            </div>
        </div>
    </form>
</body>
</html>
