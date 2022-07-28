<%@ Page Language="C#" AutoEventWireup="true" CodeFile="glDescribe.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../css/JBGL/label.css" rel="stylesheet" />
    <title></title>
    <script> 

        //=========================================控制3個Grid1=0都做完才顯示Grid=============================================================
        var waitA = false;
        var waitB = false;

        var myVar = setInterval(function () { myTimer(); }, 100);
        setTimeout(function () { clearTimeout(myVar); }, 6000);//最多6秒結束

        function myTimer() {
            if (waitA == true && waitB == true) {
                RefreshDetailGrid();                             
                clearTimeout(myVar);
            }
        }
        //========================================= ready ====================================================================================
        $(document).ready(function () {
            //預設公司別
            $('#cbCompanyID').combobox('setValue', 0);

        });

        //Grid genCheckBox
        function GridCheckBox(val) {
            if (val != "0")
                return "<input  type='checkbox' checked='true' onclick='return false;'/>";
            else
                return "<input  type='checkbox' onclick='return false;'/>";
        }
       
        //============================================================主檔====================================================================        
        function OnLoadMaster() {
            if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) {
                //1=0做完
                waitA = true;
            }
        }
        //依據選取的Master更新Detail明細
        function OnSelectMaster(rowindex, rowdata) {
            RefreshDetail(rowdata);                      
        }
       
        //刪除Master檢查
        function CheckMasterDelete() {
            var ClassID = $("#dataGridView").datagrid('getSelected').ClassID;;//取得當前主檔中選中的那個Data
            var cnt;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sglAccountItem.glAccountClass', //連接的Server端，command
                data: "mode=method&method=" + "CheckMasterDelete" + "&parameters=" + ClassID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    cnt = data;
                }
            });
            if ((cnt == "0") || (cnt == "undefined")) {

                return true;
            }
            else {

                alert('此會計類別有會計科目參考使用,無法刪除!!');

                return false;
            }
        }

       
        //============================================================明細====================================================================
        //明細OnLoad
        function OnLoadDetail() {
            if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) {
                //1=0做完
                waitB = true;
            }
        }
        //更新Detail
        function RefreshDetailGrid() {

            $('#dataGridDetail').datagrid('loadData', []); //明細清空資料            
            var result = [];

            //會計科目
            var Acno = $("#dataGridView").datagrid('getSelected').Acno;;//取得當前主檔中選中的那個Data
            if (Acno != '') result.push("Acno=" + Acno);

            //公司別
            var CompanyID = $("#cbCompanyID").combobox('getValue');
            if (CompanyID != '') result.push("CompanyID= " + CompanyID);

            $("#dataGridDetail").datagrid('setWhere', result.join(' and '));


        }
        //選主檔=>更新明細
        function RefreshDetail(row) {
            if (row != null) {
                
                var result = [];

                //會計科目
                var Acno = row.Acno;
                if (Acno != '') result.push("Acno=" + Acno);

                //公司別
                var CompanyID = $("#cbCompanyID").combobox('getValue');
                if (CompanyID != '') result.push("CompanyID= " + CompanyID);

                $("#dataGridDetail").datagrid('setWhere', result.join(' and '));
            }
            else {
                $('#dataGridDetail').datagrid('loadData', []); //明細清空資料                
            }
        }        
        //新增明細(會計科目)=>取得 公司別
        function GetCompanyID() {
            return $("#cbCompanyID").combobox('getValue');
        }
        //新增明細(會計科目)=>取得 傳票類別
        function GetVoucherID() {
            return $("#opVoucherID").options('getValue');
        }
        //新增明細(會計科目)=>取得 科目 
        function GetAcno() {
            var row = $('#dataGridView').datagrid('getSelected');
            return row.Acno;
        }
        function queryGrid(dg) {//查詢後添加固定條件
            if ($(dg).attr('id') == 'dataGridDetail') {//GridDetail
                //查詢條件
                var result = [];
                var row = $('#dataGridView').datagrid('getSelected');
                var Acno = row.Acno;//科目
                var CompanyID = $("#cbCompanyID").combobox('getValue');//公司別                
                var VoucherID = $("#opVoucherID").options('getValue');//傳票類別
                var Describe = $("#Describe_Query").val();//摘碼內容 

                if (Acno != '') result.push("Acno = '" + Acno + "'");
                if (CompanyID != '') result.push("CompanyID = " + CompanyID);
                if (VoucherID != '') result.push("VoucherID = " + VoucherID);
                if (Describe != '') result.push("Describe like '%" + Describe + "%'");

                $(dg).datagrid('setWhere', result.join(' and '));
            }
        }
        //EditOnEnter銷貨明細檢查
        function OnUpdateDG() {
            var row = $("#dataGridDetail").datagrid('getSelected');
            var index = $("#dataGridDetail").datagrid('getRowIndex', row);
            if (index != undefined) {
                $("#dataGridDetail").datagrid('selectRow', index).datagrid('beginEdit', index);
                //var cellEdit = $("#dataGridDetail").datagrid('getEditor', { index: index, field: 'SalesQtyView' });
                var cells = $("#dataGridDetail").datagrid('getEditors', index);
                $.each(cells, function (index, obj) {
                    if (obj.field != "Describe") {
                        switch (obj.type) {
                            case "validatebox": $(obj.target).attr("disabled", "disabled");
                                break;
                        }
                    }
                });
            }
        }
    </script> 
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <div class="easyui-layout" style="height: 522px;">
                <div data-options="region:'west',split:true" style="width: 300px;" >
                    <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sglDescribe.glAccountItem" runat="server" AutoApply="True"
                        DataMember="glAccountItem" Pagination="False" QueryTitle="查詢" EditDialogID="JQDialog1"
                        Title="會計科目" QueryMode="Window" AllowAdd="True" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="15,20,30,40,50" PageSize="15" QueryAutoColumn="False" QueryLeft="10px" QueryTop="10px" RecordLock="False" RecordLockMode="None" RowNumbers="False" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnLoadSuccess="OnLoadMaster" OnSelect="OnSelectMaster" OnDelete="CheckMasterDelete">
                        <Columns>
                            <JQTools:JQGridColumn Alignment="center" Caption="科目" Editor="text" FieldName="Acno" Format="" Visible="True" Width="90" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                            <JQTools:JQGridColumn Alignment="left" Caption="科目名稱" Editor="text" FieldName="AcnoName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="170">
                            </JQTools:JQGridColumn>
                        </Columns>
                        <TooItems>                                                  
                            <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                                OnClick="openQuery" Text="查詢" Enabled="True" Visible="True" />
                        </TooItems>
                        <QueryColumns>
                            <JQTools:JQQueryColumn AndOr="and" Caption="科目" Condition="%%" DataType="string" Editor="text" EditorOptions="" FieldName="Acno" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                        </QueryColumns>
                    </JQTools:JQDataGrid>
                </div>
                <div data-options="region:'center'">
                    <div>
                    <table>
                        <tr>
                            <td>
                                <h5 id="CompanyID" class="h3_Caption">公司別</h5>
                            </td>
                            <td>
                                <JQTools:JQComboBox ID="cbCompanyID" runat="server" CheckData="False" DisplayMember="CompanyName" RemoteName="sglAccountItem.glCompanyAll" ValueMember="CompanyID" PanelHeight="120" OnSelect="RefreshDetailGrid">
                                </JQTools:JQComboBox>
                                </td>
                        </tr>
                        </table>
                    <JQTools:JQDataGrid ID="dataGridDetail" runat="server" DataMember="glDescribe" RemoteName="sglDescribe.glDescribe" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="14,20,30,40,50" PageSize="14" Pagination="False" QueryAutoColumn="False" QueryLeft="450px" QueryMode="Window" QueryTitle="科目摘要查詢" QueryTop="100px" RecordLock="False" RecordLockMode="None" RowNumbers="True" Title="科目摘要" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" ParentObjectID="" EditDialogID="" OnUpdate="OnUpdateDG" OnLoadSuccess="OnLoadDetail" >
                        <Columns>
                            <JQTools:JQGridColumn Alignment="left" Caption="公司別" Editor="text" FieldName="CompanyID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="center" Caption="科目" Editor="text" FieldName="Acno" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="65">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="center" Caption="摘碼代號" Editor="text" FieldName="DescribeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="90">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="left" Caption="摘碼內容" Editor="text" FieldName="Describe" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="688">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                            </JQTools:JQGridColumn>
                        </Columns>
                        <TooItems>
                            <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                                OnClick="insertItem" Text="新增" Enabled="True" Visible="True" />    
                            <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="存檔" Enabled="True" Visible="True" />
                            <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" Enabled="True" Visible="True"  />                       
                            <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                                OnClick="openQuery" Text="查詢" Enabled="True" Visible="True" />
                        </TooItems>
                        <QueryColumns>
                            <JQTools:JQQueryColumn AndOr="and" Caption="摘碼內容" Condition="%%" DataType="string" Editor="text" FieldName="Describe" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                        </QueryColumns>
                    </JQTools:JQDataGrid>
                        <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataGridDetail" EnableTheming="True">
                            <Columns>
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetAcno" FieldName="Acno" RemoteMethod="False" />
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetCompanyID" FieldName="CompanyID" RemoteMethod="False" />
                            </Columns>
                        </JQTools:JQDefault>
                        <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataGridDetail" EnableTheming="True">
                            <Columns>
                                <JQTools:JQValidateColumn CheckNull="True" FieldName="DescribeID" RemoteMethod="True" ValidateMessage="請輸入摘碼代號！" ValidateType="None" />
                                <JQTools:JQValidateColumn CheckNull="True" FieldName="Describe" RemoteMethod="True" ValidateMessage="請輸入摘碼內容！" ValidateType="None" />
                            </Columns>
                        </JQTools:JQValidate>
                    <JQTools:JQAutoSeq ID="JQAutoSeq1" runat="server" BindingObjectID="dataGridDetail" FieldName="DescribeID" NumDig="3" />
                </div>
           </div>
        </div>
        </div>
    </form>
</body>
</html>
