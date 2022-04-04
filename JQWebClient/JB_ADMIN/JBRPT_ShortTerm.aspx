<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBRPT_ShortTerm.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script src="../js/jquery.jbjob.js"></script> 
     <script type="text/javascript">
         $(document).ready(function () {
             //將Focus 欄位背景顏色改為黃色
             $("input, select, textarea").focus(function () {
                 $(this).css("background-color", "yellow");
             });
             $("input, select, textarea").blur(function () {
                 $(this).css("background-color", "white");
             });
             $('#DLDays_Query', '#querydataGridView').blur(function () {
                 if ($('#DLDays_Query').val() > 0)
                     $('#Flowflag_Query').combobox('setValue', 'P');
                 else
                     $('#Flowflag_Query').combobox('setValue', '');
             });
         });
         function dataGridViewOnLoadSucess() {
             var dgid = $('#dataGridView');
             var queryPanel = getInfolightOption(dgid).queryDialog;
             if (queryPanel)
                 $(queryPanel).panel('resize', { width: 410 });
             $('.infosysbutton-q', '#dataGridView').closest('td').attr('align', 'middle');
         }
         function BeforeOneMonth() {
             var dt = new Date();
             var aDate = new Date($.jbDateAdd('days', -30, dt));
             return $.jbjob.Date.DateFormat(aDate, 'yyyy/MM/dd');
         }
         function TodayDate() {
             var dt = new Date();
             var aDate = new Date($.jbDateAdd('days', 0, dt));
             return $.jbjob.Date.DateFormat(aDate, 'yyyy/MM/dd');
         }
         function queryGrid(dg) {
             if ($(dg).attr('id') == 'dataGridView') {
                 var result = [];
                 var aVal = '';
                 var bVal = '';
                 aVal = $('#CompanyID_Query').combobox('getValue');
                 if (aVal != '')
                     result.push("A.CompanyID = '" + aVal + "'");
                 aVal = $('#EmployeeID_Query').combobox('getValue');
                 if (aVal != '')
                     result.push("A.EmployeeID = '" + aVal + "'");
                 aVal = $('#ShortTermDateS_Query').datebox('getValue');
                 bVal = $('#ShortTermDateE_Query').datebox('getValue');
                 if (aVal != '' && bVal != '')
                     result.push("A.ShortTermDate between '" + aVal + "' and '" + bVal + "'");
                 aVal = $('#RequestDateS_Query').datebox('getValue');
                 bVal = $('#RequestDateE_Query').datebox('getValue');
                 if (aVal != '' && bVal != '')
                     result.push("A.RequestDate between '" + aVal + "' and '" + bVal + "'");
                 aVal = $('#PayTypeID_Query').combobox('getValue');
                 if (aVal != '')
                     result.push("A.PayTypeID = '" + aVal + "'");
                 aVal = $('#Flowflag_Query').combobox('getValue');
                 if (aVal != '')
                     result.push("A.Flowflag = '" + aVal + "'");
                 aVal = $('#DLDays_Query').val();
                 if (aVal != '' && aVal > 0);
                    result.push("(dbo.funReturnDeldayDays(A.PlanPayDate) >= '" + aVal + "')");
                       $(dg).datagrid('setWhere', result.join(' and '));
             }
         }

     </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sShortTermRepo.ShortTerm" runat="server" AutoApply="True"
                DataMember="ShortTerm" Pagination="True" QueryTitle="輸出條件" EditDialogID="JQDialog1"
                Title="暫借款報表" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="20" PageSize="20" QueryAutoColumn="False" QueryLeft="50px" QueryMode="Window" QueryTop="50px" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True" Width="1020px" ReportFileName="~/JB_ADMIN/rShortTerm.rdlc" BufferView="False" NotInitGrid="False" OnLoadSuccess="dataGridViewOnLoadSucess">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="公司名稱" Editor="text" FieldName="CompanyName" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="暫借單號" Editor="text" FieldName="ShortTermNO" Format="" MaxLength="0" Width="65" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ShortTermDate" Format="yyyy/mm/dd" Width="65" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請者" Editor="text" FieldName="EmployeeName" Format="" MaxLength="0" Width="65" />
                    <JQTools:JQGridColumn Alignment="left" Caption="EmployeeID" Editor="text" FieldName="EmployeeID" Format="" MaxLength="0" Width="120" Visible="False" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="暫借事由" Editor="text" FieldName="ShortTermDescr" Format="" MaxLength="0" Width="310" />
                    <JQTools:JQGridColumn Alignment="right" Caption="金額" Editor="numberbox" FieldName="ShortTermAmount" Format="" Width="50" />
                    <JQTools:JQGridColumn Alignment="right" Caption="PayTypeID" Editor="numberbox" FieldName="PayTypeID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="支付方式" Editor="text" FieldName="PayTypeName" Format="" MaxLength="0" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="需求日期" Editor="datebox" FieldName="RequestDate" Format="yyyy/mm/dd" Width="65" />
                    <JQTools:JQGridColumn Alignment="left" Caption="預計歸還日" Editor="datebox" FieldName="PlanPayDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="65" />
                    <JQTools:JQGridColumn Alignment="left" Caption="請款單號" Editor="text" FieldName="RequisitionNO" Format="" MaxLength="0" Width="80" Visible="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="CompanyID" Editor="numberbox" FieldName="CompanyID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="逾期天數" Editor="numberbox" FieldName="DLDays" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="72">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"  OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"  Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="報表條件" />
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                    <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportReport"  Text="列印輸出"  />

                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="公司別" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sShortTermRepo.Company',tableName:'Company',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CompanyID" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="申請員工" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sShortTermRepo.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="EmployeeID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="申請起始日" Condition="&gt;=" DataType="datetime" Editor="datebox" FieldName="ShortTermDateS" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="90" DefaultMethod="" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="申請終止日" Condition="&lt;=" DataType="datetime" Editor="datebox" FieldName="ShortTermDateE" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="90" DefaultMethod="" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="需求起始日" Condition="&gt;=" DataType="datetime" DefaultMethod="" Editor="datebox" FieldName="RequestDateS" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="需求終止日" Condition="&lt;=" DataType="datetime" DefaultMethod="" Editor="datebox" FieldName="RequestDateE" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="付款方式" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'PayTypeID',textField:'PayTypeName',remoteName:'sShortTermRepo.PayType',tableName:'PayType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="PayTypeID" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="逾期天數" Condition="&gt;=" DataType="number" Editor="text" FieldName="DLDays" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="87" DefaultValue="0" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="狀態" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'FlowFlag',textField:'FlowStatus',remoteName:'sShortTermRepo.Status',tableName:'Status',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="Flowflag" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="90" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="暫借款報表">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="ShortTerm" HorizontalColumnsCount="2" RemoteName="sShortTermRepo.ShortTerm" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="ShortTermNO" Editor="text" FieldName="ShortTermNO" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ShortTermDescr" Editor="text" FieldName="ShortTermDescr" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="PlanPayDate" Editor="datebox" FieldName="PlanPayDate" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ShortTermAmount" Editor="numberbox" FieldName="ShortTermAmount" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="PayTypeID" Editor="numberbox" FieldName="PayTypeID" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="PayTypeName" Editor="text" FieldName="PayTypeName" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="RequestDate" Editor="datebox" FieldName="RequestDate" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ShortTermDate" Editor="datebox" FieldName="ShortTermDate" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="RequisitionNO" Editor="text" FieldName="RequisitionNO" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="EmployeeID" Editor="text" FieldName="EmployeeID" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CompanyID" Editor="numberbox" FieldName="CompanyID" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="EmployeeName" Editor="text" FieldName="EmployeeName" Format="" maxlength="0" Width="180" />
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
