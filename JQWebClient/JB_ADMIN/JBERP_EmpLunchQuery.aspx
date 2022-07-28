<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_EmpLunchQuery.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script>    
         
         $(document).ready(function () {
             //設定日期寬度
             $('#BeginDate_Query').datebox({
                 width: 90
             });
             $('#EndDate_Query').datebox({
                 width: 90
             });
             //設定日期預設值 => 上個月第一天與最後一天
             var date = new Date();
             var dNow = $.jbjob.Date.DateFormat(date, 'yyyy/MM/dd');//今天日期
             var FirstDate = new Date($.jbGetFirstDate(date));
             var dFirstDate = $.jbjob.Date.DateFormat(FirstDate, 'yyyy/MM/dd');//今天的月初日期
             var LastFirstDate = new Date($.jbDateAdd('months', -1, dFirstDate));
             var LastLastDate = new Date($.jbGetLastDate(LastFirstDate));
             var d1=$.jbjob.Date.DateFormat(LastFirstDate, 'yyyy/MM/dd');
             var d2=$.jbjob.Date.DateFormat(LastLastDate, 'yyyy/MM/dd');
             $("#BeginDate_Query").datebox('setValue', d1);
             $("#BeginDate_Query").val(d1);
             $("#EndDate_Query").datebox('setValue', d2);                         

         });

      function OnLoadSuccessGV() {
             //panel寬度調整
             var dgid = $('#dataGridMaster');
             var queryPanel = getInfolightOption(dgid).queryDialog;
             if (queryPanel)
                 $(queryPanel).panel('resize', { width: 750 });          
         }

         function queryGrid(dg) {//查詢後添加固定條件
             if ($(dg).attr('id') == 'dataGridMaster') {
                 //查詢條件
                 var result = [];
                 //起訖日期
                 var BeginDate = $('#BeginDate_Query').datebox('getValue');
                 var EndDate = $('#EndDate_Query').datebox('getValue');

                 ///起訖日期檢查
                 if (BeginDate == "Invalid Date" || !$.jbIsDateStr(BeginDate)) {
                     alert('起始日期:' + BeginDate + '格式錯誤');
                     $("#BeginDate_Query").datebox('textbox').focus();
                     return false;
                 }

                 if (EndDate == "Invalid Date" || !$.jbIsDateStr(EndDate)) {
                     alert('結束日期:' + EndDate + '格式錯誤');
                     $("#EndDate_Query").datebox('textbox').focus();
                     return false;
                 }

                 if (BeginDate > EndDate) {
                     alert('起始日期 : ' + BeginDate + ' 需小於結束日期 : ' + EndDate);                    
                     return false;
                 }
                 result.push("Price!=0");
                 if (BeginDate != '') result.push("adate between '" + BeginDate + "'" + " and " + "'" + EndDate + "'");
                 var UserID = $('#UserID_Query').val();//員工工號
                 if (UserID != '') result.push("EmpNum like '%" + UserID + "%'");
                 var NAME_C = $('#NAME_C_Query').val();//員工號碼                        
                 if (NAME_C != '') result.push("NAME_C like '%" + NAME_C + "%'");
                 $(dg).datagrid('setWhere', result.join(' and '));
                                
             }
         }

         function ApplyDateLink(value, row, index) {
             if (value != 0)
                 return "<a href='javascript: void(0)' onclick='LinkDelayDate(" + index + ");'>" + value + "</a>";
             else return value;
         }
         // open誤餐日期紀錄畫面 dialog
         function LinkDelayDate(index) {
             $("#dataGridMaster").datagrid('selectRow', index);
             var rows = $("#dataGridMaster").datagrid('getSelected');
             var EmpNum = rows.EmpNum;
             var BeginDate = $('#BeginDate_Query').datebox('getValue');
             var EndDate = $('#EndDate_Query').datebox('getValue');
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sERPDelayLunchQuery.ERPDelayLunchApply',  //連接的Server端，command
                 data: "mode=method&method=" + "JBePortalEmpOrderList" + "&parameters=" + BeginDate + "," + EndDate + "," + EmpNum,  //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入搜尋字串
                 cache: false,
                 async: true,
                 success: function (data) {
                     var rows = $.parseJSON(data);//將JSon轉會到Object類型提供給Grid顯示

                     if (rows.length > 0) {
                         //通過loadData方法清除掉原有Grid中的舊有資料並填補分頁資料
                         $('#dataGrid_DelayDate').datagrid({ loadFilter: pagerFilter, url: "" }).datagrid('loadData', rows);
                     } else {
                         $('#dataGrid_DelayDate').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                     }

                 }
             });
             openForm('#Dialog_DelayDate', {}, 'viewed', 'dialog');

         }



       </script> 
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sERPDelayLunchQuery.infoEmpOrder" runat="server" AutoApply="True"
                DataMember="infoEmpOrder" Pagination="True" QueryTitle="Query"
                Title="訂餐明細表" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="450px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="員工編號" Editor="text" FieldName="EmpNum" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="員工姓名" Editor="text" FieldName="NAME_C" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="薪資代碼" Editor="text" FieldName="SalaryID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="金額" Editor="numberbox" FieldName="Price" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="100" Total="" FormatScript="ApplyDateLink">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>      
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" Enabled="True" />           
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="起始日期" Condition="&gt;=" DataType="datetime" Editor="datebox" FieldName="BeginDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="75" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="結束日期" Condition="&lt;=" DataType="datetime" Editor="datebox" FieldName="EndDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="75" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="員工工號" Condition="%" DataType="string" Editor="text" FieldName="UserID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="65" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="員工姓名" Condition="%" DataType="string" Editor="text" FieldName="NAME_C" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="65" />
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>
    <JQTools:JQDefault runat="server" BindingObjectID="dataGridMaster" BorderStyle="NotSet" Enabled="True" EnableTheming="True" ClientIDMode="Inherit" ID="defaultMaster" EnableViewState="True" ViewStateMode="Inherit" >
</JQTools:JQDefault>
<JQTools:JQValidate runat="server" BindingObjectID="dataGridMaster" EnableTheming="True" ID="validateMaster" >
</JQTools:JQValidate>

            <JQTools:JQDialog ID="Dialog_DelayDate" runat="server" BindingObjectID="" Title="誤餐費明細" ShowSubmitDiv="False" DialogLeft="300px" DialogTop="100px" Width="380px" Height="430px">
                        <JQTools:JQDataGrid ID="dataGrid_DelayDate" data-options="pagination:true,view:commandview" RemoteName="sERPDelayLunchQuery.infoEmpOrder" runat="server" AutoApply="True"
                DataMember="infoEmpOrder" Pagination="False" QueryTitle="查詢條件" EditDialogID="JQDialog1"
                Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="15,30,45,60" PageSize="15" QueryAutoColumn="False" QueryLeft="80px" QueryMode="Panel" QueryTop="80px" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnLoadSuccess="OnLoadSuccessGV">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="訂購日期" Editor="datebox" FieldName="adate" Visible="True" Width="99">
                    </JQTools:JQGridColumn>   
                    <JQTools:JQGridColumn Alignment="center" Caption="金額" Editor="text" FieldName="Price" Visible="True" Width="90" Total="">
                    </JQTools:JQGridColumn>                  
                </Columns>                
            </JQTools:JQDataGrid>
            </JQTools:JQDialog>

</form>
</body>
</html>
