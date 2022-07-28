<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_SecurityDeposit1.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        $(function () {

            $('#dataFormMasterRequisitionNO').after($('#ReqButton'));
            $("#ReqButton").hide();

            //請款單的預定付款日期
            $('#JQDataForm1PlanPayDate').datebox({
                onSelect: function (date) {
                    ChkException();
                }
            }).combo('textbox').blur(function () {
                ChkException();
            });

            var userid = getClientInfo("UserID");
            $('#CreateBy_Query').combobox('setValue', userid);
        });
        function queryGrid() {
            //alert($("#dataGridView").datagrid('getWhere'));
            var result = [];
            var aVal = '';

            aVal = $('#Name_Query').val();
            if (aVal != '') {
                result.push("Name like '%" + aVal + "%'");
            }

            aVal = $("#CreateBy_Query").combobox('getValue');
            if (aVal != '') {
                result.push("CreateBy = '" + aVal + "'");
            }

            aVal = $("#RestOfWarrantAmount_Query").combobox('getValue');
            if (aVal != '') {
                if(aVal=='1')
                    result.push("RestOfWarrantAmount <> 0 and Flowflag='Z'");
                else if(aVal =='2')
                    result.push("RestOfWarrantAmount = 0 and Flowflag='Z'");
                else if (aVal == '3') {
                    result.push("Flowflag in('N','P')");
                }
            }

            aVal = $("#DepositProperty_Query").combobox('getValue');
            if (aVal != '') {
                result.push("DepositProperty = '" + aVal + "'");
            }
      
            var filtstr = result.join(' and ');
            
        $("#dataGridView").datagrid('setWhere', filtstr);
    }
        
    //"沖銷"按鈕
        function FormatScript_Button1(val, row, index) {
            if (row.Flowflag == "Z" && Number(row.RestOfWarrantAmount)!=0) {//row.FlowFlag != "X" &&  //row.ContinueFlag == null &&
                return $("<a href='#' onClick='myfunction(" + index + ")'>").linkbutton({ plain: false, text: '沖銷單申請' })[0].outerHTML;
            }
        }

        //"續聘"按鈕OnClick處理函式
        function myfunction(index) {
            $("#dataGridView").datagrid("selectRow", index);
            var row = $("#dataGridView").datagrid("getSelected");

            //履約保證相關欄位顯示或隱藏
            //if (row.IsGuaranty != '') {
            //    setTimeout(function () {
            //        if (row.IsGuaranty == '是') {
            //            ShowFields(['GuarantyNO', 'GuarantyAmount', 'GuarantyEndDate']);
            //        } else {
            //            $("#dataFormMasterGuarantyNO").val('');
            //            $("#dataFormMasterGuarantyAmount").val('');
            //            $("#dataFormMasterGuarantyEndDate").datebox('setValue', '');
            //            HideFields(['GuarantyNO', 'GuarantyAmount', 'GuarantyEndDate']);
            //        }
            //    }, 500);
            //}

            //開啟dialog
            openForm('#JQDialog1', row, 'inserted', 'dialog');

        }

        function OnLoad_dataFormMaster() {
            //請款單按鈕的顯示與隱藏
            if ($("#dataFormMasterDepositProperty").combobox('getValue') == '2') {
                $("#ReqButton").show();
            } else {
                $("#ReqButton").hide();
            }

            //var param = Request.getQueryStringByName("p1");
            //if (param == '') { param = Request.getQueryStringByName2("p1"); }

            //ShowAllFields();

            ////把查詢畫面編輯的欄位Enable回來
            //var EnabledFieldName = ['PhysicalContractNO', 'ContractName', 'Amount', 'Remarks', 'Remarks2', 'RemindDays', 'GuarantyAmount', 'GuarantyNO'];
            //var EnabledComboboxName = ['ContractClass', 'BeginDate', 'EndDate', 'ResponsibleDepart', 'IsGuaranty', 'GuarantyEndDate', 'SignDate', 'ContractB'];
            //EnableFields('#dataFormMaster', EnabledFieldName, EnabledComboboxName);
            ////$("#dataFormMasterContractB").refval('enable');

            //if (getEditMode($("#dataFormMaster")) == 'inserted') {//新增

            //    $("#dataFormMasterParentKey").val($("#dataFormMasterContractKey").val());
            //    $("#dataFormMasterContractKey").val(0);
            //    //欄位清空
            //    $("#dataFormMasterFlowFlag").val("");
            //    //$("#dataFormMasterContractNO").val("");
            //    var infofileUpload, infofileUploadvalue;
            //    for (var i = 1; i < 6; i++) {
            //        infofileUpload = $('#dataFormMasterAttachment' + i);
            //        infofileUploadvalue = $('.info-fileUpload-value', infofileUpload.next());
            //        infofileUploadvalue.val("");
            //    }

            //    //隱藏下載欄位
            //    var HiddenFields = ['Attachment1d', 'Attachment2d', 'Attachment3d', 'Attachment4d', 'Attachment5d'];
            //    HideFields(HiddenFields);

            //    //var rowData = $("#dataFormMasterContractClass").combobox('getSelectItem');//合約類別
            //    var ContractClassID = $("#dataFormMasterContractClass").combobox('getValue');
            //    var arr = GetInfoCommandValue($("#dataFormMasterContractClass"), "ContractClassID='" + ContractClassID + "'");
            //    var KeepDepart = arr[0];
            //    var ORG_MAN = arr[1];

            //    //特定保管部門主管設值
            //    $("#dataFormMasterAssignChecker").val(ORG_MAN);
            //}   
            //alert("dataFormMaster_onloadsuccess");

            //if (getEditMode($("#dataFormMaster")) == 'inserted') {
                
            //}
            //var RestOfWarrantAmount = $("#dataFormMasterRestOfWarrantAmount").val();
            //alert(RestOfWarrantAmount);
            //var DepositAmount = $("#dataFormMasterDepositAmount").val();
            var row = $("#dataGridView").datagrid('getSelected');
            $("#dataFormMasterWarrantAmount").numberbox('setValue', row.RestOfWarrantAmount);

            $("#dataFormMasterVoucherNO").val('');

            $("#dataFormMasterInOutDate").datebox('setValue', '');
            $("#dataFormMasterInOutWay").combobox('setValue', '');
            $("#dataFormMasterNotes").val('');
            $("#dataFormMasterRequisitionNO").val('');
            
            //setTimeout(function () {
                
            //}, 8000);

        }
        function OnApply_dataFormMaster() {
            //檢查
            //if ($("#dataFormMasterInOutWay").combobox('getValue') == '2' && $("#dataFormMasterCusSupplierAccName").val() == '') {
            //    alert("收支方式選匯款，客戶供應商戶名為必填");
            //    $("#dataFormMasterCusSupplierAccName").focus();
            //    return false;
            //}

            var pre = confirm("確定起單-保證金沖銷單?");
            if (pre == true) {
                return true;
            } else {
                return false;
            }
        }

        //function OnSelect_dataFormMasterDepositNO(row){
        //    $("#dataFormMasterDepositAmount").numberbox('setValue', row.DepositAmount);
        //    $("#dataFormMasterDepositAmount").focus();
        //    $("#dataFormMasterWarrantAmount").numberbox('setValue', row.DepositAmount);
        //    $("#dataFormMasterWarrantAmount").focus();
        //    $("#dataFormMasterCusSupplier").refval('setValue', row.CusSupplier);
        //    $("#dataFormMasterDepositProperty").combobox('setValue', row.DepositProperty);

        //    if (row.DepositProperty == '2') {
        //        $("#ReqButton").show();
        //    } else {
        //        $("#ReqButton").hide();
        //    }
        //}
        function OnSelect_DepositProperty() {

        }

        function OnApplied_dataFormMaster() {
            //var param = Request.getQueryStringByName("p1");
            //var ParentKey = $("#dataFormMasterParentKey").val();
            if (getEditMode($("#dataFormMaster")) == 'inserted') {//新增狀態
                var userid = getClientInfo("userid");
                //自動起單
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sForm_SecurityDeposit.SecurityDeposit',
                    data: "mode=method&method=" + "FlowStartUp" + "&parameters=" + userid, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != "False") {
                            alert('保證金沖銷單申請成功');
                        } else {
                            alert('保證金沖銷單申請失敗');
                        }
                    }
                });
                $("#dataGridView").datagrid("load");
            }
        }

        //OnClick_ReqButton(請款單按鈕)
        function ReqButtonOnClick() {
            openForm('#JQDialog3', {}, "inserted", 'dialog');
        }

        //(工具)
        function DisableFields(FormName, DisabledFieldNames, DisabledComboboxNames) {
            $.each(DisabledFieldNames, function (index, value) {
                $(FormName + value).attr('disabled', true);
            });
            $.each(DisabledComboboxNames, function (index, value) {
                $(FormName + value).combobox('disable');
            });
        }

        //(工具)
        function RedTd(FormName, FieldNames) {
            $.each(FieldNames, function (index, value) {
                $(FormName + value).closest('td').prev('td').css({ 'color': 'red' });
            });
        }

        //(工具)
        function CheckCombobox(FormName, fieldName) {
            var FieldValue = $(FormName + fieldName).combobox('getValue');
            var FieldNameC = $(FormName + fieldName).closest('td').prev('td').text();
            if ($.trim(FieldValue) == '' || FieldValue == undefined) {
                alert('注意!!,未選取 " ' + FieldNameC + ' " ,請選取!!');
                $(FormName + fieldName).focus();
                return false;
            }
        }

        //(工具)
        function CheckDatebox(FormName, fieldName) {
            var FieldValue = $(FormName + fieldName).datebox('getValue');
            var FieldNameC = $(FormName + fieldName).closest('td').prev('td').text();
            if ($.trim(FieldValue) == '' || FieldValue == undefined) {
                alert('注意!!,未選取 " ' + FieldNameC + ' " ,請選取!!');
                $(FormName + fieldName).datebox("textbox").focus();
                return false;
            }
        }

        //(工具)
        function CheckVal(FormName, fieldName) {
            var FieldValue = $(FormName + fieldName).val();
            var FieldNameC = $(FormName + fieldName).closest('td').prev('td').text();
            if ($.trim(FieldValue) == '' || FieldValue == undefined) {
                if (fieldName != 'InsuraceAmt') {
                    alert('注意!!,未填寫 " ' + FieldNameC + ' " ,請填寫!!');
                    $(FormName + fieldName).focus();
                    return false;
                } else if (fieldName == 'InsuraceAmt') {
                    alert('注意!!，投保總金額無金額，請填寫「旅平險申請」的「請款金額」!!');
                    $(FormName + fieldName).focus();
                    return false;
                }
            }
        }

        //(工具)
        function CheckUpload(FormName, fieldName) {
            var infofileUpload = $(FormName + fieldName);
            var FieldNameC = infofileUpload.closest('td').prev('td').text();
            var infofileUploadvalue = $('.info-fileUpload-value', infofileUpload.next())
            if (infofileUploadvalue.val() == '' || infofileUploadvalue.val() == undefined) {
                alert('注意!!,未上傳 " ' + FieldNameC + ' " ,請上傳!!');
                infofileUpload.focus();
                return false;
            }
        }

        //(工具)
        function CheckRefVal(FormName, fieldName) {
            var FieldValue = $(FormName + fieldName).refval('selectItem').text;
            var FieldNameC = $(FormName + fieldName).closest('td').prev('td').text();
            if ($.trim(FieldValue) == '' || FieldValue == undefined) {
                alert('注意!!,未選取 " ' + FieldNameC + ' " ,請選取!!');
                $(FormName + fieldName).focus();
                return false;
            }
        }

        function JQDataForm1OnApplied() {
            var ReqNo = $('#JQDataForm1RequisitionNO').val();
            var RoleID = getClientInfo("_GroupID");//_GroupName
            $.ajax({//請款單起單
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sForm_SecurityDeposit.SecurityDeposit', //連接的Server端，command
                data: "mode=method&method=" + "MakeRequisition" + "&parameters=" + ReqNo, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    if (data != false) {//data == true會進不去
                        GetSumRACountRA();
                        alert('請款單已成功申請');
                    } else {
                        alert('請款單申請失敗');
                    }
                    closeForm('#JQDialog3');
                }
            });
        }

        //(工具)
        function GetSumRACountRA() {
            //var DepositNO = $('#dataFormMasterDepositNO').val();
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sForm_SecurityDeposit.SecurityDeposit', //連接的Server端，command
                data: "mode=method&method=" + "SelectRequisition",//+ "&parameters=" + DepositNO, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length > 0) {
                        $('#dataFormMasterRequisitionNO').val(rows[0].RequisitionNO);
                    }
                    //if (rows[0].CountR > 0) {
                    //$('#ReqButton').linkbutton({ 'text': '請款單- 已申請' + rows[0].CountR + '筆' });
                    //}
                }
            });
        }

        //-------------------#region copy from 請款單-------------------
        function JQDataForm1OnLoadSuccess() {
            var UserID = getClientInfo("UserID");
            GetUserOrgNOs();//依登入者來設ApplyOrg_NO申請部門、Org_NOParent直屬主管的部門
            var EmpFlowAgentList = GetEmpFlowAgentList();//登入者的有效代理人人員清單
            var whereStr = " EmployeeID in (" + EmpFlowAgentList + ")";
            $('#JQDataForm1ApplyEmpID').combobox('setWhere', whereStr);
            //設定緊急付款、非付款日付款 欄位Caption 變顏色
            var flagIDs = ['#JQDataForm1IsUrgentPay', '#JQDataForm1IsNotPayDate'];
            $(flagIDs.toString()).each(function () {
                var captionTd = $(this).closest('td').prev('td');
                captionTd.css({ color: '#8A2BE2' });
            });

            $('#JQDataForm1SourceBillNO').val($('#dataFormMasterDepositNO').val());

            //把出差人員姓名帶到請款單的請款事由
            //var rowsdata = $('#dataGridDetailTvl1').datagrid('getRows');
            //var nameStr = '';
            //for (var i = 0; i < rowsdata.length; i++) {
            //    if (i == 0) nameStr = '，出差人員：' + rowsdata[i].AccStaffName
            //    else
            //        nameStr = nameStr + '、' + rowsdata[i].AccStaffName
            //}
            //$('#JQDataForm1RequisitionDescr').val($('#dataFormMasterBizTvlGist').val() + '-旅平險' + nameStr);

            //$('#JQDataForm1CostCenterID').combobox('setValue', $('#dataFormMasterCostCenterID').combobox('getValue'));
            //$('#JQDataForm1AccountType').combobox('setValue', $('#dataFormMasterAccountType').combobox('getValue'));
            //$('#JQDataForm1AccountID').combobox('setValue', $('#dataFormMasterAccountID').combobox('getValue'));
            //$('#JQDataForm1CompanyID').combobox('setValue', $('#dataFormMasterCompanyID').combobox('getValue'));
            //$('#JQDataForm1RequisitionNotes').val('出差起訖日期' + $('#dataFormMasterTvlDateS').datebox('getValue') + "~" + $('#dataFormMasterTvlDateE').datebox('getValue'));
            $("#JQDataForm1RequisitAmt").focus();
            $("#JQDataForm1RequisitAmt").numberbox('setValue', $("#dataFormMasterWarrantAmount").val());
            $("#JQDataForm1PayTo").refval('setValue', $("#dataFormMasterCusSupplier").refval('getValue'));
            $("#JQDataForm1PayTypeID").combobox('setValue', $("#dataFormMasterInOutWay").combobox('getValue'));

            var DisabledFieldName = [];
            var DisabledComboboxName = ['ApplyEmpID'];
            DisableFields('#JQDataForm1', DisabledFieldName, DisabledComboboxName);

            //var RedFieldName = ['CompanyID', 'CostCenterID', 'AccountType', 'AccountID', 'CostCenterID', 'RequisitionDescr', 'RequisitionTypeID', 'ProofTypeID',
            //    'RequisitAmt', 'PayTo', 'PayTypeID', 'PayTermID', 'PlanPayDate'];
            //RedTd('#JQDataForm1', RedFieldName);
        }

        //(工具)以預付日期來設 非付款日付款、緊急付款
        function ChkException() {
            var mess = "";
            var bdt = $('#JQDataForm1PlanPayDate').combo('textbox').val();//預付日期
            var dt = new Date(bdt);
            var days = dt.getDate()
            if ((days != 5) && (days != 25)) {
                $('#JQDataForm1IsNotPayDate').checkbox('setValue', 1);//非付款日付款
            }
            else {
                $('#JQDataForm1IsNotPayDate').checkbox('setValue', 0);
            }
            var dd = new Date();
            var dc = (dt - dd) / 86400000;
            if (dc.toPrecision() <= 5) {
                $('#JQDataForm1IsUrgentPay').checkbox('setValue', 1);//緊急付款
            }
            else {
                $('#JQDataForm1IsUrgentPay').checkbox('setValue', 0);
            }
        }

        function JQDataForm1OnApply() {

            var FormName = '#JQDataForm1';
            var fieldName = 'CompanyID';
            if (CheckCombobox(FormName, fieldName) == false) { return false; }
            var fieldName = 'CostCenterID';
            if (CheckCombobox(FormName, fieldName) == false) { return false; }
            var fieldName = 'AccountType';
            if (CheckCombobox(FormName, fieldName) == false) { return false; }
            var fieldName = 'AccountID';
            if (CheckCombobox(FormName, fieldName) == false) { return false; }
            fieldName = 'RequisitionDescr';
            if (CheckVal(FormName, fieldName) == false) { return false; }
            var fieldName = 'RequisitionTypeID';
            if (CheckCombobox(FormName, fieldName) == false) { return false; }
            var fieldName = 'ProofTypeID';
            if (CheckCombobox(FormName, fieldName) == false) { return false; }
            fieldName = 'RequisitAmt';
            if (CheckVal(FormName, fieldName) == false) { return false; }
            var fieldName = 'PayTo';
            if (CheckRefVal(FormName, fieldName) == false) { return false; }
            var fieldName = 'PayTypeID';
            if (CheckCombobox(FormName, fieldName) == false) { return false; }
            var fieldName = 'PayTermID';
            if (CheckCombobox(FormName, fieldName) == false) { return false; }
            fieldName = 'PlanPayDate';
            if (CheckDatebox(FormName, fieldName) == false) { return false; }

            var pre = confirm("確定起單-請款單?");
            if (pre == true) {
                return true;
            } else {
                return false;
            }

        }

        //OnSelect_PayTo(受款人),來設定付款條件PayTermID與付款方式PayTypeID並由付款方式改變須匯款費、匯款費誰付, 由付款條件改變預付日期
        function GetPayTerm(rowData) {
            $("#JQDataForm1PayTermID").combobox('setValue', rowData.PayTermID);
            $("#JQDataForm1PayTypeID").combobox('setValue', rowData.PayTypeID);
            var pp = $("#JQDataForm1PayTypeID").combobox('getValue'); //combobox 取值
            if (pp == 2) {
                $("#JQDataForm1IsRemit").checkbox('setValue', rowData.IsRemit);//設須匯款費
                $("#JQDataForm1RemitType").combobox('setValue', '廠商付');//設匯款費付款方式(誰付)
                $("#JQDataForm1Remit").val(30); //text給值
            }
            else {
                $("#JQDataForm1IsRemit").checkbox('setValue', 0);
                $("#JQDataForm1RemitType").combobox('setValue', "");
                $("#JQDataForm1Remit").val(0); //text給值
            }
            var PayTerm = $("#JQDataForm1PayTermID").combobox('getText');
            var WorkDays = 5; //作業最短日期
            var tdate = GetPlanPayDate(PayTerm, WorkDays, $("#JQDataForm1ApplyDate").datebox('getValue'));
            $('#JQDataForm1PlanPayDate').datebox('setValue', tdate);
        }

        //OnBlur_IsRemit(需匯款人)
        function CheckRemitType() {//需匯款費Blur時會呼叫
            var kk = $("#JQDataForm1IsRemit").checkbox('getValue')//須匯款費
            if (kk != 1) {
                $("#JQDataForm1RemitType").combobox('setValue', "");//匯款費誰付
                $("#JQDataForm1Remit").val(0); //text給值
            }
            else {
                $("#JQDataForm1RemitType").combobox('setValue', '廠商付');
                $("#JQDataForm1Remit").val(30); //text給值
            }
        }

        //OnSelect_PayTypeID(付款方式),來設定須匯款費與匯款費誰付
        function GetPayType(rowData) {
            var pp = $("#JQDataForm1PayTypeID").combobox('getValue'); //combobox 取值
            if (pp != 2) {
                $("#JQDataForm1IsRemit").checkbox('setValue', 0);//須匯款費
                $("#JQDataForm1RemitType").combobox('setValue', "");//匯款費誰付
                $("#JQDataForm1Remit").val(0); //text給值
            }
        }

        //OnSelect_PayTermID(付款條件),來設定預付日期
        function GetPayDateByTerm() {
            var PayTerm = $("#JQDataForm1PayTermID").combobox('getText');
            var tdate = GetPlanPayDate(PayTerm, 5, $("#JQDataForm1ApplyDate").datebox('getValue'));
            $('#JQDataForm1PlanPayDate').datebox('setValue', tdate);
        }

        //(工具)傳入付款條件,作業天數,申請日期→回傳預付日期
        function GetPlanPayDate(PayTerm, WorkDays, PlanPayDate) {
            var now = new Date(PlanPayDate);
            if (PayTerm == "當月") {
                var newDate = DateAdd("d ", WorkDays, now).toLocaleDateString();
            }
            else {
                var days = parseInt(PayTerm, 10);
                var newDate = DateAdd("d ", days, now).toLocaleDateString();
            }
            var ttDate = GetPayDate(newDate);
            return ttDate;
        }

        //(工具)計算付款日(回傳預付日期)
        function GetPayDate(PayDate) {
            var Dt = new Date(PayDate);
            var year = Dt.getFullYear();
            var month = Dt.getMonth() + 1;
            var Bt = Dt.toLocaleDateString();
            var Dt10 = new Date(year + '/' + month + '/' + '10').toLocaleDateString();
            var Dt25 = new Date(year + '/' + month + '/' + '25').toLocaleDateString();
            if (Bt <= Dt10) {
                return Dt10;
            }
            else
                if (Bt <= Dt25) {
                    return Dt25;
                }
                else {
                    return Dt25;
                }
        }

        //(工具)
        function DateAdd(interval, number, date) {
            switch (interval) {
                case "y ": {
                    date.setFullYear(date.getFullYear() + number);
                    return date;
                    break;
                }
                case "q ": {
                    date.setMonth(date.getMonth() + number * 3);
                    return date;
                    break;
                }
                case "m ": {
                    date.setMonth(date.getMonth() + number);
                    return date;
                    break;
                }
                case "w ": {
                    date.setDate(date.getDate() + number * 7);
                    return date;
                    break;
                }
                case "d ": {
                    date.setDate(date.getDate() + number);
                    return date;
                    break;
                }
                case "h ": {
                    date.setHours(date.getHours() + number);
                    return date;
                    break;
                }
                case "m ": {
                    date.setMinutes(date.getMinutes() + number);
                    return date;
                    break;
                }
                case "s ": {
                    date.setSeconds(date.getSeconds() + number);
                    return date;
                    break;
                }
                default: {
                    date.setDate(d.getDate() + number);
                    return date;
                    break;
                }
            }
        }

        //OnSelect_ApplyEmpID(申請員工)→申請部門、直屬主管的部門
        function OnSelectEmployee(rowData) {
            //var whereStr = "EmployeeID =  " + "'" + UserID + "'";
            //$('#dataFormMasteShortTermNO').combobox('setWhere', '1=2');
            //$('#dataFormMasteShortTermNO').combobox('setWhere', whereStr);

            $("#JQDataForm1ApplyOrg_NO").combobox('setValue', rowData.OrgNO);
            $("#JQDataForm1Org_NOParent").val(rowData.OrgNOParent);
        }

        //當點按關閉按鈕時,關閉目前Tab(JQDataForm1的Cancel時)
        //function CloseDataForm() {
        //    //self.parent.closeCurrentTab();
        //    //self.closeCurrentTab();
        //    //return false;
        //}

        //validateMaster檢查憑證號碼
        function CheckProofNO() {
            var ProofType = $("#JQDataForm1ProofTypeID").combobox("getValue");
            var ProofNO = $("#JQDataForm1ProofNO").val();
            var ProofNOLen = ProofNO.length;
            if ((ProofType == 1) && (ProofNOLen != 10)) {
                return false;
            }
            return true;
        }

        //(工具)
        function GetEmpFlowAgentList() {
            var UserID = getClientInfo("UserID");
            var Flow = "請款單申請";
            var ReturnStr = "";
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sRequisition.Requisition', //連接的Server端，command
                data: "mode=method&method=" + "GetEmpFlowAgentList" + "&parameters=" + UserID + "," + Flow, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    if (data != false) {
                        ReturnStr = data;
                    }
                }
            });
            return ReturnStr;
        }

        //(工具)由登入者設定申請部門、直屬主管的部門
        function GetUserOrgNOs() {
            var UserID = getClientInfo("UserID");
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sRequisition.Requisition', //連接的Server端，command
                data: "mode=method&method=" + "GetUserOrgNOs" + "&parameters=" + UserID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length > 0) {
                        $("#JQDataForm1ApplyOrg_NO").combobox('setValue', rows[0].OrgNO);
                        $("#JQDataForm1Org_NOParent").val(rows[0].OrgNOParent);
                    }
                }
            }
            );
        }

        //OnSelect_CostCenterID(成本中心)→CostCenterName，清空科目類別,會計科目
        function CostCenterIDOnSelect1() {
            var tt = $('#JQDataForm1CostCenterID').combobox('getText');
            $('#JQDataForm1CostCenterName').val(tt);
            $("#JQDataForm1AccountType").combobox('setValue', "");
            $("#JQDataForm1AccountID").combobox('setValue', "");
        }

        //OnSelect_AccountType(科目類型)→會計科目
        function AccountTypeOnSelect1(rowData) {
            $("#JQDataForm1AccountID").combobox("setValue", "");
            var CostCenterID = $("#JQDataForm1CostCenterID").combobox("getValue");
            var FiltStr = "AccountType=" + "'" + rowData.AccountType + "'" + " and (LimitCostCenters='' or LimitCostCenters like '%" + CostCenterID + "%' or LimitCostCenters is null)";
            $("#JQDataForm1AccountID").combobox('setWhere', FiltStr);
        }

        //=============================檢附憑證=>發票時才須計算(但會計科目=>交際費 除外)=========================================
        //=========================================請款金額 推 進項稅額*1.05(四捨五入), 未稅金額=========================================
        function OnBlurRequisitAmt() {
            var RequisitAmt = 0;
            var RequisitAmtTax = 0;
            var RequisitAmtNoTax = 0;
            var ProofTypeID = $('#JQDataForm1ProofTypeID').combobox('getValue');//檢附憑證=>1 發票
            var AccountText = $("#JQDataForm1AccountID").combobox('getText');//交際費 除外
            if (ProofTypeID == "1" && AccountText != "交際費") {
                var RequisitAmt = $("#JQDataForm1RequisitAmt").val();
                RequisitAmtNoTax = Math.round(RequisitAmt / 1.05);
                RequisitAmtTax = RequisitAmt - RequisitAmtNoTax;
            } else {
                RequisitAmtNoTax = $("#JQDataForm1RequisitAmt").val();
            }

            $("#JQDataForm1RequisitAmtTax").numberbox('setValue', RequisitAmtTax);
            $("#JQDataForm1RequisitAmtTax").val(RequisitAmtTax);
            $("#JQDataForm1RequisitAmtNoTax").numberbox('setValue', RequisitAmtNoTax);
            $("#JQDataForm1RequisitAmtNoTax").val(RequisitAmtNoTax);
        }

        //(工具)
        function GetUserOrg() {
            var UserID = getClientInfo("UserID");
            var _return = "";
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sPettyCash.PettyCash',
                data: "mode=method&method=" + "GetUserOrgNOs" + "&parameters=" + UserID,
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length == 1) {
                        _return = rows[0].OrgNO;
                    }
                }
            })
            return _return;
        }
        //(工具)
        function GetUserOrgNOParent() {
            var UserID = getClientInfo("UserID");
            var _return = "";
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sPettyCash.PettyCash',
                data: "mode=method&method=" + "GetUserOrgNOs" + "&parameters=" + UserID,
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length == 1) {
                        _return = rows[0].OrgNOParent;
                    }
                }
            })
            return _return;
        }
        //(工具)
        function GetUserOrgCostCenter() {
            var UserID = getClientInfo("UserID");
            var _return = "";
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sPettyCash.PettyCash',
                data: "mode=method&method=" + "GetUserOrgNOs" + "&parameters=" + UserID,
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length == 1) {
                        _return = rows[0].OrgCostCenter;
                    }
                }
            })
            return _return;
        }
        //-------------------#endregion copy from 請款單-------------------
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sForm_SecurityDeposit1.SecurityDeposit" runat="server" AutoApply="True"
                DataMember="SecurityDeposit" Pagination="True" QueryTitle="" EditDialogID="JQDialog0"
                Title="保證金單列表" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption=" " Editor="text" FieldName="Button1" MaxLength="0" Visible="true" Width="100" FormatScript="FormatScript_Button1" Sortable="False" Frozen="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="保證金單號" Editor="text" FieldName="DepositNO" Format="" MaxLength="0" Visible="true" Width="75" Frozen="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="合約編號" Editor="text" FieldName="ContractNO" Format="" MaxLength="0" Visible="true" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="屬性" Editor="infocombobox" FieldName="DepositProperty" Format="" MaxLength="0" Visible="true" Width="35" EditorOptions="valueField:'PropertyValue',textField:'PropertyName',remoteName:'sForm_SecurityDeposit1.DepositProperty',tableName:'DepositProperty',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶供應商" Editor="infocombobox" FieldName="CusSupplier" Format="" Visible="true" Width="110" EditorOptions="valueField:'ID',textField:'Name',remoteName:'sForm_SecurityDeposit1.VenderCustomer',tableName:'VenderCustomer',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="保證金金額" Editor="text" FieldName="DepositAmount" Format="" MaxLength="0" Visible="true" Width="65" />
                    <JQTools:JQGridColumn Alignment="left" Caption="已沖金額" Editor="text" FieldName="SumOfWarrantAmount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="55">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="已沖次數" Editor="text" FieldName="CountOfWarrant" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="55">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="保金返還日期" Editor="text" FieldName="InOutDate1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="未沖金額" Editor="text" FieldName="RestOfWarrantAmount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="55">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="Notes" Format="" Visible="true" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="存出入日期" Editor="text" FieldName="InOutDate" Format="yyyy-mm-dd" MaxLength="0" Visible="true" Width="65" EditorOptions="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="收支方式" Editor="infocombobox" FieldName="InOutWay" Format="" MaxLength="0" Visible="true" Width="52" EditorOptions="valueField:'InOutWayValue',textField:'InOutWayName',remoteName:'sForm_SecurityDeposit1.InOutWay',tableName:'InOutWay',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶供應商戶名" Editor="text" FieldName="CusSupplierAccName" Format="" MaxLength="0" Visible="False" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="傳票號碼" Editor="text" FieldName="VoucherNO" Format="" MaxLength="0" Visible="False" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立者" Editor="infocombobox" FieldName="CreateBy" Format="" Visible="True" Width="50" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sForm_SecurityDeposit1.Users',tableName:'Users',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="text" FieldName="CreateDate" Format="yyyy-mm-dd" MaxLength="0" Visible="False" Width="60" EditorOptions="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改者" Editor="infocombobox" FieldName="LastUpdateBy" Format="" Visible="False" Width="55" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sForm_SecurityDeposit1.Users',tableName:'Users',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="text" FieldName="LastUpdateDate" Format="yyyy-mm-dd" MaxLength="0" Visible="False" Width="60" EditorOptions="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="流程狀態" Editor="infocombobox" FieldName="Flowflag" Format="" MaxLength="0" Visible="true" Width="55" EditorOptions="valueField:'Value',textField:'Name',remoteName:'sForm_SecurityDeposit1.Flowflag',tableName:'Flowflag',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="請款單編號" Editor="text" FieldName="RequisitionNO" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" Visible="True" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" Visible="True" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消" Visible="True"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" Visible="False" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶供應商" Condition="%%" DataType="string" Editor="text" EditorOptions="" FieldName="Name" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="建立者" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sForm_SecurityDeposit1.Users',tableName:'Users',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CreateBy" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="80" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="沖銷狀態" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'},{value:'1',text:'已結案未沖完',selected:'false'},{value:'2',text:'已結案已沖完',selected:'false'},{value:'3',text:'未結案',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="RestOfWarrantAmount" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="110" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="保證金屬性" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'PropertyValue',textField:'PropertyName',remoteName:'sForm_SecurityDeposit1.DepositProperty',tableName:'DepositProperty',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="DepositProperty" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="60" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog0" runat="server" BindingObjectID="dataFormMaster0" Title="保證金單" DialogLeft="2px" DialogTop="60px" Width="1020px">
                <JQTools:JQDataForm ID="dataFormMaster0" runat="server" DataMember="SecurityDeposit" HorizontalColumnsCount="2" RemoteName="sForm_SecurityDeposit1.SecurityDeposit" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="保證金單號" Editor="text" FieldName="DepositNO" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="合約編號" Editor="text" FieldName="ContractNO" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="保證金屬性" Editor="infocombobox" FieldName="DepositProperty" Format="" maxlength="0" Width="180" EditorOptions="valueField:'PropertyValue',textField:'PropertyName',remoteName:'sForm_SecurityDeposit1.DepositProperty',tableName:'DepositProperty',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:OnSelect_DepositProperty,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶供應商" Editor="infocombobox" FieldName="CusSupplier" Format="" maxlength="0" Width="180" EditorOptions="valueField:'ID',textField:'Name',remoteName:'sForm_SecurityDeposit1.VenderCustomer1',tableName:'VenderCustomer1',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="保證金金額" Editor="numberbox" FieldName="DepositAmount" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="textarea" FieldName="Notes" Format="" maxlength="0" Width="420" EditorOptions="height:50" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請款單單號" Editor="text" FieldName="RequisitionNO" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="存出入日期" Editor="datebox" FieldName="InOutDate" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="收支方式" Editor="infocombobox" FieldName="InOutWay" Format="" maxlength="0" Width="180" EditorOptions="valueField:'InOutWayValue',textField:'InOutWayName',remoteName:'sForm_SecurityDeposit1.InOutWay',tableName:'InOutWay',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶供應商戶名" Editor="text" FieldName="CusSupplierAccName" Format="" maxlength="0" Width="420" EditorOptions="" Span="2" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="傳票號碼" Editor="text" FieldName="VoucherNO" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="流程狀態" Editor="infocombobox" EditorOptions="valueField:'Value',textField:'Name',remoteName:'sForm_SecurityDeposit1.Flowflag',tableName:'Flowflag',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="Flowflag" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立者" Editor="infocombobox" FieldName="CreateBy" Format="" maxlength="0" Width="180" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sForm_SecurityDeposit1.Users',tableName:'Users',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修改者" Editor="infocombobox" FieldName="LastUpdateBy" Format="" maxlength="0" Width="180" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sForm_SecurityDeposit1.Users',tableName:'Users',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="180" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster0" runat="server" BindingObjectID="dataFormMaster0" EnableTheming="True">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster0" runat="server" BindingObjectID="dataFormMaster0" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" AutoApply="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="SecurityDepositWarrant" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="False" ParentObjectID="dataFormMaster0" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sForm_SecurityDeposit1.SecurityDeposit" RowNumbers="True" Title="保證金沖銷單列表" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="保證金沖銷單號" Editor="text" FieldName="DepositWarrantNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="保證金單號" Editor="text" FieldName="DepositNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="屬性" Editor="infocombobox" FieldName="DepositProperty" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="40" EditorOptions="valueField:'PropertyValue',textField:'PropertyName',remoteName:'sForm_SecurityDepositWarrant.DepositProperty',tableName:'DepositProperty',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="客戶供應商" Editor="infocombobox" FieldName="CusSupplier" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" EditorOptions="valueField:'ID',textField:'Name',remoteName:'sForm_SecurityDepositWarrant.VenderCustomer',tableName:'VenderCustomer',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="保證金金額" Editor="text" FieldName="DepositAmount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="返還金額" Editor="text" FieldName="WarrantAmount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="未返還金額" Editor="text" FieldName="OtherWarrantAmount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="Notes" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="返還日期" Editor="text" FieldName="InOutDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70" Format="yyyy-mm-dd">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="收支方式" Editor="infocombobox" FieldName="InOutWay" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" EditorOptions="valueField:'InOutWayValue',textField:'InOutWayName',remoteName:'sForm_SecurityDepositWarrant.InOutWay',tableName:'InOutWay',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="客戶供應商戶名" Editor="text" FieldName="CusSupplierAccName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="傳票號碼" Editor="text" FieldName="VoucherNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="60">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="流程狀態" Editor="infocombobox" FieldName="Flowflag" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" EditorOptions="valueField:'Value',textField:'Name',remoteName:'sForm_SecurityDepositWarrant.Flowflag',tableName:'Flowflag',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="請款單單號" Editor="text" FieldName="RequisitionNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="建立者" Editor="infocombobox" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sForm_SecurityDepositWarrant.Users',tableName:'Users',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="text" FieldName="CreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70" Format="yyyy-mm-dd">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="修改者" Editor="infocombobox" FieldName="LastUpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sForm_SecurityDepositWarrant.Users',tableName:'Users',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="text" FieldName="LastUpdateDate" Format="yyyy-mm-dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                    </Columns>
                </JQTools:JQDataGrid>
            </JQTools:JQDialog>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="保證金沖銷單申請" Width="700px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="SecurityDepositWarrant" HorizontalColumnsCount="2" RemoteName="sForm_SecurityDepositWarrant.SecurityDepositWarrant" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="True" IsAutoPause="False" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="True" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApply="OnApply_dataFormMaster" OnLoadSuccess="OnLoad_dataFormMaster" OnApplied="OnApplied_dataFormMaster" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="保證金沖銷單號" Editor="text" FieldName="DepositWarrantNO" Format="" maxlength="0" Width="180" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="保證金單號" Editor="text" FieldName="DepositNO" Format="" maxlength="0" Width="180" EditorOptions="" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="保證金屬性" Editor="infocombobox" FieldName="DepositProperty" Format="" Width="180" EditorOptions="valueField:'PropertyValue',textField:'PropertyName',remoteName:'sForm_SecurityDepositWarrant.DepositProperty',tableName:'DepositProperty',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OnSelect_DepositProperty,panelHeight:200" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶供應商" Editor="infocombobox" FieldName="CusSupplier" Format="" Width="180" EditorOptions="valueField:'ID',textField:'Name',remoteName:'sForm_SecurityDeposit1.VenderCustomer1',tableName:'VenderCustomer1',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="保證金金額" Editor="numberbox" FieldName="DepositAmount" Format="" Width="180" ReadOnly="True" MaxLength="0" NewRow="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="返還金額" Editor="numberbox" FieldName="WarrantAmount" Format="" Width="180" maxlength="0" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="未返還金額" Editor="numberbox" FieldName="OtherWarrantAmount" MaxLength="0" ReadOnly="False" Visible="True" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="textarea" FieldName="Notes" Format="" maxlength="0" Width="420" EditorOptions="height:50" Span="2" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請款單號" Editor="text" FieldName="RequisitionNO" Format="" Width="100" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="返還日期" Editor="datebox" FieldName="InOutDate" Format="" Width="180" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="收支方式" Editor="infocombobox" FieldName="InOutWay" Format="" maxlength="0" Width="180" EditorOptions="valueField:'InOutWayValue',textField:'InOutWayName',remoteName:'sForm_SecurityDepositWarrant.InOutWay',tableName:'InOutWay',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" NewRow="False" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶供應商戶名" Editor="text" FieldName="CusSupplierAccName" Format="" maxlength="0" Width="420" NewRow="True" Span="2" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="傳票號碼" Editor="text" FieldName="VoucherNO" Format="" maxlength="0" Width="180" Visible="False" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQButton ID="ReqButton" runat="server" Icon="icon-add" OnClick="ReqButtonOnClick" Text="申請" />
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="DepositWarrantNO" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="OtherWarrantAmount" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>

            <JQTools:JQDialog ID="JQDialog3" runat="server"  BindingObjectID="JQDataForm1" Title="請款單申請" Width="730px" DialogLeft="10px" DialogTop="120px">
                <JQTools:JQDataForm ID="JQDataForm1" runat="server" DataMember="Requisition" HorizontalColumnsCount="3" RemoteName="sRequisition.Requisition" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" Width="720px" IsAutoPause="False" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" OnLoadSuccess="JQDataForm1OnLoadSuccess" OnApplied="JQDataForm1OnApplied" OnApply="JQDataForm1OnApply">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="來源單號" Editor="text" FieldName="RequisitionNO" Format="" maxlength="0" Width="127"  ReadOnly="True" Span="1"/>
                        <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sRequisition.Company',tableName:'Company',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:250" FieldName="CompanyID" maxlength="0" ReadOnly="False" Visible="True" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請款性質" Editor="infocombobox" EditorOptions="valueField:'RequisitKindID',textField:'RequistKindName',remoteName:'sRequisition.RequistKind',tableName:'RequistKind',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:150" FieldName="RequistKindID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請員工" Editor="infocombobox" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sRequisition.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OnSelectEmployee,panelHeight:200" FieldName="ApplyEmpID" MaxLength="0" ReadOnly="False" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請部門" Editor="infocombobox" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sRequisition.Organization',tableName:'Organization',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:250" FieldName="ApplyOrg_NO" Format="" ReadOnly="True" Width="130" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ApplyDate" Format="yyyy/mm/dd" ReadOnly="True" Width="90" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="成本中心" Editor="infocombobox" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sRequisition.CostCenter',tableName:'CostCenter',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:CostCenterIDOnSelect1,panelHeight:250" FieldName="CostCenterID" Format="" Span="1" Width="133" MaxLength="0" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="科目類別" Editor="infocombobox" EditorOptions="valueField:'AccountType',textField:'AccountType',remoteName:'sForm_SecurityDeposit1.AccountType',tableName:'AccountType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:AccountTypeOnSelect1,panelHeight:200" FieldName="AccountType" MaxLength="0" ReadOnly="False" Span="1" Width="130" NewRow="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="會計科目" Editor="infocombobox" EditorOptions="valueField:'AccountID',textField:'AccountName',remoteName:'sForm_SecurityDeposit1.AccountTitle',tableName:'AccountTitle',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AccountID" Span="1" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請款事由" Editor="textarea" EditorOptions="height:40" FieldName="RequisitionDescr" Format="" Span="3" Width="580" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請款依據" Editor="infocombobox" EditorOptions="valueField:'RequisitionTypeID',textField:'RequisitionTypeName',remoteName:'sRequisition.RequisitionType',tableName:'RequisitionType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="RequisitionTypeID" Format="" Width="133" maxlength="0" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="檢附憑證" Editor="infocombobox" EditorOptions="valueField:'ProofTypeID',textField:'ProofTypeName',remoteName:'sRequisition.ProofType',tableName:'ProofType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:OnBlurRequisitAmt,panelHeight:200" FieldName="ProofTypeID" Format="" Width="130" OnBlur="" />
                        
                        <JQTools:JQFormColumn Alignment="left" Caption="憑據號碼" Editor="text" FieldName="ProofNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="155" OnBlur="" />

                        <JQTools:JQFormColumn Alignment="left" Caption="請款金額" Editor="numberbox" FieldName="RequisitAmt" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="130" Format="" />
                        
                   <JQTools:JQFormColumn Alignment="left" Caption="未稅金額" Editor="numberbox" FieldName="RequisitAmtNoTax" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="125" />
                   <JQTools:JQFormColumn Alignment="left" Caption="稅額" Editor="numberbox" FieldName="RequisitAmtTax" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="130" />  
                   <JQTools:JQFormColumn Alignment="left" Caption="暫借款單" Editor="infocombobox" EditorOptions="valueField:'ShortTermNO',textField:'ShortTermDescr',remoteName:'sRequisition.ShortTerm',tableName:'ShortTerm',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ShortTermNO" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="395" />
                   <JQTools:JQFormColumn Alignment="left" Caption="會辦總務" Editor="checkbox" FieldName="NeedGeneralAffairs" Width="80" NewRow="True" OnBlur="" maxlength="0" Span="1" /> 

                        <JQTools:JQFormColumn Alignment="left" Caption="請款備註" Editor="textarea" EditorOptions="height:40" FieldName="RequisitionNotes" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="580" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="受款人" Editor="inforefval" EditorOptions="title:'受款人篩選',panelWidth:360,remoteName:'sRequisition.Vendor',tableName:'Vendor',columns:[{field:'VendShortName',title:'供應商簡稱',width:200,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactName',title:'聯絡人',width:120,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'VendID',textField:'VendShortName',valueFieldCaption:'VendID',textFieldCaption:'選取',cacheRelationText:false,checkData:false,showValueAndText:false,dialogCenter:false,onSelect:GetPayTerm,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="PayTo" Format="" maxlength="0" span="1" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="付款方式" Editor="infocombobox" EditorOptions="valueField:'PayTypeID',textField:'PayTypeName',remoteName:'sRequisition.PayType',tableName:'PayType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:GetPayType,panelHeight:200" FieldName="PayTypeID" Format="" Width="133" maxlength="0" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="付款條件" Editor="infocombobox" EditorOptions="valueField:'PayTermID',textField:'PayTermName',remoteName:'sRequisition.PayTerm',tableName:'PayTerm',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:GetPayDateByTerm,panelHeight:200" FieldName="PayTermID" Format="" Width="183" Visible="True" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="預付日期" Editor="datebox" EditorOptions="dateFormat:'datetime',showTimeSpinner:false,showSeconds:false" FieldName="PlanPayDate" Format="yyyy/mm/dd" Width="133" Visible="True" Span="1" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="緊急付款" Editor="checkbox" FieldName="IsUrgentPay" Width="80" Visible="True" Span="1" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1"/>
                        <JQTools:JQFormColumn Alignment="left" Caption="非付款日付款" Editor="checkbox" FieldName="IsNotPayDate" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="付款備註" Editor="textarea" FieldName="PayToNotes" ReadOnly="False" Span="3" Visible="True" Width="580" EditorOptions="height:40" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Span="1" Width="130" Visible="False" ReadOnly="False" maxlength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" maxlength="0" Visible="False" Width="130" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="需匯款費" Editor="checkbox" FieldName="IsRemit" Visible="True" Width="80" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" EditorOptions="on:1,off:0" OnBlur="CheckRemitType" />
                        <JQTools:JQFormColumn Alignment="left" Caption="匯款費付款" Editor="infocombobox" EditorOptions="items:[{value:'廠商付',text:'廠商付',selected:'false'},{value:'公司付',text:'公司付',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="RemitType" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="匯費金額" Editor="text" FieldName="Remit" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Org_NOParent" Editor="text" FieldName="Org_NOParent" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="出差申請單號" Editor="text" FieldName="SourceBillNO" ReadOnly="True" span="1" Visible="False" Width="80" MaxLength="0" NewRow="False" RowSpan="1" />

                        <JQTools:JQFormColumn Alignment="left" Caption="加簽對象" Editor="inforefval" EditorOptions="title:'JQRefval',panelWidth:350,panelHeight:200,remoteName:'sRequisition.Employee',tableName:'Employee',columns:[],columnMatches:[],whereItems:[],valueField:'EmployeeID',textField:'EmployeeName',valueFieldCaption:'工號',textFieldCaption:'姓名',cacheRelationText:false,checkData:false,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="DynamicUser" MaxLength="0" ReadOnly="False" Visible="True" Width="80" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="宿管人員申請" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsEmpGroupID" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="15" />
                        <JQTools:JQFormColumn Alignment="left" Caption="解除設定" Editor="checkbox" FieldName="unlock" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="15" />

                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="JQDefault1" runat="server" BindingObjectID="JQDataForm1" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="RequisitionNO" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="ApplyDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" RemoteMethod="True" FieldName="CreateBy" DefaultValue="_username" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="RequistKindID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="ApplyEmpID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="RequisitionTypeID" RemoteMethod="True" />
                        <%--<JQTools:JQDefaultColumn CarryOn="False" DefaultValue="" FieldName="ProofTypeID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="IsUrgentPay" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="IsNotPayDate" RemoteMethod="True" />--%>
                        
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetUserOrg" FieldName="ApplyOrg_NO" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetUserOrgNOParent" FieldName="Org_NOParent" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetUserOrgCostCenter" FieldName="CostCenterID" RemoteMethod="False" />

                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="JQValidate1" runat="server" BindingObjectID="JQDataForm1" EnableTheming="True">
                    <Columns>
                        <%--<JQTools:JQValidateColumn CheckNull="True" FieldName="RequisitAmt" RangeFrom="1" RangeTo="25000000" RemoteMethod="True" ValidateMessage="金額不可小於0" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="RequisitionDescr" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="PayTo" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckMethod="CheckProofNO" CheckNull="False" FieldName="ProofNO" RemoteMethod="False" ValidateMessage="發票輸入格式錯誤" ValidateType="None" />--%>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="RequisitAmt" RangeFrom="1" RangeTo="25000000" RemoteMethod="True" ValidateMessage="金額不可小於0" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="RequisitionDescr" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="PayTo" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckMethod="CheckProofNO" CheckNull="False" FieldName="ProofNO" RemoteMethod="False" ValidateMessage="發票輸入格式錯誤" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
        </JQTools:JQDialog>

        </div>
    </form>
</body>
</html>
