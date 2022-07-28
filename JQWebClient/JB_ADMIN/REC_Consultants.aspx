<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rec_Consultants.aspx.cs" Inherits="Template_JQuerySingle1" %>

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

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sREC_Consultants.REC_Consultants" runat="server" AutoApply="True"
                DataMember="REC_Consultants" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="顧問列表" Width="890px" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="15,30,45" PageSize="15" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" BufferView="False" NotInitGrid="False" RowNumbers="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="代號" Editor="text" FieldName="ID" Width="30" />
                    <JQTools:JQGridColumn Alignment="left" Caption="顧問" Editor="text" FieldName="ConsultantName" MaxLength="50" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="英文名" Editor="text" FieldName="ConsultantEName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="電話" Editor="text" FieldName="ConsultantTel" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="手機" Editor="text" FieldName="ConsultantMobile" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="所屬業務單位" Editor="inforefval" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'sHunter.HUT_SalesTeam',tableName:'HUT_SalesTeam',columns:[],columnMatches:[],whereItems:[],valueField:'ID',textField:'SalesTeamName',valueFieldCaption:'ID',textFieldCaption:'SalesTeamName',cacheRelationText:true,checkData:true,showValueAndText:false,selectOnly:false" FieldName="SalesTeamID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="信箱" Editor="text" FieldName="ConsultantEmail" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180">
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
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="顧問維護" ShowModal="true">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="REC_Consultants" HorizontalColumnsCount="3" RemoteName="sREC_Consultants.REC_Consultants" Closed="False" ContinueAdd="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" disapply="False" IsAutoPause="False" IsRejectON="False" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="代號" Editor="text" FieldName="ID" Width="180" ReadOnly="True" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="中文名" Editor="text" FieldName="ConsultantName" maxlength="50" Width="110" Span="1"/>
                        <JQTools:JQFormColumn Alignment="left" Caption="英文名" Editor="text" FieldName="ConsultantEName" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="工號" Editor="text" FieldName="EmpID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="70" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電話" Editor="text" FieldName="ConsultantTel" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="110" />
                        <JQTools:JQFormColumn Alignment="left" Caption="手機" Editor="text" FieldName="ConsultantMobile" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="業務單位" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'SalesTeamName',remoteName:'sHunter.HUT_SalesTeam',tableName:'HUT_SalesTeam',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTeamID" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" />
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
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>

   
</body>
</html>
