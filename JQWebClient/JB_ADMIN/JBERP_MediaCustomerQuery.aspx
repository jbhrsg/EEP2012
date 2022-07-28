<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_MediaCustomerQuery.aspx.cs" Inherits="Template_JQueryQuery1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
     <script type="text/javascript">
         $(function () {
             $('.infosysbutton-q', '#querydataGridMaster').closest('td').attr({ 'align': 'middle' });
         });

         function queryGrid(dg) {
             var result = [];
             var aVal = '';
             var Filtflag = 0; //查詢條件指標
             if ($(dg).attr('id') == 'dataGridMaster') {
                 aVal = $('#CustNO_Query').combobox('getValue');
                 if (aVal != '') {
                     result.push("A.CustNO = '" + aVal + "'");
                     Filtflag = 1
                 }
                 aVal = $('#CustName_Query').combobox('getValue');
                 if (aVal != '') {
                     result.push("A.CustName like '%" + aVal + "%'");
                     Filtflag = 1;
                 }
                 //aVal = $("input[id='A.SalesID_Query']").combobox('getValue');
                 aVal = $("#SalesID_Query").combobox('getValue');
                 if (aVal != '') {
                     result.push("A.SalesID = '" + aVal + "'");
                     Filtflag = 1;
                 }

                 aVal = $("#iPeopleCount_Query").val();
                 if (aVal != '') {
                     result.push("A.iPeopleCount = '" + aVal + "'");
                     Filtflag = 1;
                 }

                 aVal = $("#iPeopleFCount_Query").val();
                 if (aVal != '') {
                     result.push("A.iPeopleFCount = '" + aVal + "'");
                     Filtflag = 1;
                 }

                 aVal = $("#IndustryID_Query").combobox('getValue');
                 if (aVal != '') {
                     result.push("A.IndustryID = '" + aVal + "'");
                     Filtflag = 1;
                 }

                 aVal = $("#CustAreaID_Query").combobox('getValue');
                 if (aVal != '') {
                     result.push("A.CustAreaID = '" + aVal + "'");
                     Filtflag = 1;
                 }
                 //aVal = $('#DealDays_Query').val();
                 //if (aVal != '') {
                 //    result.push("(dbo.funReturnCustDealDays(A.LatelyDayD)) <= '" + aVal + "'");
                 //    Filtflag = 1;
                 //}

                 //var SalesTypeID = $('#SalesTypeID_Query').combobox('getValue');
                 //var QSDate = '1900/01/01';
                 //var QEDate = '1900/01/01';
                 //if (SalesTypeID != '') {
                 //    Filtflag = 1;
                 //    if ($('#QSDate_Query').datebox('getValue') != '') {
                 //        var QSDate = $.jbjob.Date.DateFormat(new Date($('#QSDate_Query').datebox('getValue')), 'yyyy-MM-dd');
                 //    }
                 //    if ($('#QEDate_Query').datebox('getValue') != '') {
                 //        var QEDate = $.jbjob.Date.DateFormat(new Date($('#QEDate_Query').datebox('getValue')), 'yyyy-MM-dd');
                 //    }
                 //    result.push("A.Custno in (SELECT Custno FROM DBO.funReturnCustBillSalesDate('" + SalesTypeID + "','" + QSDate + "','" + QEDate + "'))");
                 //}

                 var SalesTypeID = $.trim($('#SalesTypeID_Query').combobox('getValue'));
                 var QSDate = $.trim($('#QSDate_Query').datebox('getValue'));
                 var QEDate = $.trim($('#QEDate_Query').datebox('getValue'));
                 if (QSDate != '' || QEDate != '' || SalesTypeID != '') {
                     if (QSDate != '' && QEDate != '' && SalesTypeID != '') {
                         result.push("CustNO in(select distinct CustNO from ERPSALESDETAILS WHERE  SALEsDate >= '" + QSDate + "' and SALEsDate <='" + QEDate + "' and SalesTypeID ='" + SalesTypeID + "' AND IsActive=1)");
                     } else if (QSDate != '' && QEDate == '' && SalesTypeID == '') {
                         result.push("CustNO in(select distinct CustNO from ERPSALESDETAILS WHERE  SALEsDate >= '" + QSDate + "' AND IsActive=1)");
                     } else if (QSDate == '' && QEDate != '' && SalesTypeID == '') {
                         result.push("CustNO in(select distinct CustNO from ERPSALESDETAILS WHERE  SALEsDate <= '" + QEDate + "' AND IsActive=1)");
                     } else if (QSDate == '' && QEDate == '' && SalesTypeID != '') {
                         result.push("CustNO in(select distinct CustNO from ERPSALESDETAILS WHERE  SalesTypeID ='" + SalesTypeID + "' AND IsActive=1)");
                     } else if (QSDate != '' && QEDate != '' && SalesTypeID == '') {
                         result.push("CustNO in(select distinct CustNO from ERPSALESDETAILS WHERE  SALEsDate >= '" + QSDate + "' and SALEsDate <='" + QEDate + "' AND IsActive=1)");
                     } else if (QSDate != '' && QEDate == '' && SalesTypeID != '') {
                         result.push("CustNO in(select distinct CustNO from ERPSALESDETAILS WHERE  SALEsDate >= '" + QSDate + "' and SalesTypeID ='" + SalesTypeID + "' AND IsActive=1)");
                     } else if (QSDate == '' && QEDate != '' && SalesTypeID != '') {
                         result.push("CustNO in(select distinct CustNO from ERPSALESDETAILS WHERE  SALEsDate <='" + QEDate + "' and SalesTypeID ='" + SalesTypeID + "' AND IsActive=1)");
                     }
                 }


                 //當沒有輸入任何查詢條件時,顯示最近30天有交易的客戶資料
                 //if (Filtflag == 0) {
                 //    aVal = 15;
                 //    result.push("(dbo.funReturnCustDealDays(A.LatelyDayD)) <= '" + aVal + "'");
                 //}
                 //當使用者不是查詢名單時,僅能檢視開放查詢的客戶
                 //var UserID = getClientInfo("UserID");
                 if (result.length > 0) {
                     $(dg).datagrid('setWhere', result.join(' and '));
                 }else{
                     $(dg).datagrid('setWhere', '1=1');
                 }
             }
            
         }

         function clearQuery(dgid) {
             var isQueryAutoColumn = getInfolightOption($(dgid)).queryAutoColumn;
             if (isQueryAutoColumn == true) {
                 var pnid = getInfolightOption($(dgid)).queryDialog;
                 var queryTr = $('#queryTr_' + $(dgid)[0].id);
                 var queryParams = $(dgid).datagrid('options').queryParams;
                 var queryWord = new Object();

                 var where = '';
                 $(":text,select", queryTr).each(function () {
                     var text = $(this);
                     text.val('');
                 });
             }

             var pnid = getInfolightOption($(dgid)).queryDialog;
             if (pnid != undefined) {
                 var queryParams = $(dgid).datagrid('options').queryParams;
                 var queryWord = new Object();

                 var where = '';
                 //jbjob edit by serlina 增加textarea
                 $(":text,select,textarea", pnid).each(function () {
                     var text = $(this);
                     text.val('');
                     var controlClass = $(this).attr('class');
                     if (controlClass != undefined) {
                         if (controlClass.indexOf('easyui-datebox') == 0) {
                             text.datebox('setValue', '');
                         }
                         else if (controlClass.indexOf('easyui-datetimebox') == 0) {
                             text.datetimebox('setValue', '');
                         }
                         else if (controlClass.indexOf('info-combobox') == 0) {
                             text.combobox('setValue', '');
                             text.combobox('reload');
                         }
                         else if (controlClass.indexOf('info-combogrid') == 0) {
                             text.combogrid('setValue', '');
                             text.combogrid('clear');
                             text.combogrid('setWhere', '');
                         }
                         else if (controlClass.indexOf('combo-text') == 0) {
                             value = '';
                         }
                         else if (controlClass.indexOf('info-refval') == 0) {
                             text.refval('setValue', '');
                         }
                         else if (controlClass.indexOf('info-autocomplete') == 0) {
                             text.combobox('setValue', '');
                         }
                     }
                 });
                 $(":radio,:checkbox", pnid).each(function () {
                     $(this).prop("checked", false);
                 });
             }
         }
     </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sERPMediaCustomerQuery.ERPCustomers" runat="server" AutoApply="True"
                DataMember="ERPCustomers" Pagination="True" QueryTitle="查詢(若有填「銷貨類別」、「銷貨日期起」、「銷貨日期迄」，則會篩選出有交易的客戶)"
                Title="客戶資料" AllowDelete="False" AllowInsert="False" AllowUpdate="False" QueryMode="Panel" AlwaysClose="True" AllowAdd="False" ViewCommandVisible="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False">
                <Columns>

                    <%--<JQTools:JQGridColumn Alignment="left" Caption="RecallDate" Editor="datebox" FieldName="RecallDate" Format="" Width="120" />--%>
                    
                    <%--<JQTools:JQGridColumn Alignment="left" Caption="建檔人員" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CreateDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="LastUpdateBy" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="開放查詢" Editor="text" FieldName="IsPublicView" Format="" Width="120" />--%>
                    
                    <%--<JQTools:JQGridColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="HrBankURL" Editor="text" FieldName="HrBankURL" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PostedDate" Editor="datebox" FieldName="PostedDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PostedMan" Editor="text" FieldName="PostedMan" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="DevelopDescr" Editor="text" FieldName="DevelopDescr" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="PostSourceID" Editor="numberbox" FieldName="PostSourceID" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ERPCustomerID" Editor="text" FieldName="ERPCustomerID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="業務" Editor="text" FieldName="SalesName" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人" Editor="text" FieldName="CustJobName" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="CENTER_ID" Editor="numberbox" FieldName="CENTER_ID" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="交易天數" Editor="numberbox" FieldName="DealDays" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="銷貨類別" Editor="text" FieldName="SalesTypeID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="起始日期" Editor="text" FieldName="QSDate" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="終止日期" Editor="text" FieldName="QEDate" Format="" MaxLength="0" Width="120" />--%>
                    
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustNO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustName" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="簡稱" Editor="text" FieldName="CustShortName" Format="" MaxLength="0" Width="120" />
                    <%--<JQTools:JQGridColumn Alignment="left" Caption="業務人員" Editor="infocombobox" FieldName="SalesID" Format="" MaxLength="0" Width="120" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sERPMediaCustomerQuery.SalesMan',tableName:'SalesMan',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />--%>
                    <JQTools:JQGridColumn Alignment="left" Caption="業務人員" Editor="text" FieldName="SalesName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="雇主" Editor="text" FieldName="BossName" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="統一編號" Editor="text" FieldName="TaxNO" Format="" MaxLength="0" Width="120" />
                    <%--<JQTools:JQGridColumn Alignment="left" Caption="IndustryTypeID" Editor="infocombobox" FieldName="IndustryTypeID" Format="" MaxLength="0" Width="120" EditorOptions="valueField:'IndustryTypeID',textField:'IndustryTypeName',remoteName:'sERPMediaCustomerQuery.Industry',tableName:'Industry',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Visible="False" />--%>
                    <JQTools:JQGridColumn Alignment="left" Caption="電話號碼1" Editor="text" FieldName="CustTelNO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="電話號碼2" Editor="text" FieldName="CustTelNO1" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="傳真號碼" Editor="text" FieldName="CustFaxNO" Format="" MaxLength="0" Width="120" />
                    <%--<JQTools:JQGridColumn Alignment="left" Caption="縣市" Editor="infocombobox" FieldName="CustCityNO" Format="" MaxLength="0" Width="120" EditorOptions="valueField:'CityNO',textField:'CityName',remoteName:'sERPMediaCustomerQuery.City',tableName:'City',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />--%>
                    <JQTools:JQGridColumn Alignment="left" Caption="縣市" Editor="text" FieldName="CityName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="鄉鎮市區" Editor="text" FieldName="CustRegion" Format="" MaxLength="0" Width="120" EditorOptions="valueField:'Region',textField:'Region',remoteName:'sERPMediaCustomerQuery.Region',tableName:'Region',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="郵遞區號" Editor="text" FieldName="CustPost" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="地址" Editor="text" FieldName="CustAddr" Format="" MaxLength="0" Width="120" />
                    
                    <%--<JQTools:JQGridColumn Alignment="right" Caption="部門" Editor="infocombobox" FieldName="CustDeptID" Format="" Width="120" EditorOptions="valueField:'CustDeptID',textField:'CustDeptName',remoteName:'sERPMediaCustomerQuery.CustDept',tableName:'CustDept',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200" />--%>
                    <JQTools:JQGridColumn Alignment="left" Caption="部門" Editor="text" FieldName="CustDeptName1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="子部門" Editor="text" FieldName="CustDeptName" Format="" MaxLength="0" Width="120" />
                    <%--<JQTools:JQGridColumn Alignment="left" Caption="區域" Editor="infocombobox" FieldName="CustAreaID" Format="" MaxLength="0" Width="120" EditorOptions="valueField:'CustAreaID',textField:'CustAreaName',remoteName:'sERPMediaCustomerQuery.CustArea',tableName:'CustArea',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />--%>
                    <JQTools:JQGridColumn Alignment="left" Caption="區域" Editor="text" FieldName="CustAreaName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="信封備註" Editor="text" FieldName="LetterType" Format="" MaxLength="0" Width="120" />
                    
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人(1)" Editor="text" FieldName="ContactA" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="行動電話(1)" Editor="text" FieldName="ContactATel" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="分機(1)" Editor="text" FieldName="ContactASubTel" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Email(1)" Editor="text" FieldName="ContactAMail" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="發票(1)" Editor="checkbox" FieldName="samA" Format="" Width="120" />
                    <%--<JQTools:JQGridColumn Alignment="left" Caption="職稱(1)" Editor="infocombobox" FieldName="ContactAJobID" Format="" MaxLength="0" Width="120" EditorOptions="valueField:'CustJobID',textField:'CustJobName',remoteName:'sERPMediaCustomerQuery.CustJob',tableName:'CustJob',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />--%>
                    <JQTools:JQGridColumn Alignment="left" Caption="職稱(1)" Editor="text" FieldName="CustJobName1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="電子報(1)" Editor="checkbox" FieldName="ContactAIsMail" Format="" Width="120" />
                    
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人(2)" Editor="text" FieldName="ContactB" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="行動電話(2)" Editor="text" FieldName="ContactBTel" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="分機(2)" Editor="text" FieldName="ContactBSubTel" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Email(2)" Editor="text" FieldName="ContactBMail" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="發票(2)" Editor="checkbox" FieldName="samB" Format="" Width="120" />
                    <%--<JQTools:JQGridColumn Alignment="left" Caption="職稱(2)" Editor="infocombobox" FieldName="ContactBJobID" Format="" MaxLength="0" Width="120" EditorOptions="valueField:'CustJobID',textField:'CustJobName',remoteName:'sERPMediaCustomerQuery.CustJob',tableName:'CustJob',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />--%>
                    <JQTools:JQGridColumn Alignment="left" Caption="職稱(2)" Editor="text" FieldName="CustJobName2" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="電子報(2)" Editor="checkbox" FieldName="ContactBIsMail" Format="" Width="120" />

                    <JQTools:JQGridColumn Alignment="left" Caption="帳務人員(3)" Editor="text" FieldName="BillDeal" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Email(3)" Editor="text" FieldName="BillDealEmail" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="發票(3)" Editor="checkbox" FieldName="samC" Format="" Width="120" />
                    
                    <%--<JQTools:JQGridColumn Alignment="right" Caption="客戶等級" Editor="infocombobox" FieldName="PostType" Format="" Width="120" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sERPMediaCustomerQuery.PostType',tableName:'PostType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />--%>
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶等級" Editor="text" FieldName="ListContent" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="加入LINE" Editor="checkbox" FieldName="IsAddLine" Format="" Width="120" EditorOptions="on:1,off:0" />
                    <%--<JQTools:JQGridColumn Alignment="left" Caption="專案人員" Editor="infocombobox" FieldName="PMID" Format="" MaxLength="0" Width="120" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sERPMediaCustomerQuery.SalesMan',tableName:'SalesMan',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />--%>

                    <JQTools:JQGridColumn Alignment="left" Caption="專案人員" Editor="text" FieldName="PMName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>

                    <JQTools:JQGridColumn Alignment="left" Caption="電子發票詢問" Editor="checkbox" FieldName="bElecInvoice" Format="" Width="120" EditorOptions="on:1,off:0" />
                    <JQTools:JQGridColumn Alignment="left" Caption="不接受原因" Editor="text" FieldName="ElecInvoiceReason" Format="" MaxLength="0" Width="120" />
                    <%--<JQTools:JQGridColumn Alignment="left" Caption="行業別" Editor="infocombobox" FieldName="IndustryID" Format="" MaxLength="0" Width="120" EditorOptions="valueField:'jb_type',textField:'jb_name',remoteName:'sERPMediaCustomerQuery.Industry',tableName:'Industry',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />--%>
                    <JQTools:JQGridColumn Alignment="left" Caption="行業別" Editor="text" FieldName="jb_name" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <%--<JQTools:JQGridColumn Alignment="right" Caption="媒體產業" Editor="infocombobox" FieldName="IndustryType" Format="" Width="120" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sERPMediaCustomerQuery.IndustryType',tableName:'IndustryType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />--%>

                    <JQTools:JQGridColumn Alignment="left" Caption="媒體產業" Editor="text" FieldName="ListContent1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>

                    <JQTools:JQGridColumn Alignment="left" Caption="開立發票" Editor="text" FieldName="IsPutInvoice" Format="" Width="120" EditorOptions="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="報紙發票" Editor="checkbox" FieldName="IsPutPaperInvoice" Format="" Width="120" EditorOptions="on:1,off:0" />
                     <JQTools:JQGridColumn Alignment="left" Caption="轉永豐銀行" Editor="checkbox" FieldName="IsChangeBank" Format="" Width="120" EditorOptions="on:1,off:0" />
                    <JQTools:JQGridColumn Alignment="left" Caption="結帳日" Editor="text" FieldName="BalanceDay" Format="" MaxLength="0" Width="120" />
                   <%-- <JQTools:JQGridColumn Alignment="right" Caption="寄電子報" Editor="infocombobox" FieldName="IsAcceptePaper" Format="" Width="120" EditorOptions="valueField:'ePaperCode',textField:'ePaperType',remoteName:'sERPMediaCustomerQuery.PaperType',tableName:'PaperType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />--%>
                    <JQTools:JQGridColumn Alignment="left" Caption="寄電子報" Editor="text" FieldName="ePaperType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="不寄原因" Editor="text" FieldName="NotAcceptPaper" Format="" MaxLength="0" Width="120" />

                    <JQTools:JQGridColumn Alignment="left" Caption="本次複訪日" Editor="datebox" FieldName="NextCallDate" Format="" Width="120" />

                    <JQTools:JQGridColumn Alignment="left" Caption="票匯備註" Editor="text" FieldName="PayNotes" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="仲介公司" Editor="text" FieldName="ForeignCompany" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="本勞人數" Editor="numberbox" FieldName="iPeopleCount" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="外勞人數" Editor="numberbox" FieldName="iPeopleFCount" Format="" Width="120" />
                    <%--<JQTools:JQGridColumn Alignment="left" Caption="外勞宿舍" Editor="infocombobox" FieldName="ForeignDorm" Format="" MaxLength="0" Width="120" EditorOptions="valueField:'ForeignDorm',textField:'ForeignDorm',remoteName:'sERPToDoCustomer.infoForeignDorm',tableName:'infoForeignDorm',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />--%>
                    <JQTools:JQGridColumn Alignment="left" Caption="外勞宿舍" Editor="text" FieldName="ForeignDorm" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <%--<JQTools:JQGridColumn Alignment="left" Caption="辦事處" Editor="infocombobox" FieldName="OfficeNo" Format="" MaxLength="0" Width="120" EditorOptions="valueField:'OfficeNo',textField:'OfficeName',remoteName:'sERPToDoCustomer.infoERPOffice',tableName:'infoERPOffice',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200" />--%>
                    <JQTools:JQGridColumn Alignment="left" Caption="辦事處" Editor="text" FieldName="OfficeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <%--<JQTools:JQGridColumn Alignment="left" Caption="工作天數" Editor="infocombobox" FieldName="JobWeekendNo" Format="" MaxLength="0" Width="120" EditorOptions="valueField:'JobWeekendNo',textField:'JobWeekendName',remoteName:'sERPToDoCustomer.infoERPJobWeekend',tableName:'infoERPJobWeekend',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200" />--%>

                    <JQTools:JQGridColumn Alignment="left" Caption="工作天數" Editor="text" FieldName="JobWeekendName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>

                    <JQTools:JQGridColumn Alignment="left" Caption="重要備註" Editor="text" FieldName="CustNotes" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="求才備註" Editor="text" FieldName="DealNotesP" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="週報備註" Editor="text" FieldName="DealNotesW" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="報紙備註" Editor="text" FieldName="DealNotesN" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="最近交易日" Editor="datebox" FieldName="LatelyDayD" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="求才交易日" Editor="datebox" FieldName="LatelyDayP" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="週報交易日" Editor="datebox" FieldName="LatelyDayW" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="報紙交易日" Editor="datebox" FieldName="LatelyDayN" Format="" Width="120" />

                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Enabled="True" Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportXlsx" Text="匯出xlsx" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶代號" Condition="%%" DataType="string" Editor="infocombobox" FieldName="CustNO" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="110" EditorOptions="valueField:'CustNO',textField:'CustNO',remoteName:'sERPMediaCustomerQuery.ERPCustomersQ',tableName:'ERPCustomersQ',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶名稱" Condition="%%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CustName',textField:'CustName',remoteName:'sERPMediaCustomerQuery.ERPCustomersQ',tableName:'ERPCustomersQ',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CustName" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="180" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="業務代號" Condition="=" DataType="string" Editor="infocombobox" FieldName="SalesID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sERPMediaCustomerQuery.SalesMan',tableName:'SalesMan',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" TableName="A" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="本勞人數" Condition="=" DataType="number" Editor="text" FieldName="iPeopleCount" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="50" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="外勞人數" Condition="=" DataType="number" Editor="text" FieldName="iPeopleFCount" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="50" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="行業別" Condition="%%" DataType="string" Editor="infocombobox" FieldName="IndustryID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="220" EditorOptions="valueField:'jb_type',textField:'jb_name',remoteName:'sERPMediaCustomerQuery.Industry',tableName:'Industry',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="區域" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CustAreaID',textField:'CustAreaName',remoteName:'sERPMediaCustomerQuery.CustArea',tableName:'CustArea',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CustAreaID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="銷貨類別" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERPMediaCustomerQuery.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTypeID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                    <JQTools:JQQueryColumn AndOr="" Caption="銷貨日期起" Condition="" DataType="datetime" Editor="datebox" FieldName="QSDate" Format="yyyy-mm-dd" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="87" />
                    <JQTools:JQQueryColumn AndOr="" Caption="銷貨日期迄" Condition="" DataType="datetime" Editor="datebox" FieldName="QEDate" Format="yyyy-mm-dd" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="87" />
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>

    </form>
</body>
</html>
