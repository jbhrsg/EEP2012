<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBHunter_Function.aspx.cs" Inherits="Template_JQuerySingle1" %>

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
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sFunction.HUT_Function" runat="server" AutoApply="True"
                DataMember="HUT_Function" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="JBHunter_Function" Width="720px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="代號" Editor="text" FieldName="FunctionID" Width="30" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職務類別" Editor="text" FieldName="FunctionName" MaxLength="100" Width="250" />
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

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="JBHunter_Function">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="HUT_Function" HorizontalColumnsCount="2" RemoteName="sFunction.HUT_Function" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="代號" Editor="text" FieldName="FunctionID" Width="30" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="職務類別" Editor="text" FieldName="FunctionName" maxlength="100" Width="180"  Span="2" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn DefaultValue="0" FieldName="FunctionID" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
