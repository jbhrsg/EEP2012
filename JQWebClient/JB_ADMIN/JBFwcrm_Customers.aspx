<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBFwcrm_Customers.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
        <script type="text/javascript">
            $(document).ready(function () {
             
                //雇主名稱+簡稱合併
                var EmployerName = $('#dataFormMasterEmployerName').closest('td');
                var ShortName = $('#dataFormMasterEmployerShortName').closest('td').children();
                EmployerName.append('&nbsp;簡稱').append(ShortName);

                //雇主地址合併
                //var City = $('#dataFormMasterCustomerCity').closest('td');
                //var City2 = $('#dataFormMasterCustomerCity2').closest('td').children();
                //var Address = $('#dataFormMasterEmployerAddress').closest('td').children();
                //City.append('&nbsp;').append(City2).append('&nbsp;').append(Address);

                //分開收費註解
                var FirstFeeType = $('#dataFormMasterFirstFeeType').closest('td');                
                FirstFeeType.append('(不打勾,指16號(含)以後外勞的住宿費用累計下個月一起收)');
                $("#dataFormMasterFirstFeeType").closest('td').css("color", "blue");

                //加上(顏色)的欄位
                var HideFieldName = ['EmployerFee', 'EmployerFee2'];
                var FormName = '#dataFormMaster';

                $.each(HideFieldName, function (index, fieldName) {
                    var Name = $(FormName + fieldName);                   
                    $(FormName + fieldName).closest('td').prev('td').css("color", "rgb(138, 43, 226)");
                });

                //--------------客戶職缺傳入客戶代號 => 查詢客戶---------------------------------------------------
                var parameter = Request.getQueryStringByName("CustID");
                if (parameter != "") {
                   
                    $("#dataGridView").datagrid('setWhere', "ERPCustomerID = '" + parameter + "'");
                    //setTimeout(function () {
                    //    openForm('#JQDialog1', $('#dataGridView').datagrid('getSelected'), "updated", 'dialog');
                    //}, 800);
                }

            });        
       
            function ckIsActive(val) {
                if (val != "0")
                    return "<input  type='checkbox' checked='true' onclick='return false;'/>";
                else
                    return "<input  type='checkbox' onclick='return false;'/>";
            }

            function queryGrid(dg) {//查詢後添加固定條件
                if ($(dg).attr('id') == 'dataGridView') {
                    //查詢條件
                    var result = [];
                    var EmployerName = $('#EmployerName_Query').val();//客戶代號
                   
                    if (EmployerName != '') result.push("EmployerName like '%" + EmployerName + "%' or EmployerID like '%" + EmployerName + "%' or EmployerPID like '%" + EmployerName + "%'");

                    $(dg).datagrid('setWhere', result.join(' and '));
                }
            }

            function OnAppliedCus() {
                $('#dataGridView').datagrid("reload");
            }

        </script>




