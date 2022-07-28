<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_ContinueEmployQuery.aspx.cs" Inherits="Template_JQueryQuery1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        var userid = getClientInfo("UserID");
        $(function () {
            //$('#BackPot_Query').closest('td').append($('.infosysbutton-q', '#querydataGridMaster').closest('td').contents());
            var enddate = new Date();
            enddate.setMonth(enddate.getMonth() + 1);
            enddate.setDate(1);
            enddate.setDate(enddate.getDate() - 1);
            enddate.format("yyyy-MM-dd");
            $('#DueDate_Query[infolight-options*="期滿日迄"] ').datebox('setValue', enddate.format("yyyy-MM-dd"));

            var startdate = new Date();
            startdate.setDate(enddate.getDate() - 183);
            $('#DueDate_Query').datebox('setValue', startdate.format("yyyy-MM-dd"));
        });

        function FormatScript_FlowFlag(val) {
            if (val == 'N') {
                return '新申請';
            } else if (val == 'P') {
                return '流程中';
            } else if (val == 'Z') {
                return '結案';
            }
        }
        function FormatScript_TrueFalse(val) {
            return (val == "1") ? "V" : "";
        }
        function queryGrid() {
            var where="";
            where = $("#dataGridMaster").datagrid('getWhere');
            var where0 = "";
            if (where.match(/.*EmployOption='1'.*/)) {
                where0 = where.replace(/EmployOption='1'/, "IsRecontract1='1'");
            } else if (where.match(/.*EmployOption='2'.*/)) {
                where0 = where.replace(/EmployOption='2'/, "Transfer1='1'");
            } else if (where.match(/.*EmployOption='3'.*/)) {
                where0 = where.replace(/EmployOption='3'/, "ReturnHome1='1'");
            } else if (where.match(/.*EmployOption='4'.*/)) {
                where0 = where.replace(/EmployOption='4'/, "BackPot1='1'");
            } else if (where.match(/.*EmployOption='5'.*/)) {
                where0 = where.replace(/EmployOption='5'/, "TransferAg1='1'");
            } else if (where.match(/.*and EmployOption='0'.*/)) {
                where0 = where.replace(/and EmployOption='0'/, "");
            } else if (where.match(/.*EmployOption='0'.*/)) {
                where0 = where.replace(/EmployOption='0'/, "");
            } else{
                where0 = where;
            }
            $("#dataGridMaster").datagrid('setWhere', where0);

            
        }
        function OnUpdate_dataGridMaster(row) {
            if (row.IsRecontract1 == '1' && row.SalesID == userid) { return true; } else { return false; }
        }
        function OnLoad_dataGridMaster() {
            if (!$(this).data("firstLoad") && $(this).data("firstLoad", true)) {
                var email = Request.getQueryStringByName("em");
                var Userid = getClientInfo("userid");
                if (email == 1) {
                    var where = "CONVERT(DATE,GETDATE()) >= DATEADD(MONTH,-3,DueDate) and (LetterNO='' or LetterNO is null) and SalesID='" + Userid + "'";
                    //var where = "CONVERT(DATE,GETDATE()) >= DATEADD(MONTH,-3,DueDate) and (LetterNO='' or LetterNO is null)";
                    //var where0 = $("#dataGridView").datagrid("getWhere");
                    $("#dataGridMaster").datagrid("setWhere", where);
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sERPContinueEmployQuery.View_ERPContinueEmployDetail" runat="server" AutoApply="True"
                DataMember="View_ERPContinueEmployDetail" Pagination="False" QueryTitle="查詢條件"
                Title="續聘通知單明細" AllowDelete="False" AllowInsert="False" AllowUpdate="True" QueryMode="Panel" AlwaysClose="True" AllowAdd="False" ViewCommandVisible="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" EditDialogID="JQDialog1" OnUpdate="OnUpdate_dataGridMaster" OnLoadSuccess="OnLoad_dataGridMaster">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="通知單總單編號	" Editor="text" FieldName="ContinueEmployNO" MaxLength="0" Width="90" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Width="90" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="text" FieldName="CreateDate" Width="60" Format="yyyy/mm/dd" />
                    <JQTools:JQGridColumn Alignment="left" Caption="流程狀態" Editor="infocombobox" FieldName="FlowFlag" MaxLength="0" Width="40" FormatScript="" EditorOptions="valueField:'FlowFlag',textField:'FlowStatus',remoteName:'sERPContinueEmployQuery.FlowFlag',tableName:'FlowFlag',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="負責業務" Editor="infocombobox" FieldName="SalesID" MaxLength="0" Width="60" EditorOptions="valueField:'userid',textField:'username',remoteName:'sERPContinueEmployQuery.UserName',tableName:'UserName',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="函別" Editor="text" FieldName="LetterClass" MaxLength="0" Width="40" />
                    <JQTools:JQGridColumn Alignment="left" Caption="文號" Editor="text" FieldName="LetterNO" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="工人姓名" Editor="text" FieldName="LaborName" MaxLength="0" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="性別" Editor="text" FieldName="Gender" MaxLength="0" Width="30" />
                    <JQTools:JQGridColumn Alignment="left" Caption="國籍" Editor="text" FieldName="Country" MaxLength="0" Width="40" />
                    <JQTools:JQGridColumn Alignment="left" Caption="入境日" Editor="text" FieldName="ImmigrationDate" Width="70" Format="yyyy/mm/dd" />
                    <JQTools:JQGridColumn Alignment="left" Caption="期滿日" Editor="text" FieldName="DueDate" Width="70" Format="yyyy/mm/dd" />
                    <JQTools:JQGridColumn Alignment="left" Caption="續聘確認書編號" Editor="text" FieldName="CEConfirmNO" MaxLength="0" Width="130" Format="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="續聘" Editor="text" FieldName="IsRecontract1" Width="30" FormatScript="FormatScript_TrueFalse" />
                    <JQTools:JQGridColumn Alignment="left" Caption="轉雇" Editor="text" FieldName="Transfer1" Width="30" FormatScript="FormatScript_TrueFalse" />
                    <JQTools:JQGridColumn Alignment="left" Caption="返國" Editor="text" FieldName="ReturnHome1" Width="30" FormatScript="FormatScript_TrueFalse" />
                    <JQTools:JQGridColumn Alignment="left" Caption="回鍋" Editor="text" FieldName="BackPot1" Width="30" FormatScript="FormatScript_TrueFalse" />
                    <JQTools:JQGridColumn Alignment="left" Caption="轉仲" Editor="text" FieldName="TransferAg1" FormatScript="FormatScript_TrueFalse" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="雇主名稱" Editor="text" FieldName="Employer" MaxLength="0" Width="150" />
                    <JQTools:JQGridColumn Alignment="left" Caption="入境年月" Editor="text" FieldName="ImmigrationYearMonth" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="50">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="期滿年月" Editor="text" FieldName="DueYearMonth" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="50">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="Insert" Visible="False" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="Update" Visible="False" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="Delete" Visible="False" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="Apply" Visible="False" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-cancel" ItemType="easyui-linkbutton" OnClick="cancel" Text="Cancel" Visible="False" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="Query" Visible="False" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="雇主名稱" Condition="%" DataType="string" Editor="infocombobox" FieldName="Employer" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="220" EditorOptions="valueField:'Employer',textField:'Employer',remoteName:'sERPContinueEmployQuery.Employer',tableName:'Employer',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="流程狀態" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'FlowFlag',textField:'FlowStatus',remoteName:'sERPContinueEmployQuery.FlowFlag',tableName:'FlowFlag',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="FlowFlag" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="期滿年月" Condition="%" DataType="string" Editor="infocombobox" FieldName="DueYearMonth" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" EditorOptions="valueField:'DueYearMonth',textField:'DueYearMonth',remoteName:'sERPContinueEmployQuery.DueYearMonth',tableName:'DueYearMonth',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="建立年月" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CreateYearMonth',textField:'CreateYearMonth',remoteName:'sERPContinueEmployQuery.CreateYearMonth',tableName:'CreateYearMonth',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CreateYearMonth" IsNvarChar="True" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="負責業務" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'SalesID',textField:'USERNAME',remoteName:'sERPContinueEmployQuery.SalesID',tableName:'SalesID',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="國籍" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'Country',textField:'Country',remoteName:'sERPContinueEmployQuery.Country',tableName:'Country',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="Country" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="性別" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'Gender',textField:'Gender',remoteName:'sERPContinueEmployQuery.Gender',tableName:'Gender',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="Gender" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="期滿日起" Condition="&gt;=" DataType="string" Editor="datebox" FieldName="DueDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="期滿日迄" Condition="&lt;=" DataType="string" Editor="datebox" FieldName="DueDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="續聘意願" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'0',text:' ',selected:'false'},{value:'1',text:'續聘',selected:'false'},{value:'2',text:'轉雇',selected:'false'},{value:'3',text:'返國',selected:'false'},{value:'4',text:'回鍋',selected:'false'},{value:'5',text:'轉仲',selected:'false'}],checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="EmployOption" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>
            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="JQDataForm1">
                <JQTools:JQDataForm ID="JQDataForm1" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="ERPContinueEmployDetail" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="2" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="" RemoteName="sERPContinueEmployQuery.ERPContinueEmployDetail" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ContinueEmployNO" Editor="text" FieldName="ContinueEmployNO" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="工人姓名" Editor="text" FieldName="LaborName" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="性別" Editor="text" FieldName="Gender" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="國籍" Editor="text" FieldName="Country" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="雇主" Editor="text" FieldName="Employer" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="入境日" Editor="text" FieldName="ImmigrationDate" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="期滿日" Editor="text" FieldName="DueDate" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="續聘確認書編號" Editor="text" FieldName="CEConfirmNO" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="IsRecontract" Editor="text" FieldName="IsRecontract" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Transfer" Editor="text" FieldName="Transfer" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ReturnHome" Editor="text" FieldName="ReturnHome" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="BackPot" Editor="text" FieldName="BackPot" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="續聘函文類別" Editor="infocombobox" FieldName="LetterClass" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" EditorOptions="items:[{value:'初招',text:'初招',selected:'false'},{value:'重招',text:'重招',selected:'false'},{value:'遞補',text:'遞補',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="文號" Editor="text" FieldName="LetterNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
            </JQTools:JQDialog>
        </div>

    </form>
</body>
</html>
