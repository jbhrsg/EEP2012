<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MobileFormApprove.aspx.cs" Inherits="InnerPages_MobileFormSubmit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"> 
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
   <%-- <link href="jquery.mobile-1.3.2/jquery.mobile-1.3.2.min.css" rel="stylesheet" />
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
    <JQMobileTools:JQScriptManager runat="server" ID="JQScriptManager"  LocalScript="true"/>
    <div id="MobileFormApprove" class="info-flow" data-overlay-theme="b" data-role="page">
        <div data-role="header" data-theme="b">
            <h1 id="hHead_MobileFormApprove">Approve
            </h1>
        </div>
        <div data-theme="b" data-role="content" style="margin: 0 auto; min-width: 100px; max-width: 500px; height: 100px">
            <%--                <div class="ui-content" id="MobileFormApprove_popup" data-role="popup" data-theme="d" data-overlay-theme="a">
                    <a data-role="button" data-icon="delete" data-iconpos="notext" class="ui-btn-right" onclick="alert('close');">Close1</a><p></p>
                </div>--%>
            <table class="table1" style="width: 100%">
                <tr>
                    <td class="td1">
                        <fieldset class="ui-grid-a" data-theme="b">
                            <div class="ui-block-a">
                                <input data-mini="true" type="checkbox" name="chkImportant_FromApprove" id="chkImportant_FromApprove" />
                                <label data-mini="true" id="lImportant_FromApprove" for="chkImportant_FromApprove">Important</label>
                            </div>
                            <div class="ui-block-b">
                                <input data-mini="true" type="checkbox" name="chkUrgent_FromApprove" id="chkUrgent_FromApprove" />
                                <label data-mini="true" id="lUrgent_FromApprove" for="chkUrgent_FromApprove">Urgent</label>
                            </div>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td class="td1">
                        <label class="labelWhite" data-mini="true" id="suggestionView_FromApprove" for="txtSuggest_FromApprove">意见说明:</label>
                        <textarea data-mini="true" cols="40" rows="15" name="textarea-1" id="txtSuggest_FromApprove"></textarea>
                    </td>
                </tr>
                <tr>
                    <td class="td1">
                        <fieldset class="ui-grid-a" data-theme="b">
                            <div class="ui-block-a">
                                <label class="labelWhite" data-mini="true" id="lSenderRole_FromApprove" for="ddlRoles_FromApprove">Basic:</label>
                            </div>
                            <div class="ui-block-b">
                                <select data-mini="true" name="ddlRoles_FromApprove" id="ddlRoles_FromApprove" data-native-menu="false">
                                </select>
                            </div>
                        </fieldset>
<%--                        <div data-role="fieldcontain">
                        </div>--%>
                    </td>
                </tr>
                <tr>
                    <td class="td1">
                        <label class="labelWhite" data-mini="true" id="lhistoryView_FromApprove" for="historyView_FromApprove">已批示:</label>
                        <div id="historyView_FromApprove">
                            <%--                    <ul id="gdvHis_FromApprove" data-role="listview">
                        <li id="gdvHisTitle_FromApprove" data-role="list-divider">过程意见<span class="ui-li-count">2</span></li>
                    </ul>--%>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="td1">
                        <label id="result_FromApprove" style="color: red"></label>
                    </td>
                </tr>
            </table>

            <div>
                <ul id="ulFiles_FromApprove" class="ulFiles" data-role="listview">
                </ul>
            </div>


            <div data-role="popup" id="popupDialog_FromApprove" data-overlay-theme="a" data-theme="c" data-dismissible="false" style="max-width: 400px;" class="ui-corner-all">
                <div data-role="header" data-theme="a" class="ui-corner-top">
                    <h1 id="hUploadHead_MobileFormApprove">Upload files</h1>
                </div>
                <div data-theme="d" class="ui-corner-bottom ui-content">
                    <input id="FileUpload1_FromApprove" name="FileUpload1_FromApprove" type="file" class="info-upload-button">
                    <input id="FileUpload2_FromApprove" name="FileUpload2_FromApprove" type="file" class="info-upload-button">
                    <input id="FileUpload3_FromApprove" name="FileUpload3_FromApprove" type="file" class="info-upload-button">
                    <input id="FileUpload4_FromApprove" name="FileUpload4_FromApprove" type="file" class="info-upload-button">
                    <input id="FileUpload5_FromApprove" name="FileUpload5_FromApprove" type="file" class="info-upload-button">
                    <div>
                        <ul id="ulUploadFiles_FromApprove" class="ulFiles" data-role="listview">
                        </ul>
                    </div>
                    <a href="#" id="btnUpload_FromApprove" data-role="button" data-inline="true" data-theme="b" onclick="btnUpload_FromApproveClick()">Upload</a>
                    <a href="#" id="btnClose_FromApprove" data-role="button" data-inline="true" data-rel="back" data-theme="c">Close</a>
                </div>
            </div>
        </div>
        <div data-role="footer" data-theme="b">
            <fieldset class="ui-grid-c" data-theme="b">
                <div class="ui-block-a">
                    <a data-mini="true" id="btnOk_FromApprove" data-role="button" data-theme="b" onclick="btnOk_FromApproveClick()">OK</a>
                </div>
                <div class="ui-block-b">
                    <a data-mini="true" id="btnCancel_FromApprove" data-role="button" data-theme="b" onclick="btnCancel_FromApproveClick()">Close</a>
                </div>
                <div class="ui-block-c">
                    <a data-mini="true" id="btnPreview_FromApprove" data-role="button" data-theme="b" onclick="btnPreview_FromApproveClick()">Preview</a>
                </div>
                <div class="ui-block-d">
                    <a data-mini="true" id="btnUploadFile_FromApprove" data-role="button" data-theme="b"
                        href="#popupDialog_FromApprove" data-rel="popup" data-position-to="window" data-transition="pop">Upload File</a>
                </div>
            </fieldset>
        </div>
    </div>
</body>
</html>
