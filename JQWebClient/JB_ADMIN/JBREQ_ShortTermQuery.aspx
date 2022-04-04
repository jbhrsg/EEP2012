<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBREQ_ShortTermQuery.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //將Focus 欄位背景顏色改為黃色
            $(function () {
                $("input, select, textarea").focus(function () {
                    $(this).css("background-color", "yellow");
                });
                $("input, select, textarea").blur(function () {
                    $(this).css("background-color", "white");
                });
            });
            //設定 Grid QueryColunm Windows width=320px
            var dgid = $('#dataGridMaster');
            var queryPanel = getInfolightOption(dgid).queryDialog;
            if (queryPanel)
                $(queryPanel).panel('resize', { width: 410 });
            $('.infosysbutton-q', '#dataGridMaster').closest('td').attr('align', 'middle');
        });
        function genCheckBox(val) {
            if (val)
                return "<input  type='checkbox' checked='true' checked onclick='return false;'  />";
            else
                return "<input  type='checkbox' onclick='return false;' />";
        }
        </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sShortTermSetUpFee.ShortTerm" runat="server" AutoApply="True"
                DataMember="ShortTerm" Pagination="True" QueryTitle="查詢條件"
                Title="暫借款立帳查詢" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="50px" QueryMode="Window" QueryTop="50px" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn FieldName="STATUS" Caption="狀態" IsNvarChar="False" Alignment="left" Width="45" Editor="text" MaxLength="0" Sortable="False" Frozen="False" ReadOnly="False" Visible="True" QueryCondition=""></JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="暫借單號" Editor="text" FieldName="ShortTermNO" MaxLength="0" Width="65" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="暫借使用客戶" Editor="text" FieldName="EMPLOYERNAME1" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="暫借事由" Editor="text" FieldName="ShortTermGist" MaxLength="0" Width="180" />
                    <JQTools:JQGridColumn Alignment="right" Caption="暫借金額" Editor="text" FieldName="ShortTermAmount" Width="65" />
                    <JQTools:JQGridColumn Alignment="left" Caption="暫借人員" Editor="text" FieldName="EmployeeName" MaxLength="0" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="暫借日期" Editor="text" FieldName="ShortTermDate" Width="70" Format="yyyy/mm/dd" />
                    <JQTools:JQGridColumn Alignment="left" Caption="預計歸還日" Editor="text" FieldName="PlanPayDate" Width="70" Format="yyyy/mm/dd" />
                    <JQTools:JQGridColumn Alignment="left" Caption="立帳單號" Editor="text" FieldName="FeeSetUPID" Width="65" />
                    <JQTools:JQGridColumn Alignment="left" Caption="立帳客戶" Editor="text" FieldName="EMPLOYERNAME2" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="立帳科目" Editor="text" FieldName="FEENAME" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="立帳金額" Editor="text" FieldName="TIMELEAVEFEE" Width="65" />
                    <JQTools:JQGridColumn Alignment="left" Caption="立帳年月" Editor="text" FieldName="YEARMONTH" MaxLength="0" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="立帳人員" Editor="text" FieldName="CREATEBY" MaxLength="0" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="立帳日期" Editor="text" FieldName="CREATEDATE" Width="105" Format="yyyy/mm/dd HH:MM:SS" />
                    <JQTools:JQGridColumn Alignment="left" Caption="EMPLOYERID" Editor="text" FieldName="EMPLOYERID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="EMPLOYEEID" Editor="text" FieldName="EMPLOYEEID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="狀態" Editor="text" EditorOptions="" FieldName="FlowFlag" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="查詢" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="暫借起始日" Condition="&gt;=" DataType="string" DefaultValue="_today" Editor="datebox" FieldName="ShortTermDate" Format="yyyy/mm/dd" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="暫借終止日" Condition="&lt;=" DataType="string" DefaultValue="_today" Editor="datebox" FieldName="ShortTermDate" Format="yyyy/mm/dd" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="暫借客戶" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'EmployerID',textField:'EmployerName',remoteName:'sShortTermSetUpFee.Employer',tableName:'Employer',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="A.EMPLOYERID" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="暫借人員" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'EMPLOYEEID',textField:'EMPLOYEENAME',remoteName:'sShortTermSetUpFee.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="A.EMPLOYEEID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="狀態" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'FlowFlag',textField:'FlowStatus',remoteName:'sShortTermSetUpFee.STATUS',tableName:'STATUS',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="FlowFlag" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="90" />
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>
    <JQTools:JQDefault runat="server" BindingObjectID="dataGridMaster" BorderStyle="NotSet" Enabled="True" EnableTheming="True" ClientIDMode="Inherit" ID="defaultMaster" EnableViewState="True" ViewStateMode="Inherit" >
</JQTools:JQDefault>
<JQTools:JQValidate runat="server" BindingObjectID="dataGridMaster" BorderStyle="NotSet" Enabled="True" EnableTheming="True" ClientIDMode="Inherit" ID="validateMaster" EnableViewState="True" ViewStateMode="Inherit" >
</JQTools:JQValidate>
</form>
</body>
</html>
