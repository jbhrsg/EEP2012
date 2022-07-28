<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_SalesToDoList.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <%--<style>
    .datagrid-header-row td[field='Notes'] .datagrid-cell span {
 color:red;
 } 
</style>--%>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
     <script>                                
         $(document).ready(function () {
             //呼叫查詢條件
             openForm('#Dialog_Query', {}, 'inserted', 'switch');
             $('#dataGridSalesMaster').datagrid('getPanel').hide();
             $('#dataGridSalesDetails').datagrid('getPanel').hide();
             $('#dataGridNextCallDate').datagrid('getPanel').hide();
             //初始化查詢條件
             var sDate = new Date();
             var Date1 = $.jbjob.Date.DateFormat(sDate, 'yyyy/MM/dd');
             var sDate2 = new Date($.jbDateAdd('days', -7, sDate));
             var Date2 = $.jbjob.Date.DateFormat(sDate2, 'yyyy/MM/dd');
             $("#dataFormMasterSDate").datebox('setValue', Date2);//開始日期
             $("#dataFormMasterEDate").datebox('setValue', Date1);//結束日期
             
             //用戶編號=>業務代號
             var UserID = getClientInfo("UserID");
             setTimeout(function () {
                 var data = $("#dataFormMasterSalesID").combobox('getData');
                 for (var i = 0; i < data.length; i++) {
                     if (data[i].SalesEmployeeID == UserID) {
                         $("#dataFormMasterSalesID").combobox('setValue', data[i].SalesID);
                     }
                 }
             }, 200);

             //設定 複訪紀錄 dialog
             initToDoNotesDialog();

             $("#dataFormMasterSourse").options('setValue', 3);
             gvQueryTemp();//查詢業務沒指定時的待辦
         });
         function gvQueryTemp() {
             dg = '#dataGridTemp';
             //查詢條件
             var result = [];
             var MinSalesDate = $('#dataFormMasterSDate').datebox('getValue');//開始日期
             var MaxSalesDate = $('#dataFormMasterEDate').datebox('getValue');//結束日期
                    
            if (MinSalesDate != '') result.push("NextCallDate >= '" + MinSalesDate + "'");
            if (MaxSalesDate != '') result.push("NextCallDate <= '" + MaxSalesDate + "'");

             $(dg).datagrid('setWhere', result.join(' and '));
             $(dg).datagrid('getPanel').show();

            


         }

         function gvQuery() {
             

             //查詢條件
             var result = [];
             var SalesID = $('#dataFormMasterSalesID').combobox('getValue');//業務代號
             var CustNO = $('#dataFormMasterCustNO').combobox('getValue');//客戶代號
             var MinSalesDate = $('#dataFormMasterSDate').datebox('getValue');//開始日期
             var MaxSalesDate = $('#dataFormMasterEDate').datebox('getValue');//結束日期
             var iSourse = $("#dataFormMasterSourse").options('getValue');//提醒類型	
             //選擇Grid
             var dg;
             if (iSourse == 1) {
                 dg = '#dataGridSalesMaster';
                 $('#dataGridSalesDetails').datagrid('getPanel').hide();
                 $('#dataGridNextCallDate').datagrid('getPanel').hide();
                 $('#dataGridTemp').datagrid('getPanel').hide();
             } else if (iSourse == 2) {
                 dg = '#dataGridSalesDetails';
                 $('#dataGridSalesMaster').datagrid('getPanel').hide();
                 $('#dataGridNextCallDate').datagrid('getPanel').hide();
                 $('#dataGridTemp').datagrid('getPanel').hide();
             } else if (iSourse == 3) {
                 dg = '#dataGridNextCallDate';
                 $('#dataGridSalesMaster').datagrid('getPanel').hide();
                 $('#dataGridSalesDetails').datagrid('getPanel').hide();
                 $('#dataGridTemp').datagrid('getPanel').hide();
             }

             //3.複訪日期	1.到期客戶	2.銷貨備註
             if (CustNO != '') result.push("m.CustNO = '" + CustNO + "'");

             if (iSourse == 1) {//到期客戶列表      
                 if (SalesID != '') result.push("SalesID = '" + SalesID + "'");
                if (MinSalesDate != '') result.push("MaxSalesDate >= '" + MinSalesDate + "'");
                if (MaxSalesDate != '') result.push("MaxSalesDate <= '" + MaxSalesDate + "'");

             } else if (iSourse == 2) {//銷貨備註提醒列表 
                 if (SalesID != '') result.push("SalesID = '" + SalesID + "'");
                 if (MinSalesDate != '') result.push("SalesDescrDate >= '" + MinSalesDate + "'");
                 if (MaxSalesDate != '') result.push("SalesDescrDate <= '" + MaxSalesDate + "'");

             } else if (iSourse == 3) {//複訪日期提醒列表 
                 if (SalesID != '') result.push("d.SalesID = '" + SalesID + "'");
                 if (MinSalesDate != '') result.push("NextCallDate >= '" + MinSalesDate + "'");
                 if (MaxSalesDate != '') result.push("NextCallDate <= '" + MaxSalesDate + "'");
             }

             $(dg).datagrid('setWhere', result.join(' and '));
             $(dg).datagrid('getPanel').show();

         }
         //提醒類型選擇
         function OnSelectSourse(val) {
             //	1.到期客戶	2.銷貨備註 3.複訪日期
             if (val == 3) {
                 var sDate = new Date();
                 var sDate2 = new Date($.jbDateAdd('days', -7, sDate));
                 var Date2 = $.jbjob.Date.DateFormat(sDate2, 'yyyy/MM/dd');
                 $("#dataFormMasterSDate").datebox('setValue', Date2);//開始日期
             } else {
                 $("#dataFormMasterSDate").datebox('setValue', "");//開始日期
             }
         }
        //天數提醒(主檔),是否失效(明細)CheckBox
        function genCheckBox(val) {
            if (val != "0")
                return "<input  type='checkbox' checked='true' />";
            else
                return "<input  type='checkbox' />";
        }
        function SalesMasterReload() {
            $('#dataGridSalesMaster').datagrid('reload');
        }
        function SalesDetailsReload() {
            $('#dataGridSalesDetails').datagrid('reload');
        }
        function NextCallDateReload() {
            $('#dataGridNextCallDate').datagrid('reload');
            // 新增下次複訪提醒(by Grid)
            AddCustomerToDoNotes();
        }
        function NextCallDateReload2() {
            $('#dataGridNextCallDate').datagrid('reload');
            // 新增下次複訪提醒(by dataform)
            AddCustomerToDoNotesData();
        }
        function TempReload() {
            $('#dataGridTemp').datagrid('reload');
            // 新增下次複訪提醒(by Grid - 業務為空時)
            AddCustomerToDoNotes2();
        }
         // 新增下次複訪提醒(by Grid)
        function AddCustomerToDoNotes() {
            var row = $('#dataGridNextCallDate').datagrid('getSelected');
            var CustNO = row.CustNO;//客戶代號            
            var NextCallDateAdd = row.NextCallDateAdd; //下次複訪日期   
            var NextCallTimeAdd = row.NextCallTimeAdd; //下次複訪時間 
            var PostType = row.ListContent;//客戶等級
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalesToDoList.infoCustomerToDoNotes', //連接的Server端，command
                data: "mode=method&method=" + "AddCustomerToDoNotes" + "&parameters=" + encodeURIComponent(CustNO + "," + NextCallDateAdd + "," + NextCallTimeAdd + "," + PostType), //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                cache: false,
                async: false,
                success: function (data) {
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });

        }
        // 新增下次複訪提醒2 (by dataform)
        function AddCustomerToDoNotesData() {
            var row = $('#dataGridNextCallDate').datagrid('getSelected');
            var CustNO = row.CustNO;//客戶代號            
            var NextCallDateAdd = $('#dataFormGridNextCallDateNextCallDateAdd').datebox('getValue') //下次複訪日期   
            var NextCallTimeAdd = $("#dataFormGridNextCallDateNextCallTimeAdd").combobox('getValue');//下次複訪時間
            var PostType = $("#dataFormGridNextCallDateListContent").combobox('getValue');//客戶等級
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalesToDoList.infoCustomerToDoNotes', //連接的Server端，command
                data: "mode=method&method=" + "AddCustomerToDoNotes" + "&parameters=" + encodeURIComponent(CustNO + "," + NextCallDateAdd + "," + NextCallTimeAdd + "," + PostType), //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                cache: false,
                async: false,
                success: function (data) {
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });

        }
        // 新增下次複訪提醒(by Grid)----Temp沒有業務的
        function AddCustomerToDoNotes2() {
            var row = $('#dataGridTemp').datagrid('getSelected');
            var CustNO = row.CustNO;//客戶代號            
            var NextCallDateAdd = row.NextCallDateAdd; //下次複訪日期   
            var NextCallTimeAdd = row.NextCallTimeAdd; //下次複訪時間 
            var PostType = row.ListContent;//客戶等級
            var SalesID = row.SalesName;//業務
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalesToDoList.infoCustomerToDoNotes', //連接的Server端，command
                data: "mode=method&method=" + "AddCustomerToDoNotesSalse" + "&parameters=" + encodeURIComponent(CustNO + "," + NextCallDateAdd + "," + NextCallTimeAdd + "," + PostType + "," + SalesID), //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                cache: false,
                async: false,
                success: function (data) {
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });

        }

         //呼叫客戶資料頁籤
        function OpenCust(val, row) {            
            return $('<a>', { href: '#', onclick: 'GoToDoList(' + val + ');', theData: row.CustNO }).linkbutton({ text: "<b><div style=\"color:Blue\">" + val + "</div></b>", plain: true })[0].outerHTML;            
         }
        function GoToDoList(val) {
            parent.addTab("客戶資料維護", "JB_ADMIN/JBERP_Customer.aspx?CustID=" + val);
        }

         //呼叫人力銀行網址
        function OpenBankUrl(val, row) {
            if (val != '' && val != null) {
                //return $('<a>', { href: '#', onclick: 'window.open("' + val + '","人力銀行網址");', theData: row.val }).linkbutton({ text: "<b><div style=\"color:Red\">" + val + "</div></b>", plain: true })[0].outerHTML;
                return $('<a>', { href: "#", onclick: 'window.open("' + val + '","人力銀行網址");', theData: row.val }).linkbutton({ text: "<img src=../js/themes/icons/BankUrl.png></a><b><div style=\"color:Red\"></div></b>", plain: true })[0].outerHTML;
            }
        }
           
         //複訪 dialog
        function initToDoNotesDialog() {
            $("#Dialog_ToDoNotes").dialog(
            {
                height: 400,
                width: 850,
                left: 100,
                top: 80,
                resizable: false,
                modal: true,
                title: "複訪紀錄",
                closed: true
            });
        };

         //複訪紀錄
        function ToDoNotesLink(value, row, index) {
            return $('<a>', { href: 'javascript:void(0)', name: 'ToDoNotesLink', onclick: 'OpenToDoNotes.call(this)', rowIndex: index }).linkbutton({ plain: false, text: '複訪紀錄' })[0].outerHTML
        }

         // open 複訪 dialog
        function OpenToDoNotes() {
            //alert(index)
            var index = $(this).attr('rowIndex');
            $("#dataGridNextCallDate").datagrid('selectRow', index);
            var rows = $("#dataGridNextCallDate").datagrid('getSelected');
            var CustNO = rows.CustNO;
            $("#dataGrid_ToDoNotes").datagrid('setWhere', "m.CustNO = '" + CustNO +"'");
            $("#Dialog_ToDoNotes").dialog("open");
        }

         //呼叫新增視窗, 複訪內容維護
        function OpenInsertToDoNotes() {
            openForm('#Dialog_NextCallDate', $('#dataGridNextCallDate').datagrid('getSelected'), "updated", 'dialog');
        }
         //複訪紀錄 => 複訪內容 tooltip
        function genToolTipNotes(val, rowData) {
            if (rowData.CustShortName != undefined) {
                return "<p title='" + rowData.Notes + "' style='margin:0px;'>" + val + "</p>";
            }
            else {
                return val;
            }
        }
         //tooltip
        function genToolTip(val, rowData) {
            if (rowData.CustShortName != undefined) {
                return "<p title='電話號碼1:" + rowData.CustTelNO + "  聯絡人1:" + rowData.ContactA + "  分機:" + rowData.ContactASubTel + "' style='margin:0px;color:red;'>" + val + "</p>";
            }
            else {
                return val;
            }
        }
        
       

    </script> 
    
