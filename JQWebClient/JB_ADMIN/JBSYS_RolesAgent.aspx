<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBSYS_RolesAgent.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //設定欄位Caption 變顏色
            var flagIDs = ['#dataFormMasterDispatchAreaID', '#dataFormMasterDispatchAreaManager'];
            $(flagIDs.toString()).each(function () {
                var captionTd = $(this).closest('td').prev('td');
                captionTd.css({ color: '#8A2BE2' });
            });
            //將Focus 欄位背景顏色改為黃色
            $(function () {
                $("input, select, textarea").focus(function () {
                    $(this).css("background-color", "yellow");
                });
                $("input, select, textarea").blur(function () {
                    $(this).css("background-color", "white");
                });
            });
            setTimeout(function () { dataGridViewLoadSuccess(); }, 20);
         })
        function dataFormMaster_OnLoadSuccess() {
            var UserID = getClientInfo("UserID");
            $("#dataFormMasterROLE_ID").combobox('setWhere', "USERID='" + UserID + "'");
            }
        function dateFormMasterOnApply() {
            var ROLE_ID = $("#dataFormMasterROLE_ID").combobox('getValue');
            if (ROLE_ID == "" || ROLE_ID == undefined) {
                alert('注意!!,未選取使用者角色,請選取');
                $("#dataFormMasterROLE_ID").focus();
                return false;
            }
            var AGENT = $("#dataFormMasterAGENT").combobox('getValue');
            if (AGENT == "" || AGENT == undefined) {
                alert('注意!!,未選取代理者,請選取');
                $("#dataFormMasterAGENT").focus();
                return false;
            }
            var FLOW_DESC = $("#dataFormMasterFLOW_DESC").combobox('getValue');
            if (FLOW_DESC == "" || FLOW_DESC == undefined) {
                alert('注意!!,未選取指定流程,請選取');
                $("#dataFormMasterFLOW_DESC").focus();
                return false;
            }
           return CheckRoleAgentFlow();
        }
        function dataGridViewLoadSuccess() {
            var UserID = getClientInfo("UserID");
            var RoleStr = GetEmpRoleStr();
            var whereStr = " ROLE_ID in (" + RoleStr + ")";
            $('#dataGridView').datagrid('setWhere', whereStr);
        }
        //取得使用者角色清單
        function GetEmpRoleStr() {
            var UserID = getClientInfo("UserID");
            var RoleStr = "";
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sSYS_ROLES_AGENT.SYS_ROLES_AGENT', //連接的Server端，command
                data: "mode=method&method=" + "GetEmpRoleStr" + "&parameters=" + UserID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {                    
                    if (data != false) {
                        RoleStr = data;                        
                    }                                        
                }
            });
            return RoleStr;
        }

        //檢查使用者角色,代理者,指定流程是否重複?
        function CheckRoleAgentFlow() {
            var ROLEID = $("#dataFormMasterROLE_ID").combobox('getValue');
            var AGENT = $("#dataFormMasterAGENT").combobox('getValue');
            var FLOW = $("#dataFormMasterFLOW_DESC").combobox('getValue');
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                var cnt;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sSYS_ROLES_AGENT.SYS_ROLES_AGENT', //連接的Server端，command
                    data: "mode=method&method=" + "CheckRoleAgentFlow" + "&parameters=" +ROLEID+","+AGENT+","+FLOW,  //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != false) {
                            cnt = $.parseJSON(data);
                        }
                    }
                });
                if ((cnt == "0") || (cnt == "undefined")) {
                    return true;
                }
                else {
                    alert('注意!!您設定的代理人與指定流程已存在,請使用修改方式修正');
                    return false;
                }
            }
            else return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sSYS_ROLES_AGENT.SYS_ROLES_AGENT" runat="server" AutoApply="True"
                DataMember="SYS_ROLES_AGENT" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="代理人設定" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="1080px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="使用者角色" Editor="infocombobox" FieldName="ROLE_ID" Format="" MaxLength="0" Width="240" EditorOptions="valueField:'GROUPID',textField:'GROUPNAME',remoteName:'sSYS_ROLES_AGENT.GROUPS',tableName:'GROUPS',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="代理者" Editor="infocombobox" FieldName="AGENT" Format="" MaxLength="0" Width="75" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sSYS_ROLES_AGENT.USERS',tableName:'USERS',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="指定流程" Editor="text" FieldName="FLOW_DESC" Format="" MaxLength="0" Width="180" />
                    <JQTools:JQGridColumn Alignment="left" Caption="請始日期" Editor="datebox" FieldName="START_DATE" Format="yyyy/mm/dd" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="起始時間" Editor="text" FieldName="START_TIME" Format="" MaxLength="0" Width="55" />
                    <JQTools:JQGridColumn Alignment="left" Caption="終止日期" Editor="datebox" FieldName="END_DATE" Format="yyyy/mm/dd" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="終止時間" Editor="text" FieldName="END_TIME" Format="" MaxLength="0" Width="55" />
                    <JQTools:JQGridColumn Alignment="left" Caption="平行代理" Editor="text" FieldName="PAR_AGENT" Format="" MaxLength="0" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="REMARK" Format="" MaxLength="0" Width="180" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CREATEBY" Editor="text" FieldName="CREATEBY" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CREATEDATE" Editor="datebox" FieldName="CREATEDATE" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LASTUPDATEBY" Editor="text" FieldName="LASTUPDATEBY" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LASTUPDATEDATE" Editor="datebox" FieldName="LASTUPDATEDATE" Format="" Width="120" Visible="False" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="代理人設定" DialogLeft="20px" DialogTop="30px" Width="1080px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="SYS_ROLES_AGENT" HorizontalColumnsCount="7" RemoteName="sSYS_ROLES_AGENT.SYS_ROLES_AGENT" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" OnLoadSuccess="dataFormMaster_OnLoadSuccess" OnApply="dateFormMasterOnApply" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="使用者角色" Editor="infocombobox" FieldName="ROLE_ID" Format="" maxlength="0" Width="295" EditorOptions="valueField:'GROUPID',textField:'GROUPNAME',remoteName:'sSYS_ROLES_AGENT.USERSGROUP',tableName:'USERSGROUP',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" Span="7" />
                        <JQTools:JQFormColumn Alignment="left" Caption="代理人" Editor="infocombobox" FieldName="AGENT" Format="" maxlength="0" Width="95" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sSYS_ROLES_AGENT.USERS',tableName:'USERS',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="指定流程" Editor="infocombobox" FieldName="FLOW_DESC" Format="" maxlength="0" Width="150" EditorOptions="valueField:'FLOW_DESC',textField:'FLOW_DESC',remoteName:'sSYS_ROLES_AGENT.FLOWTYPE',tableName:'FLOWTYPE',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="起始日期" Editor="datebox" FieldName="START_DATE" Format="yyyy/mm/dd" maxlength="0" Width="90" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="起始時間" Editor="text" FieldName="START_TIME" Format="" maxlength="0" Width="45" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="終止日期" Editor="datebox" FieldName="END_DATE" Format="yyyy/mm/dd" maxlength="0" Width="90" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="終止時間" Editor="text" FieldName="END_TIME" Format="" maxlength="0" Width="45" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="平行代理" Editor="infocombobox" FieldName="PAR_AGENT" Format="" maxlength="0" Width="45" EditorOptions="items:[{value:'Y',text:'Y',selected:'false'},{value:'N',text:'N',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="text" FieldName="REMARK" Format="" maxlength="0" Width="900" Span="7" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CREATEBY" Editor="text" FieldName="CREATEBY" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CREATEDATE" Editor="datebox" FieldName="CREATEDATE" Format="" Width="180" Visible="False" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LASTUPDATEBY" Editor="text" FieldName="LASTUPDATEBY" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LASTUPDATEDATE" Editor="datebox" FieldName="LASTUPDATEDATE" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="START_DATE" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="END_DATE" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="N" FieldName="PAR_AGENT" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="000000" FieldName="START_TIME" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="235959" FieldName="END_TIME" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="START_DATE" RemoteMethod="False" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="START_TIME" RemoteMethod="False" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="END_DATE" RemoteMethod="False" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="END_TIME" RemoteMethod="False" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
