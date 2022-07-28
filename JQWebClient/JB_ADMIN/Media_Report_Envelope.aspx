<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Media_Report_Envelope.aspx.cs" Inherits="Template_JQueryQuery1" %>

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
            setTimeout(function () {
                clearQuery('#dataGridMaster');
            },800);

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
            //if (rows.length == 0) {
            //    alert('請先勾取客戶資料');
            //    return false;
            //} else {
            //    //開啟郵件類別視窗
            //    openForm('#MailTypeDialog', '', '', 'dialog');
            //}
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
            $("#dataGridPrintList").datagrid('setWhere', '1=0')
        }
        function OpenPrintDialog() {
            openForm('#PrintListDialog', '', '', 'dialog');
        }

        //郵件類別視窗，按下確認
        function PrintButton_OnClick() {
            var MailType = $("#MailTypeComboBox").combobox('getValue');
            if (MailType == undefined) {
                alert("請選擇郵件類別");
                return false;
            } else {
                closeForm('#MailTypeDialog');
            }
            //var rows = $('#dataGridMaster').datagrid('getChecked');
            var rows = $('#dataGridPrintList').datagrid('getData').rows;

            var aCustNO = [];

            for (var i = 0; i < rows.length; i++) {
                aCustNO.push(rows[i].CustNO);
            }
            var sCustNO = aCustNO.join('*');

            var url = "../JB_ADMIN/REPORT/Media/Media_Report_Envelope_RV.aspx?sCustNO=" + sCustNO+"&MailType="+MailType;

            var height = $(window).height() - 50;
            //var width = $(window).width() - 600;
            var width = 960;
            var dialog = $('<div/>')
            .dialog({
                draggable: false,
                modal: true,
                height: height,
                width: width,
                title: "信封(列印提醒:請選擇PDF檔下載後，PDF列印時，紙張格式請選擇ZZ)",
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
                //openForm('#PrintListDialog', '', '', 'dialog');
            }

            for (var i = 0; i < rows.length; i++) {
                var rowData = new Object();
                rowData['CustNO'] = rows[i].CustNO;
                rowData['CustName'] = rows[i].CustName;
                rowData['CustPost'] = rows[i].CustPost;
                rowData['CustAddr'] = rows[i].CustAddr;
                rowData['ContactA'] = rows[i].ContactA;
                $("#dataGridPrintList").datagrid("appendRow", rowData);
            }
            tempAlert(rows.length+'筆成功轉到待印清單', 1500);
        }
        //提示會自動關掉
        function tempAlert(msg, duration) {
            var el = document.createElement("div");
            el.setAttribute("style", "position:absolute;top:10%;left:2%;background-color:yellow;color:red");
            el.innerHTML = msg;
            setTimeout(function () {
                el.parentNode.removeChild(el);
            }, duration);
            document.body.appendChild(el);
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

        function queryGrid(dg) {
            //var whereString=($(dg).datagrid('getWhere'));
            var whereArr = []

            if ($("#CustNO_Query").combobox('getValue') != "") {
                whereArr.push("c.CustNO='"+$("#CustNO_Query").combobox('getValue')+"'");
            }
            if ($("#SalesID_Query").combobox('getValue') != "") {
                whereArr.push("c.SalesID='" + $("#SalesID_Query").combobox('getValue') + "'");
            }
            if ($("#IsPutInvoice_Query").combobox('getValue') != "") {
                whereArr.push("c.IsPutInvoice='" + $("#IsPutInvoice_Query").combobox('getValue') + "'");
            }
            if ($("#IsPutPaperInvoice_Query").combobox('getValue') != "") {
                whereArr.push("c.IsPutPaperInvoice='" + $("#IsPutPaperInvoice_Query").combobox('getValue') + "'");
            }
            if ($("#InvoiceYM_Query").combobox('getValue') != "") {

                whereArr.push("d.InvoiceYM='" + $("#InvoiceYM_Query").combobox('getValue') + "'" + " and m.IsActive=1 and d.IsActive=1");
            }
            if ($("#IsAcceptePaper_Query").combobox('getValue') != "") {
                if ($("#IsAcceptePaper_Query").combobox('getValue') != '4') {
                    whereArr.push("c.IsAcceptePaper='" + $("#IsAcceptePaper_Query").combobox('getValue') + "'");
                } else if ($("#IsAcceptePaper_Query").combobox('getValue') == '4') {
                    whereArr.push("c.IsAcceptePaper in ('1','3')");
                }
            }
            if ($("#sam_Query").combobox('getValue') != "") {
                if ($("#sam_Query").combobox('getValue') == '1') {
                    whereArr.push("(c.samA=1 or c.samB=1 or c.samC=1)");
                } else if ($("#sam_Query").combobox('getValue') == '0') {
                    whereArr.push("(samA !=1 and samB !=1 and samC !=1)");
                }
            }
            var whereStr=whereArr.join(" and ");
            $(dg).datagrid('setWhere', whereStr);
        }


        //開啟拜訪記錄表
        function VisitRecordBtn_OnClick() {
            
            //var rows = $('#dataGridMaster').datagrid('getChecked');
            var rows = $('#dataGridPrintList').datagrid('getData').rows;

            var aCustNO = [];

            for (var i = 0; i < rows.length; i++) {
                aCustNO.push(rows[i].CustNO);
            }
            var sCustNO = aCustNO.join('*');

            var url = "../JB_ADMIN/REPORT/Media/Media_Report_Envelope_VisitRecord_RV.aspx?sCustNO=" + sCustNO;

            var height = $(window).height() - 50;
            //var width = $(window).width() - 600;
            var width = 960;
            var dialog = $('<div/>')
            .dialog({
                draggable: false,
                modal: true,
                height: height,
                width: width,
                title: "客戶拜訪資料紀錄單",
                //maximizable: true                              
            });
            $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="100%"></iframe>').appendTo(dialog.find('.panel-body'));
            dialog.dialog('open');
        }

        //開啟拜訪客戶清單
        function VisitCustListBtn_OnClick() {

            //var rows = $('#dataGridMaster').datagrid('getChecked');
            var rows = $('#dataGridPrintList').datagrid('getData').rows;

            var aCustNO = [];

            for (var i = 0; i < rows.length; i++) {
                aCustNO.push($.trim(rows[i].CustNO));
            }
            var sCustNO = aCustNO.join('*');

            var url = "../JB_ADMIN/REPORT/Media/Media_Report_Envelope_VisitCustList_RV.aspx?sCustNO=" + sCustNO;

            var height = $(window).height() - 50;
            //var width = $(window).width() - 600;
            var width = 960;
            var dialog = $('<div/>')
            .dialog({
                draggable: false,
                modal: true,
                height: height,
                width: width,
                title: "拜訪客戶清單",
                //maximizable: true                              
            });
            $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="100%"></iframe>').appendTo(dialog.find('.panel-body'));
            dialog.dialog('open');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sMedia_Report_Envelope.ERPSalesDetails" runat="server" AutoApply="True"
                DataMember="ERPSalesDetails" Pagination="False" QueryTitle=""
                Title="客戶資料" AllowDelete="False" AllowInsert="False" AllowUpdate="False" QueryMode="Panel" AlwaysClose="True" AllowAdd="False" ViewCommandVisible="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="True" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" OnLoadSuccess="dataGridMaster_OnLoadSuccess">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustNO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustName" Format="" MaxLength="0" Width="150" />
                    <JQTools:JQGridColumn Alignment="left" Caption="郵遞區號" Editor="text" FieldName="CustPost" Format="" MaxLength="0" Width="50" />
                    <JQTools:JQGridColumn Alignment="left" Caption="地址" Editor="text" FieldName="CustAddr" Format="" MaxLength="0" Width="400" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人A" Editor="text" FieldName="ContactA" Format="" MaxLength="0" Width="80" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-view" ItemType="easyui-linkbutton"
                        OnClick="OpenMailTypeDialog" Text="產出信封" Visible="False" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-add" ItemType="easyui-linkbutton" OnClick="GoTodataGridPrintList" Text="轉到待印清單" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-view" ItemType="easyui-linkbutton" OnClick="OpenPrintDialog" Text="開啟待印清單" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶代號" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CustNO',textField:'CustNO',remoteName:'sMedia_Report_Envelope.ERPCustomers',tableName:'ERPCustomers',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CustNO" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" TableName="c" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="業務" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sMedia_Report_Envelope.ERPSalesMan',tableName:'ERPSalesMan',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" TableName="c" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="開立發票" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'1',text:'是',selected:'false'},{value:'0',text:'否',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="IsPutInvoice" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" TableName="c" Width="40" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="報紙發票" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'1',text:'是',selected:'false'},{value:'0',text:'否',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="IsPutPaperInvoice" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" TableName="c" Width="40" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="發票年月" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'InvoiceYM',textField:'InvoiceYM',remoteName:'sMedia_Report_Envelope.InvoiceYM',tableName:'InvoiceYM',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="InvoiceYM" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" TableName="d" Width="80" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="寄電子報" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ePaperCode',textField:'ePaperType',remoteName:'sMedia_Report_Envelope.ERPPaperType',tableName:'ERPPaperType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="IsAcceptePaper" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" TableName="c" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="開電子發票" Condition="=" DataType="number" Editor="infocombobox" EditorOptions="items:[{value:'1',text:'是',selected:'false'},{value:'0',text:'否',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="sam" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="40" />
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
                <JQTools:JQDataGrid ID="dataGridPrintList" runat="server" AllowAdd="False" AllowDelete="True" AllowInsert="False" AllowUpdate="False" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" data-options="pagination:true,view:commandview" DataMember="ERPSalesDetails" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="True" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="False" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTitle="查詢" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sMedia_Report_Envelope.ERPSalesDetails" RowNumbers="True" Title="待印清單" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustNO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustName" Format="" MaxLength="0" Width="150" />
                    <JQTools:JQGridColumn Alignment="left" Caption="郵遞區號" Editor="text" FieldName="CustPost" Format="" MaxLength="0" Width="50" />
                    <JQTools:JQGridColumn Alignment="left" Caption="地址" Editor="text" FieldName="CustAddr" Format="" MaxLength="0" Width="400" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人A" Editor="text" FieldName="ContactA" Format="" MaxLength="0" Width="80" />
                    </Columns>
                    <TooItems>
                        <JQTools:JQToolItem Enabled="True" Icon="icon-print" ItemType="easyui-linkbutton" OnClick="OpenMailTypeDialog" Text="列印信封" Visible="True" />
                        <JQTools:JQToolItem Enabled="True" Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="多筆刪除(要勾)" Visible="True" />
                        <JQTools:JQToolItem Enabled="True" Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="ClearButton_OnClick" Text="全部清除" Visible="True" />
                        <JQTools:JQToolItem Enabled="True" Icon="icon-print" ItemType="easyui-linkbutton" Text="列印客戶拜訪資料記錄單" Visible="True" ID="VisitRecordBtn" OnClick="VisitRecordBtn_OnClick" />
                        <JQTools:JQToolItem Enabled="True" Icon="icon-print" ItemType="easyui-linkbutton" Text="列印客戶基本資料明細表" Visible="True" ID="VisitCustListBtn" OnClick="VisitCustListBtn_OnClick" />
                    </TooItems>
                </JQTools:JQDataGrid>
                <br>
                <br>
                <br></br>
                <br></br>
                <center>
                    <JQTools:JQButton ID="CloseButton0" runat="server" OnClick="CloseButton0_OnClick" Text="關閉" />
                </center>
                <br>
                <br></br>
                <br></br>
                <br></br>
                </br>
                </br>
                </br>
            </JQTools:JQDialog>
    </form>
</body>
</html>
