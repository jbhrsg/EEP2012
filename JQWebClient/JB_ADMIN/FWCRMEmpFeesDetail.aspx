<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FWCRMEmpFeesDetail.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script>

        //========================================= ready ====================================================================================

        $(document).ready(function () {
            //預設發票年月
            var dt = new Date();
            var aDate = new Date($.jbDateAdd('months', -1, dt));//開始日期明天

            var date1 = $.jbjob.Date.DateFormat(aDate, 'yyyyMMdd').substring(0, 6);
            $("#YearMonth_Query").val(date1);

        });

        //--------------------查詢條件的標籤Grid--------------------------
        function queryGrid(dg) {//查詢後添加固定條件
            if ($(dg).attr('id') == 'dataGridView') {
                //查詢條件
                var CompanyID = $("#CompID_Query").combobox('getValue');            
                if (CompanyID != "") {
                    var result = [];
                    var CompID = $('#CompID_Query').combobox('getValue');//公司別
                    if (CompID != '') result.push("CompanyID = " + CompID);

                    var EmployerID = $('#EmployerID_Query').combobox('getValue');//雇主名稱
                    if (EmployerID != '') result.push("EmployerID = '" + EmployerID + "'");

                    var EmployeeTcName = $('#EmployeeTcName_Query').val();//外勞姓名
                    if (EmployeeTcName != '') result.push("EmployeeTcName like '%" + EmployeeTcName + "%'");

                    $(dg).datagrid('setWhere', result.join(' and '));

                }else alert('請選擇公司別。');
            }
        }
        //---------------------------------------呼叫Method---------------------------------------
        var GetDataFromMethod = function (methodName, data) {
            var returnValue = null;
            $.ajax({
                url: '../handler/JqDataHandle.ashx?RemoteName=sPowerData',
                data: { mode: 'method', method: methodName, parameters: $.toJSONString(data) },
                type: 'POST',
                async: false,
                success: function (data) { returnValue = $.parseJSON(data); },
                error: function (xhr, ajaxOptions, thrownError) { returnValue = null; }
            });
            return returnValue;
        };
        //---------------------------------------選公司別觸發---------------------------------
        var CompanyID_OnSelect = function (rowdata) {
            //影響
            GetEmployer("");//雇主
            $('#dataGridView').datagrid('loadData', []); //清空資料 

        }
        //得到雇主資料
        var GetEmployer = function (CompanyID) {
            //公司別
            var CompanyID = $("#CompID_Query").combobox('getValue');
            var CodeList = GetDataFromMethod('GetEmployer', { Company_ID: CompanyID });
            if (CodeList != null) {
                $("#EmployerID_Query").combobox('loadData', CodeList);//Detail
            }
        }

        //刪除外勞費用
        //=============================================取得登入者工號(是否有刪除權限)=========================================================================================
        
        function EmployeeFeesDetele() {
           
            if ($("#dataGridView").datagrid('getChecked').length == 0) {
                alert('請勾選需刪除外勞。');
            } else {
                var pre = confirm("確定刪除?");
                if (pre == true) {
                    var CompanyID = $('#CompID_Query').combobox('getValue');

                    var rows = $('#dataGridView').datagrid('getChecked');
                    var aEmpID = [];
                    var sEmpID = "";
                    for (var i = 0; i < rows.length; i++) {
                        aEmpID.push(rows[i].EmployeeID);
                    }
                    var sEmpID = aEmpID.join('*');

                    $.ajax({
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=sPowerData.infoEmp',
                        data: "mode=method&method=" + "DeleteEmpFees" + "&parameters=" + CompanyID + "," + sEmpID,
                        cache: false,
                        async: false,
                        success: function (data) {
                            //更新            
                            //$('#dataGridView').datagrid("reload");
                            queryGrid($('#dataGridView'));

                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            alert(xhr.status);
                            alert(thrownError);
                        }
                    });

                }

            }
            
           
        
        }
        


    </script>



</head>
<body>
    <form id="form1" runat="server">
        <div>
         

            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
           
      
            
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sPowerData.infoEmp" runat="server" AutoApply="False"
                DataMember="infoEmp" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="" AllowAdd="False" AllowDelete="True" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="True" NotInitGrid="False" PageList="20,40,60,80,100" PageSize="20" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Height="400px" Width="600px">
                <Columns>
<%--                    <JQTools:JQGridColumn Alignment="center" Caption="出生年" Editor="numberbox" FieldName="BirthYear" MaxLength="0" Visible="true" Width="60" Sortable="False" />--%>
                    <JQTools:JQGridColumn Alignment="center" Caption="雇主名稱" Editor="text" FieldName="EmployerName" Format="" Visible="True" Width="100" />
                    <JQTools:JQGridColumn Alignment="center" Caption="外勞工號" Editor="text" FieldName="EmployeeID" Format="" MaxLength="0" Visible="true" Width="85" Sortable="False" />
                    <JQTools:JQGridColumn Alignment="center" Caption="外勞姓名" Editor="text" FieldName="EmployeeTcName" Format="" MaxLength="0" Visible="true" Width="85" Sortable="False" />
                    <JQTools:JQGridColumn Alignment="center" Caption="國籍" Editor="text" FieldName="NationalityText" Format="" MaxLength="0" Visible="true" Width="70" Sortable="False" />
                    <JQTools:JQGridColumn Alignment="center" Caption="費用筆數" Editor="text" FieldName="ifCount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="公司別" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CompID',textField:'CompName',remoteName:'sPowerData.infoCompID',tableName:'infoCompID',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:CompanyID_OnSelect,panelHeight:200" FieldName="CompID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="費用年月" Condition="=" DataType="string" Editor="text" FieldName="YearMonth" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="60" />
                </QueryColumns>
            </JQTools:JQDataGrid>
          

        </div>
    </form>
</body>
</html>
