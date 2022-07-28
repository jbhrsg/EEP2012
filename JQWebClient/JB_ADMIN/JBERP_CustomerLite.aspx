<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_CustomerLite.aspx.cs" Inherits="Template_JQuerySingle1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
     <script type="text/javascript">
         $(document).ready(function () {
             $(function () {
                 $("input, select, textarea").focus(function () {
                     $(this).css("background-color", "yellow");
                 });
                 $("input, select, textarea").blur(function () {
                     $(this).css("background-color", "white");
                 });
             });


         });

         function queryGrid(dg) {//查詢後添加固定條件 
             if ($(dg).attr('id') == 'dataGridView') {
                 var result = [];
                 var CustName = $("#CustName_Query").val();//客戶名稱               
                 if (CustName != '') result.push("CustName like N'%" + CustName+"%'");

                 var CustNO = $("#CustNO_Query").val();//客戶代號
                 if (CustNO != '') result.push("CustNO like '%" + CustNO + "%'");

                 //var ValidateCount = $("#ValidateCount_Query").checkbox('getValue');//完成?
                 //if (ValidateCount == false) {
                 //    result.push("ValidateCount!=8");
                 //} else result.push("ValidateCount=8");

                 $(dg).datagrid('setWhere', result.join(' and '));
             }
         }

         function CheckCustNO() {
             var CustNO = $("#dataFormMasterCustNO").val();
             if (getEditMode($("#dataFormMaster")) == 'inserted') {
                 var cnt;
                 $.ajax({
                     type: "POST",
                     url: '../handler/jqDataHandle.ashx?RemoteName=sERPCustomerLite.ERPCustomers', 
                     data: "mode=method&method=" + "CheckCustNO" + "&parameters=" + CustNO, 
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
                     alert('注意!!此客戶代號已存在');
                     $('#dataFormMasterCustNO').val("");
                     $('#dataFormMasterCustNO').focus();
                     return false;
                 }
             }
             else return true;
         }
         function OnBlurCustNO() {
             CheckCustNO();
             if ($("#dataFormMasterCustTelNO").val() == '') {
                 $("#dataFormMasterCustTelNO").val($("#dataFormMasterCustNO").val());
             }
         }
         function OnBlurCustName() {
             if ($("#dataFormMasterCustShortName").val() == '') {
                 var CustName = $("#dataFormMasterCustName").val().toString().substr(0, 4);
                 $("#dataFormMasterCustShortName").val(CustName);
             }
         }

         function OnAppliedDF() {                                    
             UpdateERPCustomerToDoNotes();
             $('#dataGridView').datagrid("reload");
         }

         //複訪紀錄維護
         function UpdateERPCustomerToDoNotes() {
             // var row = $('#dataGridView').datagrid('getSelected');
             var CustNO = $("#dataFormMasterCustNO").val();
             var NextCallDate = $('#dataFormMasterNextCallDate').datebox('getValue'); //複訪日期
             var PostSourceID = $('#dataFormMasterPostSourceID').options('getValue'); //同業刊登中
             if (NextCallDate != "") {

                 $.ajax({
                     type: "POST",
                     url: '../handler/jqDataHandle.ashx?RemoteName=sERPCustomerLite.ERPCustomers', //連接的Server端，command
                     data: "mode=method&method=" + "UpdateERPCustomerToDoNotes" + "&parameters=" + encodeURIComponent(CustNO + "," + NextCallDate + "," + PostSourceID), //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                     cache: false,
                     async: false,
                     success: function (data) {
                         alert('修改成功！');
                         $('#dataGridMaster').datagrid("reload");
                         closeForm('#JQDialog1');
                     },
                     error: function (xhr, ajaxOptions, thrownError) {
                         alert(xhr.status);
                         alert(thrownError);
                     }
                 });

             }
         }


    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERPCustomerLite.ERPCustomers" runat="server" AutoApply="True"
                DataMember="ERPCustomers" Pagination="True" QueryTitle="查詢" EditDialogID="JQDialog1"
                Title="網路客戶維護" AllowAdd="True" AllowDelete="False" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="20,40,60" PageSize="20" QueryAutoColumn="False" QueryLeft="20px" QueryMode="Panel" QueryTop="30px" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustNO" Format="" MaxLength="0" Visible="true" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustName" Format="" MaxLength="0" Visible="true" Width="200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶簡稱" Editor="text" FieldName="CustShortName" Format="" MaxLength="0" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="電話號碼" Editor="text" FieldName="CustTelNO" Format="" MaxLength="0" Visible="true" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="地址" Editor="text" FieldName="CustAddr" Format="" MaxLength="0" Visible="true" Width="220" />
                    <JQTools:JQGridColumn Alignment="left" Caption="傳真" Editor="text" FieldName="CustFaxNO" Format="" MaxLength="0" Visible="true" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人" Editor="text" FieldName="ContactA" Format="" MaxLength="0" Visible="true" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="分機" Editor="text" FieldName="ContactASubTel" Format="" MaxLength="0" Visible="true" Width="30" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人郵件地址" Editor="text" FieldName="ContactAMail" Format="" MaxLength="0" Visible="true" Width="160" />
                    <JQTools:JQGridColumn Alignment="left" Caption="業務" Editor="text" FieldName="SalesID" Format="" MaxLength="0" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="業務" Editor="text" FieldName="SalesName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="複訪日" Editor="datebox" FieldName="NextCallDate" Format="" Visible="true" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="提供者" Editor="text" FieldName="PostedMan" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="提供者" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Visible="False" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="提供日期" Editor="datebox" FieldName="PostedDate" Format="yyyy/mm/dd HH:MM:SS" Visible="True" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="提供者" Editor="text" FieldName="LastUpdateBy" Format="" MaxLength="0" Visible="False" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="最近交易日" Editor="datebox" FieldName="LatelyDayD" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                  <%--  <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消"  />--%>
                    <%--<JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />--%>
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶名稱" Condition="%" DataType="string" Editor="text" FieldName="CustName" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶代號" Condition="%" DataType="string" Editor="text" FieldName="CustNO" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="網路客戶維護" Width="600px" DialogTop="20px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="ERPCustomers" HorizontalColumnsCount="1" RemoteName="sERPCustomerLite.ERPCustomers" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApplied="OnAppliedDF" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="240" OnBlur="OnBlurCustNO" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustName" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="240" OnBlur="OnBlurCustName" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶簡稱" Editor="text" FieldName="CustShortName" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="240" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶等級" Editor="infocombobox" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sERPCustomerLite.infoPostType',tableName:'infoPostType',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:150" FieldName="PostType" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="媒體產業" Editor="infocombobox" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sERPSalseDetails.infoIndustryType',tableName:'infoIndustryType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:150" FieldName="IndustryType" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="員工人數" Editor="numberbox" FieldName="iPeopleCount" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電話號碼" Editor="text" FieldName="CustTelNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="240" />
                        <JQTools:JQFormColumn Alignment="left" Caption="傳真" Editor="text" FieldName="CustFaxNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="240" />
                        <JQTools:JQFormColumn Alignment="left" Caption="地址" Editor="text" FieldName="CustAddr" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="240" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人" Editor="text" FieldName="ContactA" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="240" />
                        <JQTools:JQFormColumn Alignment="left" Caption="分機" Editor="text" FieldName="ContactASubTel" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="240" />
                        <JQTools:JQFormColumn Alignment="left" Caption="行動電話" Editor="text" FieldName="ContactATel" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="240" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電子信箱" Editor="text" FieldName="ContactAMail" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="240" />
                        <JQTools:JQFormColumn Alignment="left" Caption="業務人員" Editor="infocombobox" EditorOptions="valueField:'SALESID',textField:'SALESMAN',remoteName:'sERPCustomerLite.Salse',tableName:'Salse',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="SalesID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="人力銀行網址" Editor="text" FieldName="HrBankUrl" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="450" />
                        <JQTools:JQFormColumn Alignment="left" Caption="複訪日期" Editor="datebox" FieldName="NextCallDate" Format="yyyy/mm/dd" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="最近維保內容" Editor="textarea" EditorOptions="height:30" FieldName="Notes" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="450" />
                        <JQTools:JQFormColumn Alignment="left" Caption="同業刊登中" Editor="infooptions" FieldName="PostSourceID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" EditorOptions="title:'',panelWidth:240,remoteName:'sERPCustomerLite.PostSource',tableName:'PostSource',valueField:'ID',textField:'SourceName',columnCount:3,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[]" />
                        <JQTools:JQFormColumn Alignment="left" Caption="最近交易日" Editor="datebox" FieldName="LatelyDayD" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="100" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERNAME" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="ReCallDate" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckMethod="" CheckNull="True" FieldName="CustNO" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
