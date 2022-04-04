<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ERP_Setting_SalesType.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //將Focus 欄位背景顏色改為黃色
            $(function () {
                $("input, select, textarea").focus(function () {
                    $(this).css("background-color", "yellow");
                });
                $("input, select, textarea").blur(function () {
                    $(this).css("background-color", "white");
                });
            });
        });
        function genCheckBox(val) {
            if (val)
                return "<input  type='checkbox' checked='true' checked onclick='return false;'  />";
            else
                return "<input  type='checkbox' onclick='return false;' />";
        }
        function JQDataFormOnLoadSucess() {
            if (getEditMode($("#dataFormMaster")) == 'updated') {
                $("#dataFormMasterSalesTypeID").attr('disabled', true);
            }
            $("#dataFormMasterSalesTypeID").focus();
        }
        function JQDataFormOnApplied() {
            $("#dataGridView").datagrid('reload');
        }
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERP_Setting_SalesType.SalesType" runat="server" AutoApply="True"
                DataMember="SalesType" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="銷貨類別設定" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="15,30,45,60" PageSize="15" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="1080px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="編號" Editor="numberbox" FieldName="AutoKey" Format="" Visible="False" Width="45" />
                    <JQTools:JQGridColumn Alignment="left" Caption="代號" Editor="text" FieldName="SalesTypeID" Format="" MaxLength="0" Visible="true" Width="35" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="名稱" Editor="text" FieldName="SalesTypeName" Format="" MaxLength="0" Visible="true" Width="110" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="公司別" Editor="infocombobox" FieldName="InsGroupID" Format="" Visible="true" Width="90" EditorOptions="valueField:'InsGroupID',textField:'ShortName',remoteName:'sERP_Setting_SalesType.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="單位" Editor="text" FieldName="Unit" Format="" MaxLength="0" Visible="true" Width="30" />
                    <JQTools:JQGridColumn Alignment="left" Caption="同步DB" Editor="text" FieldName="SyncDB" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="360">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="有效" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40" FormatScript="genCheckBox">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Visible="true" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="" Visible="true" Width="75" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修正人員" Editor="text" FieldName="LastUpdateBy" Format="" MaxLength="0" Visible="true" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修正日期" Editor="datebox" FieldName="LastUpdateDate" Format="" Visible="true" Width="75" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" Visible="False" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消" Visible="False"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" Visible="False" />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="銷貨類別設定">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="SalesType" HorizontalColumnsCount="2" RemoteName="sERP_Setting_SalesType.SalesType" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="JQDataFormOnLoadSucess" OnApplied="JQDataFormOnApplied" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="編號" Editor="numberbox" FieldName="AutoKey" Format="" Width="180" Visible="False" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨類別代號" Editor="text" FieldName="SalesTypeID" Format="" maxlength="0" Width="175" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨類別名稱" Editor="text" FieldName="SalesTypeName" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="infocombobox" FieldName="InsGroupID" Format="" Width="180" EditorOptions="valueField:'InsGroupID',textField:'ShortName',remoteName:'sERP_Setting_SalesType.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="單位" Editor="text" FieldName="Unit" Format="" maxlength="0" Width="180" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="同步DB" Editor="text" FieldName="SyncDB" Span="2" Visible="True" Width="438" />
                        <JQTools:JQFormColumn Alignment="left" Caption="有效" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="20" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="False" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
