<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ERP_Report_PrintEnvelope.aspx.cs" Inherits="Template_JQueryQuery1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
            var MailType;
            $(function () {
                $('.infosysbutton-q', '#querydataGridMaster').closest('td').attr({ 'align': 'middle' });
                $("#MailTypeDialog").find(".infosysbutton-s").hide();
                $("#PrintListDialog").find(".infosysbutton-s").hide();
                clearQuery('#dataGridMaster');
            });
            function dataGridMaster_OnLoadSuccess() {
                //單選(為了OnUpdate_dataGridDetail來停用結案列的編輯
                if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) {
                    $("#dataGridMaster").datagrid({
                        singleSelect: true,
                        selectOnCheck: false,
                        checkOnSelect: false
                    });
                }

                //為了取消預設第一列勾選
                setTimeout(function () {
                    $("#dataGridMaster").datagrid("unselectAll");
                }, 600);
            }

            //按下"列印信封"
            function OpenMailTypeDialog() {
                //var rows = $('#dataGridMaster').datagrid('getChecked');
                var rows = $('#dataGridPrintList').datagrid('getData');
                if (rows.total == 0) {
                    alert('請先加入待印清單');
                    return false;
                } else {
                    //開啟郵件類別視窗
                    openForm('#MailTypeDialog', '', '', 'dialog');
                }
            }

            function CloseButton_OnClick() {
                closeForm('#MailTypeDialog');
            }
            function CloseButton0_OnClick() {
                closeForm('#PrintListDialog');
            }
            function ClearButton_OnClick() {
                $("#dataGridPrintList").datagrid('setWhere','1=0')
            }
            function OpenPrintDialog() {
                openForm('#PrintListDialog', '', '', 'dialog');
            }

            //郵件類別視窗，按下確認
            function PrintButton_OnClick() {
                //var rows = $('#dataGridMaster').datagrid('getChecked');
                
                MailType = $("#MailTypeComboBox").combobox('getValue');
                if (MailType == undefined) {
                    alert("請選擇郵件類別");
                    return false;
                } else {
                    　closeForm('#MailTypeDialog');
                }

                var rows = $('#dataGridPrintList').datagrid('getData').rows;

                //var aCustomerName = [];
                //var aTelNO = [];
                //var aAddr_Desc = [];
                //var aZIPCode = [];
                //var aAccountClerk = [];
                
                var aCustomerID = [];
                var aSalesTypeID = [];

                for (var i = 0; i < rows.length; i++) {
                //    aCustomerName.push(rows[i].CustomerName );
                //    aTelNO.push(rows[i].TelNO );
                //    aAddr_Desc.push(rows[i].Addr_Desc);
                //    aZIPCode.push(rows[i].ZIPCode);
                    //    aAccountClerk.push(rows[i].AccountClerk);
                    aCustomerID.push(rows[i].CustomerID);
                    aSalesTypeID.push(rows[i].SalesTypeID);
                }
                var sCustomerID = aCustomerID.join('*');
                var sSalesTypeID = aSalesTypeID.join('*');

                //var sCustomerName = aCustomerName.join(',');
                //var sTelNO = aTelNO.join(',');
                //var sAddr_Desc = aAddr_Desc.join(',');
                //var sZIPCode = aZIPCode.join(',');
                //var sAccountClerk = aAccountClerk.join(',');

                var url = "../JB_ADMIN/REPORT/ERP/ERP_Report_PrintEnvelope_RV.aspx?CustomerID=" + sCustomerID + "&SalesTypeID=" + sSalesTypeID + "&MailType=" + MailType;

                var height = $(window).height() - 50;
                //var width = $(window).width() - 600;
                var width = 960;
                var dialog = $('<div/>')
                .dialog({
                    draggable: false,
                    modal: true,
                    height: height,
                    width: width,
                    title: "信封",
                    //maximizable: true                              
                });
                $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="100%"></iframe>').appendTo(dialog.find('.panel-body'));
                dialog.dialog('open');
            }

            //轉到列印清單
            function GoTodataGridPrintList() {
                var rows = $("#dataGridMaster").datagrid('getChecked');

                if (rows.length == 0) {
                    alert('請先勾取客戶資料');
                    return false;
                } else {
                    openForm('#PrintListDialog', '', '', 'dialog');
                }

                for (var i = 0; i < rows.length; i++) {
                    var rowData = new Object();
                    rowData['CustomerID'] = rows[i].CustomerID;
                    rowData['SalesTypeID'] = rows[i].SalesTypeID;

                   rowData['CustomerName'] = rows[i].CustomerName;
                   rowData['TelNO'] = rows[i].TelNO;
                   rowData['Addr_Desc'] = rows[i].Addr_Desc;
                   rowData['ZIPCode'] = rows[i].ZIPCode;
                   rowData['SalesTypeName'] = rows[i].SalesTypeName;
                   rowData['AccountClerk'] = rows[i].AccountClerk;
                    $("#dataGridPrintList").datagrid("appendRow", rowData);
                }
            }

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

        </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sERP_Report_PrintEnvelope.Customer" runat="server" AutoApply="True"
                DataMember="Customer" Pagination="False" QueryTitle=""
                Title="客戶資料" AllowDelete="False" AllowInsert="False" AllowUpdate="False" QueryMode="Panel" AlwaysClose="True" AllowAdd="False" ViewCommandVisible="False" MultiSelect="True" OnLoadSuccess="dataGridMaster_OnLoadSuccess" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="CustomerID" Editor="text" FieldName="CustomerID" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustomerName" Format="" MaxLength="0" Width="200" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="電話" Editor="text" FieldName="TelNO" Format="" MaxLength="0" Width="120" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="FaxNO" Editor="text" FieldName="FaxNO" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Addr_Country" Editor="text" FieldName="Addr_Country" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Addr_City" Editor="text" FieldName="Addr_City" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="地址" Editor="text" FieldName="Addr_Desc" Format="" MaxLength="0" Width="440" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="郵遞區號" Editor="text" FieldName="ZIPCode" Format="" MaxLength="0" Width="60" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="TaxNO" Editor="text" FieldName="TaxNO" Format="" MaxLength="0" Width="120" ReadOnly="True" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="DonateMark" Editor="numberbox" FieldName="DonateMark" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="NPOBAN" Editor="text" FieldName="NPOBAN" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="CustomerTypeID" Editor="numberbox" FieldName="CustomerTypeID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PersonInCharge" Editor="text" FieldName="PersonInCharge" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Employer" Editor="text" FieldName="Employer" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="銷貨類別" Editor="infocombobox" FieldName="SalesTypeID" Format="" MaxLength="0" Width="100" Visible="True" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERP_Report_PrintEnvelope.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="帳務人員" Editor="text" FieldName="AccountClerk" Format="" MaxLength="0" Width="80" ReadOnly="True" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Enabled="True" Icon="icon-print" ItemType="easyui-linkbutton" OnClick="GoTodataGridPrintList" Text="加入待印清單" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-view" ItemType="easyui-linkbutton" OnClick="OpenPrintDialog" Text="開啟待印清單" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="銷貨類別" Condition="=" DataType="string" Editor="infocombobox" FieldName="SalesTypeID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" TableName="cst" Width="150" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERP_Report_PrintEnvelope.SalesType',tableName:'SalesType',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶類別" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'1',text:'公司',selected:'false'},{value:'2',text:'個人',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CustomerTypeID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="50" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶" Condition="=" DataType="string" Editor="infocombobox" FieldName="CustomerID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" TableName="c" Width="125" EditorOptions="valueField:'CustomerID',textField:'CustomerName',remoteName:'sERP_Report_PrintEnvelope.QCustomerID',tableName:'QCustomerID',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="電話" Condition="=" DataType="string" Editor="infocombobox" FieldName="TelNO" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" TableName="c" Width="125" EditorOptions="valueField:'TelNO',textField:'TelNO',remoteName:'sERP_Report_PrintEnvelope.QTelNO',tableName:'QTelNO',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="發票日期起迄" Condition="&gt;=" DataType="string" Editor="datebox" FieldName="InvoiceDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" TableName="id" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="~" Condition="&lt;=" DataType="string" Editor="datebox" FieldName="InvoiceDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" TableName="id" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="Mail傳送" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'0',text:'否',selected:'false'},{value:'1',text:'是',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="MailSend" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" TableName="sm" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>

        <JQTools:JQDialog ID="MailTypeDialog" runat="server" BindingObjectID="" Title="">
            <JQTools:JQLabel ID="JQLabel1" runat="server" Text="郵件類別" />
            <JQTools:JQComboBox ID="MailTypeComboBox" runat="server">
                <Items>
                    <JQTools:JQComboItem Selected="False" Text="空白" Value="" />
                    <JQTools:JQComboItem Selected="False" Text="掛號" Value="掛號" />
                    <JQTools:JQComboItem Selected="False" Text="限時掛號" Value="限時掛號" />
                    <JQTools:JQComboItem Selected="False" Text="印刷品" Value="印刷品" />
                    <JQTools:JQComboItem Selected="False" Text="限時專送" Value="限時專送" />
                    <JQTools:JQComboItem Selected="False" Text="混合郵件" Value="混合郵件" />
                </Items>
            </JQTools:JQComboBox>

            <JQTools:JQButton ID="PrintButton" runat="server" OnClick="PrintButton_OnClick" Text="確定" />
            <JQTools:JQButton ID="CloseButton" runat="server" OnClick="CloseButton_OnClick" Text="關閉" />
            
        </JQTools:JQDialog>
        <JQTools:JQDialog ID="PrintListDialog" runat="server" DialogLeft="20px" DialogTop="10px" Title="" Width="1040px">
                <JQTools:JQDataGrid ID="dataGridPrintList" runat="server" AllowAdd="False" AllowDelete="True" AllowInsert="False" AllowUpdate="False" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" data-options="pagination:true,view:commandview" DataMember="Customer" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="True" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="False" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTitle="查詢" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sERP_Report_PrintEnvelope.Customer" RowNumbers="True" Title="待印清單" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="CustomerID" Editor="text" FieldName="CustomerID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustomerName" Format="" MaxLength="0" ReadOnly="True" Visible="True" Width="200" />
                        <JQTools:JQGridColumn Alignment="left" Caption="電話" Editor="text" FieldName="TelNO" Format="" MaxLength="0" ReadOnly="True" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="地址" Editor="text" FieldName="Addr_Desc" Format="" MaxLength="0" ReadOnly="True" Width="440" />
                        <JQTools:JQGridColumn Alignment="left" Caption="郵遞區號" Editor="text" FieldName="ZIPCode" Format="" MaxLength="0" ReadOnly="True" Visible="True" Width="60" />
                        <JQTools:JQGridColumn Alignment="left" Caption="帳務人員" Editor="text" FieldName="AccountClerk" Format="" MaxLength="0" ReadOnly="True" Visible="True" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="SalesTypeID" Editor="text" FieldName="SalesTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                    </Columns>
                    <TooItems>
                        <JQTools:JQToolItem Enabled="True" Icon="icon-print" ItemType="easyui-linkbutton" OnClick="OpenMailTypeDialog" Text="列印信封(不用勾)" Visible="True" />
                        <JQTools:JQToolItem Enabled="True" Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="多筆刪除(要勾)" Visible="True" />
                        <JQTools:JQToolItem Enabled="True" Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="ClearButton_OnClick" Text="全部清除" Visible="True" />
                    </TooItems>
                </JQTools:JQDataGrid>
                <br>
                </br>
                <center>
                <JQTools:JQButton ID="CloseButton0" runat="server" OnClick="CloseButton0_OnClick" Text="關閉" />
                </center>
            </JQTools:JQDialog>
    </form>
</body>
</html>