</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sFwcrmCustomer.Employer" runat="server" AutoApply="True"
                DataMember="Employer" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="傑報外勞雇主維護" QueryMode="Panel" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="系統代碼" Editor="text" FieldName="EmployerID" Format="" MaxLength="0" Visible="true" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="雇主名稱" Editor="text" FieldName="EmployerName" Format="" MaxLength="0" Visible="true" Width="160" />
                    <JQTools:JQGridColumn Alignment="center" Caption="負責業務" Editor="text" FieldName="SalesName" Format="" Visible="true" Width="80" />
                    <JQTools:JQGridColumn Alignment="right" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="center" Caption="雇主類型" Editor="text" FieldName="EmployerTypeName" Format="" Visible="true" Width="80" />
                    <JQTools:JQGridColumn Alignment="center" Caption="統一編號" Editor="text" FieldName="EmployerPID" Format="" MaxLength="0" Visible="true" Width="90" />
                    <JQTools:JQGridColumn Alignment="center" Caption="雇主編號" Editor="text" FieldName="EmployerNo" Format="" MaxLength="0" Visible="true" Width="90" />
                    <JQTools:JQGridColumn Alignment="center" Caption="聯絡人1姓名" Editor="text" FieldName="ContactName" Format="" MaxLength="0" Visible="true" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人1電話" Editor="text" FieldName="ContactTel" Format="" MaxLength="0" Visible="true" Width="110" />
                    <JQTools:JQGridColumn Alignment="left" Caption="雇主地址" Editor="text" FieldName="EmployerAddress" Format="" MaxLength="0" Visible="true" Width="223" />
                    <JQTools:JQGridColumn Alignment="center" Caption="有效?" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="67" FormatScript="ckIsActive">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                   <%-- <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增雇主" />--%>
                   <%-- <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />--%>
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="雇主名稱/雇主編號/統一編號" Condition="%" DataType="string" Editor="text" FieldName="EmployerName" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="外勞雇主維護" DialogLeft="10px" DialogTop="10px" Width="1030px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="Employer" HorizontalColumnsCount="7" RemoteName="sFwcrmCustomer.Employer" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApplied="OnAppliedCus" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="EmployerID" Editor="text" FieldName="EmployerID" Format="" maxlength="0" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="雇主分類" Editor="infocombobox" EditorOptions="items:[{value:'1',text:'個人',selected:'false'},{value:'2',text:'公司',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="EmployerType" Format="" Width="80" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="雇主名稱" Editor="text" FieldName="EmployerName" Format="" maxlength="0" Width="170" Span="2" ReadOnly="True" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="統一編號" Editor="text" FieldName="EmployerPID" Format="" maxlength="0" Width="100" ReadOnly="True" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="雇主編號" Editor="text" FieldName="EmployerNo" Format="" maxlength="0" Width="100" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="雇主地址" Editor="text" FieldName="EmployerAddress" Format="" maxlength="0" ReadOnly="True" Span="3" Width="430" />
                        <JQTools:JQFormColumn Alignment="left" Caption="負責業務" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Name',remoteName:'sFwcrmCustomer.infoSalesId',tableName:'infoSalesId',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesID" Format="" maxlength="0" Width="105" Span="1" ReadOnly="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ZIPCode" Editor="text" FieldName="ZIPCode" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司傳真" Editor="text" FieldName="ContactFax" Format="" maxlength="0" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人1姓名" Editor="text" FieldName="ContactName" Format="" maxlength="0" NewRow="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電話" Editor="text" FieldName="ContactTel" Format="" maxlength="0" Width="115" />
                        <JQTools:JQFormColumn Alignment="left" Caption="職稱" Editor="text" FieldName="ContactTitle" Format="" maxlength="0" Width="95" />
                        <JQTools:JQFormColumn Alignment="left" Caption="手機" Editor="text" FieldName="ContactMobile" Format="" maxlength="0" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="email" Editor="text" FieldName="ContactEmail" Format="" maxlength="0" Width="180" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人2姓名" Editor="text" FieldName="ContactName1" Format="" maxlength="0" NewRow="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電話" Editor="text" FieldName="ContactTel1" Format="" maxlength="0" Width="115" />
                        <JQTools:JQFormColumn Alignment="left" Caption="職稱" Editor="text" FieldName="ContactTitle1" Format="" maxlength="0" Width="95" />
                        <JQTools:JQFormColumn Alignment="left" Caption="手機" Editor="text" FieldName="ContactMobile1" Format="" maxlength="0" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="email" Editor="text" FieldName="ContactEmail1" Format="" maxlength="0" Width="180" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人3姓名" Editor="text" FieldName="ContactName2" Format="" maxlength="0" NewRow="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電話" Editor="text" FieldName="ContactTel2" Format="" maxlength="0" Width="115" />
                        <JQTools:JQFormColumn Alignment="left" Caption="職稱" Editor="text" FieldName="ContactTitle2" Format="" maxlength="0" Width="95" />
                        <JQTools:JQFormColumn Alignment="left" Caption="手機" Editor="text" FieldName="ContactMobile2" Format="" maxlength="0" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="email" Editor="text" FieldName="ContactEmail2" Format="" maxlength="0" Width="180" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="雇主支付" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsInvoiceER" Format="" Width="60" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="匯款銀行" Editor="text" FieldName="DormFeeBankName" Format="" maxlength="0" Width="115" />
                        <JQTools:JQFormColumn Alignment="left" Caption="匯款代號" Editor="text" FieldName="DormFeeBankID" Format="" maxlength="0" Width="95" />
                        <JQTools:JQFormColumn Alignment="left" Caption="匯款戶名" Editor="text" FieldName="DormFeeAccountName" Format="" maxlength="0" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="匯款帳號" Editor="text" FieldName="DormFeeBankNo" Format="" maxlength="0" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請款總表" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="bSumFormER" Format="" NewRow="True" Width="60" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請款公司" Editor="text" FieldName="DormFeeCustomerName" Format="" maxlength="0" Width="115" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請款人" Editor="text" FieldName="DormFeeContName" Format="" maxlength="0" Width="95" />
                        <JQTools:JQFormColumn Alignment="left" Caption="預計發薪日" Editor="infocombobox" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sFwcrmCustomer.infoSalaryDate',tableName:'infoSalaryDate',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="SalaryDate" Format="" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="連絡資訊" Editor="text" FieldName="DormFeeContData" Format="" maxlength="0" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="分開收費" Editor="checkbox" FieldName="FirstFeeType" Format="" Width="60" EditorOptions="on:1,off:0" NewRow="True" Span="3" />
                        <JQTools:JQFormColumn Alignment="left" Caption="收款結帳日" Editor="numberbox" FieldName="BalanceDate" Format="" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="帳款天數" Editor="numberbox" FieldName="DebtorDays" Format="" Width="75" />
                        <JQTools:JQFormColumn Alignment="left" Caption="是否有效" Editor="checkbox" FieldName="IsActive" Format="" maxlength="0" Width="30" EditorOptions="on:1,off:0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備註內容" Editor="textarea" FieldName="Description" Format="" Width="850" Span="6" EditorOptions="height:40" />
                        <JQTools:JQFormColumn Alignment="left" Caption="DormFeeSum" Editor="text" FieldName="DormFeeSum" Format="" maxlength="0" Span="1" Width="80" NewRow="True" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ERPCustomerID" Editor="text" FieldName="ERPCustomerID" Format="" Width="80" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Width="80" NewRow="True" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" maxlength="0" Width="80" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" Width="80" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" maxlength="0" Width="80" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="KEYWord" Editor="text" FieldName="KEYWord" Format="" Width="80" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="雇主地址" Editor="infocombobox" EditorOptions="valueField:'Country',textField:'Country',remoteName:'sFwcrmCustomer.CountryCity',tableName:'CountryCity',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="CustomerCity" Format="" maxlength="0" Width="80" ReadOnly="False" Visible="False" Span="3" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" FieldName="CustomerCity2" Format="" maxlength="0" Width="80" Span="1" ReadOnly="True" Visible="False" EditorOptions="valueField:'City',textField:'City',remoteName:'sFwcrmCustomer.CountryCity',tableName:'CountryCity',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="EmployerShortName" Format="" maxlength="0" Width="95" ReadOnly="True" Visible="True" />

                        <JQTools:JQFormColumn Alignment="left" Caption="雇主負擔" Editor="infooptions" EditorOptions="title:'請選擇',panelWidth:350,remoteName:'sFwcrmCustomer.infoFee',tableName:'infoFee',valueField:'ID',textField:'Name',columnCount:3,multiSelect:true,openDialog:false,selectAll:false,selectOnly:false,items:[]" FieldName="EmployerFee" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="280" />
                        <JQTools:JQFormColumn Alignment="left" Caption="雇主代扣" Editor="infooptions" EditorOptions="title:'請選擇',panelWidth:350,remoteName:'sFwcrmCustomer.infoFee',tableName:'infoFee',valueField:'ID',textField:'Name',columnCount:3,multiSelect:true,openDialog:false,selectAll:false,selectOnly:false,items:[]" FieldName="EmployerFee2" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="280" />

                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="EmployerType" RemoteMethod="True" ValidateMessage="請選擇分類！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="EmployerName" RemoteMethod="True" ValidateMessage="雇主名稱不可空白！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="EmployerShortName" RemoteMethod="True" ValidateMessage="雇主簡稱不可空白！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="EmployerAddress" RemoteMethod="True" ValidateMessage="雇主地址不可空白！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="SalesID" RemoteMethod="True" ValidateMessage="請選擇負責業務！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ContactName" RemoteMethod="True" ValidateMessage="聯絡人1姓名不可空白！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ContactTel" RemoteMethod="True" ValidateMessage="聯絡人1電話不可空白！" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
