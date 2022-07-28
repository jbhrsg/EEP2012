<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBHunter_SalesTeam.aspx.cs" Inherits="Template_JQuerySingle1" %>

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
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sSalesTeam.HUT_SalesTeam" runat="server" AutoApply="True"
                DataMember="HUT_SalesTeam" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title=" 業務單位" Width="720px" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="代號" Editor="text" FieldName="ID" Width="30" />
                    <JQTools:JQGridColumn Alignment="left" Caption="業務單位" Editor="text" FieldName="SalesTeamName" MaxLength="50" Width="150" />
                    <JQTools:JQGridColumn Alignment="left" Caption="業務單位地址" Editor="text" FieldName="SalesTeamAddress" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="350" />
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
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title=" 業務單位">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="HUT_SalesTeam" HorizontalColumnsCount="2" RemoteName="sSalesTeam.HUT_SalesTeam" Closed="False" ContinueAdd="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" disapply="False" IsAutoPause="False" IsRejectON="False" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="代號" Editor="text" FieldName="ID" Width="30" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="業務單位" Editor="text" FieldName="SalesTeamName" maxlength="50" Width="250" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="業務單位地址" Editor="text" FieldName="SalesTeamAddress" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="250" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" maxlength="20" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修正人員" Editor="text" FieldName="LastUpdateBy" maxlength="20" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修正日期" Editor="datebox" FieldName="LastUpdateDate" Width="180" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn DefaultValue="_todate" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn DefaultValue="_usercode" FieldName="LastUpdateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn DefaultValue="_today" FieldName="LastUpdateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn DefaultValue="0" FieldName="ID" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
