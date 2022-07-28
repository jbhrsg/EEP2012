<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBJCS4_Customers.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
        <script type="text/javascript">
            $(document).ready(function () {
             
                //雇主名稱+簡稱合併
                var EmployerName = $('#dataFormMasterCustomerName').closest('td');
                var ShortName = $('#dataFormMasterCustomerShortName').closest('td').children();
                EmployerName.append('&nbsp;簡稱').append(ShortName);

                //雇主地址合併
                //var City = $('#dataFormMasterCustomerCity').closest('td');
                //var City2 = $('#dataFormMasterCustomerCity2').closest('td').children();
                //var Address = $('#dataFormMasterCustomerAddress').closest('td').children();
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
                    setTimeout(function () {
                        openForm('#JQDialog1', $('#dataGridView').datagrid('getSelected'), "updated", 'dialog');
                    }, 800);
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
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sJCSCustomer.Customers4" runat="server" AutoApply="True"
                DataMember="Customers4" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="台茂宿舍客戶資料維護" QueryMode="Panel" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="系統代碼" Editor="text" FieldName="CustomerID" Format="" MaxLength="0" Visible="true" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustomerName" Format="" MaxLength="0" Visible="true" Width="170" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶電話" Editor="text" FieldName="CustomerTel" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="負責業務" Editor="text" FieldName="SalesName" Format="" Visible="true" Width="80" />
                    <JQTools:JQGridColumn Alignment="right" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="center" Caption="統一編號" Editor="text" FieldName="CustomerTaxNO" Format="" Visible="true" Width="90" />
                    <JQTools:JQGridColumn Alignment="center" Caption="聯絡人1姓名" Editor="text" FieldName="contact1Name" Format="" MaxLength="0" Visible="true" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人1手機" Editor="text" FieldName="contact1Mobile" Format="" MaxLength="0" Visible="true" Width="110" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶地址" Editor="text" FieldName="CustomerAddress" Format="" MaxLength="0" Visible="true" Width="223" />
                    <JQTools:JQGridColumn Alignment="center" Caption="門禁時間" Editor="text" FieldName="LimitTime" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="有效?" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="67" FormatScript="ckIsActive">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <%--<JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增客戶" />--%>
                   <%-- <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />--%>
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶名稱/客戶編號/統一編號" Condition="%" DataType="string" Editor="text" FieldName="EmployerName" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="台茂客戶維護" DialogLeft="50px" DialogTop="50px" Width="1030px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="Customers4" HorizontalColumnsCount="7" RemoteName="sJCSCustomer.Customers4" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApplied="OnAppliedCus" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Visible="False" Width="180" maxlength="0" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="系統代碼" Editor="text" FieldName="CustomerID" Format="" maxlength="0" Visible="False" Width="80" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustomerName" Format="" Width="170" Span="2" maxlength="0" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="統一編號" Editor="numberbox" FieldName="CustomerTaxNO" Format="" Width="100" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶電話" Editor="text" FieldName="CustomerTel" Format="" maxlength="0" Width="100" ReadOnly="True" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶傳真" Editor="text" FieldName="CustomerFax" maxlength="0" Width="100" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶地址" Editor="text" FieldName="CustomerAddress" Format="" maxlength="0" Width="460" Span="3" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="負責業務" Editor="infocombobox" FieldName="SalesID" Format="" Width="105" EditorOptions="valueField:'ID',textField:'Name',remoteName:'sJCSCustomer.infoSalesId4',tableName:'infoSalesId4',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="門禁時間" Editor="infocombobox" EditorOptions="valueField:'stime',textField:'stime',remoteName:'sJCSCustomer.infoLimitTime',tableName:'infoLimitTime',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="LimitTime" NewRow="False" Visible="True" Width="115" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ZIPCode" Editor="text" FieldName="ZIPCode" Format="" maxlength="0" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人1姓名" Editor="text" FieldName="contact1Name" Format="" maxlength="0" NewRow="True" Width="80" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="職稱" Editor="text" FieldName="contact1Title" Format="" maxlength="0" Width="95" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="手機" Editor="text" FieldName="contact1Mobile" Format="" maxlength="0" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="分機" Editor="text" FieldName="contact1TelExt" Format="" maxlength="0" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="email" Editor="text" FieldName="Email" Format="" maxlength="0" Span="2" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人2姓名" Editor="text" FieldName="contact2Name" Format="" maxlength="0" NewRow="True" Width="80" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="職稱" Editor="text" FieldName="contact2Title" Format="" maxlength="0" Width="95" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="手機" Editor="text" FieldName="contact2Mobile" Format="" maxlength="0" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="分機" Editor="text" FieldName="contact2TelExt" Format="" maxlength="0" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="email" Editor="text" FieldName="Email1" Format="" maxlength="0" Span="2" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人3姓名" Editor="text" FieldName="Contact3Name" Format="" maxlength="0" NewRow="True" Width="80" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="職稱" Editor="text" FieldName="Contact3Title" Format="" maxlength="0" Width="95" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="手機" Editor="text" FieldName="Contact3Mobile" Format="" maxlength="0" Width="100" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="分機" Editor="text" FieldName="Contact3TelExt" Format="" maxlength="0" Width="90" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="email" Editor="text" FieldName="Email2" Format="" maxlength="0" Span="2" Width="180" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電費單價" Editor="numberbox" FieldName="iPowerQty" MaxLength="0" Visible="True" Width="80" NewRow="True" EditorOptions="precision:1,groupSeparator:'',prefix:''" />
                        <JQTools:JQFormColumn Alignment="left" Caption="收款結帳日" Editor="numberbox" FieldName="BalanceDate" NewRow="False" Span="1" Width="95" />
                        <JQTools:JQFormColumn Alignment="left" Caption="帳款天數" Editor="numberbox" FieldName="DebtorDays" MaxLength="0" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="是否有效" Editor="checkbox" FieldName="IsActive" Format="" maxlength="0" Width="30" EditorOptions="on:1,off:0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備註內容" Editor="textarea" FieldName="Notes" Format="" Width="860" Span="6" EditorOptions="height:60" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ERPCustomerID" Editor="text" FieldName="ERPCustomerID" Format="" Width="80" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Width="80" NewRow="True" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" maxlength="0" Width="80" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastupdateBy" Format="" Width="80" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" maxlength="0" Width="80" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="KEYWord" Editor="text" FieldName="KEYWord" Format="" Width="80" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="雇主地址" Editor="infocombobox" EditorOptions="valueField:'Country',textField:'Country',remoteName:'sFwcrmCustomer.CountryCity',tableName:'CountryCity',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="CustomerCity" Format="" maxlength="0" Width="80" Span="3" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" FieldName="CustomerCity2" Format="" maxlength="0" Width="80" EditorOptions="valueField:'City',textField:'City',remoteName:'sFwcrmCustomer.CountryCity',tableName:'CountryCity',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="CustomerShortName" Format="" maxlength="0" Width="95" ReadOnly="True" />

                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CustomerName" RemoteMethod="True" ValidateMessage="客戶名稱不可空白！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CustomerShortName" RemoteMethod="True" ValidateMessage="客戶簡稱不可空白！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CustomerAddress" RemoteMethod="True" ValidateMessage="客戶地址不可空白！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="SalesID" RemoteMethod="True" ValidateMessage="請選擇負責業務！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="contact1Name" RemoteMethod="True" ValidateMessage="聯絡人1姓名不可空白！" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
