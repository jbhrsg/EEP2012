<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBREQ_PettyCashQuery.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.infosysbutton-q', '#querydataGridView').closest('td').attr('align', 'middle');
           
        });
        function dataGridViewOnLoadSucess() {
            var UserID = getClientInfo("UserID");
             
            $('#APPLYEMPID_Query').combobox('setWhere', "EmployeeID='" + UserID + "'");
           
        }
        function queryGrid(dg) {
            //alert('test');
            if ($('#APPLYEMPID_Query').combobox('getValue') == '') {
                var UserID = getClientInfo("UserID");
                $('#APPLYEMPID_Query').combobox('setValue', UserID);
            }
            var where = $(dg).datagrid('getWhere');
            $(dg).datagrid('setWhere', where);
        }
        
     </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sPettyCashRepo.PettyCash" runat="server" AutoApply="True"
                DataMember="PettyCash" Pagination="True" QueryTitle="查詢條件" EditDialogID="JQDialog1"
                Title="零用金個人查詢" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="" UpdateCommandVisible="False" ViewCommandVisible="True" Width="1080px" OnLoadSuccess="dataGridViewOnLoadSucess" BufferView="False" NotInitGrid="False" RowNumbers="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="申請單號" Editor="text" FieldName="PETTYCASHID" Format="" MaxLength="10" Width="70" Frozen="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請人" Editor="infocombobox" FieldName="APPLYEMPID" Format="" MaxLength="20" Width="60" Visible="True" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sPettyCashRepo.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Frozen="True" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="申請日期" Editor="datebox" FieldName="APPLYDATE" Format="yyyy/mm/dd" Width="65" FormatScript="" Frozen="True" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請人" Editor="text" FieldName="EmployeeName" Format="" MaxLength="0" Width="60" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請事由" Editor="text" FieldName="AccountNotes" Format="" MaxLength="256" Width="270" />
                    <JQTools:JQGridColumn Alignment="left" Caption="會計科目" Editor="text" FieldName="AccountName" Format="" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="center" Caption="給付方式" Editor="text" FieldName="PayTypeName" Format="" MaxLength="0" Width="55" Visible="True" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="未稅金額" Editor="numberbox" FieldName="PettyCashAmt" Format="" Width="60" Total="" />
                    <JQTools:JQGridColumn Alignment="right" Caption="稅額" Editor="numberbox" FieldName="PettyCashTax" Format="" Width="50" Total="" />
                    <JQTools:JQGridColumn Alignment="right" Caption="小計金額" Editor="numberbox" FieldName="SumAmt" Format="" Width="60" Total="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="憑證類型" Editor="text" FieldName="ProofTypeName" Format="" MaxLength="0" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="成本中心" Editor="text" FieldName="CostCenterName" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="支出憑據" Editor="numberbox" FieldName="ProofTypeID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="憑據號碼" Editor="text" FieldName="ProofNO" Format="" MaxLength="20" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="會計科目" Editor="text" FieldName="AccountID" Format="" MaxLength="10" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="給付方式" Editor="numberbox" FieldName="PayTypeID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="成本中心" Editor="text" FieldName="CostCenterID" Format="" MaxLength="10" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請部門" Editor="text" FieldName="ApplyOrg_NO" Format="" MaxLength="8" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="OrgName" Editor="text" FieldName="OrgName" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="年月" Editor="text" FieldName="AccountYM" Format="" MaxLength="10" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="IsSettleAccount" Editor="text" FieldName="IsSettleAccount" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SettleAccountDate" Editor="text" FieldName="SettleAccountDate" Format="" MaxLength="0" Width="120" Visible="False" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="申請員工" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sPettyCashRepo.Employee',tableName:'Employee',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="APPLYEMPID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" DefaultValue="_usercode" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="結帳起始日" Condition="&gt;=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'AccountDate',textField:'AccountDate',remoteName:'sPettyCashRepo.AccDateList',tableName:'AccDateList',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SettleAccountDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="結帳終止日" Condition="&lt;=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'AccountDate',textField:'AccountDate',remoteName:'sPettyCashRepo.AccDateList',tableName:'AccDateList',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SettleAccountDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="給付方式" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'PayTypeID',textField:'PayTypeName',remoteName:'sPettyCashRepo.PayType',tableName:'PayType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="PayTypeID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="零用金個人查詢" DialogLeft="15px" DialogTop="30px" Width="750px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="PettyCash" HorizontalColumnsCount="4" RemoteName="sPettyCashRepo.PettyCash" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" Width="600px" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="申請員工" Editor="text" FieldName="EmployeeName" Format="" maxlength="0" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請部門" Editor="text" FieldName="OrgName" Format="" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="APPLYDATE" Format="" maxlength="0" Width="100" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請事由" Editor="textarea" FieldName="AccountNotes" Format="" maxlength="256" Width="590" EditorOptions="height:60" Span="4" />
                        <JQTools:JQFormColumn Alignment="left" Caption="成本中心" Editor="text" FieldName="CostCenterName" Format="" maxlength="0" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="會計科目" Editor="text" FieldName="AccountName" Format="" maxlength="0" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="支出憑據" Editor="text" FieldName="ProofTypeName" Format="" maxlength="0" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="憑據號碼" Editor="text" FieldName="ProofNO" Format="" maxlength="20" Width="100" />
                        <JQTools:JQFormColumn Alignment="right" Caption="未稅金額" Editor="numberbox" FieldName="PettyCashAmt" Format="" Width="100" />
                        <JQTools:JQFormColumn Alignment="right" Caption="稅額" Editor="numberbox" FieldName="PettyCashTax" Format="" Width="100" />
                        <JQTools:JQFormColumn Alignment="right" Caption="合計金額" Editor="numberbox" FieldName="SumAmt" Format="" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="給付方式" Editor="text" FieldName="PayTypeName" Format="" maxlength="0" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="結案" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsSettleAccount" Format="" Span="1" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="結案日期" Editor="text" FieldName="SettleAccountDate" Format="" maxlength="0" Span="3" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="零用金單號" Editor="text" FieldName="PETTYCASHID" Format="" maxlength="10" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="工號" Editor="text" FieldName="APPLYEMPID" Format="" maxlength="20" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請部門" Editor="text" FieldName="ApplyOrg_NO" Format="" maxlength="8" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="成本中心" Editor="text" FieldName="CostCenterID" Format="" maxlength="10" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="支出憑據" Editor="numberbox" FieldName="ProofTypeID" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="會計科目" Editor="text" FieldName="AccountID" Format="" Width="180" maxlength="10" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="給付方式" Editor="numberbox" FieldName="PayTypeID" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="年月" Editor="text" FieldName="AccountYM" Format="" maxlength="10" Width="180" Visible="False" />
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
