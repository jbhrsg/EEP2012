<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FWCRMStickStatus.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script>  
        var flag = true; //定義一個全域變數，只有第一次執行

        $(document).ready(function () {            

            //設定 誤餐申請紀錄 dialog
            //initStickStatus();

        });
        var OnLoadSuccess = function (data) {
            if (flag) {
   
                var UserID = getClientInfo("UserID");
                //登入的工號得出查詢權限
                var Status = "";//0 無查詢權限,1 業務=>查詢自己 ,2 SelectAdmin =>全部查詢
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sFWCRMStickStatus.FWCRMStickStatus', //連接的Server端，command
                    data: "mode=method&method=" + "getSelectRange" + "&parameters=" + UserID,
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != false) {
                            Status = $.parseJSON(data);
                        }
                    }
                });
                var obj = $($('#querydataGridMaster').find('input')[0]);
                if (Status != 2) {
                    obj.combobox('setValue', UserID);
                    obj.combobox("disable");
                }
                //求得系統設定 => 設定訂單進度全查詢人員
                //var WhereString = "";
                //WhereString = WhereString + "A.EMPLOYEE_CODE = '" + userid + "';";  //11 = 11
                //WhereString = WhereString + "M.EMPLOYEE_CODE <> '" + userid + "';";   //12 = 12
                //$("#dataGridMaster").datagrid('setWhere', WhereString);

                flag = false;
            }
        }
        // 訂單進度查詢 dialog
        function initStickStatus() {
            $("#JQDialog_StickStatus").dialog(
            {
                height: 400,
                width: 850,
                left: 100,
                top: 80,
                resizable: false,
                modal: true,
                title: "訂單進度查詢",
                closed: true
            });
        };
        //訂單進度查詢
        function StickStatusLink(value, row, index) {
            return $('<a>', { href: 'javascript:void(0)', name: 'StickStatusLink', onclick: 'LinkStickStatus.call(this)', rowIndex: index }).linkbutton({ plain: false, text: '紀錄' })[0].outerHTML
        }

        // open訂單進度查詢畫面 dialog
        function LinkStickStatus() {
            //alert(index)
            var index = $(this).attr('rowIndex');
            $("#dataGridMaster").datagrid('selectRow', index);
            var rows = $("#dataGridMaster").datagrid('getSelected');
            var OrderNo = rows.OrderNo;
            $("#dataGridStickStatus").datagrid('setWhere', "o.OrderNo = '" + OrderNo + "'");
            openForm('#JQDialog_StickStatus', {}, 'viewed', 'dialog');

            //$("#JQDialog_StickStatus").dialog("open");
        }

        //Grid下載檔案 => 聘工表檔案 
        //欄值,row,index
        function downloadScript(val, rowData, index) {
            return '<a href="../handler/JqFileHandler.ashx?File=/FWCRM/Orders/' + val + '">' + val + '</a>';
        }
        //剩餘人數 QtyResult =>變紅色
        function QtyResultScript(val, rowData, index) {
            return '<span style="color:red"> ' + val + '</span>';
        }
    </script>  
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sFWCRMStickStatus.FWCRMOrders" runat="server" AutoApply="True"
                DataMember="FWCRMOrders" Pagination="True" QueryTitle=""
                Title="訂單進度查詢" AllowAdd="True" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnLoadSuccess="OnLoadSuccess" EditDialogID="">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="挑工進度" Editor="text" FieldName="StickStatusLink" FormatScript="StickStatusLink" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="訂單編號" Editor="text" FieldName="OrderNo" Format="" MaxLength="0" Width="100" Visible="True" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="雇主名稱" Editor="text" FieldName="EmployerName" Format="" MaxLength="0" Width="200" Visible="True" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="訂單人數" Editor="text" FieldName="PersonQtyOriginal" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="已入境人數" Editor="text" FieldName="PersonQty" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="剩餘人數" Editor="text" FieldName="QtyResult" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" FormatScript="QtyResultScript">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="引進國別" Editor="text" FieldName="NationalityName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="負責業務" Editor="text" FieldName="NAME_C" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="聘工表號碼" Editor="text" FieldName="WorkNo" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="聘工表檔案" Editor="text" FieldName="WorkImg" FormatScript="downloadScript" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="150">
                    </JQTools:JQGridColumn>
                </Columns> 
                 <TooItems>
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                </TooItems>              
                <QueryColumns>
                        <JQTools:JQQueryColumn AndOr="and" Caption="負責業務" Condition="=" DataType="string" Editor="infocombobox" FieldName="SalesID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" EditorOptions="valueField:'EmpID',textField:'NAME_C',remoteName:'sFWCRMStickStatus.infoSalesID',tableName:'infoSalesID',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="雇主名稱" Condition="%" DataType="string" Editor="text" FieldName="EmployerName" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="訂單編號" Condition="=" DataType="string" Editor="text" EditorOptions="" FieldName="OrderNo" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>

            <JQTools:JQDialog ID="JQDialog_StickStatus" runat="server" BindingObjectID="" Title="訂單進度查詢" ShowSubmitDiv="False" Width="850px" DialogLeft="10px" DialogTop="10px">
                <JQTools:JQDataGrid ID="dataGridStickStatus" data-options="pagination:true,view:commandview" RemoteName="sFWCRMStickStatus.FWCRMStickStatus" runat="server" AutoApply="True"
                DataMember="FWCRMStickStatus" Pagination="True" QueryTitle=""
                Title="" AllowAdd="True" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnLoadSuccess="OnLoadSuccess">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="訂單編號" Editor="text" FieldName="OrderNo" Format="" MaxLength="0" Width="90" Visible="False" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="雇主名稱" Editor="text" FieldName="EmployerName" Format="" MaxLength="0" Width="155" Visible="False" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="WorkNo" Editor="text" FieldName="WorkNo" Format="" MaxLength="0" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="center" Caption="日期" Editor="datebox" FieldName="StatusDate" Format="" Sortable="True" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="狀態" Editor="text" FieldName="StatusName" Format="" MaxLength="0" Width="125" />
                        <JQTools:JQGridColumn Alignment="center" Caption="人數" Editor="numberbox" FieldName="PersonQty" Format="" Width="50" />
                        <JQTools:JQGridColumn Alignment="left" Caption="進度/結果" Editor="text" FieldName="StatusResult" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="150">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="Notes" Format="" MaxLength="0" Width="346" />
                        <JQTools:JQGridColumn Alignment="center" Caption="負責業務" Editor="text" FieldName="NAME_C" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="65">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="right" Caption="iAutoKey" Editor="numberbox" FieldName="iAutoKey" Format="" Width="120" Visible="False" />
                    </Columns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                    </TooItems>
                </JQTools:JQDataGrid>
            </JQTools:JQDialog>
</form>
</body>
</html>
