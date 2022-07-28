<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBHunter_Customers.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">

        var UserID = getClientInfo("UserID");
        $(document).ready(function () {                     
            　
            //設定 Grid QueryColunm panel寬度調整
            var dgid = $('#dataGridView');
            var queryPanel = getInfolightOption(dgid).queryDialog;
            if (queryPanel)
                $(queryPanel).panel('resize', { width: 1050 });
                                                        
            //客戶電話 區號+電話 合併為同TD顯示
            var CountyArea = $('#dataFormMasterContactContryArea').closest('td');
            var Custarea1 = $('#dataFormMasterCustomerTelArea').closest('td').children();
            var Custcode1 = $('#dataFormMasterCustomerTel').closest('td').children();//
            //var CustomerAddress = $('#dataFormMasterCustomerAddress').closest('td').children();
            CountyArea.append('&nbsp;&nbsp;電話').append(Custarea1).append('-').append(Custcode1);//.append('&nbsp;&nbsp;&nbsp;&nbsp;地址').append(CustomerAddress);

            //網址+客戶編號 連結顯示
            //var Url = $('#dataFormMasterCustomerUrl').closest('td');
            //var CustID = $('#dataFormMasterCustID').closest('td').children();
            //Url.append('&nbsp;&nbsp;&nbsp;客戶編號').append(CustID);

            //在客戶名稱後加入經濟部商業司超連結
            var EconmicLink = $("<a>").attr({ 'href': 'https://findbiz.nat.gov.tw/fts/query/QueryBar/queryInit.do' }).attr({ 'target': '_blank' }).text("    經濟部商業司");
            var CustName = $('#dataFormMasterCustName').closest('td');
            CustName.append(EconmicLink);

            //--------------聯絡人頁籤-----------------------------------------------------------------
            //聯絡人+性別+職稱+部門+屬性 合併為同TD顯示
            var ContactName = $('#DFContactPersonContactName').closest('td');
            var Gender = $('#DFContactPersonContactGender').closest('td').children();
            var ContactTitle = $('#DFContactPersonContactTitle').closest('td').children();
            var ContactDept = $('#DFContactPersonContactDept').closest('td').children();
            var Property = $('#DFContactPersonContactProperty').closest('td').children();
            ContactName.append('&nbsp;&nbsp;性別').append(Gender).append('&nbsp;&nbsp;職稱').append(ContactTitle).append('&nbsp;&nbsp;部門').append(ContactDept).append('&nbsp;&nbsp;屬性').append(Property);
            //客戶聯絡人區號+電話+分機+Mail 合併為同TD顯示
            var ContactCountyArea = $('#DFContactPersonContactCountyArea').closest('td');
            var area1 = $('#DFContactPersonContactTelArea').closest('td').children();
            var code1 = $('#DFContactPersonContactTel').closest('td').children();
            var ext1 = $('#DFContactPersonContactTelExt').closest('td').children();
            var Mail = $('#DFContactPersonContacteMail').closest('td').children();
            ContactCountyArea.append('&nbsp;&nbsp;電話').append(area1).append('-').append(code1).append(' 分機').append(ext1).append('&nbsp;&nbsp;eMail').append(Mail);
            //手機 =>國碼+手機合併
            var Mobile1Area = $('#DFContactPersonContactMobile1Area').closest('td');
            var Mobile1 = $('#DFContactPersonContactMobile1').closest('td').children();
            var Mobile2Area = $('#DFContactPersonContactMobile2Area').closest('td').children();
            var Mobile2 = $('#DFContactPersonContactMobile2').closest('td').children();
            Mobile1Area.append('&nbsp;&nbsp;手機1').append(Mobile1).append('例:0933-123456&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;國碼&nbsp;').append(Mobile2Area).append('&nbsp;&nbsp;手機2').append(Mobile2).append('例:0933-123456');

            //即時通類型1 即時通類型+帳號合併縣顯示
            var imtype1 = $('#DFContactPersonContIMType1').closest('td');
            var imno1 = $('#DFContactPersonContIMNO1').closest('td').children();
            //即時通類型2 即時通類型+帳號合併縣顯示
            var imtype2 = $('#DFContactPersonContIMType2').closest('td').children();
            var imno2 = $('#DFContactPersonContIMNO2').closest('td').children();
            imtype1.append('&nbsp;&nbsp;&nbsp;帳號').append(imno1).append('&nbsp;&nbsp;&nbsp;即時通2').append(imtype2).append('&nbsp;&nbsp;&nbsp;帳號').append(imno2);

            //--------------聯繫紀錄權限控管---------------------------------------------------
            //var ViewContactUsers = $('#DFContactRecordIsShade').closest('td');
            //ViewContactUsers.append($('<a>', { href: 'javascript:void(0)' }).on('click', function () {
            //    var ContactDescr = $("#DFContactRecordContactDescr").val();
            //    if (ContactDescr == "" || ContactDescr == undefined) {
            //        alert('注意!!,請先輸入聯絡內容,再設定分享');
            //        $("#DFContactRecordContactDescr").focus();
            //        return false;
            //    }
            //    var IsShade = $("#DFContactRecordIsShade").checkbox('getValue');
            //    if (IsShade == 0) {
            //        alert('注意!!,要設訂分享聯絡內容時,需先選取遮蔽紀錄');
            //        $("#DFContactRecordIsShade").checkbox('setValue', 1);
            //        return false;
            //    }
            //    var SalesKindID = $("#DFContactRecordSalesKindID").val();
            //    var FiltStr = "B.SalesKindID = '" + SalesKindID + "'";
            //    $("#JQDataGrid1").datagrid('setWhere', FiltStr);
            //    openForm('#JQDialogClu', {}, "", 'dialog');
            //    return true;
            //}).linkbutton({ text: '分享給' }));
        
            //--------------客戶職缺傳入客戶代號 => 查詢客戶---------------------------------------------------
            var parameter = Request.getQueryStringByName("CustID");
            if (parameter != "") {
                $("#CustID_Query").val(parameter); 
                queryGrid('#dataGridView');
                //setTimeout(function () {
                //    openForm('#JQDialog1', $('#dataGridView').datagrid('getSelected'), "updated", 'dialog');
                //}, 800);
            }            

                    
        });

        //--------------聯繫紀錄權限控管---------------------------------------------------
        function JQDataGrid1OnLoadSuccess() {
            var ShareTo = $("#DFContactRecordShareTo").val();
            if (ShareTo.length > 0) {
                var rows = $("#JQDataGrid1").datagrid("getRows");
                for (var k = 0; k < rows.length; k++) {
                    if (ShareTo.indexOf(rows[k].SalesID) != -1) {
                        $('#JQDataGrid1').datagrid("checkRow", k);
                    }
                }
            }
        }
        function JQDialogCluOnSubmited() {
            //var rows = $('#JQDataGrid1').datagrid("getChecked");
            //var count = rows.length;
            //if (count == 0) {
            //    alert('注意!!未選取任何業務人員,請選取');
            //    return false;
            //}
            //var ShareTo = '';
            //var ShareToName = '';
            //for (var i = 0; i <= rows.length - 1; i++) {
            //    if (i > 0) {
            //        ShareTo = ShareTo + ',' + rows[i].SalesID;
            //        ShareToName = ShareToName + ',' + rows[i].SalesName;
            //    }
            //    else {
            //        ShareTo = ShareTo + rows[0].SalesID;
            //        ShareToName = ShareToName + ',' + rows[i].SalesName;
            //    }
            //}
            //$("#DFContactRecordShareTo").val(ShareTo);
            //$("#DFContactRecordShareToName").val(ShareToName);
            //return true;
        }
        //在JQDataGridContact中,顯示ContactDescr文字內容
        function ShowContactJDGC(value, row) {
            //var str = '';
            //var ShareTo = '';
            //if (row.ShareTo != null) {
            //    var ShareTo = row.ShareTo;
            //}
            //var slen = (value).trim().length;
            //if (slen > 0) {
            //    if (row.IsShade == true && ((row.CreateBy != UserID) && (ShareTo.indexOf(UserID) == -1))) {
            //        str = ".........聯絡內容遮蔽...........";
            //    }
            //    else {
            //        str = value;
            //    }
            //}
            //return  str ;
            //return "<p style='margin:0px;word-wrap:break-word;white-space: normal'>" + str + "</p>";
        }


        //客戶資料dataForm的縣市連動
        function dataFormMasterAddr_Country_OnSelect() {
            var addr_City = $("#dataFormMasterAddr_Country").combobox('getValue');
            $("#dataFormMasterAddr_City").combobox('setWhere', "Country='" + addr_City + "'");
            $("#dataFormMasterAddr_City").combobox('enable');
        }
        //客戶資料dataForm的鄉鎮市連動
        function dataFormMasterAddr_City_OnSelect(rowdata) {
            $("#dataFormMasterZIPCode").val(rowdata.ZIPCode);
            var country = $("#dataFormMasterAddr_Country").combobox('getValue');
            var city = $("#dataFormMasterAddr_City").combobox('getValue');
            $("#dataFormMasterCustomerAddress").val(country + city);
        }

        //客戶,電話區碼 =>2-4
        function CheckCustomerTelArea(phone) {
            var regex = /^\d{2,4}$/;
            if (!regex.test(phone)) {
                $("#dataFormMasterCustomerTelArea").focus();
                return false;
            } else {
                return true;
            }
        }
        //客戶,電話 =>6-9
        function CheckCustomerTel(phone) {
            var regex = /^\d{6,9}$/;
            if (!regex.test(phone)) {
                $("#dataFormMasterCustomerTel").focus();
                return false;
            } else {
                return true;
            }
        }
        function queryGrid(dg) {//查詢後添加固定條件
            if ($(dg).attr('id') == 'dataGridView') {
                //查詢條件
                var result = [];              
                var SalesTeamID = $('#SalesTeamID_Query').combobox('getValue');//業務單位
                var CustID = $('#CustID_Query').val();//客戶名稱,統一編號,電話,客戶編號	   
                var HunterID = $('#HunterID_Query').combobox('getText');//執案顧問                             
                var bJobCount = $("#bJobCount_Query").checkbox('getValue')//有職缺
                var bContractCount = $("#bContractCount_Query").checkbox('getValue')//有合約

                if (CustID != '') result.push("(c.CustName like '%" + CustID + "%' or c.CustTaxNo like '%" + CustID + "%' or c.CustomerTelArea+c.CustomerTel like '%" + CustID + "%' or c.CustID like '%" + CustID + "%')");
                if (SalesTeamID != '') result.push("c.SalesTeamID = " + SalesTeamID);
                if (HunterID != '') result.push("dbo.funReturnHUT_CustomersHunterName(c.CustID) like '%" + HunterID + "%'");
                if (bJobCount == 1) result.push("(select count(*) from HUT_JobTemp j where CustID=c.CustID and iDate=1)>0");
                if (bContractCount == 1) result.push("dbo.funReturnCustomerContract(c.CustID)!=''");

                $(dg).datagrid('setWhere', result.join(' and '));
            }
        }

        function OnLoadDF() {
            RefreshContactLogs();
            //if (getEditMode($("#dataFormMaster")) == "inserted") {//唯讀
            //    $("#dataFormMasterCustName").prop('readonly', false);//客戶名稱
            //    $("#dataFormMasterCustomerTelArea").prop('readonly', false);//電話
            //    $("#dataFormMasterCustomerTel").prop('readonly', false);//電話
            //    $("#dataFormMasterCustTaxNo").prop('readonly', false);//統一編號	
            //    $("#dataFormMasterCustomerAddress").prop('readonly', false);//地址
            //    $("#dataFormMasterSalesTeamID").combobox("enable");//業務單位
            //    $("#dataFormMasterSalesID").combobox("enable");//獵才業務
            //    $("#dataFormMasterHunterID").combobox("enable");//執案顧問    

            //} else {//取消唯讀
            //$('#dataFormMasterCustName').prop('readonly', true);//客戶名稱
            //$("#dataFormMasterContactContryArea").combobox("disable");//國碼
            //$('#dataFormMasterCustomerTelArea').prop('readonly', true);//電話
            //$('#dataFormMasterCustomerTel').prop('readonly', true);//電話
            //$('#dataFormMasterAddr_Country').combobox("disable");//客戶縣市
            //$('#dataFormMasterAddr_City').combobox("disable");//鄉鎮區
            //$('#dataFormMasterCustomerAddress').prop('readonly', true);//地址
            //$('#dataFormMasterCustTaxNo').prop('readonly', true);//統一編號
                //$("#dataFormMasterSalesID").combobox("disable");
                //$("#dataFormMasterHunterID").combobox("disable");
            //}
        }
              
        //--------------------------聯繫紀錄-----------------------------------
        //function ContactDateLink(value, row, index) {
            //if (value != null)
            //    return "<a href='javascript: void(0)' onclick='LinkContactDate(" + index + ");' style='color:red;'>" + value + "</a>";
            //else
            //    return "<a href='javascript: void(0)' onclick='LinkContactDate(" + index + ");' style='color:red;'>新增</a>";
        //}
        //// open聯繫維護紀錄畫面 dialog
        //function LinkContactDate(index) {
        //    $("#dataGridView").datagrid('selectRow', index);        
        //    openForm('#Dialog_ContactRecord', $('#dataGridView').datagrid('getSelected'), "updated", 'dialog');
        //}
        //更新客戶聯繫紀錄
        function RefreshContactLogs() {
            var CustID = $("#dataFormMasterCustID").val();
            $("#DGContactRecord").datagrid('setWhere', "CustomerID='" + CustID + "'");
        }
        //聯絡人過濾客戶
        function OnLoadContactRecord() {
            var CustID = $("#dataFormMasterCustID").val();
            $('#DFContactRecordDialogue').refval('setWhere', "CustID='" + CustID + "'");            

        }
        //完整顯示Grid聯繫紀錄
        function ShowAllGrid(value) {
            return "<p style='margin:0px;word-wrap:break-word;white-space: normal'>" + value + "</p>";
        }
        //聯繫維護紀錄有變更時重整
        function OnAppliedContactRecord() {
            //$("#dataGridView").datagrid('reload');
            $("#DGContactRecord").datagrid('reload');
        }
        function OnDeletedContactRecord() {
            //$("#dataGridView").datagrid('reload');
            $("#DGContactRecord").datagrid('reload');
        }
        function sCheckBox(val) {
            if (val)
                return "<input  type='checkbox' checked='true' checked onclick='return false;' />";
            else
                return "<input  type='checkbox' onclick='return false;' />";
        }
        function GetCustomerID() {
            var CustID = $("#dataFormMasterCustID").val();
            return CustID;
        }
        //控制是否可以閱覽遮蔽內容=> 用業務單位控管
        function ContactViewRow(rowData) {
            var FormName = '#DFContactRecordShadeContactDescr';
            var userid = getClientInfo("userid");
            if (GetSalesTeamID() == rowData.SalesTeamID || rowData.CreateBy == userid) {
                $(FormName).closest('td').prev('td').show();
                $(FormName).closest('td').show();
            } else {
                $(FormName).closest('td').prev('td').hide();
                $(FormName).closest('td').hide();
            }

            //取得
            //if (rowData.IsShade == true) {
            //    var userid = getClientInfo("userid");
            //    //找不到則返回 -1
            //    if (rowData.ShareTo.indexOf(userid) == -1 && rowData.CreateBy != userid) {
            //        alert('目前無權限！');
            //        return false; //取消編輯的動作 
            //    }
            //}
        }
        //取得登入者業務單位
        function GetSalesTeamID() {
            var SalesTeamID;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sCustomersJobs.HUT_Customer', //連接的Server端，command
                data: "mode=method&method=" + "ReturnSalesTeamID" + "&parameters=",  //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    SalesTeamID = data
                },
            });
            return SalesTeamID;
        }
        function ContactInsertRow() {
            var FormName = '#DFContactRecordShadeContactDescr';
            $(FormName).closest('td').prev('td').show();
            $(FormName).closest('td').show();
        }
        //控制是否可以修改 
        function ContactUpdateRow(rowData) {
            var userid = getClientInfo("userid");
            if (rowData.CreateBy != userid) {
                alert('建立者才可編輯！');
                return false; //取消編輯的動作 
            } else {
                var FormName = '#DFContactRecordShadeContactDescr';
                $(FormName).closest('td').prev('td').show();
                $(FormName).closest('td').show();
            }
        }
        
        //控制是否可以刪除 
        function ContactDeleteRow(rowData) {
            var userid = getClientInfo("userid");
            if (rowData.CreateBy != userid) {
                alert('建立者才可刪除！');
                return false; //取消編輯的動作 
            }
        }
        //--------------------------聯絡人紀錄-----------------------------------            
        function OnLoadSuccessContactPerson() {
            //清空選擇
            if ($('#DFContactPersonContactMobile1Area').combobox('getValue') == "") {
                $('#DFContactPersonContactMobile1Area').combobox('setValue', "");
            }
            if ($('#DFContactPersonContactMobile2Area').combobox('getValue') == "") {
                $('#DFContactPersonContactMobile2Area').combobox('setValue', "");
            }
            if ($('#DFContactPersonContactProperty').combobox('getValue') == "") {//屬性
                $('#DFContactPersonContactProperty').combobox('setValue', "");
            }
            //個人資料頁籤文字改成黑色
            var HideFieldName = ['ContactMobile1Area'];
            var FormName = '#DFContactPerson';

            $.each(HideFieldName, function (index, fieldName) {
                $(FormName + fieldName).closest('td').prev('td').css("color", "black");                
            });

        }

        //聯繫維護紀錄有變更時重整
        function OnAppliedContactPerson() {
            //$("#dataGridView").datagrid('reload');
            $("#DGContactPerson").datagrid('reload');

        }
        //預設值- 電話
        function CountyAreaDefault() {
            return $("#dataFormMasterContactContryArea").combobox('getValue');
        }
        function ContactTelAreaDefault() {
            return $("#dataFormMasterCustomerTelArea").val();
        }
        function ContactTelDefault() {
            return $("#dataFormMasterCustomerTel").val();
        }
        //聯絡人, 手機1=> 前4 - 後9
        function CheckContactMobile1(phone) {
            var regex = /^\d{2,4}-\d{6,9}$/;
            if (!regex.test(phone)) {
                $("#DFContactPersonContactMobile1").focus();
                return false;
            } else {
                return true;
            }
        }
        //聯絡人,手機2=> 前4 - 後9
        function CheckContactMobile2(phone) {
            //var regex = /^09\d{2}-\d{6}$/;
            var regex = /^\d{2,4}-\d{6,9}$/;
            if (!regex.test(phone)) {
                $("#DFContactPersonContactMobile2").focus();
                return false;
            } else {
                return true;
            }
        }
        //聯絡人,電話區碼 =>2-4
        function CheckContactTelArea(phone) {
            var regex = /^\d{2,4}$/;
            if (!regex.test(phone)) {
                $("#DFContactPersonContactTelArea").focus();
                return false;
            } else {
                return true;
            }
        }
        //聯絡人,電話 =>6-9
        function CheckContactTel(phone) {
            var regex = /^\d{6,9}$/;
            if (!regex.test(phone)) {
                $("#DFContactPersonContactTel").focus();
                return false;
            } else {
                return true;
            }
        }
        //-------------------------檔案管理-----------------------------------
        function OnApplyCustomerFile() {            
            if ($("#infoFileUploadDFCustomerFileCustFile").val() == "") {
                 alert('請選擇檔案！');
                 return false;
             }            
        }
        function OnAppliedCustomerFile() {
            //alert('請選擇檔案！');
            $("#DGCustomerFile").datagrid('reload');
        }
        //刪除檔案
        function DeleteCustFile() {
            var row = $('#DGCustomerFile').datagrid('getSelected'); //取得當前主檔中選中的那個Data
            if (row != null) {
                var cnt;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sCustomersJobs.HUT_Customer', //連接的Server端，command
                    data: "mode=method&method=" + "DelCustomerFile" + "&parameters=" + row.AutoKey + "," + row.CustFile, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != false) {
                            cnt = $.parseJSON(data);
                        }
                    }
                });
                alert(cnt);
                if (cnt == "0") {
                    $("#DGCustomerFile").datagrid('reload');
                }
                else {
                    alert('此客戶檔案不存在!');
                    return false;
                }
            } 
        }
        function OnDeletFile() {
            var pre = confirm("確認刪除?");
            if (pre == true) {
                //callServerMethod
                DeleteCustFile();
                return false; //取消刪除的動作
            }
            else {
                return false;
            }

        }
        

        //------------------------合約紀錄-----------------------------------------------        
        //function ContractLink(value, row, index) {
        //    var at = row.ActiveContract;
        //    if (value == "0") {
        //        return "";
        //    }
        //    else if (at == "0") {
        //        return "無效";
        //    } else  {
        //        return "有效";
        //    }
        //}        

        //焦點欄位變顏色
        $(function () {
            $("input, select, textarea").focus(function () {
                $(this).css("background-color", "yellow");
            });

            $("input, select, textarea").blur(function () {
                $(this).css("background-color", "white");
            });
        });
        function genCheckBox(val) {
            if (val)
                return "<input  type='checkbox' checked='true' checked onclick='return false;' />";
            else
                return "<input  type='checkbox'  onclick='return false;'  />";
        }
      
       
        function MasterGridReload() {
            //再打開一次網頁---------------------------------------------------------------------------------------
            //if (getEditMode($("#dataFormMaster")) == 'updated') {
            //    openForm('#JQDialog1', $('#dataGridView').datagrid('getSelected'), "updated", 'dialog');
            //} else {
            //    //reload
            //    queryGrid('#dataGridView');
            //}
            //新增後開啟
            //queryGrid('#dataGridView');
            //setTimeout(function () {
            //    openForm('#JQDialog1', $('#dataGridView').datagrid('getSelected'), "updated", 'dialog');
            //}, 500);
            queryGrid('#dataGridView');
        }
       //檢查客戶記錄是否可刪除
        function CheckDelCustomer() {
            var row = $('#dataGridView').datagrid('getSelected'); //取得當前主檔中選中的那個Data
            var cnt;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sCustomersJobs.HUT_Customer', //連接的Server端，command
                data: "mode=method&method=" + "CheckDelCustomer" + "&parameters=" + row.CustID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
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
                alert('此客戶已有合約使用,無法刪除!!');
                return false;
            }
        }

        //---------------呼叫開啟Job Tab--------------------------------------------------------------------------------
        function OpenJobTab(value, row, index) {            
           return "<a href='javascript: void(0)' onclick='LinkJobTab(" + index + ");' style='color:red;'>" + value + "</a>";            
        }        
        function LinkJobTab(index) {
            $("#dataGridView").datagrid('selectRow', index);
            var rows = $("#dataGridView").datagrid('getSelected');
            var CustID = rows.CustID;
            parent.addTab('職缺資料維護', './JB_ADMIN/JBHunter_Jobs.aspx?CustID=' + CustID + '&iType=1');//開缺數
        }
        function OpenJobTab2(value, row, index) {
            if (value == "0") {
                return value;
            } else {
                return "<a href='javascript: void(0)' onclick='LinkJobTab2(" + index + ");' style='color:blue;'>" + value + "</a>";
            }
        }
        function LinkJobTab2(index) {
            $("#dataGridView").datagrid('selectRow', index);
            var rows = $("#dataGridView").datagrid('getSelected');
            var CustID = rows.CustID;
            parent.addTab('職缺資料維護', './JB_ADMIN/JBHunter_Jobs.aspx?CustID=' + CustID + '&iType=2');//關缺數
        }
        //--------------Domain 報表-----------------------------------------------------------------------------------------------       
        function OpenDomain() {
            var rows = $("#dataGridView").datagrid('getSelected');
            var CustID = rows.CustID;
            var CustName = rows.CustName;

            var url = "../JB_ADMIN/REPORT/JBHunter/CustDomainReport.aspx?CustID=" + CustID + "&CustName=" + CustName;

            var height = $(window).height() - 50;
            var height2 = $(window).height() - 90;
            var width = $(window).width() - 390;
            var dialog = $('<div/>')
            .dialog({
                draggable: false,
                modal: true,
                height: height,
                //top:0,
                width: width,
                title: "Domain-Report",
                //maximizable: true                              
            });
            $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="98%"></iframe>').appendTo(dialog.find('.panel-body'));
            dialog.dialog('open');

        }
        //Grid 同步人脈顯示
        function sCheckBoxCON_CONTACT(val) {
            if (val != null)
                return "<input  type='checkbox' checked='true' checked onclick='return false;' />";
            else
                return "<input  type='checkbox' onclick='return false;' />";
        }
        //Grid 同步人脈連結
        function ContactLink(value, row, index) {
            var CONTACT_ID = row.CONTACT_ID;
            if (CONTACT_ID == null) {
                return $('<a>', { href: 'javascript:void(0)', name: 'Link', onclick: 'OpenSyncContact(' + index + ')' }).linkbutton({ plain: false, text: '同步人脈' })[0].outerHTML;
            } else "已同步";
        }
        function OpenSyncContact(index) {
            $("#DGContactPerson").datagrid('selectRow', index);
            openForm('#Dialog_CON_CONTACT', $('#DGContactPerson').datagrid('getSelected'), "updated", 'dialog');

            //SyncContact(ConType, rows.ContactName, rows.CustID, 40);//40中高階

        }
        //同步聯絡人 傳入參數:Conb 聯絡人姓名,CustNO 客戶代號,ConType 聯絡人項次 1,2
        function SyncContact() {
            var rows = $("#DGContactPerson").datagrid('getSelected');
            var ConType = 2;
            var CTCONTACT_CNAME = $("#DFCON_CONTACTCONTACT_CNAME").val();
            var CONTACT_ENAME = $("#DFCON_CONTACTCONTACT_ENAME").val();
            var CONTACT_GENDER = $("#DFCON_CONTACTCONTACT_GENDER").combobox('getValue');
            var ContactTitle = $("#DFCON_CONTACTContactTitle").val();
            var ContactDept = $("#DFCON_CONTACTContactDept").val();
            var CONTACT_TEL = $("#DFCON_CONTACTCONTACT_TEL").val();
            var TelExt = $("#DFCON_CONTACTContactTelExt").val();
            var ContacteMail = $("#DFCON_CONTACTContacteMail").val();
            var ContactMobile1 = $("#DFCON_CONTACTContactMobile1").val();
            var ContactMobile2 = $("#DFCON_CONTACTContactMobile2").val();
            var CONTACT_TRADE = $("#DFCON_CONTACTCONTACT_TRADE").combobox('getValue');//行業別
            var CONTACT_TRADENOTES = $("#DFCON_CONTACTCONTACT_TRADENOTES").val();
            var CustID = rows.CustID;
            var AutoKey = rows.AutoKey;

            if (CTCONTACT_CNAME == "" || ContactTitle == "" || CONTACT_GENDER == "" || CONTACT_TRADE == "") {
                alert("請填寫姓名/職稱/性別/行業別。");
                return true;
            }

            //檢查必填
            //var  a =submitForm($('#Dialog_CON_CONTACT'));
            //alert(a);
            var ConfirmYN = confirm("確定要同步聯絡人:(" + CTCONTACT_CNAME + ')?');
            if (ConfirmYN == false) {
                return true;
            }
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sCustomersJobs.HUT_Customer',
                data: "mode=method&method=" + "procSyncContact" + " &parameters=" + CTCONTACT_CNAME + "," + CONTACT_ENAME + "," + CONTACT_GENDER + "," + ContactTitle + "," + ContactDept + "," + CONTACT_TEL + "," + TelExt + "," + ContacteMail + "," + ContactMobile1 + "," + ContactMobile2 + "," + CustID + "," + CONTACT_TRADE + "," + CONTACT_TRADENOTES + "," + AutoKey,
                cache: false,
                async: false,
                success: function (data) {
                    alert("同步聯絡人成功");
                    closeForm('#Dialog_CON_CONTACT');
                    $('#DGContactPerson').datagrid("reload");


                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                    alert("注意!! 同步聯絡人失敗");
                }

            });
        }


    </script>
    <style type="text/css">
        .auto-style1 {
            width: 1134px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td colspan="2" class="auto-style1">
                        <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
                        <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sCustomersJobs.HUT_Customer" runat="server" AutoApply="True" 
                            DataMember="HUT_Customer" Pagination="True" QueryTitle="查詢條件" EditDialogID="JQDialog1"
                            Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" CheckOnSelect="True" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="15,30,45,60,65" PageSize="15" QueryAutoColumn="False" QueryLeft="10px" QueryMode="Panel" QueryTop="10px" RecordLock="False" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnDelete="CheckDelCustomer" ColumnsHibeable="False" RecordLockMode="None" BufferView="False" NotInitGrid="False" RowNumbers="True" Width="1050px">
                            <Columns>
<%--                                 <JQTools:JQGridColumn Alignment="center" Caption="最後聯繫日" Editor="datebox" FieldName="ContactDate" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70" FormatScript="" Format="yyyy/mm/dd" />--%>
                                <JQTools:JQGridColumn Alignment="center" Caption="客戶編號" Editor="text" FieldName="CustID" MaxLength="20" Width="55" EditorOptions="" Visible="True" Sortable="False" />
                                <JQTools:JQGridColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustName" MaxLength="30" Width="186" Sortable="True" />
                                <JQTools:JQGridColumn Alignment="center" Caption="統一編號" Editor="text" FieldName="CustTaxNo" MaxLength="0" Width="65" />
                                <JQTools:JQGridColumn Alignment="left" Caption="客戶電話" Editor="text" FieldName="CustTel" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="100" />
                                <JQTools:JQGridColumn Alignment="center" Caption="開缺數" Editor="text" FieldName="JobOCount" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50" FormatScript="OpenJobTab" />
                                <JQTools:JQGridColumn Alignment="center" Caption="關缺數" Editor="text" FieldName="JobCCount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50" FormatScript="OpenJobTab2">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="合約區間" Editor="text" EditorOptions="" FieldName="sContract" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="180" Format="" FormatScript="ShowAllGrid" />
                                <JQTools:JQGridColumn Alignment="left" Caption="執案顧問" Editor="text" EditorOptions="" FieldName="sHunterName" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="110" FormatScript="ShowAllGrid" />
                                <JQTools:JQGridColumn Alignment="center" Caption="更新日期" Editor="datebox" FieldName="LastUpdateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70" Format="yyyy/mm/dd" FormatScript="">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="更新人員" Editor="text" EditorOptions="" FieldName="LastUpdateBy" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="65" />
                            </Columns>
                            <TooItems>
<%--                                <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增客戶" />  --%>
<%--                                <JQTools:JQToolItem ID="JQToolItem2" Icon="icon-print" ItemType="easyui-linkbutton" OnClick="OpenDomain" Text="Domain"  />--%>
                            </TooItems>
                            <QueryColumns>
                                <JQTools:JQQueryColumn Caption="客戶名稱、統編、電話" Condition="%%" DataType="string" Editor="text" FieldName="CustID" NewLine="False" RemoteMethod="False" Width="120" AndOr="" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="業務單位" Condition="=" DataType="string" DefaultValue="" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'SalesTeamName',remoteName:'sSalesTeam.HUT_SalesTeam',tableName:'HUT_SalesTeam',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTeamID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="115" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="執案顧問" Condition="=" DataType="string" DefaultValue="" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sCustomersJobs.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="HunterID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="105" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="有效職缺" Condition="%" DataType="string" Editor="checkbox" FieldName="bJobCount" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="30" />
                                <JQTools:JQQueryColumn AndOr="and" Caption="有效合約" Condition="%" DataType="string" Editor="checkbox" FieldName="bContractCount" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="30" />
                            </QueryColumns>
                        </JQTools:JQDataGrid>
                        <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="客戶資料" DialogLeft="2px" DialogTop="1px" Width="1050px" Wrap="False" EditMode="Dialog">
                            <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="HUT_Customer" HorizontalColumnsCount="9" RemoteName="sCustomersJobs.HUT_Customer" Closed="False" ContinueAdd="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" OnApplied="MasterGridReload" disapply="False" IsRejectON="False"  IsAutoPause="False" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" ParentObjectID="" ChainDataFormID="" OnLoadSuccess="OnLoadDF">
                                <Columns>
                                    <JQTools:JQFormColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustName" MaxLength="128" Span="2" Width="200" NewRow="False" ReadOnly="True" RowSpan="1" Visible="True" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="國碼" Editor="infocombobox" EditorOptions="valueField:'ContryAreaID',textField:'sAreaID',remoteName:'sCustomersJobs.HUT_ContryArea',tableName:'HUT_ContryArea',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ContactContryArea" MaxLength="0" Span="3" Width="120" NewRow="False" ReadOnly="True" Visible="True" />
                                    <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="CustomerTelArea" MaxLength="0" Width="38" Span="1" RowSpan="1" ReadOnly="True" />
                                    <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="CustomerTel" MaxLength="20" Width="80" Span="1" ReadOnly="True" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="客戶編號" Editor="text" FieldName="CustID" MaxLength="20" NewRow="False" ReadOnly="True" Span="1" Width="90" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="客戶縣市" Editor="infocombobox" FieldName="Addr_Country" Format="" maxlength="0" Width="120" EditorOptions="valueField:'Country',textField:'Country',remoteName:'sERP_Customer_Normal_Customer.Country',tableName:'Country',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:dataFormMasterAddr_Country_OnSelect,panelHeight:200" ReadOnly="True" NewRow="True" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="鄉鎮區" Editor="infocombobox" FieldName="Addr_City" Format="" maxlength="0" Width="120" NewRow="False" RowSpan="1" Span="1" EditorOptions="valueField:'City',textField:'City',remoteName:'sERP_Customer_Normal_Customer.City',tableName:'City',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:dataFormMasterAddr_City_OnSelect,panelHeight:200" ReadOnly="True" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="地址" Editor="text" FieldName="CustomerAddress" MaxLength="128" NewRow="False" ReadOnly="True" Span="5" Visible="True" Width="280" />
                                     <JQTools:JQFormColumn Alignment="left" Caption="更新日期" Editor="text" EditorOptions="" FieldName="LastUpdateDate" Format="yyyy/mm/dd" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="90" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="更新人員" Editor="text" FieldName="LastUpdateBy" MaxLength="20" ReadOnly="True" Visible="True" Width="75" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="統一編號" Editor="text" FieldName="CustTaxNo" MaxLength="10" NewRow="True" ReadOnly="True" Span="1" Visible="True" Width="110" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="業務單位" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'SalesTeamName',remoteName:'sSalesTeam.HUT_SalesTeam',tableName:'HUT_SalesTeam',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTeamID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="產業別" Editor="inforefval" FieldName="CategoryText" MaxLength="0" NewRow="False" ReadOnly="True" Span="5" Visible="True" Width="280" EditorOptions="title:'請輸入關鍵字後滑鼠離開焦點可做搜尋',panelWidth:650,panelHeight:310,remoteName:'sHUTUser.infoUserCareerCategory',tableName:'infoUserCareerCategory',columns:[{field:'CategoryName',title:'產業類別',width:615,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'CategoryID',textField:'LastCategoryName',valueFieldCaption:'CategoryID',textFieldCaption:'LastCategoryName',cacheRelationText:false,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="網址" Editor="text" FieldName="CustomerUrl" MaxLength="256" Span="2" Visible="True" Width="250" ReadOnly="True" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" MaxLength="20" Width="180" Span="1" Visible="False"/>
                                    <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" MaxLength="0" Width="180" Span="1" Visible="False" EditorOptions="dateFormat:'datetime',showTimeSpinner:true,showSeconds:true" NewRow="False" ReadOnly="False" RowSpan="1" Format=""/>
                                </Columns>

                            </JQTools:JQDataForm>

                            <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                                <Columns>
                                    <JQTools:JQDefaultColumn DefaultValue="自動編號" FieldName="CustID" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn DefaultValue="http://" FieldName="CustomerUrl" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn DefaultValue="_username" FieldName="LastUpdateBy" RemoteMethod="True" />
                                </Columns>
                            </JQTools:JQDefault>
                            <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                                <Columns>
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="CustName" RemoteMethod="True" ValidateMessage="" ValidateType="None" />
                                </Columns>
                            </JQTools:JQValidate>
                          
                                
                                     <JQTools:JQDataGrid ID="DGContactPerson" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="False" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="HUT_CustomerContactPerson" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialogContactPerson" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" OnUpdate="" PageList="10,20,30,40,50" PageSize="10" Pagination="True" ParentObjectID="dataFormMaster" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Panel" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" RemoteName="sCustomersJobs.HUT_Customer" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                                         <Columns>
                                             <JQTools:JQGridColumn Alignment="center" Caption="屬性" Editor="infocombobox" EditorOptions="items:[{value:'2',text:'主要',selected:'false'},{value:'1',text:'次要',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="ContactProperty" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="聯絡人" Editor="text" FieldName="ContactName" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="110">
                                             </JQTools:JQGridColumn>
<%--                                             <JQTools:JQGridColumn Alignment="center" Caption="轉入人脈" Editor="checkbox" FieldName="CONTACT_ID" FormatScript="sCheckBoxCON_CONTACT" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="58">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="center" Caption=" " Editor="text" FieldName="ContactLink" FormatScript="ContactLink" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="89">
                                             </JQTools:JQGridColumn>--%>
                                             <JQTools:JQGridColumn Alignment="left" Caption="職稱" Editor="text" FieldName="ContactTitle" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="130">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="部門" Editor="text" FieldName="ContactDept" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="95">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="right" Caption="國碼" Editor="infocombobox" EditorOptions="valueField:'ContryAreaID',textField:'ContryAreaID',remoteName:'sCustomersJobs.HUT_ContryArea',tableName:'HUT_ContryArea',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ContactCountyArea" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="center" Caption="區碼" Editor="text" FieldName="ContactTelArea" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="35">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="電話" Editor="text" FieldName="ContactTel" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="63">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="分機" Editor="text" FieldName="ContactTelExt" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="35">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="right" Caption="國碼" Editor="infocombobox" FieldName="ContactMobile1Area" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40" EditorOptions="valueField:'ContryAreaID',textField:'ContryAreaID',remoteName:'sCustomersJobs.HUT_ContryArea',tableName:'HUT_ContryArea',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="手機1" Editor="text" FieldName="ContactMobile1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="82">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="right" Caption="國碼" Editor="infocombobox" FieldName="ContactMobile2Area" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="40" EditorOptions="valueField:'ContryAreaID',textField:'ContryAreaID',remoteName:'sCustomersJobs.HUT_ContryArea',tableName:'HUT_ContryArea',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="center" Caption="狀態" Editor="text" FieldName="ContactStatusName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="center" Caption="更新日期" Editor="text" FieldName="UpdateDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="60">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="center" Caption="更新人員" Editor="text" FieldName="UpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="55">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="CustID" Editor="text" FieldName="CustID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="ContactStatus" Editor="text" FieldName="ContactStatus" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="ContIMType1" Editor="text" FieldName="ContIMType1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="ContIMNO1" Editor="text" FieldName="ContIMNO1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="ContIMType2" Editor="text" FieldName="ContIMType2" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="ContIMNO2" Editor="text" FieldName="ContIMNO2" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="ContacteMail" Editor="text" FieldName="ContacteMail" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="ContacteNotes" Editor="text" FieldName="ContacteNotes" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="ContactMobile2" Editor="text" FieldName="ContactMobile2" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="ContactGender" Editor="text" FieldName="ContactGender" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                             </JQTools:JQGridColumn>
                                         </Columns>
                                         <RelationColumns>
                                             <JQTools:JQRelationColumn FieldName="CustID" ParentFieldName="CustID" />
                                         </RelationColumns>
                                         <TooItems>
                                             <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增聯絡人" />
                                         </TooItems>
                            </JQTools:JQDataGrid>
                            <JQTools:JQDialog ID="JQDialogContactPerson" runat="server" BindingObjectID="DFContactPerson" DialogLeft="80px" DialogTop="80px" Title="聯絡人維護" Width="850px">
                                <JQTools:JQDataForm ID="DFContactPerson" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="HUT_CustomerContactPerson" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="4" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnApplied="OnAppliedContactPerson" OnLoadSuccess="OnLoadSuccessContactPerson" ParentObjectID="dataFormMaster" RemoteName="sCustomersJobs.HUT_Customer" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                                    <Columns>
                                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" Span="1" Visible="False" Width="80" ReadOnly="False" RowSpan="1" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="CustID" Editor="text" FieldName="CustID" MaxLength="0" NewRow="False" Span="1" Visible="False" Width="80" ReadOnly="False" RowSpan="1" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人" Editor="text" FieldName="ContactName" MaxLength="20" NewRow="False" Span="4" Visible="True" Width="125" />
                                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" EditorOptions="items:[{value:'0',text:'女',selected:'false'},{value:'1',text:'男',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:100" FieldName="ContactGender" MaxLength="0" NewRow="True" Span="1" Visible="True" Width="72" />
                                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContactTitle" MaxLength="100" NewRow="False" Span="1" Visible="True" Width="155" />
                                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContactDept" MaxLength="0" NewRow="False" Span="1" Visible="True" Width="150" RowSpan="1" />
                                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" EditorOptions="items:[{value:'2',text:'主要',selected:'false'},{value:'1',text:'次要',selected:'false'}],checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ContactProperty" MaxLength="0" NewRow="False" Span="1" Visible="True" Width="80" RowSpan="1" ReadOnly="False" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="國碼" Editor="infocombobox" FieldName="ContactCountyArea" MaxLength="0" NewRow="False" Span="4" Visible="True" Width="100" EditorOptions="valueField:'ContryAreaID',textField:'sAreaID',remoteName:'sCustomersJobs.HUT_ContryArea',tableName:'HUT_ContryArea',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContactTelArea" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="39" />
                                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContactTel" MaxLength="20" NewRow="False" Visible="True" Width="78" />
                                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContactTelExt" MaxLength="10" NewRow="False" Visible="True" Width="50" ReadOnly="False" RowSpan="1" Span="1" />
                                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContacteMail" MaxLength="128" NewRow="False" Span="1" Width="330" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="國碼" Editor="infocombobox" EditorOptions="valueField:'ContryAreaID',textField:'sAreaID',remoteName:'sCustomersJobs.HUT_ContryArea',tableName:'HUT_ContryArea',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ContactMobile1Area" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="100" />
                                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContactMobile1" MaxLength="20" NewRow="True" OnBlur="" Span="1" Width="105" />
                                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" EditorOptions="valueField:'ContryAreaID',textField:'sAreaID',remoteName:'sCustomersJobs.HUT_ContryArea',tableName:'HUT_ContryArea',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ContactMobile2Area" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContactMobile2" MaxLength="0" NewRow="False" Span="1" Width="105" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="即時通1" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'IMNAME',remoteName:'sCustomersJobs.HUT_ZIMType',tableName:'HUT_ZIMType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:150" FieldName="ContIMType1" MaxLength="20" NewRow="True" Span="3" Width="90" Visible="True" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="狀態" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'ContactStatusName',remoteName:'sCustomersJobs.HUT_CustomerContactStatus',tableName:'HUT_CustomerContactStatus',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="ContactStatus" MaxLength="0" NewRow="False" Span="1" Visible="True" Width="80" ReadOnly="False" RowSpan="1" />

                                        <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="textarea" EditorOptions="height:80" FieldName="ContacteNotes" MaxLength="256" NewRow="True" ReadOnly="False" RowSpan="1" Span="5" Visible="True" Width="700" />
                                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContIMNO1" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="136" />
                                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" EditorOptions="valueField:'ID',textField:'IMNAME',remoteName:'sCustomersJobs.HUT_ZIMType',tableName:'HUT_ZIMType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:150" FieldName="ContIMType2" MaxLength="20" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContIMNO2" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="136" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="UpdateBy" Editor="text" FieldName="UpdateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="UpdateDate" Editor="text" FieldName="UpdateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                    </Columns>
                                    <RelationColumns>
                                        <JQTools:JQRelationColumn FieldName="CustID" ParentFieldName="CustID" />
                                    </RelationColumns>
                                </JQTools:JQDataForm>
                            </JQTools:JQDialog>
                            <JQTools:JQDefault ID="JQDefault3" runat="server" BindingObjectID="DFContactPerson" EnableTheming="True">
                                <Columns>
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERCODE" FieldName="UserID" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERNAME" FieldName="UpdateBy" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="UpdateDate" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="ContactStatus" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="ContactTelAreaDefault" FieldName="ContactTelArea" RemoteMethod="False" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="ContactTelDefault" FieldName="ContactTel" RemoteMethod="False" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="CountyAreaDefault" FieldName="ContactCountyArea" RemoteMethod="False" />
                                </Columns>
                            </JQTools:JQDefault>
                            <JQTools:JQValidate ID="JQValidate3" runat="server" BindingObjectID="DFContactPerson" EnableTheming="True">
                                <Columns>
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="ContactName" RemoteMethod="True" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="ContactTitle" RemoteMethod="True" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckMethod="CheckContactMobile1" CheckNull="False" FieldName="ContactMobile1" RemoteMethod="False" ValidateMessage="格式不對！" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckMethod="CheckContactMobile2" CheckNull="False" FieldName="ContactMobile2" RemoteMethod="False" ValidateMessage="格式不對！" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="ContactTel" RemoteMethod="False" ValidateMessage="電話不可空白！" ValidateType="None" CheckMethod="CheckContactTel" />
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="ContactTelArea" RemoteMethod="False" ValidateMessage="電話不可空白！" ValidateType="None" CheckMethod="CheckContactTelArea" />
                                </Columns>
                            </JQTools:JQValidate>
                                                                
                                     <JQTools:JQAutoSeq ID="JQAutoSeq1" runat="server" BindingObjectID="DFContactPerson" FieldName="AutoKey" NumDig="1" />                                

                                     <JQTools:JQDataGrid ID="DGContactRecord" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="HUT_CustomerContactRecord" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialogContactRecord" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sCustomersJobs.HUT_CustomerContactRecord" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnDeleted="OnDeletedContactRecord" data-options="pagination:true,view:commandview" OnUpdate="ContactUpdateRow" OnDelete="ContactDeleteRow" OnView="ContactViewRow" OnInsert="ContactInsertRow">
                                         <Columns>
                                             <JQTools:JQGridColumn Alignment="center" Caption="聯繫日期" Editor="datebox" FieldName="ContactDate" Format="yyyy/mm/dd" MaxLength="0" ReadOnly="False" Width="80" Frozen="False" IsNvarChar="False" QueryCondition="" Sortable="True" Visible="True">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="聯絡人" Editor="text" FieldName="Dialogue" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" EditorOptions="">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="聯繫內容" Editor="textarea" FieldName="ContactDescr" FormatScript="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="230">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="遮蔽內容" Editor="textarea" FieldName="ShadeContactDescr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="350">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="center" Caption="更新日期" Editor="text" FieldName="CreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="60" Format="yyyy/mm/dd">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="center" Caption="更新人員" Editor="text" FieldName="CreateByName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="55">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="CustID" Editor="text" FieldName="CustomerID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="UserID" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="left" Caption="SalesKindID" Editor="text" FieldName="SalesKindID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                             </JQTools:JQGridColumn>
                                             <JQTools:JQGridColumn Alignment="center" Caption="遮蔽?" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsShade" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="50" FormatScript="sCheckBox">
                                             </JQTools:JQGridColumn>
                                         </Columns>
                                         <TooItems>
                                             <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增聯繫紀錄" />
                                         </TooItems>
                                     </JQTools:JQDataGrid>
                                     <JQTools:JQDialog ID="JQDialogContactRecord" runat="server" BindingObjectID="DFContactRecord" DialogLeft="120px" DialogTop="50px" Title="聯繫紀錄維護" Width="750px">
                                         <JQTools:JQDataForm ID="DFContactRecord" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="HUT_CustomerContactRecord" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="2" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnApplied="OnAppliedContactRecord" ParentObjectID="" RemoteName="sCustomersJobs.HUT_CustomerContactRecord" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="OnLoadContactRecord">
                                             <Columns>
                                                 <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                                 <JQTools:JQFormColumn Alignment="left" Caption="聯繫日期" Editor="datebox" FieldName="ContactDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                                                 <JQTools:JQFormColumn Alignment="left" Caption="聯絡人" Editor="inforefval" EditorOptions="title:'請選擇',panelWidth:440,remoteName:'sCustomersJobs.HUT_CustomerContactPerson',tableName:'HUT_CustomerContactPerson',columns:[{field:'ContactName',title:'聯絡人',width:120,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactTitle',title:'職稱',width:120,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactDept',title:'部門 ',width:120,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'ContactName',textField:'ContactName',valueFieldCaption:'ContactName',textFieldCaption:'ContactName',cacheRelationText:true,checkData:false,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" FieldName="Dialogue" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="140" />
                                                 <JQTools:JQFormColumn Alignment="left" Caption="CustID" Editor="text" FieldName="CustomerID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                                 <JQTools:JQFormColumn Alignment="left" Caption="聯繫內容" Editor="textarea" EditorOptions="height:110" FieldName="ContactDescr" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="620" />
                                                 <JQTools:JQFormColumn Alignment="left" Caption="遮蔽內容" Editor="textarea" EditorOptions="height:110" FieldName="ShadeContactDescr" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="620" />
                                                 <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="CreateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                                 <JQTools:JQFormColumn Alignment="left" Caption="UpdateBy" Editor="text" FieldName="CreateByName" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                                 <JQTools:JQFormColumn Alignment="left" Caption="UpdateDate" Editor="text" FieldName="CreateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                                 <JQTools:JQFormColumn Alignment="center" Caption="遮蔽?" Editor="checkbox" FieldName="IsShade" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="50" EditorOptions="on:1,off:0" />
                                                 <JQTools:JQFormColumn Alignment="left" Caption="檢視者" Editor="text" FieldName="ShareToName" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="300" />
                                                 <JQTools:JQFormColumn Alignment="left" Caption="ShareTo" Editor="text" FieldName="ShareTo" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                                 <JQTools:JQFormColumn Alignment="left" Caption="SalesKindID" Editor="text" FieldName="SalesKindID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                             </Columns>
                                         </JQTools:JQDataForm>
                                         <JQTools:JQDefault ID="JQDefault2" runat="server" BindingObjectID="DFContactRecord" EnableTheming="True">
                                             <Columns>
                                                 <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="ContactDate" RemoteMethod="True" />
                                                 <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERCODE" FieldName="CreateBy" RemoteMethod="True" />
                                                 <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERNAME" FieldName="CreateByName" RemoteMethod="True" />
                                                 <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                                                 <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="false" FieldName="IsShade" RemoteMethod="True" />
                                                 <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetCustomerID" FieldName="CustomerID" RemoteMethod="False" />
                                                 <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="S05" FieldName="SalesKindID" RemoteMethod="True" />
                                             </Columns>
                                         </JQTools:JQDefault>
                                         <JQTools:JQValidate ID="JQValidate2" runat="server" BindingObjectID="DFContactRecord" EnableTheming="True">
                                             <Columns>
                                                 <JQTools:JQValidateColumn CheckNull="True" FieldName="ContactDate" RemoteMethod="True" ValidateMessage="請選擇聯繫日期！" ValidateType="None" />
                                                 <JQTools:JQValidateColumn CheckNull="True" FieldName="ContactDescr" RemoteMethod="True" ValidateMessage="聯繫內容不可空白！" ValidateType="None" />
                                             </Columns>
                                         </JQTools:JQValidate>
                                         <JQTools:JQAutoSeq ID="JQAutoSeq2" runat="server" BindingObjectID="DFContactRecord" FieldName="AutoKey" NumDig="1" />
                            </JQTools:JQDialog>
                            <JQTools:JQDataGrid ID="DGCustomerFile" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="False" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="HUT_CustomerFile" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialogCustomerFile" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" OnDelete="OnDeletFile" OnUpdate="" PageList="10,20,30,40,50" PageSize="10" Pagination="True" ParentObjectID="dataFormMaster" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Panel" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" RemoteName="sCustomersJobs.HUT_Customer" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="700px">
                                <Columns>
                                    <JQTools:JQGridColumn Alignment="left" Caption="下載檔案" Editor="text" EditorOptions="" FieldName="CustFile" Format="download,folder:Files/Hunter/Customer" MaxLength="150" ReadOnly="False" Width="275">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="center" Caption="Domain檔案" Editor="checkbox" FieldName="bDomain" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="73" FormatScript="sCheckBox">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="center" Caption="Domain建立日期" Editor="datebox" FieldName="DomainDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="95">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="center" Caption="建立日期" Editor="text" FieldName="UpdateDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="center" Caption="建立者" Editor="text" FieldName="UpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="CustID" Editor="text" FieldName="CustID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                    </JQTools:JQGridColumn>
                                </Columns>
                                <RelationColumns>
                                    <JQTools:JQRelationColumn FieldName="CustID" ParentFieldName="CustID" />
                                </RelationColumns>
                                <TooItems>
                                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增檔案(10MB)" />
                                    <JQTools:JQToolItem ID="JQToolItem2" Icon="icon-record" ItemType="easyui-linkbutton" OnClick="OpenDomain" Text="Domain"  />
                                </TooItems>
                            </JQTools:JQDataGrid>
                                     <JQTools:JQDialog ID="JQDialogCustomerFile" runat="server" BindingObjectID="DFCustomerFile" DialogLeft="240px" DialogTop="120px" Title="客戶檔案維護" Width="550px">
                                         <JQTools:JQDataForm ID="DFCustomerFile" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="HUT_CustomerFile" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="1" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnApplied="OnAppliedCustomerFile" ParentObjectID="dataFormMaster" RemoteName="sCustomersJobs.HUT_Customer" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnApply="OnApplyCustomerFile">
                                             <Columns>
                                                 <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" Visible="False" Width="80" Span="1" NewRow="False" ReadOnly="False" RowSpan="1" />
                                                 <JQTools:JQFormColumn Alignment="left" Caption="CustID" Editor="text" FieldName="CustID" MaxLength="0" Visible="False" Width="80" NewRow="False" Span="1" ReadOnly="False" RowSpan="1" />
                                                <JQTools:JQFormColumn Alignment="left" Caption="上傳檔案" Editor="infofileupload" FieldName="CustFile" MaxLength="0" Width="400" Span="1" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" EditorOptions="filter:'docx|doc|xlsx|xls|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf|image|tif|txt',isAutoNum:true,upLoadFolder:'Files/Hunter/Customer',showButton:true,showLocalFile:true,fileSizeLimited:'10000'" />
                                                 <JQTools:JQFormColumn Alignment="center" Caption="Domain檔案" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="bDomain" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="45" />
                                                <JQTools:JQFormColumn Alignment="left" Caption="Domain建立日期" Editor="datebox" FieldName="DomainDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                                                 <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                                 <JQTools:JQFormColumn Alignment="left" Caption="UpdateBy" Editor="text" FieldName="UpdateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                                 <JQTools:JQFormColumn Alignment="left" Caption="UpdateDate" Editor="text" FieldName="UpdateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                             </Columns>
                                             <RelationColumns>
                                                 <JQTools:JQRelationColumn FieldName="CustID" ParentFieldName="CustID" />
                                             </RelationColumns>
                                         </JQTools:JQDataForm>
                                     </JQTools:JQDialog>

                                     <JQTools:JQDefault ID="JQDefault4" runat="server" BindingObjectID="DFCustomerFile" EnableTheming="True">
                                         <Columns>
                                             <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERCODE" FieldName="UserID" RemoteMethod="True" />
                                             <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERNAME" FieldName="UpdateBy" RemoteMethod="True" />
                                             <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="UpdateDate" RemoteMethod="True" />
                                         </Columns>
                                     </JQTools:JQDefault>

                                      <JQTools:JQAutoSeq ID="JQAutoSeq4" runat="server" BindingObjectID="DFCustomerFile" FieldName="AutoKey" NumDig="1" />
                                 
                           

                            <JQTools:JQValidate ID="JQValidateCustomerFile" runat="server" BindingObjectID="DFCustomerFile" EnableTheming="True">
                                <Columns>
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="CustFile" RemoteMethod="True" ValidateMessage="請選擇檔案！" ValidateType="None" />
                                </Columns>
                            </JQTools:JQValidate>
                                 
                           

                            <br />
                                 
                           

                        </JQTools:JQDialog>
        
        

                        <JQTools:JQDialog ID="Dialog_CON_CONTACT" runat="server" BindingObjectID="DFCON_CONTACT" Title="同步人脈資料" DialogLeft="30px" DialogTop="80px" Width="880px" Wrap="False" EditMode="Dialog" ShowSubmitDiv="False">
                            <table style="width:100%;">
                             <tr>
                                 <td style="vertical-align: bottom;"> 
                            <JQTools:JQDataForm ID="DFCON_CONTACT" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="HUT_CustomerContactPerson" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="5" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="dataFormMaster" RemoteName="sCustomersJobs.HUT_Customer" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0">
                                <Columns>
                                    <JQTools:JQFormColumn Alignment=" " Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" Span="1" Visible="False" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="CustID" Editor="text" FieldName="CustID" MaxLength="0" NewRow="False" Span="1" Visible="False" Width="80" ReadOnly="False" RowSpan="1" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="姓名" Editor="text" FieldName="CONTACT_CNAME" MaxLength="20" NewRow="False" Span="1" Visible="True" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="英文名" Editor="text" FieldName="CONTACT_ENAME" MaxLength="0" NewRow="False" Span="2" Visible="True" Width="120" ReadOnly="False" RowSpan="1" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="職稱" Editor="text" FieldName="ContactTitle" MaxLength="100" NewRow="False" Span="1" Visible="True" Width="155" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="部門" Editor="text" FieldName="ContactDept" MaxLength="0" NewRow="False" Span="1" Visible="True" Width="140" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="性別" Editor="infocombobox" EditorOptions="items:[{value:'0',text:'女',selected:'false'},{value:'1',text:'男',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:100" FieldName="CONTACT_GENDER" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="電話" Editor="text" FieldName="CONTACT_TEL" MaxLength="20" NewRow="False" Span="1" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="分機" Editor="text" FieldName="ContactTelExt" MaxLength="10" NewRow="False" Span="1" Width="40" ReadOnly="False" RowSpan="1" Visible="True" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="eMail" Editor="text" FieldName="ContacteMail" MaxLength="128" Width="330" Span="2" ReadOnly="False" Visible="True" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="手機1" Editor="text" FieldName="ContactMobile1" MaxLength="20" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="105" OnBlur="" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="手機2" Editor="text" FieldName="ContactMobile2" MaxLength="0" Span="2" Width="105" ReadOnly="False" Visible="True" NewRow="False" RowSpan="1" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="行業別" Editor="infocombobox" EditorOptions="valueField:'JB_TYPE',textField:'JB_NAME',remoteName:'sCustomersJobs.CONTACT_TRADE',tableName:'CONTACT_TRADE',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="CONTACT_TRADE" MaxLength="0" ReadOnly="False" Visible="True" Width="160" Span="1" NewRow="False" RowSpan="1" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="行業備註" Editor="text" FieldName="CONTACT_TRADENOTES" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="200" />                                    
                                     </Columns>
                                <RelationColumns>
                                    <JQTools:JQRelationColumn FieldName="CustID" ParentFieldName="CustID" />
                                </RelationColumns>
                            </JQTools:JQDataForm>
                                     <JQTools:JQValidate ID="JQValidateContactPerson" runat="server" BindingObjectID="DFCON_CONTACT" EnableTheming="True">
                                         <Columns>
                                             <JQTools:JQValidateColumn CheckNull="True" FieldName="CONTACT_CNAME" RemoteMethod="True" ValidateMessage="請填寫姓名！" ValidateType="None" />
                                             <JQTools:JQValidateColumn CheckNull="True" FieldName="ContactTitle" RemoteMethod="True" ValidateMessage="請填寫職稱！" ValidateType="None" />
                                             <JQTools:JQValidateColumn CheckNull="True" FieldName="CONTACT_GENDER" RemoteMethod="True" ValidateMessage="請選擇性別！" ValidateType="None" />
                                             <JQTools:JQValidateColumn CheckNull="True" FieldName="CONTACT_TRADE" RemoteMethod="True" ValidateMessage="請選擇行業別！" ValidateType="None" />
                                         </Columns>
                                     </JQTools:JQValidate>
                            </td>
                            </tr>
                                <tr>
                                    <td style="vertical-align: bottom; text-align: center;">   
                                        <a href="#" class="easyui-linkbutton" data-options="" onclick="SyncContact()">同步資料</a>
                                     </td>
                                </tr>
                            </table>
                        </JQTools:JQDialog>


                    </td>

                </tr>
            </table>
        </div>
       
       

    </form>
</body>
</html>
