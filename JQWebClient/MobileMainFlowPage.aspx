<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MobileMainFlowPage.aspx.cs"
    Inherits="MobileMainFlowPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <JQMobileTools:JQScriptManager runat="server" ID="JQScriptManager" JQueryMobileVersion="1.4.2"
            SubFolder="false" LocalScript="true" />
        <div data-role="page" data-theme="b" class="indexbg">
            <div data-role="header" data-theme="b">
                <h1>EEP2012</h1>
                <a class="refresh" data-mini="true" data-inline="true" data-icon="home" data-role="button"
                    data-iconpos="notext" data-theme="b" onclick="gotoMenu();">Refresh</a> <a class="logout" data-mini="true"
                        data-inline="true" data-icon="back" data-role="button" data-iconpos="notext"
                        data-theme="b" onclick="javascript:$('.info-main').main('logout');">Logout</a>
            </div>
            <div class="info-flow" data-role="content">
                <div data-role="tabs" id="tabs">
                    <div data-role="navbar">
                        <ul>
                            <li><a id="titleInbox_Inbox" href="#divInbox_Inbox" data-ajax="false">待办事项</a></li>
                            <li><a id="titleOutbox_Outbox" href="#divOutbox_Outbox" data-ajax="false">经办事项</a></li>
                            <li><a id="titleNotify_Notify" href="#divNotify_Notify" data-ajax="false">通知</a></li>
                            <li><a id="titleDelay_Delay" href="#divDelay_Delay" data-ajax="false">逾时事项</a></li>
                        </ul>
                    </div>
                    <div id="divInbox_Inbox">
                        <fieldset class="ui-grid-a">
                            <div class="ui-block-a">
                                <select data-mini="true" name="ddlToDoListFilter_Inbox" id="ddlToDoListFilter_Inbox"
                                    data-native-menu="false">
                                </select>
                            </div>
                            <div class="ui-block-b">
                                <fieldset class="ui-grid-a">
                                    <div class="ui-block-a">
                                        <input type="text" name="tbQuery_Inbox" id="tbQuery_Inbox" placeholder="单据查询" value="" data-mini="true" />
                                    </div>
                                    <div class="ui-block-b">
                                        <a data-mini="true" id="btnRefresh_Inbox" data-icon="search" data-role="button" data-theme="b" style="display: block"
                                            data-inline="true" onclick="btnRefresh_InboxClick()">Refresh</a>
                                    </div>
                                </fieldset>
                                <%--<a data-mini="true" id="btnRefresh_Inbox" data-role="button" data-theme="b" onclick="btnRefresh_InboxClick()">Refresh</a>--%>
                            </div>
                        </fieldset>
                        <ul id="ulInbox_Inbox" data-role="listview" data-inset="true">
                            <li><a href="#">In</a></li>
                        </ul>
                        <fieldset class="ui-grid-a">
                            <div class="ui-block-a" style="text-align: center">
                                <a class="grid-previous" data-mini="true" data-inline="true" data-role="button" data-icon="arrow-l" data-iconpos="notext">Previous page</a>
                            </div>
                            <div class="ui-block-b" style="text-align: center">
                                <a class="grid-next" data-mini="true" data-inline="true" data-role="button" data-icon="arrow-r" data-iconpos="notext">Next page</a>
                            </div>
                        </fieldset>
                        <div data-role="popup" id="popupMenu_Inbox" data-theme="d" data-history="false">
                            <ul data-role="listview" data-inset="true" style="min-width: 120px;" data-theme="b">
                                <li><a class="open" href="#" rel='external'>Open</a></li>
                                <li><a class="approve" href="#">Approve</a></li>
                                <li><a class="return" href="#">Return</a></li>
                            </ul>
                        </div>
                    </div>
                    <div id="divOutbox_Outbox">
                        <fieldset class="ui-grid-a">
                            <div class="ui-block-a">
                                <fieldset class="ui-grid-a">
                                    <div class="ui-block-a">
                                        <select data-mini="true" name="ddlToDoHisFilter_Outbox" id="ddlToDoHisFilter_Outbox"
                                            data-native-menu="false">
                                        </select>
                                    </div>
                                    <div class="ui-block-b">
                                        <label for="cbSubmitted_Outbox">
                                            已结案</label>
                                        <input type="checkbox" data-mini="true" name="cbSubmitted_Outbox" id="cbSubmitted_Outbox"
                                            onchange="cbSubmitted_OutboxChange()" />
                                    </div>
                                </fieldset>
                            </div>
                            <div class="ui-block-b">
                                <fieldset class="ui-grid-a">
                                    <div class="ui-block-a">
                                        <input type="text" name="tbQuery_Outbox" id="tbQuery_Outbox" placeholder="单据查询" value="" data-mini="true" />
                                    </div>
                                    <div class="ui-block-b">
                                        <a data-mini="true" id="btnRefresh_Outbox" data-icon="search" data-role="button" style="display: block"
                                            data-theme="b" data-inline="true" onclick="btnRefresh_OutboxClick()">Refresh</a>
                                    </div>
                                </fieldset>
                            </div>
                        </fieldset>
                        <ul id="ulOutbox_Outbox" data-role="listview" data-inset="true">
                            <li><a href="#">Out</a></li>
                        </ul>
                        <fieldset class="ui-grid-a">
                            <div class="ui-block-a" style="text-align: center">
                                <a class="grid-previous" data-mini="true" data-inline="true" data-role="button" data-icon="arrow-l" data-iconpos="notext">Previous page</a>
                            </div>
                            <div class="ui-block-b" style="text-align: center">
                                <a class="grid-next" data-mini="true" data-inline="true" data-role="button" data-icon="arrow-r" data-iconpos="notext">Next page</a>
                            </div>
                        </fieldset>
                        <div data-role="popup" id="popupMenu_Outbox" data-theme="d" data-history="false">
                            <ul data-role="listview" data-inset="true" style="min-width: 120px;" data-theme="b">
                                <li><a class="open" href="#" rel='external'>Open</a></li>
                                <li><a class="retake" href="#">Retake</a></li>
                                <li><a class="hasten" href="#">Hasten</a></li>
                            </ul>
                        </div>
                    </div>
                    <div id="divNotify_Notify">
                        <ul id="ulNotify_Notify" data-role="listview" data-inset="true">
                            <li><a href="#">Notify</a></li>
                        </ul>
                        <fieldset class="ui-grid-a">
                            <div class="ui-block-a" style="text-align: center">
                                <a class="grid-previous" data-mini="true" data-inline="true" data-role="button" data-icon="arrow-l" data-iconpos="notext">Previous page</a>
                            </div>
                            <div class="ui-block-b" style="text-align: center">
                                <a class="grid-next" data-mini="true" data-inline="true" data-role="button" data-icon="arrow-r" data-iconpos="notext">Next page</a>
                            </div>
                        </fieldset>
                        <div data-role="popup" id="popupMenu_Notify" data-theme="d" data-history="false">
                            <ul data-role="listview" data-inset="true" style="min-width: 120px;" data-theme="b">
                                <li><a class="open" href="#" rel='external'>Open</a></li>
                                <li><a class="delete" href="#">Delete</a></li>
                            </ul>
                        </div>
                    </div>
                    <div id="divDelay_Delay">
                        <fieldset class="ui-grid-a">
                            <%--<label data-mini="true" id="lOvertimeColumn_Delay" for="fs">Basic:</label>--%>
                            <div class="ui-block-a">
                                <select data-mini="true" name="sOvertimeColumn_Delay" id="sOvertimeColumn_Delay"
                                    data-native-menu="false">
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
                        <ul id="ulDelay_Delay" data-role="listview" data-inset="true">
                            <li><a href="#">Delay</a></li>
                        </ul>
                        <fieldset class="ui-grid-a">
                            <div class="ui-block-a" style="text-align: center">
                                <a class="grid-previous" data-mini="true" data-inline="true" data-role="button" data-icon="arrow-l" data-iconpos="notext">Previous page</a>
                            </div>
                            <div class="ui-block-b" style="text-align: center">
                                <a class="grid-next" data-mini="true" data-inline="true" data-role="button" data-icon="arrow-r" data-iconpos="notext">Next page</a>
                            </div>
                        </fieldset>
                        <div data-role="popup" id="popupMenu_Delay" data-theme="d" data-history="false">
                            <ul data-role="listview" data-inset="true" style="min-width: 120px;" data-theme="b">
                                <li><a class="open" href="#" rel='external'>Open</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <div data-role="footer" data-theme="b" class="transparent-bg">
                <fieldset class="ui-grid-a">
                    <div class="ui-block-a">
                        <a class="menu" data-mini="true" data-role="button" data-theme="b" onclick="gotoMenu()">Menu</a>
                    </div>
                    <div class="ui-block-b">
                        <a class="flow" data-mini="true" data-role="button" data-theme="b" onclick="gotoInbox()">Flow</a>
                    </div>
                </fieldset>
            </div>
        </div>
    </form>
</body>
</html>
