<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MobileMainFlowPageDelay.aspx.cs" Inherits="MobileMainFlowPage" %>

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
        <fieldset id="pDelay" data-role="page" data-theme="b" class="info-flow">
            <div data-role="header" data-theme="b">
                <fieldset class="ui-grid-c">
                    <div class="ui-block-a">
                        <a id="aInbox_Delay" data-role="button" data-theme="b" onclick="gotoInbox()">待办事项</a>
                    </div>
                    <div class="ui-block-b">
                        <a id="aOutbox_Delay" data-role="button" data-theme="b" onclick="gotoOutbox()">经办事项</a>
                    </div>
                    <div class="ui-block-c">
                        <a id="aNotify_Delay" data-role="button" data-theme="b" onclick="gotoNotify()">通知</a>
                    </div>
                    <div class="ui-block-d">
                        <a id="aDelay_Delay" data-role="button" data-theme="b" onclick="gotoDelay()">逾时事项</a>
                    </div>
                </fieldset>
                <fieldset class="ui-grid-a">
                    <%--<label data-mini="true" id="lOvertimeColumn_Delay" for="fs">Basic:</label>--%>
                    <div class="ui-block-a">
                        <select data-mini="true" name="sOvertimeColumn_Delay" id="sOvertimeColumn_Delay" data-native-menu="false">
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
                    </div>
                    <div class="ui-block-b">
                        <a data-mini="true" id="btnRefresh_Delay" data-role="button" data-theme="b" onclick="btnRefresh_DelayClick()">Refresh</a>
                    </div>
                </fieldset>
            </div>
            <div id="divDelay_Delay" class="info-flow" data-role="content">
                <ul id="ulDelay_Delay" data-role="listview">
                    <%--<li id="ulDelayTitle" data-role="list-divider">逾时<span class="ui-li-count">2</span></li>
                                    <li><a href="index.html">
                    <h2>jQuery Team</h2>
                    <p><strong>Boston Conference Planning</strong></p>
                    <p>In preparation for the upcoming conference in Boston, we need to start gathering a list of sponsors and speakers.</p>
                    <p class="ui-li-aside"><strong>9:18</strong>AM</p>
                </a></li>--%>
                </ul>
                <div data-role="popup" id="popupMenu_Delay" data-theme="d">
                <ul data-role="listview" data-inset="true" style="min-width: 120px;" data-theme="b">
                    <li><a class="open" href="#" rel='external'>Open</a></li>
                </ul>
            </div>
            </div>
            <div data-role="footer" data-theme="b" class="transparent-bg">
                <fieldset class="ui-grid-a">
                    <div class="ui-block-a">
                        <a id="aMenu_Delay" class="menu" data-mini="true" data-role="button" data-theme="b" onclick="gotoMenu()">菜单</a>
                    </div>
                    <div class="ui-block-b">
                        <a id="aWorkFlow_Delay" class="flow" data-mini="true" data-role="button" data-theme="b" onclick="gotoInbox()">個人工作</a>
                    </div>
                </fieldset>
            </div>
        </div>
    </form>
</body>
</html>
