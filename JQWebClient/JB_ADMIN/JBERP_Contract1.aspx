<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_Contract1.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>
<%--Contract1是續約用--%>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        $(function () {
            $("#JQDialog2").find(".infosysbutton-s").hide();
        });

        //"續聘"按鈕
        function FormatScript_Button1(val, row, index) {
            if (row.ContinueFlag == null && row.FlowFlag == "Z") {//row.FlowFlag != "X" &&
                return $("<a href='#' onClick='myfunction(" + index + ")'>").linkbutton({ plain: false, text: '續約' })[0].outerHTML;
            }
        }
        //"續聘"按鈕OnClick處理函式
        function myfunction(index) {
            $("#dataGridView").datagrid("selectRow", index);
            var row = $("#dataGridView").datagrid("getSelected");
            
            //履約保證相關欄位顯示或隱藏
            if (row.IsGuaranty != '') {
                setTimeout(function () {
                    if (row.IsGuaranty == '是') {
                        ShowFields(['GuarantyNO', 'GuarantyAmount', 'GuarantyEndDate']);
                    } else {
                        $("#dataFormMasterGuarantyNO").val('');
                        $("#dataFormMasterGuarantyAmount").val('');
                        $("#dataFormMasterGuarantyEndDate").datebox('setValue', '');
                        HideFields(['GuarantyNO', 'GuarantyAmount', 'GuarantyEndDate']);
                    }
                }, 500);
            }

            //開啟dialog
            openForm('#JQDialog1', row, 'inserted', 'dialog');
              
        }
        //登入者的群組
        var ConstContractClass = "";

        function OnLoad_dataGridView() {
            if (!$(this).data("firstLoad") && $(this).data("firstLoad", true)) {
                //登入者的群組
                var Userid = getClientInfo("userid");
                var GroupNames = "";
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sERPContract.ERPContract',
                    data: "mode=method&method=" + "GetGroupName" + "&parameters=" + Userid, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != "False") {
                            var rows = $.parseJSON(data);
                            if (rows.length > 0) {
                                for (var i = 0; i < rows.length;i++) {
                                    if (i == rows.length - 1) {
                                        GroupNames = GroupNames + "'" + rows[i]["CENTER_CNAME"] + "'";
                                    } else {
                                        GroupNames = GroupNames + "'"+rows[i]["CENTER_CNAME"] + "',";
                                    }
                                }
                            }
                        } else {

                        }
                    }
                });
                //登入者的群組
                ConstContractClass = GroupNames;
 
                
                var email = Request.getQueryStringByName("em");
                if (ConstContractClass != "") {//有權限群組
                    if (email == 1) {//來自提醒快到期email的連結，顯示 結案+沒續約過+快到期+權責部門在群組內
                        var where = "FlowFlag = 'Z' and ContinueFlag is null and EndDate-CONVERT(date,getdate())<=convert(int,RemindDays) and EndDate-CONVERT(date,getdate())>=0 and ResponsibleDepart in (" + ConstContractClass + ")";
                        $("#dataGridView").datagrid("setWhere", where);
                    } else {
                        //顯示 權責部門在群組內
                        var where = "ResponsibleDepart in (" + ConstContractClass + ")";
                        $("#dataGridView").datagrid("setWhere", where);
                    }
                    //權責部門
                    $("#ResponsibleDepart_Query").combobox('setWhere', "USERID ='" + Userid + "'");
                    //廠商客戶
                    $("#ContractB_Query").combobox('setWhere', "ResponsibleDepart in (" + ConstContractClass + ")");
                } else {//沒權限群組
                    //$("#dataGridView").datagrid("setWhere", "ResponsibleDepart i='123456'");
                    //權責部門
                    $("#ResponsibleDepart_Query").combobox('setWhere', "USERID ='123456'");
                    //廠商客戶
                    $("#ContractB_Query").combobox('setWhere', "ResponsibleDepart ='123456'");
                }
            }
        }
        function queryGrid() {
            var queryArr = [];
            var where = "";
            where = $("#dataGridView").datagrid('getWhere');
            if (where == "") {//沒下條件
                if (ConstContractClass != "") {//有權限群組
                    where = "ResponsibleDepart in (" + ConstContractClass + ")"
                } else {//沒權限群組
                    where = "ResponsibleDepart in (-1)"
                }
            } else {//有下條件
                if (ConstContractClass != "") {
                    queryArr.push("ResponsibleDepart in (" + ConstContractClass + ")");
                } else {
                    queryArr.push("ResponsibleDepart in (-1)");
                }

                if ($("#ResponsibleDepart_Query").combobox('getValue') != '') {
                    queryArr.push("ResponsibleDepart = '" + $("#ResponsibleDepart_Query").combobox('getValue') + "'");
                }
                if ($("#FlowFlag_Query").combobox('getValue') != '') {
                    queryArr.push("FlowFlag = '" + $("#FlowFlag_Query").combobox('getValue') + "'");
                }
                if ($("#EndDate_Query").datebox('getValue') != '') {
                    queryArr.push("EndDate >= '" + $("#EndDate_Query").datebox('getValue') + "'");
                }
                if ($('#EndDate_Query[infolight-options*="~"] ').datebox('getValue') != '') {
                    queryArr.push("EndDate <= '" + $('#EndDate_Query[infolight-options*="~"]  ').datebox('getValue') + "'");
                }
                if ($("#ContractName_Query").val() != '') {
                    queryArr.push("ContractName like '%" + $("#ContractName_Query").val() + "%'");
                }
                if ($("#ContractB_Query").combobox('getValue') != '') {
                    queryArr.push("ContractB = '" + $("#ContractB_Query").combobox('getValue') + "'");
                }
                if ($("#IsValid_Query").combobox('getValue') != '') {
                    var Today = new Date();
                    if ($("#IsValid_Query").combobox('getValue') == '1') {
                        queryArr.push("EndDate >= '" + Today.getFullYear() + "-" + (Today.getMonth() + 1) + "-" + Today.getDate() + "'");
                    } else if ($("#IsValid_Query").combobox('getValue') == '0') {
                        queryArr.push("EndDate < '" + Today.getFullYear() + "-" + (Today.getMonth() + 1) + "-" + Today.getDate() + "'");
                    }
                }
                if ($("#IsGuaranty_Query").combobox('getValue') != '') {
                    queryArr.push("IsGuaranty = '" + $("#IsGuaranty_Query").combobox('getValue') + "'");
                }
                where = queryArr.join(" and ");
            }
            $("#dataGridView").datagrid('setWhere', where);
        }

        function OnLoad_dataFormMaster() {
            var param = Request.getQueryStringByName("p1");
            if (param == '') { param = Request.getQueryStringByName2("p1"); }
            
            ShowAllFields();

            //把查詢畫面編輯的欄位Enable回來
            var EnabledFieldName = ['PhysicalContractNO', 'ContractName', 'Amount', 'Remarks', 'Remarks2', 'RemindDays', 'GuarantyAmount', 'GuarantyNO'];
            var EnabledComboboxName = ['ContractClass', 'BeginDate', 'EndDate', 'ResponsibleDepart', 'IsGuaranty', 'GuarantyEndDate','SignDate','ContractB'];
            EnableFields('#dataFormMaster', EnabledFieldName, EnabledComboboxName);
            //$("#dataFormMasterContractB").refval('enable');

            if (getEditMode($("#dataFormMaster")) == 'inserted') {//新增
                //alert(param);
                //GetContractNO();
                $("#dataFormMasterParentKey").val($("#dataFormMasterContractKey").val());
                $("#dataFormMasterContractKey").val(0);
                //欄位清空
                $("#dataFormMasterFlowFlag").val("");
                //$("#dataFormMasterContractNO").val("");
                var infofileUpload, infofileUploadvalue;
                for (var i = 1; i < 6; i++) {
                    infofileUpload = $('#dataFormMasterAttachment' + i);
                    infofileUploadvalue = $('.info-fileUpload-value', infofileUpload.next());
                    infofileUploadvalue.val("");
                }

                //隱藏下載欄位
                var HiddenFields = ['Attachment1d', 'Attachment2d', 'Attachment3d', 'Attachment4d', 'Attachment5d'];
                HideFields(HiddenFields);

                //var rowData = $("#dataFormMasterContractClass").combobox('getSelectItem');//合約類別
                var ContractClassID = $("#dataFormMasterContractClass").combobox('getValue');
                var arr = GetInfoCommandValue($("#dataFormMasterContractClass"), "ContractClassID='" + ContractClassID + "'");
                var KeepDepart = arr[0];
                var ORG_MAN = arr[1];

                //特定保管部門主管設值
                $("#dataFormMasterAssignChecker").val(ORG_MAN);
                //保管人
                if (KeepDepart == null || KeepDepart == '') {//無特定保管部門
                    //保管人延遲篩選
                    KeeperFilter();
                    $("#dataFormMasterKeeper").closest('td').prev('td').css({ 'color': 'red' });
                } else {//有特定保管部門
                    //保管人鎖定
                    $("#dataFormMasterKeeper").combobox('disable');
                    $("#dataFormMasterKeeper").closest('td').prev('td').css({ 'color': 'black' });
                }

                //停用欄位
                DisableFields('#dataFormMaster', [], ['ContractClass','ContractB']);
                //$("#dataFormMasterContractB").com('disable');
                //是否為外勞部
                $("#dataFormMasterIsForeignDept").val(IsForeignDept());

            } else if (getEditMode($("#dataFormMaster")) == 'viewed' && param == "") {//瀏覽
                //隱藏上傳欄位
                var HiddenFields = ['Attachment1', 'Attachment2', 'Attachment3', 'Attachment4', 'Attachment5'];
                //ShowFields(['ContractNO']);
                HideFields(HiddenFields);
            }else if (getEditMode($("#dataFormMaster")) == 'viewed' && param == "K") {
                //隱藏上傳欄位
                var HiddenFields = ['Attachment1', 'Attachment2', 'Attachment3', 'Attachment4', 'Attachment5'];
                HideFields(HiddenFields);
            } else if (getEditMode($("#dataFormMaster")) == 'updated' && param =='apply') {//被退回&& param != "M" && param != "A"
                //隱藏下載欄位
                var HiddenFields = ['Attachment1d', 'Attachment2d', 'Attachment3d', 'Attachment4d', 'Attachment5d'];
                HideFields(HiddenFields);
            } else if (param == "A") {//會計
                //停用
                var DisabledFieldName = ['PhysicalContractNO', 'ContractName', 'Amount', 'Remarks', 'Remarks2', 'RemindDays', 'GuarantyAmount', 'GuarantyNO'];
                var DisabledComboboxName = ['ContractClass', 'BeginDate', 'EndDate', 'ResponsibleDepart', 'IsGuaranty', 'GuarantyEndDate','SignDate','ContractB'];
                DisableFields('#dataFormMaster', DisabledFieldName, DisabledComboboxName);
                //$("#dataFormMasterContractB").refval('disable');
                //隱藏
                HideFields(['Attachment1', 'Attachment2', 'Attachment3', 'Attachment4', 'Attachment5']);
                //保管人延遲篩選
                KeeperFilter();
            } else if (getEditMode($("#dataFormMaster")) == 'updated' && param == '') {//合約查詢編輯
                //停用
                var DisabledFieldName = ['ContractName', 'Amount', 'Remarks', 'Remarks2', 'GuarantyAmount', 'GuarantyNO', 'RemindDays', 'PhysicalContractNO'];
                var DisabledComboboxName = ['ContractClass', 'BeginDate', 'ResponsibleDepart', 'IsGuaranty', 'SignDate', 'ContractB', 'EndDate', 'GuarantyEndDate'];
                DisableFields('#dataFormMaster', DisabledFieldName, DisabledComboboxName);
                //$("#dataFormMasterContractB").refval('disable');
                //隱藏
                var HiddenFields = ['Attachment1d', 'Attachment2d', 'Attachment3d', 'Attachment4d', 'Attachment5d'];
                HideFields(HiddenFields);
            }

            
            if ((param == "M" && $('#dataFormMasterContractKey').val() != "")) {//主管審核
                //隱藏上傳欄位
                var HiddenFields = ['Attachment1', 'Attachment2', 'Attachment3', 'Attachment4', 'Attachment5'];
                HideFields(HiddenFields);
                //停用全部欄位，為了合約編號給值
                var DisabledFieldName = ['PhysicalContractNO', 'ContractName', 'Amount', 'Remarks', 'Remarks2', 'RemindDays', 'GuarantyAmount', 'GuarantyNO'];
                var DisabledComboboxName = ['ContractClass', 'BeginDate', 'EndDate', 'Keeper','ResponsibleDepart','IsGuaranty','GuarantyEndDate','SignDate','ContractB'];
                DisableFields('#dataFormMaster', DisabledFieldName, DisabledComboboxName);
                //$("#dataFormMasterContractB").refval('disable');
                //取號ContractNO
                //if ($("#dataFormMasterContractNO").val() == "") {
                //    var ContractKey = $('#dataFormMasterContractKey').val();
                //    $.ajax({
                //        type: "POST",
                //        url: '../handler/jqDataHandle.ashx?RemoteName=sERPContract.ERPContract', //連接的Server端，command
                //        data: "mode=method&method=" + "GetContractNO" + "&parameters=" + ContractKey, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                //        cache: false,
                //        async: false,
                //        success: function (data) {
                //            if (data != "False") {
                //                $('#dataFormMasterContractNO').val(data);
                //            } else {
                //                alert("取號失敗");
                //            }
                //        }
                //    });
                //}
            }

            //總經理申請時就取號ContractNO
                //if ($("#dataFormMasterContractNO").val() == "" && getEditMode($("#dataFormMaster")) == 'inserted' && getClientInfo("userid") == '003') {//因總經理申請，後面審核會跳過，故無法取號
                //var ContractKey = $('#dataFormMasterContractKey').val();
                //$.ajax({
                //    type: "POST",
                //    url: '../handler/jqDataHandle.ashx?RemoteName=sERPContract.ERPContract', //連接的Server端，command
                //    data: "mode=method&method=" + "GetContractNO" + "&parameters=" + ContractKey, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                //    cache: false,
                //    async: false,
                //    success: function (data) {
                //        if (data != "False") {
                //            $('#dataFormMasterContractNO').val(data);
                //        } else {
                //            alert("取號失敗");
                //        }
                //    }
                //});
                //}

                //履約保證相關欄位顯示或隱藏
                if ($("#dataFormMasterIsGuaranty").combobox('getValue') != '') {
                    setTimeout(function () {
                        GuarantyHideShow();
                    }, 500);
                }
        }

        //function GetContractNO() {
        //    //取號ContractNO
        //    //if ($("#dataFormMasterContractNO").val() == "") {
        //        //var ContractKey = $('#dataFormMasterContractKey').val();
        //        $.ajax({
        //            type: "POST",
        //            url: '../handler/jqDataHandle.ashx?RemoteName=sERPContract.ERPContract', //連接的Server端，command
        //            data: "mode=method&method=" + "GetContractNO",// + "&parameters=" + ContractKey, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
        //            cache: false,
        //            async: false,
        //            success: function (data) {
        //                if (data != "False") {
        //                    $('#dataFormMasterContractNO').val(data);
        //                } else {
        //                    alert("取號失敗");
        //                }
        //            }
        //        });
        //    //}
        //}

        function OnApplied_dataFormMaster() {
            var param = Request.getQueryStringByName("p1");
            var ParentKey = $("#dataFormMasterParentKey").val();
            if (getEditMode($("#dataFormMaster")) == 'inserted') {//新增狀態
                var userid = getClientInfo("userid");
                //自動起單
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sERPContract.ERPContract',
                    data: "mode=method&method=" + "FlowStartUp" + "&parameters=" + userid + "," + ParentKey, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != "False") {
                            alert('續約申請成功');
                        } else {
                            alert('續約申請失敗');
                        }
                    }
                });
                $("#dataGridView").datagrid("load");
            } 
        }
        //合約類別
        function OnSelectContractClass() {
            //保管部門設值及保管人立即篩選或鎖定
            KeeperDepart_SetValue();
        }
        //權責部門
        function OnSelectResponsibleDepart() {
            //保管部門設值及保管人立即篩選或鎖定
            KeeperDepart_SetValue();
        }
        //履約保證
        function OnSelectIsGuaranty() {
            //履約保證相關欄位顯現或隱藏
            GuarantyHideShow();
        }

        function FormatScriptFlowFlag(val) {
            if (val == 'N') {
                return '新申請';
            } else if (val == 'P') {
                return '流程中';
            } else if (val == 'Z') {
                return '結案';
            } else if (val == 'X') {
                return '作廢';
            }
        }

        function OnApply_dataFormMaster() {
            var param = Request.getQueryStringByName("p1");
            //var rowData = $("#dataFormMasterContractClass").combobox('getSelectItem');//合約類別
            var ContractClassID = $("#dataFormMasterContractClass").combobox('getValue');
            var arr = GetInfoCommandValue($("#dataFormMasterContractClass"), "ContractClassID='" + ContractClassID + "'");
            var KeepDepart = arr[0];
            var ORG_MAN = arr[1];

            if (getEditMode($("#dataFormMaster")) == 'inserted') {//新申請時
                //無特定保管部門
                if ((KeepDepart == null || KeepDepart == '') && ($("#dataFormMasterKeeper").combobox('getValue') == '' || $("#dataFormMasterKeeper").combobox('getValue') == '---請選擇---')) {
                    alert('保管人必填');
                    return false;
                }
            } else if (param == "A") {//會簽會計
                if (($("#dataFormMasterKeeper").combobox('getValue') == '' || $("#dataFormMasterKeeper").combobox('getValue') == '---請選擇---')) {
                    alert('保管人必填');
                    return false;
                }
            }
        }

        var AlterCounts_FormatScript = function (value, row, index) {
            if (value > 0) {
                var fieldName = this.field;
                return $('<a>', { href: 'javascript: void(0)', title: '', onclick: "OpenAlterDialog.call(this,'')" }).linkbutton({ iconCls: '', plain: false, text: value })[0].outerHTML;
            }
        }
        var OpenAlterDialog = function (command) {
            var rowIndex = parseInt($(this).closest('tr').attr('datagrid-row-index'));
            var rowData = $("#dataGridView").datagrid('selectRow', rowIndex).datagrid('getSelected');
            var FiltStr = "ContractNO = " + "'" + rowData.ContractNO + "'";
            $("#JQDataGrid1").datagrid('setWhere', FiltStr);
            openForm('#JQDialog2', {}, "", 'dialog');
            return true;
        }
        //工具------------------------------------------------------------------------------------
        function IsForeignDept() {
            var userid = getClientInfo("userid");
            var counts = 0;
            if (userid != "") {
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sERPContract.ERPContract',
                    data: "mode=method&method=IsForeignDept&parameters=" + userid,
                    cache: false,
                    async: false,
                    success: function (data) {
                        counts = $.parseJSON(data);
                    }
                });
            }
            return counts;
        }
        function HideFields(FieldNames) {
            var FormName = '#dataFormMaster';
            $.each(FieldNames, function (index, value) {
                $(FormName + value).closest('td').prev('td').hide();//closest上一層selector,prev下一個selector
                $(FormName + value).closest('td').hide();
            });
        }
        function ShowFields(FieldNames) {
            var FormName = '#dataFormMaster';
            $.each(FieldNames, function (index, value) {
                $(FormName + value).closest('td').prev('td').show();//closest上一層selector,prev下一個selector
                $(FormName + value).closest('td').show();
            });
        }
        function ShowAllFields() {
            var FormName = '#dataFormMaster';
            var FieldNames = ['Attachment1', 'Attachment2', 'Attachment3', 'Attachment4', 'Attachment5', 'Attachment1d', 'Attachment2d', 'Attachment3d', 'Attachment4d', 'Attachment5d', 'PhysicalContractNO', 'ContractName', 'Amount', 'Remarks', 'Remarks2', 'RemindDays', 'GuarantyAmount', 'GuarantyNO', 'ContractClass', 'BeginDate', 'EndDate', 'ResponsibleDepart', 'IsGuaranty', 'GuarantyEndDate', 'ContractB', 'ContractNO']
            $.each(FieldNames, function (index, value) {
                $(FormName + value).closest('td').prev('td').show();//closest上一層selector,prev下一個selector
                $(FormName + value).closest('td').show();
            });
        }
        function DisableFields(FormName, DisabledFieldNames, DisabledComboboxNames) {
            $.each(DisabledFieldNames, function (index, value) {
                $(FormName + value).attr('disabled', true);
            });
            $.each(DisabledComboboxNames, function (index, value) {
                $(FormName + value).combobox('disable');
            });
        }
        function EnableFields(FormName, DisabledFieldNames, DisabledComboboxNames) {
            $.each(DisabledFieldNames, function (index, value) {
                $(FormName + value).attr('disabled', false);
            });
            $.each(DisabledComboboxNames, function (index, value) {
                $(FormName + value).combobox('enable');
            });
        }
        //保管人Combobox延遲篩選(onloaddataform用)
        function KeeperFilter() {
            $("#dataFormMasterKeeper").combobox('disable');
            setTimeout(function () {
                var departText = $("#dataFormMasterKeepDepart").combobox('getValue');
                var where = "org_no='" + departText + "'";
                $("#dataFormMasterKeeper").combobox('setWhere', where);
                $("#dataFormMasterKeeper").combobox('enable');
            }, 1000);
        }
        //保管人立即篩選
        function KeeperFilter0() {
            $("#dataFormMasterKeeper").combobox('setValue', '');
            $("#dataFormMasterKeeper").combobox('disable');
            var departText = $("#dataFormMasterKeepDepart").combobox('getValue');
            var where = "org_no='" + departText + "'";
            $("#dataFormMasterKeeper").combobox('setWhere', where);
            $("#dataFormMasterKeeper").combobox('enable');
        }
        //保管部門設值及保管人立即篩選或鎖定
        function KeeperDepart_SetValue() {
            var ContractClass = $("#dataFormMasterContractClass").combobox('getValue');//合約類別ID
            var ResponsibleDepart = $("#dataFormMasterResponsibleDepart").combobox('getValue');//權責部門ID
            //兩個都有填才進行保管部門設值
            if ((ContractClass != '' && ContractClass != '---請選擇---') && (ResponsibleDepart != '---請選擇---' && ResponsibleDepart != '')) {
                //var rowData = $("#dataFormMasterContractClass").combobox('getSelectItem');//合約類別的保管部門
                var ContractClassID = $("#dataFormMasterContractClass").combobox('getValue');
                var arr = GetInfoCommandValue($("#dataFormMasterContractClass"), "ContractClassID='" + ContractClassID + "'");
                var KeepDepart = arr[0];
                var ORG_MAN = arr[1];
                //if (rowData.KeepDepart == null || rowData.KeepDepart == '') {//合約類別無特定保管部門
                if (KeepDepart == null || KeepDepart == '') {//合約類別無特定保管部門
                    //保管部門設值
                    //$("#dataFormMasterKeepDepart").val(ResponsibleDepart);
                    $("#dataFormMasterKeepDepart").combobox('setValue', ResponsibleDepart);
                    //保管人立即篩選
                    KeeperFilter0();
                    $("#dataFormMasterKeeper").closest('td').prev('td').css({ 'color': 'red' });
                } else {//合約類別有特定保管部門
                    $("#dataFormMasterKeepDepart").combobox('setValue', KeepDepart);
                    //保管人鎖定
                    $("#dataFormMasterKeeper").combobox('setValue', '');
                    $("#dataFormMasterKeeper").combobox('disable');
                    $("#dataFormMasterKeeper").closest('td').prev('td').css({ 'color': 'black' });
                }

                //特定保管部門主管角色
                $("#dataFormMasterAssignChecker").val(ORG_MAN);
            }
        }
        function GuarantyHideShow() {
            if ($("#dataFormMasterIsGuaranty").combobox('getValue') == '是') {
                ShowFields(['GuarantyNO','GuarantyAmount', 'GuarantyEndDate']);
            } else {
                $("#dataFormMasterGuarantyNO").val('');
                $("#dataFormMasterGuarantyAmount").val('');
                $("#dataFormMasterGuarantyEndDate").datebox('setValue', '');
                HideFields(['GuarantyNO', 'GuarantyAmount', 'GuarantyEndDate']);
            }
        }

        //呼叫dll指定的infoCommand
        function GetInfoCommandValue(controller, where) {
            var remoteName = getInfolightOption(controller).remoteName;
            var tableName = getInfolightOption(controller).tableName;
            // var valueField = getInfolightOption(infoRefval).valueField;
            var returnValue = [];
            $.ajax({
                type: "POST",
                dataType: 'json',
                url: getRemoteUrl(remoteName, tableName, false) + "&whereString=" + encodeURIComponent(where),
                data: { rows: 1 },
                cache: false,
                async: false,
                success: function (data) {
                    if (data.length > 0) {
                        var valueArr = []
                        valueArr[0] = data[0]["KeepDepart"];
                        valueArr[1] = data[0]["ORG_MAN"];
                        returnValue = valueArr;
                    }
                },
                error: function (data) { }
            });
            return returnValue;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERPContract.ERPContract" runat="server" AutoApply="True"
                DataMember="ERPContract" Pagination="True" QueryTitle="查詢條件" EditDialogID="JQDialog1"
                Title="合約續約登錄" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True" OnLoadSuccess="OnLoad_dataGridView">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption=" " Editor="text" FieldName="Button1" Format="" FormatScript="FormatScript_Button1" Frozen="True" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="ContractKey" Editor="numberbox" FieldName="ContractKey" Format="" Visible="False" Width="50" />
                    <JQTools:JQGridColumn Alignment="right" Caption="ParentKey" Editor="numberbox" FieldName="ParentKey" Format="" Visible="False" Width="50" />
                    <JQTools:JQGridColumn Alignment="left" Caption="合約名稱" Editor="text" FieldName="ContractName" Format="" MaxLength="0" Visible="True" Width="120" Frozen="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶/廠商" Editor="infocombobox" FieldName="ContractB" Frozen="True" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120" Format="" EditorOptions="valueField:'ID',textField:'Name',remoteName:'sERPContract.VenderCustomer',tableName:'VenderCustomer',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="合約編號" Editor="text" FieldName="ContractNO" Format="" MaxLength="0" Visible="true" Width="60" />
                    
                    <JQTools:JQGridColumn Alignment="left" Caption="流程狀態" Editor="infocombobox" FieldName="FlowFlag" Format="" MaxLength="0" Visible="true" Width="50" EditorOptions="items:[{value:'N',text:'新申請',selected:'false'},{value:'P',text:'流程中',selected:'false'},{value:'X',text:'作廢',selected:'false'},{value:'Z',text:'結案',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FormatScript="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="合約類別" Editor="infocombobox" FieldName="ContractClass" Format="" MaxLength="0" Visible="true" Width="80" EditorOptions="valueField:'ContractClassID',textField:'ContractClass',remoteName:'sERPContract.ContractClass',tableName:'ContractClass',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="保管人" Editor="infocombobox" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sERPContract.USERS',tableName:'USERS',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="Keeper" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="金額" Editor="numberbox" FieldName="Amount" MaxLength="0" Visible="true" Width="80" Format="N" />
                    <JQTools:JQGridColumn Alignment="left" Caption="紙本合約編號" Editor="text" FieldName="PhysicalContractNO" MaxLength="0" Visible="true" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="起日" Editor="text" FieldName="BeginDate" Format="yyyy-mm-dd" Visible="true" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="迄日" Editor="text" FieldName="EndDate" Format="yyyy-mm-dd" Visible="true" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="提醒天數" Editor="numberbox" FieldName="RemindDays" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="履約保證到期日" Editor="text" FieldName="GuarantyEndDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" Format="yyyy-mm-dd">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="異動次數" Editor="text" FieldName="AlterCounts" FormatScript="AlterCounts_FormatScript" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    
                    <JQTools:JQGridColumn Alignment="left" Caption="簽約日期" Editor="text" FieldName="SignDate" Format="yyyy-mm-dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    
                    <JQTools:JQGridColumn Alignment="left" Caption="權責部門" Editor="infocombobox" FieldName="ResponsibleDepart" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sERPContract.SYS_ORG',tableName:'SYS_ORG',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="保管部門" Editor="infocombobox" FieldName="KeepDepart" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sERPContract.SYS_ORG',tableName:'SYS_ORG',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="履約保證是否" Editor="text" FieldName="IsGuaranty" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="履約保證票號" Editor="text" FieldName="GuarantyNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="履約保證金額" Editor="text" FieldName="GuarantyAmount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" Format="N">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="注意事項" Editor="text" FieldName="Remarks" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="Remarks2" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="附件1" Editor="text" FieldName="Attachment1" Format="download,folder:JB_ADMIN/Contract/Attachment1" MaxLength="0" Visible="true" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="附件2" Editor="text" FieldName="Attachment2" Format="download,folder:JB_ADMIN/Contract/Attachment2" MaxLength="0" Visible="true" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="附件3" Editor="text" FieldName="Attachment3" Format="download,folder:JB_ADMIN/Contract/Attachment3" MaxLength="0" Visible="true" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="附件4" Editor="text" FieldName="Attachment4" Format="download,folder:JB_ADMIN/Contract/Attachment4" MaxLength="0" Visible="true" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="附件5" Editor="text" FieldName="Attachment5" Format="download,folder:JB_ADMIN/Contract/Attachment5" MaxLength="0" Visible="true" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="合約屬性(是否已續過約)" Editor="text" FieldName="ContinueFlag" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="50">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="建立者" Editor="infocombobox" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sERPContract.USERS',tableName:'USERS',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="text" FieldName="CreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="保證金返還日期" Editor="text" FieldName="InOutDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="保證金返還金額" Editor="text" FieldName="WarrantAmount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" Visible="False" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" Visible="False" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消" Visible="False"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" Visible="False" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="權責部門" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CENTER_CNAME',textField:'CENTER_ENAME',remoteName:'sERPContract.CenterCname',tableName:'CenterCname',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ResponsibleDepart" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="流程狀態" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'},{value:'N',text:'新申請',selected:'false'},{value:'P',text:'流程中',selected:'false'},{value:'Z',text:'結案',selected:'false'},{value:'X',text:'作廢',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="FlowFlag" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="合約到期日" Condition="&gt;=" DataType="datetime" Editor="datebox" FieldName="EndDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="~" Condition="&lt;=" DataType="datetime" Editor="datebox" FieldName="EndDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="合約名稱" Condition="%%" DataType="string" Editor="text" FieldName="ContractName" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="122" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶/廠商" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ContractB',textField:'Name',remoteName:'sERPContract.ContractB',tableName:'ContractB',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ContractB" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="180" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="合約狀態" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'},{value:'1',text:'有效(未到期)',selected:'false'},{value:'0',text:'無效(過期)',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="IsValid" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="履約保證" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'},{value:'是',text:'是',selected:'false'},{value:'否',text:'否',selected:'false'}],checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="IsGuaranty" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="合約續約登錄" DialogTop="50px" Width="600px" >
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="ERPContract" HorizontalColumnsCount="2" RemoteName="sERPContract.ERPContract" IsAutoPageClose="True" IsAutoSubmit="True" IsShowFlowIcon="True" OnLoadSuccess="OnLoad_dataFormMaster" OnApplied="OnApplied_dataFormMaster" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPause="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApply="OnApply_dataFormMaster" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="ContractKey" Editor="numberbox" FieldName="ContractKey" Format="" Width="180" ReadOnly="True" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ParentKey" Editor="numberbox" FieldName="ParentKey" Format="" Width="180" ReadOnly="True" Visible="False" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="合約編號" Editor="text" FieldName="ContractNO" Format="" Width="180" ReadOnly="True" maxlength="0" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="紙本合約編號" Editor="text" FieldName="PhysicalContractNO" Width="180" maxlength="0" ReadOnly="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="合約名稱" Editor="text" FieldName="ContractName" Format="" Width="180" maxlength="0" ReadOnly="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶/廠商" Editor="infocombobox" FieldName="ContractB" Format="" Width="184" maxlength="0" ReadOnly="False" EditorOptions="valueField:'ID',textField:'Name',remoteName:'sERPContract.VenderCustomer',tableName:'VenderCustomer',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" NewRow="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="合約類別" Editor="infocombobox" FieldName="ContractClass" Format="" Width="184" EditorOptions="valueField:'ContractClassID',textField:'ContractClass',remoteName:'sERPContract.ContractClass',tableName:'ContractClass',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:OnSelectContractClass,panelHeight:200" ReadOnly="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="權責部門" Editor="infocombobox" FieldName="ResponsibleDepart" Width="184" EditorOptions="valueField:'CENTER_CNAME',textField:'CENTER_ENAME',remoteName:'sERPContractGroup.ERPContractGroup',tableName:'ERPContractGroup',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,onSelect:OnSelectResponsibleDepart,panelHeight:200" maxlength="0" ReadOnly="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="保管部門" Editor="infocombobox" FieldName="KeepDepart" ReadOnly="True" Visible="True" Width="180" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sERPContract.SYS_ORG',tableName:'SYS_ORG',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="保管人" Editor="infocombobox" FieldName="Keeper" ReadOnly="True" Width="184" maxlength="0" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sERPContract.GroupUsers',tableName:'GroupUsers',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="起日" Editor="datebox" FieldName="BeginDate" Format="" ReadOnly="False" Width="184" Visible="True" maxlength="0" EditorOptions="dateFormat:'datetime',showTimeSpinner:false,showSeconds:true,editable:false" />

                        <JQTools:JQFormColumn Alignment="left" Caption="迄日" Editor="datebox" FieldName="EndDate" Format="" Width="184" ReadOnly="False" Visible="True" EditorOptions="dateFormat:'datetime',showTimeSpinner:false,showSeconds:true,editable:false" />
                        <JQTools:JQFormColumn Alignment="left" Caption="簽約日期" Editor="datebox" FieldName="SignDate" Width="184" maxlength="0" ReadOnly="False" Visible="True" NewRow="False" Span="1" EditorOptions="dateFormat:'datetime',showTimeSpinner:false,showSeconds:true,editable:false" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="金額" Editor="numberbox" FieldName="Amount" ReadOnly="False" Visible="True" Width="180" MaxLength="0" NewRow="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="到期前幾天提醒" Editor="numberbox" FieldName="RemindDays" ReadOnly="False" Width="180" Span="1" MaxLength="0" NewRow="False" RowSpan="1" Visible="True" />
                        
                        <JQTools:JQFormColumn Alignment="left" Caption="履約保證" Editor="infocombobox" FieldName="IsGuaranty" ReadOnly="False" Span="1" Width="184" EditorOptions="items:[{value:'是',text:'是',selected:'false'},{value:'否',text:'否',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,onSelect:OnSelectIsGuaranty,panelHeight:50" />
                        
                        <JQTools:JQFormColumn Alignment="left" Caption="履約保證票號" Editor="text" FieldName="GuarantyNO" maxlength="0" ReadOnly="False" Visible="True" Width="180" NewRow="False" Span="1" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="履約保證金額" Editor="numberbox" FieldName="GuarantyAmount" Width="180" Span="1" ReadOnly="False" Visible="True" maxlength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="履約保證到期日" Editor="datebox" FieldName="GuarantyEndDate" ReadOnly="False" Span="1" Width="184" Visible="True" EditorOptions="dateFormat:'datetime',showTimeSpinner:false,showSeconds:true,editable:false" />
                        
                        <JQTools:JQFormColumn Alignment="left" Caption="流程狀態" Editor="text" FieldName="FlowFlag" Format="" maxlength="0" Width="180" Span="1" ReadOnly="True" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="注意事項" Editor="textarea" FieldName="Remarks" maxlength="0" Width="453" EditorOptions="height:60" NewRow="False" Span="2" Format="" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="textarea" FieldName="Remarks2" MaxLength="0" NewRow="False" Width="453" EditorOptions="height:60" Span="2" ReadOnly="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附件1" Editor="infofileupload" FieldName="Attachment1" Format="" Width="430" EditorOptions="filter:'',isAutoNum:false,upLoadFolder:'JB_ADMIN/Contract/Attachment1',showButton:true,showLocalFile:false,fileSizeLimited:'10000'" NewRow="True" Span="2" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附件2" Editor="infofileupload" FieldName="Attachment2" Format="" Width="430" EditorOptions="filter:'',isAutoNum:false,upLoadFolder:'JB_ADMIN/Contract/Attachment2',showButton:true,showLocalFile:false,fileSizeLimited:'10000'" ReadOnly="False" NewRow="True" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附件3" Editor="infofileupload" FieldName="Attachment3" Format="" Width="430" EditorOptions="filter:'',isAutoNum:false,upLoadFolder:'JB_ADMIN/Contract/Attachment3',showButton:true,showLocalFile:false,fileSizeLimited:'10000'" ReadOnly="False" NewRow="True" Span="2" MaxLength="0" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附件4" Editor="infofileupload" FieldName="Attachment4" Format="" Width="430" EditorOptions="filter:'',isAutoNum:false,upLoadFolder:'JB_ADMIN/Contract/Attachment4',showButton:true,showLocalFile:false,fileSizeLimited:'10000'" ReadOnly="False" NewRow="True" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附件5" Editor="infofileupload" FieldName="Attachment5" Format="" Width="430" ReadOnly="False" maxlength="0" Visible="True" NewRow="True" Span="2" EditorOptions="filter:'',isAutoNum:false,upLoadFolder:'JB_ADMIN/Contract/Attachment5',showButton:true,showLocalFile:false,fileSizeLimited:'10000'" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附件1" Editor="text" FieldName="Attachment1d" Format="download,folder:JB_ADMIN/Contract/Attachment1" ReadOnly="False" Width="430" maxlength="0" NewRow="True" RowSpan="1" Span="2" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附件2" Editor="text" FieldName="Attachment2d" Format="download,folder:JB_ADMIN/Contract/Attachment2" maxlength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="430" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附件3" Editor="text" FieldName="Attachment3d" Format="download,folder:JB_ADMIN/Contract/Attachment3" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="430" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附件4" Editor="text" FieldName="Attachment4d" Format="download,folder:JB_ADMIN/Contract/Attachment4" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="430" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附件5" Editor="text" FieldName="Attachment5d" ReadOnly="False" Visible="True" Width="430" Format="download,folder:JB_ADMIN/Contract/Attachment5" MaxLength="0" NewRow="True" RowSpan="1" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="特定保管部門主管" Editor="text" FieldName="AssignChecker" ReadOnly="False" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="IsForeignDept" Editor="text" FieldName="IsForeignDept" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="RemindDays" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="ContractNO" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                        <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ContractName" RemoteMethod="False" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ContractB" RemoteMethod="False" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ContractClass" RemoteMethod="False" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="BeginDate" RemoteMethod="False" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="EndDate" RemoteMethod="False" ValidateType="None" />
                            <JQTools:JQValidateColumn CheckNull="True" FieldName="ResponsibleDepart" RemoteMethod="False" ValidateType="None" />
                            <JQTools:JQValidateColumn CheckNull="True" FieldName="RemindDays" RangeFrom="0" RangeTo="999" RemoteMethod="False" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
            <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="" DialogLeft="50px" Title="異動紀錄" Width="1000px">
                <JQTools:JQDataGrid ID="JQDataGrid1" runat="server" DataMember="ERPContractAlter" RemoteName="sERPContract.ERPContractAlter" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="合約異動編號" Editor="text" FieldName="ContractAlterNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="合約編號" Editor="text" FieldName="ContractNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="紙本合約編號" Editor="text" FieldName="PhysicalContractNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="起日" Editor="text" FieldName="BeginDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" Format="yyyy-mm-dd">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="迄日" Editor="text" FieldName="EndDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" Format="yyyy-mm-dd">
                        </JQTools:JQGridColumn>
                        
                        <JQTools:JQGridColumn Alignment="left" Caption="前幾日提醒" Editor="text" FieldName="RemindDays" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="履約保證到期日" Editor="text" FieldName="GuarantyEndDate" Format="yyyy-mm-dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="IsForeignDept" Editor="text" FieldName="IsForeignDept" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="AssignChecker" Editor="text" FieldName="AssignChecker" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="Keeper" Editor="text" FieldName="Keeper" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="異動狀態" Editor="infocombobox" FieldName="FlowFlag" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" EditorOptions="items:[{value:'N',text:'新申請',selected:'false'},{value:'P',text:'流程中',selected:'false'},{value:'Z',text:'結案',selected:'false'},{value:'X',text:'作廢',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="建立者" Editor="infocombobox" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sERPContract.USERS',tableName:'USERS',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="text" FieldName="CreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70" Format="yyyy-mm-dd">
                        </JQTools:JQGridColumn>
                    </Columns>
                    <TooItems>
                        <JQTools:JQToolItem Enabled="True" Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="Insert" Visible="False" />
                        <JQTools:JQToolItem Enabled="True" Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="Update" Visible="False" />
                        <JQTools:JQToolItem Enabled="True" Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="Delete" Visible="False" />
                        <JQTools:JQToolItem Enabled="True" Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="Apply" Visible="False" />
                        <JQTools:JQToolItem Enabled="True" Icon="icon-cancel" ItemType="easyui-linkbutton" OnClick="cancel" Text="Cancel" Visible="False" />
                        <JQTools:JQToolItem Enabled="True" Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="Query" Visible="False" />
                        <JQTools:JQToolItem Enabled="True" Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Export" Visible="False" />
                    </TooItems>
                </JQTools:JQDataGrid>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
