<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_CustomerView.aspx.cs" Inherits="Template_JQuerySingle1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        var backcolor = "#E8FFE8"
        $(document).ready(function () {

            //------------設定欄位Caption 變顏色------------

            //電子發票確認,本次複訪日 => red
            var flagIDs = ['#dataFormMasterbElecInvoice', '#dataFormMasterNextCallDate'];
            $(flagIDs.toString()).each(function () {
                var captionTd = $(this).closest('td').prev('td');
                captionTd.css({ color: "#FF0000" });
            });
            var flagIDs2 = ['#dataFormMasterCallNote', '#dataFormMasterNewRecallDate'];
            $(flagIDs2.toString()).each(function () {
                var captionTd = $(this);
                captionTd.css("background-color", "#FFCCCC");
            });

            //同步電子發票 1,2,3 => blue
            var flagIDs3 = ['#dataFormMastersamA', '#dataFormMastersamB', '#dataFormMastersamC'];
             $(flagIDs3.toString()).each(function () {
                 var captionTd = $(this);
                 captionTd.css({ color: "#0044BB" });
            });

            //------------將Focus 欄位背景顏色改為黃色------------
            $(function () {
               
                $("input, select, textarea").focus(function () {
                    $(this).css("background-color", "#FFFFDE");
                });
                $("input, select, textarea").blur(function () {
                    $(this).css("background-color", backcolor);
                });
            });

            //------------統一編號+地址------------
            var TaxNO = $('#dataFormMasterTaxNO').closest('td');
            var CustCityNO = $('#dataFormMasterCustCityNO').closest('td').children();
            var CustRegion = $('#dataFormMasterCustRegion').closest('td').children();
            var CustPost = $('#dataFormMasterCustPost').closest('td').children();
            var CustAddr = $('#dataFormMasterCustAddr').closest('td').children();
            TaxNO.append('&nbsp;&nbsp;&nbsp;縣市地址').append(CustCityNO).append('-').append(CustRegion).append('-').append(CustPost).append('&nbsp;地址').append(CustAddr);
            //$('td', CustCityNO.attr('colspan', 7).closest('tr')).slice(4).remove();

            //------------傳真號碼+部門+信封備註+加入LINE------------
            var CustFaxNO = $('#dataFormMasterCustFaxNO').closest('td');
            var CustDeptID = $('#dataFormMasterCustDeptID').closest('td').children();
            var CustDeptName = $('#dataFormMasterCustDeptName').closest('td').children();
            var CustAreaID = $('#dataFormMasterCustAreaID').closest('td').children();
            var LetterType = $('#dataFormMasterLetterType').closest('td').children();
            CustFaxNO.append('&nbsp;&nbsp;&nbsp;部門').append(CustDeptID).append('-').append(CustDeptName).append('&nbsp;&nbsp;&nbsp;區域').append(CustAreaID).append('&nbsp;&nbsp;&nbsp;信封備註').append(LetterType);
                       
            //var PayNotes = $('#dataFormMasterPayNotes').closest('td');
            //PayNotes.append($('<lable>').css({ color: '#8A2BE2' }).html(' 最多可輸256文數字'));

            //------------聯絡人A------------------
            var ContactA = $('#dataFormMasterContactA').closest('td');
            var samA = $('#dataFormMastersamA').closest('td').children();
            var ContactASubTel = $('#dataFormMasterContactASubTel').closest('td').children();
            var ContactAJobID = $('#dataFormMasterContactAJobID').closest('td').children();
            var rContactATel = $('#dataFormMasterContactATel').closest('td').children();
            var ContactAMail = $('#dataFormMasterContactAMail').closest('td').children();
            var ContactAIsMail = $('#dataFormMasterContactAIsMail').closest('td').children();
            ContactA.append('&nbsp;Email').append(ContactAMail).append('發票').append(samA).append('&nbsp;分機').append(ContactASubTel).append('&nbsp;&nbsp;&nbsp;職稱').append(ContactAJobID).append('&nbsp;行動電話').append(rContactATel).append('&nbsp;電子報').append(ContactAIsMail);

            //------------聯絡人B------------------
            var ContactB = $('#dataFormMasterContactB').closest('td');
            var samB = $('#dataFormMastersamB').closest('td').children();
            var ContactBSubTel = $('#dataFormMasterContactBSubTel').closest('td').children();
            var ContactBJobID = $('#dataFormMasterContactBJobID').closest('td').children();
            var rContactBTel = $('#dataFormMasterContactBTel').closest('td').children();
            var ContactBMail = $('#dataFormMasterContactBMail').closest('td').children();
            var ContactBIsMail = $('#dataFormMasterContactBIsMail').closest('td').children();
            ContactB.append('&nbsp;Email').append(ContactBMail).append('發票').append(samB).append('&nbsp;分機').append(ContactBSubTel).append('&nbsp;&nbsp;&nbsp;職稱').append(ContactBJobID).append('&nbsp;行動電話').append(rContactBTel).append('&nbsp;電子報').append(ContactBIsMail);

            //------------帳務人員.....------------------
            var BillDeal = $('#dataFormMasterBillDeal').closest('td');
            var samC = $('#dataFormMastersamC').closest('td').children();
            var BillDealEmail = $('#dataFormMasterBillDealEmail').closest('td').children();
            var PostType = $('#dataFormMasterPostType').closest('td').children();
            var IsAddLine = $('#dataFormMasterIsAddLine').closest('td').children();
            var PMID = $('#dataFormMasterPMID').closest('td').children();
            BillDeal.append('&nbsp;Email').append(BillDealEmail).append('發票').append(samC).append('&nbsp;&nbsp;&nbsp;客戶等級').append(PostType).append('&nbsp;&nbsp;&nbsp;加入LINE').append(IsAddLine).append('&nbsp;&nbsp;&nbsp;專案人員').append(PMID);

            //------------電子發票確認+行業別------------------
            var bElecInvoice = $('#dataFormMasterbElecInvoice').closest('td');
            var ElecInvoiceReason = $('#dataFormMasterElecInvoiceReason').closest('td').children();
            var IndustryID = $('#dataFormMasterIndustryID').closest('td').children();
            var IndustryType = $('#dataFormMasterIndustryType').closest('td').children();
            bElecInvoice.append('不接受原因').append(ElecInvoiceReason).append('&nbsp;&nbsp;&nbsp;行業別').append(IndustryID).append('&nbsp;&nbsp;&nbsp;媒體產業').append(IndustryType);


            //-------------開立發票--------------------------
            var IsPutInvoice = $('#dataFormMasterIsPutInvoice').closest('td');
            var IsPutPaperInvoice = $('#dataFormMasterIsPutPaperInvoice').closest('td').children();
            var IsChangeBank = $('#dataFormMasterIsChangeBank').closest('td').children();
            var BalanceDay = $('#dataFormMasterBalanceDay').closest('td').children();
            var IsAcceptePaper = $('#dataFormMasterIsAcceptePaper').closest('td').children();
            var NotAcceptPaper = $('#dataFormMasterNotAcceptPaper').closest('td').children();
            IsPutInvoice.append('&nbsp;&nbsp;&nbsp;報紙發票').append(IsPutPaperInvoice).append('&nbsp;&nbsp;轉永豐銀行').append(IsChangeBank).append('&nbsp;&nbsp;結帳日').append(BalanceDay).append('&nbsp;&nbsp;&nbsp;寄電子報').append(IsAcceptePaper).append('&nbsp;&nbsp;&nbsp;不寄原因').append(NotAcceptPaper);

            //----------------本次複訪日----------------
            var NextCallDate = $('#dataFormMasterNextCallDate').closest('td');
            var CallNote = $('#dataFormMasterCallNote').closest('td').children();
            var NewRecallDate = $('#dataFormMasterNewRecallDate').closest('td').children();
            NextCallDate.append('&nbsp;本次內容').append(CallNote).append('&nbsp;下次複訪日').append(NewRecallDate);

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
                //var Center_ID = $('#dataFormMasterCENTER_ID').val();
                //if (Center_ID != 35) {
                //    alert('注意!!僅業務人員是媒體人脈群組可同步');
                //    return false;
                //}
                if (SyncContact(ConType,ConA, CustNO,35) == false) {
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
            //$('#dataFormMasterCustNotes').after($('<a>', { href: 'javascript:void(0)' }).on('click', function () {
            //    if ($('#dataFormMasterCustNO').val() == '') {
            //        alert('注意!!,新增模式時,無法維護連絡事項');
            //        return false;
            //    }
            //      var vdg = '#JQDataGrid4'
            //      queryGrid(vdg);
            //      openForm('#JQDialog3', {}, "", 'dialog');
            //    }).linkbutton({ text: '連絡事項' }));
            ////媒體代辦事項傳入客戶代號
            //var parameter = Request.getQueryStringByName("CustID");
            //if (parameter != "") {
            //    $("#CustNO_Query").combobox('setValue', parameter);
            //    queryGrid('#dataGridView');
            //}
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

            //同步聯絡人1
            $("#dataFormMastersamA").click(function () {
                if ($(this).is(":checked") == true) {  //讀取是選中還是非選中，返回true、false                   
                    $("#dataFormMastersamB").checkbox('setValue', false);
                    $("#dataFormMastersamC").checkbox('setValue', false);
                }
            });
            //同步聯絡人2
            $("#dataFormMastersamB").click(function () {
                if ($(this).is(":checked") == true) {  //讀取是選中還是非選中，返回true、false
                    $("#dataFormMastersamA").checkbox('setValue', false);
                    $("#dataFormMastersamC").checkbox('setValue', false);
                }
            });
            //帳務人員
            $("#dataFormMastersamC").click(function () {
                if ($(this).is(":checked") == true) {  //讀取是選中還是非選中，返回true、false
                    $("#dataFormMastersamA").checkbox('setValue', false);
                    $("#dataFormMastersamB").checkbox('setValue', false);
                }
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
        
        function SetWhereGridView(rowindex, rowdata) {
                //if (rowdata != null && rowdata != undefined) {
                //    var CustNO = rowdata.CustNO;
                //    $("#JQDataGrid1").datagrid('setWhere', "A.CustNO='" + CustNO +"'" );
                //}
                //else
                //    $("#JQDataGrid1").datagrid('setWhere', "A.CustNO='KKK'");
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
            //load時,清空 本次內容,下次複訪日,
            $("#dataFormMasterCallNote").val('');
            $('#dataFormMasterNewRecallDate').datebox('setValue', '');

            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                $('#dataFormMasterSalesID').combobox('enable');
                $('#dataFormMasterIsPutInvoice').attr('disabled', false);
            } else {
                $('#dataFormMasterCustNO').attr('disabled', true);

                $('#dataFormMasterSalesID').combobox('disable');
                var UserID = getClientInfo("UserID");
                $('#dataFormMasterIsPutInvoice').attr('disabled', true);
                var row = $('#JQDataGrid3').datagrid('getSelected');
                var ViewList = row.CategoryValue;
                var VL = ViewList.indexOf(UserID);
                if (VL > -1) {
                    $('#dataFormMasterIsPutInvoice').attr('disabled', false);
                }
                //
                var row2 = $('#JQDataGrid6').datagrid('getSelected');
                var ViewList2 = row2.CategoryValue;
                var VL2 = ViewList2.indexOf(UserID);
                if (VL2 > -1) {
                    $('#dataFormMasterSalesID').combobox('enable');
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

            //選取客戶清單 => Setwhere 1.外勞國籍 , 2.維保紀錄 ,4.重要紀錄 , 5.聯絡人資料 , 6.收款方式
            var CustNO = $("#dataFormMasterCustNO").val();

            $("#DGCustNationality").datagrid('setWhere', "CustNO='" + CustNO + "'");//外勞國籍
            $("#JQDataGrid4").datagrid('setWhere', "CustNO='" + CustNO + "'");//維保紀錄
            $("#JQDataGrid1").datagrid('setWhere', "A.CustNO='" + CustNO + "'");//收款方式
            $("#DGImportantRecord").datagrid('setWhere', "CustNO='" + CustNO + "'");//重要紀錄
            $("#DGContactRecord").datagrid('setWhere', "CustNO='" + CustNO + "'");//聯絡人資料

            //聯絡人1,發票勾選觸發=>修改JBADMIN.dbo.ERPPayKind , JBERP.dbo.CustomerSaleType
            $('#dataFormMastersamA').click(function () {               
                var Name = $("#dataFormMasterContactA").val();
                var Email = $("#dataFormMasterContactAMail").val();
                //取消時=> 設定資料清空
                if ($(this).is(":checked") == false) {
                    Email = "";
                    Name = "";
                }
                AddCustomerERPPayKind(Name, Email)
            });

            //聯絡人2,發票勾選觸發=>修改JBADMIN.dbo.ERPPayKind , JBERP.dbo.CustomerSaleType
            $('#dataFormMastersamB').click(function () {
                var Name = $("#dataFormMasterContactB").val();
                var Email = $("#dataFormMasterContactBMail").val();
                //取消時=> 設定資料清空
                if ($(this).is(":checked") == false) {
                    Email = "";
                    Name = "";
                }
                AddCustomerERPPayKind(Name, Email)
            });
                
            //帳務人員,發票勾選觸發=>修改JBADMIN.dbo.ERPPayKind , JBERP.dbo.CustomerSaleType
            $('#dataFormMastersamC').click(function () {
                var Name = $("#dataFormMasterBillDeal").val();
                var Email = $("#dataFormMasterBillDealEmail").val();
                //取消時=> 設定資料清空
                if ($(this).is(":checked") == false) {
                    Email = "";
                    Name = "";
                }
                AddCustomerERPPayKind(Name, Email)
            });
        }
        function queryGrid(dg) {
            var result = [];
            var aVal = '';
            var Filtflag = 0; //查詢條件指標
            if ($(dg).attr('id') == 'dataGridView') {
                aVal = $('#CustNO_Query').combobox('getValue');
                if (aVal != '') {
                    result.push("CustTelNO = '" + aVal + "' OR CustTelNO1 = '" + aVal + "' OR A.CustNO = '" + aVal + "'");
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

        ////修改1,6,31之電子發票Email------------------------------------------------------------------------------    
        function AddCustomerERPPayKind(Name,Email) {
            var row = $("#dataGridView").datagrid('getSelected');
            var ERPCustomerID = row.ERPCustomerID;
            var CustNO = row.CustNO;            
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERPToDoCustomer.infoCustomerToDoNotes', //連接的Server端，command
                data: "mode=method&method=" + "procAddCustomerERPPayKind" + "&parameters=" + encodeURIComponent(CustNO + "," + ERPCustomerID + "," + Name + "," + Email ), //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                cache: false,
                async: false,
                success: function (data) {
                    $('#JQDataGrid1').datagrid('reload');
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });
        }
        // 修改複訪內容 & 新增下次複訪提醒
        function AddCustomerToDoNotesData() {
            var CustNO = $('#dataFormMasterCustNO').val();
            var NextCallDate = $('#dataFormMasterNextCallDate').datebox('getValue'); //本次複訪日期
            var CallNote = $('#dataFormMasterCallNote').val();//本次內容
            if (CallNote != "" && NextCallDate == "") {//本次複訪日期(帶今天)
                var dt = new Date();                
                NextCallDate = $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd');               
            }
            var NewNextCallDate = $('#dataFormMasterNewRecallDate').datebox('getValue'); //下次複訪日期
            var UserName = getClientInfo("UserName");

            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERPToDoCustomer.infoCustomerToDoNotes', //連接的Server端，command
                data: "mode=method&method=" + "AddCustomerToDoNotes" + "&parameters=" + encodeURIComponent(CustNO + "," + NextCallDate + "," + CallNote + "," + NewNextCallDate + "," + UserName), //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                cache: false,
                async: false,
                success: function (data) {
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });

        }
        // 新增客戶資料後 & 新增下次複訪提醒
        function AddCustomerToDoNotesData2() {
            var CustNO = $('#dataFormMasterCustNO').val();
            var NextCallDate = $('#dataFormMasterNextCallDate').datebox('getValue'); //本次複訪日期
            var CallNote = $('#dataFormMasterCallNote').val();//本次內容
            if (CallNote != "" && NextCallDate == "") {//本次複訪日期(帶今天)
                var dt = new Date();
                NextCallDate = $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd');
            }
            var NewNextCallDate = $('#dataFormMasterNewRecallDate').datebox('getValue'); //下次複訪日期
            var UserName = getClientInfo("UserName");

            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERPToDoCustomer.infoCustomerToDoNotes', //連接的Server端，command
                data: "mode=method&method=" + "AddCustomerToDoNotes2" + "&parameters=" + encodeURIComponent(CustNO + "," + NextCallDate + "," + CallNote + "," + NewNextCallDate + "," + UserName), //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                cache: false,
                async: false,
                success: function (data) {
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });

        }
        //新增客戶資料後
        function dataFormMasterOnApplied() {
            var CustNO = $("#dataFormMasterCustNO").val();
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                $("#CustNO_Query").combobox('setValue', CustNO);
                                $("#CustShortName_Query").val('');
                                $("#SalesID_Query").combobox('setValue', '');
                                $("#DealDays_Query").val('');
                                //queryGrid($('#dataGridView'));
                                //$('#dataGridView').datagrid('reload');
            }
            
            //修改複訪內容 & 新增下次複訪提醒----------------------------------------------------------------------
            var CallNote = $('#dataFormMasterCallNote').val();//本次內容
            var NewNextCallDate = $('#dataFormMasterNewRecallDate').datebox('getValue'); //下次複訪日期
            if (CallNote != "" || NewNextCallDate != "") {

                if (getEditMode($("#dataFormMaster")) == 'updated') {
                    AddCustomerToDoNotesData();
                } else {
                    AddCustomerToDoNotesData2();
                }
               
            }

            

            //再打開一次網頁---------------------------------------------------------------------------------------
            if (getEditMode($("#dataFormMaster")) == 'updated') {
                openForm('#JQDialog1', $('#dataGridView').datagrid('getSelected'), "updated", 'dialog');

            }
            //-----------------------------------------------------------------------------------------------------
            //1.更新維保紀錄
            //2.重整複訪客戶清單          
            $('#JQDataGrid4').datagrid('reload');
            $('#dataGridView').datagrid('reload');
            //-----------------------------------------------------------------------------------------------------            
            ////拼湊重要備註 => 本次複訪日+ 本次內容                
            //if ($('#dataFormMasterCallNote').val() != '' && $('#dataFormMasterNextCallDate').datebox('getValue') != '') {
            //    var note = $('#dataFormMasterCallNote').val().trim();
            //    note = note + ' ' + $('#dataFormMaster2CustNotes').val().trim();
            //    $('#dataFormMaster2CustNotes').val(note);
            //}

            //本次複訪日修改 =>下次複訪日有新增的話---------------------------------------------------------------  
            if ($('#dataFormMasterNewRecallDate').datebox('getValue') != '') {
                $('#dataFormMasterNextCallDate').val($('#dataFormMasterNewRecallDate').datebox('getValue').toString());
            }
            //清空本次複訪日           
            $('#dataFormMasterNextCallDate').datebox('setValue', '');
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
            if (getEditMode($("#JQDataForm2")) == 'inserted') {
                $('#JQDataForm2NotesCreateDate').closest("tr").hide();
            } else {
                $('#JQDataForm2NotesCreateDate').closest("tr").show();
            }
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
            //var NotesType = $("#JQDataForm2NotesTypeID").options('getValue');
            //if (NotesType == 1) {
            //    var nc = $('#JQDataForm2NextCallTime').combobox('getValue');
            //    if ((nc == '' || nc == 'undefined') && (getEditMode($("#JQDataForm2")) == 'inserted')) {
            //        alert('注意!!,請選取時段');
            //        $('#JQDataForm2NextCallTime').combobox().next('span').find('input').focus();
            //        return false;
            //       }
            //}
            //else {
            //    var nc = $('#JQDataForm2Notes').val();
            //    if (nc == '' || nc == 'undefined') {
            //        alert('注意!!,請輸入覆訪內容');
            //        $('#JQDataForm2Notes').focus();
            //        return false;
            //    }
            //}
        }
        function OnSelectSalesID(rowData) {
            $('#dataFormMasterCENTER_ID').val(rowData.CENTER_ID);
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
       
        function genToolTipNotes(val, rowData) {
            if (rowData.Notes != undefined) {
                return "<p title='" + rowData.Notes + "' style='margin:0px;'>" + val + "</p>";
            }
            else {
                return val;
            }
        }
        
        //新增收款方式後執行
        function ERPPayKindOnInserted() {
            //至銷貨客戶新增收款方式
            AddJBERPCustomerSaleType();
            $('#JQDataGrid1').datagrid('reload');
        }

        //至銷貨客戶新增收款方式
        function AddJBERPCustomerSaleType() {
            var CustNO = $('#dataFormMasterCustNO').val();         
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERPCustomer.ERPCustomers', //連接的Server端，command
                data: "mode=method&method=" + "procAddJBERPCustomerSaleType" + "&parameters=" + encodeURIComponent(CustNO), //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                cache: false,
                async: false,
                success: function (data) {
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });

        }

        //修改收款方式後執行
        function ERPPayKindOnUpdated() {
            //至銷貨客戶新增收款方式
            UpdateJBERPCustomerSaleType();
            $('#JQDataGrid1').datagrid('reload');
        }
        //至銷貨客戶修改收款方式
        function UpdateJBERPCustomerSaleType() {
            var CustNO = $('#dataFormMasterCustNO').val();
            var row = $("#JQDataGrid1").datagrid('getSelected');
            var PayKindNO = row.PayKindNO;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERPCustomer.ERPCustomers', //連接的Server端，command
                data: "mode=method&method=" + "procUpdateJBERPCustomerSaleType" + "&parameters=" + CustNO + "," + PayKindNO,
                cache: false,
                async: false,
                success: function (data) {
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });
        }
       


   </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERPCustomer.ERPCustomers" runat="server" AutoApply="True"
                DataMember="ERPCustomers" Pagination="True" QueryTitle="客戶篩選" EditDialogID="JQDialog1"
                Title="客戶維護" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True" ParentObjectID="" OnSelect="SetWhereGridView" OnDelete="dataGridViewOnDelete" OnLoadSuccess="dataGridViewLoadSucess" BufferView="False" NotInitGrid="False" RowNumbers="True">
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
             <div style="display:none;">
             <JQTools:JQDataGrid ID="JQDataGrid6" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" AutoApply="True" CheckOnSelect="True" ColumnsHibeable="False" DataMember="SYSVar3" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sERPCustomer.SYSVar3" Title="JQDataGrid" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="CategoryValue" Editor="text" FieldName="CategoryValue" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                </Columns>
            </JQTools:JQDataGrid>
            </div>
            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="客戶維護" DialogLeft="10px" DialogTop="10px" EnableTheming="False" Width="1020px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="ERPCustomers" HorizontalColumnsCount="7" RemoteName="sERPToDoCustomer.ERPCustomers" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="True" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" OnApplied="dataFormMasterOnApplied" ParentObjectID="dataGridView" OnLoadSuccess="dataFormMasterLoadSucess" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" OnApply="dataFormMasterOnApply" ChainDataFormID="dataFormMaster2" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustNO" maxlength="20" Width="90" OnBlur="OnBlurCustNO" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustName" maxlength="50" Width="180" OnBlur="OnBlurCustName" Span="1" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="簡稱" Editor="text" FieldName="CustShortName" maxlength="30" Width="116" Span="1" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="雇主" Editor="text" FieldName="BossName" Span="1" Width="115" maxlength="0" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="業務人員" Editor="infocombobox" FieldName="SalesID" Width="90" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sERPCustomer.SalesMan',tableName:'SalesMan',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:OnSelectSalesID,panelHeight:200" Span="2" maxlength="0" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電話號碼1" Editor="text" FieldName="CustTelNO" maxlength="20" Span="1" Width="90" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="統一編號" Editor="text" FieldName="TaxNO" maxlength="0" NewRow="False" Span="6" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" FieldName="CustCityNO" NewRow="False" Span="1" Width="80" EditorOptions="valueField:'CityNO',textField:'CityName',remoteName:'sERPCustomer.City',tableName:'City',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:CityNOOnSelect,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" EditorOptions="valueField:'Region',textField:'Region',remoteName:'sERPCustomer.Region',tableName:'Region',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:CityrRegionOnSelect,panelHeight:200" FieldName="CustRegion" NewRow="False" Span="1" Width="75" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="CustPost" NewRow="False" Span="1" Width="40" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="CustAddr" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="295" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電話號碼2" Editor="text" FieldName="CustTelNO1" MaxLength="0" NewRow="True" Span="1" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="傳真號碼" Editor="text" FieldName="CustFaxNO" MaxLength="0" NewRow="False" Span="6" Width="90" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" FieldName="CustDeptID" Format="" maxlength="0" NewRow="False" Width="110" EditorOptions="valueField:'CustDeptID',textField:'CustDeptName',remoteName:'sERPCustomer.CustDept',tableName:'CustDept',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,onSelect:CustDeptOnSelect,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="CustDeptName" Format="" maxlength="0" NewRow="False" Width="70" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" FieldName="CustAreaID" Format="" maxlength="0" NewRow="False" Width="90" EditorOptions="valueField:'CustAreaID',textField:'CustAreaName',remoteName:'sERPCustomer.CustArea',tableName:'CustArea',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsAddLine" maxlength="0" NewRow="True" Span="1" Width="30" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" EditorOptions="valueField:'LetterTypeID',textField:'LetterTypeName',remoteName:'sERPCustomer.LetterType',tableName:'LetterType',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="LetterType" maxlength="0" Span="1" Width="140" NewRow="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人1" Editor="text" FieldName="ContactA" maxlength="0" Span="7" Width="90" NewRow="False" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContactASubTel" NewRow="False" Span="1" Width="33" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="checkbox" FieldName="samA" NewRow="False" Width="20" OnBlur="" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" FieldName="ContactAJobID" Format="" Width="83" NewRow="False" EditorOptions="valueField:'CustJobID',textField:'CustJobName',remoteName:'sERPCustomer.CustJob',tableName:'CustJob',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContactATel" Format="" Width="90" NewRow="False" maxlength="0" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContactAMail" Format="" Width="190" NewRow="False" Span="1" ReadOnly="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="checkbox" FieldName="ContactAIsMail" Format="" Width="30" NewRow="False" Span="1" Visible="True" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人2 " Editor="text" FieldName="ContactB" Width="90" Span="7" NewRow="True" Visible="True" Format="" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContactBMail" Width="190" NewRow="False" Span="1" Format="" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="checkbox" FieldName="samB" Width="20" maxlength="0" ReadOnly="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContactBSubTel" maxlength="0" Width="33" Format="" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" FieldName="ContactBJobID" Format="" Width="83" Span="1" EditorOptions="valueField:'CustJobID',textField:'CustJobName',remoteName:'sERPCustomer.CustJob',tableName:'CustJob',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" NewRow="False" ReadOnly="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContactBTel" Format="" Width="90" maxlength="0" Span="1" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="checkbox" FieldName="ContactBIsMail" Format="" Width="30" NewRow="False" Span="1" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="帳務人員" Editor="text" FieldName="BillDeal" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="7" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="BillDealEmail" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="190" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="checkbox" FieldName="samC" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="20" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sERPCustomer.PostType',tableName:'PostType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="PostType" MaxLength="0" Span="1" Width="110" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" FieldName="PMID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sERPCustomer.SalesMan',tableName:'SalesMan',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電子發票詢問" Editor="checkbox" FieldName="bElecInvoice" Width="30" maxlength="0" Span="7" NewRow="True" ReadOnly="False" RowSpan="1" Visible="True" EditorOptions="on:1,off:0" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ElecInvoiceReason" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="250" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" FieldName="IndustryID" Width="280" NewRow="False" Span="1" Visible="True" ReadOnly="False" EditorOptions="valueField:'jb_type',textField:'jb_name',remoteName:'sERPCustomer.Industry',tableName:'Industry',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" MaxLength="0" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="開立發票" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsPutInvoice" MaxLength="0" NewRow="True" ReadOnly="True" RowSpan="1" Span="7" Visible="True" Width="30" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsPutPaperInvoice" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="30" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsChangeBank" MaxLength="0" NewRow="False" Span="1" Width="30" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="BalanceDay" MaxLength="0" NewRow="True" Span="1" Width="35" Format="" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" FieldName="IsAcceptePaper" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" EditorOptions="valueField:'ePaperCode',textField:'ePaperType',remoteName:'sERPCustomer.PaperType',tableName:'PaperType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="NotAcceptPaper" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="220" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sERPCustomer.IndustryType',tableName:'IndustryType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="IndustryType" MaxLength="0" NewRow="False" ReadOnly="False" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="本次複訪日" Editor="datebox" FieldName="NextCallDate" Format="" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="7" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="CallNote" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="520" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="datebox" FieldName="NewRecallDate" Format="" maxlength="0" ReadOnly="False" Span="1" Visible="True" Width="90" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="開放查詢" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsPublicView" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" EditorOptions="valueField:'IndustryTypeID',textField:'IndustryTypeName',remoteName:'sERPCustomer.Industry',tableName:'Industry',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="IndustryTypeID" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />     
                        <JQTools:JQFormColumn Alignment="left" Caption="ERPCustomerID" Editor="text" FieldName="ERPCustomerID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
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
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="NextCallDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="LastUpdateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="LastUpdateDate" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                </JQTools:JQValidate>
                <div class="easyui-tabs" style="width:960px;height100%" id="tabs1">
                    <div title="外勞資料" style="padding:10px">
                        <JQTools:JQDataForm ID="dataFormMaster3" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="ERPCustomers" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="6" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="True" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="dataGridView" RemoteName="sERPToDoCustomer.ERPCustomers" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                            <Columns>
                                <JQTools:JQFormColumn Alignment="left" Caption="票匯備註" Editor="textarea" EditorOptions="height:17" FieldName="PayNotes" maxlength="256" NewRow="True" ReadOnly="False" RowSpan="1" Span="6" Visible="True" Width="840" />
                                <JQTools:JQFormColumn Alignment="left" Caption="仲介公司" Editor="text" FieldName="ForeignCompany" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="130" />
                                <JQTools:JQFormColumn Alignment="left" Caption="本勞人數" Editor="numberbox" FieldName="iPeopleCount" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="40" />
                                <JQTools:JQFormColumn Alignment="left" Caption="外勞人數" Editor="numberbox" FieldName="iPeopleFCount" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="40" />
                                <JQTools:JQFormColumn Alignment="left" Caption="外勞宿舍" Editor="infocombobox" EditorOptions="valueField:'ForeignDorm',textField:'ForeignDorm',remoteName:'sERPToDoCustomer.infoForeignDorm',tableName:'infoForeignDorm',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ForeignDorm" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="110" />
                                <JQTools:JQFormColumn Alignment="left" Caption="辦事處" Editor="infocombobox" EditorOptions="valueField:'OfficeNo',textField:'OfficeName',remoteName:'sERPToDoCustomer.infoERPOffice',tableName:'infoERPOffice',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="OfficeNo" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="95" />
                                <JQTools:JQFormColumn Alignment="left" Caption="工作天數" Editor="infocombobox" EditorOptions="valueField:'JobWeekendNo',textField:'JobWeekendName',remoteName:'sERPToDoCustomer.infoERPJobWeekend',tableName:'infoERPJobWeekend',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="JobWeekendNo" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="185" />

                            </Columns>
                            <RelationColumns>
                                <JQTools:JQRelationColumn FieldName="CustNO" ParentFieldName="CustNO" />
                            </RelationColumns>
                        </JQTools:JQDataForm>
                        <JQTools:JQDataGrid ID="DGCustNationality" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="infoERPCustNationality" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sERPToDoCustomer.infoERPCustNationality" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="595px" ParentObjectID="">
                            <Columns>
                                <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="70" />
                                <JQTools:JQGridColumn Alignment="center" Caption="國籍" Editor="infocombobox" EditorOptions="valueField:'NationalityNo',textField:'NationalityName',remoteName:'sERPToDoCustomer.infoERPNationality',tableName:'infoERPNationality',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:150" FieldName="NationalityNo" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="150">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="女" Editor="numberbox" FieldName="Female" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                                <JQTools:JQGridColumn Alignment="center" Caption="男" Editor="numberbox" FieldName="Male" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                                <JQTools:JQGridColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CreateBy" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="70" Frozen="False">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="建檔日期" Editor="text" FieldName="CreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="120" Format="yyyy/mm/dd HH:MM:SS" />
                                <JQTools:JQGridColumn Alignment="left" Caption="UpdateBy" Editor="text" FieldName="UpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                <JQTools:JQGridColumn Alignment="left" Caption="UpateDate" Editor="text" FieldName="UpateDate" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                            </Columns>
                            <TooItems>
                                <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增國籍" />
                                <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="存檔" Enabled="True" Visible="True" />
                                <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" Enabled="True" Visible="True"  />
                            </TooItems>
                        </JQTools:JQDataGrid>
                        <JQTools:JQDefault ID="defaultCustNationality" runat="server" BindingObjectID="DGCustNationality" EnableTheming="True">
                            <Columns>
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetCustNO" FieldName="CustNO" RemoteMethod="False" />
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="UpdateBy" RemoteMethod="True" />
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="UpateDate" RemoteMethod="True" />
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                            </Columns>
                        </JQTools:JQDefault>
                        <JQTools:JQValidate ID="validateCustNationality" runat="server" BindingObjectID="DGCustNationality" EnableTheming="True">
                            <Columns>
                                <JQTools:JQValidateColumn CheckNull="True" FieldName="NationalityNo" RemoteMethod="True" ValidateMessage="請選擇國籍" ValidateType="None" />
                            </Columns>
                        </JQTools:JQValidate>
                    </div>
                    <div title="交易狀況" style="padding:10px">
                        <JQTools:JQDataForm ID="dataFormMaster2" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="ERPCustomers" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="4" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="True" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="dataGridView" RemoteName="sERPToDoCustomer.ERPCustomers" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" ChainDataFormID="dataFormMaster3">
                            <Columns>
                                <JQTools:JQFormColumn Alignment="left" Caption="重要備註" Editor="textarea" EditorOptions="height:45" FieldName="CustNotes" Format="" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="840" />
                                <JQTools:JQFormColumn Alignment="left" Caption="求才備註" Editor="textarea" EditorOptions="height:30" FieldName="DealNotesP" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="840" />
                                <JQTools:JQFormColumn Alignment="left" Caption="週報備註" Editor="textarea" EditorOptions="height:30" FieldName="DealNotesW" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="840" />
                                <JQTools:JQFormColumn Alignment="left" Caption="報紙備註" Editor="textarea" EditorOptions="height:30" FieldName="DealNotesN" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="840" />
                                <JQTools:JQFormColumn Alignment="left" Caption="最近交易日" Editor="datebox" FieldName="LatelyDayD" Format="yyyy/mm/dd" maxlength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="90" />
                                <JQTools:JQFormColumn Alignment="left" Caption="求才交易日" Editor="datebox" FieldName="LatelyDayP" Format="yyyy/mm/dd" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="90" />
                                <JQTools:JQFormColumn Alignment="left" Caption="週報交易日" Editor="datebox" FieldName="LatelyDayW" Format="yyyy/mm/dd" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="90" />
                                <JQTools:JQFormColumn Alignment="left" Caption="報紙交易日" Editor="datebox" FieldName="LatelyDayN" Format="yyyy/mm/dd" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="90" />
                            </Columns>
                            <RelationColumns>
                                <JQTools:JQRelationColumn FieldName="CustNO" ParentFieldName="CustNO" />
                            </RelationColumns>
                        </JQTools:JQDataForm>
                    </div>
                    <div title="維保紀錄" style="padding:10px">
                         <JQTools:JQDataGrid ID="JQDataGrid4" runat="server" EditDialogID="JQDialog4" AlwaysClose="True" DataMember="CustNotes" RemoteName="sERPCustomer.CustNotes" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AutoApply="True" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="5,10,20,30,40,50" PageSize="5" Pagination="True" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Panel" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" OnUpdate="" Width="940px" BufferView="False" NotInitGrid="False" RowNumbers="True">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="提出人員" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="實際覆訪日" Editor="text" FieldName="NotesCreateDate" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="110">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="預計覆訪日" Editor="text" FieldName="NextCallDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="70" Format="yyyy/mm/dd">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="預計時段" Editor="text" FieldName="NextCallTime" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="實際覆訪內容" Editor="text" FieldName="Notes" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="480" FormatScript="genToolTipNotes">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="實際覆訪人" Editor="text" FieldName="UpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="65">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="NotesTypeID" Editor="text" FieldName="NotesTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                     </Columns>
                     <TooItems>
<%--                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增維保紀錄" />--%>
                        <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                     </TooItems>
                </JQTools:JQDataGrid>
                            <JQTools:JQDialog ID="JQDialog4" runat="server" BindingObjectID="JQDataForm2" Title="維保紀錄維護" Width="780px" DialogLeft="55px" DialogTop="120px">
                                <JQTools:JQDataForm ID="JQDataForm2" runat="server" DataMember="CustNotes" HorizontalColumnsCount="1" RemoteName="sERPCustomer.CustNotes" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" OnLoadSuccess="JQDataForm2OnLoadSucess" OnApply="JQDataForm2OnApply" OnApplied="JQDataForm2OnApplied" >
                                      <Columns>
                                          <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                          <JQTools:JQFormColumn Alignment="left" Caption="連絡事項類別" Editor="infooptions" FieldName="NotesTypeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="150" EditorOptions="title:'',panelWidth:0,remoteName:'sERPCustomer.NotesType',tableName:'NotesType',valueField:'LISTID',textField:'LISTCONTENT',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,onSelect:NotesTypeOnSelect,selectOnly:true,items:[]" />
                                          <JQTools:JQFormColumn Alignment="left" Caption="提出人員" Editor="text" FieldName="CreateBy" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="130" />
                                          <JQTools:JQFormColumn Alignment="left" Caption="預計覆訪日" Editor="datebox" FieldName="NextCallDate" Format="yyyy/mm/dd" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="150" />
                                          <JQTools:JQFormColumn Alignment="left" Caption="預計時段" Editor="infocombobox" EditorOptions="valueField:'NextCallTime',textField:'NextCallTime',remoteName:'sERPCustomer.NextCallTime',tableName:'NextCallTime',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="NextCallTime" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="150" />
                                          <JQTools:JQFormColumn Alignment="left" Caption="實際覆訪內容" Editor="textarea" FieldName="Notes" MaxLength="256" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="620" EditorOptions="height:80" />
                                          <JQTools:JQFormColumn Alignment="left" Caption="實際覆訪日" Editor="datebox" FieldName="NotesCreateDate" Format="yyyy/mm/dd" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="150" OnBlur="" />
                                          <JQTools:JQFormColumn Alignment="left" Caption="下次覆訪日" Editor="datebox" FieldName="PreNextCallDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="150" Format="yyyy/mm/dd" />
                                          <JQTools:JQFormColumn Alignment="left" Caption="下次覆訪時段" Editor="infocombobox" EditorOptions="valueField:'NextCallTime',textField:'NextCallTime',remoteName:'sERPCustomer.NextCallTime',tableName:'NextCallTime',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="PreNextCallTime" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="150" />
                                          <JQTools:JQFormColumn Alignment="left" Caption="覆訪日期" Editor="datebox" FieldName="CreateDate" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="150" />
                                          <JQTools:JQFormColumn Alignment="left" Caption="CustNO" Editor="text" FieldName="CustNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                          <JQTools:JQFormColumn Alignment="left" Caption="實際覆訪人" Editor="text" FieldName="UpdateBy" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="130" />
                                      </Columns>
                                </JQTools:JQDataForm>
                                <JQTools:JQDefault ID="JQDefault2" runat="server" BindingObjectID="JQDataForm2" EnableTheming="True">
                                    <Columns>
                                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERNAME" FieldName="CreateBy" RemoteMethod="True" />
                                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="NotesTypeID" RemoteMethod="True" />
                                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="NextCallDate" RemoteMethod="True" />
                                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetCustNO" FieldName="CustNO" RemoteMethod="False" />
                                    </Columns>
                                </JQTools:JQDefault>

                                <JQTools:JQValidate ID="JQValidate2" runat="server" BindingObjectID="JQDataForm2" EnableTheming="True">
                                </JQTools:JQValidate>
                            </JQTools:JQDialog>
                    </div>
                    <div title="重要紀錄" style="padding:10px">
                        <JQTools:JQDataGrid ID="DGImportantRecord" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="infoImportantRecord" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" ParentObjectID="" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sERPToDoCustomer.infoImportantRecord" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="100%">
                            <Columns>
                                <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="70" />
                                <JQTools:JQGridColumn Alignment="left" Caption="紀錄內容" Editor="textarea" EditorOptions="height:40" FieldName="Description" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="650">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="建檔人員" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="70">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="建檔日期" Editor="text" FieldName="CreateDate" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="120" />
                                <JQTools:JQGridColumn Alignment="left" Caption="UpdateBy" Editor="text" FieldName="UpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                <JQTools:JQGridColumn Alignment="left" Caption="UpateDate" Editor="text" FieldName="UpateDate" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                            </Columns>
                            <TooItems>
                                <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增紀錄" />
                                <JQTools:JQToolItem Enabled="True" Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="存檔" Visible="True" />
                                <JQTools:JQToolItem Enabled="True" Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" Visible="True" />
                            </TooItems>
                        </JQTools:JQDataGrid>
                        <JQTools:JQDefault ID="defaultImportantRecord" runat="server" BindingObjectID="DGImportantRecord" EnableTheming="True">
                            <Columns>
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetCustNO" FieldName="CustNO" RemoteMethod="False" />
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="UpdateBy" RemoteMethod="True" />
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="UpateDate" RemoteMethod="True" />
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                            </Columns>
                        </JQTools:JQDefault>
                        <JQTools:JQValidate ID="validateImportantRecord" runat="server" BindingObjectID="DGImportantRecord" EnableTheming="True">
                            <Columns>
                                <JQTools:JQValidateColumn CheckNull="True" FieldName="Description" RemoteMethod="True" ValidateMessage="請填寫內容" ValidateType="None" />
                            </Columns>
                        </JQTools:JQValidate>
                    </div>
                    <div title="聯絡人資料" style="padding:10px">
                        <JQTools:JQDataGrid ID="DGContactRecord" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="infoContactRecord" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" ParentObjectID="" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sERPToDoCustomer.infoContactRecord" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="99%">
                            <Columns>
                                <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="70" />
                                <JQTools:JQGridColumn Alignment="left" Caption="姓名" Editor="text" EditorOptions="" FieldName="ContName" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70" Frozen="False">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="職稱" Editor="text" FieldName="ContJobName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="分機" Editor="text" FieldName="ContSubTel" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="Mail" Editor="text" FieldName="ContEmail" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="280">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="手機" Editor="text" FieldName="ContPhone" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="建檔人員" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="70">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="建檔日期" Editor="text" FieldName="CreateDate" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="120" />
                                <JQTools:JQGridColumn Alignment="left" Caption="UpdateBy" Editor="text" FieldName="UpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                <JQTools:JQGridColumn Alignment="left" Caption="UpateDate" Editor="text" FieldName="UpateDate" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                            </Columns>
                            <TooItems>
                                <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增聯絡人" />
                                <JQTools:JQToolItem Enabled="True" Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="存檔" Visible="True" />
                                <JQTools:JQToolItem Enabled="True" Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" Visible="True" />
                            </TooItems>
                        </JQTools:JQDataGrid>
                        <JQTools:JQDefault ID="defaultContactRecord" runat="server" BindingObjectID="DGContactRecord" EnableTheming="True">
                            <Columns>
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetCustNO" FieldName="CustNO" RemoteMethod="False" />
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="UpdateBy" RemoteMethod="True" />
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="UpateDate" RemoteMethod="True" />
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                            </Columns>
                        </JQTools:JQDefault>
                        <JQTools:JQValidate ID="validateContactRecord" runat="server" BindingObjectID="DGContactRecord" EnableTheming="True">
                            <Columns>
                                <JQTools:JQValidateColumn CheckNull="True" FieldName="ContName" RemoteMethod="True" ValidateMessage="請填寫姓名" ValidateType="None" />
                            </Columns>
                        </JQTools:JQValidate>
                    </div>
                    <div title="收款方式" style="padding:10px">
                        <JQTools:JQDataGrid ID="JQDataGrid1" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="ERPPayKind" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sERPCustomer.ERPPayKind" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="100%" OnInserted="ERPPayKindOnInserted" OnUpdated="ERPPayKindOnUpdated">
                            <Columns>
                                <JQTools:JQGridColumn Alignment="left" Caption="PayKindNO" Editor="text" FieldName="PayKindNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustNO" Frozen="True" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="70" />
                                <JQTools:JQGridColumn Alignment="left" Caption="名稱" Editor="text" FieldName="CUSTSHORTNAME" Frozen="True" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="160" />
                                <JQTools:JQGridColumn Alignment="left" Caption="交易別" Editor="infocombobox" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERPCustomer.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100" />
                                <JQTools:JQGridColumn Alignment="left" Caption="付款代碼" Editor="infocombobox" EditorOptions="valueField:'PayTypeID',textField:'PayTypeName',remoteName:'sERPCustomer.PayType',tableName:'PayType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="PayTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                                <JQTools:JQGridColumn Alignment="center" Caption="收款結帳日" Editor="text" FieldName="BalanceDay" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="請款日" Editor="text" FieldName="PayDays" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" />
                                <JQTools:JQGridColumn Alignment="center" Caption="票期" Editor="text" FieldName="PayLeadDays" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" />
                                <JQTools:JQGridColumn Alignment="left" Caption="發票姓名" Editor="text" FieldName="ElectronicName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                                <JQTools:JQGridColumn Alignment="left" Caption="電子發票Email" Editor="text" FieldName="ElectronicEmail" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="230" />
                                <JQTools:JQGridColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="65" />
                                <JQTools:JQGridColumn Alignment="left" Caption="建檔日期" Editor="text" FieldName="CreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="110" Format="yyyy/mm/dd HH:MM:SS" />
                            </Columns>
                            <TooItems>
                                <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                                <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="存檔" Enabled="True" Visible="True" />
                                <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" Enabled="True" Visible="True"  />
                            </TooItems>
                        </JQTools:JQDataGrid>
                        <JQTools:JQValidate ID="JQValidate1" runat="server" BindingObjectID="JQDataGrid1" EnableTheming="True">
                            <Columns>
                                <JQTools:JQValidateColumn CheckNull="True" FieldName="SalesTypeID" RemoteMethod="True" ValidateMessage="請選擇交易別" ValidateType="None" />
                                <JQTools:JQValidateColumn CheckNull="True" FieldName="PayTypeID" RemoteMethod="True" ValidateMessage="請選擇付款方式" ValidateType="None" />
                            </Columns>
                        </JQTools:JQValidate>
                        <JQTools:JQDefault ID="JQDefault1" runat="server" BindingObjectID="JQDataGrid1" EnableTheming="True">
                            <Columns>
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="PayKindNO" RemoteMethod="True" />
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="30" FieldName="BalanceDay" RemoteMethod="True" />
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="30" FieldName="PayDays" RemoteMethod="True" />
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="30" FieldName="PayLeadDays" RemoteMethod="True" />
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetCustNO" DefaultValue="" FieldName="CustNO" RemoteMethod="False" />
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                            </Columns>
                        </JQTools:JQDefault>
                        <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="JQDataForm1" DialogLeft="35px" DialogTop="300px" Title="收款方式維護" Width="720px">
                            <JQTools:JQDataForm ID="JQDataForm1" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="ERPPayKind" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="5" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnApply="CheckPayKind" OnLoadSuccess="JQDataForm1LoadSucess" RemoteName="sERPCustomer.ERPPayKind" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                                <Columns>
                                    <JQTools:JQFormColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="96" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="名稱" Editor="text" FieldName="CUSTSHORTNAME" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="4" Visible="True" Width="96" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="PayKindNO" Editor="text" FieldName="PayKindNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="100" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="交易別" Editor="infocombobox" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERPCustomer.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTypeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="付款代碼" Editor="infocombobox" EditorOptions="valueField:'PayTypeID',textField:'PayTypeName',remoteName:'sERPCustomer.PayType',tableName:'PayType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="PayTypeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="收款結帳日" Editor="numberbox" FieldName="BalanceDay" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="請款日" Editor="numberbox" FieldName="PayDays" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="票期天數" Editor="numberbox" FieldName="PayLeadDays" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" />
                                </Columns>
                            </JQTools:JQDataForm>
                        </JQTools:JQDialog>
                    </div>

                               
            </JQTools:JQDialog>

        </div>
         <div style="display:none;">
             <JQTools:JQDataGrid ID="JQDataGrid5" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" AutoApply="True" CheckOnSelect="True" ColumnsHibeable="False" DataMember="SYSVar2" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sERPCustomer.SYSVar2" Title="JQDataGrid" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="CategoryValue" Editor="text" FieldName="CategoryValue" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                </Columns>
            </JQTools:JQDataGrid>
            </div>
        <script type="text/javascript">
            $(":input").css("background", backcolor);
         </script>
    </form>
    <p>
&nbsp;&nbsp;&nbsp;
    </p>
</body>
</html>
