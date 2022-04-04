<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBRecruit_Customer.aspx.cs" Inherits="Template_JQuerySingle1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
         //將Focus 欄位背景顏色改為黃色------------
         $(function () {
                $("input, select, textarea").focus(function () {
                    $(this).css("background-color", "#FFFFDE");
                });
                $("input, select, textarea").blur(function () {
                    $(this).css("background-color", backcolor);
                });
         });
         //傳入客戶代號查詢,並呼叫編輯主畫面
         var parameter = Request.getQueryStringByName("CustID");
         if (parameter != "") {
             $("#dataGridView").datagrid('setWhere', "ERPCustomerID = '" + parameter + "'");
             setTimeout(function () {
                 openForm('#JQDialog1', $('#dataGridView').datagrid('getSelected'), "updated", 'dialog');
             }, 800);
         }
        });
   </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sJBRecruit_Customer.Customer" runat="server" AutoApply="True"
                DataMember="Customer" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="派遣客戶資料維護" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustomerID" Format="" MaxLength="0" Visible="True" Width="60" Frozen="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustomerName" Format="" MaxLength="0" Visible="true" Width="130" Frozen="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶統編" Editor="text" FieldName="CustomerTaxNO" Format="" MaxLength="0" Visible="true" Width="60" Frozen="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶簡稱" Editor="text" FieldName="CustomerShortName" Format="" MaxLength="0" Visible="true" Width="80" Frozen="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="行業類別" Editor="infocombobox" FieldName="IndustryType" Format="" Visible="true" Width="60" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sJBRecruit_Customer.ListTable_IndustryType',tableName:'ListTable_IndustryType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶區域" Editor="infocombobox" FieldName="SalesArea" Format="" Visible="true" Width="60" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sJBRecruit_Customer.ListTable_SalesArea',tableName:'ListTable_SalesArea',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="縣市" Editor="text" FieldName="Addr_Country" Format="" MaxLength="0" Visible="False" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="鄉鎮區" Editor="text" FieldName="Addr_City" Format="" MaxLength="0" Visible="False" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="地址" Editor="text" FieldName="CustomerAddress_B" Format="" MaxLength="0" Visible="true" Width="200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="郵遞區號" Editor="text" FieldName="ZIPCode" Format="" MaxLength="0" Visible="true" Width="60" />
                    <JQTools:JQGridColumn Alignment="right" Caption="業務單位" Editor="infocombobox" FieldName="SalesDepartment" Format="" Visible="true" Width="80" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sJBRecruit_Customer.ListTable_SalesDepartment',tableName:'ListTable_SalesDepartment',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="right" Caption="結帳日" Editor="infocombobox" FieldName="BalanceID" Format="" Visible="False" Width="60" EditorOptions="valueField:'BalanceID',textField:'BalanceName',remoteName:'sJBRecruit_Customer.BalanceType',tableName:'BalanceType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="right" Caption="發薪日" Editor="infocombobox" FieldName="SalaryDate" Format="" Visible="False" Width="60" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sJBRecruit_Customer.ReferenceTable_SalaryDate',tableName:'ReferenceTable_SalaryDate',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人1姓名" Editor="text" FieldName="ContactName_1" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人1信箱" Editor="text" FieldName="ContacteMail_1" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人1電話" Editor="text" FieldName="ContactTel_1" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人1手機" Editor="text" FieldName="ContactMobile_1" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人1傳真" Editor="text" FieldName="ContactFax_1" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人2姓名" Editor="text" FieldName="ContactName_2" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人2信箱" Editor="text" FieldName="ContacteMail_2" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人2電話" Editor="text" FieldName="ContactTel_2" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人2手機" Editor="text" FieldName="ContactMobile_2" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人2傳真" Editor="text" FieldName="ContactFax_2" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="有效" Editor="text" FieldName="IsActive" Format="L-V-" Visible="true" Width="40" />
                    <JQTools:JQGridColumn Alignment="right" Caption="業務姓名" Editor="infocombobox" FieldName="SalesID" Format="" Visible="true" Width="100" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sJBRecruit_Customer.ListTable_Handler',tableName:'ListTable_Handler',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="right" Caption="客戶等級" Editor="infocombobox" FieldName="CustomerLevel" Format="" Visible="true" Width="60" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sJBRecruit_Customer.ListTable_CustomerLevel',tableName:'ListTable_CustomerLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="薪資列印" Editor="text" FieldName="IsOpenPrint" Format="L-V-" Visible="true" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="加保證明" Editor="text" FieldName="IsInsAdd" Format="L-V-" Visible="true" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="退保證明" Editor="text" FieldName="IsInsLess" Format="L-V-" Visible="true" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="薪調證明" Editor="text" FieldName="IsInsAdjust" Format="L-V-" Visible="true" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="意外責任險" Editor="text" FieldName="IsAccident" Format="L-V-" Visible="true" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="團體保險" Editor="text" FieldName="IsGroup" Format="L-V-" Visible="true" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="Description" Format="" MaxLength="0" Visible="true" Width="180" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立者" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Visible="true" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="yyyy-mm-dd" Visible="true" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改者" Editor="text" FieldName="LastUpdateBy" Format="" MaxLength="0" Visible="true" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="LastUpdateDate" Format="yyyy-mm-dd" Visible="true" Width="60" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" Visible="False" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" Visible="False" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消" Visible="False"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" Visible="False" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶統編" Condition="%%" DataType="string" Editor="text" FieldName="CustomerTaxNO" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶名稱" Condition="%%" DataType="string" Editor="text" FieldName="CustomerName" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="派遣客戶資料維護" Width="670px" DialogTop="20px" DialogLeft="70px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="Customer" HorizontalColumnsCount="2" RemoteName="sJBRecruit_Customer.Customer" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="CustomerID" Editor="text" FieldName="CustomerID" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶統編" Editor="text" FieldName="CustomerTaxNO" Format="" maxlength="0" Width="180" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustomerName" Format="" maxlength="0" Width="180" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶簡稱" Editor="text" FieldName="CustomerShortName" Format="" maxlength="0" Width="180" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="行業類別" Editor="infocombobox" FieldName="IndustryType" Format="" Width="188" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sJBRecruit_Customer.ListTable_IndustryType',tableName:'ListTable_IndustryType',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="所屬業務單位" Editor="infocombobox" FieldName="SalesDepartment" Format="" Width="188" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sJBRecruit_Customer.ListTable_SalesDepartment',tableName:'ListTable_SalesDepartment',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" Span="1" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="所屬客戶區域" Editor="infocombobox" FieldName="SalesArea" Format="" maxlength="0" Width="187" ReadOnly="True" NewRow="False" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sJBRecruit_Customer.ListTable_SalesArea',tableName:'ListTable_SalesArea',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="所在縣市" Editor="text" FieldName="Addr_Country" Format="" maxlength="0" Width="180" ReadOnly="True" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="鄉鎮區" Editor="text" FieldName="Addr_City" Format="" maxlength="0" Width="180" ReadOnly="True" NewRow="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="郵遞區號" Editor="text" FieldName="ZIPCode" Format="" maxlength="0" Width="180" NewRow="False" ReadOnly="True" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="地址" Editor="text" FieldName="CustomerAddress_B" Format="" Width="440" Span="2" NewRow="True" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="結帳日" Editor="infocombobox" FieldName="BalanceID" Format="" Width="188" EditorOptions="valueField:'BalanceID',textField:'BalanceName',remoteName:'sJBRecruit_Customer.BalanceType',tableName:'BalanceType',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="發薪日" Editor="infocombobox" FieldName="SalaryDate" Format="" Width="188" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sJBRecruit_Customer.ReferenceTable_SalaryDate',tableName:'ReferenceTable_SalaryDate',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人1姓名" Editor="text" FieldName="ContactName_1" Format="" maxlength="0" Width="180" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人1信箱" Editor="text" FieldName="ContacteMail_1" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人1電話" Editor="text" FieldName="ContactTel_1" Format="" maxlength="0" Width="180" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人1手機" Editor="text" FieldName="ContactMobile_1" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人1傳真" Editor="text" FieldName="ContactFax_1" Format="" maxlength="0" Width="180" ReadOnly="True" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人2姓名" Editor="text" FieldName="ContactName_2" Format="" maxlength="0" Width="180" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人2信箱" Editor="text" FieldName="ContacteMail_2" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人2電話" Editor="text" FieldName="ContactTel_2" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人2手機" Editor="text" FieldName="ContactMobile_2" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人2傳真" Editor="text" FieldName="ContactFax_2" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="是否有效" Editor="checkbox" FieldName="IsActive" Format="" Width="10" />
                        <JQTools:JQFormColumn Alignment="left" Caption="業務姓名" Editor="infocombobox" FieldName="SalesID" Format="" Width="188" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sJBRecruit_Customer.ListTable_Handler',tableName:'ListTable_Handler',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶等級" Editor="infocombobox" FieldName="CustomerLevel" Format="" Width="188" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sJBRecruit_Customer.ListTable_CustomerLevel',tableName:'ListTable_CustomerLevel',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="薪資列印" Editor="checkbox" FieldName="IsOpenPrint" Format="" Width="10" />
                        <JQTools:JQFormColumn Alignment="left" Caption="加保證明" Editor="checkbox" FieldName="IsInsAdd" Format="" Width="10" />
                        <JQTools:JQFormColumn Alignment="left" Caption="退保證明" Editor="checkbox" FieldName="IsInsLess" Format="" Width="10" />
                        <JQTools:JQFormColumn Alignment="left" Caption="薪調證明" Editor="checkbox" FieldName="IsInsAdjust" Format="" Width="10" />
                        <JQTools:JQFormColumn Alignment="left" Caption="意外責任險" Editor="checkbox" FieldName="IsAccident" Format="" Width="10" />
                        <JQTools:JQFormColumn Alignment="left" Caption="團體保險" Editor="checkbox" FieldName="IsGroup" Format="" Width="10" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="textarea" FieldName="Description" Format="" maxlength="0" Width="440" EditorOptions="height:50" Span="2" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
