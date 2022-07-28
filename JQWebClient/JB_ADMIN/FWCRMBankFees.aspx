<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FWCRMBankFees.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script>

        //========================================= ready ====================================================================================

        $(document).ready(function () {
            //預設年月
            var dt = new Date();
            var aDate = new Date();
            var date1 = $.jbjob.Date.DateFormat(aDate, 'yyyyMMdd').substring(0, 6);
            var date2 = $.jbjob.Date.DateFormat(aDate, 'yyyyMMdd').substring(0, 4);

            $("#YearMonth_Query").val(date1);
            $("#YearMonth2_Query").val(date2 + "12");
            //Grid隱藏
            for (var i = 1; i < 13; i++) {
                $('#dataGridView'+i).datagrid('getPanel').hide();
            }
           

        });

        //--------------------查詢條件的標籤Grid--------------------------
        function queryGrid(dg) {//查詢後添加固定條件
            if ($(dg).attr('id') == 'dataGridView0') {
                //查詢條件
                var result = [];

                var YearMonth = $('#YearMonth_Query').val();//費用年月
                var YearMonth2 = $('#YearMonth2_Query').val();//費用年月
                if (YearMonth != '') result.push("Left(Convert(nvarchar(10),Dateadd(month,1,Right(s.Description,10)),112),6) between '" + YearMonth + "' and '" + YearMonth2 + "'");

                $(dg).datagrid('setWhere', result.join(' and '));
            }
            //Grid清空
            for (var i = 1; i <= 12; i++) {               
                $("#dataGridView" + i).datagrid('loadData', []); //Grid清空資料     
            }
            
        }
        
        function BankexportGrid() {
            var YearMonth = $('#YearMonth_Query').val();//起始費用年月
            var YearMonth2 = $('#YearMonth2_Query').val();//結束費用年月

            var date = YearMonth.substr(0, 4) + '/' + YearMonth.substr(4, 2) + '/01';
            var date2 = YearMonth2.substr(0, 4) + '/' + YearMonth2.substr(4, 2) + '/01';

            var beginDate = date;
            var endDate = date2;
            var beginDateValidate = $.fn.datebox('parseDate', beginDate.replace(/\//g, '-'));
            var endDateValidate = $.fn.datebox('parseDate', endDate.replace(/\//g, '-'));
            if (beginDateValidate == "Invalid Date" || !$.jbIsDateStr(beginDate)) {
                alert('起始費用年月:' + YearMonth + '格式錯誤');
                $("#dataFormMasterBeginDate").datebox('textbox').focus();
                return false;
            }

            if (endDateValidate == "Invalid Date" || !$.jbIsDateStr(endDate)) {
                alert('結束費用年月:' + YearMonth2 + '格式錯誤');
                $("#dataFormMasterEndDate").datebox('textbox').focus();
                return false;
            }

            if (beginDate > endDate) {
                alert('起始費用年月 : ' + YearMonth + ' 需小於結束費用年月 : ' + YearMonth2);
                $("#dataFormMasterBeginDate").datebox('textbox').focus();
                return false;
            }
            var data = $("#dataGridView0").datagrid('getData');
            if (data.total == 0) {
                alert('無顯示資料');
                return false;
            }

            var Url = "";
            for (var i = 1; YearMonth <= YearMonth2; i++) {
                var aDate = new Date($.jbDateAdd('months', i-1, date));//開始日期明天
                var YM = $.jbjob.Date.DateFormat(aDate, 'yyyyMMdd').substring(0, 6);

                //先讓YearMonth+1個月
                var aDate2 = new Date($.jbDateAdd('months', i, date));
                YearMonth = $.jbjob.Date.DateFormat(aDate2, 'yyyyMMdd').substring(0, 6);

                //Set Grid Title
                $("#dataGridView" + i).datagrid('getPanel').panel('setTitle', YM);

                $("#dataGridView" + i).datagrid('setWhere', "Left(Convert(nvarchar(10),Dateadd(month,1,Right(s.Description,10)),112),6)='" + YM + "'");
                if (i == 6) {
                    Url = Url + "<br><br>";
                }
                Url = Url + "&nbsp;&nbsp;&nbsp;<a href='javascript: void(0)' onclick='downExcel(" + i + ");' style='color:red;text-decoration: underline'>" + YM.toString() + "</a>";
                //exportGrid($("#dataGridView" + i));
            }
            Url = Url + "<br><br>&nbsp;&nbsp;&nbsp;<a href='#' class='easyui-linkbutton' OnClick='UpdateStatus()'>作業完成</a>";
            $.messager.alert('檔案下載', Url, '');

        }
       
        function downExcel(i) {
            exportGrid($("#dataGridView" + i));
        }
     
        function UpdateStatus() {
            var pre = confirm("確定?");
            if (pre == true) {
                var YearMonth = $('#YearMonth_Query').val();//起始費用年月
                var YearMonth2 = $('#YearMonth2_Query').val();//結束費用年月

                    $.ajax({
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=sPowerData.infoBankFees', //連接的Server端，command
                        data: "mode=method&method=" + "UpdateFeeIsExcel" + "&parameters=" + YearMonth + "," + YearMonth2, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                        cache: false,
                        async: false,
                        success: function (data) {
                            if (data != false) {
                                cnt = data;
                            }
                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            alert(xhr.status);
                            alert(thrownError);
                        }
                    });

                queryGrid($('#dataGridView0'));
                alert('執行完成');

            }
        }
           
        

        


    </script>



</head>
<body>
    <form id="form1" runat="server">
        <div>
         
            
  
                <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />

                <JQTools:JQDataGrid ID="dataGridView0" data-options="pagination:true,view:commandview" RemoteName="sPowerData.infoBankFees" runat="server" AutoApply="True"
                    DataMember="infoBankFees" Pagination="True" QueryTitle="" EditDialogID=""
                    Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="960px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="EmployeeID" Editor="text" FieldName="EmployeeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="100" EditorOptions="">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="收費年月" Editor="text" FieldName="sYearMonth" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="客戶編號" Editor="text" FieldName="ResidenceID" Format="" Visible="True" Width="100" />
                        <JQTools:JQGridColumn Alignment="left" Caption="姓名" Editor="text" FieldName="EmployeeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="地址" Editor="text" FieldName="Addr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="電子郵件(非必填)" Editor="text" FieldName="eMail" Format="" MaxLength="0" Visible="true" Width="50" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="服務費" Editor="text" FieldName="FeeAmount" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="體檢費" Editor="text" FieldName="FeeAmount2" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="居留證費" Editor="text" FieldName="FeeAmount3" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="公告欄1" Editor="text" FieldName="sTimeLeave" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="公告欄2" Editor="text" FieldName="Description" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="140">
                        </JQTools:JQGridColumn>
                    </Columns>
                        <TooItems>
                        <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="BankexportGrid" Text="Excel輸出" />
                        </TooItems>

                    <QueryColumns>
                        <JQTools:JQQueryColumn AndOr="and" Caption="費用年月" Condition="=" DataType="string" Editor="text" FieldName="YearMonth" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="60" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="～" Condition="=" DataType="string" Editor="text" FieldName="YearMonth2" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="60" />
                    </QueryColumns>
                </JQTools:JQDataGrid>
          


          
                <JQTools:JQDataGrid ID="dataGridView1" data-options="pagination:true,view:commandview" RemoteName="sPowerData.infoBankFees" runat="server" AutoApply="True"
                    DataMember="infoBankFees" Pagination="True" QueryTitle="" EditDialogID=""
                    Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="960px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="EmployeeID" Editor="text" FieldName="EmployeeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="100" EditorOptions="">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="客戶編號" Editor="text" FieldName="ResidenceID" Format="" Visible="True" Width="100" />
                        <JQTools:JQGridColumn Alignment="left" Caption="姓名" Editor="text" FieldName="EmployeeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="地址" Editor="text" FieldName="Addr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="電子郵件(非必填)" Editor="text" FieldName="eMail" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="服務費" Editor="text" FieldName="FeeAmount" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="其他" Editor="text" FieldName="FeeAmount2" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="其他1" Editor="text" FieldName="FeeAmount3" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="公告欄1" Editor="text" FieldName="sTimeLeave" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="公告欄2" Editor="text" FieldName="Description" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="140">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄3" Editor="text" FieldName="Description3" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄4" Editor="text" FieldName="Description4" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄5" Editor="text" FieldName="Description5" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                    </Columns>

                </JQTools:JQDataGrid>
          


          
                <JQTools:JQDataGrid ID="dataGridView2" data-options="pagination:true,view:commandview" RemoteName="sPowerData.infoBankFees" runat="server" AutoApply="True"
                    DataMember="infoBankFees" Pagination="True" QueryTitle="" EditDialogID=""
                    Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="960px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="EmployeeID" Editor="text" FieldName="EmployeeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="100" EditorOptions="">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="客戶編號" Editor="text" FieldName="ResidenceID" Format="" Visible="True" Width="100" />
                        <JQTools:JQGridColumn Alignment="left" Caption="姓名" Editor="text" FieldName="EmployeeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="地址" Editor="text" FieldName="Addr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="電子郵件(非必填)" Editor="text" FieldName="eMail" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="服務費" Editor="text" FieldName="FeeAmount" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="其他" Editor="text" FieldName="FeeAmount2" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="其他1" Editor="text" FieldName="FeeAmount3" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="公告欄1" Editor="text" FieldName="sTimeLeave" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="公告欄2" Editor="text" FieldName="Description" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="140">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄3" Editor="text" FieldName="Description3" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄4" Editor="text" FieldName="Description4" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄5" Editor="text" FieldName="Description5" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>

                    </Columns>

                </JQTools:JQDataGrid>
          


          
                <JQTools:JQDataGrid ID="dataGridView3" data-options="pagination:true,view:commandview" RemoteName="sPowerData.infoBankFees" runat="server" AutoApply="True"
                    DataMember="infoBankFees" Pagination="True" QueryTitle="" EditDialogID=""
                    Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="960px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="EmployeeID" Editor="text" FieldName="EmployeeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="100" EditorOptions="">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="客戶編號" Editor="text" FieldName="ResidenceID" Format="" Visible="True" Width="100" />
                        <JQTools:JQGridColumn Alignment="left" Caption="姓名" Editor="text" FieldName="EmployeeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="地址" Editor="text" FieldName="Addr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="電子郵件(非必填)" Editor="text" FieldName="eMail" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="服務費" Editor="text" FieldName="FeeAmount" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="其他" Editor="text" FieldName="FeeAmount2" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="其他1" Editor="text" FieldName="FeeAmount3" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="公告欄1" Editor="text" FieldName="sTimeLeave" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="公告欄2" Editor="text" FieldName="Description" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="140">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄3" Editor="text" FieldName="Description3" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄4" Editor="text" FieldName="Description4" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄5" Editor="text" FieldName="Description5" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>

                    </Columns>

                </JQTools:JQDataGrid>
          


          
                <JQTools:JQDataGrid ID="dataGridView4" data-options="pagination:true,view:commandview" RemoteName="sPowerData.infoBankFees" runat="server" AutoApply="True"
                    DataMember="infoBankFees" Pagination="True" QueryTitle="" EditDialogID=""
                    Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="960px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="EmployeeID" Editor="text" FieldName="EmployeeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="100" EditorOptions="">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="客戶編號" Editor="text" FieldName="ResidenceID" Format="" Visible="True" Width="100" />
                        <JQTools:JQGridColumn Alignment="left" Caption="姓名" Editor="text" FieldName="EmployeeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="地址" Editor="text" FieldName="Addr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="電子郵件(非必填)" Editor="text" FieldName="eMail" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="服務費" Editor="text" FieldName="FeeAmount" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="其他" Editor="text" FieldName="FeeAmount2" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="其他1" Editor="text" FieldName="FeeAmount3" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="公告欄1" Editor="text" FieldName="sTimeLeave" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="公告欄2" Editor="text" FieldName="Description" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="140">
                        </JQTools:JQGridColumn>
                       <JQTools:JQGridColumn Alignment="left" Caption="公告欄3" Editor="text" FieldName="Description3" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄4" Editor="text" FieldName="Description4" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄5" Editor="text" FieldName="Description5" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>

                    </Columns>

                </JQTools:JQDataGrid>
          


          
                <JQTools:JQDataGrid ID="dataGridView5" data-options="pagination:true,view:commandview" RemoteName="sPowerData.infoBankFees" runat="server" AutoApply="True"
                    DataMember="infoBankFees" Pagination="True" QueryTitle="" EditDialogID=""
                    Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="960px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="EmployeeID" Editor="text" FieldName="EmployeeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="100" EditorOptions="">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="客戶編號" Editor="text" FieldName="ResidenceID" Format="" Visible="True" Width="100" />
                        <JQTools:JQGridColumn Alignment="left" Caption="姓名" Editor="text" FieldName="EmployeeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="地址" Editor="text" FieldName="Addr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="電子郵件(非必填)" Editor="text" FieldName="eMail" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="服務費" Editor="text" FieldName="FeeAmount" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="其他" Editor="text" FieldName="FeeAmount2" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="其他1" Editor="text" FieldName="FeeAmount3" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="公告欄1" Editor="text" FieldName="sTimeLeave" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="公告欄2" Editor="text" FieldName="Description" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="140">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄3" Editor="text" FieldName="Description3" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄4" Editor="text" FieldName="Description4" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄5" Editor="text" FieldName="Description5" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>

                    </Columns>

                </JQTools:JQDataGrid>
          


          
                <JQTools:JQDataGrid ID="dataGridView6" data-options="pagination:true,view:commandview" RemoteName="sPowerData.infoBankFees" runat="server" AutoApply="True"
                    DataMember="infoBankFees" Pagination="True" QueryTitle="" EditDialogID=""
                    Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="960px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="EmployeeID" Editor="text" FieldName="EmployeeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="100" EditorOptions="">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="客戶編號" Editor="text" FieldName="ResidenceID" Format="" Visible="True" Width="100" />
                        <JQTools:JQGridColumn Alignment="left" Caption="姓名" Editor="text" FieldName="EmployeeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="地址" Editor="text" FieldName="Addr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="電子郵件(非必填)" Editor="text" FieldName="eMail" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="服務費" Editor="text" FieldName="FeeAmount" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="其他" Editor="text" FieldName="FeeAmount2" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="其他1" Editor="text" FieldName="FeeAmount3" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="公告欄1" Editor="text" FieldName="sTimeLeave" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="公告欄2" Editor="text" FieldName="Description" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="140">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄3" Editor="text" FieldName="Description3" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄4" Editor="text" FieldName="Description4" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄5" Editor="text" FieldName="Description5" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                    </Columns>

                </JQTools:JQDataGrid>
          


          
                <JQTools:JQDataGrid ID="dataGridView7" data-options="pagination:true,view:commandview" RemoteName="sPowerData.infoBankFees" runat="server" AutoApply="True"
                    DataMember="infoBankFees" Pagination="True" QueryTitle="" EditDialogID=""
                    Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="960px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="EmployeeID" Editor="text" FieldName="EmployeeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="100" EditorOptions="">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="客戶編號" Editor="text" FieldName="ResidenceID" Format="" Visible="True" Width="100" />
                        <JQTools:JQGridColumn Alignment="left" Caption="姓名" Editor="text" FieldName="EmployeeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="地址" Editor="text" FieldName="Addr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="電子郵件(非必填)" Editor="text" FieldName="eMail" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="服務費" Editor="text" FieldName="FeeAmount" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="其他" Editor="text" FieldName="FeeAmount2" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="其他1" Editor="text" FieldName="FeeAmount3" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="公告欄1" Editor="text" FieldName="sTimeLeave" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="公告欄2" Editor="text" FieldName="Description" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="140">
                        </JQTools:JQGridColumn>
                         <JQTools:JQGridColumn Alignment="left" Caption="公告欄3" Editor="text" FieldName="Description3" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄4" Editor="text" FieldName="Description4" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄5" Editor="text" FieldName="Description5" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>

                    </Columns>

                </JQTools:JQDataGrid>
          


          
                <JQTools:JQDataGrid ID="dataGridView8" data-options="pagination:true,view:commandview" RemoteName="sPowerData.infoBankFees" runat="server" AutoApply="True"
                    DataMember="infoBankFees" Pagination="True" QueryTitle="" EditDialogID=""
                    Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="960px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="EmployeeID" Editor="text" FieldName="EmployeeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="100" EditorOptions="">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="客戶編號" Editor="text" FieldName="ResidenceID" Format="" Visible="True" Width="100" />
                        <JQTools:JQGridColumn Alignment="left" Caption="姓名" Editor="text" FieldName="EmployeeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="地址" Editor="text" FieldName="Addr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="電子郵件(非必填)" Editor="text" FieldName="eMail" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="服務費" Editor="text" FieldName="FeeAmount" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="其他" Editor="text" FieldName="FeeAmount2" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="其他1" Editor="text" FieldName="FeeAmount3" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="公告欄1" Editor="text" FieldName="sTimeLeave" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="公告欄2" Editor="text" FieldName="Description" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="140">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄3" Editor="text" FieldName="Description3" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄4" Editor="text" FieldName="Description4" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄5" Editor="text" FieldName="Description5" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>

                    </Columns>

                </JQTools:JQDataGrid>
          


          
                <JQTools:JQDataGrid ID="dataGridView9" data-options="pagination:true,view:commandview" RemoteName="sPowerData.infoBankFees" runat="server" AutoApply="True"
                    DataMember="infoBankFees" Pagination="True" QueryTitle="" EditDialogID=""
                    Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="960px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="EmployeeID" Editor="text" FieldName="EmployeeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="100" EditorOptions="">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="客戶編號" Editor="text" FieldName="ResidenceID" Format="" Visible="True" Width="100" />
                        <JQTools:JQGridColumn Alignment="left" Caption="姓名" Editor="text" FieldName="EmployeeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="地址" Editor="text" FieldName="Addr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="電子郵件(非必填)" Editor="text" FieldName="eMail" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="服務費" Editor="text" FieldName="FeeAmount" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="其他" Editor="text" FieldName="FeeAmount2" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="其他1" Editor="text" FieldName="FeeAmount3" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="公告欄1" Editor="text" FieldName="sTimeLeave" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="公告欄2" Editor="text" FieldName="Description" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="140">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄3" Editor="text" FieldName="Description3" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄4" Editor="text" FieldName="Description4" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄5" Editor="text" FieldName="Description5" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>

                    </Columns>

                </JQTools:JQDataGrid>
          


          
                <JQTools:JQDataGrid ID="dataGridView10" data-options="pagination:true,view:commandview" RemoteName="sPowerData.infoBankFees" runat="server" AutoApply="True"
                    DataMember="infoBankFees" Pagination="True" QueryTitle="" EditDialogID=""
                    Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="960px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="EmployeeID" Editor="text" FieldName="EmployeeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="100" EditorOptions="">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="客戶編號" Editor="text" FieldName="ResidenceID" Format="" Visible="True" Width="100" />
                        <JQTools:JQGridColumn Alignment="left" Caption="姓名" Editor="text" FieldName="EmployeeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="地址" Editor="text" FieldName="Addr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="電子郵件(非必填)" Editor="text" FieldName="eMail" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="服務費" Editor="text" FieldName="FeeAmount" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="其他" Editor="text" FieldName="FeeAmount2" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="其他1" Editor="text" FieldName="FeeAmount3" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="公告欄1" Editor="text" FieldName="sTimeLeave" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="公告欄2" Editor="text" FieldName="Description" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="140">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄3" Editor="text" FieldName="Description3" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄4" Editor="text" FieldName="Description4" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄5" Editor="text" FieldName="Description5" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>

                    </Columns>

                </JQTools:JQDataGrid>
          


          
                <JQTools:JQDataGrid ID="dataGridView11" data-options="pagination:true,view:commandview" RemoteName="sPowerData.infoBankFees" runat="server" AutoApply="True"
                    DataMember="infoBankFees" Pagination="True" QueryTitle="" EditDialogID=""
                    Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="960px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="EmployeeID" Editor="text" FieldName="EmployeeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="100" EditorOptions="">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="客戶編號" Editor="text" FieldName="ResidenceID" Format="" Visible="True" Width="100" />
                        <JQTools:JQGridColumn Alignment="left" Caption="姓名" Editor="text" FieldName="EmployeeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="地址" Editor="text" FieldName="Addr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="電子郵件(非必填)" Editor="text" FieldName="eMail" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="服務費" Editor="text" FieldName="FeeAmount" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="其他" Editor="text" FieldName="FeeAmount2" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="其他1" Editor="text" FieldName="FeeAmount3" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="公告欄1" Editor="text" FieldName="sTimeLeave" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="公告欄2" Editor="text" FieldName="Description" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="140">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄3" Editor="text" FieldName="Description3" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄4" Editor="text" FieldName="Description4" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄5" Editor="text" FieldName="Description5" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>

                    </Columns>

                </JQTools:JQDataGrid>
          


          
                <JQTools:JQDataGrid ID="dataGridView12" data-options="pagination:true,view:commandview" RemoteName="sPowerData.infoBankFees" runat="server" AutoApply="True"
                    DataMember="infoBankFees" Pagination="True" QueryTitle="" EditDialogID=""
                    Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="960px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="EmployeeID" Editor="text" FieldName="EmployeeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="100" EditorOptions="">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="客戶編號" Editor="text" FieldName="ResidenceID" Format="" Visible="True" Width="100" />
                        <JQTools:JQGridColumn Alignment="left" Caption="姓名" Editor="text" FieldName="EmployeeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="地址" Editor="text" FieldName="Addr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="電子郵件(非必填)" Editor="text" FieldName="eMail" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="服務費" Editor="text" FieldName="FeeAmount" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="其他" Editor="text" FieldName="FeeAmount2" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="其他1" Editor="text" FieldName="FeeAmount3" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="公告欄1" Editor="text" FieldName="sTimeLeave" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="公告欄2" Editor="text" FieldName="Description" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="140">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄3" Editor="text" FieldName="Description3" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄4" Editor="text" FieldName="Description4" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公告欄5" Editor="text" FieldName="Description5" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>

                    </Columns>

                </JQTools:JQDataGrid>
          


          
        </div>
    </form>
</body>
</html>
