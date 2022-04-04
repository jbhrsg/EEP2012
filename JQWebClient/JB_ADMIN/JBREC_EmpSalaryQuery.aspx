<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBREC_EmpSalaryQuery.aspx.cs" Inherits="Template_JQuerySingle1" %>

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
         }
         // 動態取得客戶 Combobox 資料-傳入1參數
         var GetCustomer = function (SYearMonth,EYearMonth) {
             var CodeList = GetDataFromMethod('GetCustomerID', { SYearMonth: SYearMonth, EYearMonth: EYearMonth });
             if (CodeList != null) $("#CustomerID_Query").combobox('loadData', CodeList);
         }
         // 動態取得薪資項目 Combobox 資料-傳入1參數
         var GetSalaryItem = function (SYearMonth, EYearMonth) {
             var CodeList = GetDataFromMethod('GetSaryID', { SYearMonth: SYearMonth, EYearMonth: EYearMonth });
             if (CodeList != null) $("#SaryID_Query").combobox('loadData', CodeList);
         }
         // 動態取得客戶 Combobox 資料-傳入3參數
         var GetCustomer_Period = function (SYearMonth,EYearMonth) {
             var CodeList = GetDataFromMethod('GetCustomerID_Period', { SYearMonth: SYearMonth, EYearMonth: EYearMonth });
             if (CodeList != null) $("#CustomerID_Query").combobox('loadData', CodeList);
         }
         // 動態取得薪資項目 Combobox 資料-傳入3參數
         var GetSalaryItem_Period = function (SYearMonth, EYearMonth, CustomerID) {
             var CodeList = GetDataFromMethod('GetSaryID_Period', { SYearMonth: SYearMonth , EYearMonth: EYearMonth, CustomerID: CustomerID });
             if (CodeList != null) $("#SaryID_Query").combobox('loadData', CodeList);
         }
         // 動態取得薪資項目 Grid 資料-傳入3參數
         //var GetSalaryItem_Select = function (SYearMonth, EYearMonth, CustomerID) {
         //    var CodeList = GetDataFromMethod('GetSaryID_Select', { SYearMonth: SYearMonth, EYearMonth: EYearMonth, CustomerID: CustomerID });
         //    if (CodeList != null) {
         //        $('#dataGridView').datagrid('loadData', CodeList);
         //        $("#JQDialog2").dialog('open');
         //    }
         //}

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
             $("#SaryID_Query").combobox('clear');
             GetCustomer(YearMonthS,YearMonthE);
             GetSalaryItem(YearMonthS,YearMonthE);
             $('#YearMonthE_Query').combobox('setValue',YearMonthS);
         }
         var YearMonth1OnSelect = function (rowData) {
             YearMonthS = $('#YearMonthS_Query').combobox('getValue');
             YearMonthE = $('#YearMonthE_Query').combobox('getValue');
             var CustomerID = ''
             $("#CustomerID_Query").combobox('clear');
             $("#SaryID_Query").combobox('clear');
             GetCustomer_Period(YearMonthS,YearMonthE);
             GetSalaryItem(YearMonthS,YearMonthE);

         }
         var CustomerIDOnSelect = function (rowData) {
             $("#SaryID_Query").combobox('clear');
             CustomerID = $("#CustomerID_Query").combobox('getValue');
             YearMonthS = $('#YearMonthS_Query').combobox('getValue');
             YearMonthE = $('#YearMonthE_Query').combobox('getValue');
             GetSalaryItem_Period(YearMonthS,YearMonthE, CustomerID);
      
         }
         function QSaryIDOnSelect() {
             SaryIDStr = ''
             QSaryID = $("#SaryID_Query").combobox('getValue');
             if (QSaryID == '0000') {
                 CustomerID = $("#CustomerID_Query").combobox('getValue');
                 SYearMonth = $('#YearMonthS_Query').combobox('getValue');
                 EYearMonth = $('#YearMonthE_Query').combobox('getValue');
                 $.ajax({
                     type: "POST",
                     url: '../handler/jqDataHandle.ashx?RemoteName=sJBRecruitEmpSalary',
                     data: "mode=method&method=" + "GetSaryID_Select" + "&parameters=" + SYearMonth + "," + EYearMonth + "," + CustomerID,
                     cache: false,
                     async: false,
                     success: function (data) {
                         var rows = $.parseJSON(data);
                         if (rows.length == 0) {
                             $('#JQDataGrid1').datagrid('loadData', []);//清空Grid資料
                         } else {
                             if (rows.length > 0) {
                                 $('#JQDataGrid1').datagrid({ loadFilter: pagerFilter, url: "" }).datagrid('loadData', rows);
                                 openForm('#JQDialog2', {}, 'viewed', 'dialog');
                             }
                         }
                     }
                 }
         );
             }
             else
                 SaryIDStr = '(' + "'" + QSaryID + "'" + ')';

  }
         function JQDialog2OnSubmited() {
             var rows = $('#JQDataGrid1').datagrid("getChecked");
             var count = rows.length;
             if (count == 0) {
                 alert('注意!!未選取任何薪資項目,請選取');
                 return false;
             }
             var ContactIDStr = '';
             for (var i = 0; i <= rows.length - 1; i++) {
                 if (i > 0)
                     ContactIDStr = ContactIDStr + ',' + "'" + rows[i].SaryID + "'";
                 else
                     ContactIDStr = ContactIDStr + '(' + "'" + rows[0].SaryID + "'";
                }
                if (ContactIDStr != '') {
                    ContactIDStr = ContactIDStr + ")";
                }
                SaryIDStr = ContactIDStr;
                closeForm('#JQDialog2');
         }
         function queryGrid(dg) {
                 var result = [];
                 var aVal = '';
                 var aVal = $('#CustomerID_Query').combobox('getValue');
                 if (aVal != '')
                     result.push("CustomerID = '" + aVal + "'");
                 var aVal = $('#YearMonthS_Query').combobox('getValue');;
                 if (aVal != '')
                    result.push("YearMonth >= '" + aVal + "'");
                 var aVal = $('#YearMonthE_Query').combobox('getValue');
                 if (aVal != '')
                    result.push("YearMonth <= '" + aVal + "'");
                 if ( SaryIDStr != '')
                    result.push("SaryID in " + SaryIDStr + "");
                 var aVal = $('#SaryAmount_Query').val();
                if (aVal != '')
                   result.push("SaryAmount > '"+aVal+"'");
                 $(dg).datagrid('setWhere', result.join(' and '));
         }
         

     </script>
    
        
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sJBRecruitEmpSalary.View_EmployeeSalary" runat="server" AutoApply="False"
                DataMember="View_EmployeeSalary" Pagination="True" QueryTitle="派遣員工薪資查詢" EditDialogID="JQDialog1"
                Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="20,40,60" PageSize="20" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="" UpdateCommandVisible="False" ViewCommandVisible="True" OnLoadSuccess="dataGridViewLoadSucess">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustomerID" Format="" MaxLength="0" Visible="true" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustomerShortName" Format="" MaxLength="0" Visible="true" Width="150" />
                    <JQTools:JQGridColumn Alignment="left" Caption="年月" Editor="text" FieldName="YearMonth" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:jqgridcolumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="員工代號" Editor="text" FieldName="EmpID" Format="" MaxLength="0" Visible="False" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="身分證號" Editor="text" FieldName="IDNumber" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="員工姓名" Editor="text" FieldName="NameC" Format="" MaxLength="0" Visible="true" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="薪項名稱" Editor="text" FieldName="SaryName" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="金額" Editor="numberbox" FieldName="SaryAmount" Format="N" Visible="true" Width="90" Total="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="戶籍地址" Editor="text" FieldName="Addr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="280">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="SaryID" Editor="text" FieldName="SaryID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="起始薪資年月" Condition="&gt;=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'YearMonth',textField:'YearMonth',remoteName:'sJBRecruitEmpSalary.YearMonth',tableName:'YearMonth',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:YearMonthOnSelect,panelHeight:200" FieldName="YearMonthS" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="終止薪資年月" Condition="&lt;=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'YearMonth',textField:'YearMonth',remoteName:'sJBRecruitEmpSalary.YearMonth',tableName:'YearMonth',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:YearMonth1OnSelect,panelHeight:200" FieldName="YearMonthE" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,onSelect:CustomerIDOnSelect,panelHeight:200" FieldName="CustomerID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="150" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="薪資項目" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,onSelect:QSaryIDOnSelect,panelHeight:200" FieldName="SaryID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="薪項金額大於" Condition="&gt;" DataType="number" DefaultValue="0" Editor="text" FieldName="SaryAmount" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="派遣員工薪資查詢">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="View_EmployeeSalary" HorizontalColumnsCount="2" RemoteName="sJBRecruitEmpSalary.View_EmployeeSalary" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustomerID" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustomerName" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="員工代號" Editor="text" FieldName="EmpID" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="員工姓名" Editor="text" FieldName="NameC" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="年月" Editor="text" FieldName="YearMonth" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="薪項名稱" Editor="text" FieldName="SaryName" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="金額" Editor="numberbox" FieldName="SaryAmount" Format="N" Width="180" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
            <JQTools:JQDialog ID="JQDialog2" runat="server" DialogLeft="240px" DialogTop="30px" Title=" " Width="420px" OnSubmited="JQDialog2OnSubmited" Closed="True" ShowSubmitDiv="True" HorizontalAlign="Center">
            <JQTools:JQDataGrid ID="JQDataGrid1" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" AutoApply="True" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="SaryItem" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="False" MultiSelect="True" NotInitGrid="False" PageList="25,50,75,100" PageSize="25" Pagination="False" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sJBRecruitEmpSalary.SaryItem" RowNumbers="True" Title="選擇薪資項目" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="360px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="給付類別" Editor="text" FieldName="SaryPayTypeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="薪項代號" Editor="text" FieldName="SaryID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="薪項名稱" Editor="text" FieldName="SaryName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="SaryPayType" Editor="text" FieldName="SaryPayType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                    </JQTools:JQGridColumn>
                </Columns>
            </JQTools:JQDataGrid>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
