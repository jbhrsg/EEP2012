<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MobileFormSubmit.aspx.cs" Inherits="InnerPages_MobileFormSubmit" %>

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
        <div id="MobileFormSubmit" class="info-flow" data-overlay-theme="b" data-role="page">
            <div data-role="header" data-theme="b">
                <h1 id="hHead_MobileFromSubmit">Submit
                </h1>
            </div>
            <div data-theme="b" data-role="content" style="margin: 0 auto; min-width: 100px; max-width: 500px">
                <%--                <div class="ui-content" id="MobileFormSubmit_popup" data-role="popup" data-theme="d" data-overlay-theme="a">
                    <a data-role="button" data-icon="delete" data-iconpos="notext" class="ui-btn-right" onclick="alert('close');">Close1</a><p></p>
                </div>--%>
                <table class="table1" style="width: 100%">
                    <tr>
                        <td class="td1">
                            <fieldset class="ui-grid-a" data-theme="b">
                                <div class="ui-block-a">
                                    <input data-mini="true" type="checkbox" name="chkImportant_FromSubmit" id="chkImportant_FromSubmit" />
                                    <label data-mini="true" id="lImportant_FromSubmit" for="chkImportant_FromSubmit">Important</label>
                                </div>
                                <div class="ui-block-b">
                                    <input data-mini="true" type="checkbox" name="chkUrgent_FromSubmit" id="chkUrgent_FromSubmit" />
                                    <label data-mini="true" id="lUrgent_FromSubmit" for="chkUrgent_FromSubmit">Urgent</label>
                                </div>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td class="td1">
                            <label class="labelWhite" data-mini="true" id="suggestionView_FromSubmit" for="txtSuggest_FromSubmit">意见说明:</label>
                            <textarea data-mini="true" cols="40" rows="15" name="textarea-1" id="txtSuggest_FromSubmit"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td class="td1">
                            <fieldset class="ui-grid-a" data-theme="b">
                                <div class="ui-block-a">
                                    <label class="labelWhite" data-mini="true" id="lSenderRole_FromSubmit" for="ddlRoles_FromSubmit">Basic:</label>
                                </div>
                                <div class="ui-block-b">
                                    <select data-mini="true" name="ddlRoles_FromSubmit" id="ddlRoles_FromSubmit" data-native-menu="false"></select>
                                </div>
                            </fieldset>
                            <%--                            <div data-role="fieldcontain">
                                
                            </div>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="td1">
                            <label class="labelWhite" data-mini="true" id="lhistoryView_FromSubmit" for="historyView_FromApprove">已批示:</label>
                            <div id="historyView_FromSubmit">
                                <%--                    <ul id="gdvHis_FromSubmit" data-role="listview">
                        <li id="gdvHisTitle_FromSubmit" data-role="list-divider">过程意见<span class="ui-li-count">2</span></li>
                    </ul>--%>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="td1">
                            <label id="result_FromSubmit" style="color: red"></label>
                        </td>
                    </tr>
                </table>
                <div>
                    <ul id="ulFiles_FromSubmit" class="ulFiles" data-role="listview">
                    </ul>
                </div>

                <div data-role="popup" id="popupDialog_FromSubmit" data-overlay-theme="a" data-theme="c" data-dismissible="false" style="max-width: 400px;" class="ui-corner-all">
                    <div data-role="header" data-theme="a" class="ui-corner-top">
                        <h1 id="hUploadHead_MobileFromSubmit">Upload files</h1>
                    </div>
                    <div data-theme="d" class="ui-corner-bottom ui-content">
                        <input id="FileUpload1_FromSubmit" name="FileUpload1_FromSubmit" type="file" class="info-upload-button">
                        <input id="FileUpload2_FromSubmit" name="FileUpload2_FromSubmit" type="file" class="info-upload-button">
                        <input id="FileUpload3_FromSubmit" name="FileUpload3_FromSubmit" type="file" class="info-upload-button">
                        <input id="FileUpload4_FromSubmit" name="FileUpload4_FromSubmit" type="file" class="info-upload-button">
                        <input id="FileUpload5_FromSubmit" name="FileUpload5_FromSubmit" type="file" class="info-upload-button">
                        <div>
                            <ul id="ulUploadFiles_FromSubmit" class="ulFiles" data-role="listview">
                            </ul>
                        </div>
                        <a href="#" id="btnUpload_FromSubmit" data-role="button" data-inline="true" data-theme="b" onclick="btnUpload_FromSubmitClick()">Upload</a>
                        <a href="#" id="btnClose_FromSubmit" data-role="button" data-inline="true" data-rel="back" data-theme="c">Close</a>
                    </div>
                </div>
            </div>
            <div data-role="footer" data-theme="b">
                <fieldset class="ui-grid-c" data-theme="b">
                    <div class="ui-block-a">
                        <a data-mini="true" id="btnOk_FromSubmit" data-role="button" data-theme="b" onclick="btnOk_FromSubmitClick()">OK</a>
                    </div>
                    <div class="ui-block-b">
                        <a data-mini="true" id="btnCancel_FromSubmit" data-role="button" data-theme="b" onclick="btnCancel_FromSubmitClick()">Close</a>
                    </div>
                    <div class="ui-block-c">
                        <a data-mini="true" id="btnPreview_FromSubmit" data-role="button" data-theme="b" onclick="btnPreview_FromSubmitClick()">Preview</a>
                    </div>
                    <div class="ui-block-d">
                        <a data-mini="true" id="btnUploadFile_FromSubmit" data-role="button" data-theme="b"
                            href="#popupDialog_FromSubmit" data-rel="popup" data-position-to="window" data-transition="pop">Upload File</a>
                    </div>
                </fieldset>
            </div>
        </div>
    </form>
</body>
</html>
