<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_Customer.aspx.cs" Inherits="Template_JQuerySingle1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        var backcolor = "#E8FFE8"
        $(document).ready(function () {
            //設定欄位Caption 變顏色
            var flagIDs = ['#dataFormMasterBillDeal', '#dataFormMasterBillDealEmail', '#dataFormMasterIsPublicView', '#dataFormMasterIsAcceptePaper', '#dataFormMasterNotAcceptPaper', '#dataFormMasterIsAddLine', '#dataFormMasterPostType', '#dataFormMasterIsChangeBank', '#dataFormMasterIndustryType'];
            $(flagIDs.toString()).each(function () {
                var captionTd = $(this).closest('td').prev('td');
                captionTd.css({ color: "#4400CC" });
             });
            //將Focus 欄位背景顏色改為黃色
            $(function () {
               
                $("input, select, textarea").focus(function () {
                    $(this).css("background-color", "#FFFFDE");
                });
                $("input, select, textarea").blur(function () {
                    $(this).css("background-color", backcolor);
                });
            });
            var CustCityNO = $('#dataFormMasterCustCityNO').closest('td');
            var CustRegion = $('#dataFormMasterCustRegion').closest('td').children();
            var CustPost = $('#dataFormMasterCustPost').closest('td').children();
            var CustAddr = $('#dataFormMasterCustAddr').closest('td').children();
            CustCityNO.append('-').append(CustRegion).append('-').append(CustPost).append('      地址      ').append(CustAddr);
            $('td', CustCityNO.attr('colspan', 7).closest('tr')).slice(4).remove();
            var CustDeptID = $('#dataFormMasterCustDeptID').closest('td');
            var CustDeptName = $('#dataFormMasterCustDeptName').closest('td').children();
            var CustAreaID = $('#dataFormMasterCustAreaID').closest('td').children();
            CustDeptID.append('-').append(CustDeptName).append('  區域  ').append(CustAreaID);
            var ContactASubTel = $('#dataFormMasterContactASubTel').closest('td')
            var ContactAJobID = $('#dataFormMasterContactAJobID').closest('td').children();
            ContactASubTel.append('  職稱  ').append(ContactAJobID);
            var ContactBSubTel = $('#dataFormMasterContactBSubTel').closest('td')
            var ContactBJobID = $('#dataFormMasterContactBJobID').closest('td').children();
            ContactBSubTel.append('  職稱  ').append(ContactBJobID);
            var PayNotes = $('#dataFormMasterPayNotes').closest('td');
            PayNotes.append($('<lable>').css({ color: '#8A2BE2' }).html(' 最多可輸256文數字'));
            $('#dataFormMasterCustName').blur(function () {
                var cc = $('#dataFormMasterCustShortName').val();
                if (cc == "") {
                    $('#dataFormMasterCustShortName').val($('#dataFormMasterCustName').val());
                }
            });
            ////將QUERY PANEL 按鈕置中
            $('.infosysbutton-q', '#querydataGridView').closest('td').attr({ 'align': 'middle' });
            $('.infosysbutton-q', '#queryJQDataGrid4').closest('td').attr('align', 'middle');
            //
            $('#SalesTypeID_Query').combobox({
                onChange: function (newValue, oldValue) {
                    if (newValue == undefined || newValue == '') {
                        $('#QSDate_Query').datebox('setValue', '');
                        $('#QEDate_Query').datebox('setValue', '');
                    }
                }
            });
            //$("#dataGridView").datagrid('setWhere', "(dbo.funReturnCustDealDays(A.LatelyDayD)) <=3");
            $('#dataFormMasterContactAIsMail').after($('<a>', { href: 'javascript:void(0)' }).on('click', function () {
                var ConA = $('#dataFormMasterContactA').val().trim();
                var ConType = 1;
                if ((ConA == '') || (ConA == 'undefined')) {
                    alert('注意!!未輸入聯絡人姓名,無法同步');
                    return false;
                }
                var CustNO = $('#dataFormMasterCustNO').val();
                var Center_ID = $('#dataFormMasterCENTER_ID').val();
                if (Center_ID != 35) {
                    alert('注意!!僅業務人員是媒體人脈群組可同步');
                    return false;
                }
                if (SyncContact(ConType,ConA, CustNO,Center_ID) == false) {
                    alert('同步聯絡人失敗,請洽管理室');
                    return false;
                }
            }).linkbutton({ text: '同步人脈' }));
            $('#dataFormMasterContactBIsMail').after($('<a>', { href: 'javascript:void(0)' }).on('click', function () {
                var ConB = $('#dataFormMasterContactB').val().trim();
                var ConType = 2;
                if ((ConB == '') || (ConB == 'undefined')) {
                    alert('注意!!未輸入聯絡人姓名,無法同步');
                    return false;
                }
                var CustNO = $('#dataFormMasterCustNO').val();
                var Center_ID = $('#dataFormMasterCENTER_ID').val();
                if (Center_ID != 35) {
                    alert('注意!!僅業務人員是媒體人脈群組可同步');
                    return false;
                }
                if (SyncContact(ConType,ConB, CustNO,Center_ID) == false) {
                    alert('同步 聯絡人失敗,請洽管理室');
                    return false;
                }
                
            }).linkbutton({ text: '同步人脈' }));
            $('#dataFormMasterCustNotes').after($('<a>', { href: 'javascript:void(0)' }).on('click', function () {
                if ($('#dataFormMasterCustNO').val() == '') {
                    alert('注意!!,新增模式時,無法維護連絡事項');
                    return false;
                }
                  var vdg = '#JQDataGrid4'
                  queryGrid(vdg);
                  openForm('#JQDialog3', {}, "", 'dialog');
                }).linkbutton({ text: '連絡事項' }));
            //媒體代辦事項傳入客戶代號
            var parameter = Request.getQueryStringByName("CustID");
            if (parameter != "") {
                $("#CustNO_Query").combobox('setValue', parameter);
                queryGrid('#dataGridView');
            }
            //當實際覆訪日選取時
            $('#JQDataForm2NotesCreateDate').datebox({
                width: 90,
                onSelect: function (date) {
                    var vdate = $('#JQDataForm2NotesCreateDate').datebox('getValue');
                    var NotesType = $("#JQDataForm2NotesTypeID").options('getValue');
                    if (NotesType == 2) {
                        $('#JQDataForm2NextCallDate').datebox('setValue',vdate);
                    }
                }
            }).combo('textbox').blur(function () {
                setTimeout(function () {
    
                }, 500);
            });
        });
        function OnBlurCustName() {
            if ($("#dataFormMasterCustShortName").val() == '') {
                var CustName = $("#dataFormMasterCustName").val().toString().substr(0, 6);
                $("#dataFormMasterCustShortName").val(CustName);
            }
        }
        function GetCustNO() {
            var id = $("#dataGridView").datagrid('getSelected').CustNO;
            return id;
        }
        function SetWherePayKind(rowindex, rowdata) {
                if (rowdata != null && rowdata != undefined) {
                    var CustNO = rowdata.CustNO;
                    $("#JQDataGrid1").datagrid('setWhere', "A.CustNO='" + CustNO +"'" );
                }
                else
                    $("#JQDataGrid1").datagrid('setWhere', "A.CustNO='KKK'");
        }
        function CityNOOnSelect(rowData) {
            $("#dataFormMasterCustRegion").combobox('setValue', "");
            $("#dataFormMasterCustRegion").combobox('setWhere', "CityNO=" + rowData.CityNO);
        }
        function CityrRegionOnSelect(rowData) {
            $("#dataFormMasterCustPost").val(rowData.PostCode);
            var tmp = $("#dataFormMasterCustAddr").val();
            if (tmp == "") {
                var City = $("#dataFormMasterCustCityNO").combobox('getText');
                var Region = $("#dataFormMasterCustRegion").combobox('getValue');
                $("#dataFormMasterCustAddr").val(City.trim()+Region);
            }
        }
        function CustDeptOnSelect(rowData) {
            var tmp = $("#dataFormMasterCustDeptName").val();
            $("#dataFormMasterCustDeptName").val(rowData.CustDeptName);
        }
        //檢查客戶代號是否重複
        function CheckCustNO() {
            var CustNO = $("#dataFormMasterCustNO").val();
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                //var valStr = $("#dataFormMasterCustNO").val();
                //var r = valStr.match(/^\d+$/);   //至少要有一碼
                //if (r == null) {
                //    alert('注意!!,客戶代號必須為數字字元,且不可有空白字元')
                //    var valStr = $("#dataFormMasterCustNO").val('');
                //    return false;
                //}
                var cnt;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sERPCustomer.ERPCustomers', //連接的Server端，command
                    data: "mode=method&method=" + "CheckCustNO" + "&parameters=" + CustNO, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != false) {
                            cnt = $.parseJSON(data);
                        }
                    }
                });
                if ((cnt == "0") || (cnt == "undefined")) {
                    return true;
                }
                else {
                   
                    return false;
                }
            }
            else
                return true;
        }
        //檢查付款方式是否已存在
        function CheckPayKind() {
            var CustNO = $("#JQDataForm1CustNO").val();
            var SalesTypeID = $("#JQDataForm1SalesTypeID").combobox('getValue');
            var PayTypeID = $("#JQDataForm1PayTypeID").combobox('getValue');
            if (getEditMode($("#JQDataForm1")) == 'inserted') {
                var cnt;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sERPCustomer.ERPCustomers', //連接的Server端，command
                    data: "mode=method&method=" + "CheckPayKind" + "&parameters=" + CustNO + "," + SalesTypeID + "," + PayTypeID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != false) {
                            cnt = $.parseJSON(data);
                        }
                    }
                });
                if ((cnt == "0") || (cnt == "undefined")) {
                    return true;
                }
                else {
                    alert('注意!!此付款方式已存在');
                    $('#JQDataForm1CustNO').val("");
                    $('#JQDataForm1CustNO').focus;
                    return false;
                }
            }
            else return true;
        }
        function dataFormMasterLoadSucess() {
          
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                $('#dataFormMasterSalesID').combobox('enable');
                $('#dataFormMasterIsPutInvoice').attr('disabled', false);
            }
            else {
                $('#dataFormMasterSalesID').combobox('disable');
                var UserID = getClientInfo("UserID");
                $('#dataFormMasterIsPutInvoice').attr('disabled', true);
                var row = $('#JQDataGrid3').datagrid('getSelected');
                var ViewList = row.CategoryValue;
                var VL = ViewList.indexOf(UserID);
                if (VL >-1) {
                    $('#dataFormMasterIsPutInvoice').attr('disabled', false);
                }
            }
            var row = $('#JQDataGrid2').datagrid('getSelected');
            var ViewList = row.CategoryValue;
            var VL = ViewList.indexOf(UserID);
            if (VL == -1) {
                var FormName = '#dataFormMaster';
                var HideFieldName = ['IsPublicView'];
                $.each(HideFieldName, function (index, fieldName) {
                    $(FormName + fieldName).closest('td').prev('td').hide();
                    $(FormName + fieldName).closest('td').hide();
                });
            }
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                $('#dataFormMasterCustNO').focus();
            }
            else {
                $('#dataFormMasterCustName').focus();
            }
        }
        function queryGrid(dg) {
            var result = [];
            var aVal = '';
            var Filtflag = 0; //查詢條件指標
            if ($(dg).attr('id') == 'dataGridView') {
                aVal = $('#CustNO_Query').combobox('getValue');
                if (aVal != '') {
                    result.push("CustTelNO = '" + aVal + "' OR CustTelNO1 = '" + aVal + "' OR CustNO = '" + aVal + "'");
                    Filtflag = 1
                }
                aVal = $('#CustName_Query').val();
                if (aVal != '') {
                    result.push("CustName like '%" + aVal + "%'");
                    Filtflag = 1;
                }
                aVal = $("input[id='A.SalesID_Query']").combobox('getValue');
                if (aVal != '') {
                    result.push("A.SalesID = '" + aVal + "'");
                    Filtflag = 1;
                }
                aVal = $('#DealDays_Query').val();
                if (aVal != '') {
                    result.push("(dbo.funReturnCustDealDays(A.LatelyDayD)) <= '" + aVal + "'");
                    Filtflag = 1;
                }
                var SalesTypeID = $('#SalesTypeID_Query').combobox('getValue');
                var QSDate = '1900/01/01';
                var QEDate = '1900/01/01';
                if (SalesTypeID != '') {
                    Filtflag = 1;
                    if ($('#QSDate_Query').datebox('getValue') != '') {
                        var QSDate = $.jbjob.Date.DateFormat(new Date($('#QSDate_Query').datebox('getValue')), 'yyyy-MM-dd');
                    }
                    if ($('#QEDate_Query').datebox('getValue') != '') {
                        var QEDate = $.jbjob.Date.DateFormat(new Date($('#QEDate_Query').datebox('getValue')), 'yyyy-MM-dd');
                    }
                    result.push("Custno in (SELECT Custno FROM DBO.funReturnCustBillSalesDate('" + SalesTypeID + "','" + QSDate + "','" + QEDate + "'))");
                }
                //當沒有輸入任何查詢條件時,顯示最近30天有交易的客戶資料
                if (Filtflag == 0) {
                    aVal = 15;
                    result.push("(dbo.funReturnCustDealDays(A.LatelyDayD)) <= '" + aVal + "'");
                }
                //當使用者不是查詢名單時,僅能檢視開放查詢的客戶
                var UserID = getClientInfo("UserID");
                $(dg).datagrid('setWhere', result.join(' and '));
            }
            else {
                var UserID = getClientInfo("UserID");
                aVal = $('#NotesTypeID_Query').options('getValue');
                result.push("NotesTypeID = " + aVal);
                aVal = $('#dataFormMasterCustNO').val();
                result.push("CustNO ='" + aVal+"'");
                $(dg).datagrid('setWhere', result.join(' and '));
            }
         }
        //新增付款方式紀錄
        function dataFormMasterOnApplied() {
            var CustNO = $("#dataFormMasterCustNO").val();
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                $("#CustNO_Query").combobox('setValue', CustNO);
                                $("#CustShortName_Query").val('');
                                $("#SalesID_Query").combobox('setValue', '');
                                $("#DealDays_Query").val('');
                                queryGrid($('#dataGridView'));
                                $('#dataGridView').datagrid('reload');
            }
            if (getEditMode($("#dataFormMaster")) == 'updated') {
                openForm('#JQDialog1', $('#dataGridView').datagrid('getSelected'), "updated", 'dialog');
            }
        }
        //客戶刪除檢查
        function dataGridViewOnDelete(data) {
                var CustNO = data.CustNO;
                var cnt;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sERPCustomer.ERPCustomers', //連接的Server端，command
                    data: "mode=method&method=" + "CheckDelCustNO" + "&parameters=" + CustNO, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != false) {
                            cnt = $.parseJSON(data);
                        }
                    }
                });
                if ((cnt == "0") || (cnt == "undefined")) {
                    return true;
                }
                else {
                    alert('注意!!,此客戶已有銷貨資料,無法刪除！！');
                    return false;
                }
        }
        function JQDataForm1LoadSucess() {
            var row = $("#dataGridView").datagrid('getSelected');
            var CustShortName = row.CustShortName
            $("#JQDataForm1CUSTSHORTNAME").val(CustShortName);
         }
        function OnBlurCustNO() {
            if (CheckCustNO() == false) {
                var cus = $('#dataFormMasterCustNO').val();
                alert("注意!!客戶代號:"+cus+" 已存在");
                $('#dataFormMasterCustNO').val("");
                $('#dataFormMasterCustNO').focus();
            }
            if ($("#dataFormMasterCustTelNO").val() == '') {
                $("#dataFormMasterCustTelNO").val($("#dataFormMasterCustNO").val());
            }
        }
        function dataFormMasterOnApply() {
            var SalesID = $("#dataFormMasterSalesID").combobox('getValue');
            if ((SalesID == '') || (SalesID == 'undefined')) {
                alert('注意!!,未選取業務人員,請選取！！')
                $("#dataFormMasterSalesID").combobox().next('span').find('input').focus();
                return false;
            }
        }
        function QSalesTypeIDOnSelect() {
            //if ($('#SalesTypeID_Query').combobox('getValue') != '') {
            //    var Dt = new Date();
            //    var dd = Dt.getFullYear() + "/" + (Dt.getMonth() + 1) + "/" + Dt.getDate()

            //    if ($('#QSDate_Query').datebox('getValue') != "undefined") {
            //        $('#QSDate_Query').datebox('setValue', dd);
            //    }
            //    if ($('#QEDate_Query').datebox('getValue') != "undefined") {
            //        $('#QEDate_Query').datebox('setValue', dd);
            //    }
            //}
            //else {
            //    $('#QSDate_Query').datebox('setValue', '');
            //    $('#QEDate_Query').datebox('setValue', '');
            //}
        }
        function dataGridViewLoadSucess() {
             //$("#dataGridView").datagrid('setWhere', "(dbo.funReturnCustDealDays(A.LatelyDayD)) <=3");
             if ($("#dataGridView").datagrid('getRows').length == 0) {
                 $("#JQDataGrid1").datagrid('setWhere', '1=2');
             }
        }
        //同步聯絡人 傳入參數:Conb 聯絡人姓名,CustNO 客戶代號,ConType 聯絡人項次 1,2
        function SyncContact(ConType,ConB,CustNO,Center_ID) {
            var flag = false;
            var UserID = getClientInfo("UserID");
            var ConfirmYN = confirm("注意!!確定要同步聯絡人:(" + ConB + ')?');
            if (ConfirmYN == false) {
                return true;
            }
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERPCustomer.ERPCustomers',
                data: "mode=method&method=" + "procSyncContact" + " &parameters=" + ConType + "," + ConB + "," + CustNO + "," + Center_ID+ "," + UserID,
                cache: false,
                async: false,
                success: function (data) {
                    if (data == 'True') {
                        flag = true;
                    }
                    else {
                        alert("注意!! 同步聯絡人失敗");
                    }
                }
            });
            return flag;
        }
        function JQDataForm2OnLoadSucess() {
            $('#JQDataForm2NotesTypeID').next('span').find('input').attr('disabled', true);
            var NotesType = $("#NotesTypeID_Query").options('getValue');
            $("#JQDataForm2NotesTypeID").options('setValue', NotesType);
            $('#JQDataForm2Notes').closest("tr").show();
            $('#JQDataForm2NextCallDate').closest("tr").show();
            $('#JQDataForm2NextCallTime').closest("tr").show();
            $('#JQDataForm2PreNextCallDate').closest("tr").show();
            $('#JQDataForm2PreNextCallTime').closest("tr").show();
            $('#JQDataForm2NotesCreateDate').closest("tr").show();
            if (getEditMode($("#JQDataForm2")) == 'inserted') {
                if (NotesType == 1) {
                    var Dt = new Date();
                    var dd = Dt.getFullYear() + "/" + (Dt.getMonth() + 1) + "/" + Dt.getDate();
                    $('#JQDataForm2NextCallDate').datebox('setValue', dd);
                    $('#JQDataForm2Notes').closest("tr").hide();
                    $('#JQDataForm2NotesCreateDate').closest("tr").hide();
                    $('#JQDataForm2PreNextCallDate').closest("tr").hide();
                    $('#JQDataForm2PreNextCallTime').closest("tr").hide();
                }
                else {
                    $('#JQDataForm2NextCallDate').closest("tr").hide();
                    $('#JQDataForm2NextCallTime').closest("tr").hide();
                }
            }
            else
                 {
                if (NotesType == 1) {
                    var Dt = new Date();
                    var dd = Dt.getFullYear() + "/" + (Dt.getMonth() + 1) + "/" + Dt.getDate();
                    $('#JQDataForm2NotesCreateDate').datebox('setValue', dd);
                    $('#JQDataForm2PreNextCallDate').closest("tr").hide();
                    $('#JQDataForm2PreNextCallTime').closest("tr").hide();
                    $('#JQDataForm2NextCallDate').closest("tr").hide();
                    $('#JQDataForm2NextCallTime').closest("tr").hide();
                }
                else
                {
                    $('#JQDataForm2NextCallDate').closest("tr").hide();
                    $('#JQDataForm2NextCallTime').closest("tr").hide();
                }
            }
            $("#JQDataForm2CustNO").val($("#dataFormMasterCustNO").val());
        }
        function JQDialog3OnSubmited() {
        }
        function NotesTypeOnSelect() {
            $('#JQDataForm2PreNextCallDate').closest("tr").show();
            $('#JQDataForm2PreNextCallTime').closest("tr").show();
            $('#JQDataForm2NextCallDate').closest("tr").show();
            $('#JQDataForm2NotesCreateDate').closest("tr").show();
            $('#JQDataForm2NextCallTime').closest("tr").show();
            $('#JQDataForm2Notes').closest("tr").show();
            $('#JQDataForm2CreateBy').closest("tr").show();
            var NotesType = $("#JQDataForm2NotesTypeID").options('getValue');
        }
        function JQDataForm2OnApply() {
            var NotesType = $("#JQDataForm2NotesTypeID").options('getValue');
            if (NotesType == 1) {
                var nc = $('#JQDataForm2NextCallTime').combobox('getValue');
                if ((nc == '' || nc == 'undefined') && (getEditMode($("#JQDataForm2")) == 'inserted')) {
                    alert('注意!!,請選取時段');
                    $('#JQDataForm2NextCallTime').combobox().next('span').find('input').focus();
                    return false;
                   }
            }
            else {
                var nc = $('#JQDataForm2Notes').val();
                if (nc == '' || nc == 'undefined') {
                    alert('注意!!,請輸入覆訪內容');
                    $('#JQDataForm2Notes').focus();
                    return false;
                }
            }
        }
        function OnSelectSalesID(rowData) {
            $('#dataFormMasterCENTER_ID').val(rowData.CENTER_ID);
        }
        function NotesTypeQueryOnSelect(data) {
            var vdg = '#JQDataGrid4'
            queryGrid(vdg);
        }
        function JQDataForm2OnApplied() {
             if (getEditMode($("#JQDataForm2")) == 'inserted') {
                var NotesType = $("#JQDataForm2NotesTypeID").options('getValue');
                if (NotesType == 2) {
                    if ($('#JQDataForm2PreNextCallDate').datebox('getValue') != '') {
                        var flag = false;
                        var CustNO = $("#dataFormMasterCustNO").val();
                        var PreNextCallDate = $('#JQDataForm2PreNextCallDate').datebox('getValue');
                        var PreNextCallTime = $('#JQDataForm2PreNextCallTime').val();
                        var UserName = getClientInfo("UserName");
                        $.ajax({
                            type: "POST",
                            url: '../handler/jqDataHandle.ashx?RemoteName=sERPCustomer.ERPCustomers',
                            data: "mode=method&method=" + "procAddERPCustomerToDoNotes" + " &parameters=" + CustNO + "*" + PreNextCallDate + "*" + PreNextCallTime + "*" + UserName,
                            cache: false,
                            async: false,
                            success: function (data) {
                                if (data == "True") {
                                    $("#JQDataGrid4").datagrid('reload');
                                    flag = true;
                                }
                                else {
                                    alert("注意!! 覆訪紀錄更新失敗")
                                }
                            }
                        });
                        return flag;
                    }
                }
            }
           
        }
        function JQDataGrid4OnLoadSucess() {
            var NotesType = $("#NotesTypeID_Query").options('getValue');
            if (NotesType == 2) {
                $("#JQDataGrid4").datagrid('hideColumn', 'CreateBy')
                $("#JQDataGrid4").datagrid('hideColumn', 'NextCallDate')
                $("#JQDataGrid4").datagrid('hideColumn', 'NextCallTime')
            }
            else {
                $("#JQDataGrid4").datagrid('showColumn', 'CreateBy')
                $("#JQDataGrid4").datagrid('showColumn', 'NextCallDate')
                $("#JQDataGrid4").datagrid('showColumn', 'NextCallTime')
            }
        }
        function genToolTipNotes(val, rowData) {
            if (rowData.Notes != undefined) {
                return "<p title='" + rowData.Notes + "' style='margin:0px;'>" + val + "</p>";
            }
            else {
                return val;
            }
        }
        
   </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERPCustomer.ERPCustomers" runat="server" AutoApply="True"
                DataMember="ERPCustomers" Pagination="True" QueryTitle="客戶篩選" EditDialogID="JQDialog1"
                Title="客戶維護" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" ParentObjectID="" OnSelect="SetWherePayKind" OnDelete="dataGridViewOnDelete" OnLoadSuccess="dataGridViewLoadSucess" BufferView="False" NotInitGrid="False" RowNumbers="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustNO" Format="" MaxLength="0" Width="80" Frozen="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶簡稱" Editor="text" FieldName="CustShortName" Format="" MaxLength="0" Width="100" Frozen="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人" Editor="text" FieldName="CustJobName" MaxLength="0" Width="110" />
                    <JQTools:JQGridColumn Alignment="left" Caption="分機" Editor="text" FieldName="ContactASubTel" Format="" MaxLength="0" Width="45" />
                    <JQTools:JQGridColumn Alignment="left" Caption="行動電話" Editor="text" FieldName="ContactATel" Format="" MaxLength="0" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="業務" Editor="text" FieldName="SalesName" Width="65" />
                    <JQTools:JQGridColumn Alignment="center" Caption="最近交易日" Editor="datebox" FieldName="LatelyDayD" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70" />
                    <JQTools:JQGridColumn Alignment="center" Caption="交易天數" Editor="text" FieldName="DealDays" MaxLength="0" Width="52" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶備註" Editor="text" FieldName="CustNotes" Format="" MaxLength="0" Width="1020" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="電話號碼" Condition="%%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CustNO',textField:'CustNO',remoteName:'sERPCustomer.CustBill',tableName:'CustBill',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CustNO" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="120" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶名稱" Condition="%%" DataType="string" Editor="text" FieldName="CustName" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="120" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="業務" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sERPCustomer.SalesMan',tableName:'SalesMan',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="A.SalesID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="100" />
                    <JQTools:JQQueryColumn AndOr="" Caption="銷貨類別" Condition="" DataType="string" Editor="infocombobox" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERPCustomer.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:QSalesTypeIDOnSelect,panelHeight:200" FieldName="SalesTypeID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="105" DefaultValue="1" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="交易天數" Condition="&lt;=" DataType="string" DefaultValue="" Editor="text" FieldName="DealDays" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="30" />
                    <JQTools:JQQueryColumn AndOr="" Caption="起始日期" Condition="" DataType="datetime" Editor="datebox" FieldName="QSDate" Format="yyyy-mm-dd" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="87" />
                    <JQTools:JQQueryColumn AndOr="" Caption="終止日期" Condition="" DataType="datetime" Editor="datebox" FieldName="QEDate" Format="yyyy-mm-dd" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="87" />
                </QueryColumns>
            </JQTools:JQDataGrid>
            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="客戶維護" DialogLeft="10px" DialogTop="25px" EnableTheming="False" Width="1020px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="ERPCustomers" HorizontalColumnsCount="5" RemoteName="sERPCustomer.ERPCustomers" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="True" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" OnApplied="dataFormMasterOnApplied" ParentObjectID="dataGridView" OnLoadSuccess="dataFormMasterLoadSucess" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" OnApply="dataFormMasterOnApply" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustNO" maxlength="20" Width="90" OnBlur="OnBlurCustNO" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustName" maxlength="50" Width="180" OnBlur="OnBlurCustName" Span="1" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="簡稱" Editor="text" FieldName="CustShortName" maxlength="30" Width="116" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="雇主" Editor="text" FieldName="BossName" Span="2" Width="115" maxlength="0" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="統一編號" Editor="text" FieldName="TaxNO" Width="90" Span="1" maxlength="0" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="業務人員" Editor="infocombobox" FieldName="SalesID" Width="110" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sERPCustomer.SalesMan',tableName:'SalesMan',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:OnSelectSalesID,panelHeight:200" Span="1" maxlength="0" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="專案人員" Editor="infocombobox" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sERPCustomer.SalesMan',tableName:'SalesMan',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="PMID" Span="1" Width="120" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="信封備註" Editor="infocombobox" EditorOptions="valueField:'LetterTypeID',textField:'LetterTypeName',remoteName:'sERPCustomer.LetterType',tableName:'LetterType',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="LetterType" maxlength="0" Span="2" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電話號碼1" Editor="text" FieldName="CustTelNO" maxlength="20" Span="1" Width="90" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="縣市地址" Editor="infocombobox" EditorOptions="valueField:'CityNO',textField:'CityName',remoteName:'sERPCustomer.City',tableName:'City',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:CityNOOnSelect,panelHeight:200" FieldName="CustCityNO" Span="1" Width="80" MaxLength="0" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" FieldName="CustRegion" MaxLength="0" Span="1" Width="75" EditorOptions="valueField:'Region',textField:'Region',remoteName:'sERPCustomer.Region',tableName:'Region',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:CityrRegionOnSelect,panelHeight:200" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="區號" Editor="text" FieldName="CustPost" MaxLength="0" Span="1" Width="40" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="地址" Editor="text" FieldName="CustAddr" NewRow="False" Span="1" Width="295" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電話號碼2" Editor="text" FieldName="CustTelNO1" MaxLength="0" Span="1" Width="90" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="傳真號碼" Editor="text" FieldName="CustFaxNO" Format="" NewRow="False" Width="90" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="部門" Editor="infocombobox" EditorOptions="valueField:'CustDeptID',textField:'CustDeptName',remoteName:'sERPCustomers.CustDept',tableName:'CustDept',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,onSelect:CustDeptOnSelect,panelHeight:200" FieldName="CustDeptID" Format="" NewRow="False" Span="3" Width="110" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="CustDeptName" Format="" NewRow="False" Width="70" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" EditorOptions="valueField:'CustAreaID',textField:'CustAreaName',remoteName:'sERPCustomers.CustArea',tableName:'CustArea',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CustAreaID" Format="" NewRow="False" Width="150" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人1" Editor="text" FieldName="ContactA" Format="" Width="90" NewRow="True" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="分機" Editor="text" FieldName="ContactASubTel" Format="" Width="40" NewRow="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="行動電話" Editor="text" FieldName="ContactATel" Format="" Width="90" maxlength="0" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="eMail" Editor="text" FieldName="ContactAMail" Format="" Width="120" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電子報" Editor="checkbox" FieldName="ContactAIsMail" Format="" Width="30" NewRow="False" maxlength="0" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" EditorOptions="valueField:'CustJobID',textField:'CustJobName',remoteName:'sERPCustomers.CustJob',tableName:'CustJob',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ContactAJobID" Format="" Width="113" NewRow="False" maxlength="0" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人2" Editor="text" FieldName="ContactB" Format="" Width="90" NewRow="True" ReadOnly="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="分機" Editor="text" FieldName="ContactBSubTel" Format="" Width="40" maxlength="0" Visible="True" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="行動電話" Editor="text" FieldName="ContactBTel" Format="" Width="90" maxlength="0" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="eMail" Editor="text" FieldName="ContactBMail" Format="" Width="120" Visible="True" ReadOnly="False" maxlength="0" NewRow="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電子報" Editor="checkbox" FieldName="ContactBIsMail" Format="" Width="30" maxlength="0" Span="1" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="帳務人員" Editor="text" FieldName="BillDeal" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="eMail" Editor="text" FieldName="BillDealEmail" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="加入LINE" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsAddLine" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶等級" Editor="infocombobox" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sERPCustomer.PostType',tableName:'PostType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="PostType" MaxLength="0" Span="2" Width="122" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" FieldName="ContactBJobID" Format="" Width="113" EditorOptions="valueField:'CustJobID',textField:'CustJobName',remoteName:'sERPCustomers.CustJob',tableName:'CustJob',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" Visible="True" ReadOnly="False" maxlength="0" NewRow="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="結帳日" Editor="text" FieldName="BalanceDay" Format="" Width="90" NewRow="True" Span="1" Visible="True" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="下次複訪日" Editor="datebox" FieldName="RecallDate" Format="" Width="90" Visible="True" ReadOnly="False" maxlength="0" NewRow="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="開立發票" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsPutInvoice" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="報紙發票" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsPutPaperInvoice" ReadOnly="False" Span="2" Visible="True" Width="80" MaxLength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="重要備註" Editor="textarea" FieldName="CustNotes" Format="" Width="760" EditorOptions="height:45" Span="5" Visible="True" ReadOnly="False" MaxLength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="求才備註" Editor="textarea" EditorOptions="height:30" FieldName="DealNotesP" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="5" Visible="True" Width="760" />
                        <JQTools:JQFormColumn Alignment="left" Caption="週報備註" Editor="textarea" EditorOptions="height:30" FieldName="DealNotesW" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="5" Visible="True" Width="760" />
                        <JQTools:JQFormColumn Alignment="left" Caption="報紙備註" Editor="textarea" EditorOptions="height:30" FieldName="DealNotesN" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="5" Visible="True" Width="760" />
                        <JQTools:JQFormColumn Alignment="left" Caption="票匯備註" Editor="textarea" EditorOptions="height:17" FieldName="PayNotes" maxlength="256" NewRow="False" ReadOnly="False" RowSpan="1" Span="5" Visible="True" Width="760" />
                        <JQTools:JQFormColumn Alignment="left" Caption="最近交易日" Editor="datebox" FieldName="LatelyDayD" Format="yyyy/mm/dd" ReadOnly="True" Visible="True" Width="90" maxlength="0" NewRow="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="求才交易日" Editor="datebox" FieldName="LatelyDayP" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="90" Format="yyyy/mm/dd" />
                        <JQTools:JQFormColumn Alignment="left" Caption="週報交易日" Editor="datebox" FieldName="LatelyDayW" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="90" Format="yyyy/mm/dd" />
                        <JQTools:JQFormColumn Alignment="left" Caption="報紙交易日" Editor="datebox" FieldName="LatelyDayN" ReadOnly="True" Span="2" Visible="True" Width="90" Format="yyyy/mm/dd" MaxLength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />     
                        <JQTools:JQFormColumn Alignment="left" Caption="工業區別" Editor="infocombobox" EditorOptions="valueField:'IndustryTypeID',textField:'IndustryTypeName',remoteName:'sERPCustomer.Industry',tableName:'Industry',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="IndustryTypeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="開放查詢" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsPublicView" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="寄電子報" Editor="infocombobox" FieldName="IsAcceptePaper" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" EditorOptions="valueField:'ePaperCode',textField:'ePaperType',remoteName:'sERPCustomer.PaperType',tableName:'PaperType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="不寄原因" Editor="text" FieldName="NotAcceptPaper" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="275" />
                        <JQTools:JQFormColumn Alignment="left" Caption="轉永豐銀行" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsChangeBank" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="媒體產業" Editor="infocombobox" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sERPCustomer.IndustryType',tableName:'IndustryType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="IndustryType" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="行業別" Editor="infocombobox" EditorOptions="valueField:'jb_type',textField:'jb_name',remoteName:'sERPCustomer.Industry',tableName:'Industry',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="IndustryID" MaxLength="0" Span="3" Width="276" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CENTER_ID" Editor="text" FieldName="CENTER_ID" maxlength="0" NewRow="False" ReadOnly="False" Span="1" Width="80" Visible="False" />
                </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="FromServer" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="IsPublicView" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1900/01/01" FieldName="LatelyDayD" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="IsAcceptePaper" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="IsChangeBank" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" FieldName="IsPutInvoice" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="IndustryType" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue=" 0" FieldName="CustDeptID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="A0" FieldName="LetterType" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckMethod="" CheckNull="True" FieldName="CustNO" RemoteMethod="False" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>

            <JQTools:JQDataGrid ID="JQDataGrid1" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="True" CheckOnSelect="True" ColumnsHibeable="False" DataMember="ERPPayKind" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sERPCustomer.ERPPayKind" Title="收款方式" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" EditDialogID="JQDialog2" BufferView="False" NotInitGrid="False" RowNumbers="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="PayKindNO" Editor="text" FieldName="PayKindNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustNO" Frozen="True" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="名稱" Editor="text" FieldName="CUSTSHORTNAME" Frozen="True" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180" />
                    <JQTools:JQGridColumn Alignment="left" Caption="交易別" Editor="infocombobox" FieldName="SalesTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERPCustomer.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="付款代碼" Editor="infocombobox" FieldName="PayTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" EditorOptions="valueField:'PayTypeID',textField:'PayTypeName',remoteName:'sERPCustomer.PayType',tableName:'PayType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="center" Caption="收款結帳日" Editor="text" FieldName="BalanceDay" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="center" Caption="請款日" Editor="text" FieldName="PayDays" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="center" Caption="票期" Editor="text" FieldName="PayLeadDays" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔日期" Editor="text" FieldName="CreateDate" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120" />
                </Columns>
                <TooItems>
                <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                 </TooItems>
            </JQTools:JQDataGrid>
            <div style="display:none;">
            <JQTools:JQDataGrid ID="JQDataGrid2" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" AutoApply="True" CheckOnSelect="True" ColumnsHibeable="False" DataMember="SYSVar" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sERPCustomer.SYSVar" Title="JQDataGrid" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="CategoryValue" Editor="text" FieldName="CategoryValue" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                </Columns>
            </JQTools:JQDataGrid>
            </div>
            <div style="display:none;">
             <JQTools:JQDataGrid ID="JQDataGrid3" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" AutoApply="True" CheckOnSelect="True" ColumnsHibeable="False" DataMember="SYSVar2" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sERPCustomer.SYSVar2" Title="JQDataGrid" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="CategoryValue" Editor="text" FieldName="CategoryValue" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                </Columns>
            </JQTools:JQDataGrid>
            </div>
             <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="JQDataForm1" Title="收款方式維護" Width="720px" DialogLeft="35px" DialogTop="300px">
                <JQTools:JQDataForm ID="JQDataForm1" runat="server" DataMember="ERPPayKind" HorizontalColumnsCount="5" RemoteName="sERPCustomer.ERPPayKind" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" OnApply="CheckPayKind" OnLoadSuccess="JQDataForm1LoadSucess" >
                      <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="96" />
                            <JQTools:JQFormColumn Alignment="left" Caption="名稱" Editor="text" FieldName="CUSTSHORTNAME" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="4" Visible="True" Width="96" />
                            <JQTools:JQFormColumn Alignment="left" Caption="PayKindNO" Editor="text" FieldName="PayKindNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="100" />
                            <JQTools:JQFormColumn Alignment="left" Caption="交易別" Editor="infocombobox" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERPCustomers.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTypeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                            <JQTools:JQFormColumn Alignment="left" Caption="付款代碼" Editor="infocombobox" EditorOptions="valueField:'PayTypeID',textField:'PayTypeName',remoteName:'sERPCustomers.PayType',tableName:'PayType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="PayTypeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                            <JQTools:JQFormColumn Alignment="left" Caption="收款結帳日" Editor="numberbox" FieldName="BalanceDay" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" />
                            <JQTools:JQFormColumn Alignment="left" Caption="請款日" Editor="numberbox" FieldName="PayDays" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" />
                            <JQTools:JQFormColumn Alignment="left" Caption="票期天數" Editor="numberbox" FieldName="PayLeadDays" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" />
                        </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="JQDefault1" runat="server" BindingObjectID="JQDataForm1" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="PayKindNO" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="BalanceDay" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="PayDays" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="PayLeadDays" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="" FieldName="CustNO" RemoteMethod="False" DefaultMethod="GetCustNO" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="JQValidate1" runat="server" BindingObjectID="JQDataForm1" EnableTheming="True">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
            
            <JQTools:JQDialog ID="JQDialog3" runat="server" DialogLeft="10px" DialogTop="70px" Title="連絡事項" Width="1020px" OnSubmited="JQDialog3OnSubmited" Closed="False" ShowModal="True" ShowSubmitDiv="False" Icon="">
                 <JQTools:JQDataGrid ID="JQDataGrid4" runat="server" EditDialogID="JQDialog4" AlwaysClose="True" DataMember="CustNotes" RemoteName="sERPCustomer.CustNotes" AllowAdd="True" AllowDelete="False" AllowUpdate="True" AutoApply="True" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Panel" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnUpdate="" Height="480px" Width="940px" BufferView="False" NotInitGrid="False" RowNumbers="True" OnLoadSuccess="JQDataGrid4OnLoadSucess">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="提出人員" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="覆訪日期" Editor="text" FieldName="NextCallDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="70" Format="yyyy/mm/dd">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="覆訪時段" Editor="text" FieldName="NextCallTime" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="覆訪內容" Editor="text" FieldName="Notes" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="480" FormatScript="genToolTipNotes">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="實際覆訪日" Editor="text" FieldName="NotesCreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="110" Format="yyyy/mm/dd HH:MM:SS">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="覆訪人員" Editor="text" FieldName="UpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="NotesTypeID" Editor="text" FieldName="NotesTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                     </Columns>
                     <TooItems>
                     <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增覆訪維保" />
                     <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                </TooItems>
                     <QueryColumns>
                         <JQTools:JQQueryColumn AndOr="and" Caption="聯絡事項類別" Condition="=" DataType="number" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:0,remoteName:'sERPCustomer.NotesType',tableName:'NotesType',valueField:'LISTID',textField:'LISTCONTENT',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,onSelect:NotesTypeQueryOnSelect,selectOnly:false,items:[]" FieldName="NotesTypeID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" DefaultValue="1" />
                     </QueryColumns>
                </JQTools:JQDataGrid>
            </JQTools:JQDialog>
            <JQTools:JQDialog ID="JQDialog4" runat="server" BindingObjectID="JQDataForm2" Title="連絡事項維護" Width="780px" DialogLeft="45px" DialogTop="120px">
                <JQTools:JQDataForm ID="JQDataForm2" runat="server" DataMember="CustNotes" HorizontalColumnsCount="1" RemoteName="sERPCustomer.CustNotes" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" OnLoadSuccess="JQDataForm2OnLoadSucess" OnApply="JQDataForm2OnApply" OnApplied="JQDataForm2OnApplied" >
                      <Columns>
                          <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                          <JQTools:JQFormColumn Alignment="left" Caption="連絡事項類別" Editor="infooptions" FieldName="NotesTypeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="150" EditorOptions="title:'',panelWidth:0,remoteName:'sERPCustomer.NotesType',tableName:'NotesType',valueField:'LISTID',textField:'LISTCONTENT',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,onSelect:NotesTypeOnSelect,selectOnly:true,items:[]" />
                          <JQTools:JQFormColumn Alignment="left" Caption="實際覆訪日" Editor="datebox" FieldName="NotesCreateDate" Format="yyyy/mm/dd" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="150" OnBlur="" />
                          <JQTools:JQFormColumn Alignment="left" Caption="覆訪內容" Editor="textarea" FieldName="Notes" MaxLength="256" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="620" EditorOptions="height:80" />
                          <JQTools:JQFormColumn Alignment="left" Caption="預計覆訪日" Editor="datebox" FieldName="NextCallDate" Format="yyyy/mm/dd" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="150" />
                          <JQTools:JQFormColumn Alignment="left" Caption="覆訪時段" Editor="infocombobox" EditorOptions="valueField:'NextCallTime',textField:'NextCallTime',remoteName:'sERPCustomer.NextCallTime',tableName:'NextCallTime',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="NextCallTime" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="150" />
                          <JQTools:JQFormColumn Alignment="left" Caption="下次覆訪日" Editor="datebox" FieldName="PreNextCallDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="150" Format="yyyy/mm/dd" />
                          <JQTools:JQFormColumn Alignment="left" Caption="下次覆訪時段" Editor="infocombobox" EditorOptions="valueField:'NextCallTime',textField:'NextCallTime',remoteName:'sERPCustomer.NextCallTime',tableName:'NextCallTime',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="PreNextCallTime" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="150" />
                          <JQTools:JQFormColumn Alignment="left" Caption="覆訪人員" Editor="text" FieldName="CreateBy" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="150" />
                          <JQTools:JQFormColumn Alignment="left" Caption="覆訪日期" Editor="datebox" FieldName="CreateDate" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="150" />
                          <JQTools:JQFormColumn Alignment="left" Caption="CustNO" Editor="text" FieldName="CustNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                      </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="JQDefault2" runat="server" BindingObjectID="JQDataForm2" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERNAME" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="NotesTypeID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="NotesCreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERNAME" FieldName="UpdateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="NextCallDate" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>

                <JQTools:JQValidate ID="JQValidate2" runat="server" BindingObjectID="JQDataForm2" EnableTheming="True">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
        <script type="text/javascript">
            $(":input").css("background", backcolor);
         </script>
    </form>
</body>
</html>
