<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_InvoiceVoidApply.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
     <script type="text/javascript">
         $(document).ready(function () {
             //設定欄位Caption 變顏色
             //var flagIDs = ['#dataFormMasterIsUrgentPay', '#dataFormMasterIsNotPayDate'];
             //$(flagIDs.toString()).each(function () {
             //    var captionTd = $(this).closest('td').prev('td');
             //    captionTd.css({ color: '#8A2BE2' });
             //});
             //將Focus 欄位背景顏色改為黃色
             $(function () {
                 $("input, select, textarea").focus(function () {
                     $(this).css("background-color", "yellow");
                 });
                 $("input, select, textarea").blur(function () {
                     $(this).css("background-color", "white");
                 });
             });
             $(function () {
                 $('#dataFormMasterInvoiceNO').combobox({
                 }
                 ).combo('textbox').blur(function () {
                     InVoiceNOOnselect($('#dataFormMasterInvoiceNO').combobox('getSelectItem'));
                 });
             });
         })
         function dataFormMasterOnLoadSucess() {
             var parameters = Request.getQueryStringByName("P1");
             var mode = Request.getQueryStringByName("NAVIGATOR_MODE");
             if (getEditMode($("#dataFormMaster")) == 'inserted') {
                 GetUserOrgNOs();
                 var EmpFlowAgentList = GetEmpFlowAgentList();
                 var whereStr = " UserID in (" + EmpFlowAgentList + ")";
                 $('#dataFormMasterApplyEmpID').combobox('setWhere', whereStr);
             }
             var InvoiceVoidNO = $("#dataFormMasterInvoiceVoidNO").val();
             var InvoiceNO = $("#dataFormMasterInvoiceNO").combobox('getValue');
             var setWhereStr = "InvoiceVoidNO=" + "'" + InvoiceVoidNO + "' AND InvoiceNO=" + "'" + InvoiceNO + "'";
             $("#dataGridDetail").datagrid('setWhere', setWhereStr);
             $("#dataGridDetail").datagrid('reload');
         }
         function dataFormMasterOnApply() {
             var dataFormMasterInvoiceNO = $("#dataFormMasterInvoiceNO").combobox('getValue');
             if (dataFormMasterInvoiceNO == "" || dataFormMasterInvoiceNO == undefined) {
                 alert('注意!!,未選取發票號碼,請選取');
                 $("#dataFormMasterInvoiceNO").focus();
                 return false;
             }
             var IsExit = IsInvoiceNOExist(dataFormMasterInvoiceNO);
             if (IsExit == false) {
                 alert('發票號碼錯誤,請再選取');
                 return false;
             }
          }
         //取得選取的發票明細資料
         function GetInvoiceVoidApplyDetails(InvoiceVoidNO,InvoiceNO) {
             var UserID = getClientInfo("UserID");
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sInvoiceVoidApply.ERPInvoiceVoidApplyMaster',
                 data: "mode=method&method=" + "GetInvoiceVoidApplyDetails" + " &parameters=" + InvoiceVoidNO + "," + InvoiceNO + ","+ UserID, 
                 cache: false,
                 async: false,
                 success: function (data) {
                     if (data == "True") {
                         // $.messager.confirm('提示訊息', '計畫課程開課成功,按下「確定」離開', function (r) {
                         //     if (r) {
                         var setWhereStr = "InvoiceVoidNO=" + "'" + InvoiceVoidNO + "' AND InvoiceNO=" + "'" + InvoiceNO + "'";
                         $("#dataGridDetail").datagrid('setWhere', setWhereStr);
                         //$("#dataGridDetail").datagrid('reload');
                         //     }
                         // })
                     }
                     else {
                         alert("取得發票明細資料失敗")
                     }
                 }
             });
         }
         function GetEmpFlowAgentList() {
             var UserID = getClientInfo("UserID");
             var Flow = "發票註銷申請單";
             var ReturnStr = "";
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sInvoiceVoidApply.ERPInvoiceVoidApplyMaster',
                 data: "mode=method&method=" + "GetEmpFlowAgentList" + "&parameters=" + UserID + "," + Flow, 
                 cache: false,
                 async: false,
                 success: function (data) {
                     if (data != false) {
                         ReturnStr = data;
                     }
                 }
             });
             return ReturnStr;
         }
         function GetUserOrgNOs() {
             var UserID = getClientInfo("UserID");
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sInvoiceVoidApply.ERPInvoiceVoidApplyMaster',
                 data: "mode=method&method=" + "GetUserOrgNOs" + "&parameters=" + UserID,
                 cache: false,
                 async: false,
                 success: function (data) {
                     var rows = $.parseJSON(data);
                     if (rows.length > 0) {
                         $("#dataFormMasterApplyOrg_NO").combobox('setValue', rows[0].OrgNO);
                         $("#dataFormMasterOrg_NOParent").val(rows[0].OrgNOParent);
                     }
                 }
             }
             );
         }
         function GetInvoiceVoidNO() {
             var UserID = getClientInfo("UserID");
             var ReturnStr = "";
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sInvoiceVoidApply.ERPInvoiceVoidApplyMaster',
                 data: "mode=method&method=" + "GetInvoiceVoidNO" + "&parameters=" +  UserID,
                 cache: false,
                 async: false,
                 success: function (data) {
                     if (data != false) {
                         ReturnStr = data;
                     }
                 }
             });
             return ReturnStr;
         }
         function InVoiceNOOnselect(rowdata) {
             $("#dataFormMasterCustNO").val(rowdata.CUNO);
             $("#dataFormMasterCustShortName").val(rowdata.Cust_SH);
             $("#dataFormMasterInvoiceAmt").val(rowdata.DLV_L_AMT);
             $("#dataFormMasterInvoiceTax").val(rowdata.DLV_L_TAX);
             $("#dataFormMasterInvoiceTotal").val(rowdata.DLV_L_REM);
             InvoiceVoidNO = $("#dataFormMasterInvoiceVoidNO").val();
             InvoiceNO = $("#dataFormMasterInvoiceNO").combobox('getValue');
             GetInvoiceVoidApplyDetails(InvoiceVoidNO, InvoiceNO);
         }
         function IsInvoiceNOExist(InvoiceNO) { //檢查發票號碼是否存在
             var cnt;
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sInvoiceVoidApply.ERPInvoiceVoidApplyMaster',
                 data: "mode=method&method=" + "IsInvoiceNOExist" + "&parameters=" + InvoiceNO,
                 cache: false,
                 async: false,
                 success: function (data) {
                     if (data != false) {
                         cnt = $.parseJSON(data);
                     }
                 }
             });
             if ((cnt == "0") || (cnt == "undefined")) {

                 return false;
             }
             else {
                 return true;
             }
         }
         function CloseDataForm() {
             self.parent.closeCurrentTab();
             return false;
         }
         function CheckApplyEmpIsGroupID(GroupID) {
             var ApplyEmpID = $("#dataFormMasterApplyEmpID").combobox('getValue');
             var cnt;
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sInvoiceVoidApply.ERPInvoiceVoidApplyMaster',
                 data: "mode=method&method=" + "CheckApplyEmpIsGroupID" + "&parameters=" + ApplyEmpID + "," + GroupID,
                 cache: false,
                 async: false,
                 success: function (data) {
                     cnt = data;
                 }
             });
             if ((cnt == "Y")) {
                 return true;
             }
             else {
                 return false;
             }
         }
     </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sInvoiceVoidApply.ERPInvoiceVoidApplyMaster" runat="server" AutoApply="True"
                DataMember="ERPInvoiceVoidApplyMaster" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="InvoiceVoidNO" Editor="text" FieldName="InvoiceVoidNO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyDate" Editor="datebox" FieldName="ApplyDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyEmpID" Editor="text" FieldName="ApplyEmpID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyOrg_NO" Editor="text" FieldName="ApplyOrg_NO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Org_NOParent" Editor="text" FieldName="Org_NOParent" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CustNO" Editor="text" FieldName="CustNO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CustShortName" Editor="text" FieldName="CustShortName" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="InvoiceNO" Editor="text" FieldName="InvoiceNO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="InvoiceAmt" Editor="numberbox" FieldName="InvoiceAmt" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="InvoiceTax" Editor="numberbox" FieldName="InvoiceTax" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="VoidNotes" Editor="text" FieldName="VoidNotes" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="FlowFlag" Editor="text" FieldName="Flowflag" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" />
                </Columns>
                <TooItems>
                <%--    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />--%>
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="發票註銷申請" DialogLeft="10px" DialogTop="10px" Width="660px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="ERPInvoiceVoidApplyMaster" HorizontalColumnsCount="4" RemoteName="sInvoiceVoidApply.ERPInvoiceVoidApplyMaster" IsShowFlowIcon="True" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="True" IsAutoPause="False" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" OnCancel="CloseDataForm" ShowApplyButton="False" ValidateStyle="Hint" OnLoadSuccess="dataFormMasterOnLoadSucess" Width="600px" OnApply="dataFormMasterOnApply" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="註銷單號" Editor="text" FieldName="InvoiceVoidNO" Format="" maxlength="0" Width="95" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請人" Editor="infocombobox" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sInvoiceVoidApply.Employee',tableName:'Employee',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ApplyEmpID" Format="" maxlength="0" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ApplyDate" Format="" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請部門" Editor="infocombobox" FieldName="ApplyOrg_NO" Format="" maxlength="0" Width="80" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sInvoiceVoidApply.Organization',tableName:'Organization',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="發票號碼" Editor="infocombobox" EditorOptions="valueField:'DLVNO',textField:'DLVNO',remoteName:'sInvoiceVoidApply.InvoiceLists',tableName:'InvoiceLists',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:InVoiceNOOnselect,panelHeight:200" FieldName="InvoiceNO" Format="" maxlength="0" Span="4" Width="102" />
                        <JQTools:JQFormColumn Alignment="left" Caption="註銷事由" Editor="textarea" EditorOptions="height:30" FieldName="VoidNotes" Format="" maxlength="0" Span="4" Width="520" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustNO" Format="" maxlength="0" ReadOnly="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶簡稱" Editor="text" FieldName="CustShortName" Format="" maxlength="0" ReadOnly="True" Span="3" Width="375" />
                        <JQTools:JQFormColumn Alignment="left" Caption="發票金額" Editor="numberbox" FieldName="InvoiceAmt" Format="" ReadOnly="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="發票稅額" Editor="numberbox" FieldName="InvoiceTax" Format="" ReadOnly="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="發票總額" Editor="numberbox" FieldName="InvoiceTotal" maxlength="0" ReadOnly="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Org_NOParent" Editor="text" FieldName="Org_NOParent" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="FlowFlag" Editor="text" FieldName="Flowflag" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>

                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="False" DataMember="ERPInvoiceVoidApplyDetails" EditDialogID="JQDialog2" Pagination="False" ParentObjectID="" RemoteName="sInvoiceVoidApply.ERPInvoiceVoidApplyDetails" Title="明細資料" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" >

                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="發票號碼" Editor="text" FieldName="InvoiceNO" Format="" Width="120" />
                        <JQTools:JQGridColumn Alignment="center" Caption="項次" Editor="text" FieldName="ItemNO" Format="" Width="30" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="銷貨日期" Editor="text" FieldName="InvoiceDate" Format="" Width="80" />
                        <JQTools:JQGridColumn Alignment="right" Caption="發票金額" Editor="numberbox" FieldName="InvoiceAmt" Format="" Width="120" />
                        <JQTools:JQGridColumn Alignment="right" Caption="發票稅額" Editor="numberbox" FieldName="InvoiceTax" Format="" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="InvoiceVoidNO" Editor="text" FieldName="InvoiceVoidNO" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Width="120" Visible="False" />
                    </Columns>
                 <%--   <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="InvoiceVoidNO" ParentFieldName="InvoiceVoidNO" />
                    </RelationColumns>--%>
                </JQTools:JQDataGrid>
                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" ParentObjectID="" DataMember="ERPInvoiceVoidApplyDetails" HorizontalColumnsCount="2" RemoteName="sInvoiceVoidApply.ERPInvoiceVoidApplyDetails" >
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="InvoiceVoidNO" Editor="text" FieldName="InvoiceVoidNO" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="InvoiceNO" Editor="text" FieldName="InvoiceNO" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="ItemNO" Editor="text" FieldName="ItemNO" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="InvoiceAmt" Editor="numberbox" FieldName="InvoiceAmt" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="InvoiceTax" Editor="numberbox" FieldName="InvoiceTax" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" />
                        </Columns>
                     <%--   <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="InvoiceVoidNO" ParentFieldName="InvoiceVoidNO" />
                        </RelationColumns>--%>
                    </JQTools:JQDataForm>
                </JQTools:JQDialog>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="ApplyEmpID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="ApplyDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="" FieldName="InvoiceVoidNO" RemoteMethod="False" DefaultMethod="GetInvoiceVoidNO" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="VoidNotes" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormDetail" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
