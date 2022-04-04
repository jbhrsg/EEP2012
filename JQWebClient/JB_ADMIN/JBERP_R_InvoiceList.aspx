<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_R_InvoiceList.aspx.cs" Inherits="Template_JQuerySingle1" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="EFClientTools" namespace="EFClientTools" tagprefix="cc1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script>                                
         $(document).ready(function () {
             $("input, select, textarea").focus(function () {
                 $(this).css("background-color", "#FFFFB5");
             });

             $("input, select, textarea").blur(function () {
                 $(this).css("background-color", "white");
             });                          
            
             
         });
                    
         function genCheckBox(val) {
             if (val != "0")
                 return "<input  type='checkbox' checked='true' onclick='return false;'/>";
             else
                 return "<input  type='checkbox' onclick='return false;'/>";
         }

         function OnLoadSuccessGV() {
             //panel寬度調整
             var dgid = $('#dataGridMaster');
             var queryPanel = getInfolightOption(dgid).queryDialog;
             if (queryPanel)
                 $(queryPanel).panel('resize', { width: 600 });
             //Grid隱藏
             $('#dataGridMaster').datagrid('getPanel').hide();

             //查詢條件預設值
             var sDate = new Date();
             var date = $.jbjob.Date.DateFormat(sDate, 'yyyy/MM/dd').substring(0, 7);
             $("#InvoiceYM_Query").val(date);//發票年月

             $('#GrantTypeID_Query').options('setValue', 3);
             LinkToDoList();
         }

         function queryGrid(dg) {//查詢後添加固定條件
             if ($(dg).attr('id') == 'dataGridMaster') {
                 var InvoiceYM = $('#InvoiceYM_Query').val();//發票年月
                 var SalesID = $("#SalesID_Query").combobox('getValue');//業務
                 var SalesTypeID = $("#SalesTypeID_Query").combobox('getValue');//交易別
                 var CustNO = $("#CustNO_Query").combobox('getValue');
                 var IsTransSys = $("#IsTransSys_Query").combobox('getValue');
                 var Type = $('#GrantTypeID_Query').options('getValue');//呈現種類	1總表,2明細表,3電子發票用(已開金額抓EEP發票檔)

                 var url = "../JB_ADMIN/REPORT/Media/JBERP_R_InvoiceListReportView.aspx?InvoiceYM=" + InvoiceYM + "&SalesID=" + SalesID + "&SalesTypeID=" + SalesTypeID + "&CustNO=" + CustNO + "&IsTransSys=" + IsTransSys + "&Type=" + Type;

                 var height = $(window).height() - 40;
                 var height2 = $(window).height() - 80;
                 var width = $(window).width() - 60;
                 var dialog = $('<div/>')
                 .dialog({
                     draggable: false,
                     modal: true,
                     height: height,
                     //top:0,
                     width: width,
                     title: "發票概算表",
                     //maximizable: true                              
                 });
                 $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="' + height2 + '"></iframe>').appendTo(dialog.find('.panel-body'));
                 dialog.dialog('open');
             }
            
         }
         //檢查一筆訂單有無重複發票年月
         function LinkToDoList() {
             var InvoiceYM = $('#InvoiceYM_Query').val();//發票年月           

             if (InvoiceYM != "") {                
              
                 var cnt;
                 $.ajax({
                     type: "POST",
                     url: '../handler/jqDataHandle.ashx?RemoteName=sR_SalesDetails.infoInvoiceList', //連接的Server端，command
                     data: "mode=method&method=" + "SalesDetailsCountRepeat" + "&parameters=" + InvoiceYM + ",1", //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                     cache: false,
                     async: false,
                     success: function (data) {
                         if (data != false) {
                             cnt = data;
                         }
                     },
                     error: function (xhr, ajaxOptions, thrownError) {
                         alert(xhr.status);
                         alert(thrownError);
                     }
                 });
                 //檢查一筆訂單有無重複發票年月
                 if (cnt != undefined) {
                     $('#LinkToDoList').html('提醒您！目前有 ' + cnt + ' 筆重複資訊。').css("background-color", "pink");
                 }
                 else {
                     $('#LinkToDoList').html('');
                 }
             }
         }

         //呼叫重複資訊頁面
         function OpenCountRepeat() {

             var InvoiceYM = $('#InvoiceYM_Query').val();//發票年月           

             if (InvoiceYM != "") {

                 var cnt;
                 $.ajax({
                     type: "POST",
                     url: '../handler/jqDataHandle.ashx?RemoteName=sR_SalesDetails.infoInvoiceList', //連接的Server端，command
                     data: "mode=method&method=" + "SalesDetailsCountRepeat" + "&parameters=" + InvoiceYM + ",2", //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                     cache: false,
                     async: false,
                     success: function (data) {
                         var rows = $.parseJSON(data);//將JSon轉會到Object類型提供給Grid顯示            
                         if (rows.length == 0) {
                             $('#JQCountRepeat').datagrid('loadData', []);//清空Grid資料
                         } else {
                             $('#JQCountRepeat').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                         }
                     },
                     error: function (xhr, ajaxOptions, thrownError) {
                         alert(xhr.status);
                         alert(thrownError);
                     }
                 });
             }
             openForm('#JQDialogCountRepeat', {}, 'viewed', 'dialog');

         }



    </script> 
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />


