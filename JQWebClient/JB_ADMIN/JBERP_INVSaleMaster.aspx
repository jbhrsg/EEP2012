<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_INVSaleMaster.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
     var backcolor = "#cbf1de"
     $(document).ready(function () {
       $(function () {
             $("input, select, textarea").focus(function () {
                 $(this).css("background-color", "yellow");
             });
             $("input, select, textarea").blur(function () {
                 $(this).css("background-color", backcolor);
             });
       });
      
     });
     var DGOrderItems_FormatScript = function (value, row, index) {
         if (value > 1) {
             var fieldName = this.field;
             return $('<a>', { href: 'javascript: void(0)', title: '', onclick: "DGOrderItems_CommandTrigger.call(this,'')" }).linkbutton({ iconCls: '', plain: true, text: value })[0].outerHTML;
         }
     }
     var DGOrderItems_CommandTrigger = function (command) {
         var rowIndex = parseInt($(this).closest('tr').attr('datagrid-row-index'));
         var rowData = $("#dataGridView").datagrid('selectRow', rowIndex).datagrid('getSelected');
         var FiltStr = "OrderID = " + "'" + rowData.OrderID + "'";
         $("#JQDataGridDetails").datagrid('setWhere', FiltStr);
         openForm('#JQDialogDetails', {}, "", 'dialog');
         return true;
     }
     function JQDialogDetailsOnSubmited() {
     }
     function JQDataGridDetailsOnSelect(rowIndex, rowData) {
         var CenterName = '銷貨明細';
         $("#JQDataGridDetails").datagrid('getPanel').panel('setTitle', CenterName);
         $("#JQDataGridDetails").datagrid('options').title = CenterName;
     }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sINVSaleMaster.INVSaleMaster" runat="server" AutoApply="True"
                DataMember="INVSaleMaster" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="電子發票作業維護" AllowAdd="False" AllowDelete="False" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="center" Caption="發票號碼" Editor="text" FieldName="InvoiceNumber" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="發票日期" Editor="text" FieldName="InvoiceDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="銷貨單號" Editor="text" FieldName="OrderID" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="center" Caption="銷貨日期" Editor="datebox" FieldName="OrderDate" Format="" Visible="true" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="銷售種類" Editor="infocombobox" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sINVSaleMaster.SaleType',tableName:'SaleType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="SaleType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="BuyerName" Format="" MaxLength="0" Visible="true" Width="130" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶統編" Editor="text" FieldName="BuyerIdentifier" Format="" MaxLength="0" Visible="true" Width="70" />
                    <JQTools:JQGridColumn Alignment="right" Caption="銷售合計" Editor="text" FieldName="SaleTotal" Format="N" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="營業稅" Editor="text" FieldName="SaleTax" Format="N" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="總計" Editor="text" FieldName="SaleTotalTax" Format="N" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="明細" Editor="text" FieldName="OrderItems" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40" FormatScript="DGOrderItems_FormatScript">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="載具類別" Editor="infocombobox" FieldName="DonateMark" Format="" MaxLength="0" Visible="true" Width="60" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sINVSaleMaster.DonateMark',tableName:'DonateMark',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="稅別" Editor="infocombobox" FieldName="TaxType" Format="" MaxLength="0" Visible="true" Width="60" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sINVSaleMaster.TaxType',tableName:'TaxType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="發票方式" Editor="infocombobox" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sINVSaleMaster.InvoiceType',tableName:'InvoiceType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="InvoiceType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="狀態" Editor="infocombobox" FieldName="OrderStatus" Format="" MaxLength="0" Visible="true" Width="80" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sINVSaleMaster.OrderStatus',tableName:'OrderStatus',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="BuyerCustomerNumber" Editor="text" FieldName="BuyerCustomerNumber" Format="" MaxLength="0" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ProductionCode" Editor="text" FieldName="ProductionCode" Format="" MaxLength="0" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CustNO" Editor="text" FieldName="CustNO" Format="" MaxLength="0" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="OrderStdDate" Editor="datebox" FieldName="OrderStdDate" Format="" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="OrderEndDate" Editor="datebox" FieldName="OrderEndDate" Format="" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="BuyerAddress" Editor="text" FieldName="BuyerAddress" Format="" MaxLength="0" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="TaxRate" Editor="numberbox" FieldName="TaxRate" Format="" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="BuyerPersonInCharge" Editor="text" FieldName="BuyerPersonInCharge" Format="" MaxLength="0" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="BuyerTelephoneNumber" Editor="text" FieldName="BuyerTelephoneNumber" Format="" MaxLength="0" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="BuyerFacsimileAddress" Editor="text" FieldName="BuyerFacsimileAddress" Format="" MaxLength="0" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="BuyerEmailAddress" Editor="text" FieldName="BuyerEmailAddress" Format="" MaxLength="0" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PayWay" Editor="text" FieldName="PayWay" Format="" MaxLength="0" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="InsGroupID" Editor="text" FieldName="InsGroupID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" MaxLength="0" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Visible="False" Width="120" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="ImportOrder" Text="匯入銷貨" Visible="True" />
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"   OnClick="UploadInvoice" Text="上傳產生發票"/>
                    <JQTools:JQToolItem Icon="icon-cut" ItemType="easyui-linkbutton"   OnClick="CancelInvoice" Text="註銷發票"/>
                    <JQTools:JQToolItem Icon="icon-sum" ItemType="easyui-linkbutton"   OnClick="UploadAllowance" Text="開立折讓單" />
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"   OnClick="CancelAllowance" Text="註銷折讓單"/>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"   OnClick="CancelAllowance" Text="發票開立確認"/>
                    <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出EXCEL" />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="電子發票作業維護">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="INVSaleMaster" HorizontalColumnsCount="2" RemoteName="sINVSaleMaster.INVSaleMaster" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="OrderID" Editor="text" FieldName="OrderID" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="OrderDate" Editor="datebox" FieldName="OrderDate" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ProductionCode" Editor="text" FieldName="ProductionCode" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CustNO" Editor="text" FieldName="CustNO" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="OrderStdDate" Editor="datebox" FieldName="OrderStdDate" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="OrderEndDate" Editor="datebox" FieldName="OrderEndDate" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="BuyerIdentifier" Editor="text" FieldName="BuyerIdentifier" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="BuyerName" Editor="text" FieldName="BuyerName" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="BuyerAddress" Editor="text" FieldName="BuyerAddress" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="BuyerPersonInCharge" Editor="text" FieldName="BuyerPersonInCharge" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="BuyerTelephoneNumber" Editor="text" FieldName="BuyerTelephoneNumber" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="BuyerFacsimileAddress" Editor="text" FieldName="BuyerFacsimileAddress" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="BuyerEmailAddress" Editor="text" FieldName="BuyerEmailAddress" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="BuyerCustomerNumber" Editor="text" FieldName="BuyerCustomerNumber" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="DonateMark" Editor="text" FieldName="DonateMark" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="TaxType" Editor="text" FieldName="TaxType" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="TaxRate" Editor="numberbox" FieldName="TaxRate" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="PayWay" Editor="text" FieldName="PayWay" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="OrderStatus" Editor="text" FieldName="OrderStatus" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="180" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
            <JQTools:JQDialog ID="JQDialogDetails" runat="server" DialogLeft="180px" DialogTop="120px" Title="銷貨明細" Width="635px" OnSubmited="JQDialogDetailsOnSubmited" Closed="True" ShowSubmitDiv="False">
                 <JQTools:JQDataGrid ID="JQDataGridDetails" runat="server" AlwaysClose="True" DataMember="INVSaleDetails" RemoteName="sINVSaleMaster.INVSaleDetails" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AutoApply="True" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Window" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True" OnUpdate="" Height="450px" Width="560px" BufferView="False" NotInitGrid="False" RowNumbers="True" OnSelect="JQDataGridDetailsOnSelect">
                    <Columns>
                      
                        <JQTools:JQGridColumn Alignment="left" Caption="項次" Editor="text" FieldName="Seq" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="訂單日期" Editor="datebox" FieldName="OrderDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="銷貨明細" Editor="text" FieldName="Description" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="數量" Editor="text" FieldName="Quantity" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="單位" Editor="text" FieldName="Unit" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="right" Caption="單價" Editor="text" FieldName="UnitPrice" Format="N" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                      
                     </Columns>
                </JQTools:JQDataGrid>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
