<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HRM_AttendSetDays.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sSYS_Variable.HRMAttendSetDays" runat="server" AutoApply="True"
                DataMember="HRMAttendSetDays" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="天數限制設定" AllowAdd="True" AllowDelete="False" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="請假終止日期天數" Editor="numberbox" FieldName="AbsentDays" Format="" Visible="true" Width="130" />
                    <JQTools:JQGridColumn Alignment="right" Caption="加班天數" Editor="numberbox" FieldName="OverTimeDays" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="LastUpdateBy" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="LastUpdateDate" Format="" Visible="true" Width="120" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消"  />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="天數修改">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="HRMAttendSetDays" HorizontalColumnsCount="2" RemoteName="sSYS_Variable.HRMAttendSetDays" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="iAutokey" Editor="numberbox" FieldName="iAutokey" Format="" Width="200" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請假終止日期天數" Editor="numberbox" FieldName="AbsentDays" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="加班天數" Editor="numberbox" FieldName="OverTimeDays" Format="" Width="180" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="LastUpdateBy" Format="" maxlength="0" Width="180" NewRow="True" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="180" NewRow="True" Visible="False" />
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
