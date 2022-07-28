<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HRM_REC_Consultants_Management.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        //焦點欄位變顏色
        $(function () {
            $("input, select, textarea").focus(function () {
                $(this).css("background-color", "yellow");
            });

            $("input, select, textarea").blur(function () {
                $(this).css("background-color", "white");
            });
        });

        function sCheckBox(val) {
            if (val)
                return "<input  type='checkbox' checked='true' checked onclick='return false;' />";
            else
                return "<input  type='checkbox' onclick='return false;' />";
        }

        function queryGrid(dg) {

            if ($(dg).attr('id') == 'dataGridView') {
                var result = [];
                var ConsultantName = $('#ConsultantName_Query').val();//姓名	   
                var SalesTeamID = $('#SalesTeamID_Query').combobox('getValue');//業務單位

                if (ConsultantName != '') result.push("ConsultantName like '%" + ConsultantName + "%'");
                if (SalesTeamID != '') result.push("SalesTeamID=" + SalesTeamID);
                $(dg).datagrid('setWhere', result.join(' and '));
            }
        }
        function DFOnApplied() {
            $('#dataGridView').datagrid("reload");
            InsertHandler();//新增招募系統對照檔

        }
        // 修改多選選項對應的文字
        function InsertHandler() {
            var row = $('#dataGridView').datagrid('getSelected');
            
            var EmpID = $('#dataFormMasterEmpID').val();

            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_REC_Consultants_Management.REC_Consultants', //連接的Server端，command
                data: "mode=method&method=" + "InsertJBRecruitHandler" + "&parameters=" + encodeURIComponent(EmpID), //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                cache: false,
                async: false,
                success: function (data) {
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });

        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="_HRM_REC_Consultants_Management.REC_Consultants" runat="server" AutoApply="True"
                DataMember="REC_Consultants" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="招募列表" Width="890px" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="15,30,45" PageSize="15" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" BufferView="False" NotInitGrid="False" RowNumbers="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="代號" Editor="text" FieldName="ID" Width="30" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="招募姓名" Editor="text" FieldName="ConsultantName" MaxLength="50" Width="85" />
                    <JQTools:JQGridColumn Alignment="left" Caption="英文名" Editor="text" FieldName="ConsultantEName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="電話" Editor="text" FieldName="ConsultantTel" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="手機" Editor="text" FieldName="ConsultantMobile" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100" />
                    <JQTools:JQGridColumn Alignment="center" Caption="所屬業務單位" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'SalesTeamName',remoteName:'_HRM_REC_Consultants_Management.Rec_SalesTeam',tableName:'Rec_SalesTeam',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:true,panelHeight:200" FieldName="SalesTeamID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="信箱" Editor="text" FieldName="ConsultantEmail" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="性別" Editor="text" FieldName="GenderText" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="55">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="是否有效" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Format="" FormatScript="sCheckBox">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                   <%-- <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />--%>
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="業務單位" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'SalesTeamName',remoteName:'_HRM_REC_Consultants_Management.Rec_SalesTeam',tableName:'Rec_SalesTeam',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:true,panelHeight:130" FieldName="SalesTeamID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="姓名" Condition="%" DataType="string" Editor="text" FieldName="ConsultantName" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="70" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="招募維護" ShowModal="true">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="REC_Consultants" HorizontalColumnsCount="3" RemoteName="_HRM_REC_Consultants_Management.REC_Consultants" Closed="False" ContinueAdd="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Dialog" disapply="False" IsAutoPause="False" IsRejectON="False" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" OnApplied="DFOnApplied" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="代號" Editor="text" FieldName="ID" Width="180" ReadOnly="True" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="招募姓名" Editor="inforefval" FieldName="ConsultantName" maxlength="50" Width="110" Span="1" EditorOptions="title:'選擇招募',panelWidth:250,remoteName:'_HRM_REC_Consultants_Management.infoUSERSUSERS',tableName:'infoUSERSUSERS',columns:[],columnMatches:[{field:'EmpID',value:'USERID'},{field:'ConsultantEmail',value:'EMAIL'}],whereItems:[],valueField:'USERNAME',textField:'USERNAME',valueFieldCaption:'姓名',textFieldCaption:'USERNAME',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'"/>
                        <JQTools:JQFormColumn Alignment="left" Caption="英文名" Editor="text" FieldName="ConsultantEName" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="性別" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:120,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,selectOnly:true,items:[{text:'女',value:'false'},{text:'男',value:'true'}]" FieldName="Gender" MaxLength="20" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="55" />                       
                         <JQTools:JQFormColumn Alignment="left" Caption="電話" Editor="text" FieldName="ConsultantTel" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="110" />
                        <JQTools:JQFormColumn Alignment="left" Caption="手機" Editor="text" FieldName="ConsultantMobile" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="工號" Editor="text" FieldName="EmpID" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="70" />                        
                        <JQTools:JQFormColumn Alignment="left" Caption="業務單位" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'SalesTeamName',remoteName:'_HRM_REC_Consultants_Management.Rec_SalesTeam',tableName:'Rec_SalesTeam',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTeamID" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="信箱" Editor="text" FieldName="ConsultantEmail" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="250" />
                        <JQTools:JQFormColumn Alignment="center" Caption="是否有效" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" maxlength="20" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修正人員" Editor="text" FieldName="LastUpdateBy" maxlength="20" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修正日期" Editor="datebox" FieldName="LastUpdateDate" Width="180" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn DefaultValue="0" FieldName="ID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn DefaultValue="_usercode" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn DefaultValue="_usercode" FieldName="LastUpdateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn DefaultValue="_today" FieldName="LastUpdateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="IsActive" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ConsultantName" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="EmpID" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Gender" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ConsultantEName" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="SalesTeamID" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>

   
</body>
</html>
