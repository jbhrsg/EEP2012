<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_OutWorkApply.aspx.cs" Inherits="Template_JQuerySingle1" %>

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
                       $(this).css("background-color", '#FFFFE8');
                   });
                   $("input, select, textarea").blur(function () {
                       $(this).css("background-color", backcolor);
                   });
               });
            
           })
           function GetUserOrg() {
               var UserID = getClientInfo("UserID");
               var _return = "";
               $.ajax({
                   type: "POST",
                   url: '../handler/jqDataHandle.ashx?RemoteName=sOutWorkMaster.OutWorkMaster',
                   data: "mode=method&method=" + "GetUserOrgNOs" + "&parameters=" + UserID,
                   cache: false,
                   async: false,
                   success: function (data) {
                       var rows = $.parseJSON(data);
                       if (rows.length == 1) {
                           _return = rows[0].OrgNO;
                       }
                   }
               })
               return _return;
           }
           function GetUserOrgNOParent() {
               var UserID = getClientInfo("UserID");
               var _return = "";
               $.ajax({
                   type: "POST",
                   url: '../handler/jqDataHandle.ashx?RemoteName=sOutWorkMaster.OutWorkMaster',
                   data: "mode=method&method=" + "GetUserOrgNOs" + "&parameters=" + UserID,
                   cache: false,
                   async: false,
                   success: function (data) {
                       var rows = $.parseJSON(data);
                       if (rows.length == 1) {
                           _return = rows[0].OrgNOParent;
                       }
                   }
               })
               return _return;
           }
           function dataFormMasterOnApply() {
               var WorkPlace = $("#dataFormMasterWorkPlace").val();
               if (WorkPlace == "" || WorkPlace == undefined) {
                   alert('注意!!請填入工作地點');
                   return false;
               }
               var OutWorkDateDulYN = CheckOutWorkDateDul();
               if (OutWorkDateDulYN != "N") {
                   alert("申請工作日期重複,請檢查");
                   return false;
               }

           }
           //檢查申請日期區間是否重疊
           function CheckOutWorkDateDul() {
               var DateS = $("#dataFormMasterOWDateS").datebox('getValue');
               var DateE = $("#dataFormMasterOWDateE").datebox('getValue');
               var ApplyEmp = $("#dataFormMasterApplyEmpID").combobox('getValue');
               var UserID = getClientInfo("UserID");
               var cnt;
               $.ajax({
                   type: "POST",
                   url: '../handler/jqDataHandle.ashx?RemoteName=sOutWorkMaster.OutWorkMaster', //連接的Server端，command
                   data: "mode=method&method=" + "CheckOutWorkDateDul" + "&parameters=" + DateS + "," + DateE + "," + ApplyEmp + "," + UserID,
                   cache: false,
                   async: false,
                   success: function (data) {
                       cnt = data;
                   }
               });
               return cnt;
               //if ((cnt == "N")) {
               //    return true;
               //}
               //else {
               //    alert('此公務車起迄時間已被 ' + cnt + ' 行程所佔用');
               //    $("#dataFormMasterDeviceItemsID").combobox('setValue', '');
               //    return false;
               //}
           }
       </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sOutWorkMaster.OutWorkMaster" runat="server" AutoApply="True"
                DataMember="OutWorkMaster" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="OutWorkNO" Editor="text" FieldName="OutWorkNO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyEmpID" Editor="text" FieldName="ApplyEmpID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="OWDateS" Editor="datebox" FieldName="OWDateS" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="OWDateE" Editor="datebox" FieldName="OWDateE" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyGist" Editor="text" FieldName="ApplyGist" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyOrg_NO" Editor="text" FieldName="ApplyOrg_NO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Org_NOParent" Editor="text" FieldName="Org_NOParent" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" />
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

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="遠端工作申請" DialogLeft="10px" DialogTop="10px" Width="420px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="OutWorkMaster" HorizontalColumnsCount="2" RemoteName="sOutWorkMaster.OutWorkMaster" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="True" IsAutoPause="False" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="True" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApply="dataFormMasterOnApply" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="申請單號" Editor="text" FieldName="OutWorkNO" Format="" maxlength="0" Width="115" ReadOnly="True" Span="2" NewRow="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請者" Editor="infocombobox" FieldName="ApplyEmpID" Format="" maxlength="0" Width="120" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sOutWorkMaster.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" ReadOnly="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請部門" Editor="infocombobox" FieldName="ApplyOrg_NO" Format="" Width="120" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sOutWorkMaster.Organization',tableName:'Organization',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" maxlength="0" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="起始日期" Editor="datebox" FieldName="OWDateS" Format="yyyy/mm/dd" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="終止日期" Editor="datebox" FieldName="OWDateE" Format="yyyy/mm/dd" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="起始時間" Editor="text" FieldName="OWTimeS" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="115" />
                        <JQTools:JQFormColumn Alignment="left" Caption="終止時間" Editor="text" FieldName="OWTimeE" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="115" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請內容" Editor="textarea" FieldName="ApplyGist" Format="" maxlength="0" Width="300" Span="2" EditorOptions="height:40" />
                        <JQTools:JQFormColumn Alignment="left" Caption="工作地點" Editor="text" FieldName="WorkPlace" maxlength="0" Span="2" Width="300" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請部門" Editor="infocombobox" FieldName="Org_NOParent" Format="" maxlength="0" Width="120" Visible="False" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sOutWorkMaster.Organization',tableName:'Organization',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="ApplyEmpID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="OWDateS" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="OWDateE" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0830" FieldName="OWTimeS" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1730" FieldName="OWTimeE" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="OutWorkNO" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetUserOrg" FieldName="ApplyOrg_NO" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetUserOrgNOParent" FieldName="Org_NOParent" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="遠端上班" FieldName="ApplyGist" RemoteMethod="True" />
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
