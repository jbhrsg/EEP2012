<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_Competition.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        var userDepartment;////實績dataform的部門篩選 和 實績資料的篩選
        var userid = getClientInfo("UserID");
        //設定使用者的部門，用來篩選可看到的資料(只用在實績dataGridReal，目標dataGridView沒用到)
        switch (userid) {
            case '105'://吳宗偉
                userDepartment = "人才服務桃園";
                break;
            case '343'://王純純
                userDepartment = "外勞事業部";
                break;
            case '011'://林翠玲
                userDepartment = "外勞事業部";
                break;
            case '004'://鍾華亮
                userDepartment = "人才服務";
                break;
            case '355'://吳敏慧
                userDepartment = "人才服務台北";
                break;
            case '185'://彭筱崴
                userDepartment = "人才服務新竹";
                break;
            case '014'://李素英
                userDepartment = "南崁長安茂台";
                break;
            case '286'://邱碧娥
                userDepartment = "經營企劃室";
                break;
            case '347'://林姿伶
                userDepartment = "經營企劃室";
                break;
            case '335':
                userDepartment = "管理室";
                break;
            case '060':
                userDepartment = "管理室";
                break;
            case '009':
                userDepartment = "管理室";
                break;
            case '005':
                userDepartment = "外勞事業部/傑報幸福村";
                break;


        }
        $(function () {
            $("#dataFormMasterDepartment").closest('td').prev('td').css({ 'color': 'red' });
            $("#dataFormMasterUnit").closest('td').prev('td').css({ 'color': 'red' });
            $('#dataFormRealDate').datebox({
                onSelect: function (date) {
                    $('#dataFormRealYear').val(date.getFullYear());
                    $('#dataFormRealMonth').val((date.getMonth()) + 1);
                }
            });
            //目標dataGridView查詢條件預設值
            $("#Year_Query").combobox('setValue',DefaultMethodYear());
            $("#Quarter_Query").combobox('setValue', DefaultMethodQuarter());

        });
        function OnLoaddataGridView() {
            $("#JQDialog0").find(".infosysbutton-s").hide();

            if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) {//只執行一次
                RefreshGrid();
            }
        }
        function RefreshGrid() {
            var YearQuery=$("#Year_Query").combobox('getValue');
            var QuarterQuery = $("#Quarter_Query").combobox('getValue');
            var whereArr = [];
            var whereStr = '';
            if (YearQuery != '') {
                whereArr.push("Year='" + YearQuery + "'");
            }
            if (QuarterQuery != '') {
                whereArr.push("Quarter='" + QuarterQuery + "'");
            }
            if (whereArr.length > 0) {
                whereStr=whereArr.join(' and ');
            }
            $("#dataGridView").datagrid('setWhere', whereStr);
        }
        function DefaultMethodYear() {//目標dataform的年
            var curYear = (new Date).getFullYear();
            return curYear;
        }
        function DefaultMethodQuarter() {//目標dataform的季
            var curMonth =parseInt((new Date).getMonth());
            var curQuarter;
            if (curMonth <= 2 && curMonth >=0) curQuarter = 1;
            else if (curMonth <= 5 && curMonth >= 3) curQuarter = 2;
            else if (curMonth <= 8 && curMonth >= 6) curQuarter = 3;
            else curQuarter = 4;
            return curQuarter;
        }
        function OnClickToolItemDataGridReal() {
            openForm("#JQDialog0", {}, "viewed", "dialog");
        }
        function OnApplydataFormMaster() {//目標dataform
            if ($("#dataFormMasterDepartment").combobox('getValue') == '') {
                alert("請填'部門'");
                $("#dataFormMasterDepartment").next(".combo").find(".combo-text").focus();
                return false;
            }
            if ($("#dataFormMasterUnit").val() == '') {
                alert("請填'單位'");
                $("#dataFormMasterUnit").focus();
                return false;
            }
        }

        function OnSelectDepartmentDFReal(row) {//實績dataform的部門連動"目標單位"
            $("#dataFormRealUnit").val(row.Unit);
        }
        function OnLoaddataFormReal() {//實績dataform的部門篩選
            if (userDepartment == '人才服務') {
                $("#dataFormRealDepartment").combobox("enable");
                $("#dataFormRealDepartment").combobox("setWhere", "Department in ('人才服務台北','人才服務新竹')");
            } else if (userDepartment == '經營企劃室' || userDepartment == '管理室') {
                $("#dataFormRealDepartment").combobox("enable");
                $("#dataFormRealDepartment").combobox("setWhere", "Department not in ('媒體部','顧問室','資訊部','中高階獵才部')");
            } else if (userDepartment == '外勞事業部/傑報幸福村') {
                $("#dataFormRealDepartment").combobox("enable");
                $("#dataFormRealDepartment").combobox("setWhere", "Department in ('外勞事業部','傑報幸福村')");
            }else if (userDepartment != 'undefined') {
                $("#dataFormRealDepartment").combobox("select", userDepartment);
                $("#dataFormRealDepartment").combobox("disable");
            } else {
                $("#dataFormRealDepartment").combobox("setWhere", "1=0");
                $("#dataFormRealDepartment").combobox("disable");
            }
        }
        function OnLoadSuccessdataGridReal() {//看到實績資料的篩選
            if (!$(this).data("firstload") && $(this).data("firstload", true)) {
                if (userDepartment == '人才服務') {
                    $("#dataGridReal").datagrid("setWhere", "c2.Department in ('人才服務台北','人才服務新竹')");
                } else if (userDepartment == '經營企劃室' || userDepartment == '管理室') {
                    $("#dataGridReal").datagrid("setWhere", "1=1");
                } else if (userDepartment == '外勞事業部/傑報幸福村') {
                    $("#dataGridReal").datagrid("setWhere", "c2.Department in ('外勞事業部','傑報幸福村')");
                } else if (userDepartment != 'undefined') {
                    $("#dataGridReal").datagrid("setWhere", "c2.Department='" + userDepartment + "'");
                } else {
                    $("#dataGridReal").datagrid("setWhere", "1=0");
                }
            }
        }
        //function OnLoadSuccessdataFormReal() {
        //    $("#dataFormRealDepartment").combobox("setWhere", "Department ='" + userDepartment + "'");
        //}
        function OnDeletedataGridView() {
            if (userid == '335' || userid == '060' || userid == '009' || userid == '286' || userid == '015') {
                return true;
            } else {
                return false;
            }
        }
        function OnUpdatedataGridView() {
            if (userid == '335' || userid == '060' || userid == '009' || userid == '286' || userid == '015') {
                return true;
            } else {
                return false;
            }
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="季目標設定" ShowSubmitDiv="True" Width="900px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="Competition" HorizontalColumnsCount="5" RemoteName="sCompetition.Competition" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="True" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApply="OnApplydataFormMaster"  >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="年" Editor="text" FieldName="Year" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="季" Editor="text" FieldName="Quarter" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="30" />
                        <JQTools:JQFormColumn Alignment="left" Caption="部門" Editor="infocombobox" FieldName="Department" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" EditorOptions="valueField:'ORG_DESC',textField:'ORG_DESC',remoteName:'sCompetition.SYS_ORG',tableName:'SYS_ORG',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:220" />
                        <JQTools:JQFormColumn Alignment="left" Caption="季目標" Editor="text" FieldName="QuarterTarget" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="指標/單位" Editor="text" EditorOptions="" FieldName="Unit" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="AutoKey" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="DefaultMethodYear" DefaultValue="" FieldName="Year" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="DefaultMethodQuarter" FieldName="Quarter" RemoteMethod="False" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
            <JQTools:JQDataGrid ID="dataGridReal" data-options="pagination:true,view:commandview" RemoteName="sCompetition.Competition2" runat="server" AutoApply="True"
                DataMember="Competition2" Pagination="True" QueryTitle="" EditDialogID="JQDialog2"
                Title="實績清單" DuplicateCheck="True" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="20" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnLoadSuccess="OnLoadSuccessdataGridReal">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" Visible="False" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="日期" Editor="datebox" FieldName="Date" MaxLength="0" Visible="true" Width="90" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Year" Editor="text" FieldName="Year" Visible="False" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Month" Editor="text" FieldName="Month" Visible="False" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="部門" Editor="text" FieldName="Department" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="實績" Editor="text" FieldName="Real" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="指標/單位" Editor="text" FieldName="Unit" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="建立者" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="text" FieldName="CreateDate" Format="yyyy-mm-dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="修改者" Editor="text" FieldName="LastUpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="text" FieldName="LastUpdateDate" Format="yyyy-mm-dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="實績新增" />
                    <JQTools:JQToolItem Icon="icon-view" ItemType="easyui-linkbutton" OnClick="OnClickToolItemDataGridReal"
                        Text="季目標清單" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="日期起迄" Condition="=" DataType="string" Editor="datebox" FieldName="Date" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="~" Condition="=" DataType="string" Editor="datebox" FieldName="Date" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <br />
            <JQTools:JQDialog ID="JQDialog0" runat="server" Width="700px" Title="季目標清單">
                <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sCompetition.Competition" runat="server" AutoApply="True"
                DataMember="Competition" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="" DuplicateCheck="True" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnLoadSuccess="OnLoaddataGridView" OnDelete="OnDeletedataGridView" OnUpdate="OnUpdatedataGridView" Width="620px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Format="" Visible="False" Width="90" MaxLength="0" />
                        <JQTools:JQGridColumn Alignment="left" Caption="年" Editor="numberbox" FieldName="Year" Format="" Visible="true" Width="90" />
                        <JQTools:JQGridColumn Alignment="left" Caption="季" Editor="numberbox" FieldName="Quarter" Visible="True" Width="30" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="部門" Editor="text" FieldName="Department" Format="" Visible="True" Width="90" MaxLength="0" />
                        <JQTools:JQGridColumn Alignment="left" Caption="季目標" Editor="numberbox" FieldName="QuarterTarget" Visible="True" Width="90" Format="">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="指標/單位" Editor="text" FieldName="Unit" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180">
                        </JQTools:JQGridColumn>
                    </Columns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    </TooItems>
                    <QueryColumns>
                        <JQTools:JQQueryColumn AndOr="and" Caption="年" Condition="=" DataType="string" Editor="infocombobox" FieldName="Year" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" EditorOptions="valueField:'Year',textField:'Year',remoteName:'sCompetition.Year_Query',tableName:'Year_Query',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="季" Condition="=" DataType="string" Editor="infocombobox" FieldName="Quarter" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" EditorOptions="valueField:'Quarter',textField:'Quarter',remoteName:'sCompetition.Quarter_Query',tableName:'Quarter_Query',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    </QueryColumns>
                </JQTools:JQDataGrid>
            </JQTools:JQDialog>

            <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormReal" Title="實績輸入" Width="700px" DialogLeft="370px" DialogTop="70px">
                <JQTools:JQDataForm ID="dataFormReal" runat="server" DataMember="Competition2" HorizontalColumnsCount="6" RemoteName="sCompetition.Competition2" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="True" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="OnLoaddataFormReal" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="日期" Editor="datebox" FieldName="Date" MaxLength="0" NewRow="False" OnBlur="" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Year" Editor="text" FieldName="Year" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Month" Editor="text" FieldName="Month" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="部門" Editor="infocombobox" FieldName="Department" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" EditorOptions="valueField:'Department',textField:'Department',remoteName:'sCompetition.dataFormReal_Department_cb',tableName:'dataFormReal_Department_cb',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:OnSelectDepartmentDFReal,panelHeight:220" />
                        <JQTools:JQFormColumn Alignment="left" Caption="實績" Editor="text" FieldName="Real" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="指標/單位" Editor="text" EditorOptions="" FieldName="Unit" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="180" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultReal" runat="server" BindingObjectID="dataFormReal" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="AutoKey" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateReal" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
        <p>
            </p>
    </form>
</body>
</html>
