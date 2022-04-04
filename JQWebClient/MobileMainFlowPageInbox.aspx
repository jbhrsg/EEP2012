<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MobileMainFlowPageInbox.aspx.cs"
    Inherits="MobileMainFlowPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head runat="server">
    <title></title>
<%--    <meta name="viewport" content="initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
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
        <div id="pInbox" data-role="page" data-theme="b" class="info-flow">
            <div data-role="header" data-theme="b">
                <fieldset class="ui-grid-c">
                    <div id="dInbox_Inbox" class="ui-block-a">
                        <a id="aInbox_Inbox" data-role="button" data-theme="b" data-mini="true" onclick="gotoInbox()">待办事项</a>
                    </div>
                    <div class="ui-block-b">
                        <a id="aOutbox_Inbox" data-role="button" data-theme="b" data-mini="true" onclick="gotoOutbox()">经办事项</a>
                    </div>
                    <div class="ui-block-c">
                        <a id="aNotify_Inbox" data-role="button" data-theme="b" data-mini="true" onclick="gotoNotify()">通知</a>
                    </div>
                    <div class="ui-block-d">
                        <a id="aDelay_Inbox" data-role="button" data-theme="b" data-mini="true" onclick="gotoDelay()">逾时事项</a>
                    </div>
                </fieldset>
                <fieldset class="ui-grid-a">
                    <div class="ui-block-a">
                        <select data-mini="true" name="ddlToDoListFilter_Inbox" id="ddlToDoListFilter_Inbox"
                            data-native-menu="false">
                        </select>
                    </div>
                    <div class="ui-block-b">
                        <fieldset class="ui-grid-a">
                            <div class="ui-block-a">
                                <input type="text" name="tbQuery_Inbox" id="tbQuery_Inbox" value="" data-mini="true" />
                            </div>
                            <div class="ui-block-b">
                                <a data-mini="true" id="btnRefresh_Inbox" data-icon="search" data-role="button" data-theme="b" data-inline="true" onclick="btnRefresh_InboxClick()">Refresh</a>
                            </div>
                        </fieldset>
                        <%--<a data-mini="true" id="btnRefresh_Inbox" data-role="button" data-theme="b" onclick="btnRefresh_InboxClick()">Refresh</a>--%>
                    </div>
                </fieldset>
            </div>
            <div id="divInbox_Inbox" class="info-flow" data-role="content">
                <ul id="ulInbox_Inbox" data-role="listview" class="ui-listview-outer">
                    <%--<li id="ulInboxTitle" data-role="list-divider">待办事项<span class="ui-li-count">2</span></li>
                    <li><a href="MobileFooterTemplate.htm">111</a></li>
                    <li><a href="index.html">
                    <h2>jQuery Team</h2>
                    <p><strong>Boston Conference Planning</strong></p>
                    <p>In preparation for the upcoming conference in Boston, we need to start gathering a list of sponsors and speakers.</p>
                    <p class="ui-li-aside"><strong>9:18</strong>AM</p>
                </a></li>--%>
                </ul>
                <div data-role="popup" id="popupMenu_Inbox" data-theme="d">
                    <ul data-role="listview" data-inset="true" style="min-width: 120px;" data-theme="b">
                        <li><a class="open" href="#" rel='external'>Open</a></li>
                        <li><a class="approve" href="#">Approve</a></li>
                        <li><a class="return" href="#">Return</a></li>
                    </ul>
                </div>
            </div>
            <div data-role="footer" data-theme="b" class="transparent-bg">
                <fieldset class="ui-grid-a">
                    <div class="ui-block-a">
                        <a id="aMenu_Inbox" class="menu" data-mini="true" data-role="button" data-theme="b"
                            onclick="gotoMenu()">菜单</a>
                    </div>
                    <div class="ui-block-b">
                        <a id="aWorkFlow_Inbox" class="flow" data-mini="true" data-role="button" data-theme="b"
                            onclick="gotoInbox()">個人工作</a>
                    </div>
                </fieldset>
            </div>
        </div>
    </form>
</body>
</html>