</head>


<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDialog ID="Dialog_Query" runat="server" BindingObjectID="dataFormMaster" EditMode="Switch" Title="銷貨明細修改" DialogLeft="50px" DialogTop="20px" Width="750px" ShowSubmitDiv="False">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="infoQuery" HorizontalColumnsCount="2" RemoteName="sERPSalesToDoList.infoQuery" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶代號" Editor="infocombobox" FieldName="CustNO" Format="" Width="170" ReadOnly="False" Span="1" EditorOptions="valueField:'CustNO',textField:'CustShortName',remoteName:'sERPSalseDetails.infoCustomersAll',tableName:'infoCustomersAll',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="業務代號" Editor="infocombobox" FieldName="SalesID" Format="" MaxLength="0" Span="1" Width="120" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sERPSalseDetails.infoSalesMan',tableName:'infoSalesMan',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="開始日期	" Editor="datebox" FieldName="SDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="結束日期" Editor="datebox" FieldName="EDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="提醒類型" Editor="infooptions" FieldName="Sourse" MaxLength="0" Width="80" EditorOptions="title:'JQOptions',panelWidth:280,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:3,multiSelect:false,openDialog:false,selectAll:false,onSelect:OnSelectSourse,selectOnly:false,items:[{text:'複訪日期',value:'3'},{text:'到期客戶',value:'1'},{text:'銷貨備註',value:'2'}]" Span="2" />
                    </Columns>
                </JQTools:JQDataForm>
                <a href="#" class="easyui-linkbutton" OnClick="gvQuery()">查詢</a>
            </JQTools:JQDialog>
            <div>
                <JQTools:JQDataGrid ID="dataGridTemp" data-options="pagination:true,view:commandview" RemoteName="sERPSalesToDoList.infoTempToDoNotes" runat="server" AutoApply="False"
                DataMember="infoTempToDoNotes" Pagination="True" QueryTitle="" EditDialogID=""
                Title="無對應業務客戶" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" OnUpdated="TempReload" BufferView="False" NotInitGrid="False" RowNumbers="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="預計複訪日期" Editor="text" FieldName="NextCallDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="80" Format="yyyy/mm/dd">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="預計時間" Editor="text" FieldName="NextCallTime" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="同業刊登中" Editor="text" FieldName="SourceName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="69" Format="" EditorOptions="" />
                    <JQTools:JQGridColumn Alignment="center" Caption="客戶代號" Editor="text" FieldName="CustNO" Format="" MaxLength="0" Width="70" ReadOnly="True" FormatScript="OpenCust" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶簡稱" Editor="text" FieldName="CustShortName" Format="" MaxLength="0" Width="80" ReadOnly="True" FormatScript="genToolTip" />
                    <JQTools:JQGridColumn Alignment="center" Caption="網址" Editor="text" FieldName="HrBankUrl" FormatScript="OpenBankUrl" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="37">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="客戶等級" Editor="infocombobox" FieldName="ListContent" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="69" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sERPCustomerLite.infoPostType',tableName:'infoPostType',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="center" Caption="業務" Editor="infocombobox" FieldName="SalesName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="85" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sERPSalseDetails.infoSalesMan',tableName:'infoSalesMan',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="複訪內容" Editor="text" FieldName="Notes" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="230">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="下次複訪日期" Editor="datebox" FieldName="NextCallDateAdd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="88">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="下次複訪時間" Editor="infocombobox" FieldName="NextCallTimeAdd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="85" EditorOptions="valueField:'NextCallTime',textField:'NextCallTime',remoteName:'sERPSalesToDoList.infoNextCallTime',tableName:'infoNextCallTime',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="120" Format="" />
                    <JQTools:JQGridColumn Alignment="center" Caption="建立者" Editor="" FieldName="UpdateBy" Format="" Width="40" ReadOnly="True" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" Visible="False" />
                    <JQTools:JQGridColumn Alignment="center" Caption="建立日期" Editor="datebox" FieldName="UpateDate" Format="yyyy-mm-dd HH:MM" Width="94" ReadOnly="True" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" Visible="False" FormatScript="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="NotesCreateDate" Editor="datebox" FieldName="NotesCreateDate" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption=" " Editor="text" FieldName="CreateBy" FormatScript="ToDoNotesLink" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>                  
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="確定" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />  
                </TooItems>
            </JQTools:JQDataGrid>
                    </div>
            <div>
                <JQTools:JQDataGrid ID="dataGridNextCallDate" data-options="pagination:true,view:commandview" RemoteName="sERPSalesToDoList.infoCustomerToDoNotes" runat="server" AutoApply="False"
                DataMember="infoCustomerToDoNotes" Pagination="True" QueryTitle="" EditDialogID=""
                Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" OnUpdated="NextCallDateReload" BufferView="False" NotInitGrid="False" RowNumbers="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="預計複訪日期" Editor="text" FieldName="NextCallDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="83" Format="yyyy/mm/dd">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="預計時間" Editor="text" FieldName="NextCallTime" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="同業刊登中" Editor="text" FieldName="SourceName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="69" Format="" EditorOptions="" />
                    <JQTools:JQGridColumn Alignment="center" Caption="客戶代號" Editor="text" FieldName="CustNO" Format="" MaxLength="0" Width="70" ReadOnly="True" FormatScript="OpenCust" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶簡稱" Editor="text" FieldName="CustShortName" Format="" MaxLength="0" Width="90" ReadOnly="True" FormatScript="genToolTip" />
                    <JQTools:JQGridColumn Alignment="center" Caption="網址" Editor="text" FieldName="HrBankUrl" FormatScript="OpenBankUrl" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="37">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="客戶等級" Editor="infocombobox" FieldName="ListContent" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="69" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sERPCustomerLite.infoPostType',tableName:'infoPostType',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="center" Caption="業務" Editor="text" FieldName="SalesName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="55">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="複訪內容" Editor="text" FieldName="Notes" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="230">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="下次複訪日期" Editor="datebox" FieldName="NextCallDateAdd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="93">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="下次複訪時間" Editor="infocombobox" FieldName="NextCallTimeAdd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="92" EditorOptions="valueField:'NextCallTime',textField:'NextCallTime',remoteName:'sERPSalesToDoList.infoNextCallTime',tableName:'infoNextCallTime',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="120" Format="" />
                    <JQTools:JQGridColumn Alignment="center" Caption="建立者" Editor="" FieldName="UpdateBy" Format="" Width="40" ReadOnly="True" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" Visible="False" />
                    <JQTools:JQGridColumn Alignment="center" Caption="建立日期" Editor="datebox" FieldName="UpateDate" Format="yyyy-mm-dd HH:MM" Width="94" ReadOnly="True" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" Visible="False" FormatScript="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="NotesCreateDate" Editor="datebox" FieldName="NotesCreateDate" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption=" " Editor="text" FieldName="CreateBy" FormatScript="ToDoNotesLink" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>                  
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="確定" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />  
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="OpenInsertToDoNotes" Text="複訪內容維護" Enabled="True" Visible="True" />
                </TooItems>
            </JQTools:JQDataGrid>
                    </div>
                    <JQTools:JQDialog ID="Dialog_NextCallDate" runat="server" BindingObjectID="dataFormGridNextCallDate" EditMode="Dialog" Title="複訪內容維護" DialogLeft="100px" DialogTop="60px" Width="750px">
                                <JQTools:JQDataForm runat="server" ID="dataFormGridNextCallDate" RemoteName="sERPSalesToDoList.infoCustomerToDoNotes" DataMember="infoCustomerToDoNotes" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" HorizontalColumnsCount="4" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" ParentObjectID="dataGridNextCallDate" OnApplied="NextCallDateReload2" >
                                    <Columns>
                                        <JQTools:JQFormColumn Alignment="left" Caption="客戶代號" Editor="" FieldName="CustNO" ReadOnly="True" Visible="True" Width="80" NewRow="False" MaxLength="0" RowSpan="1" Span="1" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="客戶簡稱" Editor="" FieldName="CustShortName" ReadOnly="True" Visible="True" Width="170" NewRow="False" MaxLength="0" RowSpan="1" Span="1" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="客戶等級" Editor="infocombobox" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sERPCustomerLite.infoPostType',tableName:'infoPostType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ListContent" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="85" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="複訪內容 " Editor="textarea" EditorOptions="height:130" FieldName="Notes" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="400" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="下次複訪日期" Editor="datebox" FieldName="NextCallDateAdd" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="下次複訪時間" Editor="infocombobox" EditorOptions="valueField:'NextCallTime',textField:'NextCallTime',remoteName:'sERPSalesToDoList.infoNextCallTime',tableName:'infoNextCallTime',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="NextCallTimeAdd" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                    </Columns>
                                    <RelationColumns>
                                        <JQTools:JQRelationColumn FieldName="AutoKey" ParentFieldName="AutoKey" />
                                    </RelationColumns>
                                </JQTools:JQDataForm>                               
                                <JQTools:JQValidate ID="validateMaster0" runat="server" BindingObjectID="dataFormGridNextCallDate" EnableTheming="True">
                                    <Columns>
                                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Notes" RemoteMethod="True" ValidateMessage="複訪內容不可空白" ValidateType="None" />
                                        <JQTools:JQValidateColumn CheckNull="True" FieldName="NextCallDateAdd" RemoteMethod="True" ValidateMessage="下次複訪日期不可空白" ValidateType="None" CheckMethod="" />
                                    </Columns>
                                </JQTools:JQValidate>
                             </JQTools:JQDialog>
                                            <JQTools:JQValidate ID="validateGridNextCallDate" runat="server" BindingObjectID="dataGridNextCallDate" EnableTheming="True">
                                                <Columns>
                                                    <JQTools:JQValidateColumn CheckMethod="" CheckNull="True" FieldName="Notes" RemoteMethod="True" ValidateMessage="複訪內容不可空白！" ValidateType="None" />
                                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="NextCallDateAdd" RemoteMethod="True" ValidateMessage="下次複訪日期不可空白！" ValidateType="None" />
                                                </Columns>
                                            </JQTools:JQValidate>
                                            
                                            <JQTools:JQValidate ID="validateGridTemp" runat="server" BindingObjectID="dataGridTemp" EnableTheming="True">
                                                <Columns>
                                                    <JQTools:JQValidateColumn CheckMethod="" CheckNull="True" FieldName="Notes" RemoteMethod="True" ValidateMessage="複訪內容不可空白！" ValidateType="None" />
                                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="NextCallDateAdd" RemoteMethod="True" ValidateMessage="下次複訪日期不可空白！" ValidateType="None" />
                                                </Columns>
                                            </JQTools:JQValidate>
                                            
                    <JQTools:JQDataGrid ID="dataGridSalesMaster" data-options="pagination:true,view:commandview" RemoteName="sERPSalesToDoList.ERPSalesMaster" runat="server" AutoApply="True"
                DataMember="ERPSalesMaster" Pagination="True" QueryTitle="" EditDialogID=""
                Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="15" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnUpdated="SalesMasterReload">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustNO" Format="" MaxLength="0" Width="70" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶簡稱" Editor="text" FieldName="CustShortName" Format="" MaxLength="0" Width="75" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="刊登起始日期" Editor="text" FieldName="MinSalesDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="center" Caption="最後刊登日期" Editor="text" FieldName="MaxSalesDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="85" Format="yyyy/mm/dd" />
                    <JQTools:JQGridColumn Alignment="center" Caption="剩餘數" Editor="text" FieldName="UseQty" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="53" />
                    <JQTools:JQGridColumn Alignment="center" Caption="保留天數" Editor="text" FieldName="KeepDays" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="53" />
                    <JQTools:JQGridColumn Alignment="center" Caption="刊登提醒" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="KeepDaysAlert" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="53" FormatScript="genCheckBox" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesMasterNO" Editor="text" FieldName="SalesMasterNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesEmployeeID" Editor="text" FieldName="SalesEmployeeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡備註" Editor="text" FieldName="ContractDescr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="450" />
                </Columns>
                <TooItems>                  
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="確定" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />  
                </TooItems>
            </JQTools:JQDataGrid>
        </div>
                     <JQTools:JQDataGrid ID="dataGridSalesDetails" data-options="pagination:true,view:commandview" RemoteName="sERPSalesToDoList.ERPSalesDetails" runat="server" AutoApply="True"
                DataMember="ERPSalesDetails" Pagination="True" QueryTitle="" EditDialogID=""
                Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnUpdated="SalesDetailsReload">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="客戶代號" Editor="text" FieldName="CustNO" Format="" MaxLength="0" Width="70" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="客戶簡稱" Editor="text" FieldName="CustShortName" Format="" MaxLength="0" Width="80" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡備註" Editor="textarea" FieldName="ContractDescr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="550" />
                    <JQTools:JQGridColumn Alignment="center" Caption="備註提醒" Editor="checkbox" FieldName="SalesDescrAlert" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="53" EditorOptions="on:1,off:0" FormatScript="genCheckBox" />
                    <JQTools:JQGridColumn Alignment="center" Caption="備註提醒日期" Editor="datebox" FieldName="SalesDescrDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" Format="yyyy/mm/dd" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesID" Editor="text" FieldName="ItemSeq" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="120" Format="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesMasterNO" Editor="text" FieldName="SalesMasterNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesEmployeeID" Editor="text" FieldName="SalesEmployeeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                </Columns>
                <TooItems>                  
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="確定" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />  
                </TooItems>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="Dialog_ToDoNotes" runat="server" BindingObjectID="" Title="複訪記錄" ShowSubmitDiv="False" DialogLeft="300px" DialogTop="100px" Width="350px">
                        <JQTools:JQDataGrid ID="dataGrid_ToDoNotes" data-options="pagination:true,view:commandview" RemoteName="sERPSalesToDoList.infoCustomerToDoNotesList" runat="server" AutoApply="True"
                DataMember="infoCustomerToDoNotesList" Pagination="False" QueryTitle="查詢條件" EditDialogID="JQDialog1"
                Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="80px" QueryMode="Panel" QueryTop="80px" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="複訪日期" Editor="text" FieldName="NextCallDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="70" Format="yyyy/mm/dd">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="複訪時間" Editor="text" FieldName="NextCallTime" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="同業刊登中" Editor="text" FieldName="SourceName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="70" Format="" EditorOptions="" />
                    <JQTools:JQGridColumn Alignment="center" Caption="客戶等級" Editor="text" FieldName="ListContent" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="65" EditorOptions="" />
                    <JQTools:JQGridColumn Alignment="center" Caption="業務" Editor="text" FieldName="SalesName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="54">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="複訪內容" Editor="text" FieldName="Notes" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="200" EditorOptions="" FormatScript="genToolTipNotes">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="網址" Editor="text" FieldName="HrBankUrl" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="140" FormatScript="">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="120" Format="" />
                    <JQTools:JQGridColumn Alignment="center" Caption="建立者" Editor="" FieldName="UpdateBy" Format="" Width="40" ReadOnly="True" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" Visible="False" />
                    <JQTools:JQGridColumn Alignment="center" Caption="建立日期" Editor="datebox" FieldName="NotesCreateDate" Format="yyyy-mm-dd HH:MM" Width="94" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" >
                    </JQTools:JQGridColumn>
                </Columns>
            </JQTools:JQDataGrid>
                        <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster0" DialogLeft="180px" DialogTop="130px" Title="" Width="560px">
                            <JQTools:JQDataForm ID="dataFormMaster0" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="infoCustomerToDoNotes" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="1" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sERPSalesToDoList.infoCustomerToDoNotes" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                                <Columns>
                                    <JQTools:JQFormColumn Alignment="left" Caption="複訪內容" Editor="textarea" EditorOptions="height:200" FieldName="Notes" MaxLength="0" NewRow="False" OnBlur="OnBlurCustNO" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="400" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="建立時間" Editor="text" FieldName="NotesCreateDate" Format="yyyy/mm/dd" MaxLength="0" NewRow="False" OnBlur="OnBlurCustName" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                                </Columns>
                            </JQTools:JQDataForm>
                        </JQTools:JQDialog>
            </JQTools:JQDialog>

</form>
</body>
</html>
