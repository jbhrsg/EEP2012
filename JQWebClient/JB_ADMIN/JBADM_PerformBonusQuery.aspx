<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBADM_PerformBonusQuery.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
        });

        function queryGrid(dg) {
            if ($(dg).attr('id') == 'dataGridView') {
                var FiltStr = '';
                var YMS = $("#PerfBonusYMS_Query").combobox('getValue');
                var YME = $("#PerfBonusYME_Query").combobox('getValue');
                if (YMS != '' && YME != '') {
                    FiltStr = "(PerfBonusYM>='" + YMS + "' AND PerfBonusYM<='"+YME+"')";
                }
                var Str = $("#OrgParent_Query").combobox('getValue');
                if (Str != '' && Str != undefined) {
                    FiltStr = FiltStr + " And (Org_NOParent = '" + Str + "')";
                }
                $(dg).datagrid('setWhere', FiltStr);
            }
        }
        function PerfBonusYMSOnSelect(rows) {
            $("#PerfBonusYME_Query").combobox('setValue', rows.PerfBonusYM);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sPerformBonusQuery.PerfBonusDetails" runat="server" AutoApply="True"
                DataMember="PerfBonusDetails" Pagination="True" QueryTitle="績效獎金查詢報表" EditDialogID="JQDialog1"
                Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="合計:" UpdateCommandVisible="False" ViewCommandVisible="True" Width="660px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="績效年月" Editor="text" FieldName="PerfBonusYM" Format="" MaxLength="0" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="部門代號" Editor="text" FieldName="Org_NOParent" Format="" MaxLength="0" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="部門" Editor="text" FieldName="OrgParentName" Format="" MaxLength="0" Width="180" />
                    <JQTools:JQGridColumn Alignment="left" Caption="工號" Editor="text" FieldName="EMPID" Format="" MaxLength="0" Width="45" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="姓名" Editor="text" FieldName="EmpName" Format="" MaxLength="0" Width="80" />
                    <JQTools:JQGridColumn Alignment="right" Caption="績效金額" Editor="numberbox" FieldName="BonusAmt" Format="N0" MaxLength="0" Width="100" Total="sum" />
                </Columns>
                <TooItems>
                  <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                  <%--  <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />--%>
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="起迄年月" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'PerfBonusYM',textField:'PerfBonusYM',remoteName:'sPerformBonusQuery.PerfBonusYM',tableName:'PerfBonusYM',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:PerfBonusYMSOnSelect,panelHeight:200" FieldName="PerfBonusYMS" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="-" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'PerfBonusYM',textField:'PerfBonusYM',remoteName:'sPerformBonusQuery.PerfBonusYM',tableName:'PerfBonusYM',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="PerfBonusYME" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="績效部門" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'Org_NOParent',textField:'OrgParentName',remoteName:'sPerformBonusQuery.OrgParent',tableName:'OrgParent',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="OrgParent" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="績效獎金查詢報表">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="PerfBonusDetails" HorizontalColumnsCount="2" RemoteName="sPerformBonusQuery.PerfBonusDetails" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="ApplyEmpID" Editor="text" FieldName="ApplyEmpID" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="部門代號" Editor="text" FieldName="Org_NOParent" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="部門名稱" Editor="text" FieldName="OrgParentName" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="績效年月" Editor="text" FieldName="PerfBonusYM" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="工號" Editor="text" FieldName="EMPID" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="姓名" Editor="text" FieldName="EmpName" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="績效金額" Editor="numberbox" FieldName="BonusAmt" Format="" Width="180" />
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
