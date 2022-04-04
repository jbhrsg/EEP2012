<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_EmpPayQuery.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script src="../js/jquery.jbjob.js"></script>
     <script type="text/javascript">
     function LastYear() {
             var dt = new Date();
             var aDate = new Date($.jbDateAdd('years', -1, dt));
             return aDate.getFullYear();
         }


     </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sEmpYearPayQuery.EmpYearBill_JB" runat="server" AutoApply="True"
                DataMember="EmpYearBill_JB" Pagination="True" QueryTitle="查詢條件" EditDialogID="JQDialog1"
                Title="年度所得查詢" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True" Width="1020px" BufferView="False" NotInitGrid="False" RowNumbers="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="統一編證號" Editor="text" FieldName="IDNUMBER" Format="" MaxLength="0" Width="75" />
                    <JQTools:JQGridColumn Alignment="left" Caption="所得人姓名" Editor="text" FieldName="NAMEC" Format="" MaxLength="0" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="給付年度" Editor="text" FieldName="YEARNO" Format="" MaxLength="0" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="扣繳單位" Editor="text" FieldName="InsGroupName" Format="" MaxLength="0" Width="200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="所得所屬年月" Editor="text" FieldName="YM" Format="" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="格式" Editor="text" FieldName="IncomeType" Format="" MaxLength="0" Width="60" />
                    <JQTools:JQGridColumn Alignment="right" Caption="給付總額" Editor="numberbox" FieldName="IncomeAmt" Format="N" Width="80" />
                    <JQTools:JQGridColumn Alignment="right" Caption="扣繳稅額" Editor="numberbox" FieldName="IncomeTax" Format="N" Width="80" />
                    <JQTools:JQGridColumn Alignment="right" Caption="給付淨額" Editor="numberbox" FieldName="NetAmt" Format="N" Width="80" />
                    <JQTools:JQGridColumn Alignment="right" Caption="勞退自提金額" Editor="numberbox" FieldName="LpAmt" Format="N" Width="80" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="所得年度" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'YEARNO',textField:'YEARNO',remoteName:'sEmpYearPayQuery.PayYear',tableName:'PayYear',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="YEARNO" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="70" DefaultMethod="LastYear" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="年度所得查詢">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="EmpYearBill_JB" HorizontalColumnsCount="2" RemoteName="sEmpYearPayQuery.EmpYearBill_JB" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="統一編證號" Editor="text" FieldName="IDNUMBER" Format="" maxlength="0" Width="180" RowSpan="1" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="所得人姓名" Editor="text" FieldName="NAMEC" Format="" maxlength="0" Width="180" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="性別" Editor="text" FieldName="Gender" Format="" maxlength="0" Width="180" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="給付年度" Editor="text" FieldName="YEARNO" Format="" maxlength="0" Width="180" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="所得所屬年月" Editor="text" FieldName="YM" Format="" maxlength="0" Width="180" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="格式" Editor="text" FieldName="IncomeType" Format="" maxlength="0" Width="180" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="給付總額" Editor="numberbox" FieldName="IncomeAmt" Format="" Width="180" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="扣繳稅額" Editor="numberbox" FieldName="IncomeTax" Format="" Width="180" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="給付淨額" Editor="numberbox" FieldName="NetAmt" Format="" Width="180" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="勞退自提金額" Editor="numberbox" FieldName="LpAmt" Format="" Width="180" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="扣繳單位" Editor="text" FieldName="InsGroupName" Format="" Width="180" maxlength="0" Span="2" />
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
