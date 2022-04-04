<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ERP_Normal_Warrant.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        //$(function () {
            //$("#InsGroupID_Query").combobox({
                //onSelect: function (rowData) {
                    //var customerName=$("#CustomerName_Query").combobox('getValue');
                    //if ( customerName != '') {
                    //    $("#dataGridView").datagrid('setWhere', "InsGroupID=" + rowData.InsGroupID + "and CustomerName='" + customerName+"'");
                    //}else {
                        //$("#dataGridView").datagrid('setWhere', "InsGroupID=" + rowData.InsGroupID);
                    //}
                //}
            //});
        //});
        $(function () {
            $('.infosysbutton-q', '#querydataGridView').closest('td').attr({ 'align': 'middle' });

            //按X設firstLoad為false，為了OnLoad_dataGridDetail變單選，checkbox多選
            $('#JQDialog1').dialog('options').onBeforeClose = function () {
                $("#dataGridDetail").data('firstLoad', false);
                return true;
            }

            //必填欄位
            RedTd('#dataFormCheck', ['BankRootID', 'BankBranchID']);
        });
        function dataGridView_OnLoadSuccess() {
            var UserID = getClientInfo("_usercode");
            $('#InsGroupID_Query').combobox('setWhere', "InsGroupID IN (Select Distinct InsGroupID From JBERP.dbo.SalesSalesType X,JBERP.dbo.SalesType Y  Where X.SalesTypeID=Y.SalesTypeID AND X.SalesID = " + UserID + ")");
        }

        function dataFormMaster_OnLoadSuccess() {
            if(getEditMode($('#dataFormMaster'))=='inserted'){
                //var cbInsGroupID = $("#cbInsGroupID").combobox('getValue');
                //查詢的公司別帶入dataFormMaster公司別
                var cbInsGroupID = $("#InsGroupID_Query").combobox('getValue');
                if (cbInsGroupID == '' || cbInsGroupID == undefined) { closeForm('#JQDialog1'); alert('請先選取「公司別」'); return false; }
                $("#dataFormMasterInsGroupID").combobox('setValue', cbInsGroupID);
                //公司別連動公司客戶
                $("#dataFormMasterCompanyCustomerID").combobox('setWhere', "InsGroupID=" + cbInsGroupID);
            }
        }
        //轉入未沖銷之發票明細資料到dataGridDetail
        function SelectInvoiceDetails() {
            //-----------檢查Begin------------
            //卡dataGridDetail已經有資料
            if ($('#dataGridDetail').datagrid('getData').total != 0) {//$('#dataGridDetailTvl1').datagrid('getData')會回傳json object物件ex:object{total: 0, tableName: "dbo.[BizTravelMaster]", keys: "AutoKey,TvlNo", rows: Array[0], footer: Array[1]}
                alert('已有資料，無法轉入');
                return false;
            }

            //卡收款日期
            if ($("#dataFormMasterWarrantDate").datebox('getValue') == "") {
                alert("請填「收款日期」");
                return false;
            }
            //卡公司客戶
            if ($("#dataFormMasterCompanyCustomerID").combobox('getValue') == "") {
                alert("請填「公司客戶」");
                return false;
            }
            //卡沖銷對象
            if ($("#dataFormMasterOffsetTypeID").combobox('getValue') == "") {
                alert("請填「沖銷對象」");
                return false;
            }
            //卡收款方式
            if ($("#dataFormMasterPayWayID").combobox('getValue') == "") {
                alert("請填「收款方式」");
                return false;
            }
            //卡現金單號
            if (($("#dataFormMasterPayWayID").combobox('getText') == "現金" || $("#dataFormMasterPayWayID").combobox('getText') == "匯款") && ($("#dataFormMasterCashNO").val().trim() == "" || $("#dataFormMasterItemNO").val().trim() == "")) {
                alert("請填「現金單號」和「現金單項次」");
                return false;
            }
            //現金單號重複檢查
            var CashNO = $("#dataFormMasterCashNO").val().trim();
            var ItemNO = $("#dataFormMasterItemNO").val().trim();
            if (CashNO != "" && ItemNO != "") {
                var flag1 = true;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sERP_Normal_Warrant.WarrantMaster', //連接的Server端，command
                    data: "mode=method&method=" + "CheckCashNODuplicate" + "&parameters=" + CashNO + "," + ItemNO,
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != false) {
                            var rows = $.parseJSON(data);
                            if (getEditMode($('#dataFormMaster')) == "inserted" && rows.length != 0) {
                                alert("現金單號,現金單項次重複");
                                //return false;
                                flag1 = false;
                            } else if (getEditMode($('#dataFormMaster')) == "updated" && rows[0]["WarrantNO"] != $("#dataFormMasterWarrantNO").val() && rows.length > 0) {
                                alert("現金單號,現金單項次重複");
                                flag1 = false;
                            }
                        }
                    }
                , error: function () { }
                });
                if (flag1 == false) { return false }
            }
            //-----------檢查End------------

            //轉入
            var companyCustomerID = $("#dataFormMasterCompanyCustomerID").combobox('getValue');
            var offsetTypeID = $("#dataFormMasterOffsetTypeID").combobox('getValue');
            var insGroupID = $("#dataFormMasterInsGroupID").combobox('getValue');
            //var cashNOItemNO = $("#dataFormMasterCashNO").refval('getValue');
            var SalesDateB = $("#dataFormMasterSalesDateB").datebox('getValue');
            var SalesDateE = $("#dataFormMasterSalesDateE").datebox('getValue');
            //if (companyCustomerID == '' || insGroupID == '' || offsetTypeID == '') { alert('轉入前，請先選取「公司客戶」和「沖銷對象」'); return false; }
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERP_Normal_Warrant.WarrantMaster', //連接的Server端，command
                data: "mode=method&method=" + "SelectInvoiceDetails" + "&parameters=" + companyCustomerID + "," + insGroupID + "," + offsetTypeID + "," + SalesDateB + "," + SalesDateE,// + "," + cashNOItemNO,
                cache: false,
                async: true,
                beforeSend:function(){
                    var win=$.messager.progress({
                        title: '轉入未沖銷之發票明細資料',
                        msg: '轉入未沖銷之發票明細資料,請稍後...',
                        interval: '500'
                    });
                },
                success: function (data) {
                    $.messager.progress('close');
                    if (data != false) {
                        var WarrantNO=$("#dataFormMasterWarrantNO").val();
                        var rows = $.parseJSON(data);
                        var sumSalesTotal = 0;
                        var sumRecAmount = 0;
                        for (var i = 0; i < rows.length; i++) {
                            var rowData = new Object();
                            rowData['WarrantNO'] = WarrantNO;
                            rowData['ItemNO'] =FormatNumberLength(i + 1,4);
                            rowData['InvoiceNO'] = rows[i].InvoiceNO;
                            rowData['InvoiceTypeName'] = rows[i].InvoiceTypeName;
                            rowData['SalesDate'] = rows[i].SalesDate;
                            rowData['InvoiceDate'] = rows[i].InvoiceDate;
                            rowData['ARDate'] =rows[i].ARDate;
                            rowData['SalesTotal'] =rows[i].SalesTotal;//應收
                            rowData['AcceptedAmount'] = rows[i].AcceptedAmount;//已收
                            rowData['RecAmount'] = rows[i].SalesTotal - rows[i].AcceptedAmount;//收款
                            rowData['OthAmount'] =0;
                            rowData['RebAmount'] =0;
                            rowData['RetAmount'] =0;
                            rowData['BadAmount'] =0;
                            rowData['InsGroupID'] = rows[i].InsGroupID;
                            $("#dataGridDetail").datagrid("appendRow", rowData);

                            //應收金額加總
                            if ($.trim(rowData['SalesTotal']) != "")
                                sumSalesTotal = sumSalesTotal + parseInt($.trim(rowData['SalesTotal']));
                            //收款金額加總
                            if ($.trim(rowData['RecAmount']) != "")
                                sumRecAmount = sumRecAmount + parseInt($.trim(rowData['RecAmount']));
                            
                        }
                        //加總金額顯示
                        $("#dataFormMasterRecAmount").val(sumRecAmount);
                        var footerRows = $('#dataGridDetail').datagrid('getFooterRows');
                        footerRows[0]["SalesTotal"] = sumSalesTotal;
                        footerRows[0]["RecAmount"] = sumRecAmount;
                        $('#dataGridDetail').datagrid('reloadFooter', footerRows);
                    }
                }
                , error: function () { $.messager.progress('close'); }
            });
        }
        //footer值轉到主form收款金額
        function dataGridDetail_RecAmount_OnTotal(footerRow) {
            $("#dataFormMasterRecAmount").val(footerRow.RecAmount);
        }
        function dataGridDetail_OthAmount_OnTotal(footerRow) {
            $("#dataFormMasterOthAmount").val(footerRow.OthAmount);
        }
        function dataGridDetail_RebAmount_OnTotal(footerRow) {
            $("#dataFormMasterRebAmount").val(footerRow.RebAmount);
        }
        function dataGridDetail_RetAmount_OnTotal(footerRow) {
            $("#dataFormMasterRetAmount").val(footerRow.RetAmount);
        }
        function dataGridDetail_BadAmount_OnTotal(footerRow) {
            $("#dataFormMasterBadAmount").val(footerRow.BadAmount);
        }
        //function cbInsGroupID_OnSelect(rowData) {
        //    $("#dataGridView").datagrid('setWhere', "InsGroupID=" + rowData.InsGroupID);
        //}
        //主form存檔後，篩選主grid
        function dataFormMaster_OnApplied() {
            var insGroupID_Query = $("#InsGroupID_Query").combobox('getValue');
            if(insGroupID_Query!=''){
                $("#dataGridView").datagrid('setWhere', "InsGroupID=" + insGroupID_Query);
            }
            $("#dataGridView").datagrid('reload');
            $("#CompanyCustomerID_Query").combobox('setWhere', '');//重整公司客戶_查詢
        }
        function dataFormMaster_OnApply() {
            //-----------檢查Begin------------
            //卡收款日期
            if ($("#dataFormMasterWarrantDate").datebox('getValue') == "") {
                alert("請填「收款日期」");
                return false;
            }
            //卡公司客戶
            if ($("#dataFormMasterCompanyCustomerID").combobox('getValue') == "") {
                alert("請填「公司客戶」");
                return false;
            }
            //卡沖銷對象
            if ($("#dataFormMasterOffsetTypeID").combobox('getValue') == "") {
                alert("請填「沖銷對象」");
                return false;
            }
            //卡收款方式
            if ($("#dataFormMasterPayWayID").combobox('getValue') == "") {
                alert("請填「收款方式」");
                return false;
            }
            //卡現金單號
            if (($("#dataFormMasterPayWayID").combobox('getText') == "現金" || $("#dataFormMasterPayWayID").combobox('getText') == "匯款") && ($("#dataFormMasterCashNO").val().trim() == "" || $("#dataFormMasterItemNO").val().trim() == "")) {
                alert("請填「現金單號」和「現金單項次」");
                return false;
            }
            //現金單號重複檢查
            var CashNO = $("#dataFormMasterCashNO").val().trim();
            var ItemNO = $("#dataFormMasterItemNO").val().trim();
            if (CashNO != "" && ItemNO != "") {
                var flag1 = true;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sERP_Normal_Warrant.WarrantMaster', //連接的Server端，command
                    data: "mode=method&method=" + "CheckCashNODuplicate" + "&parameters=" + CashNO + "," + ItemNO,
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != false) {
                            var rows = $.parseJSON(data);
                            if (getEditMode($('#dataFormMaster')) == "inserted" && rows.length != 0) {
                                alert("現金單號,現金單項次重複");
                                //return false;
                                flag1 = false;
                            } else if (getEditMode($('#dataFormMaster')) == "updated" && rows[0]["WarrantNO"] != $("#dataFormMasterWarrantNO").val() && rows.length > 0) {
                                alert("現金單號,現金單項次重複");
                                flag1 = false;
                            }
                        }
                    }
                , error: function () { }
                });
                if (flag1 == false) { return false }
            }
            //-----------檢查End------------

            //從編輯狀態跳出
            var editRow = $("#dataGridDetail").datagrid('getSelected');
            if (editRow) {
                var editIndex = $("#dataGridDetail").datagrid('getRowIndex', editRow);
                if (editIndex != undefined) {
                    //$("#dataGridDetail").datagrid('selectRow', editIndex).datagrid('beginEdit', editIndex);
                    $("#dataGridDetail").datagrid('selectRow', editIndex).datagrid('endEdit', editIndex);
                }
            }
            
            //明細grid收款金額加總轉到主form收款金額
            var rows = $("#dataGridDetail").datagrid('getData').rows;
            var sumRecAmount = 0;
            for (var i = 0; i < rows.length; i++) {
                if ($.trim(rows[i]["RecAmount"]) != "")
                    sumRecAmount = sumRecAmount + parseInt($.trim(rows[i]["RecAmount"]));
            }
            $("#dataFormMasterRecAmount").val(sumRecAmount);

            //var oldRows = $("#dataGridDetail").datagrid('getData').rows;
            //var oldRowsLength = oldRows.length;
            //for (var i = 0; i < oldRowsLength; i++) {
            //    if (oldRows[i]["RecAmount"] != "0" && oldRows[i]["RecAmount"] != "") {
            //        $("#dataGridDetail").datagrid("appendRow", oldRows[i]);
            //    }
            //}
            //for(var i=oldRowsLength-1;i>=0;i--){
            //    var index = $("#dataGridDetail").datagrid("getRowIndex", oldRows[i]);
            //    $("#dataGridDetail").datagrid("deleteRow", index);
            //}
        }

        function dataFormCheck_BankRootID_OnSelect(rowdata) {
            $("#dataFormCheckBankBranchID").combobox('setWhere', "BankRootID='" + rowdata.BankRootID + "'");
        }

        //function dataFormCheck_BankBranchID_OnSelect(rowdata) {
        //    $("#dataFormCheckBourse").val(rowdata.Bourse);
        //    $("#dataFormCheckBankID").val(rowdata.BankID);
        //}

        //多選刪除
        function deleteItem(dgid) {
            //var ChcekedRow = $(dgid).datagrid('getChceked');
            var rows = $(dgid).datagrid('getChecked');
            var rowIndexes = [];
            var index;
            //取勾選的Index，再刪除該index的row
            for (var i = 0; i < rows.length; i++) {
                index = $(dgid).datagrid('getRowIndex', rows[i]);//只能對一個row取
                $(dgid).datagrid('deleteRow', index);//刪除後，index就會改變
            }
        }

        function dataGridDetail_OnLoadSuccess() {
            //單select，多check
            if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) {
                $("#dataGridDetail").datagrid({
                    singleSelect: true,
                    selectOnCheck: false,
                    checkOnSelect:false
                });
            }

            //為了取消預設第一列勾選
            setTimeout(function () {
                $("#dataGridDetail").datagrid("unselectAll");
            }, 600);

        }

        function dataFormCheck_OnApply() {

            var bankRootID=$("#dataFormCheckBankRootID").combobox("getValue").trim();
            var bankBranchID = $("#dataFormCheckBankBranchID").combobox("getValue").trim();
            var stopFlag = false;
            if (bankRootID != "" && bankBranchID != "") {
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sERP_Normal_Warrant.WarrantMaster', //連接的Server端，command
                    data: "mode=method&method=" + "SelectBourseBankID" + "&parameters=" + bankRootID + "," + bankBranchID,
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != "False") {
                            var rows = $.parseJSON(data);
                            if (rows.length > 0) {
                                if (rows[0]["BankID"] != "") {
                                    $("#dataFormCheckBourse").val(rows[0]["Bourse"]);
                                    $("#dataFormCheckBankID").val(rows[0]["BankID"]);
                                } else {
                                    alert("銀行代號BankID不能空白，請洽管理室");
                                    stopFlag = true;
                                }
                            } else {
                                alert("銀行代號BankID有誤，請洽管理室");
                                stopFlag = true;
                            }
                        } else {
                            alert("SelectBourseBankID有誤，請洽管理室");
                            stopFlag = true;
                        }
                    }
                });
            } else {
                alert("銀行主行代號和分行代號為必填");
                stopFlag = true;
            }
            if (stopFlag == true) { return false }
        }

        //明細grid項次編碼用
        function FormatNumberLength(num, length) {
            var r = "" + num;
            while (r.length < length) {
                r = "0" + r;
            }
            return r;
        }
        function RedTd(FormName, FieldNames) {
            $.each(FieldNames, function (index, value) {
                $(FormName + value).closest('td').prev('td').css({ 'color': 'red' });
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <%--<div>
            <asp:Label ID="lbInsGroupID" runat="server" Font-Size="Small" Text="公司別:"></asp:Label>
            <JQTools:JQComboBox ID="cbInsGroupID" runat="server" DisplayMember="InsGroupName" OnSelect="cbInsGroupID_OnSelect" RemoteName="sERP_Normal_Warrant.InsGroup" ValueMember="InsGroupID" CheckData="True" Width="350px"></JQTools:JQComboBox>
            </div>--%>

            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERP_Normal_Warrant.WarrantMaster" runat="server" AutoApply="True"
                DataMember="WarrantMaster" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="收款單資料    (新增前，請先選取「公司別」)" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="20,40,60,80,100" PageSize="20" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnLoadSuccess="dataGridView_OnLoadSuccess">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="收款單號" Editor="text" FieldName="WarrantNO" MaxLength="20" Visible="true" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="收款日期" Editor="text" FieldName="WarrantDate" Visible="true" Width="70" MaxLength="8" Format="yyyy-mm-dd" />
                    <JQTools:JQGridColumn Alignment="left" Caption="收款方式" Editor="infocombobox" FieldName="PayWayID" MaxLength="1" Visible="False" Width="60" EditorOptions="valueField:'PayWayID',textField:'PayWayName',remoteName:'sERP_Normal_Warrant.PayWay',tableName:'PayWay',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="公司別" Editor="infocombobox" FieldName="InsGroupID" MaxLength="4" Visible="true" Width="90" EditorOptions="valueField:'InsGroupID',textField:'ShortName',remoteName:'sERP_Normal_Warrant.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="公司客戶" Editor="infocombobox" FieldName="CompanyCustomerID" Visible="True" Width="100" MaxLength="20" EditorOptions="valueField:'CustomerID',textField:'ShortName',remoteName:'sERP_Normal_Warrant.Customer',tableName:'Customer',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="公司客戶" Editor="text" FieldName="CustomerName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="沖銷對象" Editor="infocombobox" FieldName="OffsetTypeID" Visible="true" Width="72" MaxLength="0" EditorOptions="valueField:'OffsetTypeID',textField:'OffsetName',remoteName:'sERP_Normal_Warrant.OffsetAccountType',tableName:'OffsetAccountType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="right" Caption="應收金額" Editor="numberbox" FieldName="SumSalesTotal" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70" Format="N">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="收款金額" Editor="text" FieldName="RecAmount" Frozen="False" IsNvarChar="False" MaxLength="4" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70" Format="N">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="其他金額" Editor="text" FieldName="OthAmount" Visible="False" Width="70" MaxLength="4" Format="N" />
                    <JQTools:JQGridColumn Alignment="right" Caption="折讓金額" Editor="text" FieldName="RebAmount" Visible="False" Width="70" MaxLength="4" Format="N" />
                    <JQTools:JQGridColumn Alignment="right" Caption="退貨金額" Editor="text" FieldName="RetAmount" Visible="False" Width="70" MaxLength="4" Format="N" />
                    <JQTools:JQGridColumn Alignment="right" Caption="呆帳金額" Editor="text" FieldName="BadAmount" Visible="False" Width="70" MaxLength="4" Format="N" />
                    <JQTools:JQGridColumn Alignment="left" Caption="現金單號" Editor="text" FieldName="CashNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="項次" Editor="text" FieldName="ItemNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" MaxLength="20" Visible="true" Width="50" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Visible="true" Width="95" MaxLength="8" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="LastUpdateBy" MaxLength="20" Visible="true" Width="50" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="LastUpdateDate" Visible="true" Width="95" MaxLength="8" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem"
                        Text="Update" Visible="False" />
                    <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem"
                        Text="Delete" Visible="False" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton"
                        OnClick="apply" Text="Apply" Visible="False" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-cancel" ItemType="easyui-linkbutton" OnClick="cancel" Text="Cancel" Visible="False" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="Query" Visible="False" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Export" Visible="False" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="公司別" Condition="=" DataType="string" Editor="infocombobox" FieldName="InsGroupID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="120" EditorOptions="valueField:'InsGroupID',textField:'ShortName',remoteName:'sERP_Normal_Warrant.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="公司客戶" Condition="=" DataType="string" Editor="infocombobox" FieldName="CompanyCustomerID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="140" EditorOptions="valueField:'CompanyCustomerID',textField:'ShortName',remoteName:'sERP_Normal_Warrant.CompanyCustomerOfWarrantMaster',tableName:'CompanyCustomerOfWarrantMaster',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="沖銷對象" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'OffsetTypeID',textField:'OffsetName',remoteName:'sERP_Normal_Warrant.OffsetAccountType',tableName:'OffsetAccountType',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="OffsetTypeID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="收款日起迄" Condition="&gt;=" DataType="datetime" Editor="datebox" FieldName="WarrantDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" Format="yyyy/mm/dd" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="~" Condition="&lt;=" DataType="datetime" Editor="datebox" FieldName="WarrantDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" Format="yyyy/mm/dd" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="收款單" DialogLeft="10px" DialogTop="10px" Width="1200px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="WarrantMaster" HorizontalColumnsCount="2" RemoteName="sERP_Normal_Warrant.WarrantMaster" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="dataFormMaster_OnLoadSuccess" OnApplied="dataFormMaster_OnApplied" OnApply="dataFormMaster_OnApply">

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="收款單號" Editor="text" FieldName="WarrantNO" Format="" Width="180" ReadOnly="True" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="infocombobox" FieldName="InsGroupID" Format="" Width="180" EditorOptions="valueField:'InsGroupID',textField:'ShortName',remoteName:'sERP_Normal_Warrant.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" ReadOnly="True" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="收款日期" Editor="datebox" FieldName="WarrantDate" Format="yyyy/mm/dd" Width="180" maxlength="0" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司客戶" Editor="infocombobox" EditorOptions="valueField:'CustomerID',textField:'ShortName',remoteName:'sERP_Normal_Warrant.CompanyCustomerOfInvoiceDetails',tableName:'CompanyCustomerOfInvoiceDetails',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CompanyCustomerID" Format="" Width="180" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨日起" Editor="datebox" EditorOptions="dateFormat:'datetime',showTimeSpinner:false,showSeconds:false,editable:true" FieldName="SalesDateB" maxlength="0" Width="100" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨日迄" Editor="datebox" FieldName="SalesDateE" Width="100" EditorOptions="dateFormat:'datetime',showTimeSpinner:false,showSeconds:false,editable:true" ReadOnly="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="沖銷對象" Editor="infocombobox" FieldName="OffsetTypeID" Width="100" ReadOnly="False" EditorOptions="valueField:'OffsetTypeID',textField:'OffsetName',remoteName:'sERP_Normal_Warrant.OffsetAccountType',tableName:'OffsetAccountType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="收款方式" Editor="infocombobox" FieldName="PayWayID" Width="100" ReadOnly="False" Visible="True" EditorOptions="valueField:'PayWayID',textField:'PayWayName',remoteName:'sERP_Normal_Warrant.PayWay',tableName:'PayWay',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" Format="" MaxLength="0" NewRow="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="現金單號" Editor="text" FieldName="CashNO" Width="180" ReadOnly="False" Visible="True" EditorOptions="title:'現金單號項次',panelWidth:500,remoteName:'sERP_Normal_Warrant.CashTakeBackDetails',tableName:'CashTakeBackDetails',columns:[{field:'CashTakeBackNO',title:'現金單號',width:100,align:'left',table:'d',isNvarChar:false,queryCondition:''},{field:'ItemNO',title:'項次',width:90,align:'left',table:'d',isNvarChar:false,queryCondition:''},{field:'Amount',title:'金額',width:90,align:'right',table:'',isNvarChar:false,queryCondition:'',formatter:formatValue,format:'N'},{field:'ShortName',title:'客戶',width:90,align:'left',table:'c',isNvarChar:false,queryCondition:''},{field:'CreateDate',title:'建立日期',width:100,align:'left',table:'d',isNvarChar:false,queryCondition:'',formatter:formatValue,format:'yyyy/mm/dd'}],columnMatches:[],whereItems:[],valueField:'CashTakeBackNOItemNO',textField:'CashTakeBackNOItemNO',valueFieldCaption:'CashTakeBackNOItemNO',textFieldCaption:'CashTakeBackNOItemNO',cacheRelationText:true,checkData:false,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" />
                        <JQTools:JQFormColumn Alignment="left" Caption="現金單項次" Editor="text" FieldName="ItemNO" Width="100" ReadOnly="False" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="收款金額" Editor="text" FieldName="RecAmount" Width="180" Format="" ReadOnly="True" Visible="True" maxlength="0" NewRow="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="其他金額" Editor="text" FieldName="OthAmount" ReadOnly="True" Visible="False" Width="180" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="折讓金額" Editor="text" FieldName="RebAmount" Format="" Width="180" Visible="False" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="退貨金額" Editor="text" FieldName="RetAmount" Format="" Width="180" Visible="False" maxlength="0" ReadOnly="True" NewRow="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="呆帳金額" Editor="text" FieldName="BadAmount" Format="" Width="180" Visible="False" maxlength="0" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" Format="" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="" MaxLength="0" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="LastUpdateBy" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="LastUpdateDate" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" />
                    </Columns>
                </JQTools:JQDataForm>

                <JQTools:JQTab ID="JQTab1" runat="server"  Width="">
                <JQTools:JQTabItem ID="JQTabItem1" runat="server" PreLoad="True" Title="收款單明細"><JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="False" DataMember="WarrantDetails" EditDialogID="" Pagination="False" ParentObjectID="dataFormMaster" RemoteName="sERP_Normal_Warrant.WarrantMaster" Title="收款單明細" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="True" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="True" NotInitGrid="False" PageList="20,40,60,80,100" PageSize="20" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="總計" UpdateCommandVisible="False" ViewCommandVisible="False" OnLoadSuccess="dataGridDetail_OnLoadSuccess" ><Columns><JQTools:JQGridColumn Alignment="left" Caption="WarrantNO" Editor="text" FieldName="WarrantNO" Format="" Width="120" Frozen="False" ReadOnly="True" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="項次" Editor="text" FieldName="ItemNO" Format="" Width="30" Frozen="False" ReadOnly="True" /><JQTools:JQGridColumn Alignment="left" Caption="單據號碼" Editor="text" FieldName="InvoiceNO" Format="" Width="80" Frozen="False" ReadOnly="True" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" Visible="True" /><JQTools:JQGridColumn Alignment="left" Caption="單據類型" Editor="text" FieldName="InvoiceTypeName" Frozen="False" ReadOnly="True" Visible="True" Width="96"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="銷貨日期" Editor="datebox" FieldName="SalesDate" Format="yyyy-mm-dd" Width="70" Frozen="False" ReadOnly="True" Visible="True" /><JQTools:JQGridColumn Alignment="left" Caption="單據日期" Editor="text" FieldName="InvoiceDate" Format="yyyy-mm-dd" Frozen="False" ReadOnly="True" Visible="True" Width="70" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False"></JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="應收款日" Editor="datebox" FieldName="ARDate" Width="70" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Format="yyyy-mm-dd" /><JQTools:JQGridColumn Alignment="right" Caption="應收金額" Editor="text" FieldName="SalesTotal" Width="70" Frozen="False" ReadOnly="True" OnTotal="" Total="sum" Format="N" TableName="d" /><JQTools:JQGridColumn Alignment="right" Caption="已收金額" Editor="text" FieldName="AcceptedAmount" Width="70" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Total="" Format="N" /><JQTools:JQGridColumn Alignment="right" Caption="收款金額" Editor="numberbox" FieldName="RecAmount" Format="N" Width="70" OnTotal="dataGridDetail_RecAmount_OnTotal" Total="sum" TableName="" ReadOnly="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="其他金額" Editor="numberbox" FieldName="OthAmount" Format="N" Width="70" OnTotal="dataGridDetail_OthAmount_OnTotal" Total="" /><JQTools:JQGridColumn Alignment="right" Caption="折讓金額" Editor="numberbox" FieldName="RebAmount" Format="N" Width="70" OnTotal="dataGridDetail_RebAmount_OnTotal" Total="" /><JQTools:JQGridColumn Alignment="right" Caption="退貨金額" Editor="numberbox" FieldName="RetAmount" Format="N" Width="70" OnTotal="dataGridDetail_RetAmount_OnTotal" Total="" /><JQTools:JQGridColumn Alignment="right" Caption="呆帳金額" Editor="numberbox" FieldName="BadAmount" Format="N" Width="70" OnTotal="dataGridDetail_BadAmount_OnTotal" Total="" /><JQTools:JQGridColumn Alignment="right" Caption="InsGroupID" Editor="numberbox" FieldName="InsGroupID" Format="" Width="120" Visible="False" Total="" /><JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" Format="" Width="60" ReadOnly="True" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="text" FieldName="CreateDate" Format="" Width="70" ReadOnly="True" /><JQTools:JQGridColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="LastUpdateBy" Format="" Width="60" ReadOnly="True" /><JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="text" FieldName="LastUpdateDate" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="70"></JQTools:JQGridColumn></Columns><RelationColumns><JQTools:JQRelationColumn FieldName="WarrantNO" ParentFieldName="WarrantNO" /></RelationColumns><TooItems><JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" Visible="False" /><JQTools:JQToolItem Enabled="True" Icon="icon-add" ItemType="easyui-linkbutton" OnClick="SelectInvoiceDetails" Text="轉入未沖銷完的單據明細(公司別,公司客戶,銷貨日起迄,沖銷對像為轉入條件)" Visible="True" /><JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="刪除" Visible="True" /></TooItems></JQTools:JQDataGrid><JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail"><JQTools:JQDataForm ID="dataFormDetail" runat="server" ParentObjectID="dataFormMaster" DataMember="WarrantDetails" HorizontalColumnsCount="2" RemoteName="sERP_Normal_Warrant.WarrantMaster" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" ><Columns><JQTools:JQFormColumn Alignment="left" Caption="WarrantNO" Editor="text" FieldName="WarrantNO" Format="" Width="120" Visible="False" /><JQTools:JQFormColumn Alignment="left" Caption="ItemNO" Editor="text" FieldName="ItemNO" Format="" Width="120" ReadOnly="True" /><JQTools:JQFormColumn Alignment="left" Caption="InvoiceNO" Editor="text" FieldName="InvoiceNO" Format="" Width="120" ReadOnly="True" /><JQTools:JQFormColumn Alignment="left" Caption="SalesDate" Editor="datebox" FieldName="SalesDate" Format="" Width="120" ReadOnly="True" /><JQTools:JQFormColumn Alignment="left" Caption="RecAmount" Editor="numberbox" FieldName="RecAmount" Format="" Width="120" /><JQTools:JQFormColumn Alignment="left" Caption="OthAmount" Editor="numberbox" FieldName="OthAmount" Format="" Width="120" /><JQTools:JQFormColumn Alignment="left" Caption="RebAmount" Editor="numberbox" FieldName="RebAmount" Format="" Width="120" /><JQTools:JQFormColumn Alignment="left" Caption="RetAmount" Editor="numberbox" FieldName="RetAmount" Format="" Width="120" /><JQTools:JQFormColumn Alignment="left" Caption="BadAmount" Editor="numberbox" FieldName="BadAmount" Format="" Width="120" /><JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="numberbox" FieldName="InsGroupID" Format="" Width="120" Visible="False" /><JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="120" Visible="False" /><JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" /><JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" Width="120" Visible="False" /><JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="120" Visible="False" /></Columns><RelationColumns><JQTools:JQRelationColumn FieldName="WarrantNO" ParentFieldName="WarrantNO" /></RelationColumns></JQTools:JQDataForm></JQTools:JQDialog></JQTools:JQTabItem>
                <JQTools:JQTabItem ID="JQTabItem2" runat="server" PreLoad="True" Title="支票明細"><JQTools:JQDataGrid ID="dataGridCheck" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" AutoApply="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="CheckDetails" DeleteCommandVisible="True" DuplicateCheck="True" EditDialogID="JQDialog3" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" ParentObjectID="dataFormMaster" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sERP_Normal_Warrant.WarrantMaster" RowNumbers="True" Title="支票明細" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True"><Columns><JQTools:JQGridColumn Alignment="left" Caption="WarrantNO" Editor="text" FieldName="WarrantNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="ItemNO" Editor="text" FieldName="ItemNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="InsGroupID" Editor="text" FieldName="InsGroupID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="WarrantDate" Editor="text" FieldName="WarrantDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="支票號碼" Editor="text" FieldName="CheckNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="到期日" Editor="datebox" EditorOptions="dateFormat:'datetime',showTimeSpinner:false,showSeconds:false,editable:true" FieldName="CheckDueDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="支票金額" Editor="numberbox" EditorOptions="" FieldName="Amount" Format="N" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="銀行主行代號" Editor="text" EditorOptions="" FieldName="BankRootID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="銀行分行代號" Editor="text" EditorOptions="" FieldName="BankBranchID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="交換碼" Editor="text" FieldName="Bourse" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="90"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="銀行代碼" Editor="text" FieldName="BankID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="支票帳號" Editor="text" FieldName="CheckAccount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="支票銀行帳戶" Editor="infocombobox" EditorOptions="valueField:'CheckAccountID',textField:'CheckAccountName',remoteName:'sERP_Normal_Warrant.CheckAccount',tableName:'CheckAccount',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AccountID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="CustomerID" Editor="text" FieldName="CustomerID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="90"></JQTools:JQGridColumn></Columns><RelationColumns><JQTools:JQRelationColumn FieldName="WarrantNO" ParentFieldName="WarrantNO" /></RelationColumns><TooItems><JQTools:JQToolItem Enabled="True" Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" Visible="True" /><JQTools:JQToolItem Enabled="True" Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="Update" Visible="False" /><JQTools:JQToolItem Enabled="True" Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="Delete" Visible="False" /><JQTools:JQToolItem Enabled="True" Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="Apply" Visible="False" /><JQTools:JQToolItem Enabled="True" Icon="icon-cancel" ItemType="easyui-linkbutton" OnClick="cancel" Text="Cancel" Visible="False" /><JQTools:JQToolItem Enabled="True" Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="Query" Visible="False" /><JQTools:JQToolItem Enabled="True" Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Export" Visible="False" /></TooItems></JQTools:JQDataGrid><JQTools:JQDialog ID="JQDialog3" runat="server" BindingObjectID="dataFormCheck" Title="支票內容" DialogTop="0px">
                    <JQTools:JQDataForm ID="dataFormCheck" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="CheckDetails" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="1" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="dataFormMaster" RemoteName="sERP_Normal_Warrant.WarrantMaster" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApply="dataFormCheck_OnApply"><Columns><JQTools:JQFormColumn Alignment="left" Caption="WarrantNO" Editor="text" FieldName="WarrantNO" Visible="False" Width="80" /><JQTools:JQFormColumn Alignment="left" Caption="ItemNO" Editor="text" FieldName="ItemNO" ReadOnly="True" Width="80" Visible="False" /><JQTools:JQFormColumn Alignment="left" Caption="InsGroupID" Editor="text" FieldName="InsGroupID" ReadOnly="False" Visible="False" Width="80" /><JQTools:JQFormColumn Alignment="left" Caption="WarrantDate" Editor="text" FieldName="WarrantDate" ReadOnly="False" Visible="False" Width="80" NewRow="False" /><JQTools:JQFormColumn Alignment="left" Caption="支票號碼" Editor="text" FieldName="CheckNO" Width="300" NewRow="False" /><JQTools:JQFormColumn Alignment="left" Caption="到期日" Editor="datebox" EditorOptions="dateFormat:'datetime',showTimeSpinner:false,showSeconds:false,editable:true" FieldName="CheckDueDate" Width="100" NewRow="False" /><JQTools:JQFormColumn Alignment="left" Caption="支票金額" Editor="numberbox" FieldName="Amount" Width="100" NewRow="False" />
                    <JQTools:JQFormColumn Alignment="left" Caption="銀行主行代號" Editor="infocombobox" EditorOptions="valueField:'BankRootID',textField:'BankRootName',remoteName:'sERP_Normal_Warrant.BankRootID',tableName:'BankRootID',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:dataFormCheck_BankRootID_OnSelect,panelHeight:200" FieldName="BankRootID" Width="300" NewRow="False" ReadOnly="False" />
                    <JQTools:JQFormColumn Alignment="left" Caption="銀行分行代號" Editor="infocombobox" EditorOptions="valueField:'BankBranchID',textField:'BankBranchName',remoteName:'sERP_Normal_Warrant.Bank',tableName:'Bank',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="BankBranchID" Width="300" NewRow="False" ReadOnly="False" /><JQTools:JQFormColumn Alignment="left" Caption="交換碼" Editor="text" FieldName="Bourse" ReadOnly="True" Visible="True" Width="80" /><JQTools:JQFormColumn Alignment="left" Caption="銀行代碼" Editor="text" FieldName="BankID" ReadOnly="True" Visible="False" Width="80" /><JQTools:JQFormColumn Alignment="left" Caption="支票帳號" Editor="text" FieldName="CheckAccount" Visible="True" Width="300" NewRow="False" /><JQTools:JQFormColumn Alignment="left" Caption="支票銀行帳戶" Editor="inforefval" EditorOptions="title:'支票銀行帳戶',panelWidth:300,panelHeight:320,remoteName:'sERP_Normal_Warrant.CheckAccount',tableName:'CheckAccount',columns:[{field:'CheckAccountID',title:'帳戶代號',width:80,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'CheckAccountName',title:'帳戶名稱',width:200,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'CheckAccountID',textField:'CheckAccountName',valueFieldCaption:'CheckAccountID',textFieldCaption:'CheckAccountName',cacheRelationText:false,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" FieldName="AccountID" Visible="True" Width="300" NewRow="False" /><JQTools:JQFormColumn Alignment="left" Caption="CustomerID" Editor="text" FieldName="CustomerID" Visible="False" Width="80" /><JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" /><JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" /><JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" /><JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" /></Columns><RelationColumns><JQTools:JQRelationColumn FieldName="WarrantNO" ParentFieldName="WarrantNO" /></RelationColumns></JQTools:JQDataForm><JQTools:JQAutoSeq ID="dataFormCheckAutoSeq" runat="server" BindingObjectID="dataFormCheck" FieldName="ItemNO" NumDig="1" /></JQTools:JQDialog></JQTools:JQTabItem>
                </JQTools:JQTab>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="WarrantNO" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="WarrantDate" RemoteMethod="False" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CompanyCustomerID" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="InsGroupID" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="OffsetTypeID" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="WarrantDate" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="PayWayID" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="RecAmount" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
        
    </form>
</body>
</html>
