<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_SalesApplyQuery.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //設定 Grid QueryColunm Windows width=320px
            var dgid = $('#dataGridView');
            var queryPanel = getInfolightOption(dgid).queryDialog;
            //if (queryPanel)
            //    $(queryPanel).panel('resize', { width: 480 });
            $('.infosysbutton-q', '#querydataGridView').closest('td').attr({ 'align': 'middle' });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERPSalesApplyQuery.ERPSalesApplyMaster" runat="server" AutoApply="True"
                DataMember="ERPSalesApplyMaster" Pagination="True" QueryTitle="銷貨查詢條件" EditDialogID="JQDialog1"
                Title="銷貨查詢" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="20" QueryAutoColumn="False" QueryLeft="50px" QueryMode="Window" QueryTop="60px" RecordLock="False" RecordLockMode="None" TotalCaption="合計:" UpdateCommandVisible="False" ViewCommandVisible="True" Width="1080px" BufferView="False" NotInitGrid="False" RowNumbers="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="銷售類別" Editor="infocombobox" FieldName="SalesTypeID" Format="" Width="80" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERPSalesApplyQuery.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Frozen="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="銷貨單號" Editor="text" FieldName="SalesApplyNO" Format="" MaxLength="0" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶" Editor="text" FieldName="CustShortName" Format="" MaxLength="0" Width="90" EditorOptions="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="銷貨日期" Editor="datebox" FieldName="ApplyDate" Format="yyyy/mm/dd" Width="65" />
                    <JQTools:JQGridColumn Alignment="left" Caption="發票年月" Editor="text" FieldName="SalesYM" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="55" />
                    <JQTools:JQGridColumn Alignment="left" Caption="銷貨說明" Editor="text" FieldName="SalesOutLine" Format="" MaxLength="0" Width="180" />
                    <JQTools:JQGridColumn Alignment="right" Caption="銷貨金額" Editor="text" FieldName="InvoiceAmt" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70" Total="sum" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請者" Editor="inforefval" FieldName="ApplyEmpID" Format="" MaxLength="0" Width="60" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'sERPSalesApplyQuery.Employee',tableName:'Employee',columns:[],columnMatches:[],whereItems:[],valueField:'EmployeeID',textField:'EmployeeName',valueFieldCaption:'EmployeeID',textFieldCaption:'EmployeeName',cacheRelationText:true,checkData:true,showValueAndText:false,selectOnly:false" />
                    <JQTools:JQGridColumn Alignment="left" Caption="銷貨業務" Editor="inforefval" FieldName="SalesID" Format="" MaxLength="0" Width="60" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'sERPSalesApplyQuery.Sales',tableName:'Sales',columns:[],columnMatches:[],whereItems:[],valueField:'SalesID',textField:'SalesName',valueFieldCaption:'SalesID',textFieldCaption:'SalesName',cacheRelationText:true,checkData:true,showValueAndText:false,selectOnly:false" />
                    <JQTools:JQGridColumn Alignment="left" Caption="銷貨備註" Editor="text" FieldName="SalesNotes" Format="" MaxLength="0" Width="180" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="發票編號" Editor="text" FieldName="InvoiceNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="right" Caption="公司" Editor="inforefval" FieldName="InsGroupID" Format="" Width="70" Visible="False" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'sERPSalesApplyQuery.InsGroup',tableName:'InsGroup',columns:[],columnMatches:[],whereItems:[],valueField:'InsGroupID',textField:'InsGroupShortName',valueFieldCaption:'InsGroupID',textFieldCaption:'InsGroupShortName',cacheRelationText:true,checkData:true,showValueAndText:false,selectOnly:false" />
                    <JQTools:JQGridColumn Alignment="left" Caption="TaxNO" Editor="text" FieldName="TaxNO" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Contact" Editor="text" FieldName="Contact" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="flowflag" Editor="text" FieldName="flowflag" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="起始日期" Condition="&gt;=" DataType="datetime" DefaultValue="_today" Editor="datebox" FieldName="ApplyDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="終止日期" Condition="&lt;=" DataType="datetime" DefaultValue="_today" Editor="datebox" FieldName="ApplyDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="銷貨類別" Condition="=" DataType="number" Editor="infocombobox" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERPSalesApplyQuery.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTypeID" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="銷貨業務" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sERPSalesApplyQuery.Sales',tableName:'Sales',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="A.SalesID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶" Condition="%%" DataType="string" Editor="inforefval" EditorOptions="title:'搜尋客戶',panelWidth:350,remoteName:'sERPSalesApplyQuery.Customers',tableName:'Customers',columns:[],columnMatches:[],whereItems:[],valueField:'CustNO',textField:'CustShortName',valueFieldCaption:'客戶代號',textFieldCaption:'客戶名稱',cacheRelationText:true,checkData:true,showValueAndText:false,selectOnly:false" FieldName="A.CustNO" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="公司別" Condition="%%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'InsGroupID',textField:'InsGroupShortName',remoteName:'sERPSalesApplyQuery.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="InsGroupID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="銷貨資訊" Width="780px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="ERPSalesApplyMaster" HorizontalColumnsCount="4" RemoteName="sERPSalesApplyQuery.ERPSalesApplyMaster" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨單號" Editor="text" FieldName="SalesApplyNO" Format="" maxlength="0" Width="87" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨屬性" Editor="infocombobox" EditorOptions="valueField:'SalesItemID',textField:'SalesItemName',remoteName:'sERPSalesApplyQuery.SalesItem',tableName:'SalesItem',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesItemID" maxlength="0" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨金額" Editor="text" FieldName="InvoiceAmt" maxlength="0" Width="80" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請人員" Editor="infocombobox" FieldName="ApplyEmpID" Format="" maxlength="0" Width="90" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sERPSalesApplyQuery.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ApplyDate" Format="yyyy/mm/dd" Width="100" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="infocombobox" FieldName="InsGroupID" Format="" maxlength="0" Width="95" EditorOptions="valueField:'InsGroupID',textField:'InsGroupShortName',remoteName:'sERPSalesApplyQuery.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨客戶" Editor="text" FieldName="CustShortName" maxlength="0" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人" Editor="text" FieldName="Contact" Format="" maxlength="0" Width="96" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="統一編號" Editor="text" FieldName="TaxNO" Format="" maxlength="0" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨業務" Editor="infocombobox" FieldName="SalesID" Format="" maxlength="0" Width="90" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sERPSalesApplyQuery.Sales',tableName:'Sales',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨類別" Editor="infocombobox" FieldName="SalesTypeID" Format="" Width="90" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERPSalesApplyQuery.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨說明" Editor="text" FieldName="SalesOutLine" Format="" Width="468" Span="3" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨備註" Editor="text" FieldName="SalesNotes" Format="" maxlength="0" Width="613" Span="4" />
                        <JQTools:JQFormColumn Alignment="left" Caption="flowflag" Editor="text" FieldName="flowflag" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Visible="False" Width="180" />
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
