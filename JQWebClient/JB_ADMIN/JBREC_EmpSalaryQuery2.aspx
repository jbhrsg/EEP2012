<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBREC_EmpSalaryQuery2.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script>
         var SaryIDStr = '';
         $(document).ready(function () {
             $(function () {
                 $("input, select, textarea").focus(function () {
                     $(this).css("background-color", "yellow");
                 });
                 $("input, select, textarea").blur(function () {
                     $(this).css("background-color", "white");
                 });
             });
             $('#CustomerID_Query').combobox('clear');
         });
         function dataGridViewLoadSucess() {

             //panel寬度調整
             var dgid = $('#dataGridView');
             var queryPanel = getInfolightOption(dgid).queryDialog;
             if (queryPanel)
                 $(queryPanel).panel('resize', { width: 750 });
             //Grid隱藏
             $('#dataGridView').datagrid('getPanel').hide();

         }
         // 動態取得客戶 Combobox 資料-傳入1參數
         var GetCustomer = function (SYearMonth,EYearMonth) {
             var CodeList = GetDataFromMethod('GetCustomerID', { SYearMonth: SYearMonth, EYearMonth: EYearMonth });
             if (CodeList != null) $("#CustomerID_Query").combobox('loadData', CodeList);
         }
         // 動態取得客戶 Combobox 資料-傳入3參數
         var GetCustomer_Period = function (SYearMonth,EYearMonth) {
             var CodeList = GetDataFromMethod('GetCustomerID_Period', { SYearMonth: SYearMonth, EYearMonth: EYearMonth });
             if (CodeList != null) $("#CustomerID_Query").combobox('loadData', CodeList);
         }

         // 呼叫 Method
         var GetDataFromMethod = function (methodName, data) {
             var returnValue = null;
             $.ajax({
                 url: '../handler/JqDataHandle.ashx?RemoteName=sJBRecruitEmpSalary',
                 data: { mode: 'method', method: methodName, parameters: $.toJSONString(data) },
                 type: 'POST',
                 async: false,
                 success: function (data) { returnValue = $.parseJSON(data); },
                 error: function (xhr, ajaxOptions, thrownError) { returnValue = null; }
             });
             return returnValue;
         };
         var YearMonthOnSelect = function (rowData) {
             var CustomerID = ''
             var YearMonthS = $('#YearMonthS_Query').combobox('getValue');
             var YearMonthE = YearMonthS;
             $("#CustomerID_Query").combobox('clear');
             //$("#SaryID_Query").combobox('clear');
             GetCustomer(YearMonthS,YearMonthE);
             //GetSalaryItem(YearMonthS,YearMonthE);
             $('#YearMonthE_Query').combobox('setValue',YearMonthS);
         }
         var YearMonth1OnSelect = function (rowData) {
             YearMonthS = $('#YearMonthS_Query').combobox('getValue');
             YearMonthE = $('#YearMonthE_Query').combobox('getValue');
             var CustomerID = ''
             //$("#CustomerID_Query").combobox('clear');
             //$("#SaryID_Query").combobox('clear');
             GetCustomer_Period(YearMonthS,YearMonthE);
             //GetSalaryItem(YearMonthS,YearMonthE);

         }
         var CustomerIDOnSelect = function (rowData) {
             $("#SaryID_Query").combobox('clear');
             CustomerID = $("#CustomerID_Query").combobox('getValue');
             YearMonthS = $('#YearMonthS_Query').combobox('getValue');
             YearMonthE = $('#YearMonthE_Query').combobox('getValue');
      
         }

         function queryGrid(dg) {
            var result = [];
            var CustomerID = $('#CustomerID_Query').combobox('getValue');
            var YearMonthS = $('#YearMonthS_Query').combobox('getValue');;
            var YearMonthE = $('#YearMonthE_Query').combobox('getValue');
            var CustomerShortName = $('#CustomerID_Query').combobox('getText');

            if (YearMonthS == "") {
                alert('請選擇薪資年月');
                return false;
            } else if (CustomerID == "") {
                alert('請選擇客戶');
                return false;
            } else {

                var url = "../JB_ADMIN/REPORT/JBRecruit/EmpSalaryReportView.aspx?CustomerID=" + CustomerID + "&YearMonthS=" + YearMonthS + "&YearMonthE=" + YearMonthE + "&CustomerShortName=" + CustomerShortName;

                var height = $(window).height() - 20;
                var width = $(window).width() - 20;
                var dialog = $('<div/>')
                .dialog({
                    draggable: false,
                    modal: true,
                    height: height,
                    width: width,
                    title: "Report",
                    //maximizable: true                              
                });
                $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="100%"></iframe>').appendTo(dialog.find('.panel-body'));
                dialog.dialog('open');
            }

         }
         


     </script>
    
        
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sJBRecruitEmpSalary2.View_EmployeeSalary" runat="server" AutoApply="False"
                DataMember="View_EmployeeSalary" Pagination="True" QueryTitle="" EditDialogID=""
                Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="20,40,60" PageSize="20" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="" UpdateCommandVisible="False" ViewCommandVisible="False" OnLoadSuccess="dataGridViewLoadSucess" Height="450px" Width="900px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustomerID" Format="" MaxLength="0" Visible="true" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustomerShortName" Format="" MaxLength="0" Visible="true" Width="150" />
                    <JQTools:JQGridColumn Alignment="center" Caption="年月" Editor="text" FieldName="YearMonth" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                    </JQTools:jqgridcolumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="員工代號" Editor="text" FieldName="EmpID" Format="" MaxLength="0" Visible="False" Width="70" />
                    <JQTools:JQGridColumn Alignment="center" Caption="員工姓名" Editor="text" FieldName="NameC" Format="" MaxLength="0" Visible="true" Width="70" />
                    <JQTools:JQGridColumn Alignment="center" Caption="到職日期" Editor="datebox" FieldName="EffectDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="離職日期" Editor="datebox" FieldName="QuitDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="薪資合計" Editor="numberbox" FieldName="PaySalary" Format="N" Visible="true" Width="80" Total="" />
                    <JQTools:JQGridColumn Alignment="right" Caption="勞保投保級距" Editor="numberbox" FieldName="LInsAmount" Format="N" Visible="true" Width="90" Total="" />
                    <JQTools:JQGridColumn Alignment="right" Caption="健保投保級距" Editor="numberbox" FieldName="HInsAmount" Format="N" Visible="true" Width="90" Total="" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="AutoExcel" Text="匯出Excel" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="起始薪資年月" Condition="&gt;=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'YearMonth',textField:'YearMonth',remoteName:'sJBRecruitEmpSalary.YearMonth',tableName:'YearMonth',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:YearMonthOnSelect,panelHeight:200" FieldName="YearMonthS" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="終止薪資年月" Condition="&lt;=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'YearMonth',textField:'YearMonth',remoteName:'sJBRecruitEmpSalary.YearMonth',tableName:'YearMonth',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:YearMonth1OnSelect,panelHeight:200" FieldName="YearMonthE" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,onSelect:CustomerIDOnSelect,panelHeight:200" FieldName="CustomerID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="150" />
                </QueryColumns>
            </JQTools:JQDataGrid>

        </div>
    </form>
</body>
</html>
