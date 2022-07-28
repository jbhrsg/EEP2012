<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_ISODocumentQuery.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        var JQDialog4TextBox = "";//部門組權限，0下載，1取號
        var JQDialog5TextBox = "";//個人權限，0下載，1取號
        $(function () {
            initJQDialog2();
            initJQDialog3();
            $('#dataFormMasterOrgCanDownloadC').after($('#OrgCanDownloadButton'));
            $('#dataFormMasterWhoCanDownloadC').after($('#WhoCanDownloadButton'));
            $('#dataFormMasterOrgCanDownload1C').after($('#OrgCanDownloadButton1'));
            $('#dataFormMasterWhoCanDownload1C').after($('#WhoCanDownloadButton1'));

            

            $("#FlowFlag_Query").combobox('setValue', 'Z');

            ////人資可以編輯
            //if (getClientInfo("UserID") == "335") {
            //    var dgView = $("#dataGridView");
            //    var infolightOptions = dgView.attr("infolight-options");
            //    infolightOptions = infolightOptions.replace("commandButtons:'vu'", "commandButtons:'vu'");
            //    dgView.attr("infolight-options", infolightOptions);

            //    $("#JQDialog4").find(".infosysbutton-s").show();
            //    $("#JQDialog5").find(".infosysbutton-s").show();
            //} else {
            //    var dgView = $("#dataGridView");
            //    var infolightOptions = dgView.attr("infolight-options");
            //    infolightOptions = infolightOptions.replace("commandButtons:'vu'", "commandButtons:'v'");
            //    dgView.attr("infolight-options", infolightOptions);

            //    $("#JQDialog4").find(".infosysbutton-s").hide();
            //    $("#JQDialog5").find(".infosysbutton-s").hide();
            //}

            //全公司下載_click處理函式
            $('#dataFormMasterIsAllCanDownload').click(function () {
                if ($(this).is(":checked") == true) {
                    $("#OrgCanDownloadButton").attr("onclick", "").unbind("click");
                    $("#WhoCanDownloadButton").attr("onclick", "").unbind("click");
                    $('#OrgCanDownloadButton').addClass('l-btn-disabled');
                    $('#WhoCanDownloadButton').addClass('l-btn-disabled');
                } else if ($(this).is(":checked") == false) {
                    $("#OrgCanDownloadButton").attr("onclick", "OpenJQDialog4(0)").unbind("click");
                    $("#WhoCanDownloadButton").attr("onclick", "OpenJQDialog5(0)").unbind("click");
                    $('#OrgCanDownloadButton').removeClass('l-btn-disabled');
                    $('#WhoCanDownloadButton').removeClass('l-btn-disabled');
                }
            });

            //全公司取號_click處理函式
            $('#dataFormMasterIsAllCanDownload1').click(function () {
                if ($(this).is(":checked") == true) {
                    $("#OrgCanDownloadButton1").attr("onclick", "").unbind("click");
                    $("#WhoCanDownloadButton1").attr("onclick", "").unbind("click");
                    $('#OrgCanDownloadButton1').addClass('l-btn-disabled');
                    $('#WhoCanDownloadButton1').addClass('l-btn-disabled');
                } else if ($(this).is(":checked") == false) {
                    $("#OrgCanDownloadButton1").attr("onclick", "OpenJQDialog4(1)").unbind("click");
                    $("#WhoCanDownloadButton1").attr("onclick", "OpenJQDialog5(1)").unbind("click");
                    $('#OrgCanDownloadButton1').removeClass('l-btn-disabled');
                    $('#WhoCanDownloadButton1').removeClass('l-btn-disabled');
                }
            });
        });

    function FirstNO_Query_OnSelect(row) {
        $("#SecondNO_Query").combobox("setValue", "");
        $("#SecondNO_Query").combobox("setWhere", "FirstNO='" + row.FirstNO + "'");
    }
    function FormatScript_Button1(val, row, index) {
        //if (row.Flowflag == "Z" && Number(row.RestOfWarrantAmount) != 0) {//row.FlowFlag != "X" &&  //row.ContinueFlag == null &&
        if(row.FlowFlag=="Z" && row.IsLast=="y"){
            return $("<a href='#' onClick='myfunction(" + index + ")'>").linkbutton({ plain: false, text: '修訂' })[0].outerHTML;
        }
    }
    function FormatScript_Button2(val, row, index) {
        //if (row.Flowflag == "Z" && Number(row.RestOfWarrantAmount) != 0) {//row.FlowFlag != "X" &&  //row.ContinueFlag == null &&
        if (row.FlowFlag == "Z" && row.IsLast == "y" && row.DocPropertyNO == "F") {
            if (IsAccess1(row) == true) {
                return $("<a href='#' onClick='myfunction1(" + index + ")'>").linkbutton({ plain: false, text: '表單取號' })[0].outerHTML;
            } else {
                return $("<a href='#' onClick='myfunction1(" + index + ")'>").linkbutton({ plain: false, text: '無取號權限' })[0].outerHTML;
            }

        } else if (row.FlowFlag == "Z" && row.IsLast == "n" && row.DocPropertyNO == "F") {
            return $("<a href='#' onClick='myfunction1(" + index + ")'>").linkbutton({ plain: false, text: '查看取號' })[0].outerHTML;
        }
    }
    //修訂按鈕之處裡函式
    function myfunction(index) {
        $("#dataGridView").datagrid("selectRow", index);
        var row = $("#dataGridView").datagrid("getSelected");

        if (IsProcess(row.DocPaperNO)=='y') {
            alert("此文件已提出修訂申請，正在審核流程中");
            return false;
        } else if (IsProcess(row.DocPaperNO) == 'n') {
            openForm('#JQDialog1', '', 'inserted', 'dialog');
        } else {
            alert("有錯誤，請洽管理室");
            return false;
        }
    }
    //表單取號或查看取號按鈕之處裡函式
    function myfunction1(index) {
        $("#dataGridView").datagrid("selectRow", index);
        var row = $("#dataGridView").datagrid("getSelected");
        if (row.IsLast == "n") {
                $("#JQDialog3").dialog("open");
                $("#dataGridForm1").datagrid("setWhere", "DocNO='" + row.DocNO + "'");
        } else if (row.IsLast == "y") {//表單取號   
            if (IsAccess() == true) {
            $("#JQDialog2").dialog("open");
            $("#dataGridForm").datagrid("setWhere", "DocNO='" + row.DocNO + "'");
            }
        }
    }
    //能否進入表單取號
    function IsAccess() {
        var access = false;
        var row = $("#dataGridView").datagrid("getSelected");
        var FlowFlag = row.FlowFlag;
        if (FlowFlag == 'Z') {

            var UserID = getClientInfo("UserID");
            var WhoCanDownload = row.WhoCanDownload1;
            var OrgCanDownload = row.OrgCanDownload1;
            var IsAllCanDownload = row.IsAllCanDownload1;
            
            var IsInOrgCanDownload = false;
            var OrgStr = GetUserOrgNOs(UserID);
            if (OrgStr != null && OrgStr != "") {
                var OrgNOArr = OrgStr.split(',');
                if ((OrgCanDownload!==null && OrgCanDownload != "" && OrgCanDownload != undefined) && OrgNOArr.length > 1) {
                    for (var i = 0; i < OrgNOArr.length; i++) {
                        if (OrgCanDownload.includes(OrgNOArr[i]) == true) {
                            IsInOrgCanDownload = true;
                            break;
                        }
                    }
                }
            }


            if ((WhoCanDownload!==null && WhoCanDownload.includes(UserID) == true) || IsInOrgCanDownload == true || IsAllCanDownload == '1') {
                access = true;
            } else {
                access = false;
            }
        } else {
            access = false;
        }
        return access;
    }

    //能否進入表單取號
    function IsAccess1(row) {
        var access = false;
        //var row = $("#dataGridView").datagrid("getSelected");
        var FlowFlag = row.FlowFlag;
        if (FlowFlag == 'Z') {

            var UserID = getClientInfo("UserID");
            var WhoCanDownload = row.WhoCanDownload1;
            var OrgCanDownload = row.OrgCanDownload1;
            var IsAllCanDownload = row.IsAllCanDownload1;

            var IsInOrgCanDownload = false;
            var OrgStr = GetUserOrgNOs(UserID);
            if (OrgStr != null && OrgStr != "") {
                var OrgNOArr = OrgStr.split(',');
                if ((OrgCanDownload!==null && OrgCanDownload != "" && OrgCanDownload != undefined) && OrgNOArr.length > 1) {
                    for (var i = 0; i < OrgNOArr.length; i++) {
                        if (OrgCanDownload.includes(OrgNOArr[i]) == true) {
                            IsInOrgCanDownload = true;
                            break;
                        }
                    }
                }
            }


            if ((WhoCanDownload!==null && WhoCanDownload.includes(UserID) == true) || IsInOrgCanDownload == true || IsAllCanDownload == '1') {
                access = true;
            } else {
                access = false;
            }
        } else {
            access = false;
        }
        return access;
    }

    function IsProcess(DocPaperNO) {
        var returnValue = "0";
        $.ajax({
            type: "POST",
            url: '../handler/jqDataHandle.ashx?RemoteName=sForm_ISODocumentQuery.ISODocument',
            data: "mode=method&method=" + "IsProcess&parameters=" + DocPaperNO, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
            cache: false,
            async: false,
            success: function (data) {
                if (data != "0") {
                    returnValue = data;
                } else {
                    alert("錯誤，請洽管理室");
                }
            }
        });

        return returnValue;
    }
   
    function JQDataForm1_OnLoad() {

        if (getEditMode($("#JQDataForm1")) == 'inserted') {//新增
            var row = $("#dataGridView").datagrid('getSelected');
            $("#JQDataForm1FirstNO").combobox('setValue', row.FirstNO);
            $("#JQDataForm1SecondNO").combobox('setValue', row.SecondNO);
            $("#JQDataForm1DocPropertyNO").combobox('setValue', row.DocPropertyNO);
            $("#JQDataForm1DocName").val(row.DocName);
            $("#JQDataForm1DocPaperNO").val(row.DocPaperNO.substr(0, 9) + row.DocName);
            $("#JQDataForm1WhoCanDownload").val(row.WhoCanDownload);
            $("#JQDataForm1IsAllCanDownload").val(row.IsAllCanDownload);
            $("#JQDataForm1OrgCanDownload").val(row.OrgCanDownload);
            $("#JQDataForm1WhoCanDownload1").val(row.WhoCanDownload1);
            $("#JQDataForm1IsAllCanDownload1").val(row.IsAllCanDownload1);
            $("#JQDataForm1OrgCanDownload1").val(row.OrgCanDownload1);

            $("#JQDataForm1DocNO").val("自動編號");

            var today = new Date();
            var dd = String(today.getDate()).padStart(2, '0');
            var mm = String(today.getMonth() + 1).padStart(2, '0');
            var yyyy = today.getFullYear();
            todayFormat = yyyy + '-' + mm + '-' + dd;
            $("#JQDataForm1CreateDate").val(todayFormat);
            var _IsModify = row.IsModify;
            if (_IsModify == "n") {
                $("#JQDataForm1FirstDocNO").val(row.DocNO);
            }
            else {
                $("#JQDataForm1FirstDocNO").val(row.FirstDocNO);
            }
            $("#JQDataForm1IsModify").val('y');

            //var infofileUpload = $('#JQDataForm1PdfFile');
            //var infofileUploadvalue = $('.info-fileUpload-value', infofileUpload.next())
            //var infofileUploadfile = $('.info-fileUpload-file', infofileUpload.next())
            //infofileUploadvalue.val('');
            //infofileUploadfile.val('');

            //infofileUpload = $('#JQDataForm1WordFile');
            //infofileUploadvalue = $('.info-fileUpload-value', infofileUpload.next())
            //infofileUploadfile = $('.info-fileUpload-file', infofileUpload.next())
            //infofileUploadvalue.val('');
            //infofileUploadfile.val('');

            //$("#JQDataForm1CreateBy").combobox('setValue','');
            //$("#JQDataForm1CreateDate").val('');
            //$("#JQDataForm1LastUpdateBy").combobox('setValue', '');
            //$("#JQDataForm1LastUpdateDate").val('');
        }
    }
    //沒用到，修訂不需改ISO Code
    function GetISOCode(ISOCodePrefix) {
        var returnValue = "有誤";
        $.ajax({
            type: "POST",
            url: '../handler/jqDataHandle.ashx?RemoteName=sForm_ISODocument.ISODocument',
            data: "mode=method&method=" + "GetISOCode&parameters=" + ISOCodePrefix, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
            cache: false,
            async: false,
            success: function (data) {
                if (data != "0") {
                    returnValue = data;
                } else {
                    alert("產生編號有誤，請洽管理室");
                }
            }
        });

        return returnValue;
    }

    function JQDataForm1_OnApplied() {
        //var param = Request.getQueryStringByName("p1");
        //var ParentKey = $("#dataFormMasterParentKey").val();
        if (getEditMode($("#JQDataForm1")) == 'inserted') {//新增狀態
            var userid = getClientInfo("userid");
            //自動起單
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sForm_ISODocument.ISODocument',
                data: "mode=method&method=" + "FlowStartUp" + "&parameters=" + userid, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    if (data != "False") {
                        alert('申請成功');
                    } else {
                        alert('申請失敗');
                    }
                }
            });
            $("#dataGridView").datagrid("load");
        }
    }

    function JQDataForm1_OnApply() {
        var pre = confirm("確定起單-文件修訂申請單?");
        if (pre == true) {
            return true;
        } else {
            return false;
        }
    }

    function dataFormMaster_Onload() {
        //顯示部門組或個人代碼的中文
        DisplayChineseName();


        var FlowFlag = $("#dataFormMasterFlowFlag").combobox("getValue");

        //控制可否看到下載連結
        if (FlowFlag == 'Z') {

            var UserID = getClientInfo("UserID");
            var WhoCanDownload = $("#dataFormMasterWhoCanDownload").val();
            var OrgCanDownload = $("#dataFormMasterOrgCanDownload").val();
            var IsAllCanDownload = $("#dataFormMasterIsAllCanDownload").checkbox("getValue");
            //alert(IsAllCanDownload);
            var IsInOrgCanDownload = false;
            var OrgStr = GetUserOrgNOs(UserID);
            if (OrgStr != null && OrgStr != "") {
                var OrgNOArr = OrgStr.split(',');
                if ((OrgCanDownload!==null && OrgCanDownload != "" && OrgCanDownload != undefined) && OrgNOArr.length > 1) {
                    for (var i = 0; i < OrgNOArr.length; i++) {
                        if (OrgCanDownload.includes(OrgNOArr[i]) == true) {
                            IsInOrgCanDownload = true;
                            break;
                        }
                    }
                }
            }


            if ((WhoCanDownload!==null && WhoCanDownload.includes(UserID) == true) || IsInOrgCanDownload == true || IsAllCanDownload == '1') {
                $("#downloadPdfFile").remove();
                var PdfFile = $('.info-fileUpload-value', $("#dataFormMasterPdfFile").next()).val();
                if (PdfFile != '') {
                    var link = $("<a download>").attr({ 'id': 'downloadPdfFile', 'href': '../JB_ADMIN/Form_ISODocument/PdfFile/' + PdfFile }).html('下載');
                    $('#dataFormMasterPdfFile').closest('td').append(link);
                }
                $("#downloadWordFile").remove();
                var WordFile = $('.info-fileUpload-value', $("#dataFormMasterWordFile").next()).val();
                if (WordFile != '') {
                    var link = $("<a download>").attr({ 'id': 'downloadWordFile', 'href': '../JB_ADMIN/Form_ISODocument/WordFile/' + WordFile }).html('下載');
                    $('#dataFormMasterWordFile').closest('td').append(link);
                }
            } else {
                $("#downloadPdfFile").remove();
                $("#downloadWordFile").remove();
            }
        } else {
            $("#downloadPdfFile").remove();
            $("#downloadWordFile").remove();
        }


        //選全公司下載要停用部門下載和個人下載按鈕
        if ($("#dataFormMasterIsAllCanDownload").is(":checked") == true) {
            $("#OrgCanDownloadButton").attr("onclick", "").unbind("click");
            $("#WhoCanDownloadButton").attr("onclick", "").unbind("click");
            $('#OrgCanDownloadButton').addClass('l-btn-disabled');
            $('#WhoCanDownloadButton').addClass('l-btn-disabled');
        } else if ($("#dataFormMasterIsAllCanDownload").is(":checked") == false) {
            $("#OrgCanDownloadButton").attr("onclick", "OpenJQDialog4(0)").unbind("click");
            $("#WhoCanDownloadButton").attr("onclick", "OpenJQDialog5(0)").unbind("click");
            $('#OrgCanDownloadButton').removeClass('l-btn-disabled');
            $('#WhoCanDownloadButton').removeClass('l-btn-disabled');
        }
        //選全公司取號要停用部門取號和個人取號按鈕
        if ($("#dataFormMasterIsAllCanDownload1").is(":checked") == true) {
            $("#OrgCanDownloadButton1").attr("onclick", "").unbind("click");
            $("#WhoCanDownloadButton1").attr("onclick", "").unbind("click");
            $('#OrgCanDownloadButton1').addClass('l-btn-disabled');
            $('#WhoCanDownloadButton1').addClass('l-btn-disabled');
        } else if ($("#dataFormMasterIsAllCanDownload1").is(":checked") == false) {
            $("#OrgCanDownloadButton1").attr("onclick", "OpenJQDialog4(1)").unbind("click");
            $("#WhoCanDownloadButton1").attr("onclick", "OpenJQDialog5(1)").unbind("click");
            $('#OrgCanDownloadButton1').removeClass('l-btn-disabled');
            $('#WhoCanDownloadButton1').removeClass('l-btn-disabled');
        }

        
        //若文件屬性是表單
        if ($("#dataFormMasterDocPropertyNO").combobox("getValue") == "F") {
            var HiddenMasterFields = ['WhoCanDownload1C', 'OrgCanDownload1C', 'IsAllCanDownload1'];
            ShowFields('#dataFormMaster', HiddenMasterFields);
        } else {
            var HiddenMasterFields = ['WhoCanDownload1C', 'OrgCanDownload1C', 'IsAllCanDownload1'];
            HideFields('#dataFormMaster', HiddenMasterFields);
        }
        //若文件是首次或修訂
        if ($("#dataFormMasterIsModify").val() == 'n') {
            $("#JQDialog1").panel('setTitle', '文件首次申請單');
            $("#dataFormMasterReason").closest("td").prev("td").html("文件制訂申請說明");
        } else if ($("#dataFormMasterIsModify").val() == 'y') {
            $("#JQDialog1").panel('setTitle', '文件修訂申請單');
            $("#dataFormMasterReason").closest("td").prev("td").html("文件修訂申請說明");
        }
    }

    function queryGrid() {
        var queryArr = [];
        var isQueryFlag = 0;

        if ($("#FirstNO_Query").combobox('getValue') != '') {
            queryArr.push("FirstNO = '" + $("#FirstNO_Query").combobox('getValue') + "'");
            isQueryFlag = 1;
        }
        if ($("#SecondNO_Query").combobox('getValue') != '') {
            queryArr.push("SecondNO = '" + $("#SecondNO_Query").combobox('getValue') + "'");
            isQueryFlag = 1;
        }
        if ($("#DocPropertyNO_Query").combobox('getValue') != '') {
            queryArr.push("DocPropertyNO = '" + $("#DocPropertyNO_Query").combobox('getValue') + "'");
            isQueryFlag = 1;
        }
        if ($("#FlowFlag_Query").combobox('getValue') != '') {
            queryArr.push("FlowFlag = '" + $("#FlowFlag_Query").combobox('getValue') + "'");
            isQueryFlag = 1;
        }
        if ($("#IsLast_Query").checkbox('getValue') == 'y') {
            queryArr.push("IsLast = 'y'");
            isQueryFlag = 1;
        }

        if (isQueryFlag == 1) {//有查詢條件
            $("#dataGridView").datagrid('setWhere', queryArr.join(" and "));
        } else {//無查詢條件
            $("#dataGridView").datagrid('setWhere', '');
        }
    }

    function initJQDialog2() {
        $("#JQDialog2").dialog(
        {
            height: 400,
            width: 500,
            left: 400,
            top: 100,
            resizable: false,
            modal: true,
            title: "表單號碼",
            closed: true
        });
    };
    function initJQDialog3() {
        $("#JQDialog3").dialog(
        {
            height: 400,
            width: 450,
            left: 400,
            top: 100,
            resizable: false,
            modal: true,
            title: "表單號碼",
            closed: true
        });
    };
    function dataFormForm_OnLoad() {
        //var pre = confirm("確定取號?");
        //if (pre == true) {
            var row = $("#dataGridView").datagrid("getSelected");
            $("#dataFormFormDocNO").val(row.DocNO);
            var _FirstDocNO = row.FirstDocNO;            
            $("#dataFormFirstDocNO").val(_FirstDocNO);

            //applyForm("#dataFormForm");
            //closeForm("#JQDialogForm");
            //$('#dataGridForm').datagrid('reload');
            //alert("取號完成");
        //} else {
        //    closeForm("#JQDialogForm");
        //}
    }

    function OpenJQDialog4(i) {
        //var rows = $("#dataGridView").datagrid('getRows');
        //if (rows.length <= 0) {
        //    alert('須請先選取一筆業務人員');
        //    return false;
        //}
        var nodes = $('#TreeOrgCanDownload').tree('getChecked');
        $.each(nodes, function (index, node) {
            $('#TreeOrgCanDownload').tree('uncheck', node.target);
            $(node.target).removeClass('.tree-node-clicked');
        });
        var root = $('#TreeOrgCanDownload').tree('getRoot');
        $('#TreeOrgCanDownload').tree('uncheck', root.target);
        //var row = $('#dataGridView').datagrid('getSelected');
        var OrgCanDownload ="";
        if (i == 0) {
            OrgCanDownload = $("#dataFormMasterOrgCanDownload").val();
            JQDialog4TextBox = 0;
        } else if (i == 1) {
            OrgCanDownload = $("#dataFormMasterOrgCanDownload1").val();
            JQDialog4TextBox = 1;
        }
        TreeOrgCanDownloadSetChecked(OrgCanDownload);
        openForm('#JQDialog4', {}, "", 'dialog');
    }
    function TreeOrgCanDownloadSetChecked(IDstr) {
        if (IDstr != '') {
            IDstrArr=IDstr.split(",");
            $.each(IDstrArr, function (i, v) {
                //alert(i + ',' + v);
                var node = $('#TreeOrgCanDownload').tree('find', v);
                if (node != null) {
                    $(node.target).addClass('tree-node-clicked');
                    $('#TreeOrgCanDownload').tree('check', node.target);
                }
            });
        }
    }

    function OpenJQDialog5(i) {
        //var rows = $("#dataGridView").datagrid('getRows');
        //if (rows.length <= 0) {
        //    alert('須請先選取一筆業務人員');
        //    return false;
        //}
        var nodes = $('#TreeWhoCanDownload').tree('getChecked');
        $.each(nodes, function (index, node) {
            $('#TreeWhoCanDownload').tree('uncheck', node.target);
            $(node.target).removeClass('.tree-node-clicked');
        });
        var root = $('#TreeWhoCanDownload').tree('getRoot');
        $('#TreeWhoCanDownload').tree('uncheck', root.target);
        //var row = $('#dataGridView').datagrid('getSelected');
        var WhoCanDownload = "";
        if (i == 0) {
            WhoCanDownload = $("#dataFormMasterWhoCanDownload").val();
            JQDialog5TextBox = 0;
        }else if(i==1){
            WhoCanDownload = $("#dataFormMasterWhoCanDownload1").val();
            JQDialog5TextBox = 1;
        }
        TreeWhoCanDownloadSetChecked(WhoCanDownload);
        openForm('#JQDialog5', {}, "", 'dialog');
    }
    function TreeWhoCanDownloadSetChecked(IDstr) {
        if (IDstr != '') {
            IDstrArr = IDstr.split(",");
            $.each(IDstrArr, function (i, v) {
                //alert(i + ',' + v);
                var node = $('#TreeWhoCanDownload').tree('find', v);
                if (node != null) {
                    $(node.target).addClass('tree-node-clicked');
                    $('#TreeWhoCanDownload').tree('check', node.target);
                }
            });
        }
    }

    function JQDialog4OnSubmited() {
        var nodes = $('#TreeOrgCanDownload').tree('getChecked');
        var SalesTypeIDs = '';
        var i = 1;
        $.each(nodes, function (index, node) {
            if (i > 1) {
                SalesTypeIDs = SalesTypeIDs + ',' + node.id;
            }
            else {
                SalesTypeIDs = SalesTypeIDs + node.id;
            }
            i = i + 1;
        });

        var OrgNames = "";
        if (SalesTypeIDs != "") {
            //SalesTypeIDs轉中文名稱
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sForm_ISODocumentQuery.ISODocument',
                data: "mode=method&method=" + "ReturnOrgNames&parameters=" + SalesTypeIDs, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    if (data.length > 0) {
                        OrgNames = data;
                    } else {
                        alert("錯誤，請洽管理室");
                    }
                }
            });
        }

        if (JQDialog4TextBox == 0) {
            $("#dataFormMasterOrgCanDownload").val(SalesTypeIDs);
            $("#dataFormMasterOrgCanDownloadC").val(OrgNames);
        } else if (JQDialog4TextBox == 1) {
            $("#dataFormMasterOrgCanDownload1").val(SalesTypeIDs);
            $("#dataFormMasterOrgCanDownload1C").val(OrgNames);
        }
        closeForm('#JQDialog4');
    }

    function JQDialog5OnSubmited() {
        var nodes = $('#TreeWhoCanDownload').tree('getChecked');
        var SalesTypeIDs = '';
        var i = 1;
        $.each(nodes, function (index, node) {
            if (i > 1) {
                SalesTypeIDs = SalesTypeIDs + ',' + node.id;
            }
            else {
                SalesTypeIDs = SalesTypeIDs + node.id;
            }
            i = i + 1;
        });

        var UserNames = "";
        if (SalesTypeIDs != "") {
            //SalesTypeIDs轉中文名稱
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sForm_ISODocumentQuery.ISODocument',
                data: "mode=method&method=" + "ReturnUserNames&parameters=" + SalesTypeIDs, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    if (data.length > 0) {
                        UserNames = data;
                    } else {
                        alert("錯誤，請洽管理室");
                    }
                }
            });
        }

        if (JQDialog5TextBox == 0) {
            $("#dataFormMasterWhoCanDownload").val(SalesTypeIDs);
            $("#dataFormMasterWhoCanDownloadC").val(UserNames);
        } else if (JQDialog5TextBox == 1) {
            $("#dataFormMasterWhoCanDownload1").val(SalesTypeIDs);
            $("#dataFormMasterWhoCanDownload1C").val(UserNames);
        }
        closeForm('#JQDialog5');
    }
    //沒用，因載入慢
    function dataGridView_Onload() {
        if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) {
            //篩選
            var where = $("#dataGridView").datagrid('getWhere');
            $("#dataGridView").datagrid('setWhere', where);
        }
    }

    function dataFormMaster_Onapply() {
        if ($("#dataFormMasterIsAllCanDownload").checkbox("getValue") == "1") {
            $("#dataFormMasterWhoCanDownload").val("");
            $("#dataFormMasterOrgCanDownload").val("");
        }
        if ($("#dataFormMasterIsAllCanDownload1").checkbox("getValue") == "1") {
            $("#dataFormMasterWhoCanDownload1").val("");
            $("#dataFormMasterOrgCanDownload1").val("");
        }
    }

    //------------工具---------------
    function GetUserOrgNOs(UserID) {
        //var UserID = getClientInfo("UserID");
        var returnString = "";
        $.ajax({
            type: "POST",
            url: '../handler/jqDataHandle.ashx?RemoteName=sForm_ISODocumentQuery.ISODocument', //連接的Server端，command
            data: "mode=method&method=" + "GetUserOrgNOs" + "&parameters=" + UserID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
            cache: false,
            async: false,
            success: function (data) {
                var rows = $.parseJSON(data);
                if (rows.length > 0) {
                    returnString = rows[0].OrgNOs;
                    //myArr[1] = rows[0].OrgNOParent;
                }
            }
        }
        );
        return returnString;
    }

    function DisplayChineseName() {
        //顯示中文名稱
        if ($("#dataFormMasterOrgCanDownload").val() != "") {
            //轉中文名稱
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sForm_ISODocumentQuery.ISODocument',
                data: "mode=method&method=" + "ReturnOrgNames&parameters=" + $("#dataFormMasterOrgCanDownload").val(), //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    if (data.length > 0) {
                        $("#dataFormMasterOrgCanDownloadC").val(data);
                    }
                }
            });
        }

        //顯示中文名稱
        if ($("#dataFormMasterOrgCanDownload1").val() != "") {
            //轉中文名稱
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sForm_ISODocumentQuery.ISODocument',
                data: "mode=method&method=" + "ReturnOrgNames&parameters=" + $("#dataFormMasterOrgCanDownload1").val(), //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    if (data.length > 0) {
                        $("#dataFormMasterOrgCanDownload1C").val(data);
                    }
                }
            });
        }

        //顯示中文名稱
        if ($("#dataFormMasterWhoCanDownload").val() != "") {
            //轉中文名稱
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sForm_ISODocumentQuery.ISODocument',
                data: "mode=method&method=" + "ReturnUserNames&parameters=" + $("#dataFormMasterWhoCanDownload").val(), //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    if (data.length > 0) {
                        $("#dataFormMasterWhoCanDownloadC").val(data);
                    }
                }
            });
        }

        //顯示中文名稱
        if ($("#dataFormMasterWhoCanDownload1").val() != "") {
            //轉中文名稱
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sForm_ISODocumentQuery.ISODocument',
                data: "mode=method&method=" + "ReturnUserNames&parameters=" + $("#dataFormMasterWhoCanDownload1").val(), //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    if (data.length > 0) {
                        $("#dataFormMasterWhoCanDownload1C").val(data);
                    }
                }
            });
        }
    }

    function HideFields(FormName, FieldNames) {
        $.each(FieldNames, function (index, value) {
            $(FormName + value).closest('td').prev('td').hide();//closest上一層selector,prev下一個selector
            $(FormName + value).closest('td').hide();
        });
    }
    function ShowFields(FormName, FieldNames) {
        $.each(FieldNames, function (index, value) {
            $(FormName + value).closest('td').prev('td').show();//closest上一層selector,prev下一個selector
            $(FormName + value).closest('td').show();
        });
    }
    //function dataGridForm_OnLoad() {
    //    var row = $("#dataGridView").datagrid("getSelected");
    //    alert(row.IsLast);
    //    if (row.IsLast == "y") {
    //        $('#toolItemdataGridForm取號').show();
    //    } else {
    //        $('#toolItemdataGridForm取號').hide();
    //    }
        //}

    //控制是否可以修改 
    function UpdateRow(rowData) {
        var username = getClientInfo("UserID");

        if (rowData.CreateBy != username) {
            alert('無編輯權限！');
            return false; //取消編輯的動作 
        }
    }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sForm_ISODocumentQuery.ISODocument" runat="server" AutoApply="True"
                DataMember="ISODocument" Pagination="True" QueryTitle="查詢條件" EditDialogID="JQDialog0"
                Title="文件查詢與修訂" AllowAdd="False" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnUpdate="UpdateRow">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption=" " Editor="text" FieldName="Button1" FormatScript="FormatScript_Button1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption=" " Editor="text" FieldName="Button2" FormatScript="FormatScript_Button2" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="申請文號" Editor="text" FieldName="DocNO" Format="" MaxLength="0" Visible="true" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ISO文編" Editor="text" FieldName="DocPaperNO" Format="" MaxLength="0" Visible="true" Width="250" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="主編碼" Editor="infocombobox" FieldName="FirstNO" Format="" MaxLength="0" Visible="true" Width="120" EditorOptions="valueField:'FirstNO',textField:'FirstName',remoteName:'sForm_ISODocumentQuery.ISOFirstNO',tableName:'ISOFirstNO',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="次編碼" Editor="infocombobox" FieldName="SecondNO" Format="" MaxLength="0" Visible="true" Width="120" EditorOptions="valueField:'SecondNO',textField:'SecondName',remoteName:'sForm_ISODocumentQuery.ISOSecondNO',tableName:'ISOSecondNO',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="文件屬性" Editor="infocombobox" FieldName="DocPropertyNO" Format="" MaxLength="0" Visible="true" Width="120" EditorOptions="valueField:'DocPropertyNO',textField:'DocPropertyName',remoteName:'sForm_ISODocumentQuery.ISODocProperty',tableName:'ISODocProperty',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="文件名稱" Editor="text" FieldName="DocName" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="掃描文件" Editor="text" FieldName="PdfFile" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="電子原稿" Editor="text" FieldName="WordFile" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="全公司下載" Editor="text" FieldName="IsAllCanDownload" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        <DrillFields>
                            <JQTools:JQDrillDownFields />
                        </DrillFields>
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="部門組下載" Editor="text" FieldName="OrgCanDownload" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="個人下載" Editor="text" FieldName="WhoCanDownload" Format="" MaxLength="0" Visible="False" Width="120" EditorOptions="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="全公司取號" Editor="text" FieldName="IsAllCanDownload1" MaxLength="0" Visible="False" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="部門組取號" Editor="text" FieldName="OrgCanDownload1" Visible="False" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="個人取號" Editor="text" FieldName="WhoCanDownload1" MaxLength="0" Visible="False" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請者" Editor="infocombobox" FieldName="CreateBy" Format="" Visible="true" Width="50" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sForm_ISODocumentQuery.USERS',tableName:'USERS',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="預定發行日期" Editor="text" FieldName="CreateDate" Format="yyyy-mm-dd" MaxLength="0" Visible="true" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改者" Editor="infocombobox" FieldName="LastUpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sForm_ISODocumentQuery.USERS',tableName:'USERS',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Format="">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="text" FieldName="LastUpdateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Format="yyyy-mm-dd">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="流程狀態" Editor="infocombobox" FieldName="FlowFlag" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" EditorOptions="valueField:'FlowNO',textField:'FlowName',remoteName:'sForm_ISODocumentQuery.FlowFlag',tableName:'FlowFlag',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Format="">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="FirstDocNO" Editor="text" FieldName="FirstDocNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" Visible="False" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消" Visible="False"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" Visible="False" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportXlsx" Text="匯出xlsx" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="主編碼" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'FirstNO',textField:'FirstName',remoteName:'sForm_ISODocumentQuery.ISOFirstNO',tableName:'ISOFirstNO',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:FirstNO_Query_OnSelect,panelHeight:200" FieldName="FirstNO" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="160" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="次編碼" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'SecondNO',textField:'SecondName',remoteName:'sForm_ISODocumentQuery.ISOSecondNO',tableName:'ISOSecondNO',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SecondNO" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="180" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="文件屬性" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'DocPropertyNO',textField:'DocPropertyName',remoteName:'sForm_ISODocumentQuery.ISODocProperty',tableName:'ISODocProperty',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="DocPropertyNO" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="流程狀態" Condition="=" DataType="string" DefaultValue="" Editor="infocombobox" EditorOptions="valueField:'FlowNO',textField:'FlowName',remoteName:'sForm_ISODocumentQuery.FlowFlag',tableName:'FlowFlag',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="FlowFlag" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="80" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="最新版" Condition="=" DataType="string" Editor="checkbox" EditorOptions="on:'y',off:'n'" FieldName="IsLast" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="40" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog0" runat="server" BindingObjectID="dataFormMaster" Title="文件" Width="820px" DialogTop="50px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="ISODocument" HorizontalColumnsCount="2" RemoteName="sForm_ISODocumentQuery.ISODocument" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnLoadSuccess="dataFormMaster_Onload" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApply="dataFormMaster_Onapply" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="申請文號" Editor="text" FieldName="DocNO" Format="" maxlength="0" Width="180" NewRow="False" ReadOnly="True" Span="1" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ISO文編" Editor="text" FieldName="DocPaperNO" Format="" maxlength="0" Width="500" NewRow="True" Span="2" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="主編碼" Editor="infocombobox" FieldName="FirstNO" Format="" maxlength="0" Width="180" EditorOptions="valueField:'FirstNO',textField:'FirstName',remoteName:'sForm_ISODocumentQuery.ISOFirstNO',tableName:'ISOFirstNO',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="次編碼" Editor="infocombobox" FieldName="SecondNO" Format="" maxlength="0" Width="180" EditorOptions="valueField:'SecondNO',textField:'SecondName',remoteName:'sForm_ISODocumentQuery.ISOSecondNO',tableName:'ISOSecondNO',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="文件屬性" Editor="infocombobox" FieldName="DocPropertyNO" Format="" maxlength="0" Width="180" EditorOptions="valueField:'DocPropertyNO',textField:'DocPropertyName',remoteName:'sForm_ISODocumentQuery.ISODocProperty',tableName:'ISODocProperty',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="文件名稱" Editor="text" FieldName="DocName" Format="" maxlength="0" Width="180" ReadOnly="True" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="掃描文件" Editor="infofileupload" FieldName="PdfFile" Format="" maxlength="0" Width="500" EditorOptions="filter:'',isAutoNum:true,upLoadFolder:'JB_ADMIN/Form_ISODocument/PdfFile',showButton:true,showLocalFile:false,fileSizeLimited:'100000'" ReadOnly="False" Span="2" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電子原稿" Editor="infofileupload" FieldName="WordFile" Format="" Width="500" EditorOptions="filter:'',isAutoNum:true,upLoadFolder:'JB_ADMIN/Form_ISODocument/WordFile',showButton:true,showLocalFile:false,fileSizeLimited:'100000'" NewRow="False" Span="2" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="文件制(修)訂申請說明" Editor="textarea" FieldName="Reason" MaxLength="0" Width="500" ReadOnly="True" EditorOptions="height:50" NewRow="False" RowSpan="1" Span="2" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="全公司下載" Editor="checkbox" FieldName="IsAllCanDownload" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" EditorOptions="on:1,off:0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="部門組下載" Editor="text" FieldName="OrgCanDownload" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="False" Width="500" EditorOptions="panelWidth:350,valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sForm_ISODocumentQuery.OrgCanDownload',tableName:'OrgCanDownload',valueFieldCaption:'號碼',textFieldCaption:'名稱',selectOnly:false,checkData:false,columns:[],cacheRelationText:false,multiple:true" PlaceHolder="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="個人下載" Editor="text" EditorOptions="panelWidth:600,valueField:'USERID',textField:'USERNAME',remoteName:'sForm_ISODocumentQuery.WhoCanDownload_User',tableName:'WhoCanDownload_User',valueFieldCaption:'工號',textFieldCaption:'姓名',selectOnly:false,checkData:false,columns:[{field:'USERID',title:'工號',width:80,align:'left',sortable:true},{field:'USERNAME',title:'姓名',width:80,align:'left',sortable:true}],cacheRelationText:false,multiple:true" FieldName="WhoCanDownload" Format="" NewRow="True" Span="2" Width="500" Visible="False" MaxLength="0" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="部門組下載" Editor="text" FieldName="OrgCanDownloadC" Width="500" MaxLength="0" Span="2" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="個人下載" Editor="text" FieldName="WhoCanDownloadC" Width="500" maxlength="0" Span="2" ReadOnly="True" NewRow="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="全公司取號" Editor="checkbox" FieldName="IsAllCanDownload1" Width="80" EditorOptions="on:1,off:0" maxlength="0" NewRow="False" ReadOnly="False" Span="1" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="部門組取號" Editor="text" FieldName="OrgCanDownload1" Width="500" maxlength="0" EditorOptions="panelWidth:350,valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sForm_ISODocumentQuery.OrgCanDownload',tableName:'OrgCanDownload',valueFieldCaption:'號碼',textFieldCaption:'名稱',selectOnly:false,checkData:false,columns:[],cacheRelationText:false,multiple:true" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="個人取號" Editor="text" FieldName="WhoCanDownload1" Width="500" EditorOptions="panelWidth:600,valueField:'USERID',textField:'USERNAME',remoteName:'sForm_ISODocumentQuery.WhoCanDownload_User',tableName:'WhoCanDownload_User',valueFieldCaption:'工號',textFieldCaption:'姓名',selectOnly:false,checkData:false,columns:[{field:'USERID',title:'工號',width:80,align:'left',sortable:true},{field:'USERNAME',title:'姓名',width:80,align:'left',sortable:true}],cacheRelationText:false,multiple:true" maxlength="0" NewRow="False" ReadOnly="False" Span="2" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="部門組取號" Editor="text" FieldName="OrgCanDownload1C" maxlength="0" Width="500" NewRow="False" Span="2" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="個人取號" Editor="text" FieldName="WhoCanDownload1C" maxlength="0" Width="500" ReadOnly="True" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請者" Editor="infocombobox" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sForm_ISODocumentQuery.USERS',tableName:'USERS',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CreateBy" Format="" Width="180" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="預定發行日" Editor="datebox" FieldName="CreateDate" Width="100" Format="" ReadOnly="False" maxlength="0" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修改者" Editor="infocombobox" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sForm_ISODocumentQuery.USERS',tableName:'USERS',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="LastUpdateBy" Format="" maxlength="0" Visible="True" Width="180" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="LastUpdateDate" Format="" maxlength="0" Visible="True" Width="100" ReadOnly="True" NewRow="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="流程狀態" Editor="infocombobox" EditorOptions="valueField:'FlowNO',textField:'FlowName',remoteName:'sForm_ISODocumentQuery.FlowFlag',tableName:'FlowFlag',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="FlowFlag" Format="" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="是否為修訂" Editor="text" FieldName="IsModify" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="FirstDocNO" Editor="text" FieldName="FirstDocNO" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQButton ID="OrgCanDownloadButton" runat="server" Icon="icon-view" OnClick="OpenJQDialog4(0)" Text="權限" />
                <JQTools:JQButton ID="WhoCanDownloadButton" runat="server" Icon="icon-view" OnClick="OpenJQDialog5(0)" Text="權限" />

                <JQTools:JQButton ID="OrgCanDownloadButton1" runat="server" Icon="icon-view" OnClick="OpenJQDialog4(1)" Text="權限" />
                <JQTools:JQButton ID="WhoCanDownloadButton1" runat="server" Icon="icon-view" OnClick="OpenJQDialog5(1)" Text="權限" />
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                </JQTools:JQValidate>
            </JQTools:JQDialog>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="JQDataForm1" Title="文件修訂申請單" Width="780px">
                <JQTools:JQDataForm ID="JQDataForm1" runat="server" DataMember="ISODocument" HorizontalColumnsCount="2" RemoteName="sForm_ISODocumentQuery.ISODocument" OnLoadSuccess="JQDataForm1_OnLoad" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApplied="JQDataForm1_OnApplied" OnApply="JQDataForm1_OnApply" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="申請文號" Editor="text" FieldName="DocNO" Format="" maxlength="0" Width="180" ReadOnly="True" NewRow="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ISOO文編" Editor="text" FieldName="DocPaperNO" Format="" maxlength="0" Width="500" ReadOnly="True" NewRow="True" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="主編碼" Editor="infocombobox" FieldName="FirstNO" Format="" maxlength="0" Width="180" EditorOptions="valueField:'FirstNO',textField:'FirstName',remoteName:'sForm_ISODocumentQuery.ISOFirstNO',tableName:'ISOFirstNO',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="次編碼" Editor="infocombobox" FieldName="SecondNO" Format="" maxlength="0" Width="180" EditorOptions="valueField:'SecondNO',textField:'SecondName',remoteName:'sForm_ISODocumentQuery.ISOSecondNO',tableName:'ISOSecondNO',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" ReadOnly="True" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="文件屬性" Editor="infocombobox" FieldName="DocPropertyNO" Format="" maxlength="0" Width="180" EditorOptions="valueField:'DocPropertyNO',textField:'DocPropertyName',remoteName:'sForm_ISODocumentQuery.ISODocProperty',tableName:'ISODocProperty',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" ReadOnly="True" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="文件名稱" Editor="text" FieldName="DocName" Format="" maxlength="0" Width="180" ReadOnly="True" Visible="True" NewRow="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="掃描文件" Editor="infofileupload" FieldName="PdfFile" Format="" maxlength="0" Width="180" EditorOptions="filter:'',isAutoNum:true,upLoadFolder:'JB_ADMIN/Form_ISODocument/PdfFile',showButton:true,showLocalFile:false,fileSizeLimited:'20000'" Visible="False" ReadOnly="True" NewRow="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電子原稿" Editor="infofileupload" FieldName="WordFile" Format="" maxlength="0" Width="180" EditorOptions="filter:'',isAutoNum:true,upLoadFolder:'JB_ADMIN/Form_ISODocument/WordFile',showButton:true,showLocalFile:false,fileSizeLimited:'20000'" Visible="False" ReadOnly="True" NewRow="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="文件修訂申請說明" Editor="textarea" FieldName="Reason" MaxLength="0" Width="500" ReadOnly="False" EditorOptions="height:50" NewRow="False" RowSpan="1" Span="2" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="全公司下載" Editor="text" FieldName="IsAllCanDownload" maxlength="0" Width="80" Visible="False" ReadOnly="False" NewRow="False" Span="1" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="部門組下載" Editor="text" FieldName="OrgCanDownload" Width="80" Visible="False" NewRow="False" Span="1" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="個人下載" Editor="text" FieldName="WhoCanDownload" Format="" Width="500" ReadOnly="False" EditorOptions="" NewRow="True" Span="2" Visible="False" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請者" Editor="infocombobox" FieldName="CreateBy" Format="" Width="180" Visible="False" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sForm_ISODocumentQuery.USERS',tableName:'USERS',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="預定發行日期" Editor="text" FieldName="CreateDate" Format="yyyy-mm-dd" Width="180" Visible="True" ReadOnly="True" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="infocombobox" FieldName="LastUpdateBy" Format="" Width="180" Visible="False" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sForm_ISODocumentQuery.USERS',tableName:'USERS',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" MaxLength="0" NewRow="False" ReadOnly="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" MaxLength="0" Visible="False" Width="180" Format="yyyy-mm-dd" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="FlowFlag" Editor="text" FieldName="FlowFlag" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="IsModify" Editor="text" FieldName="IsModify" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="全公司取號" Editor="text" FieldName="IsAllCanDownload1" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="部門組取號" Editor="text" FieldName="OrgCanDownload1" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="個人取號" Editor="text" FieldName="WhoCanDownload1" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="原申請文號" Editor="text" FieldName="FirstDocNO" maxlength="0" NewRow="False" ReadOnly="False" Span="1" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="JQDefault1" runat="server" BindingObjectID="JQDataForm1" EnableTheming="True">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="JQValidate1" runat="server" BindingObjectID="JQDataForm1" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>

            <div ID="JQDialog2" ><%--runat="server" BindingObjectID="dataGridForm" Title="表單號碼" Width="780px"--%>
                <JQTools:JQDataGrid ID="dataGridForm" data-options="pagination:true,view:commandview" RemoteName="sForm_ISODocumentQuery.ISODocumentForm" runat="server" AutoApply="True"
                DataMember="ISODocumentForm" Pagination="True" QueryTitle="查詢條件" EditDialogID="JQDialogForm"
                Title=" " AllowAdd="True" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="DocNO" Editor="text" FieldName="DocNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="表單號碼" Editor="text" FieldName="FormNO" MaxLength="0" Visible="true" Width="90" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="取號說明" Editor="text" FieldName="Remark" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="取號者" Editor="infocombobox" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sForm_ISODocumentQuery.USERS',tableName:'USERS',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="取號日期" Editor="text" FieldName="CreateDate" MaxLength="0" Visible="true" Width="120" Sortable="False" Format="yyyy-mm-dd" />
                    </Columns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="取號" />
                        <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem"
                        Text="Update" Visible="False" />
                        <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem"
                        Text="Delete" Visible="False"  />
                        <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton"
                        OnClick="apply" Text="Apply" Visible="False" />
                        <JQTools:JQToolItem Enabled="True" Icon="icon-cancel" ItemType="easyui-linkbutton" OnClick="cancel" Text="Cancel" Visible="False" />
                        <JQTools:JQToolItem Enabled="True" Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="Query" Visible="False" />
                        <JQTools:JQToolItem Enabled="True" Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Export" Visible="False" />
                    </TooItems>
                </JQTools:JQDataGrid>
                <JQTools:JQDialog ID="JQDialogForm" runat="server" BindingObjectID="dataFormForm" Title="表單取號" Width="380px">
                    <JQTools:JQDataForm ID="dataFormForm" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="ISODocumentForm" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="2" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sForm_ISODocumentQuery.ISODocumentForm" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="dataFormForm_OnLoad">
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="DocNO" Editor="text" FieldName="DocNO" maxlength="0" ReadOnly="True" Width="100" NewRow="False" Span="1" Visible="False" RowSpan="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="FormNO" Editor="text" FieldName="FormNO" maxlength="0" ReadOnly="True" Span="1" Width="100" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="取號說明" Editor="textarea" FieldName="Remark" maxlength="0" ReadOnly="False" Width="200" EditorOptions="height:50" Span="2" NewRow="False" RowSpan="1" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="FirstDocNO" Editor="text" FieldName="FirstDocNO" maxlength="0" NewRow="False" ReadOnly="False" Span="1" Visible="False" Width="80" />
                        </Columns>
                    </JQTools:JQDataForm>
                </JQTools:JQDialog>
            </div>

            <div ID="JQDialog3" ><%--runat="server" BindingObjectID="dataGridForm" Title="表單號碼" Width="780px"--%>
                <JQTools:JQDataGrid ID="dataGridForm1" data-options="pagination:true,view:commandview" RemoteName="sForm_ISODocumentQuery.ISODocumentForm" runat="server" AutoApply="True"
                DataMember="ISODocumentForm" Pagination="True" QueryTitle="查詢條件" EditDialogID=""
                Title=" " AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="DocNO" Editor="text" FieldName="DocNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="表單號碼" Editor="text" FieldName="FormNO" MaxLength="0" Visible="true" Width="90" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="取號說明" Editor="textarea" FieldName="Remark" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="取號者" Editor="infocombobox" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sForm_ISODocumentQuery.USERS',tableName:'USERS',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="取號日期" Editor="text" FieldName="CreateDate" MaxLength="0" Visible="true" Width="120" Sortable="False" Format="" />
                    </Columns>
                </JQTools:JQDataGrid>
                
            </div>
        </div>

        <JQTools:JQDialog ID="JQDialog4" runat="server" DialogLeft="260px" DialogTop="0px" Title="部門組別" Width="450px" OnSubmited="JQDialog4OnSubmited" ><%--OnSubmited="SalesTypeOnSubmited"--%>
                  <div class="easyui-layout" style="height: 480px;">
                    <div data-options="region:'center',title:'',border:false" title="">
                 <JQTools:JQTreeView ID="TreeOrgCanDownload" runat="server" DataMember="OrgCanDownload" idField="ORG_NO" parentField="UPPER_ORG" RemoteName="sForm_ISODocumentQuery.OrgCanDownload" textField="ORG_DESC" Checkbox="True"></JQTools:JQTreeView>
                    </div>
                    </div>
       </JQTools:JQDialog>

        <JQTools:JQDialog ID="JQDialog5" runat="server" DialogLeft="260px" DialogTop="0px" Title="個人" Width="450px" OnSubmited="JQDialog5OnSubmited" ><%--OnSubmited="SalesTypeOnSubmited"--%>
                  <div class="easyui-layout" style="height: 480px;">
                    <div data-options="region:'center',title:'',border:false" title="">
                 <JQTools:JQTreeView ID="TreeWhoCanDownload" runat="server" DataMember="USERS" idField="USERID" parentField="ParentID" RemoteName="sForm_ISODocumentQuery.USERS" textField="USERNAME" Checkbox="True"></JQTools:JQTreeView>
                    </div>
                    </div>
       </JQTools:JQDialog>

    </form>
</body>
</html>
