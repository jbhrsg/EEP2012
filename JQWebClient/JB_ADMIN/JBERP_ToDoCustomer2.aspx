<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_ToDoCustomer2.aspx.cs" Inherits="Template_JQuerySingle1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        var backcolor = "#E8FFE8"
        $(document).ready(function () {
            //呼叫查詢條件
            openForm('#Dialog_Query', {}, 'inserted', 'switch');
            $('#dataGridSalesMaster').datagrid('getPanel').hide();
            $('#dataGridSalesDetails').datagrid('getPanel').hide();
            $('#dataGridView').datagrid('getPanel').hide();
            //初始化查詢條件
            var sDate = new Date();
            var Date1 = $.jbjob.Date.DateFormat(sDate, 'yyyy/MM/dd');
            var sDate2 = new Date($.jbDateAdd('days', -7, sDate));
            var Date2 = $.jbjob.Date.DateFormat(sDate2, 'yyyy/MM/dd');
            $("#JQDFQuerySDate").datebox('setValue', Date2);//開始日期
            $("#JQDFQueryEDate").datebox('setValue', Date1);//結束日期

            //用戶編號=>業務代號
            var UserID = getClientInfo("UserID");
            setTimeout(function () {
                var data = $("#JQDFQuerySalesID").combobox('getData');
                for (var i = 0; i < data.length; i++) {
                    if (data[i].SalesEmployeeID == UserID) {
                        $("#JQDFQuerySalesID").combobox('setValue', data[i].SalesID);
                    }
                }
            }, 200);


            $("#JQDFQuerySourse").options('setValue', 3);
            gvQueryTemp();//查詢業務沒指定時的待辦


            ///客戶資料

            //設定欄位Caption 變顏色(藍)
            var flagIDs = ['#dataFormMasterBillDeal', '#dataFormMasterBillDealEmail', '#dataFormMasterIsPublicView', '#dataFormMasterIsAcceptePaper', '#dataFormMasterNotAcceptPaper', '#dataFormMasterIsAddLine', '#dataFormMasterPostType', '#dataFormMasterIsChangeBank', '#dataFormMasterIndustryType'];
            $(flagIDs.toString()).each(function () {
                var captionTd = $(this).closest('td').prev('td');
                captionTd.css({ color: "#4400CC" });
            });

            //設定欄位Caption 變顏色(紅)
            var flagIDs2 = ['#dataFormMasterNextCallDate', '#dataFormMasterCallNote', '#dataFormMasterNewRecallDate'];
            $(flagIDs2.toString()).each(function () {
                var captionTd = $(this).closest('td').prev('td');
                captionTd.css({ color: "#FF0000" });
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
            //var PayNotes = $('#dataFormMasterPayNotes').closest('td');
            //PayNotes.append($('<lable>').css({ color: '#8A2BE2' }).html(' 最多可輸256文數字'));
            //$('#dataFormMasterCustName').blur(function () {
            //    var cc = $('#dataFormMasterCustShortName').val();
            //    if (cc == "") {
            //        $('#dataFormMasterCustShortName').val($('#dataFormMasterCustName').val());
            //    }
            //});
           
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
        function SetWhereGrid(rowindex, rowdata) {
            

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

            //選取複訪客戶清單 => Setwhere 1.維保紀錄 , 2.交易狀況 , 3.收款方式
            var CustNO = $("#dataFormMasterCustNO").val();

            $("#JQDataGrid1").datagrid('setWhere', "A.CustNO='" + CustNO + "'");
            $("#JQDataGrid4").datagrid('setWhere', "CustNO='" + CustNO + "'");


        }

        //新增付款方式紀錄
        function dataFormMasterOnApplied() {
            var CustNO = $("#dataFormMasterCustNO").val();
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                $("#CustNO_Query").combobox('setValue', CustNO);
                                $("#CustShortName_Query").val('');
                                $("#SalesID_Query").combobox('setValue', '');
                                $("#DealDays_Query").val('');
            }
            //修改複訪內容 & 新增下次複訪提醒
            AddCustomerToDoNotesData();

            if (getEditMode($("#dataFormMaster")) == 'updated') {                
                openForm('#JQDialog1', $('#dataGridView').datagrid('getSelected'), "updated", 'dialog');                

            }
                        
            //1.更新維保紀錄
            //2.重整複訪客戶清單          
            $('#JQDataGrid4').datagrid('reload');
            $('#dataGridView').datagrid('reload');            
            
            //setTimeout(function () {
                //拼湊重要備註 => 本次複訪日+ 本次內容                
            if ($('#dataFormMasterCallNote').val() != '' && $('#dataFormMasterNextCallDate').datebox('getValue') != '') {
                var note = $('#dataFormMasterCallNote').val().trim();
                note = note + ' ' + $('#dataFormMaster2CustNotes').val().trim();
                $('#dataFormMaster2CustNotes').val(note);
            }

            //本次複訪日修改 =>下次複訪日有新增的話
            if ($('#dataFormMasterNewRecallDate').datebox('getValue') != '') {
                $('#dataFormMasterNextCallDate').val($('#dataFormMasterNewRecallDate').datebox('getValue').toString());
            }
            //}, 200);
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
            //alert(123);
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
        
        //ToDoList------------------------------------------------------------------------------


        // 修改複訪內容 & 新增下次複訪提醒
        function AddCustomerToDoNotesData() {
            var CustNO = $('#dataFormMasterCustNO').val();
            var NextCallDate = $('#dataFormMasterNextCallDate').datebox('getValue'); //本次複訪日期(可能修改日期)
            var CallNote = $('#dataFormMasterCallNote').val();//本次內容
            var NewNextCallDate = $('#dataFormMasterNewRecallDate').datebox('getValue'); //下次複訪日期
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERPToDoCustomer.infoCustomerToDoNotes', //連接的Server端，command
                data: "mode=method&method=" + "AddCustomerToDoNotes" + "&parameters=" + encodeURIComponent(CustNO + "," + NextCallDate + "," + CallNote + "," + NewNextCallDate), //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
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

        function gvQueryTemp() {
            dg = '#dataGridTemp';
            //查詢條件
            var result = [];
            var MinSalesDate = $('#JQDFQuerySDate').datebox('getValue');//開始日期
            var MaxSalesDate = $('#JQDFQueryEDate').datebox('getValue');//結束日期

            if (MinSalesDate != '') result.push("NextCallDate >= '" + MinSalesDate + "'");
            if (MaxSalesDate != '') result.push("NextCallDate <= '" + MaxSalesDate + "'");

            $(dg).datagrid('setWhere', result.join(' and '));
            $(dg).datagrid('getPanel').show();

        }

        function gvQuery() {

            //查詢條件
            var result = [];
            var SalesID = $('#JQDFQuerySalesID').combobox('getValue');//業務代號
            var CustNO = $('#JQDFQueryCustNO').combobox('getValue');//客戶代號
            var MinSalesDate = $('#JQDFQuerySDate').datebox('getValue');//開始日期
            var MaxSalesDate = $('#JQDFQueryEDate').datebox('getValue');//結束日期
            var iSourse = $("#JQDFQuerySourse").options('getValue');//提醒類型	
            //選擇Grid
            var dg;
            if (iSourse == 1) {
                dg = '#dataGridSalesMaster';
                $('#dataGridSalesDetails').datagrid('getPanel').hide();
                $('#dataGridView').datagrid('getPanel').hide();
                $('#dataGridTemp').datagrid('getPanel').hide();
            } else if (iSourse == 2) {
                dg = '#dataGridSalesDetails';
                $('#dataGridSalesMaster').datagrid('getPanel').hide();
                $('#dataGridView').datagrid('getPanel').hide();
                $('#dataGridTemp').datagrid('getPanel').hide();
            } else if (iSourse == 3) {
                dg = '#dataGridView';
                $('#dataGridSalesMaster').datagrid('getPanel').hide();
                $('#dataGridSalesDetails').datagrid('getPanel').hide();
                $('#dataGridTemp').datagrid('getPanel').hide();
            }

            //3.複訪日期	1.到期客戶	2.銷貨備註
            if (CustNO != '') result.push("m.CustNO = '" + CustNO + "'");

            if (iSourse == 1) {//到期客戶列表      
                if (SalesID != '') result.push("SalesID = '" + SalesID + "'");
                if (MinSalesDate != '') result.push("MaxSalesDate >= '" + MinSalesDate + "'");
                if (MaxSalesDate != '') result.push("MaxSalesDate <= '" + MaxSalesDate + "'");

            } else if (iSourse == 2) {//銷貨備註提醒列表 
                if (SalesID != '') result.push("SalesID = '" + SalesID + "'");
                if (MinSalesDate != '') result.push("SalesDescrDate >= '" + MinSalesDate + "'");
                if (MaxSalesDate != '') result.push("SalesDescrDate <= '" + MaxSalesDate + "'");

            } else if (iSourse == 3) {//複訪日期提醒列表 
                if (SalesID != '') result.push("d.SalesID = '" + SalesID + "'");
                if (MinSalesDate != '') result.push("NextCallDate >= '" + MinSalesDate + "'");
                if (MaxSalesDate != '') result.push("NextCallDate <= '" + MaxSalesDate + "'");
            }

            $(dg).datagrid('setWhere', result.join(' and '));
            $(dg).datagrid('getPanel').show();

        }

        function gvQuerylist() {
            //查詢條件
            var result = [];
            var SalesID = $('#JQDFQuerySalesID').combobox('getValue');//業務代號
            var CustNO = $('#JQDFQueryCustNO').combobox('getValue');//客戶代號
            var MinSalesDate = $('#JQDFQuerySDate').datebox('getValue');//開始日期
            var MaxSalesDate = $('#JQDFQueryEDate').datebox('getValue');//結束日期
            var iSourse = $("#JQDFQuerySourse").options('getValue');//提醒類型	           

            $('#dataGridView').datagrid('getPanel').show();
            //	1.到期客戶	2.銷貨備註 3.複訪日期
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERPToDoCustomer.Querylist',  //連接的Server端，command
                data: "mode=method&method=" + "getQuerylist" + "&parameters=" + SalesID + "," + CustNO + "," + MinSalesDate + "," + MaxSalesDate + "," + iSourse,
                cache: false,
                async: true,
                success: function (data) {
                    var rows = $.parseJSON(data);//將JSon轉會到Object類型提供給Grid顯示                            

                    if (rows.length > 10) {
                        //通過loadData方法清除掉原有Grid中的舊有資料並填補分頁資料
                        $('#dataGridView').datagrid({ loadFilter: pagerFilter, url: "" }).datagrid('loadData', rows);
                    } else {
                        $('#dataGridView').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                    }
                }
            });

            ControlGridItem(iSourse);//顯示或隱藏DataGrid Item    
        }
        //依據提醒類型 顯示隱藏 DataGrid Item選項 (提醒類型 1.到期客戶	2.銷貨備註 3.複訪日期)        
        function ControlGridItem(iSourse) {
           
            //1.到期客戶 => 訂單刊登起始日期 ,訂單刊登結束日期 ,最後刊登日期, 剩餘數,保留天數,刊登提醒,聯絡備註
            var HideFieldName1 = ['MinSalesDate', 'MaxSalesDate', 'LastSalesDate', 'UseQty', 'KeepDays', 'KeepDaysAlert', 'ContractDescr'];

            //2.銷貨備註 => 聯絡備註  ,備註提醒, 備註提醒日期
            var HideFieldName2 = ['ContractDescr', 'SalesDescrAlert', 'SalesDescrDate'];

            //3.複訪日期 => 預計複訪日期,預計時間,同業刊登中,網址,客戶等級,業務,電話號碼1,聯絡人 & 分機
            var HideFieldName3 = ['NextCallDate', 'NextCallTime', 'SourceName', 'HrBankUrl', 'ListContent', 'SalesName', 'CustTelNO', 'ContactAandTel']

            if (iSourse == "1")//
            {
                $.each(HideFieldName1, function (index, fieldName2) {
                    $("#dataGridView").datagrid('showColumn', fieldName2);
                });
                $.each(HideFieldName2, function (index, fieldName) {
                    $("#dataGridView").datagrid('hideColumn', fieldName);
                });
                $.each(HideFieldName3, function (index, fieldName) {
                    $("#dataGridView").datagrid('hideColumn', fieldName);
                });
                
            } else if (iSourse == "2")//
            {
                $.each(HideFieldName1, function (index, fieldName2) {
                    $("#dataGridView").datagrid('hideColumn', fieldName2);
                });
                $.each(HideFieldName2, function (index, fieldName) {
                    $("#dataGridView").datagrid('showColumn', fieldName);
                });
                $.each(HideFieldName3, function (index, fieldName) {
                    $("#dataGridView").datagrid('hideColumn', fieldName);
                });

            } else if (iSourse == "3") {
                $.each(HideFieldName1, function (index, fieldName2) {
                    $("#dataGridView").datagrid('hideColumn', fieldName2);
                });
                $.each(HideFieldName2, function (index, fieldName) {
                    $("#dataGridView").datagrid('hideColumn', fieldName);
                });
                $.each(HideFieldName3, function (index, fieldName) {
                    $("#dataGridView").datagrid('showColumn', fieldName);
                });
            }
        }
     

        function TempReload() {
            $('#dataGridTemp').datagrid('reload');
            AddCustomerToDoNotes2();
        }

        // 新增下次複訪提醒(by Grid)----Temp沒有業務的
        function AddCustomerToDoNotes2() {
            var row = $('#dataGridTemp').datagrid('getSelected');
            var CustNO = row.CustNO;//客戶代號            
            var NextCallDateAdd = row.NextCallDateAdd; //下次複訪日期   
            var NextCallTimeAdd = row.NextCallTimeAdd; //下次複訪時間 
            var PostType = row.ListContent;//客戶等級
            var SalesID = row.SalesName;//業務
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalesToDoList.infoCustomerToDoNotes', //連接的Server端，command
                data: "mode=method&method=" + "AddCustomerToDoNotesSalse" + "&parameters=" + encodeURIComponent(CustNO + "," + NextCallDateAdd + "," + NextCallTimeAdd + "," + PostType + "," + SalesID), //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
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
        //呼叫人力銀行網址
        function OpenBankUrl(val, row) {
            val = row.HrBankURL;
            if (val != '' && val != null) {
                //return $('<a>', { href: '#', onclick: 'window.open("' + val + '","人力銀行網址");', theData: row.val }).linkbutton({ text: "<b><div style=\"color:Red\">" + val + "</div></b>", plain: true })[0].outerHTML;
                return $('<a>', { href: "#", onclick: 'window.open("' + val + '","人力銀行網址");', theData: row.val }).linkbutton({ text: "<img src=../js/themes/icons/BankUrl.png></a><b><div style=\"color:Red\"></div></b>", plain: true })[0].outerHTML;
            }
        }
        //提醒類型選擇
        function OnSelectSourse(val) {
            //	1.到期客戶	2.銷貨備註 3.複訪日期
            if (val == 3) {
                var sDate = new Date();
                var sDate2 = new Date($.jbDateAdd('days', -7, sDate));
                var Date2 = $.jbjob.Date.DateFormat(sDate2, 'yyyy/MM/dd');
                $("#dataFormMasterSDate").datebox('setValue', Date2);//開始日期
            } else {
                $("#dataFormMasterSDate").datebox('setValue', "");//開始日期
            }
        }
        function SalesMasterReload() {
            $('#dataGridSalesMaster').datagrid('reload');
        }
        function SalesDetailsReload() {
            $('#dataGridSalesDetails').datagrid('reload');
        }
        //-------------------------------1.到期客戶------------------------------------------------------------------------------
        // 到期客戶=>天數提醒(主檔),是否失效(明細)CheckBox
        function genCheckBox(value, row, index) {            
            if (value != 0)
                return "<a href='javascript: void(0)' onclick='KeepDaysAlert(" + row.SalesMasterNO + "," + row.ItemSeq + ",1);'>×</a>";
            else return value;
        }
        // 到期客戶=>刊登提醒 取消 ERPSalesMaster => KeepDaysAlert=0
        function KeepDaysAlert(SalesMasterNO,ItemSeq,Type) {
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERPToDoCustomer.UpdateERPSalesMasterDaysAlert', //連接的Server端，command
                data: "mode=method&method=" + "UpdateERPSalesMasterDaysAlert" + "&parameters=" + encodeURIComponent(SalesMasterNO + "," + ItemSeq + "," + Type), //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                cache: false,
                async: false,
                success: function (data) {
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });
            gvQuerylist();
        }
        //--------------------------2.銷貨備註-----------------------------------------------------------------
        // 備註提醒失效(明細)CheckBox
        function genCheckBox2(value, row, index) {
            if (value != 0)
                return "<a href='javascript: void(0)' onclick='KeepDaysAlert(" + row.SalesMasterNO + "," + row.ItemSeq + ",2);'>×</a>";
            else return value;
        }
        
   </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />

                        <JQTools:JQDialog ID="Dialog_Query" runat="server" BindingObjectID="JQDFQuery" EditMode="Switch" Title="查詢" DialogLeft="50px" DialogTop="20px" Width="750px" ShowSubmitDiv="False">
                <JQTools:JQDataForm ID="JQDFQuery" runat="server" DataMember="infoQuery" HorizontalColumnsCount="2" RemoteName="sERPToDoCustomer.infoQuery" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶代號" Editor="infocombobox" FieldName="CustNO" Format="" Width="170" ReadOnly="False" Span="1" EditorOptions="valueField:'CustNO',textField:'CustShortName',remoteName:'sERPSalseDetails.infoCustomersAll',tableName:'infoCustomersAll',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="業務代號" Editor="infocombobox" FieldName="SalesID" Format="" MaxLength="0" Span="1" Width="120" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sERPSalseDetails.infoSalesMan',tableName:'infoSalesMan',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="開始日期	" Editor="datebox" FieldName="SDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="結束日期" Editor="datebox" FieldName="EDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="提醒類型" Editor="infooptions" FieldName="Sourse" MaxLength="0" Width="80" EditorOptions="title:'JQOptions',panelWidth:280,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:3,multiSelect:false,openDialog:false,selectAll:false,onSelect:OnSelectSourse,selectOnly:false,items:[{text:'複訪日期',value:'3'},{text:'到期客戶',value:'1'},{text:'銷貨備註',value:'2'}]" Span="2" />
                    </Columns>
                </JQTools:JQDataForm>
                <a href="#" class="easyui-linkbutton" OnClick="gvQuerylist()">查詢</a>
            </JQTools:JQDialog>
            <div>
                <JQTools:JQDataGrid ID="dataGridTemp" data-options="pagination:true,view:commandview" RemoteName="sERPToDoCustomer.infoTempToDoNotes" runat="server" AutoApply="False"
                DataMember="infoTempToDoNotes" Pagination="True" QueryTitle="" EditDialogID=""
                Title="無對應業務客戶" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" OnUpdated="TempReload" BufferView="False" NotInitGrid="False" RowNumbers="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="預計複訪日期" Editor="text" FieldName="NextCallDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="80" Format="yyyy/mm/dd">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="預計時間" Editor="text" FieldName="NextCallTime" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="同業刊登中" Editor="text" FieldName="SourceName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="69" Format="" EditorOptions="" />
                    <JQTools:JQGridColumn Alignment="center" Caption="客戶代號" Editor="text" FieldName="CustNO" Format="" MaxLength="0" Width="70" ReadOnly="True"  />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶簡稱" Editor="text" FieldName="CustShortName" Format="" MaxLength="0" Width="80" ReadOnly="True"  />
                    <JQTools:JQGridColumn Alignment="center" Caption="網址" Editor="text" FieldName="HrBankUrl" FormatScript="OpenBankUrl" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="37">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="客戶等級" Editor="infocombobox" FieldName="ListContent" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="69" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sERPCustomerLite.infoPostType',tableName:'infoPostType',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="center" Caption="業務" Editor="infocombobox" FieldName="SalesName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="85" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sERPSalseDetails.infoSalesMan',tableName:'infoSalesMan',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="複訪內容" Editor="text" FieldName="Notes" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="230">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="下次複訪日期" Editor="datebox" FieldName="NextCallDateAdd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="88">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="下次複訪時間" Editor="infocombobox" FieldName="NextCallTimeAdd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="85" EditorOptions="valueField:'NextCallTime',textField:'NextCallTime',remoteName:'sERPToDoCustomer.infoNextCallTime',tableName:'infoNextCallTime',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="120" Format="" />
                    <JQTools:JQGridColumn Alignment="center" Caption="建立者" Editor="" FieldName="UpdateBy" Format="" Width="40" ReadOnly="True" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" Visible="False" />
                    <JQTools:JQGridColumn Alignment="center" Caption="建立日期" Editor="datebox" FieldName="UpateDate" Format="yyyy-mm-dd HH:MM" Width="94" ReadOnly="True" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" Visible="False" FormatScript="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="NotesCreateDate" Editor="datebox" FieldName="NotesCreateDate" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption=" " Editor="text" FieldName="CreateBy" FormatScript="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="70">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>                  
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="確定" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />  
                </TooItems>
            </JQTools:JQDataGrid>
            </div>       

            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERPToDoCustomer.infoCustomerToDoNotes" runat="server" AutoApply="True"
                DataMember="infoCustomerToDoNotes" Pagination="True" QueryTitle="客戶篩選" EditDialogID="JQDialog1"
                Title="複訪客戶清單" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" ParentObjectID="" OnSelect="SetWhereGrid" OnDelete="dataGridViewOnDelete" OnLoadSuccess="dataGridViewLoadSucess" BufferView="False" NotInitGrid="False" RowNumbers="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustNO" Format="" MaxLength="0" Width="80" Frozen="True" Sortable="True" />
                     <JQTools:JQGridColumn Alignment="center" Caption="預計複訪日期" Editor="text" FieldName="NextCallDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="True" Visible="True" Width="83" Format="yyyy/mm/dd">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="預計時間" Editor="text" FieldName="NextCallTime" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="同業刊登中" Editor="text" FieldName="SourceName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="69" Format="" EditorOptions="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶簡稱" Editor="text" FieldName="CustShortName" Format="" MaxLength="0" Width="100" ReadOnly="True" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="網址" Editor="text" FieldName="HrBankUrl" MaxLength="0" Width="110" ReadOnly="True" FormatScript="OpenBankUrl"  />
                    <JQTools:JQGridColumn Alignment="center" Caption="客戶等級" Editor="text" FieldName="ListContent" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="69" EditorOptions="">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="業務" Editor="text" FieldName="SalesName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="True" Visible="True" Width="55" />
                    <JQTools:JQGridColumn Alignment="left" Caption="電話號碼1" Editor="text" FieldName="CustTelNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="75">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人&amp;分機" Editor="text" FieldName="ContactAandTel" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="訂單刊登起始日期" Editor="datebox" FieldName="MinSalesDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="訂單刊登結束日期" Editor="datebox" FieldName="MaxSalesDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="最後刊登日期" Editor="datebox" FieldName="LastSalesDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="剩餘數" Editor="text" FieldName="UseQty" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="保留天數" Editor="text" FieldName="KeepDays" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="刊登提醒" Editor="text" FieldName="KeepDaysAlert" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" EditorOptions="" FormatScript="genCheckBox">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡備註 " Editor="text" FieldName="ContractDescr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="350">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="備註提醒" Editor="text" FieldName="SalesDescrAlert" FormatScript="genCheckBox2" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="備註提醒日期" Editor="text" FieldName="SalesDescrDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100" Format="yyyy/mm/dd">
                    </JQTools:JQGridColumn>
                </Columns>    
                                        
            </JQTools:JQDataGrid>
            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="客戶維護" DialogLeft="10px" DialogTop="25px" EnableTheming="False" Width="1020px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="infoCustomerInfo" HorizontalColumnsCount="5" RemoteName="sERPToDoCustomer.infoCustomerInfo" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="True" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" OnApplied="dataFormMasterOnApplied" ParentObjectID="dataGridView" OnLoadSuccess="dataFormMasterLoadSucess" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" OnApply="dataFormMasterOnApply" ChainDataFormID="dataFormMaster2" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustNO" maxlength="20" Width="90" OnBlur="OnBlurCustNO" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustName" maxlength="50" Width="180" OnBlur="OnBlurCustName" Span="1" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="簡稱" Editor="text" FieldName="CustShortName" maxlength="30" Width="116" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="雇主" Editor="text" FieldName="BossName" Span="1" Width="115" maxlength="0" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="員工人數" Editor="numberbox" FieldName="iPeopleCount" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="60" />
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
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" FieldName="ContactBJobID" Format="" Width="113" EditorOptions="valueField:'CustJobID',textField:'CustJobName',remoteName:'sERPCustomers.CustJob',tableName:'CustJob',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" Visible="True" ReadOnly="False" maxlength="0" NewRow="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="開立發票" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsPutInvoice" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="報紙發票" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsPutPaperInvoice" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="寄電子報" Editor="infocombobox" EditorOptions="valueField:'ePaperCode',textField:'ePaperType',remoteName:'sERPCustomer.PaperType',tableName:'PaperType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="IsAcceptePaper" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="不寄原因" Editor="text" FieldName="NotAcceptPaper" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="275" />
                        <JQTools:JQFormColumn Alignment="left" Caption="轉永豐銀行" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsChangeBank" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="媒體產業" Editor="infocombobox" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sERPSalseDetails.infoIndustryType',tableName:'infoIndustryType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="IndustryType" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="結帳日" Editor="text" FieldName="BalanceDay" Format="" Width="90" NewRow="False" Span="1" Visible="True" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="行業別" Editor="infocombobox" FieldName="IndustryID" Width="276" maxlength="0" Span="2" EditorOptions="valueField:'jb_type',textField:'jb_name',remoteName:'sERPCustomer.Industry',tableName:'Industry',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="帳務人員" Editor="text" FieldName="BillDeal" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="eMail" Editor="text" FieldName="BillDealEmail" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="加入LINE" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsAddLine" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶等級" Editor="infocombobox" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sERPCustomer.PostType',tableName:'PostType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="PostType" MaxLength="0" Span="1" Width="122" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="本次複訪日" Editor="datebox" FieldName="NextCallDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="本次內容" Editor="text" FieldName="CallNote" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="695" />
                        <JQTools:JQFormColumn Alignment="left" Caption="下次複訪日" Editor="datebox" FieldName="NewRecallDate" Format="" maxlength="0"  ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="重要備註" Editor="textarea" FieldName="CustNotes" Format="" Width="760" EditorOptions="height:45" Span="5" Visible="False" ReadOnly="False" MaxLength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="求才備註" Editor="textarea" EditorOptions="height:30" FieldName="DealNotesP" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="5" Visible="False" Width="760" />
                        <JQTools:JQFormColumn Alignment="left" Caption="週報備註" Editor="textarea" EditorOptions="height:30" FieldName="DealNotesW" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="5" Visible="False" Width="760" />
                        <JQTools:JQFormColumn Alignment="left" Caption="報紙備註" Editor="textarea" EditorOptions="height:30" FieldName="DealNotesN" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="5" Visible="False" Width="760" />
                        <JQTools:JQFormColumn Alignment="left" Caption="票匯備註" Editor="textarea" EditorOptions="height:17" FieldName="PayNotes" maxlength="256" NewRow="False" ReadOnly="False" RowSpan="1" Span="5" Visible="False" Width="760" />
                        <JQTools:JQFormColumn Alignment="left" Caption="最近交易日" Editor="datebox" FieldName="LatelyDayD" Format="yyyy/mm/dd" ReadOnly="True" Visible="False" Width="90" maxlength="0" NewRow="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="求才交易日" Editor="datebox" FieldName="LatelyDayP" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="90" Format="yyyy/mm/dd" />
                        <JQTools:JQFormColumn Alignment="left" Caption="週報交易日" Editor="datebox" FieldName="LatelyDayW" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="90" Format="yyyy/mm/dd" />
                        <JQTools:JQFormColumn Alignment="left" Caption="報紙交易日" Editor="datebox" FieldName="LatelyDayN" ReadOnly="True" Span="2" Visible="False" Width="90" Format="yyyy/mm/dd" MaxLength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />     
                        <JQTools:JQFormColumn Alignment="left" Caption="工業區別" Editor="infocombobox" EditorOptions="valueField:'IndustryTypeID',textField:'IndustryTypeName',remoteName:'sERPCustomer.Industry',tableName:'Industry',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="IndustryTypeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="開放查詢" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsPublicView" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
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
                </JQTools:JQValidate>
                <div class="easyui-tabs" style="width:960px;height100%" id="tabs1">
                    <div title="交易狀況" style="padding:10px">
                        <JQTools:JQDataForm ID="dataFormMaster2" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="infoCustomerToDoNotes" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="4" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="True" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="dataGridView" RemoteName="sERPToDoCustomer.infoCustomerToDoNotes" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                            <Columns>
                                <JQTools:JQFormColumn Alignment="left" Caption="重要備註" Editor="textarea" EditorOptions="height:45" FieldName="CustNotes" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="840" />
                                <JQTools:JQFormColumn Alignment="left" Caption="求才備註" Editor="textarea" EditorOptions="height:30" FieldName="DealNotesP" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="840" />
                                <JQTools:JQFormColumn Alignment="left" Caption="週報備註" Editor="textarea" EditorOptions="height:30" FieldName="DealNotesW" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="840" />
                                <JQTools:JQFormColumn Alignment="left" Caption="報紙備註" Editor="textarea" EditorOptions="height:30" FieldName="DealNotesN" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="840" />
                                <JQTools:JQFormColumn Alignment="left" Caption="票匯備註" Editor="textarea" EditorOptions="height:17" FieldName="PayNotes" maxlength="256" NewRow="False" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="840" />
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
                         <JQTools:JQDataGrid ID="JQDataGrid4" runat="server" EditDialogID="JQDialog4" AlwaysClose="True" DataMember="CustNotes" RemoteName="sERPCustomer.CustNotes" AllowAdd="True" AllowDelete="False" AllowUpdate="True" AutoApply="True" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="5,10,20,30,40,50" PageSize="5" Pagination="True" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Panel" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" OnUpdate="" Width="940px" BufferView="False" NotInitGrid="False" RowNumbers="True">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="提出人員" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="預計覆訪日" Editor="text" FieldName="NextCallDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="70" Format="yyyy/mm/dd">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="預計時段" Editor="text" FieldName="NextCallTime" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="實際覆訪內容" Editor="text" FieldName="Notes" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="480" FormatScript="genToolTipNotes">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="實際覆訪日" Editor="text" FieldName="NotesCreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="110" Format="yyyy/mm/dd HH:MM:SS">
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
                    <div title="收款方式" style="padding:10px">
                        <JQTools:JQDataGrid ID="JQDataGrid1" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="ERPPayKind" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialog2" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sERPCustomer.ERPPayKind" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" Width="750px">
                            <Columns>
                                <JQTools:JQGridColumn Alignment="left" Caption="PayKindNO" Editor="text" FieldName="PayKindNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustNO" Frozen="True" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70" />
                                <JQTools:JQGridColumn Alignment="left" Caption="名稱" Editor="text" FieldName="CUSTSHORTNAME" Frozen="True" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="160" />
                                <JQTools:JQGridColumn Alignment="left" Caption="交易別" Editor="infocombobox" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERPCustomer.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100" />
                                <JQTools:JQGridColumn Alignment="left" Caption="付款代碼" Editor="infocombobox" EditorOptions="valueField:'PayTypeID',textField:'PayTypeName',remoteName:'sERPCustomer.PayType',tableName:'PayType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="PayTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                                <JQTools:JQGridColumn Alignment="center" Caption="收款結帳日" Editor="text" FieldName="BalanceDay" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                                <JQTools:JQGridColumn Alignment="center" Caption="請款日" Editor="text" FieldName="PayDays" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70" />
                                <JQTools:JQGridColumn Alignment="center" Caption="票期" Editor="text" FieldName="PayLeadDays" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70" />
                                <JQTools:JQGridColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70" />
                                <JQTools:JQGridColumn Alignment="left" Caption="建檔日期" Editor="text" FieldName="CreateDate" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120" />
                            </Columns>
                            <TooItems>
                                <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                            </TooItems>
                        </JQTools:JQDataGrid>
                        <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="JQDataForm1" DialogLeft="35px" DialogTop="300px" Title="收款方式維護" Width="720px">
                        <JQTools:JQDataForm ID="JQDataForm1" runat="server" Closed="False" ContinueAdd="False" DataMember="ERPPayKind" disapply="False" DuplicateCheck="False" HorizontalColumnsCount="5" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnApply="CheckPayKind" OnLoadSuccess="JQDataForm1LoadSucess" RemoteName="sERPCustomer.ERPPayKind" ShowApplyButton="False" ValidateStyle="Hint">
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
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetCustNO" DefaultValue="" FieldName="CustNO" RemoteMethod="False" />
                            </Columns>
                        </JQTools:JQDefault>
                        <JQTools:JQValidate ID="JQValidate1" runat="server" BindingObjectID="JQDataForm1" EnableTheming="True">
                        </JQTools:JQValidate>
                    </JQTools:JQDialog>
                    </div>
                </div>

            </JQTools:JQDialog>

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
            
                
                                            
                    <JQTools:JQDataGrid ID="dataGridSalesMaster" data-options="pagination:true,view:commandview" RemoteName="sERPToDoCustomer.ERPSalesMaster" runat="server" AutoApply="True"
                DataMember="ERPSalesMaster" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="15" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" OnUpdated="SalesMasterReload" BufferView="False" NotInitGrid="False" RowNumbers="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustNO" Format="" MaxLength="0" Width="70" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶簡稱" Editor="text" FieldName="CustShortName" Format="" MaxLength="0" Width="75" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="刊登起始日期" Editor="text" FieldName="MinSalesDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="center" Caption="最後刊登日期" Editor="text" FieldName="MaxSalesDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="85" Format="yyyy/mm/dd" />
                    <JQTools:JQGridColumn Alignment="center" Caption="剩餘數" Editor="text" FieldName="UseQty" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="53" />
                    <JQTools:JQGridColumn Alignment="center" Caption="保留天數" Editor="text" FieldName="KeepDays" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="53" />
                    <JQTools:JQGridColumn Alignment="center" Caption="刊登提醒" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="KeepDaysAlert" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="53" FormatScript="genCheckBox" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesMasterNO" Editor="text" FieldName="SalesMasterNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesEmployeeID" Editor="text" FieldName="SalesEmployeeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡備註" Editor="text" FieldName="ContractDescr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="450" />
                </Columns>
                <TooItems>                  
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="確定" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />  
                </TooItems>
            </JQTools:JQDataGrid>
                     <JQTools:JQDataGrid ID="dataGridSalesDetails" data-options="pagination:true,view:commandview" RemoteName="sERPToDoCustomer.ERPSalesDetails" runat="server" AutoApply="True"
                DataMember="ERPSalesDetails" Pagination="True" QueryTitle="" EditDialogID=""
                Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnUpdated="SalesDetailsReload">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="客戶代號" Editor="text" FieldName="CustNO" Format="" MaxLength="0" Width="70" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="客戶簡稱" Editor="text" FieldName="CustShortName" Format="" MaxLength="0" Width="80" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡備註" Editor="textarea" FieldName="ContractDescr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="550" />
                    <JQTools:JQGridColumn Alignment="center" Caption="備註提醒" Editor="checkbox" FieldName="SalesDescrAlert" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="53" EditorOptions="on:1,off:0" FormatScript="genCheckBox" />
                    <JQTools:JQGridColumn Alignment="center" Caption="備註提醒日期" Editor="datebox" FieldName="SalesDescrDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" Format="yyyy/mm/dd" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesID" Editor="text" FieldName="ItemSeq" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="120" Format="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesMasterNO" Editor="text" FieldName="SalesMasterNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesEmployeeID" Editor="text" FieldName="SalesEmployeeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                </Columns>
                <TooItems>                  
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="確定" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />  
                </TooItems>
            </JQTools:JQDataGrid>

        </div>
        
    </form>
</body>
</html>
