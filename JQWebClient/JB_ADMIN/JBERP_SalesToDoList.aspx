<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_SalesToDoList.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script>                                
         $(document).ready(function () {
             //初始化查詢條件
             var sDate = new Date();
             var Date2 = $.jbjob.Date.DateFormat(sDate, 'yyyy/MM/dd');
             $("#MinSalesDate_Query").datebox('setValue', '2015/10/01');//開始日期
             $("#MaxSalesDate_Query").datebox('setValue', Date2);//結束日期
             //用戶編號=>業務代號
             var UserID = getClientInfo("UserID");
             setTimeout(function () {
                 var data = $("#SalesID_Query").combobox('getData');
                 for (var i = 0; i < data.length; i++) {
                     if (data[i].SalesEmployeeID == UserID) {
                         $("#SalesID_Query").combobox('setValue', data[i].SalesID);
                     }                    
                 }                
             }, 200);
         });
         function queryGrid(dg) {
             var result = [];
             var SalesID = $('#SalesID_Query').combobox('getValue');//業務代號
             var CustNO = $('#CustNO_Query').combobox('getValue');//客戶代號
             var MinSalesDate = $('#MinSalesDate_Query').datebox('getValue');//開始日期
             var MaxSalesDate = $('#MaxSalesDate_Query').datebox('getValue');//結束日期
             //到期客戶列表
             if ($(dg).attr('id') == 'dataGridSalesMaster') {                
                 if (SalesID != '') result.push("SalesID = '" + SalesID + "'");                 
                 if (CustNO != '') result.push("m.CustNO = '" + CustNO + "'");                 
                 if (MinSalesDate != '') result.push("MaxSalesDate >= '" + MinSalesDate + "'");                 
                 if (MaxSalesDate != '') result.push("MaxSalesDate <= '" + MaxSalesDate + "'");
                 $(dg).datagrid('setWhere', result.join(' and '));
             }
             //銷貨備註提醒列表 
             var result2 = [];
             if (SalesID != '') result2.push("SalesID = '" + SalesID + "'");
             if (CustNO != '') result2.push("m.CustNO = '" + CustNO + "'");
             if (MinSalesDate != '') result2.push("SalesDescrDate >= '" + MinSalesDate + "'");
             if (MaxSalesDate != '') result2.push("SalesDescrDate <= '" + MaxSalesDate + "'");
             $('#dataGridSalesDetails').datagrid('setWhere', result2.join(' and '));
         }
     
        function CheckNO() {
            //alert('111');
            //return false;
        }
        //天數提醒(主檔),是否失效(明細)CheckBox
        function genCheckBox(val) {
            if (val != "0")
                return "<input  type='checkbox' checked='true' />";
            else
                return "<input  type='checkbox' />";
        }
        function SalesMasterReload() {
            $('#dataGridSalesMaster').datagrid('reload');
        }
        function SalesDetailsReload() {
            $('#dataGridSalesDetails').datagrid('reload');
        }
       
    </script> 
    
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            &nbsp;<JQTools:JQDataGrid ID="dataGridSalesMaster" data-options="pagination:true,view:commandview" RemoteName="sERPSalesToDoList.ERPSalesMaster" runat="server" AutoApply="True"
                DataMember="ERPSalesMaster" Pagination="True" QueryTitle="" EditDialogID=""
                Title="到期客戶列表" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="15" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnUpdated="SalesMasterReload">
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
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="業務代號" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sERPSalseDetails.infoSalesMan',tableName:'infoSalesMan',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesID" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="85" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶代號" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CustNO',textField:'CustShortName',remoteName:'sERPSalseDetails.infoCustomersAll',tableName:'infoCustomersAll',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CustNO" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="250" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="開始日期" Condition="&gt;=" DataType="datetime" Editor="datebox" FieldName="MinSalesDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="85" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="結束日期" Condition="&lt;=" DataType="datetime" Editor="datebox" FieldName="MaxSalesDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="85" />
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>
                     <JQTools:JQDataGrid ID="dataGridSalesDetails" data-options="pagination:true,view:commandview" RemoteName="sERPSalesToDoList.ERPSalesDetails" runat="server" AutoApply="True"
                DataMember="ERPSalesDetails" Pagination="True" QueryTitle="" EditDialogID=""
                Title="銷貨備註提醒列表" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnUpdated="SalesDetailsReload">
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
</form>
</body>
</html>
