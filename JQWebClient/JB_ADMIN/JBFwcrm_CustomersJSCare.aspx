<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBFwcrm_CustomersJSCare.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
        <script type="text/javascript">

            var UserID = getClientInfo("UserID");

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
               
                //--------------聯繫紀錄權限控管---------------------------------------------------
                var ViewContactUsers = $('#DFContactRecordIsShade').closest('td');
                ViewContactUsers.append($('<a>', { href: 'javascript:void(0)' }).on('click', function () {
                    var ContactDescr = $("#DFContactRecordContactDescr").val();
                    if (ContactDescr == "" || ContactDescr == undefined) {
                        alert('注意!!,請先輸入聯絡內容,再設定分享');
                        $("#DFContactRecordContactDescr").focus();
                        return false;
                    }
                    var IsShade = $("#DFContactRecordIsShade").checkbox('getValue');
                    if (IsShade == 0) {
                        alert('注意!!,要設訂分享聯絡內容時,需先選取遮蔽紀錄');
                        $("#DFContactRecordIsShade").checkbox('setValue', 1);
                        return false;
                    }
                    var SalesKindID = $("#DFContactRecordSalesKindID").val();
                    var FiltStr = "SalesKindID = '" + SalesKindID + "'";
                    $("#JQDataGrid1").datagrid('setWhere', FiltStr);
                    openForm('#JQDialogClu', {}, "", 'dialog');
                    return true;
                }).linkbutton({ text: '分享給' }));

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

                    var SDate = $('#SDate_Query').datebox('getValue');//開始日期
                    var EDate = $('#EDate_Query').datebox('getValue');//結束日期                           
                    if (SDate != '') result.push("NextContactDate >= '" + SDate + "'");
                    if (EDate != '') result.push("NextContactDate <= '" + EDate + "'");

                    $(dg).datagrid('setWhere', result.join(' and '));
                }
            }
            function OnAppliedCus() {
                $('#dataGridView').datagrid("reload");
            }

            //------------------------------------//客戶聯繫紀錄---------------------------------------------------------    
            function ContactLogsLink(value, row, index) {
                //if (value != null) {
                    return "<a href='javascript: void(0)' onclick='LinkContactLogs(" + index + ");' style='color:red;'>" + value + "</a>";
                //}
                //else return "<a href='javascript: void(0)' onclick='LinkContactDate(" + index + ");' style='color:red;'>新增</a>";;
            }
            // open客戶聯繫紀錄 dialog
            function LinkContactLogs(index) {
                $("#dataGridView").datagrid('selectRow', index);
                openForm('#Dialog_ContactLogs', $('#dataGridView').datagrid('getSelected'), "updated", 'dialog');
                var rows = $("#dataGridView").datagrid('getSelected');
                var ERPCustomerID = rows.ERPCustomerID;
                $("#DGContactRecord").datagrid('setWhere', "CustomerID='" + ERPCustomerID + "'");

            }
            //--------------聯繫紀錄權限控管---------------------------------------------------
            function JQDataGrid1OnLoadSuccess() {
                var ShareTo = $("#DFContactRecordShareTo").val();
                if (ShareTo.length > 0) {
                    var rows = $("#JQDataGrid1").datagrid("getRows");
                    for (var k = 0; k < rows.length; k++) {
                        if (ShareTo.indexOf(rows[k].SalesID) != -1) {
                            $('#JQDataGrid1').datagrid("checkRow", k);
                        }
                    }
                }
            }
            function JQDialogCluOnSubmited() {
                var rows = $('#JQDataGrid1').datagrid("getChecked");
                var count = rows.length;
                if (count == 0) {
                    alert('注意!!未選取任何業務人員,請選取');
                    return false;
                }
                var ShareTo = '';
                var ShareToName = '';
                for (var i = 0; i <= rows.length - 1; i++) {
                    if (i > 0) {
                        ShareTo = ShareTo + ',' + rows[i].SalesID;
                        ShareToName = ShareToName + ',' + rows[i].SalesName;
                    }
                    else {
                        ShareTo = ShareTo + rows[0].SalesID;
                        ShareToName = ShareToName + ',' + rows[i].SalesName;
                    }
                }
                $("#DFContactRecordShareTo").val(ShareTo);
                $("#DFContactRecordShareToName").val(ShareToName);
                return true;
            }
            //在JQDataGridContact中,顯示ContactDescr文字內容
            function ShowContactJDGC(value, row) {
                var str = '';
                var ShareTo = '';
                if (row.ShareTo != null) {
                    var ShareTo = row.ShareTo;
                }
                var slen = (value).trim().length;
                if (slen > 0) {
                    if (row.IsShade == true && ((row.CreateBy != UserID) && (ShareTo.indexOf(UserID) == -1))) {
                        str = ".........聯絡內容遮蔽...........";
                    }
                    else {
                        str = value;
                    }
                }
                return str;
                //return "<p style='margin:0px;word-wrap:break-word;white-space: normal'>" + str + "</p>";
            }
            function sCheckBox(val) {
                if (val)
                    return "<input  type='checkbox' checked='true' checked onclick='return false;' />";
                else
                    return "<input  type='checkbox' onclick='return false;' />";
            }
            function GetCustomerID() {
                var rows = $("#dataGridView").datagrid('getSelected');
                return rows.ERPCustomerID;
            }
            //聯繫維護紀錄有變更時重整
            function OnDeletedContactRecord() {
                $("#DGContactRecord").datagrid('reload');
                $("#dataGridView").datagrid('reload');

            }
            function OnAppliedContactRecord() {
                $("#DGContactRecord").datagrid('reload');
                $("#dataGridView").datagrid('reload');
            }
            


        </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sFwcrmCustomer.EmployerJSCare" runat="server" AutoApply="True"
                DataMember="EmployerJSCare" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="傑信家服外勞雇主維護" QueryMode="Panel" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" >
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="系統代碼" Editor="text" FieldName="EmployerID" Format="" MaxLength="0" Visible="true" Width="55" />
                    <JQTools:JQGridColumn Alignment="left" Caption="雇主名稱" Editor="text" FieldName="EmployerName" Format="" MaxLength="0" Visible="true" Width="135" />
                    <JQTools:JQGridColumn Alignment="center" Caption="聯繫紀錄" Editor="text" FieldName="ContactDate" FormatScript="ContactLogsLink" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="72">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="預計聯繫日" Editor="text" FieldName="NextContactDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="負責業務" Editor="text" FieldName="SalesName" Format="" Visible="true" Width="70" />
                    <JQTools:JQGridColumn Alignment="right" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="center" Caption="雇主類型" Editor="text" FieldName="EmployerTypeName" Format="" Visible="true" Width="60" />
                    <JQTools:JQGridColumn Alignment="center" Caption="統一編號" Editor="text" FieldName="EmployerPID" Format="" MaxLength="0" Visible="true" Width="70" />
                    <JQTools:JQGridColumn Alignment="center" Caption="雇主編號" Editor="text" FieldName="EmployerNo" Format="" MaxLength="0" Visible="true" Width="100" />
                    <JQTools:JQGridColumn Alignment="center" Caption="聯絡人1姓名" Editor="text" FieldName="ContactName" Format="" MaxLength="0" Visible="true" Width="75" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人1電話" Editor="text" FieldName="ContactTel" Format="" MaxLength="0" Visible="true" Width="85" />
                    <JQTools:JQGridColumn Alignment="left" Caption="雇主地址" Editor="text" FieldName="EmployerAddress" Format="" MaxLength="0" Visible="true" Width="216" />
                    <JQTools:JQGridColumn Alignment="center" Caption="有效?" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="47" FormatScript="ckIsActive">
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
                    <JQTools:JQQueryColumn AndOr="and" Caption="預計聯繫日期" Condition="%" DataType="string" Editor="datebox" FieldName="SDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="〜" Condition="%" DataType="string" Editor="datebox" FieldName="EDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="外勞雇主維護" DialogLeft="10px" DialogTop="10px" Width="1050px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="EmployerJSCare" HorizontalColumnsCount="7" RemoteName="sFwcrmCustomer.EmployerJSCare" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApplied="OnAppliedCus" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="EmployerID" Editor="text" FieldName="EmployerID" Format="" maxlength="0" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="雇主分類" Editor="infocombobox" EditorOptions="items:[{value:'1',text:'個人',selected:'false'},{value:'2',text:'公司',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="EmployerType" Format="" Width="80" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="雇主名稱" Editor="text" FieldName="EmployerName" Format="" maxlength="0" Width="170" Span="2" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="統一編號" Editor="text" FieldName="EmployerPID" Format="" maxlength="0" Width="100" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="雇主編號" Editor="text" FieldName="EmployerNo" Format="" maxlength="0" Width="130" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="雇主地址" Editor="text" FieldName="EmployerAddress" Format="" maxlength="0" Width="430" Span="3" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="負責業務" Editor="infocombobox" FieldName="SalesID" Format="" Width="105" EditorOptions="valueField:'ID',textField:'Name',remoteName:'sFwcrmCustomer.infoSalesIdJSCare',tableName:'infoSalesIdJSCare',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ZIPCode" Editor="text" FieldName="ZIPCode" Format="" maxlength="0" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司傳真" Editor="text" FieldName="ContactFax" Format="" maxlength="0" Width="130" />
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
                        <JQTools:JQFormColumn Alignment="left" Caption="預計發薪日" Editor="infocombobox" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sFwcrmCustomer.infoSalaryDateJSCare',tableName:'infoSalaryDateJSCare',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="SalaryDate" Format="" Width="80" />
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
                        <JQTools:JQFormColumn Alignment="left" Caption="雇主地址" Editor="infocombobox" EditorOptions="valueField:'Country',textField:'Country',remoteName:'sFwcrmCustomer.CountryCity',tableName:'CountryCity',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="CustomerCity" Format="" maxlength="0" Width="80" ReadOnly="True" Span="3" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="infocombobox" FieldName="CustomerCity2" Format="" maxlength="0" Width="80" Span="1" EditorOptions="valueField:'City',textField:'City',remoteName:'sFwcrmCustomer.CountryCity',tableName:'CountryCity',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200" ReadOnly="True" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="EmployerShortName" Format="" maxlength="0" Width="95" ReadOnly="True" />

                        <JQTools:JQFormColumn Alignment="left" Caption="雇主負擔" Editor="infooptions" EditorOptions="title:'請選擇',panelWidth:435,remoteName:'sFwcrmCustomer.infoFeeJSCare',tableName:'infoFeeJSCare',valueField:'ID',textField:'Name',columnCount:3,multiSelect:true,openDialog:false,selectAll:false,selectOnly:false,items:[]" FieldName="EmployerFee" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="370" />
                        <JQTools:JQFormColumn Alignment="left" Caption="雇主代扣" Editor="infooptions" EditorOptions="title:'請選擇',panelWidth:435,remoteName:'sFwcrmCustomer.infoFeeJSCare',tableName:'infoFeeJSCare',valueField:'ID',textField:'Name',columnCount:3,multiSelect:true,openDialog:false,selectAll:false,selectOnly:false,items:[]" FieldName="EmployerFee2" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="370" />

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
            <JQTools:JQDialog ID="Dialog_ContactLogs" runat="server" BindingObjectID="" Title="聯絡紀錄維護" DialogLeft="10px" DialogTop="10px" Width="1050px" Height="410px" ShowSubmitDiv="False">
                <JQTools:JQDataGrid ID="DGContactRecord" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="HUT_CustomerContactRecord" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialogContactRecord" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sFwcrmCustomer.HUT_CustomerContactRecord" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" OnDeleted="OnDeletedContactRecord" data-options="pagination:true,view:commandview">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="聯繫日期" Editor="datebox" FieldName="ContactDate" Format="yyyy/mm/dd" MaxLength="0" ReadOnly="False" Width="80" Frozen="False" IsNvarChar="False" QueryCondition="" Sortable="True" Visible="True">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="預計聯繫日" Editor="datebox" FieldName="NextContactDate" Format="yyyy/mm/dd" MaxLength="0" ReadOnly="False" Width="80" Frozen="False" IsNvarChar="False" QueryCondition="" Sortable="True" Visible="True">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="聯絡人" Editor="text" FieldName="Dialogue" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" EditorOptions="">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="聯繫內容" Editor="textarea" FieldName="ContactDescr" FormatScript="ShowContactJDGC" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="480">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="更新日期" Editor="text" FieldName="CreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="60" Format="yyyy/mm/dd">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="更新人員" Editor="text" FieldName="CreateByName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="55">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="CustID" Editor="text" FieldName="CustomerID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="UserID" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="SalesKindID" Editor="text" FieldName="SalesKindID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="遮蔽?" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsShade" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50" FormatScript="sCheckBox">
                        </JQTools:JQGridColumn>
                    </Columns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增聯繫紀錄" />
                    </TooItems>
                </JQTools:JQDataGrid>
                <JQTools:JQDialog ID="JQDialogContactRecord" runat="server" BindingObjectID="DFContactRecord" DialogLeft="120px" DialogTop="90px" Title="聯繫紀錄維護" Width="750px">
                    <JQTools:JQDataForm ID="DFContactRecord" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="HUT_CustomerContactRecord" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="3" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnApplied="OnAppliedContactRecord" ParentObjectID="" RemoteName="sFwcrmCustomer.HUT_CustomerContactRecord" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="聯繫日期" Editor="datebox" FieldName="ContactDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                            <JQTools:JQFormColumn Alignment="left" Caption="聯絡人" Editor="text" EditorOptions="" FieldName="Dialogue" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="140" />
                            <JQTools:JQFormColumn Alignment="left" Caption="預計聯繫日" Editor="datebox" FieldName="NextContactDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="95" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CustID" Editor="text" FieldName="CustomerID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="聯繫內容" Editor="textarea" EditorOptions="height:90" FieldName="ContactDescr" MaxLength="256" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="620" />
                            <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="CreateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="UpdateBy" Editor="text" FieldName="CreateByName" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="UpdateDate" Editor="text" FieldName="CreateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="center" Caption="遮蔽?" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsShade" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" />
                            <JQTools:JQFormColumn Alignment="left" Caption="檢視者" Editor="text" FieldName="ShareToName" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="2" Visible="True" Width="300" />
                            <JQTools:JQFormColumn Alignment="left" Caption="ShareTo" Editor="text" FieldName="ShareTo" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="SalesKindID" Editor="text" FieldName="SalesKindID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        </Columns>
                    </JQTools:JQDataForm>
                    <JQTools:JQDefault ID="JQDefault2" runat="server" BindingObjectID="DFContactRecord" EnableTheming="True">
                        <Columns>
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="ContactDate" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERCODE" FieldName="CreateBy" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERNAME" FieldName="CreateByName" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="false" FieldName="IsShade" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetCustomerID" FieldName="CustomerID" RemoteMethod="False" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="S35" FieldName="SalesKindID" RemoteMethod="True" />
                        </Columns>
                    </JQTools:JQDefault>
                    <JQTools:JQValidate ID="JQValidate2" runat="server" BindingObjectID="DFContactRecord" EnableTheming="True">
                        <Columns>
                            <JQTools:JQValidateColumn CheckNull="True" FieldName="ContactDate" RemoteMethod="True" ValidateMessage="請選擇聯繫日期！" ValidateType="None" />
                            <JQTools:JQValidateColumn CheckNull="True" FieldName="ContactDescr" RemoteMethod="True" ValidateMessage="聯繫內容不可空白！" ValidateType="None" />
                        </Columns>
                    </JQTools:JQValidate>
                    <JQTools:JQAutoSeq ID="JQAutoSeq2" runat="server" BindingObjectID="DFContactRecord" FieldName="AutoKey" NumDig="1" />
                </JQTools:JQDialog>
            </JQTools:JQDialog>
        
        <JQTools:JQDialog ID="JQDialogClu" runat="server" DialogLeft="330px" DialogTop="50px" Title="業務人員" Width="200px" Closed="True" ShowSubmitDiv="True" DialogCenter="False"  EnableTheming="False" ScrollBars="Vertical" OnSubmited="JQDialogCluOnSubmited" Height="370px">
                 <JQTools:JQDataGrid ID="JQDataGrid1" runat="server" EditDialogID="" AlwaysClose="True" DataMember="SalesKindUser" RemoteName="sERP_Customer_Normal_Customer.SalesKindUser" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AutoApply="True" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="True" PageList="10,20,40,60,80" PageSize="10" Pagination="False" QueryAutoColumn="False" QueryLeft="120px" QueryMode="Window" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Height="270px" Width="150px" BufferView="False" NotInitGrid="False" RowNumbers="False" EnableTheming="False" OnLoadSuccess="JQDataGrid1OnLoadSuccess">
                     <Columns>
                         <JQTools:JQGridColumn Alignment="left" Caption="業務人員" Editor="text" FieldName="SalesName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="75">
                         </JQTools:JQGridColumn>
                         <JQTools:JQGridColumn Alignment="left" Caption="SalesID" Editor="text" FieldName="SalesID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                         </JQTools:JQGridColumn>
                     </Columns>
                 </JQTools:JQDataGrid>
          </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
