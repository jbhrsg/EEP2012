<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_SalesDetailsTrans.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script>                                
         $(document).ready(function () {
             //設定 Grid QueryColunm Windows width=500px
             var dgid = $('#dataGridMaster');
             var queryPanel = getInfolightOption(dgid).queryDialog;
             if (queryPanel)
                 $(queryPanel).panel('resize', { width: 500 });           
             
             $('#InvoiceYM_Query').combobox({ required: true });
             $('#InvoiceYM2_Query').combobox({ required: true });
             $('#SalesMasterNO_Query').combobox({ required: true });

         });
         function genCheckBox(val) {
             if (val != "0")
                 return "<input  type='checkbox' checked='true' onclick='return false;'/>";
             else
                 return "<input  type='checkbox' onclick='return false;'/>";
         }
        
         //修改轉入時,轉入條件值iAutoKey=>取得條件式
         function GetsCondition(val) {
             var sCondition;
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalseDetailsTrans.SalesDetailsTransList', //連接的Server端，command
                 data: "mode=method&method=" + "ReturnSalesTransCondition" + "&parameters=" + val, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                 cache: false,
                 async: false,
                 success: function (data) {                    
                     if (data != false) {
                         sCondition = data;
                     }
                 },
             });            
             return sCondition;
         }

         function queryGrid(dg) { //查詢後添加固定條件
             //******************************************新增轉入
             if ($(dg).attr('id') == 'dataGridMaster') {
                 var InvoiceYM = $("#InvoiceYM_Query").combobox('getValue');
                 if (InvoiceYM == "") {
                     $(dg).datagrid('setWhere', "1 = 0");
                     alert('請選擇發票年月');
                 } else {
                     var where = $(dg).datagrid('getWhere');
                     if (where.length > 0) {
                         if ($('#CustNO_Query').combobox('getValue') != '') {// 客戶代號
                             where = where.replace("CustNO like", "d.CustNO like");
                         }
                         if ($('#SalesEmployeeID_Query').combobox('getValue') != '') { //業務
                             where = where.replace("SalesEmployeeID=", "d.SalesEmployeeID=");
                         }                        
                     }                    
                     $(dg).datagrid('setWhere', where);
                 }
                 //var rows = $("#dataGridMaster").datagrid('getRows');//表示主檔有選擇到
                 //if (rows.length != 0)
                 //{ 
                 //    $("#toolItemdataGridMaster匯入行政系統").show();
                 //}
                 //else
                 //{
                 //    $("#toolItemdataGridMaster匯入行政系統").hide();
                 //}
             //******************************************修改轉入
             } else if ($(dg).attr('id') == 'dataGridMaster2') {
                 var InvoiceYM2 = $("#InvoiceYM2_Query").combobox('getValue');
                 var SalesMasterNO = $("#SalesMasterNO_Query").combobox('getValue');      
                 var sCondition=GetsCondition(SalesMasterNO);
                 if (InvoiceYM2 == "" || SalesMasterNO == "") {
                     $(dg).datagrid('setWhere', "1 = 0");
                     alert('請選擇發票年月或轉入條件');
                 } else {
                     var where = $(dg).datagrid('getWhere');
                     if (where.length > 0) {                         
                         where = where.replace("InvoiceYM2", "InvoiceYM");
                         where = where.replace("and SalesMasterNO=" + SalesMasterNO, sCondition);
                     }
                     $(dg).datagrid('setWhere', where);
                 }
             }
         }
         function TransSys() {
             var r = confirm('確定轉入嗎??')
             //匯入至行政系統
             if (r == true) {
                 var InvoiceYM = $('#InvoiceYM_Query').combobox('getValue');// 發票年月
                 var CustNO = $('#CustNO_Query').combobox('getValue');// 客戶代號
                 var SalesTypeID = $('#SalesTypeID_Query').combobox('getValue');// 交易別
                 var SalesID = $('#SalesEmployeeID_Query').combobox('getValue');// 業務
                 var JQDate1 = $($('input', '#querydataGridMaster')[13]).val();//起始日期     
                 var JQDate2 = $($('input', '#querydataGridMaster')[16]).val();//終止日期
                 $.ajax({
                     type: "POST",
                     url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalseDetailsTrans.SalesDetailsTransList', //連接的Server端，command
                     data: "mode=method&method=" + "InsertERPSalseDetailsTrans" + "&parameters=" + InvoiceYM + "," + CustNO + "," + SalesTypeID + "," + SalesID + "," + JQDate1 + "," + JQDate2,  //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                     cache: false,
                     async: false,
                     success: function (data) {                         
                         alert('轉入完成');
                         queryGrid($('#dataGridMaster'));
                     }
                 });
             }
         }
         //轉入條件過濾發票年月
         function OnSelectCondition() {
             var InvoiceYM2 = $("#InvoiceYM2_Query").combobox('getValue');
             $("#SalesMasterNO_Query").combobox('setWhere', " t.InvoiceYM='" + InvoiceYM2 + "'");
             $("#CustName_Query").refval('setWhere', " t.InvoiceYM='" + InvoiceYM2 + "'");
         }
         //重轉
         function TransSys2() {
             var r = confirm('確定重轉嗎??')
             //匯入至行政系統
             if (r == true) {
                 var InvoiceYM = $('#InvoiceYM2_Query').combobox('getValue');// 發票年月
                 var iAutoKey = $('#SalesMasterNO_Query').combobox('getValue');// 轉入條件                 
                 $.ajax({
                     type: "POST",
                     url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalseDetailsTrans.SalesDetailsTransList', //連接的Server端，command
                     data: "mode=method&method=" + "InsertERPSalseDetailsTrans2" + "&parameters=" + InvoiceYM + "," + iAutoKey,  //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                     cache: false,
                     async: false,
                     success: function (data) {
                         alert('重轉完成');
                         queryGrid($('#dataGridMaster2'));
                     }
                 });
             }
         }
     </script> 
