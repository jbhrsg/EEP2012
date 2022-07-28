<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_SalesEmail.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">

        $(document).ready(function () {
            $('#JQDate1').datebox({ 'width': 86 });
            $('#JQDate2').datebox({ 'width': 86 });
            $('#cbIsAcceptePaper').combobox({ 'width': 60 });
            $('#cbInvoiceYM').combobox({ 'width': 70 });
            var dt = new Date();
            var aDate = new Date($.jbDateAdd('days', 0, dt));//今天日期
            var StartaDate = $.jbGetFirstDate(aDate);
            var LastaDate = $.jbGetLastDate(aDate);
            $("#JQDate1").datebox('setValue', $.jbjob.Date.DateFormat(StartaDate, 'yyyy/MM/dd'));
            $("#JQDate2").datebox('setValue', $.jbjob.Date.DateFormat(LastaDate, 'yyyy/MM/dd'));
            //$("#cbInvoiceYM").combobox('setValue', $.jbjob.Date.DateFormat(aDate, 'yyyy/MM'));
            $("#EmailButton").linkbutton({ 'plain': false });//"發信"按鈕style      
        });
        function OnLoadSuccessdataGridMaster() {//查詢也會觸發OnLoadSuccessdataGridMaster
            if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) {//dataGridMaster第一次OnLoadSuccess
                RefreshGrid();
            }
            $("#dataGridMaster").datagrid("unselectAll");//取消打勾(multiselect會預設打勾)
        }
       
        //OnClick"發信"按鈕
        function OnClickEmailButton() {
            if ($("#dataGridMaster").datagrid('getChecked').length == 0) {
                alert('請勾選發信項目。');
            } else {
               var pre = confirm("確定發信?");
               if (pre == true) {
                   var rows = $('#dataGridMaster').datagrid('getChecked');

                   var aCustNO = [];
                   var aoInvoiceYM = [];
                   var aCustShortName = [];
                   var aContactA = [];
                   var aContactAMail = [];
                   var abilldeal = [];
                   var abilldealemail = [];
                   var aSalesName = [];
                   var temp1, temp2;
                   for (var i = 0; i < rows.length; i++) {
                       aCustNO.push(rows[i].CustNO);
                       aoInvoiceYM.push(rows[i].oInvoiceYM);
                       aCustShortName.push(rows[i].CustShortName);

                       temp1 = rows[i].ContactAMail;
                       temp2 = rows[i].billdealemail;
                       if (temp1 == null) { temp1 = '' } else { temp1 = temp1.trim() }
                       if (temp2 == null) { temp2 = '' } else { temp2 = temp2.trim() }
                       if ((temp1 == '') && (temp2 == '')) {
                           alert(rows[i].CustNO + '' + rows[i].CustShortName + '的email空白');
                           return false;
                       }

                       aContactA.push(rows[i].ContactA);
                       if (validateEmail(temp1) || temp1 == '') {
                           aContactAMail.push(temp1);
                       } else {
                           alert(rows[i].CustNO + '' + rows[i].CustShortName + '的email有誤');
                           return false;
                       }


                       abilldeal.push(rows[i].billdeal);
                       if (validateEmail(temp2) || temp2 == '') {
                           abilldealemail.push(temp2);
                       } else {
                           alert(rows[i].CustNO + '' + rows[i].CustShortName + '的email有誤');
                           return false;
                       }


                       aSalesName.push(rows[i].SalesName);
                   }
                   var sCustNO = aCustNO.join('*');
                   var soInvoiceYM = aoInvoiceYM.join('*');
                   var sCustShortName = aCustShortName.join('*');
                   var sContactA = aContactA.join('*');
                   var sContactAMail = aContactAMail.join('*');
                   var sbilldeal = abilldeal.join('*');
                   var sbilldealemail = abilldealemail.join('*');
                   var sSalesName = aSalesName.join('*');

                   $.ajax({//新增資料到ERPSalesEmail，排程發信軟體會監聽此表格是否新增資料(IsSendMail is NULL)，在抓所新增的資料去寄出
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalesEmail.ERPSalesDetails', //連接的Server端，command
                        data: "mode=method&method=" + "UpdateERPSalesEmail" + "&parameters=" + sCustNO + "," + soInvoiceYM + "," + sCustShortName + "," + sContactA + "," + sContactAMail + "," + sbilldeal + "," + sbilldealemail + "," + sSalesName,//+ "," + SalesYM
                        cache: false,
                        async: false,
                        success: function (data) {
                            if (data != false) {
                                alert("排入發信排程成功");
                            } else { alert("排入發信排程失敗"); }
                            $('#dataGridMaster').datagrid("reload");//dataGridMaster重load資料
                        }
                    });
                }
            }
        }

        function validateEmail(email) {
            var reg = /[\w\-\.]+\@[\w\-\.]+\.[\w\-\.]+/;
            if (reg.test(email)) {
                return true;
            } else {
                return false;
            }
        }

        //OnClick"查詢"按鈕或dataGridMaster第一次OnLoadSuccess
        function RefreshGrid() {
            var CustNO = $("#cbCustNO").combobox('getValue');
            var SalesID = $("#cbSalesEmployeeID").combobox('getValue');
            var JQDate1 = $("#JQDate1").combo('textbox').val();//datebox("getBindingValue");//datebox("getValue");                
            var JQDate2 = $("#JQDate2").combo('textbox').val();
            var InvoiceYM = $("#cbInvoiceYM").combobox('getValue');
            var IsAcceptePaper = $("#cbIsAcceptePaper").combobox('getValue');
            var where = $("#dataGridMaster").datagrid('getWhere');
            if (JQDate1 == '' && JQDate2 == '' && InvoiceYM != "不拘" && InvoiceYM != ""){//只選發票年月
                where = where + " d.InvoiceYM ='" + InvoiceYM + "'";
            } else if (JQDate1 != '' && JQDate2 != '' &&( InvoiceYM == "不拘" || InvoiceYM == "")) {//只選銷貨日期
                where = where + " d.SalesDate between '" + JQDate1 + "' and '" + JQDate2 + "'";
            } else if (JQDate1 != '' && JQDate2 != '' && (InvoiceYM != "不拘" && InvoiceYM != "")) {//選銷貨日期及發票年月
                where = where + " d.SalesDate between '" + JQDate1 + "' and '" + JQDate2 + "'";
                where = where + " and d.InvoiceYM ='" + InvoiceYM + "'";
            }
            if (CustNO != "==不拘==" && CustNO != "") {
                    where = where + " and d.CustNO='" + CustNO + "'";
                }
                if (SalesID != "==不拘==" && SalesID != "") {
                    where = where + " and d.SalesID='" + SalesID + "'";
                }
                if (IsAcceptePaper != "-1" && IsAcceptePaper != "") {
                    where = where + " and c.IsAcceptePaper='" + IsAcceptePaper + "'";
                }
                $("#dataGridMaster").datagrid('setWhere', where);
        }

        function FormatScriptIsSendMail(value) {
            if (value == null) { return "未發" }
            else if (value == 1) { return "成功" }
            else if (value == 0) { return "失敗" }
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
            <asp:Label ID="Label3" runat="server" Font-Size="Small" Text="客戶代號:"></asp:Label>
            <JQTools:JQComboBox ID="cbCustNO" runat="server" DisplayMember="CustShortName" RemoteName="sERPSalesEmail.View_ERPCustomers" ValueMember="CustNO">
            </JQTools:JQComboBox>           
            &nbsp; <asp:Label ID="Label1" runat="server" Font-Size="Small" Text="業務:"></asp:Label>
            <JQTools:JQComboBox ID="cbSalesEmployeeID" runat="server" DisplayMember="SalesName" PanelHeight="150" RemoteName="sERPSalesEmail.ERPSalesMan" ValueMember="SalesID" Width="50px">
            </JQTools:JQComboBox>
            &nbsp;<asp:Label ID="Label4" runat="server" Font-Size="Small" Text="接受電子報:"></asp:Label>
            <JQTools:JQComboBox ID="cbIsAcceptePaper" runat="server" DisplayMember="ePaperType" RemoteName="sERPSalesEmail.IsAcceptePaper" ValueMember="IsAcceptePaper" Width="100px"></JQTools:JQComboBox>
            <asp:Label ID="Label5" runat="server" Font-Size="Small" Text="銷貨日期:"></asp:Label>
            <JQTools:JQDateBox ID="JQDate1" runat="server" Width="100px" />
            〜<JQTools:JQDateBox ID="JQDate2" runat="server" />
            &nbsp;<asp:Label ID="Label2" runat="server" Font-Size="Small" Text="發票年月:"></asp:Label>
            <JQTools:JQComboBox ID="cbInvoiceYM" runat="server" DisplayMember="InvoiceYM" RemoteName="sERPSalesEmail.InvoiceYM" ValueMember="InvoiceYM" Width="80px"></JQTools:JQComboBox>
            &nbsp;<JQTools:JQButton ID="JQButton1" runat="server" OnClick="RefreshGrid" Text="查詢" />
            <br />
            </div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sERPSalesEmail.ERPSalesDetails" runat="server" AutoApply="True"
                DataMember="ERPSalesDetails" Pagination="False" QueryTitle="Query"
                Title="銷貨清單" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="True" MultiSelect="True" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" IDField="" Height="500px" OnLoadSuccess="OnLoadSuccessdataGridMaster">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶編號" Editor="text" FieldName="CustNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶簡稱" Editor="text" FieldName="CustShortName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="130">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="電子報" Editor="text" FieldName="ePaperType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="44">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人1" Editor="text" FieldName="ContactA" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人1信箱" Editor="text" FieldName="ContactAMail" MaxLength="0" Width="150" />
                    <JQTools:JQGridColumn Alignment="left" Caption="帳務人員" Editor="text" FieldName="billdeal" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="帳務人員信箱" Editor="text" FieldName="billdealemail" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="150">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="發票年月" Editor="text" FieldName="oInvoiceYM" MaxLength="0" Width="62" />
                    <JQTools:JQGridColumn Alignment="left" Caption="發數" Editor="text" FieldName="Counts" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="最近發信日期" Editor="text" FieldName="MaxCreateDate" Format="yyyy-mm-dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="業務人員" Editor="text" FieldName="SalesName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="發信狀態" Editor="text" FieldName="IsSendMail" FormatScript="FormatScriptIsSendMail" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="Update" Visible="False" />
                    <JQTools:JQToolItem Icon="icon-cancel" ItemType="easyui-linkbutton" OnClick="cancel" Text="Cancel" Visible="False" />
                    <JQTools:JQToolItem ID="EmailButton" Enabled="True" ItemType="easyui-linkbutton" OnClick="OnClickEmailButton" Text="發信" Visible="True" Icon="icon-next" />
                    <JQTools:JQToolItem ID="SalesDrawCustomerUrl" Enabled="True" Icon="icon-tip" ItemType="easyui-textbox" Text="客戶畫面連結 http://www.jbhr.com.tw/jqwebclient/JB_ADMIN/JBERP_SalesDrawCustomer.aspx?cn=?????&amp;ym=2017/??" Visible="True" />
                </TooItems>
            </JQTools:JQDataGrid>
        </div>
    <JQTools:JQDefault runat="server" BindingObjectID="dataGridMaster" BorderStyle="NotSet" Enabled="True" EnableTheming="True" ClientIDMode="Inherit" ID="defaultMaster" EnableViewState="True" ViewStateMode="Inherit" >
</JQTools:JQDefault>
<JQTools:JQValidate runat="server" BindingObjectID="dataGridMaster" BorderStyle="NotSet" Enabled="True" EnableTheming="True" ClientIDMode="Inherit" ID="validateMaster" EnableViewState="True" ViewStateMode="Inherit" >
</JQTools:JQValidate>
</form>
</body>
</html>
