<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_BizTravel.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            //將Focus 欄位背景顏色改為黃色
            $("input, select, textarea").focus(function () {
                $(this).css("background-color", "yellow");
            });
            $("input, select, textarea").blur(function () {
                $(this).css("background-color", "white");
            });

            //排幣別列
            layout4To2('Currency1', 'BorrowAmt1', '3', '320');//fieldname1,fieldname2,colspan,fieldname1Width
            layout4To2('Currency2', 'BorrowAmt2', '3', '320');
            //layout4To2('CUR1', 'RealTvlAmt1', '3', '320');
            //layout4To2('CUR2', 'RealTvlAmt2', '3', '320');
            //layout4To2('ACurrency1', 'ABorrowAmt1', '3', '320');
            //layout4To2('ACurrency2', 'ABorrowAmt2', '3', '320');
            $('#dataFormMasterTvlDateE').closest('td').prev('td').width('60');

            //紅字
            var RedFieldName = ['CompanyID', 'AccountType', 'AccountID', 'BizTvlGist', 'TvlNationCity', 'PreTvlDescr', 'ApplyDate', 'CostCenterID', 'TvlDateS', 'TvlDateE', 'DemandDate',
                'DrawEmpID', 'InsuraceAmt', 'FeeDetailsPath', 'TvlReportsPath', 'FeeDetailsPath1', 'TvlReportsPath1'];//, 'RealTvlAmt1','CUR1'
            RedTd('#dataFormMaster', RedFieldName);
            RedTd('#dataFormDetail', ['AccStaffID', 'AccStaffPID', 'AccStaffInsGroupName', 'AccStaffBirthday']);

            //請購單的預定付款日期
            $('#JQDataForm1PlanPayDate').datebox({
                onSelect: function (date) {
                    ChkException();
                }
            }).combo('textbox').blur(function () {
                ChkException();
            });

            var Link = $("<a>").attr({ 'href': '../JB_ADMIN/BizTravelFeeDetails/費用明細表格式.xlsx', 'target': '_blank' }).text("格式");
            var Link1 = $("<a>").attr({ 'href': '../JB_ADMIN/BizTravelReport/出差報告格式.xlsx', 'target': '_blank' }).text("格式");
            $('#dataFormMasterFeeDetailsPath').closest('td').append(Link);
            $('#dataFormMasterTvlReportsPath').closest('td').append(Link1);
        });
        //4td併成2td
        function layout4To2(FieldName1, FieldName2, ColspanN, w) {
            var FieldName1td = $('#dataFormMaster' + FieldName1).closest('td');
            var FieldName2Contents = $('#dataFormMaster' + FieldName2).closest('td').prev('td').contents();
            var FieldName2tdC = $('#dataFormMaster' + FieldName2).closest('td').children();
            FieldName1td.append(FieldName2Contents).append(FieldName2tdC);
            FieldName1td.prev('td').width('100');
            FieldName1td.attr({ 'colspan': +ColspanN });
            FieldName1td.css({ 'width': +w });
            FieldName1td.next('td').remove();
            FieldName1td.next('td').remove();
        }
        //成本中心select→CostCenterName，清空科目類別,會計科目
        function CostCenterIDOnSelect() {
            var tt = $('#dataFormMasterCostCenterID').combobox('getText');
            $('#dataFormMasterCostCenterName').val(tt);
            $("#dataFormMasterAccountType").combobox('setValue', "");
            $("#dataFormMasterAccountID").combobox('setValue', "");
        }
        //科目類型→會計科目
        function AccountTypeOnSelect(rowData) {
            $("#dataFormMasterAccountID").combobox("setValue", "");
            var CostCenterID = $("#dataFormMasterCostCenterID").combobox("getValue");
            var FiltStr = "AccountType=" + "'" + rowData.AccountType + "'" + " and (LimitCostCenters='' or LimitCostCenters like '%" + CostCenterID + "%' or LimitCostCenters is null)";
            $("#dataFormMasterAccountID").combobox('setWhere', FiltStr);
        }
        //領款人select→DrawEmpName
        function DrawEmpIDOnSelect() {
            var tt = $('#dataFormMasterDrawEmpID').combobox('getText');
            $('#dataFormMasterDrawEmpName').val(tt);
        }
        //出差人員select→AccStaffName，身分證，生日，投保單位
        function AccStaffIDOnSelect(rowdata) {
            var cid = $('#dataFormDetailAccStaffID').combobox('getText');
            var date1 = rowdata.BIRTHDAY.substr(0, 10).replace(new RegExp('\-', 'g'), '/');
            $('#dataFormDetailAccStaffName').val(cid);
            $('#dataFormDetailAccStaffPID').val(rowdata.IDNO);
            $('#dataFormDetailAccStaffBirthday').datebox('setValue', date1);
            $('#dataFormDetailAccStaffInsGroupName').combobox('setValue', rowdata.INSURANCENAME);
        }
        //實際出差費1,2Blur→還款額1,2
        function RealTvlAmtOnBlur() {
            var b1 = $.trim($('#dataFormMasterABorrowAmt1').val());
            var b2 = $.trim($('#dataFormMasterABorrowAmt2').val());
            var r1 = $.trim($('#dataFormMasterRealTvlAmt1').val());
            var r2 = $.trim($('#dataFormMasterRealTvlAmt2').val());
            if (b1 == '') { b1 = 0; }
            if (b2 == '') { b2 = 0; }
            if (r1 == '') { r1 = 0; }
            if (r2 == '') { r2 = 0; }
            $('#dataFormMasterReback1').val(b1 - r1);
            $('#dataFormMasterReback2').val(b2 - r2);
        }
        //dataGridDetailTvl1新增時
        function GridDetailTvl1OnInsert() {
            var param = Request.getQueryStringByName("P1");
            if (param == '') { param = Request.getQueryStringByName2("P1"); }
            if (param == 'insurance' || param == 'borrowA') {//申請時
                alert('不允許新增');
                return false;
            }
        }
        //dataGridDetailTvl1刪除時
        function GridDetailTvl1OnDelete() {
            var param = Request.getQueryStringByName("P1");
            if (param == '') { param = Request.getQueryStringByName2("P1"); }
            if (param == 'insurance' || param == 'borrowA') {
                alert('不允許刪除');
                return false;
            }
        }
        //旅平險按鈕
        function ReqButtonOnClick() {
            openForm('#JQDialog3', {}, "inserted", 'dialog');
        }
        //預支幣別Val→實際支出幣別Val
        function ComboGetVal(GCombo1, Com1, GCombo2, Com2) {
            var c1 = $.trim($(GCombo1).combobox('getValue'));
            var c2 = $.trim($(GCombo2).combobox('getValue'));
            if (c1 != '') { $(Com1).combobox('setValue', c1); }
            if (c2 != '') { $(Com2).combobox('setValue', c2); }
        }
        //預支金額Val→會計預支金額Val
        function TextGetVal(GetCombo1, Combo1, GetCombo2, Combo2) {
            var c1 = $(GetCombo1).val()
            var c2 = $(GetCombo2).val()
            if (c1 != '') { $(Combo1).val(c1); }
            if (c2 != '') { $(Combo2).val(c2); }
        }
        function FormOnLoadSuccess() {
            var param = Request.getQueryStringByName("P1");
            if (param == '') { param = Request.getQueryStringByName2("P1"); }
            var ApplyHiddenFields = ['InsuraceAmt', 'RealTvlAmt1', 'CUR1', 'RealTvlAmt2', 'CUR2', 'Reback1', 'Reback2', 'FeeDetailsPath', 'TvlReportsPath', 'FeeDetailsPath1', 'TvlReportsPath1', 'FeeDetailsReceiptPath', 'FeeDetailsReceiptPath1', 'ABorrowAmt1', 'ABorrowAmt2', 'ACurrency1', 'ACurrency2'];
            var InsuranceHiddenFields = ['RealTvlAmt1', 'CUR1', 'RealTvlAmt2', 'CUR2', 'Reback1', 'Reback2', 'FeeDetailsPath', 'TvlReportsPath', 'FeeDetailsPath1', 'TvlReportsPath1', 'FeeDetailsReceiptPath', 'FeeDetailsReceiptPath1', 'ABorrowAmt1', 'ABorrowAmt2', 'ACurrency1', 'ACurrency2'];

            if (param == 'apply') {//申請時
                HideFields(ApplyHiddenFields);
                $('#ReqButton').css({ 'display': 'none' });
                $('#toolItemdataGridDetailTvl1下載Excel').css({ 'visibility': 'hidden' });
                //$('#div1').css({ 'display': 'none' });
            } else if (param == 'applyA') {//主管審核申請
                HideFields(ApplyHiddenFields);
                $('#divGrid1').css({ 'display': 'none' });
                $('#divGrid2').css({ 'display': 'block' });
                $('#ReqButton').css({ 'display': 'none' });
            } else if (param == 'insurance') {//總務保險
                var DisabledFieldName = ['BizTvlGist', 'TvlNationCity', 'PreTvlDescr', 'AccCustomers', 'PlanVisitCustomers', 'BorrowAmt1', 'BorrowAmt2', 'Notes'];
                var DisabledComboboxName = ['ApplyDate', 'CostCenterID', 'TvlDateS', 'TvlDateE', 'Currency1', 'Currency2', 'DemandDate', 'DrawEmpID', 'BorrowPayType', 'AccountType', 'AccountID', 'CompanyID'];
                DisableFields('#dataFormMaster', DisabledFieldName, DisabledComboboxName);
                HideFields(InsuranceHiddenFields);
                $('#toolItemdataGridDetailTvl1下載Excel').css({ 'visibility': 'visible' });
                var RequistKind = $('#JQDataForm1RequistKindID').closest('td');//請款單之請款性質
                RequistKind.append("<a href='../JB_ADMIN/Files/委任權限表.pdf' target='_blank'>請款性質說明</a>");
                if ($('#dataFormMasterInsuraceAmt').val() !== '' || $('#dataFormMasterInsuraceAmt').val() !== undefined) { GetSumRACountRA(); }
                $('#dataFormMasterInsuraceAmt').after($('#ReqButton'));
            } else if (param == 'borrowA') {//會計預支審核
                ComboGetVal('#dataFormMasterCurrency1', '#dataFormMasterACurrency1', '#dataFormMasterCurrency2', '#dataFormMasterACurrency2');//抓預支幣別給會計預支幣別
                TextGetVal('#dataFormMasterBorrowAmt1', '#dataFormMasterABorrowAmt1', '#dataFormMasterBorrowAmt2', '#dataFormMasterABorrowAmt2');//抓預支金額給會計預支金額
                var DisabledFieldName = ['BizTvlGist', 'TvlNationCity', 'PreTvlDescr', 'AccCustomers', 'PlanVisitCustomers', 'BorrowAmt1', 'BorrowAmt2', 'Notes'];
                var DisabledComboboxName = ['ApplyDate', 'CostCenterID', 'TvlDateS', 'TvlDateE', 'Currency1', 'Currency2', 'DemandDate', 'DrawEmpID', 'CompanyID'];
                DisableFields('#dataFormMaster', DisabledFieldName, DisabledComboboxName);
                var BorrowAHiddenFields = ['RealTvlAmt1', 'CUR1', 'RealTvlAmt2', 'CUR2', 'Reback1', 'Reback2', 'FeeDetailsPath', 'TvlReportsPath', 'FeeDetailsPath1', 'TvlReportsPath1', 'FeeDetailsReceiptPath', 'FeeDetailsReceiptPath1'];
                HideFields(BorrowAHiddenFields);
                $('#toolbardataGridDetailTvl1').css('display', 'none');
                $('#ReqButton').css({ 'display': 'none' });
                $('#toolItemdataGridDetailTvl1下載Excel').css({ 'visibility': 'hidden' });
            } else if (param == 'report') {//實際費用、費用明細、出差報告
                ComboGetVal('#dataFormMasterACurrency1', '#dataFormMasterCUR1', '#dataFormMasterACurrency2', '#dataFormMasterCUR2');//抓會計預支幣別給實際支出幣別
                var DisabledFieldName = ['BizTvlGist', 'TvlNationCity', 'PreTvlDescr', 'AccCustomers', 'PlanVisitCustomers', 'BorrowAmt1', 'BorrowAmt2', 'Notes', 'InsuraceAmt', 'ABorrowAmt1', 'ABorrowAmt2'];
                var DisabledComboboxName = ['ApplyDate', 'CostCenterID', 'TvlDateS', 'TvlDateE', 'Currency1', 'Currency2', 'DemandDate', 'DrawEmpID', 'ACurrency1', 'ACurrency2', 'BorrowPayType', 'AccountType', 'AccountID', 'CompanyID'];
                DisableFields('#dataFormMaster', DisabledFieldName, DisabledComboboxName);
                if ($('#dataFormMasterACurrency1').combobox('getValue') != '') { $('#dataFormMasterCUR1').combobox('disable'); }
                if ($('#dataFormMasterACurrency2').combobox('getValue') != '') { $('#dataFormMasterCUR2').combobox('disable'); }
                HideFields(['FeeDetailsPath1', 'TvlReportsPath1', 'FeeDetailsReceiptPath1']);
                $('#divGrid1').css({ 'display': 'none' });
                $('#divGrid2').css({ 'display': 'block' });
                $('#ReqButton').css({ 'display': 'none' });
            } else if (param == 'reportA1') {//主管審核費用明細與報告
                HideFields(['FeeDetailsPath', 'TvlReportsPath', 'FeeDetailsReceiptPath']);
                $('#divGrid1').css({ 'display': 'none' });
                $('#divGrid2').css({ 'display': 'block' });
                $('#ReqButton').css({ 'display': 'none' });
            } else if (param == 'reportA2') {//會計費用明細與出差審核
                ComboGetVal('#dataFormMasterACurrency1', '#dataFormMasterCUR1', '#dataFormMasterACurrency2', '#dataFormMasterCUR2');//抓會計預支幣別給實際支出幣別
                var DisabledFieldName = ['BizTvlGist', 'TvlNationCity', 'PreTvlDescr', 'AccCustomers', 'PlanVisitCustomers', 'BorrowAmt1', 'BorrowAmt2', 'Notes', 'InsuraceAmt', 'ABorrowAmt1', 'ABorrowAmt2'];
                var DisabledComboboxName = ['ApplyDate', 'CostCenterID', 'TvlDateS', 'TvlDateE', 'Currency1', 'Currency2', 'DemandDate', 'DrawEmpID', 'ACurrency1', 'ACurrency2', 'BorrowPayType', 'AccountType', 'AccountID', 'CompanyID'];
                DisableFields('#dataFormMaster', DisabledFieldName, DisabledComboboxName);
                HideFields(['FeeDetailsPath', 'TvlReportsPath','FeeDetailsReceiptPath']);
                $('#divGrid1').css({ 'display': 'none' });
                $('#divGrid2').css({ 'display': 'block' });
                $('#ReqButton').css({ 'display': 'none' });
            } else if (param == 'SucessView') {//申請成功檢視
                HideFields(['FeeDetailsPath', 'TvlReportsPath','FeeDetailsReceiptPath']);
                $('#ReqButton').css({ 'display': 'none' });
                $('#toolItemdataGridDetailTvl1下載Excel').css({ 'visibility': 'hidden' });
                $('#divGrid1').css({ 'display': 'none' });
                $('#divGrid2').css({ 'display': 'block' });
            }
            else if (param == '') {//經辦事項
                HideFields(['FeeDetailsPath', 'TvlReportsPath','FeeDetailsReceiptPath']);
                $('#ReqButton').css({ 'display': 'none' });
                $('#divGrid1').css({ 'display': 'none' });
                $('#divGrid2').css({ 'display': 'block' });
            }

            //總務要下載投保資料
            var UserID = getClientInfo("UserID");
            //var RoleID = getClientInfo("_groupid");
            if (UserID == '001') {//1030051
                $('#divGrid1').css({ 'display': 'block' });
                $('#divGrid2').css({ 'display': 'none' });
                $('#toolbardataGridDetailTvl1').css('display', 'block');
                $('#toolItemdataGridDetailTvl1下載Excel').css({ 'visibility': 'visible' });
            }

            RealTvlAmtOnBlur();//領款人還款計算
            //批示
            //if (param == 'apply') {
            //    $('#divSignNotesData').hide();
            //} else {
            //    GetSignNotesData();
            //}
        }
        //得到核示資訊
        function GetSignNotesData() {
            //var AbsentMinusID = $('#dataFormMasterAbsentMinusID').val();
            var no = $("#dataFormMasterTvlNo").val();
            //if (AbsentMinusID != "") {
            if (no != "") {
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sBizTravel.BizTravelMaster',  //連接的Server端，command
                    data: "mode=method&method=" + "GetSignNotesData" + "&parameters=" + no,
                    cache: false,
                    async: true,
                    success: function (data) {
                        var rows = $.parseJSON(data);//將JSon轉會到Object類型提供給Grid顯示                           
                        $('#GridSignNotesData').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                        if (rows.length == 0) {
                            $('#divSignNotesData').hide();
                        } else $('#divSignNotesData').show();
                    }
                });
            }

        }
        //取得有簽核內容簽核數
        function GetSignCount() {
            var TvlNo = $("#dataFormMasterTvlNo").val();
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sBizTravel.BizTravelMaster',
                data: "mode=method&method=" + "GetSignCount" + "&parameters=" + TvlNo,
                cache: false,
                async: false,
                success: function (data) {
                    cnt = $.parseJSON(data);
                }
            });
            return cnt;
        }
        function dataFormMasterOnApply() {
            var param = Request.getQueryStringByName("P1");
            if (param == '') { param = Request.getQueryStringByName2("P1"); }
            var FormName = '#dataFormMaster';
            var fieldName;
            if (param == 'apply') {//申請時
                fieldName = 'CompanyID';
                if (CheckCombobox(FormName, fieldName) == false) { return false; }
                fieldName = 'CostCenterID';
                if (CheckCombobox(FormName, fieldName) == false) { return false; }
                fieldName = 'AccountType';
                if (CheckCombobox(FormName, fieldName) == false) { return false; }
                fieldName = 'AccountID';
                if (CheckCombobox(FormName, fieldName) == false) { return false; }
                fieldName = 'BizTvlGist';
                if (CheckVal(FormName, fieldName) == false) { return false; }
                fieldName = 'TvlDateS';
                if (CheckDatebox(FormName, fieldName) == false) { return false; }
                fieldName = 'TvlDateE';
                if (CheckDatebox(FormName, fieldName) == false) { return false; }

                if ($('#dataFormMasterTvlDateE').datebox('getValue') < $('#dataFormMasterTvlDateS').datebox('getValue')) {
                    alert('注意!!,回程時間不得早於出發時間!!');
                    $('#dataFormMasterTvlDateE').datebox("textbox").focus();
                    return false;
                }

                fieldName = 'TvlNationCity';
                if (CheckVal(FormName, fieldName) == false) { return false; }
                fieldName = 'PreTvlDescr';
                if (CheckVal(FormName, fieldName) == false) { return false; }
                if ($('#dataGridDetailTvl1').datagrid('getData').total == 0) {//$('#dataGridDetailTvl1').datagrid('getData')會回傳json object物件ex:object{total: 0, tableName: "dbo.[BizTravelMaster]", keys: "AutoKey,TvlNo", rows: Array[0], footer: Array[1]}
                    alert('注意!!,未新增出差人員,請填寫!!');
                    return false;
                }
                //有填金額就需填幣別
                var ba1=$.trim($('#dataFormMasterBorrowAmt1').val());
                if (ba1 != '0' && ba1 != '') {
                    fieldName = 'Currency1';
                    if (CheckCombobox(FormName, fieldName) == false) { return false; }
                }
                var ba2=$.trim($('#dataFormMasterBorrowAmt2').val());
                if (ba2 != '0' && ba2 != '') {
                    fieldName = 'Currency2';
                    if (CheckCombobox(FormName, fieldName) == false) { return false; }
                }
                //有填金額就需填付款方式
                if ((ba1 != '0' && ba1 != '') || (ba2 != '0' && ba2 != '')) {
                    fieldName = 'BorrowPayType';
                    if (CheckCombobox(FormName, fieldName) == false) { return false; }
                }
                //if (TvlDateSOnBlur() == false) { return false; } //已被validate控制項取代
                //if (DemandDateOnBlur() == false) { return false; }

            } else if (param == 'insurance' || param == 'borrowA') {//總務保險、預支審核時
                fieldName = 'InsuraceAmt';
                if (CheckVal(FormName, fieldName) == false) { return false; }
                //有填金額就必需填幣別
                var ba1 = $.trim($('#dataFormMasterABorrowAmt1').val());
                if (ba1 != '0' && ba1 != '') {
                    fieldName = 'ACurrency1';
                    if (CheckCombobox(FormName, fieldName) == false) { return false; }
                }
                var ba2 = $.trim($('#dataFormMasterABorrowAmt2').val());
                if (ba2 != '0' && ba2 != '') {
                    fieldName = 'ACurrency2';
                    if (CheckCombobox(FormName, fieldName) == false) { return false; }
                }
                //有填金額就必需填付款方式
                if ((ba1 != '0' && ba1 != '') || (ba2 != '0' && ba2 != '')) {
                    fieldName = 'BorrowPayType';
                    if (CheckCombobox(FormName, fieldName) == false) { return false; }
                }
            //} else if (param == 'realFee') {
            //    fieldName = 'FeeDetailsPath';
            //    if (CheckUpload(FormName, fieldName) == false) { return false; }
            } else if (param == 'report') {//實際費用,費用明細報告,出差報告
                //if (CheckCombobox(FormName, 'CUR1') == false) { return false; }
                ////if (CheckVal(FormName, 'RealTvlAmt1') == false) { return false; }
                //var FieldValue = $('#dataFormMasterRealTvlAmt1').val();
                //if ($.trim(FieldValue) == '' || FieldValue == undefined||$.trim(FieldValue) == 0) {
                //    alert('注意!!,未填寫"實際金額1",請填寫!!');
                //    $('#dataFormMasterRealTvlAmt1').focus();
                //    return false;
                //}
              if (CheckUpload(FormName, 'FeeDetailsPath') == false) { return false; }
              if (CheckUpload(FormName, 'TvlReportsPath') == false) { return false; }
            }
        }
        function dataFormDetailOnApply() {
            var FormName = '#dataFormDetail';
            var fieldName = 'AccStaffID';
            if (CheckCombobox(FormName, fieldName) == false) { return false; }
            fieldName = 'AccStaffPID';
            if (CheckVal(FormName, fieldName) == false) { return false; }
            var fieldName = 'AccStaffInsGroupName';
            if (CheckCombobox(FormName, fieldName) == false) { return false; }
            fieldName = 'AccStaffBirthday';
            if (CheckDatebox(FormName, fieldName) == false) { return false; }
            //有填金額就須填付款方式
            var avf = $.trim($(FormName+'AirfareVisafee').val());
            if (avf != '0' && avf != '') {
                fieldName = 'AiviPayType';
                if (CheckCombobox(FormName, fieldName) == false) { return false; }
            }
            //AddAirfareVisafee();
        }
        function TvlDateSOnBlur() {
            var a = Date.parse($('#dataFormMasterApplyDate').datebox('getText'));
            var b = Date.parse($('#dataFormMasterTvlDateS').datebox('getText'));
            //alert(a + ';' + b);
            if (a > b) {
                //alert('注意!!,出發日不得早於申請日');
                return false;
            } else { return true; }
        }
        function DemandDateOnBlur() {
            var a = Date.parse($('#dataFormMasterApplyDate').datebox('getText'));
            var b = Date.parse($('#dataFormMasterDemandDate').datebox('getText'));
            //alert(a + ';' + b);
            if (parseInt(a) > parseInt(b)) {
                //alert('注意!!,需求日不得早於申請日');
                return false;
            } else { return true; }
        }
        function FeeDetailsPathOnSuccess() {
            $('input[name="FeeDetailsPath"]').attr('disabled', true);
        }
        function TvlReportsPathOnSuccess() {
            $('input[name="TvlReportsPath"]').attr('disabled', true);
        }
        function FeeDetailsReceiptPathOnSuccess() {
            $('input[name="FeeDetailsReceiptPath"]').attr('disabled', true);
        }
        function CheckCombobox(FormName, fieldName) {
            var FieldValue = $(FormName + fieldName).combobox('getValue');
            var FieldNameC = $(FormName + fieldName).closest('td').prev('td').text();
            if ($.trim(FieldValue) == '' || FieldValue == undefined) {
                alert('注意!!,未選取 " ' + FieldNameC + ' " ,請選取!!');
                $(FormName + fieldName).focus();
                return false;
            }
        }
        function CheckDatebox(FormName, fieldName) {
            var FieldValue = $(FormName + fieldName).datebox('getValue');
            var FieldNameC = $(FormName + fieldName).closest('td').prev('td').text();
            if ($.trim(FieldValue) == '' || FieldValue == undefined) {
                alert('注意!!,未選取 " ' + FieldNameC + ' " ,請選取!!');
                $(FormName + fieldName).datebox("textbox").focus();
                return false;
            }
        }
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
        function CheckRefVal(FormName, fieldName) {
            var FieldValue = $(FormName + fieldName).refval('selectItem').text;
            var FieldNameC = $(FormName + fieldName).closest('td').prev('td').text();
            if ($.trim(FieldValue) == '' || FieldValue == undefined) {
                alert('注意!!,未選取 " ' + FieldNameC + ' " ,請選取!!');
                $(FormName + fieldName).focus();
                return false;
            }
        }
        function HideFields(FieldNames) {
            var FormName = '#dataFormMaster';
            $.each(FieldNames, function (index, value) {
                $(FormName + value).closest('td').prev('td').hide();//closest上一層selector,prev下一個selector
                $(FormName + value).closest('td').hide();
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
        function RedTd(FormName, FieldNames) {
            //var FormName = '#dataFormMaster';
            $.each(FieldNames, function (index, value) {
                $(FormName + value).closest('td').prev('td').css({ 'color': 'red' });
            });
        }
        function AirfareVisafeeOnTotal(rowData) {
            $("#dataFormMasterTotalAFVF").val(rowData.AirfareVisafee);
        }
        function GetSumRACountRA() {
            var TvlNo = $('#dataFormMasterTvlNo').val();
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sBizTravel.BizTravelMaster', //連接的Server端，command
                data: "mode=method&method=" + "SelectRequisition" + "&parameters=" + TvlNo, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    $('#dataFormMasterInsuraceAmt').val(rows[0].SumR);
                    if (rows[0].CountR > 0) {
                        $('#ReqButton').linkbutton({ 'text': '旅平險申請- 已申請' + rows[0].CountR + '筆' });
                    }
                }
            });
        }
        function JQDataForm1OnApplied() {
            var ReqNo = $('#JQDataForm1RequisitionNO').val();
            var RoleID = getClientInfo("_GroupID");//_GroupName
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sBizTravel.BizTravelMaster', //連接的Server端，command
                data: "mode=method&method=" + "MakeRequisition" + "&parameters=" + ReqNo, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    if (data != false) {//data == true會進不去
                        GetSumRACountRA();
                        alert('您已成功申請旅平險之請款單');
                    } else {
                        alert('申請失敗，請重新申請旅平險');
                    }
                    closeForm('#JQDialog3');
                }
            });
        }
        function GG(ReqNo) {
            //var UserID = getClientInfo("UserID");
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sBizTravel.BizTravelMaster', //連接的Server端，command
                data: "mode=method&method=" + "MakeRequisition" + "&parameters=" + ReqNo, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    alert(data);
                }
            });
        }
        //#region  ////////////////////////////////////以下copy from 請款單///////////////////
        function JQDataForm1OnLoadSuccess() {
            //var parameters = Request.getQueryStringByName("P1");
            //var mode = Request.getQueryStringByName("NAVIGATOR_MODE");
            //if (getEditMode($("#JQDataForm1")) == 'inserted') {
            var UserID = getClientInfo("UserID");
            ////依使用者過濾暫借單號
            //var whereStr = "EmployeeID =  " + "'" + UserID + "'";
            //$('#JQDataForm1ShortTermNO').combobox('setWhere', '1=2');
            //$('#JQDataForm1ShortTermNO').combobox('setWhere', whereStr);
            GetUserOrgNOs();//依登入者來設ApplyOrg_NO申請部門、Org_NOParent直屬主管的部門
            var EmpFlowAgentList = GetEmpFlowAgentList();//登入者的有效代理人人員清單
            var whereStr = " EmployeeID in (" + EmpFlowAgentList + ")";
            $('#JQDataForm1ApplyEmpID').combobox('setWhere', whereStr);
            //var FormName = '#JQDataForm1';
            //var HideFieldName = ['IsRemit', 'RemitType', 'Remit'];
            //$.each(HideFieldName, function (index, fieldName) {
            //    $(FormName + fieldName).closest('td').prev('td').hide();
            //    $(FormName + fieldName).closest('td').hide();
            //});
            //}
            //if (parameters == "SERVICE" && mode == "0") {
            //$('#JQDataForm1IsRemit').removeAttr("disabled");
            //$("#JQDataForm1RemitType").combobox('enable');  //combobox 設為可用
            //$('#JQDataForm1Remit').removeAttr("disabled");
            //}
            //alert('ok');
            //在請款性質加入請款性質說明超連結
            //var RequisitKindLink = $("<a>").attr({ 'href': '../JB_ADMIN/Files/委任權限表.pdf', 'target': '_blank' }).text("     請款性質說明");
            //var RequistKind = $('#JQDataForm1RequistKindID').closest('td');
            //RequistKind.append(RequisitKindLink);
            //RequistKind.remove($('a'));
            //RequistKind.append("<a href='../JB_ADMIN/Files/委任權限表.pdf' target='_blank'>請款性質說明</a>");

            //設定緊急付款、非付款日付款 欄位Caption 變顏色
            var flagIDs = ['#JQDataForm1IsUrgentPay', '#JQDataForm1IsNotPayDate'];
            $(flagIDs.toString()).each(function () {
                var captionTd = $(this).closest('td').prev('td');
                captionTd.css({ color: '#8A2BE2' });
            });

            $('#JQDataForm1SourceBillNO').val($('#dataFormMasterTvlNo').val());
            $('#JQDataForm1RequisitionDescr').val($('#dataFormMasterBizTvlGist').val() + '-旅平險');

            $('#JQDataForm1CostCenterID').combobox('setValue', $('#dataFormMasterCostCenterID').combobox('getValue'));
            $('#JQDataForm1AccountType').combobox('setValue', $('#dataFormMasterAccountType').combobox('getValue'));
            $('#JQDataForm1AccountID').combobox('setValue', $('#dataFormMasterAccountID').combobox('getValue'));
            $('#JQDataForm1CompanyID').combobox('setValue', $('#dataFormMasterCompanyID').combobox('getValue'));
            $('#JQDataForm1RequisitionNotes').val('出差起訖日期'+$('#dataFormMasterTvlDateS').datebox('getValue') + "~" + $('#dataFormMasterTvlDateE').datebox('getValue'));

            var DisabledFieldName = [];
            var DisabledComboboxName = ['ApplyEmpID'];
            DisableFields('#JQDataForm1', DisabledFieldName, DisabledComboboxName);

            var RedFieldName = ['CompanyID', 'CostCenterID', 'AccountType', 'AccountID', 'CostCenterID', 'RequisitionDescr', 'RequisitionTypeID', 'ProofTypeID',
                'RequisitAmt', 'PayTo', 'PayTypeID', 'PayTermID', 'PlanPayDate'];
            RedTd('#JQDataForm1', RedFieldName);
        }
        //以預付日期來設 非付款日付款,緊急付款(LoadSuccess時會呼叫)
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
        //JQDataForm1的Apply時呼叫
        function checkCombo() {

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


        }
        //當選取付款客戶時,來設定付款條件PayTermID與付款方式PayTypeID並
        //                  由付款方式改變須匯款費、匯款費誰付, 由付款條件改變預付日期(受款人Select會呼叫)
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
        //當選取付款方式時,來設定須匯款費與匯款費誰付(付款方式select時會呼叫)
        function GetPayType(rowData) {
            var pp = $("#JQDataForm1PayTypeID").combobox('getValue'); //combobox 取值
            if (pp != 2) {
                $("#JQDataForm1IsRemit").checkbox('setValue', 0);//須匯款費
                $("#JQDataForm1RemitType").combobox('setValue', "");//匯款費誰付
                $("#JQDataForm1Remit").val(0); //text給值
            }
        }
        //當選取付款條件時,來改變預付日期
        function GetPayDateByTerm() {
            var PayTerm = $("#JQDataForm1PayTermID").combobox('getText');
            var tdate = GetPlanPayDate(PayTerm, 5, $("#JQDataForm1ApplyDate").datebox('getValue'));
            $('#JQDataForm1PlanPayDate').datebox('setValue', tdate);
        }
        //傳入付款條件,作業天數,申請日期求得預付日期
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
        //計算付款日(回傳預付日期)
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
        //申請員工→申請部門、直屬主管的部門
        function OnSelectEmployee(rowData) {
            alert(rowData.ApplyEmpID);
            //var whereStr = "EmployeeID =  " + "'" + UserID + "'";
            //$('#dataFormMasteShortTermNO').combobox('setWhere', '1=2');
            //$('#dataFormMasteShortTermNO').combobox('setWhere', whereStr);

            $("#JQDataForm1ApplyOrg_NO").combobox('setValue', rowData.OrgNO);
            $("#JQDataForm1Org_NOParent").val(rowData.OrgNOParent);
        }
        //當點按關閉按鈕時,關閉目前Tab(JQDataForm1的Cancel時)
        function CloseDataForm() {
            //self.parent.closeCurrentTab();
            //self.closeCurrentTab();
            //return false;
        }
        //檢查憑證號碼
        function CheckProofNO() {
            var ProofType = $("#JQDataForm1ProofTypeID").combobox("getValue");
            var ProofNO = $("#JQDataForm1ProofNO").val();
            var ProofNOLen = ProofNO.length;
            if ((ProofType == 1) && (ProofNOLen != 10)) {
                return false;
            }
            return true;
        }
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
        //由登入者設定申請部門、直屬主管的部門
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
        //成本中心select→CostCenterName，清空科目類別,會計科目
        function CostCenterIDOnSelect1() {
            var tt = $('#JQDataForm1CostCenterID').combobox('getText');
            $('#JQDataForm1CostCenterName').val(tt);
            $("#JQDataForm1AccountType").combobox('setValue', "");
            $("#JQDataForm1AccountID").combobox('setValue', "");
        }
        //科目類型→會計科目
        function AccountTypeOnSelect1(rowData) {
            $("#JQDataForm1AccountID").combobox("setValue", "");
            var CostCenterID = $("#JQDataForm1CostCenterID").combobox("getValue");
            var FiltStr = "AccountType=" + "'" + rowData.AccountType + "'" + " and (LimitCostCenters='' or LimitCostCenters like '%" + CostCenterID + "%' or LimitCostCenters is null)";
            $("#JQDataForm1AccountID").combobox('setWhere', FiltStr);
        }
        //#endregion
        </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sBizTravel.BizTravelMaster" runat="server" AutoApply="True"
                DataMember="BizTravelMaster" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="TvlNo" Editor="text" FieldName="TvlNo" Format="" MaxLength="0" Visible="true" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請人" Editor="text" FieldName="ApplyEmpName" Format="" MaxLength="0" Visible="true" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="成本中心" Editor="text" FieldName="CostCenterName" Format="" MaxLength="0" Visible="true" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ApplyDate" Format="" Visible="true" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請事由" Editor="text" FieldName="BizTvlGist" MaxLength="0" Visible="true" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="出發時間" Editor="datebox" FieldName="TvlDateS" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="回程時間" Editor="datebox" FieldName="TvlDateE" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="出差地點" Editor="text" FieldName="TvlNationCity" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="領款人" Editor="text" FieldName="DrawEmpName" Format="" MaxLength="0" Visible="true" Width="50" />
                    <JQTools:JQGridColumn Alignment="left" Caption="預支需求日" Editor="datebox" FieldName="DemandDate" Format="" MaxLength="0" Visible="true" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="FeeDetailsPath" Editor="text" FieldName="FeeDetailsPath" Format="download,folder:JB_ADMIN/BizTravelFeeDetails" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="TvlReportsPath" Editor="text" FieldName="TvlReportsPath" Format="download,folder:JB_ADMIN/BizTravelReport" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Visible="true" Width="80" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>
            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="國外及長期出差申請表" DialogCenter="False" EditMode="Dialog" Width="800px" DialogTop="6px" DialogLeft="6px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="BizTravelMaster" HorizontalColumnsCount="4" RemoteName="sBizTravel.BizTravelMaster" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="True" IsAutoPause="False" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="True" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="FormOnLoadSuccess" OnApply="dataFormMasterOnApply" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="出差申請單號" Editor="text" FieldName="TvlNo" Format="" Width="117" ReadOnly="True" Span="1" NewRow="True" Visible="True" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="yyyy/mm/dd" Width="100" ReadOnly="True" Span="1" Visible="False" maxlength="0" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請人" Editor="text" FieldName="ApplyEmpName" Format="" Width="117" ReadOnly="True" Span="1" Visible="True" maxlength="0" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ApplyDate" Format="yyyy/mm/dd" Width="140" Span="2" ReadOnly="False" Visible="True" maxlength="0" OnBlur="" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ApplyEmpID" Editor="text" FieldName="ApplyEmpID" Format="" ReadOnly="True" Width="180" Visible="False" Span="1" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sBizTravel.Company',tableName:'Company',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CompanyID" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="成本中心" Editor="infocombobox" FieldName="CostCenterID" Format="" Width="120" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sBizTravel.CostCenter',tableName:'CostCenter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:CostCenterIDOnSelect,panelHeight:200" Span="1" NewRow="True" ReadOnly="False" Visible="True" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="旅平險類別" Editor="infocombobox" EditorOptions="valueField:'AccountType',textField:'AccountType',remoteName:'sBizTravel.AccountType',tableName:'AccountType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:AccountTypeOnSelect,panelHeight:200" FieldName="AccountType" MaxLength="0" NewRow="False" ReadOnly="False" Span="1" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="旅平險會計科目" Editor="infocombobox" EditorOptions="valueField:'AccountID',textField:'AccountName',remoteName:'sBizTravel.AccountTitle',tableName:'AccountTitle',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AccountID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="140" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CostCenterName" Editor="text" FieldName="CostCenterName" Format="" Width="180" EditorOptions="" ReadOnly="True" Visible="False" Span="1" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請事由" Editor="text" FieldName="BizTvlGist" NewRow="True" Span="4" Width="600" OnBlur="" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="出發時間" Editor="datebox" FieldName="TvlDateS" Format="yyyy/mm/dd HH:MM" Width="120" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:false,editable:true" Span="1" NewRow="True" OnBlur="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="回程時間" Editor="datebox" FieldName="TvlDateE" Format="yyyy/mm/dd HH:MM" Width="120" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:false,editable:true" Span="1" OnBlur="" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="出差國與城市" Editor="text" FieldName="TvlNationCity" Format="" Width="137" Span="2" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="出差預計行程說明" Editor="textarea" FieldName="PreTvlDescr" Format="" Width="600" EditorOptions="height:50" Span="4" NewRow="True" maxlength="0" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="隨同客戶" Editor="text" FieldName="AccCustomers" Format="" Width="600" Span="4" NewRow="True" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="拜訪客戶" Editor="text" FieldName="PlanVisitCustomers" Format="" Width="600" Span="4" NewRow="True" Visible="True" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="預支幣別1" Editor="infocombobox" EditorOptions="valueField:'CurrencyName',textField:'CurrencyName',remoteName:'sBizTravel.Currency',tableName:'Currency',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="Currency1" Format="" Span="1" Width="60" NewRow="True" Visible="True" maxlength="0" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="預支金額1" Editor="numberbox" FieldName="BorrowAmt1" Format="" Width="80" Span="1" NewRow="False" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="預支幣別2" Editor="infocombobox" FieldName="Currency2" Format="" Width="60" EditorOptions="valueField:'CurrencyName',textField:'CurrencyName',remoteName:'sBizTravel.Currency',tableName:'Currency',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Span="1" NewRow="False" ReadOnly="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="預支金額2" Editor="numberbox" FieldName="BorrowAmt2" Format="" Span="1" Width="77" NewRow="False" ReadOnly="False" Visible="True" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="預支付款方式" Editor="infocombobox" EditorOptions="items:[{value:'現金',text:'現金',selected:'false'},{value:'匯款',text:'匯款',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="BorrowPayType" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="機簽費總額" Editor="text" FieldName="TotalAFVF" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="2" Visible="True" Width="87" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="text" FieldName="Notes" Format="" Width="600" EditorOptions="" Span="4" NewRow="False" ReadOnly="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="領款需求日" Editor="datebox" FieldName="DemandDate" Format="yyyy/mm/dd" Width="90" Span="2" ReadOnly="False" NewRow="True" Visible="True" MaxLength="0" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="領款人" Editor="infocombobox" FieldName="DrawEmpID" Format="" Width="90" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sBizTravel.USERS',tableName:'USERS',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:DrawEmpIDOnSelect,panelHeight:200" Span="2" ReadOnly="False" Visible="True" NewRow="False" MaxLength="0" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="領款人" Editor="text" FieldName="DrawEmpName" Format="" Width="180" EditorOptions="" Visible="False" Span="1" ReadOnly="False" NewRow="False" MaxLength="0" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="投保總金額" Editor="numberbox" FieldName="InsuraceAmt" Format="" Width="87" Span="4" ReadOnly="True" MaxLength="0" NewRow="True" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="會計預支幣別1" Editor="infocombobox" EditorOptions="valueField:'CurrencyName',textField:'CurrencyName',remoteName:'sBizTravel.Currency',tableName:'Currency',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ACurrency1" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="60" />
                        <JQTools:JQFormColumn Alignment="left" Caption="會計預支1" Editor="numberbox" FieldName="ABorrowAmt1" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="會計預支幣別2" Editor="infocombobox" EditorOptions="valueField:'CurrencyName',textField:'CurrencyName',remoteName:'sBizTravel.Currency',tableName:'Currency',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ACurrency2" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="60" />
                        <JQTools:JQFormColumn Alignment="left" Caption="會計預支2" Editor="numberbox" EditorOptions="" FieldName="ABorrowAmt2" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="實際幣別1" Editor="infocombobox" FieldName="CUR1" Width="60" ReadOnly="False" Span="1" EditorOptions="valueField:'CurrencyName',textField:'CurrencyName',remoteName:'sBizTravel.Currency',tableName:'Currency',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" NewRow="True" Visible="True" maxlength="0" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="實際金額1" Editor="numberbox" FieldName="RealTvlAmt1" Width="80" ReadOnly="False" Span="1" MaxLength="0" NewRow="False" RowSpan="1" Visible="True" Format="" OnBlur="RealTvlAmtOnBlur" />
                        <JQTools:JQFormColumn Alignment="left" Caption="實際幣別2" Editor="infocombobox" FieldName="CUR2" Width="60" Span="1" NewRow="False" ReadOnly="False" Visible="True" EditorOptions="valueField:'CurrencyName',textField:'CurrencyName',remoteName:'sBizTravel.Currency',tableName:'Currency',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" maxlength="0" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="實際金額2" Editor="numberbox" FieldName="RealTvlAmt2" Width="80" ReadOnly="False" MaxLength="0" NewRow="False" RowSpan="1" Span="1" Visible="True" Format="" OnBlur="RealTvlAmtOnBlur" />
                        <JQTools:JQFormColumn Alignment="left" Caption="領款人還款1" Editor="numberbox" FieldName="Reback1" MaxLength="0" ReadOnly="True" Span="2" Width="87" NewRow="True" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="領款人還款2" Editor="numberbox" FieldName="Reback2" NewRow="False" ReadOnly="True" Span="2" Visible="True" Width="87" maxlength="0" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="出差費用明細" Editor="infofileupload" EditorOptions="filter:'',isAutoNum:true,upLoadFolder:'JB_ADMIN/BizTravelFeeDetails',showButton:true,showLocalFile:false,onSuccess:FeeDetailsPathOnSuccess,fileSizeLimited:'5000'" FieldName="FeeDetailsPath" maxlength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="190" />
                        <JQTools:JQFormColumn Alignment="left" Caption="出差報告" Editor="infofileupload" EditorOptions="filter:'',isAutoNum:true,upLoadFolder:'JB_ADMIN/BizTravelReport',showButton:true,showLocalFile:false,onSuccess:TvlReportsPathOnSuccess,fileSizeLimited:'5000'" FieldName="TvlReportsPath" Span="2" Width="190" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="出差費用收據" Editor="infofileupload" EditorOptions="filter:'',isAutoNum:true,upLoadFolder:'JB_ADMIN/BizTravelFeeDetailsReceipt',showButton:true,showLocalFile:false,onSuccess:FeeDetailsReceiptPathOnSuccess,fileSizeLimited:'20000'" FieldName="FeeDetailsReceiptPath" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="190" />
                        <JQTools:JQFormColumn Alignment="left" Caption="出差費用明細下載" Editor="text" FieldName="FeeDetailsPath1" Format="download,folder:JB_ADMIN/BizTravelFeeDetails" maxlength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="出差報告下載" Editor="text" FieldName="TvlReportsPath1" Format="download,folder:JB_ADMIN/BizTravelReport" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="費用收據下載" Editor="text" FieldName="FeeDetailsReceiptPath1" Format="download,folder:JB_ADMIN/BizTravelFeeDetailsReceipt" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQButton ID="ReqButton" runat="server" Icon="icon-add" OnClick="ReqButtonOnClick" Text="旅平險申請" />
                <div id="divGrid1">
                    <JQTools:JQDataGrid ID="dataGridDetailTvl1" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" AutoApply="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="BizTravelDetails_Accom" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialog2" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" OnDelete="GridDetailTvl1OnDelete" OnInsert="GridDetailTvl1OnInsert" PageList="10,20,30,40,50" PageSize="10" Pagination="False" ParentObjectID="dataFormMaster" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sBizTravel.BizTravelMaster" RowNumbers="True" Title="出差人員" TotalCaption="加總" UpdateCommandVisible="True" ViewCommandVisible="True">
                        <Columns>
                            <JQTools:JQGridColumn Alignment="left" Caption="編號" Editor="text" FieldName="AutoKey" Visible="True" Width="80">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="left" Caption="出差單號" Editor="text" FieldName="TvlNo" Visible="False" Width="30" />
                            <JQTools:JQGridColumn Alignment="left" Caption="工號" Editor="text" FieldName="AccStaffID" Format="" Width="50" />
                            <JQTools:JQGridColumn Alignment="left" Caption="姓名" Editor="text" FieldName="AccStaffName" Format="" Width="60" />
                            <JQTools:JQGridColumn Alignment="left" Caption="身份證字號" Editor="text" FieldName="AccStaffPID" Format="" Width="100" />
                            <JQTools:JQGridColumn Alignment="left" Caption="投保單位" Editor="text" FieldName="AccStaffInsGroupName" Format="" Width="120" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" />
                            <JQTools:JQGridColumn Alignment="left" Caption="出生日期" Editor="datebox" EditorOptions="dateFormat:'datetime',showTimeSpinner:false,showSeconds:false,editable:true" FieldName="AccStaffBirthday" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                            <JQTools:JQGridColumn Alignment="left" Caption="機簽費" Editor="text" FieldName="AirfareVisafee" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50" OnTotal="AirfareVisafeeOnTotal" Total="sum" />
                            <JQTools:JQGridColumn Alignment="left" Caption="付款方式" Editor="text" FieldName="AiviPayType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                            </JQTools:JQGridColumn>
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="TvlNo" ParentFieldName="TvlNo" />
                        </RelationColumns>
                        <TooItems>
                            <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                            <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="更改" />
                            <JQTools:JQToolItem Enabled="True" Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="刪除" Visible="True" />
                            <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="下載Excel" Visible="True" />
                        </TooItems>
                    </JQTools:JQDataGrid>
                </div>
                <div id="divGrid2" style="display:none">
                    <JQTools:JQDataGrid ID="dataGridDetailTvl2" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" AutoApply="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="BizTravelDetails_Accom" DeleteCommandVisible="False" DuplicateCheck="False" EditDialogID="JQDialog2" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="False" ParentObjectID="dataFormMaster" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sBizTravel.BizTravelMaster" RowNumbers="True" Title="出差人員" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True">
                        <Columns>
                            <JQTools:JQGridColumn Alignment="left" Caption="編號" Editor="text" FieldName="AutoKey" Frozen="False"  Visible="True" Width="80" Total="">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="left" Caption="出差單號" Editor="text" FieldName="TvlNo" Width="30" Visible="False"/>
                            <JQTools:JQGridColumn Alignment="left" Caption="員工工號" Editor="text" FieldName="AccStaffID" Format="" Width="50" />
                            <JQTools:JQGridColumn Alignment="left" Caption="姓名" Editor="text" FieldName="AccStaffName" Format="" Width="60" />
                            <JQTools:JQGridColumn Alignment="left" Caption="身份證字號" Editor="text" FieldName="AccStaffPID" Format="" Width="100" />
                            <JQTools:JQGridColumn Alignment="left" Caption="投保單位" Editor="text" FieldName="AccStaffInsGroupName" Format=""  Width="120" />
                            <JQTools:JQGridColumn Alignment="left" Caption="出生日期" Editor="datebox" EditorOptions="dateFormat:'datetime',showTimeSpinner:false,showSeconds:false,editable:true" FieldName="AccStaffBirthday" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                            <JQTools:JQGridColumn Alignment="left" Caption="機簽費" Editor="text" FieldName="AirfareVisafee"  Visible="True" Width="50" Total="" />
                            <JQTools:JQGridColumn Alignment="left" Caption="付款方式" Editor="text" FieldName="AiviPayType" Visible="True" Width="60">
                            </JQTools:JQGridColumn>
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="TvlNo" ParentFieldName="TvlNo" />
                        </RelationColumns>
                        <TooItems>
                            <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                            <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="更改" />
                            <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="刪除" />
                        </TooItems>
                    </JQTools:JQDataGrid>
                </div>
                <br />
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="TvlNo" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="ApplyEmpName" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="ApplyEmpID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="DrawEmpID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="DrawEmpName" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="DemandDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="ApplyDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="BorrowAmt1" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="BorrowAmt2" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="RealTvlAmt1" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="RealTvlAmt2" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="ABorrowAmt1" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="ABorrowAmt2" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckMethod="TvlDateSOnBlur" CheckNull="False" FieldName="TvlDateS" RemoteMethod="False" ValidateMessage="出發日不得早於申請日" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckMethod="DemandDateOnBlur" CheckNull="False" FieldName="DemandDate" RemoteMethod="False" ValidateMessage="領款需求日不得早於申請日" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>      
            </JQTools:JQDialog>
            <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" Width="500px">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" ParentObjectID="dataFormMaster" DataMember="BizTravelDetails_Accom" HorizontalColumnsCount="2" RemoteName="sBizTravel.BizTravelMaster" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApply="dataFormDetailOnApply" >
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="TvlNo" Editor="text" FieldName="TvlNo" ReadOnly="True" Width="116" MaxLength="0" NewRow="True" RowSpan="1" Span="1" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="出差人員" Editor="infocombobox" FieldName="AccStaffID" Format="" Width="120" EditorOptions="valueField:'EMPLOYEE_CODE',textField:'NAME_C',remoteName:'sBizTravel.EMPINFO',tableName:'EMPINFO',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:AccStaffIDOnSelect,panelHeight:200" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="AccStaffName" Editor="text" FieldName="AccStaffName" Format="" Width="120" EditorOptions="" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="身分證字號" Editor="text" FieldName="AccStaffPID" Format="" Width="156" NewRow="False" ReadOnly="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="出生日期" Editor="datebox" EditorOptions="dateFormat:'datetime',showTimeSpinner:false,showSeconds:false,editable:true" FieldName="AccStaffBirthday" Format="yyyy/mm/dd" RowSpan="1" Span="1" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="投保單位" Editor="infocombobox" FieldName="AccStaffInsGroupName" Format="" Width="160" EditorOptions="valueField:'InsGroupName',textField:'InsGroupName',remoteName:'sBizTravel.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                            <JQTools:JQFormColumn Alignment="left" Caption="機票與簽證費" Editor="numberbox" FieldName="AirfareVisafee" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="機加簽付款方式" Editor="infocombobox" EditorOptions="items:[{value:'現金',text:'現金',selected:'false'},{value:'匯款',text:'匯款',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AiviPayType" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="TvlNo" ParentFieldName="TvlNo" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                    <JQTools:JQAutoSeq ID="JQAutoSeq1" runat="server" BindingObjectID="dataFormDetail" FieldName="AutoKey" />
                    <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    </JQTools:JQValidate>
                    <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    </JQTools:JQDefault>
                </JQTools:JQDialog>
            <JQTools:JQDialog ID="JQDialog3" runat="server"  BindingObjectID="JQDataForm1" Title="請款單申請" Width="730px" DialogLeft="10px" DialogTop="10px">
                <JQTools:JQDataForm ID="JQDataForm1" runat="server" DataMember="Requisition" HorizontalColumnsCount="3" RemoteName="sRequisition.Requisition" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" Width="720px" IsAutoPause="False" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" OnLoadSuccess="JQDataForm1OnLoadSuccess" OnApplied="JQDataForm1OnApplied" OnApply="checkCombo">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="來源單號" Editor="text" FieldName="RequisitionNO" Format="" maxlength="0" Width="127"  ReadOnly="True" Span="1"/>
                        <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sRequisition.Company',tableName:'Company',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:250" FieldName="CompanyID" maxlength="0" ReadOnly="False" Visible="True" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請款性質" Editor="infocombobox" EditorOptions="valueField:'RequisitKindID',textField:'RequistKindName',remoteName:'sRequisition.RequistKind',tableName:'RequistKind',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:150" FieldName="RequistKindID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請員工" Editor="infocombobox" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sRequisition.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OnSelectEmployee,panelHeight:200" FieldName="ApplyEmpID" MaxLength="0" ReadOnly="False" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請部門" Editor="infocombobox" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sRequisition.Organization',tableName:'Organization',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:250" FieldName="ApplyOrg_NO" Format="" ReadOnly="True" Width="130" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ApplyDate" Format="yyyy/mm/dd" ReadOnly="True" Width="90" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="成本中心" Editor="infocombobox" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sRequisition.CostCenter',tableName:'CostCenter',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:CostCenterIDOnSelect1,panelHeight:250" FieldName="CostCenterID" Format="" Span="1" Width="133" MaxLength="0" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="科目類別" Editor="infocombobox" EditorOptions="valueField:'AccountType',textField:'AccountType',remoteName:'sBizTravel.AccountType',tableName:'AccountType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:AccountTypeOnSelect1,panelHeight:200" FieldName="AccountType" MaxLength="0" ReadOnly="False" Span="1" Width="130" NewRow="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="會計科目" Editor="infocombobox" EditorOptions="valueField:'AccountID',textField:'AccountName',remoteName:'sBizTravel.AccountTitle',tableName:'AccountTitle',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AccountID" Span="1" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請款事由" Editor="textarea" EditorOptions="height:40" FieldName="RequisitionDescr" Format="" Span="3" Width="580" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請款依據" Editor="infocombobox" EditorOptions="valueField:'RequisitionTypeID',textField:'RequisitionTypeName',remoteName:'sRequisition.RequisitionType',tableName:'RequisitionType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="RequisitionTypeID" Format="" Width="133" maxlength="0" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="檢附憑證" Editor="infocombobox" EditorOptions="valueField:'ProofTypeID',textField:'ProofTypeName',remoteName:'sRequisition.ProofType',tableName:'ProofType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="ProofTypeID" Format="" Width="130" OnBlur="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請款金額" Editor="numberbox" FieldName="RequisitAmt" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="130" Format="" />
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
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="3" FieldName="RequisitionTypeID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="4" FieldName="ProofTypeID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="IsUrgentPay" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="IsNotPayDate" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
        </JQTools:JQDialog>
        </div>
        <p>
            &nbsp;</p>
    </form>
</body>
</html>