</head>
<body>
    <form id="form1" runat="server">
       <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
        <div id="tab1" class="easyui-tabs">
            <div id="aa" title="新增轉入">
                <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sERPSalseDetailsTrans.SalesDetailsTransList" runat="server" AutoApply="True"
                    DataMember="SalesDetailsTransList" Pagination="True" QueryTitle="轉入條件"
                    Title="" DeleteCommandVisible="False" QueryMode="Panel" UpdateCommandVisible="False" ViewCommandVisible="False" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" Width="680px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="right" Caption="SalesMasterNO" Editor="numberbox" FieldName="SalesMasterNO" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="ItemSeq" Editor="text" FieldName="ItemSeq" Format="" MaxLength="0" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="center" Caption="客戶代號" Editor="text" FieldName="CustNO" Format="" MaxLength="0" Width="80" />
                        <JQTools:JQGridColumn Alignment="center" Caption="客戶簡稱" Editor="text" FieldName="CustName" Format="" MaxLength="0" Width="90" />
                        <JQTools:JQGridColumn Alignment="center" Caption="發票年月" Editor="text" FieldName="InvoiceYM" Format="" MaxLength="0" Width="70" />
                        <JQTools:JQGridColumn Alignment="center" Caption="業務" Editor="text" FieldName="SalesName" Format="" MaxLength="0" Width="80" />
                        <JQTools:JQGridColumn Alignment="center" Caption="銷貨日期" Editor="datebox" FieldName="SalesDate" Format="" Width="120" />
                        <JQTools:JQGridColumn Alignment="center" Caption="交易別" Editor="text" FieldName="SalesTypeID" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" />
                        <JQTools:JQGridColumn Alignment="center" Caption="總額" Editor="numberbox" FieldName="CustAmt" Format="" Width="100" />
                    </Columns>
                    <TooItems>                  
                        <JQTools:JQToolItem Enabled="True" ItemType="easyui-linkbutton" OnClick="TransSys" Text="轉入行政系統" Visible="True" Icon="icon-ok" />
                    </TooItems>
                    <QueryColumns>
                        <JQTools:JQQueryColumn AndOr="and" Caption="發票年月" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'InvoiceYM',textField:'InvoiceYM',remoteName:'sERPSalseDetailsTrans.infoInvoiceYM',tableName:'infoInvoiceYM',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="InvoiceYM" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="客戶代號" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CustNO',textField:'CustShortName',remoteName:'sERPSalseDetailsTrans.infoCustomersAll',tableName:'infoCustomersAll',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CustNO" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="200" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="交易別" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERPSalseDetailsTrans.infoERPSalesType',tableName:'infoERPSalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTypeID" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="業務" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'SalesEmployeeID',textField:'SalesName',remoteName:'sERPSalseDetailsTrans.infoSalesMan',tableName:'infoSalesMan',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesEmployeeID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="起始日期" Condition="&gt;=" DataType="datetime" Editor="datebox" FieldName="SalesDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="100" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="終止日期" Condition="&lt;=" DataType="datetime" Editor="datebox" FieldName="SalesDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="100" />
                    </QueryColumns>
                </JQTools:JQDataGrid>
            </div>
            <div id="bb" title="修改轉入">
                                <JQTools:JQDataGrid ID="dataGridMaster2" data-options="pagination:true,view:commandview" RemoteName="sERPSalseDetailsTrans.SalesDetailsTransList2" runat="server" AutoApply="True"
                    DataMember="SalesDetailsTransList2" Pagination="True" QueryTitle="重轉條件"
                    Title="" DeleteCommandVisible="False" QueryMode="Panel" UpdateCommandVisible="False" ViewCommandVisible="False" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" Width="755px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="right" Caption="SalesMasterNO" Editor="numberbox" FieldName="SalesMasterNO" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="ItemSeq" Editor="text" FieldName="ItemSeq" Format="" MaxLength="0" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="center" Caption="客戶代號" Editor="text" FieldName="CustNO" Format="" MaxLength="0" Width="80" />
                        <JQTools:JQGridColumn Alignment="center" Caption="客戶簡稱" Editor="text" FieldName="CustName" Format="" MaxLength="0" Width="90" />
                        <JQTools:JQGridColumn Alignment="center" Caption="發票年月" Editor="text" FieldName="InvoiceYM2" Format="" MaxLength="0" Width="70" />
                        <JQTools:JQGridColumn Alignment="center" Caption="業務" Editor="text" FieldName="SalesName" Format="" MaxLength="0" Width="80" />
                        <JQTools:JQGridColumn Alignment="center" Caption="銷貨日期" Editor="datebox" FieldName="SalesDate" Format="" Width="120" />
                        <JQTools:JQGridColumn Alignment="center" Caption="交易別" Editor="text" FieldName="SalesTypeID" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" />
                        <JQTools:JQGridColumn Alignment="center" Caption="總額" Editor="numberbox" FieldName="CustAmt" Format="" Width="100" />
                        <JQTools:JQGridColumn Alignment="center" Caption="開發票" Editor="checkbox" FieldName="ov" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="50" FormatScript="genCheckBox" />
                    </Columns>
                    <TooItems>                  
                        <JQTools:JQToolItem Enabled="True" ItemType="easyui-linkbutton" OnClick="TransSys2" Text="重轉行政系統" Visible="True" Icon="icon-ok" />
                    </TooItems>
                    <QueryColumns>
                        <JQTools:JQQueryColumn AndOr="and" Caption="發票年月" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'InvoiceYM',textField:'InvoiceYM',remoteName:'sERPSalseDetailsTrans.infoInvoiceYM',tableName:'infoInvoiceYM',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OnSelectCondition,panelHeight:200" FieldName="InvoiceYM2" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="轉入條件" Condition="=" DataType="number" Editor="infocombobox" EditorOptions="valueField:'iAutoKey',textField:'Condition',remoteName:'sERPSalseDetailsTrans.infoTransCondition',tableName:'infoTransCondition',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesMasterNO" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="600" />
                    </QueryColumns>
                </JQTools:JQDataGrid>

            </div>
        </div>
</form>
</body>
</html>
