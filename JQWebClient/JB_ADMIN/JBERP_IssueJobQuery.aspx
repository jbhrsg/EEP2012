<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_IssueJobQuery.aspx.cs" Inherits="Template_JQueryQuery1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        $(function () {
            //$('#Flowflag_Query').closest('td').next('td').next('td').append("&nbsp;&nbsp;");
            //$('#Flowflag_Query').closest('td').next('td').next('td').append($('.infosysbutton-q', '#querydataGridMaster').closest('td').contents());
            $('#ServeEmployeeID_Query').combobox('disable');
            $('#IssueBelongID_Query').combobox({
                onChange: function (newVal, oldVal) {
                    if (newVal == "" || newVal == "") {
                        $('#IssueTypeID_Query').combobox('setWhere',"1=0");
                    }
                }
            });
        });
        function OnSelectIssueBelongID_Query() {
            $('#ServeEmployeeID_Query').combobox('enable');
            $('#ServeEmployeeID_Query').combobox('setValue', '');
            where = "GROUPID =" + $('#IssueBelongID_Query').combobox('getValue');
            $('#ServeEmployeeID_Query').combobox('setWhere', where);
            where = "IssueBelongID =" + $('#IssueBelongID_Query').combobox('getValue');
            $('#IssueTypeID_Query').combobox('setWhere', where);
        }
        function OnLoadSuccessdataGridMaster() {
            $("#querydataGridMaster").find(".infosysbutton-cl").unbind().bind("click", OnClickServeEmployeeID);
        }
        function OnClickServeEmployeeID() {
            clearQuery('#dataGridMaster');
            $('#ServeEmployeeID_Query').combobox('disable');
        }
        function queryGrid() {
            var whereArr = [];
            var CreateBy = $('#CreateBy_Query').combobox('getValue');
            var IssueBelongID = $('#IssueBelongID_Query').combobox('getValue');
            var ServeEmployeeID = $('#ServeEmployeeID_Query').combobox('getValue');
            var EstimationDateS = $('#EstimationDate_Query').datebox('getValue');
            var EstimationDateE = $('#EstimationDate_Query[infolight-options*="~"]  ').datebox('getValue');
            var CloseDateS = $('#CloseDate_Query').datebox('getValue');
            var CloseDateE = $('#CloseDate_Query[infolight-options*="~"]  ').datebox('getValue');
            var UrgentLevel = $('#UrgentLevel_Query').combobox('getValue');
            var ORG_NO = $('#ORG_NO_Query').combobox('getValue');
            var IssueTypeID = $('#IssueTypeID_Query').combobox('getValue')
            if (CreateBy != '') {
                whereArr.push("ij.CreateBy = '" + CreateBy + "'");
            }
            if (IssueBelongID != '') {
                whereArr.push("ij.IssueBelongID = '" + IssueBelongID + "'");
            }
            if (ServeEmployeeID != '') {
                whereArr.push("ij.ServeEmployeeID = '" + ServeEmployeeID + "'");
            }
            if (IssueTypeID != '') {
                whereArr.push("ij.IssueTypeID = '" + IssueTypeID + "'");
            }
            if (EstimationDateS != '') {
                whereArr.push("ij.EstimationDate>='" + EstimationDateS + "'");
            } if (EstimationDateE != '') {
                whereArr.push("ij.EstimationDate<='" + EstimationDateE + "'");
            } if (CloseDateS != '') {
                whereArr.push("ij.CloseDate>='" + CloseDateS + "'");
            } if (CloseDateE != '') {
                whereArr.push("ij.CloseDate<='" + CloseDateE + "'");
            } if (UrgentLevel != '') {
                whereArr.push("ij.UrgentLevel='" + UrgentLevel + "'");
            } if (ORG_NO != '') {
                whereArr.push("ij.ORG_NO='" + ORG_NO + "'");
            }
            var ff = $('#Flowflag_Query').options('getValue');
            if (ff == '1') {//已結案
                whereArr.push("ij.Flowflag='Z'");
            } else if (ff == '0') {//未結案但已完成
                whereArr.push("ij.Flowflag!='Z' and ij.CloseDate is not null");
            } else if (ff == '2') {//未完成
                whereArr.push("ij.CloseDate is null");
            }
            where=whereArr.join(" and ");
            $('#dataGridMaster').datagrid('setWhere', where);
            //控制GRID欄位顯示
            var DGVColumns = ['IssueJobNO', 'CreateBy', 'IssueJobDate', 'IssueTypeName', 'IssueDescr', 'BeginDate', 'PeriodDays', 'EndDate',
                'IsReset', 'ResetDate', 'UrgentLevel', 'ORG_NO', 'EstimationDate', 'CloseDate', 'CheckDate', 'Cost', 'ATTACHMENTS', 'CheckScore', 'USERNAME', 'FlowStatus'];
            HideGridColumns(DGVColumns);
            if ((IssueTypeID == 84 || IssueTypeID == 85) && IssueTypeID != "") {
                DGVColumns = ['IssueJobNO','CreateBy', 'IssueJobDate', 'IssueTypeName', 'IssueDescr', 'BeginDate', 'PeriodDays', 'EndDate', 'IsReset', 'ResetDate','CheckScore', 'USERNAME', 'FlowStatus'];
                ShowGridColumns(DGVColumns);
            }
            else {
                DGVColumns = ['IssueJobNO','CreateBy', 'IssueJobDate', 'IssueTypeName', 'IssueDescr','UrgentLevel', 'ORG_NO', 'EstimationDate', 'CloseDate', 'CheckDate', 'Cost', 'ATTACHMENTS', 'CheckScore', 'USERNAME', 'FlowStatus'];
                ShowGridColumns(DGVColumns);
           }
        }
        function genCheckBox(val) {
            if (val)
                return "<input  type='checkbox' checked='true' />";
            else
                return "<input  type='checkbox' />";
        }
        function ShowGridColumns(fields) {
            var GridName = '#dataGridMaster'
            $.each(fields, function (index, fieldName) {
                $(GridName).datagrid('showColumn', fieldName);
            });
        }
        function HideGridColumns(fields) {
            var GridName = '#dataGridMaster';
            $.each(fields, function (index, fieldName) {
                $(GridName).datagrid('hideColumn', fieldName);
            });
        }
        function layout(FieldName1, FieldName2) {
            //var FieldName1td = $(FieldName1).closest('td');
            //var FieldName2Contents = $(FieldName2).closest('td').prev('td').contents();
            //var FieldName2tdC = $(FieldName2).closest('td').children();
            //FieldName1td.append(FieldName2Contents).append(FieldName2tdC);
            //FieldName1td.next('td').remove();
            //FieldName1td.next('td').remove();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sIssueJobQuery.View_IssueJob" runat="server" AutoApply="True"
                DataMember="View_IssueJob" Pagination="True" QueryTitle=""
                Title="工作需求查詢" AllowDelete="False" AllowInsert="False" AllowUpdate="False" QueryMode="Panel" AlwaysClose="True" AllowAdd="False" ViewCommandVisible="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" EditDialogID="JQDialog1" OnLoadSuccess="OnLoadSuccessdataGridMaster">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="需求單號" Editor="text" FieldName="IssueJobNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="55">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="申請員工" Editor="text" FieldName="CreateBy" Format="" MaxLength="20" Width="60" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請日期" Editor="text" FieldName="IssueJobDate" Format="yyyy/mm/dd" MaxLength="0" Width="65" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="需求項目" Editor="text" FieldName="IssueTypeName" Format="" MaxLength="0" Width="130" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="需求描述" Editor="text" FieldName="IssueDescr" Format="" MaxLength="1000" Width="330" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="啟用日期" Editor="datebox" FieldName="BeginDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="65">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="需求天數" Editor="text" FieldName="PeriodDays" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="截止日期" Editor="datebox" FieldName="EndDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="65">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="設定復原" Editor="checkbox" FieldName="IsReset" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="60" EditorOptions="on:1,off:0" Format="" FormatScript="genCheckBox">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="復原日期" Editor="text" FieldName="ResetDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="65">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="急度" Editor="text" FieldName="UrgentLevel" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="申請部門" Editor="infocombobox" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sIssueJobQuery.ORG',tableName:'ORG',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ORG_NO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="滿意度" Editor="numberbox" FieldName="CheckScore" Format="" Width="40" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="承辦人" Editor="text" FieldName="USERNAME" Format="" MaxLength="0" Width="50" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="預計完成日" Editor="datebox" FieldName="EstimationDate" Format="" Width="65" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="完成日期" Editor="datebox" FieldName="CloseDate" Format="" Width="65" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="驗收日期" Editor="datebox" FieldName="CheckDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="65">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="時數" Editor="text" FieldName="Cost" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="30">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="上傳檔案1" Editor="text" FieldName="ATTACHMENTS" Format="Download,Folder:WorkflowFiles" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="流程狀態" Editor="text" FieldName="FlowStatus" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="55">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Enabled="True" Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="輸出EXCEL" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn Caption="負責職稱" Condition="=" Editor="infocombobox" FieldName="IssueBelongID" EditorOptions="valueField:'GROUPID',textField:'GROUPNAME',remoteName:'sIssueJobQuery.GROUPS',tableName:'GROUPS',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OnSelectIssueBelongID_Query,panelHeight:200" TableName="ij" NewLine="False" Width="140" />
                    <JQTools:JQQueryColumn Caption="承辦人員" Condition="=" Editor="infocombobox" FieldName="ServeEmployeeID" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sIssueJobQuery.USERS',tableName:'USERS',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" NewLine="False" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="需求項目" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'IssueTypeID',textField:'IssueTypeName',remoteName:'sIssueJobQuery.IssueType',tableName:'IssueType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="IssueTypeID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="160" />
                    <JQTools:JQQueryColumn Caption="預計完成日起訖" Condition="&gt;=" Editor="datebox" FieldName="EstimationDate" NewLine="True" Width="140" />
                    <JQTools:JQQueryColumn Caption="~" Condition="&lt;" Editor="datebox" FieldName="EstimationDate" NewLine="False" />
                    <JQTools:JQQueryColumn Caption="完成日期起訖" Condition="&gt;=" Editor="datebox" FieldName="CloseDate" NewLine="True" Width="140" />
                    <JQTools:JQQueryColumn Caption="~" Condition="&lt;" Editor="datebox" FieldName="CloseDate" NewLine="False" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="狀態" Condition="" DataType="string" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:300,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:3,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'已結案',value:'1'},{text:'未結案但已完成',value:'0'},{text:'未完成',value:'2'}]" FieldName="Flowflag" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="160" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="緊急程度" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'},{value:'最急',text:'最急',selected:'false'},{value:'急',text:'急',selected:'false'},{value:'不急',text:'不急',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="UrgentLevel" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="140" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="申請部門" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sIssueJobQuery.ORG',tableName:'ORG',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ORG_NO" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="申請者" Condition="%%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CreateBy',textField:'CreateBy',remoteName:'sIssueJobQuery.Applier',tableName:'Applier',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CreateBy" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" TableName="ij" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>

        <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" DialogLeft="30px" DialogTop="1px" Title="工作需求單" Width="600px">
            <JQTools:JQDataForm ID="dataFormMaster" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="View_IssueJob" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="2" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sIssueJobQuery.View_IssueJob" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                <Columns>
                    <JQTools:JQFormColumn Alignment="left" Caption="需求編號" Editor="text" FieldName="IssueJobNO" Format="" NewRow="False" ReadOnly="True" Width="130" />
                    <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="IssueJobDate" Format="" NewRow="False" Width="120" maxlength="0" Span="1" />
                    <JQTools:JQFormColumn Alignment="left" Caption="負責職稱" Editor="text" EditorOptions="" FieldName="GROUPNAME" Format="" maxlength="0" NewRow="True" Span="1" Width="130" />
                    <JQTools:JQFormColumn Alignment="left" Caption="需求項目" Editor="text" EditorOptions="" FieldName="IssueTypeName" Format="" maxlength="0" NewRow="False" Span="1" Width="180" />
                    <JQTools:JQFormColumn Alignment="left" Caption="需求描述" Editor="textarea" EditorOptions="height:80" FieldName="IssueDescr" Format="" maxlength="500" NewRow="True" Span="2" Width="400" />
                    <JQTools:JQFormColumn Alignment="left" Caption="緊急程度" Editor="text" FieldName="UrgentLevel" maxlength="0" NewRow="False" Span="1" Width="85" ReadOnly="False" RowSpan="1" Visible="True" />
                    <JQTools:JQFormColumn Alignment="left" Caption="申請部門" Editor="infocombobox" FieldName="ORG_NO" Width="120" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sIssueJobQuery.ORG',tableName:'ORG',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" NewRow="False" ReadOnly="False" />
                    <JQTools:JQFormColumn Alignment="center" Caption="預計完成日" Editor="datebox" FieldName="EstimationDate" Format="" maxlength="0" NewRow="True" Span="1" Width="90" />
                    <JQTools:JQFormColumn Alignment="center" Caption="實際完成日期" Editor="datebox" FieldName="CloseDate" MaxLength="0" NewRow="False" Span="1" Width="120" Format="" />
                    <JQTools:JQFormColumn Alignment="left" Caption="處理描述" Editor="textarea" FieldName="CloseDescr" Format="" maxlength="500" NewRow="False" Span="2" Visible="True" Width="400" EditorOptions="height:80" />
                    <JQTools:JQFormColumn Alignment="left" Caption="花費時數" Editor="numberbox" FieldName="Cost" maxlength="0" NewRow="False" Span="1" Visible="True" Width="85" ReadOnly="False" RowSpan="1" />
                    <JQTools:JQFormColumn Alignment="center" Caption="驗收日期" Editor="datebox" FieldName="CheckDate" Format="" maxlength="0" Visible="True" Width="90" NewRow="False" Span="2" />
                    <JQTools:JQFormColumn Alignment="left" Caption="驗收備註" Editor="textarea" FieldName="CheckDescr" Format="" maxlength="0" Visible="True" Width="400" EditorOptions="height:50" NewRow="True" Span="2" />
                    <JQTools:JQFormColumn Alignment="left" Caption="Flowflag" Editor="text" FieldName="Flowflag" Format="" MaxLength="0" Visible="False" Width="180" />
                    <JQTools:JQFormColumn Alignment="left" Caption="EmployeeID" Editor="text" FieldName="EmployeeID" Format="" MaxLength="20" NewRow="False" ReadOnly="False" Span="1" Visible="False" Width="180" RowSpan="1" />
                    <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" maxlength="0" NewRow="False" ReadOnly="False" Span="1" Visible="False" Width="180" Format="" />
                    <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" ReadOnly="False" Visible="False" Width="180" Format="" />
                    <JQTools:JQFormColumn Alignment="left" Caption="滿意度" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:300,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:5,multiSelect:false,openDialog:false,selectAll:false,selectOnly:true,items:[{text:'1分',value:'1'},{text:'2分',value:'2'},{text:'3分',value:'3'},{text:'4分',value:'4'},{text:'5分',value:'5'}]" FieldName="CheckScore" maxlength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="80" />
                    <JQTools:JQFormColumn Alignment="left" Caption="ServeEmployeeID" Editor="text" FieldName="ServeEmployeeID" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="80" />
                </Columns>
            </JQTools:JQDataForm>
        </JQTools:JQDialog>

    </form>
</body>
</html>
