<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_ARInvoice.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script type="text/javascript">
         //宣告公共變數
         var P_APIWebCode = ''; //網站代號
         var P_APIPassword = '';//網站密碼
         var P_RentID = ''; //公司統編
         var P_backcolor = "#E8FFE8";
         $(document).ready(function () {
             $(function () {
                 //$("input, select, textarea").focus(function () {
                 //    $(this).css("background-color", "#FFFFDE");

                 //});
                 //$("input, select, textarea").blur(function () {
                 //    $(this).css("background-color", P_backcolor);
                 //});
                 $('.infosysbutton-q', '#querydataGridView').closest('td').attr({ 'align': 'middle' });
             });
         });
         function genCheckBox(val) {
             if (val)
                 return "<input  type='checkbox' checked='true' />";
             else
                 return "";
         }
         function dataGridViewOnLoadSucess() {
             var UserID = getClientInfo("UserID");
             if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) {
                 $(this).datagrid({
                     singleSelect: true,
                     selectOnCheck: false,
                     checkOnSelect: false
                 });
             }
             $('#InsGroupID_Query').combobox('setWhere', "InsGroupID IN (Select Distinct InsGroupID From SalesSalesType X,SalesType Y Where X.SalesTypeID=Y.SalesTypeID AND X.SalesID = " +"'"+ UserID +"'"+")");

         }
         function toCurrency(num) {
             var parts = num.toString().split('.');
             parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ',');
             return parts.join('.');
         }
         function getFirstDate() {
             var date = new Date();
             var firstDay = new Date(date.getFullYear(), date.getMonth(), 1);
             return convertDate(firstDay);
         }
         function convertDate(date) {
             var yyyy = date.getFullYear().toString();
             var mm = (date.getMonth() + 1).toString();
             var dd = date.getDate().toString();
             var mmChars = mm.split('');
             var ddChars = dd.split('');
             return yyyy + '/' + (mmChars[1] ? mm : "0" + mmChars[0]) + '/' + (ddChars[1] ? dd : "0" + ddChars[0]);
         }
         function queryGrid(dg) {
             if ($(dg).attr('id') == 'dataGridView') {
                 var result = [];
                 var aVal = '';
                 aVal = $('#InsGroupID_Query').combobox('getValue');
                 if (aVal != '') {
                     //result.push("dbo.InvoiceDetails.InsGroupID = '" + aVal + "'");
                     result.push("InsGroupID = '" + aVal + "'");
                 }

                 //else {
                 //    alert('注意!!,請選擇公司別...');
                 //    return false;
                 //}

                 //銷貨起日
                 aVal = $("#SalesDateS_Query").datebox('getValue');
                 if (aVal != '')
                     //result.push("dbo.InvoiceDetails.SalesDate >= '" + aVal + "'");
                     result.push("SalesDate >= '" + aVal + "'");
                 //銷貨迄日
                 aVal = $("#SalesDateE_Query").datebox('getValue');
                 if (aVal != '')
                     //result.push("dbo.InvoiceDetails.SalesDate <= '" + aVal + "'");
                     result.push("SalesDate <= '" + aVal + "'");
                 //發票起日
                 aVal = $("#InvDateS_Query").datebox('getValue');
                 if (aVal != '')
                     //result.push("dbo.InvoiceDetails.InvoiceDate >= '" + aVal + "'");
                    result.push("InvoiceDate >= '" + aVal + "'");
                 //發票迄日
                 aVal = $("#InvDateE_Query").datebox('getValue');
                 if (aVal != '')
                     //result.push("dbo.InvoiceDetails.InvoiceDate <= '" + aVal + "'");
                     result.push("InvoiceDate <= '" + aVal + "'");
                 //發票類別
                 aVal = $("#InvoiceType_Query").combobox('getValue');
                 if (aVal != '') {
                     //result.push("S.QInvoiceType = '" + aVal + "'");
                     result.push("QInvoiceType = '" + aVal + "'");
                 }
                 //銷貨類別
                 aVal = $("#SalesTypeID_Query").combobox('getValue');
                 if (aVal != '')
                     //result.push("dbo.InvoiceDetails.SalesTypeID = '" + aVal + "'");
                    result.push("SalesTypeID = '" + aVal + "'");
                 //客戶
                 aVal = $("#CustomerID_Query").combobox('getValue');
                 if (aVal != '')
                     //result.push("dbo.InvoiceDetails.CustomerID = '" + aVal + "'");
                     result.push("CustomerID = '" + aVal + "'");
                 //雇主
                 aVal = $("#Employer_Query").combobox('getValue');
                 if (aVal != '')
                     //result.push("dbo.InvoiceDetails.Employer = '" + aVal + "'");
                     result.push("Employer = '" + aVal + "'");
                 //發票號碼
                 aVal = $("#InvoiceNO_Query").val();
                 if (aVal != '')
                     //result.push("dbo.InvoiceDetails.InvoiceNO = '" + aVal + "'");
                     result.push("InvoiceNO = '" + aVal + "'");
                 //銷貨單號
                 aVal = $("#SalesNO_Query").val();
                 if (aVal != '')
                     //result.push("S.SalesNO = '" + aVal + "'");
                     result.push("SalesNO = '" + aVal + "'");
                 //有效
                 aVal = $("#IsActive_Query").combobox('getValue');
                 if (aVal != '')
                     //result.push("dbo.InvoiceDetails.IsActive = '" + aVal + "'");
                     result.push("IsActive = '" + aVal + "'");
                 //業務
                 aVal = $('#SalesID_Query').combobox('getValue');
                 if (aVal != '') {
                     //result.push("dbo.InvoiceDetails.SalesID = '" + aVal + "'");
                     result.push("SalesID = '" + aVal + "'");
                 }

                 //客名統編電話
                 aVal = $('#VirtualColumn_Query').val();
                 if (aVal != '') {
                     //result.push("(C.CustomerName like '%" + aVal + "%' or C.TaxNO like '%" + aVal + "%' or C.TelNO like '%" + aVal + "%')");
                     result.push("(CustomerName like '%" + aVal + "%' or TaxNO like '%" + aVal + "%' or TelNO like '%" + aVal + "%')");
                 }
             }
             var filtstr = result.join(' and ');
             $(dg).datagrid('setWhere', filtstr);

         }
         function dataGridViewOnUpdate(rowData) {
             openForm('#JQDialog1', rowData, "viewed", 'dialog');
             return false;
         }
         //選取公司別時,篩選銷貨客戶、銷貨類別、雇主
         function QInsGroupOnSelect(rowData) {
             var InsGroupID = $('#InsGroupID_Query').combobox('getValue');
             P_APIWebCode = (rowData.APIWebCode);
             P_APIPassword = (rowData.APIPassword);
             P_RentID = (rowData.TaxNO);
             $("#CustomerID_Query").combobox('setValue', '');//客戶
             $("#CustomerID_Query").combobox('setWhere', "CustomerID IN (Select CustomerID From SalesMaster Where InsGroupID='" + InsGroupID + "' Group By CustomerID )");
             $("#SalesTypeID_Query").combobox('setValue', '');
             $("#SalesTypeID_Query").combobox('setWhere', "InsGroupID ='" + InsGroupID + "'");
             $("#Employer_Query").combobox('setWhere', '');
             $("#Employer_Query").combobox('setWhere', "CustomerID IN (Select Employer From SalesMaster Where InsGroupID='" + InsGroupID + "' Group By Employer)");
         }
         function CancelInvoice() {
             //var rows = $('#dataGridView').datagrid("getChecked");
             var rows = $('#dataGridView').datagrid("getSelected");
             if (rows.UploadCode == 'C0') {
                 alert('注意!!發票號碼['+rows.InvoiceNO+']已作廢');
                 return false;
             }
             var amount = 0;
             var before1 = '';
             var before2 = '';
             var after = '';
             var scount = 0;
             var samt = 0;
             var fcount = 0;
             var famt = 0;
             var count = rows.length;
             if (count == 0) {
                 alert('注意!!未選取任何發票號碼,請選取');
                 return false;
             }
             var ReturnTaxNumber = ''; //作廢文號
             var ReturnRemark = '作廢測試'
             var InsGroupID = $('#InsGroupID_Query').combobox('getValue');
             amount = rows.SalesAmount;
             before1 = '注意!!您已選取要上傳取消發票的資訊如下:';
             before2 = '筆數:' + i.toString() + '\n' + '銷貨金額:' + toCurrency(amount);
             var yn = confirm(before1 + '\n' + before2);
             if (yn == false) {
                 return
             }
                 var Rstr = CallAPI_Cancel(InsGroupID, rows.SalesNO, rows.InvoiceNO, P_RentID, P_APIWebCode + P_APIPassword, ReturnTaxNumber, ReturnRemark);
                 if (Rstr == 'C0') {
                     scount = scount + 1;
                     samt = samt + rows.SalesAmount;
                 }
                 else {
                     fcount = fcount + 1;
                     famt = famt + rows.SalesAmount;
                 }
             //}
             after = '發票取消資訊如下:' + '\n' + '執行前...' + '\n' + before2 + '\n\n\n' + '執行後...' + '\n' + '成功筆數:' + scount.toString() + '      ' + '成功金額:' + toCurrency(samt) + '\n' + '失敗筆數:' + fcount.toString() + '      ' + '失敗金額:' + famt.toString();
             $('#dataGridView').datagrid('reload');
             return true;
         }
         //呼叫鯨耀開立發票API 作廢發票
         //傳入參數:@Para1:公司代號 @SalesNO:銷貨單號 @InvoiceNO:發票號碼 @RentID:公司統編 @Source:網站代號與密碼 @UserID:使用者代號 @ReturnTaxNumber:發票作廢文號 @ReturnRemark:作廢原因 @UserID:使用者代號
         function CallAPI_Cancel(InsGroupID, SalesNO, InvoiceNO, RentID, Source, ReturnTaxNumber, ReturnRemark) {
             var ReturnStr = '';
             var UserID = getClientInfo("UserID");
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sARInvoice.InvoiceDetails',
                 data: "mode=method&method=" + "procCallApi_Cancel" + " &parameters=" + InsGroupID + "," + SalesNO + "," + InvoiceNO + "," + RentID + "," + Source + "," + ReturnTaxNumber + "," + ReturnRemark + "," + UserID,
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
         //刪除紀錄前檢查
         function DataGridViewOnDelete() {
             var rows = $('#dataGridView').datagrid("getSelected");
             if ((rows.InvoiceTypeID == '98' || rows.InvoiceTypeID == '99') && rows.UploadCode != 'C0') {
                 alert('注意!!本發票狀態需為"成功作廢發票"時才可刪除...')
                 return false;
             };
             if (rows.InvoiceTypeID == '97' && rows.RecAmount == 0) {
                 alert('注意!!本收據狀已有收款金額,無法刪除...')
                 return false;
             }
             $("dataGridView").datagrid('reload');
             return true;
          
         }
       


     </script>       
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sARInvoice.InvoiceDetails" runat="server" AutoApply="True"
                DataMember="InvoiceDetails" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="發票應收資料" AllowAdd="True" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="20,40,60,80,120,180" PageSize="20" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="合計:" UpdateCommandVisible="False" ViewCommandVisible="True" OnLoadSuccess="dataGridViewOnLoadSucess" OnDelete="DataGridViewOnDelete" OnUpdate="dataGridViewOnUpdate">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="發票號碼" Editor="text" FieldName="InvoiceNO" Format="" MaxLength="0" Width="85" />
                    <JQTools:JQGridColumn Alignment="left" Caption="銷貨單號" Editor="text" FieldName="SalesNO" Format="" MaxLength="0" Width="85" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="銷貨日期" Editor="text" FieldName="SalesDate" Format="yyyy/mm/dd" Width="75" />
                    <JQTools:JQGridColumn Alignment="left" Caption="發票日期" Editor="text" FieldName="InvoiceDate" Format="yyyy/mm/dd" Width="75" />
                    <JQTools:JQGridColumn Alignment="left" Caption="應收款日" Editor="text" FieldName="ARDate" Format="yyyy/mm/dd" Width="75" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶" Editor="text" FieldName="CustomerName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="雇主" Editor="infocombobox" FieldName="Employer" Format="" MaxLength="0" Width="60" Visible="True" EditorOptions="valueField:'CUSTOMERID',textField:'SHORTNAME',remoteName:'sARInvoice.Employer',tableName:'Employer',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="銷貨類別" Editor="text" FieldName="SalesTypeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="發票類別" Editor="text" FieldName="InvoiceTypeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="75">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="銷貨金額" Editor="numberbox" FieldName="SalesAmount" Format="N" Width="65" Total="sum" />
                    <JQTools:JQGridColumn Alignment="right" Caption="稅額" Editor="numberbox" FieldName="SalesTax" Format="N" Width="55" Total="sum" />
                    <JQTools:JQGridColumn Alignment="right" Caption="銷貨總額" Editor="numberbox" FieldName="SalesTotal" Format="N" Width="65" Total="sum" />
                    <JQTools:JQGridColumn Alignment="right" Caption="已沖金額" Editor="text" FieldName="RecAmount" Format="N" MaxLength="0" Width="65" Total="sum" />
                    <JQTools:JQGridColumn Alignment="right" Caption="未沖金額" Editor="text" FieldName="DiffAmount" Format="N" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70" Total="sum">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="載具註記" Editor="text" FieldName="DonateMark" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="載具類別" Editor="text" FieldName="CarrierTypeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="愛心碼" Editor="text" FieldName="NPOBAN" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="載具顯碼" Editor="text" FieldName="CarrierID1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="載具隱碼" Editor="text" FieldName="CarrierID2" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="業務" Editor="text" FieldName="SalesName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="銷貨狀態" Editor="text" FieldName="UploadDesc" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="CustomerID" Editor="text" FieldName="CustomerID" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="RandNO" Editor="text" FieldName="RandNO" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="InsGroupID" Editor="numberbox" FieldName="InsGroupID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesTypeID" Editor="text" FieldName="SalesTypeID" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesID" Editor="text" FieldName="SalesID" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="TaxRate" Editor="numberbox" FieldName="TaxRate" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="InvoiceTypeID" Editor="text" FieldName="InvoiceTypeID" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="IsActive" Editor="text" FieldName="IsActive" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="UploadCode" Editor="text" FieldName="UploadCode" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="銷貨品名清單" Editor="text" FieldName="SalesTypeNameList" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="250">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="沖款日期" Editor="text" FieldName="WarrantDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="240">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="備註(內部)" Editor="text" FieldName="RemarkForInner" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton"    OnClick="CancelInvoice" Text="作廢發票" Visible="False"/>
                    <JQTools:JQToolItem Icon="icon-cut" ItemType="easyui-linkbutton"    OnClick="CancelInvoice" Text="刪除發票收據" Visible="False"/>
                    <JQTools:JQToolItem Enabled="True" Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="公司別" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'InsGroupID',textField:'ShortName',remoteName:'sARInvoice.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:QInsGroupOnSelect,panelHeight:200" FieldName="InsGroupID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                     <JQTools:JQQueryColumn AndOr="and" Caption="銷貨起迄日" Condition="&gt;=" DataType="datetime" Editor="datebox" FieldName="SalesDateS" Format="yyyy/mm/dd" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" DefaultMethod="getFirstDate" DefaultValue="" />
                     <JQTools:JQQueryColumn AndOr="and" Caption="-" Condition="&lt;=" DataType="datetime" Editor="datebox" FieldName="SalesDateE" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" DefaultValue="_today" Format="yyyy/mm/dd" />
                     <JQTools:JQQueryColumn AndOr="and" Caption="發票起迄日" Condition="&gt;=" DataType="datetime" Editor="datebox" FieldName="InvDateS" Format="yyyy/mm/dd" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" DefaultMethod="" />
                     <JQTools:JQQueryColumn AndOr="and" Caption="-" Condition="&lt;=" DataType="datetime" Editor="datebox" FieldName="InvDateE" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" DefaultValue="" Format="yyyy/mm/dd" />
                     <JQTools:JQQueryColumn AndOr="and" Caption="發票有效" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'1',text:'是',selected:'false'},{value:'0',text:'否',selected:'false'}],checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:100" FieldName="IsActive" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="60" />
                     <JQTools:JQQueryColumn AndOr="and" Caption="銷貨類別" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sARInvoice.SalesTypeQ',tableName:'SalesTypeQ',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTypeID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                     <JQTools:JQQueryColumn AndOr="and" Caption="單據類別" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'INVOICETYPEID',textField:'INVOICETYPENAME',remoteName:'sARInvoice.InvoiceType',tableName:'InvoiceType',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="InvoiceType" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="92" />
                     <JQTools:JQQueryColumn AndOr="and" Caption="銷貨客戶" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CustomerID',textField:'ShortName',remoteName:'sARInvoice.QCustomer',tableName:'QCustomer',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CustomerID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="雇主" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CUSTOMERID',textField:'SHORTNAME',remoteName:'sARInvoice.QEmployer',tableName:'QEmployer',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="Employer" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="發票號碼" Condition="=" DataType="string" Editor="text" FieldName="InvoiceNO" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="銷貨單號" Condition="=" DataType="string" Editor="text" FieldName="SalesNO" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="業務" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sARInvoice.SalesPerson',tableName:'SalesPerson',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="80" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="客名統編電話" Condition="%" DataType="string" Editor="text" FieldName="VirtualColumn" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="180" />
                   </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="發票應收資料" DialogLeft="50px" DialogTop="50px" Width="660px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="InvoiceDetails" HorizontalColumnsCount="3" RemoteName="sARInvoice.InvoiceDetails" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="發票號碼" Editor="text" FieldName="InvoiceNO" Format="" maxlength="0" Width="100" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="檢查碼" Editor="text" FieldName="RandNO" Format="" maxlength="0" Width="35" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="發票日期" Editor="datebox" FieldName="InvoiceDate" Format="" maxlength="0" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨單號" Editor="text" FieldName="SalesNO" Format="" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨日期" Editor="datebox" FieldName="SalesDate" Format="" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="應收款日" Editor="datebox" FieldName="ARDate" Format="" maxlength="0" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨類別" Editor="infocombobox" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sARInvoice.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="SalesTypeID" Format="" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨客戶" Editor="inforefval" EditorOptions="valueField:'CustomerID',textField:'ShortName',remoteName:'sARInvoice.Customer',tableName:'Customer',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CustomerID" Format="" maxlength="0" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="雇主" Editor="inforefval" EditorOptions="valueField:'CustomerID',textField:'ShortName',remoteName:'sARInvoice.Customer',tableName:'Customer',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="Employer" Format="" maxlength="0" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="發票類別" Editor="infocombobox" EditorOptions="valueField:'INVOICETYPEID',textField:'INVOICETYPENAME',remoteName:'sARInvoice.InvoiceType',tableName:'InvoiceType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="QInvoiceType" Format="" maxlength="0" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="業務員" Editor="infocombobox" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sARInvoice.SalesPerson',tableName:'SalesPerson',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesID" Format="" maxlength="0" Span="2" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="right" Caption="銷貨金額" Editor="numberbox" FieldName="SalesAmount" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="right" Caption="銷貨稅額" Editor="numberbox" FieldName="SalesTax" Format="" maxlength="0" Width="90" />
                        <JQTools:JQFormColumn Alignment="right" Caption="發票金額" Editor="numberbox" FieldName="SalesTotal" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="TaxRate" Editor="numberbox" FieldName="TaxRate" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="載具註記" Editor="text" FieldName="DonateMark" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="載具類別" Editor="text" FieldName="CarrierTypeName" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="愛心碼" Editor="text" FieldName="NPOBAN" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="載具顯碼" Editor="text" FieldName="CarrierID1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="載具隱碼" Editor="text" FieldName="CarrierID2" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="生效" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" Format="" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="InsGroupID" Editor="numberbox" FieldName="InsGroupID" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="UploadCode" Editor="text" FieldName="UploadCode" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="UploadDesc" Editor="text" FieldName="UploadDesc" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="沖款日期" Editor="text" FieldName="WarrantDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="440" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨品名清單" Editor="textarea" EditorOptions="height:80" FieldName="SalesTypeNameList" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="440" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
        <script type="text/javascript">
         //$(":input").css("background",P_backcolor);
        </script>
    </form>
</body>
</html>