&nbsp;<div id="Dialog_dataGridMasterData">
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sR_SalesDetails.infoInvoiceList" runat="server" AutoApply="True"
                DataMember="infoInvoiceList" Pagination="True" QueryTitle="查詢條件"
                Title="" ReportFileName="~/JB_ADMIN/JBERP_R_SalesDetails.rdlc" QueryMode="Panel" DeleteCommandVisible="False" UpdateCommandVisible="False" ViewCommandVisible="False" AlwaysClose="True" OnLoadSuccess="OnLoadSuccessGV" >
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="SalesMasterNO" Editor="numberbox" FieldName="SalesMasterNO" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ItemSeq" Editor="text" FieldName="ItemSeq" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CustNO" Editor="text" FieldName="CustNO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CustShortName" Editor="text" FieldName="CustShortName" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesTypeID" Editor="text" FieldName="SalesTypeID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="DMTypeID" Editor="text" FieldName="DMTypeID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="SalesQty" Editor="numberbox" FieldName="SalesQty" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="AcceptDate" Editor="datebox" FieldName="AcceptDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="iday" Editor="text" FieldName="iday" Format="" MaxLength="0" Width="120" />
                </Columns>                 
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="發票年月" Condition="%" DataType="string" Editor="text" FieldName="InvoiceYM" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="70" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="業務" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sR_SalesDetails.infoSalesMan',tableName:'infoSalesMan',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="130" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="交易別" Condition="=" DataType="string" Editor="infocombobox" FieldName="SalesTypeID" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="75" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sR_SalesDetails.infoSalesTypeAll',tableName:'infoSalesTypeAll',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶代號" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CustNO',textField:'CustShortName',remoteName:'sR_SalesDetails.infoCustomersAll',tableName:'infoCustomersAll',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CustNO" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="是否匯入" Condition="=" DataType="number" Editor="infocombobox" FieldName="IsTransSys" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="90" EditorOptions="items:[{value:'0',text:'未匯入',selected:'true'},{value:'1',text:'已匯入',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Format="" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="呈現種類" Condition="=" DataType="string" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:110,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:3,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'電子發票報表',value:'3'}]" FieldName="GrantTypeID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>
            </div>
            <a href="#" class="easyui-linkbutton" OnClick="LinkToDoList()">查詢</a>&nbsp;&nbsp;&nbsp;<a href="#" id="LinkToDoList" onclick="OpenCountRepeat()"></a>
        </div>

        <JQTools:JQDialog ID="JQDialogCountRepeat" runat="server" DialogLeft="60px" DialogTop="20px" Title="重複年月客戶清單" ShowSubmitDiv="False" Width="570px" BindingObjectID="">
                <JQTools:JQDataGrid ID="JQCountRepeat" runat="server" AllowAdd="True" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" AutoApply="True" CheckOnSelect="True" ColumnsHibeable="False" DataMember="infoInvoiceList" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" Pagination="False" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTitle="" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sR_SalesDetails.infoInvoiceList" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="500px" BufferView="False" NotInitGrid="False" Height="390px" RowNumbers="True">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="訂單序號" Editor="text" FieldName="SalesMasterNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="70" />
                        <JQTools:JQGridColumn Alignment="center" Caption="業務" Editor="text" FieldName="SalesID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="98" />
                        <JQTools:JQGridColumn Alignment="left" Caption="客戶簡稱" Editor="text" FieldName="CustShortName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="110" />
                        <JQTools:JQGridColumn Alignment="Center" Caption="發票年月" Editor="text" FieldName="InvoiceYM" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="110" />
                    </Columns>
                </JQTools:JQDataGrid>
        </JQTools:JQDialog>

</form>
</body>
</html>
