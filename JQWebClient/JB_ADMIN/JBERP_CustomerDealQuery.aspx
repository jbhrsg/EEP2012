<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_CustomerDealQuery.aspx.cs" Inherits="Template_JQuerySingle1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(function () {
                $("input, select, textarea").focus(function () {
                    $(this).css("background-color", "yellow");
                });
                $("input, select, textarea").blur(function () {
                    $(this).css("background-color", "white");
                });
            });
        });
        var DGVSalesTypeCount_FormatScript = function (value, row, index) {
            if (value >= 1) {
                var fieldName = this.field;
                return $('<a>', { href: 'javascript: void(0)', title: '', onclick: "DGVSalesTypeCount_CommandTrigger.call(this,'')" }).linkbutton({ iconCls: '', plain: false, text: value })[0].outerHTML;
            }
        }
        //DataGrid 中SalesTypeCount 點按觸發
        var DGVSalesTypeCount_CommandTrigger = function (command) {
            var rowIndex = parseInt($(this).closest('tr').attr('datagrid-row-index'));
            var rowData = $("#dataGridMaster").datagrid('selectRow', rowIndex).datagrid('getSelected');
            //取得並顯示銷貨類別日期資訊
            GetGridDataSalesTypeDate(rowData.CustomerID);
            return true;
        }
        function JQDialog1OnSubmited() {
        }
        function queryGrid(dg) {
            if ($(dg).attr('id') == 'dataGridMaster') {
                var Str = $('#CustomerName_Query').val();
                var QStr = String(Str).trim();
                if (QStr == '' || QStr == undefined) {
                    alert("注意!!請輸入客戶名稱-簡稱-電話-統編.....");
                    $('#CustomerName_Query').focus();
                    return;
                }
                var FiltStr = "(CustomerName LIKE '%" + QStr + "%')" + " OR (ShortName Like '%" + QStr + "%')  OR  (TelNO Like '%" + QStr + "%') OR (TaxNO Like '%" + QStr + "%') ";
                $(dg).datagrid('setWhere', FiltStr);
           }
          
        }
        function GetGridDataSalesTypeDate(CustomerID) {
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sCustomerDealQuery.Customer',
                data: "mode=method&method=" + "GetGridDataSalesTypeDate" + "&parameters=" + CustomerID,
                cache: false,
                async: false,
                success: function (data) {
                    //$.messager.progress({ msg: '資料下載中...', interval: '1000' });//進度條開始
                    var rows = $.parseJSON(data);
                    if (rows.length == 0) {
                        $('#JQDataGridSalesType').datagrid('loadData', []);//清空Grid資料
                    } else {
                        if (rows.length > 0) {
                            //通過loadData方法清除掉原有Grid中的舊有資料並填補分頁資料
                            $('#JQDataGridSalesType').datagrid({ loadFilter: pagerFilter, url: "" }).datagrid('loadData', rows);
                            openForm('#JQDialog1', {}, "", 'dialog');
                        }
                    }
                }
            }
      );
        }
    </script>   
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sCustomerDealQuery.Customer" runat="server" AutoApply="True"
                DataMember="Customer" Pagination="True" QueryTitle=""
                Title="客戶交易查詢" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="15,30,45,90" PageSize="15" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustomerID" MaxLength="0" Width="80" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustomerName" MaxLength="0" Width="260" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶簡稱" Editor="text" FieldName="ShortName" MaxLength="0" Width="100" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="電話號碼" Editor="text" FieldName="TelNO" MaxLength="0" Width="100" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="最早交易日" Editor="text" FieldName="FirstDealDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="最早交易類別" Editor="infocombobox" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sCustomerDealQuery.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="FirstSaleType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="最近交易日" Editor="text" FieldName="LastestDealDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="最近交易類別" Editor="infocombobox" FieldName="LastestSaleType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="100" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sCustomerDealQuery.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="交易類別數" Editor="text" FieldName="SalesTypeCount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="75" FormatScript="DGVSalesTypeCount_FormatScript">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="總交易筆數" Editor="text" FieldName="SalesCount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="75">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                <JQTools:JQToolItem Enabled="True" Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Export" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="請輸入公司名稱-簡稱-電話-統編" Condition="%%" 
                        DataType="string" Editor="text" FieldName="CustomerName" IsNvarChar="False" 
                        NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="250" />
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>
    <JQTools:JQDefault runat="server" BindingObjectID="dataGridMaster" BorderStyle="NotSet" Enabled="True" EnableTheming="True" ClientIDMode="Inherit" ID="defaultMaster" EnableViewState="True" ViewStateMode="Inherit" >
</JQTools:JQDefault>
<JQTools:JQValidate runat="server" BindingObjectID="dataGridMaster" BorderStyle="NotSet" Enabled="True" EnableTheming="True" ClientIDMode="Inherit" ID="validateMaster" EnableViewState="True" ViewStateMode="Inherit" >
</JQTools:JQValidate>
<JQTools:JQDialog ID="JQDialog1" runat="server" DialogLeft="320px" DialogTop="90px" Title="銷售類別列表" Width="430px" Closed="True" ShowSubmitDiv="False" DialogCenter="False" EditMode="Dialog" EnableTheming="True" ScrollBars="Vertical">
                 <JQTools:JQDataGrid ID="JQDataGridSalesType" runat="server" AlwaysClose="True" DataMember="SalesTypeDateList" RemoteName="sCustomerDealQuery.SalesTypeDateList" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AutoApply="False" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="20,40,60,80,100" PageSize="20" Pagination="True" QueryAutoColumn="False" QueryLeft="120px" QueryMode="Window" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnUpdate="" Height="450px" Width="355px" BufferView="False" NotInitGrid="False" RowNumbers="True">
                     <Columns>
                         <JQTools:JQGridColumn Alignment="left" Caption="交易類別" Editor="infocombobox" FieldName="SalesTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="140" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sCustomerDealQuery.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                         </JQTools:JQGridColumn>
                         <JQTools:JQGridColumn Alignment="center" Caption="最近交易日期" Editor="datebox" FieldName="SalesDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="90">
                         </JQTools:JQGridColumn>
                         <JQTools:JQGridColumn Alignment="center" Caption="類別交易筆數" Editor="text" FieldName="SalesCount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="90">
                         </JQTools:JQGridColumn>
                     </Columns>
                </JQTools:JQDataGrid>
            </JQTools:JQDialog>
</form>
</body>
</html>
