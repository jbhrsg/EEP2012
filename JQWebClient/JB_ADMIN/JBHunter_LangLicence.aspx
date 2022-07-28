<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBHunter_LangLicence.aspx.cs" Inherits="Template_JQuerySingle1" %>

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
     function CheckDataGridViewDelete() {
            var row = $('#dataGridView').datagrid('getSelected');//取得當前主檔中選中的那個Data
            var cnt;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sLangLicence.HUT_LangLicence', //連接的Server端，command
                data: "mode=method&method=" + "CheckDelLangLincence" + "&parameters=" + row.LangLicenceID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
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
                alert('此語言證照已有職缺語言參考使用,無法刪除!!');

                return false;
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sLangLicence.HUT_LangLicence" runat="server" AutoApply="True"
                DataMember="HUT_LangLicence" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="語言證照" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnDelete="CheckDataGridViewDelete" Width="720px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="代號" Editor="text" FieldName="LangLicenceID" Format="" Width="60" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="代號" Editor="text" FieldName="LangID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="語言" Editor="text" FieldName="LangName" Format="" MaxLength="0" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="證照名稱" Editor="text" FieldName="LangLicenceName" Format="" MaxLength="0" Width="250" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
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

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="語言證照">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="HUT_LangLicence" HorizontalColumnsCount="2" RemoteName="sLangLicence.HUT_LangLicence" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="代號" Editor="text" FieldName="LangLicenceID" Format="" Width="180" ReadOnly="True" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="語言" Editor="infocombobox" FieldName="LangID" Format="" Width="180" EditorOptions="valueField:'LangID',textField:'LangName',remoteName:'sReferences.HUT_ZLangType',tableName:'HUT_ZLangType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="證照名稱" Editor="text" FieldName="LangLicenceName" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LangName" Editor="text" FieldName="LangName" Format="" maxlength="0" Width="180" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="LangLicenceID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
