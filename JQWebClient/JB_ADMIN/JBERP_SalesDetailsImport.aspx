<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_SalesDetailsImport.aspx.cs" Inherits="Template_JQuerySingle1" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="EFClientTools" namespace="EFClientTools" tagprefix="cc1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script>          
         
         $(document).ready(function () {
             //查詢條件預設值
             var sDate = new Date();
             var date = $.jbjob.Date.DateFormat(sDate, 'yyyy/MM/dd').substring(0, 7);
             $("#InvoiceYM_Query").val(date);//發票年月

             $("#toolItemdataGridView匯出Excel").hide();
             $("#toolItemdataGridView同步發票資訊").hide();
             $("#toolItemdataGridView寫入銷貨").hide();
         });
         //-------------------------------------------------------------------------------------
         function OnLoadSuccessGV() {
             //panel寬度調整
             var dgid = $('#dataGridView');
             var queryPanel = getInfolightOption(dgid).queryDialog;
             if (queryPanel)
                 $(queryPanel).panel('resize', { width: 800 });
             
         }

         function queryGrid(dg) {//查詢後添加固定條件
            var InvoiceYM = $('#InvoiceYM_Query').val();//發票年月           
             
            if (InvoiceYM == "") {
                alert('發票年月不可空白！');
                $('#dataGridView').datagrid('loadData', []);//清空Grid資料
            } else {

                //同步發票資訊檢查--1查詢,2修改
                var cnt;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalseDetailsImport.infoInvoiceList', //連接的Server端，command
                    data: "mode=method&method=" + "UpdateCustomerInvoiceData" + "&parameters=" + InvoiceYM + ",1", //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                    cache: false,
                    async: false,
                    success: function (data) {                        
                        cnt = data;
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        alert(xhr.status);
                        alert(thrownError);
                    }
                });
                if (cnt > 0) {
                    $("#toolItemdataGridView同步發票資訊").show();
                    $("#toolItemdataGridView寫入銷貨").hide();
                    $('#dataGridView').datagrid('loadData', []);//清空Grid資料
                }
                else {
                    ProcessSalesData("1");
                }
            }
        
         }
         //1查詢媒體銷貨 or 3寫銷貨
         function ProcessSalesData(sType) {
             var InvoiceYM = $('#InvoiceYM_Query').val();//發票年月
             var SalesID = $("#SalesID_Query").combobox('getValue');//業務
             var SalesTypeID = $("#SalesTypeID_Query").combobox('getValue');//交易別
             var CustNO = $("#CustNO_Query").combobox('getValue');
             var IsEmail = $("#IsEmail_Query").combobox('getValue');//0不拘.1無Email,2有Email

             //--------------------
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalseDetailsImport.infoInvoiceList',  //連接的Server端，command
                 data: "mode=method&method=" + "ImportSalesFromERPSalesMaster" + "&parameters=" + InvoiceYM + "," + SalesID + "," + SalesTypeID + "," + CustNO + "," + IsEmail + "," + sType,
                 cache: false,
                 async: true,
                 success: function (data) {
                     var rows = $.parseJSON(data);//將JSon轉會到Object類型提供給Grid顯示                            

                     if (sType == "1") {//1查詢媒體銷貨
                         if (rows.length == 0) {
                             $('#dataGridView').datagrid('loadData', []);//清空Grid資料
                         } else {
                             $("#toolItemdataGridView同步發票資訊").hide();
                             $("#toolItemdataGridView寫入銷貨").show();
                             $("#toolItemdataGridView匯出Excel").show();
                             $('#dataGridView').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                         }
                     } else if (sType == "3") {//3寫銷貨
                         alert('寫入銷貨完成！');
                         queryGrid($('#dataGridView'));
                     }

                 },
                 error: function (xhr, ajaxOptions, thrownError) {
                     alert(xhr.status);
                     alert(thrownError);
                 }
             });
             //--------------------

         }



         //錯誤代碼變色
         function ScriptsCheck(val, rowData) {
             if (rowData.sCheck != "") {

                 return "<div style=\"color:Red\">" + val + "</div>";

             }
             return val;
         }
         //筆數&加總
         function ScriptCustAmt(val, rowData) {
             if (rowData.iAutoKey == "0") {
                 return "<div style=\"color:Blue;font-weight:bold\">" + val + "</div>";
             }
             return val;
         }
         //匯出Excel
         function AutoExcel() {
             var InvoiceYM = $('#InvoiceYM_Query').val();//發票年月
             var SalesID = $("#SalesID_Query").combobox('getValue');//業務
             var SalesTypeID = $("#SalesTypeID_Query").combobox('getValue');//交易別
             var CustNO = $("#CustNO_Query").combobox('getValue');
             var IsEmail = $("#IsEmail_Query").combobox('getValue');//0不拘.1無Email,2有Email
             var Type = "2";//1查詢,2匯出Excel,3寫銷貨        

             $.ajax({
                 url: '../handler/JBHRISUseCaseHandler.ashx?' + $.param({ mode: 'CallServerMethodReturnFile', remoteName: 'sERPSalseDetailsImport', method: 'SalseDetailsAutoExcel' }),

                 data: "&parameters=" + InvoiceYM + "," + SalesID + "," + SalesTypeID + "," + CustNO + "," + IsEmail + "," + Type,

                 type: 'POST',
                 async: true,
                 success: function (data) {
                     //Json.FileName
                     var Json = $.parseJSON(data);
                     if (Json.IsOK) {
                         var Url = $('<a>', {
                             href: '../handler/JbExcelHandler.ashx?' + $.param({ mode: 'FileDownload', DownloadName:InvoiceYM+ '銷貨明細.xls', FilePathName: Json.FileStreamOrFileName }),
                             target: '_blank'

                         }).html('檔案下載')[0].outerHTML;

                         $.messager.alert('下載', Url, '');

                     }

                     else $.messager.alert('錯誤', Json.Msg, 'error');

                 },

                 beforeSend: function () { $.messager.progress({ msg: '執行中...' }); },

                 complete: function () { $.messager.progress('close'); },

                 error: function (xhr, ajaxOptions, thrownError) { alert('error'); }

             });
         }

         //寫入銷貨
         function InsertERPSalesDetails() {


             var rows = $('#dataGridView').datagrid('getRows');
             
             if (rows.length > 0) {
                 var scheck = rows[0].sCheck;
                 if (scheck != "") {
                     alert('尚有錯誤資料！');
                 } else {
                     var pre = confirm("確定寫入銷貨?");
                     if (pre == true) {
                         $("#toolItemdataGridView寫入銷貨").hide();
                         ProcessSalesData("3");
                        
                     }
                 }
             } else {
                 alert('無資料！');
             }
         }
         //同步發票資訊
         function UpdateCustInfo() {
             var InvoiceYM = $('#InvoiceYM_Query').val();//發票年月

             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalseDetailsImport.infoInvoiceList', //連接的Server端，command
                 data: "mode=method&method=" + "UpdateCustomerInvoiceData" + "&parameters=" + InvoiceYM + ",2", //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                 cache: false,
                 async: false,
                 success: function (data) {
                     alert('資料同步成功！');
                     //ProcessSalesData("1");//再查詢一次
                     queryGrid($('#dataGridView'));
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

        <div id="Dialog_dataGridMasterData">
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERPSalseDetailsImport.infoInvoiceList" runat="server" AutoApply="False"
                DataMember="infoInvoiceList" Pagination="False" QueryTitle="" EditDialogID="JQDialog1"
                Title="銷貨列表" QueryMode="Panel" OnLoadSuccess="OnLoadSuccessGV" AllowAdd="True" AllowDelete="True" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Height="450px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="業務" Editor="text" FieldName="SalesID" Format="" Width="50" />
                    <JQTools:JQGridColumn Alignment="center" Caption="交易別" Editor="text" FieldName="SalesTypeID" Format="" MaxLength="0" Width="55" />
                    <JQTools:JQGridColumn Alignment="left" Caption="付款" Editor="text" FieldName="PayWayIDText" Format="" MaxLength="0" Width="65" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustNO" Format="" MaxLength="0" Width="85" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustName" Format="" Width="160" FormatScript="ScriptsCheck" />
                    <JQTools:JQGridColumn Alignment="center" Caption="單據" Editor="text" FieldName="QInvoiceTypeText" Format="" MaxLength="0" Width="65" />
                    <JQTools:JQGridColumn Alignment="center" Caption="統一編號" Editor="text" FieldName="TaxNO" Format="" Width="85" />
                    <JQTools:JQGridColumn Alignment="right" Caption="未稅金額" Editor="numberbox" FieldName="CustAmt" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Total="" Visible="True" Width="85" OnTotal="" FormatScript="ScriptCustAmt">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="結帳日" Editor="text" FieldName="BalanceDate" Format="" MaxLength="0" Width="60" FormatScript="ScriptCustAmt" />
                    <JQTools:JQGridColumn Alignment="left" Caption="電子發票Email" Editor="text" FieldName="ElectronicEmail" Format="" MaxLength="0" Width="170" />
                    <JQTools:JQGridColumn Alignment="center" Caption="錯誤代碼" Editor="text" FieldName="sCheck" Format="" MaxLength="0" Width="80" FormatScript="ScriptsCheck" />
                    <JQTools:JQGridColumn Alignment="left" Caption="iAutoKey" Editor="text" FieldName="iAutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                </Columns>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="發票年月" Condition="%" DataType="string" Editor="text" FieldName="InvoiceYM" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="70" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="業務" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sERPSalseDetailsImport.infoSalesMan',tableName:'infoSalesMan',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="110" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶代號" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CustNO',textField:'CustShortName',remoteName:'sERPSalseDetailsImport.infoCustomersAll',tableName:'infoCustomersAll',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CustNO" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="交易別" Condition="=" DataType="string" Editor="infocombobox" FieldName="SalesTypeID" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="85" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERPSalseDetailsImport.infoSalesType',tableName:'infoSalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="Email" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'2',text:'有',selected:'false'},{value:'1',text:'無',selected:'false'},{value:'0',text:'不拘',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="IsEmail" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="110" />
                </QueryColumns>
                <TooItems>                  
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="AutoExcel" Text="匯出Excel" Visible="True" />
                    <JQTools:JQToolItem Icon="icon-next" ItemType="easyui-linkbutton" OnClick="UpdateCustInfo" Text="同步發票資訊" />
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="InsertERPSalesDetails" Text="寫入銷貨" />
                </TooItems>
            </JQTools:JQDataGrid>

            </div>
        </div>
</form>
</body>
</html>
