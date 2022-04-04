<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_InsGroup.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
     <script type="text/javascript">
         var backcolor = "#E8FFE8"
         $(document).ready(function () {
             $(function () {

                 $("input, select, textarea").focus(function () {
                     $(this).css("background-color", "#FFFFDE");
                 });
                 $("input, select, textarea").blur(function () {
                     $(this).css("background-color", backcolor);
                 });
             });
         });
         function genCheckBox(val) {
             if (val)
                 return "<input  type='checkbox' checked='true' />";
             else
                 return "";
             //  return "<input  type='checkbox' />";
         }
     </script>   

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERPInsGroup.InsGroup" runat="server" AutoApply="True"
                DataMember="InsGroup" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="公司別維護" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="代碼" Editor="text" FieldName="InsGroupID" Format="" Width="40" />
                    <JQTools:JQGridColumn Alignment="left" Caption="公司名稱" Editor="text" FieldName="InsGroupName" Format="" MaxLength="0" Width="250" />
                    <JQTools:JQGridColumn Alignment="left" Caption="公司簡稱" Editor="text" FieldName="ShortName" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="統一編號" Editor="text" FieldName="TaxNo" Format="" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="負責人" Editor="text" FieldName="Person" Format="" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="API代碼" Editor="text" FieldName="APIWebCode" Format="" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="API密碼" Editor="text" FieldName="APIPassword" Format="" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="會計簽核角色" Editor="infocombobox" EditorOptions="valueField:'GROUPID',textField:'GROUPNAME',remoteName:'sERPInsGroup.GroupRoles',tableName:'GroupRoles',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AccountantRoleID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="生效" Editor="text" FieldName="IsActive" Format="" Width="40" FormatScript="genCheckBox" />
                    <JQTools:JQGridColumn Alignment="right" Caption="InsLaborRate" Editor="numberbox" FieldName="InsLaborRate" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="InsBusinessTax" Editor="text" FieldName="InsBusinessTax" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LaborInsNo" Editor="text" FieldName="LaborInsNo" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LaborInsNoChk" Editor="text" FieldName="LaborInsNoChk" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="HealthInsNo" Editor="text" FieldName="HealthInsNo" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="HealthInsSubID" Editor="numberbox" FieldName="HealthInsSubID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="InsAccount" Editor="text" FieldName="InsAccount" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="InsNo" Editor="text" FieldName="InsNo" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Address" Editor="text" FieldName="Address" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Tel" Editor="text" FieldName="Tel" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="AssetControl" Editor="text" FieldName="AssetControl" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="IsAssetControl" Editor="text" FieldName="IsAssetControl" Format="" Width="120" Visible="False" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
              <%--      <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消"  />--%>
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="公司別維護" DialogLeft="50px" DialogTop="50px" Width="720px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="InsGroup" HorizontalColumnsCount="3" RemoteName="sERPInsGroup.InsGroup" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="代碼" Editor="text" FieldName="InsGroupID" Format="" Width="30" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司名稱" Editor="text" FieldName="InsGroupName" Format="" maxlength="0" Width="250" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司簡稱" Editor="text" FieldName="ShortName" Format="" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="統一編號" Editor="text" FieldName="TaxNo" Format="" maxlength="0" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="負責人" Editor="text" FieldName="Person" Format="" maxlength="0" Width="100" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="API代碼" Editor="text" FieldName="APIWebCode" Format="" maxlength="0" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="API密碼" Editor="text" FieldName="APIPassword" Format="" maxlength="0" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="生效" Editor="checkbox" FieldName="IsActive" Format="" Width="90" EditorOptions="on:1,off:0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="InsLaborRate" Editor="numberbox" FieldName="InsLaborRate" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="InsBusinessTax" Editor="text" FieldName="InsBusinessTax" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LaborInsNo" Editor="text" FieldName="LaborInsNo" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LaborInsNoChk" Editor="text" FieldName="LaborInsNoChk" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="HealthInsNo" Editor="text" FieldName="HealthInsNo" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="HealthInsSubID" Editor="numberbox" FieldName="HealthInsSubID" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="InsAccount" Editor="text" FieldName="InsAccount" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="InsNo" Editor="text" FieldName="InsNo" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Address" Editor="text" FieldName="Address" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Tel" Editor="text" FieldName="Tel" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="AssetControl" Editor="text" FieldName="AssetControl" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="IsAssetControl" Editor="text" FieldName="IsAssetControl" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="會計簽核角色" Editor="infocombobox" EditorOptions="valueField:'GROUPID',textField:'GROUPNAME',remoteName:'sERPInsGroup.GroupRoles',tableName:'GroupRoles',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AccountantRoleID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="105" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="InsGroupName" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="TaxNo" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="InsGroupID" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
        <script type="text/javascript">
               $(":input").css("background", backcolor);
        </script>
    </form>
</body>
</html>
