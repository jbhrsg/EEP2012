<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBAST_AssetLicence.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var flagIDs = ['#IsPublicView'];
            $(flagIDs.toString()).each(function () {
                var captionTd = $(this).closest('td').prev('td');
                captionTd.css({ color: '#8A2BE2' });
            });
            //Grid設定寬度
            var dgid = $('#dataGridView');
            var queryPanel = getInfolightOption(dgid).queryDialog;
            if (queryPanel)
                $(queryPanel).panel('resize', { width: 560 });

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
        function dataFormMasterLoadSucess() {
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                $("#dataFormMasterAssetLiceTypeName").combobox('setWhere', "AssetLiceType='OS'");
            }
            else {
                var AssetLiceType = $("#dataFormMasterAssetLiceType").combobox('getValue');
                $("#dataFormMasterAssetLiceTypeName").combobox('setWhere', "AssetLiceType='" + AssetLiceType+"'");
            }
         }
        function OnSelectAssetLiceType(rowData) {
            $("#dataFormMasterAssetLiceTypeName").combobox('setValue', '');
            $("#dataFormMasterAssetLiceTypeName").combobox('setWhere', "AssetLiceType='" + rowData.AssetLiceType+"'");
        }
</script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sAssetLicence.AssetLicence" runat="server" AutoApply="True"
                DataMember="AssetLicence" Pagination="True" QueryTitle="查詢" EditDialogID="JQDialog1"
                Title="資產設備序號" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="20" QueryAutoColumn="False" QueryLeft="10px" QueryMode="Window" QueryTop="20px" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="1020px" BufferView="False" NotInitGrid="False" RowNumbers="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="編號" Editor="numberbox" FieldName="AssetLiceID" Format="" Width="40" />
                    <JQTools:JQGridColumn Alignment="left" Caption="類別" Editor="text" FieldName="AssetLiceType" Format="" MaxLength="0" Width="60" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="項目" Editor="text" FieldName="AssetLiceTypeName" Format="" MaxLength="0" Width="180" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="序號" Editor="text" FieldName="AssetLiceCode" Format="" MaxLength="0" Width="360" />
                    <JQTools:JQGridColumn Alignment="left" Caption="到期日期" Editor="datebox" FieldName="AssetLiceDueDate" Format="yyyy/mm/dd" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="配屬設備" Editor="text" FieldName="AssignTo" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="360" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"    OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="查詢" />
                     <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出EXCEL" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="類別" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'AssetLiceType',textField:'AssetLiceType',remoteName:'sAssetLicence.LicenceType',tableName:'LicenceType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AssetLiceType" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="項目" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'AssetLiceTypeName',textField:'AssetLiceTypeName',remoteName:'sAssetLicence.LicenceName',tableName:'LicenceName',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AssetLiceTypeName" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="資產設備序號" DialogLeft="30px" DialogTop="30px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="AssetLicence" HorizontalColumnsCount="2" RemoteName="sAssetLicence.AssetLicence" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" OnLoadSuccess="dataFormMasterLoadSucess" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="編號" Editor="numberbox" FieldName="AssetLiceID" Format="" Width="40" ReadOnly="True" Span="2" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="類別" Editor="infocombobox" FieldName="AssetLiceType" Format="" maxlength="0" Width="90" EditorOptions="valueField:'AssetLiceType',textField:'AssetLiceType',remoteName:'sAssetLicence.LicenceType',tableName:'LicenceType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OnSelectAssetLiceType,panelHeight:200" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="項目" Editor="infocombobox" FieldName="AssetLiceTypeName" Format="" maxlength="0" Width="180" EditorOptions="valueField:'AssetLiceTypeName',textField:'AssetLiceTypeName',remoteName:'sAssetLicence.LicenceName',tableName:'LicenceName',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="序號" Editor="text" FieldName="AssetLiceCode" Format="" Width="300" />
                        <JQTools:JQFormColumn Alignment="left" Caption="到期日期" Editor="datebox" FieldName="AssetLiceDueDate" Format="yyyy/mm/dd" Width="90" maxlength="0" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="text" FieldName="AssetLiceNotes" ReadOnly="False" Span="2" Width="380" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AssetLiceID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="OS" FieldName="AssetLiceType" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
