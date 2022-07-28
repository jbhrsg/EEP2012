<%@ Page Language="C#" AutoEventWireup="true" CodeFile="glAccountItem.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../css/JBGL/label.css" rel="stylesheet" />
    <title></title>
    <script> 

        //=========================================控制Grid1=0都做完才顯示Grid=============================================================
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

            //會計類別
            var ClassID = $("#dataGridView").datagrid('getSelected').ClassID;;//取得當前主檔中選中的那個Data
            if (ClassID != '') result.push("a.ClassID=" + ClassID);

            //公司別
            var CompanyID = $("#cbCompanyID").combobox('getValue');
            if (CompanyID != '') result.push("a.CompanyID= " + CompanyID);

            $("#dataGridDetail").datagrid('setWhere', result.join(' and '));


        }
        //選主檔=>更新明細
        function RefreshDetail(row) {
            if (row != null) {
                //var where = $(dg).datagrid('getWhere');                              
                var result = [];

                //會計類別
                var ClassID = row.ClassID;
                if (ClassID != '') result.push("a.ClassID=" + ClassID);

                //公司別
                var CompanyID = $("#cbCompanyID").combobox('getValue');
                if (CompanyID != '') result.push("a.CompanyID= " + CompanyID);

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
        
        //新增明細(會計科目)=>取得 類別代號
        function GetClassID() {
            var row = $('#dataGridView').datagrid('getSelected');
            return row.ClassID;//類別
        }
        function queryGrid(dg) {//查詢後添加固定條件
            if ($(dg).attr('id') == 'dataGridDetail') {//GridDetail
                //查詢條件
                var result = [];

                var row = $('#dataGridView').datagrid('getSelected');
                var ClassID = row.ClassID;//類別

                var CompanyID = $("#cbCompanyID").combobox('getValue');//公司別                
                var Acno = $("#Acno_Query").val();//科目
                var AcnoName = $("#AcnoName_Query").val();//科目名稱

                if (ClassID != '') result.push("a.ClassID = '" + ClassID + "'");
                if (CompanyID != '') result.push("a.CompanyID = " + CompanyID);
                if (Acno != '') result.push("a.Acno+Isnull(a.SubAcno,'') like '%" + Acno + "%'");
                if (AcnoName != '') result.push("a.AcnoName like '%" + AcnoName + "%'");

                $(dg).datagrid('setWhere', result.join(' and '));
            }
        }
        function OnApplydataFormDetail() {
            var CompanyID = $("#cbCompanyID").combobox('getValue');
            if (CompanyID == "0") {
                alert('請選擇公司別！');
                return false;
            }
            var Acno = $("#dataFormDetailAcno").val();//科目
            var SubAcno = $("#dataFormDetailSubAcno").val();//明細
            var iCount = CheckAcnoSubAcnoCount(CompanyID,Acno + SubAcno);

            if (getEditMode($("#dataFormDetail")) == 'inserted') {
                if (iCount > 0) {
                    alert('此科目明細重複！');
                    return false;
                }
            } else {
                if (iCount > 1) {
                    alert('此科目明細重複！');
                    return false;
                }
            }
        }
        //檢查科目+明細不可以重複
        function CheckAcnoSubAcnoCount(CompanyID,AcnoSubAcno) {
            var iCount;

            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sglAccountItem.glAccountClass', //連接的Server端，command
                data: "mode=method&method=" + "ReturnAcnoSubAcnoCount" + "&parameters=" + CompanyID + "*" + AcnoSubAcno,  //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    iCount = data
                },
            });
            return iCount;
        }

    </script> 
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <div class="easyui-layout" style="height: 522px;">
                <div data-options="region:'west',split:true" style="width: 409px;" >
                    <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sglAccountItem.glAccountClass" runat="server" AutoApply="True"
                        DataMember="glAccountClass" Pagination="True" QueryTitle="查詢" EditDialogID="JQDialog1"
                        Title="會計類別" QueryMode="Window" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="15,20,30,40,50" PageSize="15" QueryAutoColumn="False" QueryLeft="10px" QueryTop="10px" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" OnLoadSuccess="OnLoadMaster" OnSelect="OnSelectMaster" OnDelete="CheckMasterDelete">
                        <Columns>
                            <JQTools:JQGridColumn Alignment="right" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Visible="False" Width="120" />
                            <JQTools:JQGridColumn Alignment="right" Caption="會計主類" Editor="text" FieldName="BalanceSheetName" Format="" Visible="True" Width="60" />
                            <JQTools:JQGridColumn Alignment="center" Caption="類別代號" Editor="numberbox" FieldName="ClassID" Format="" Visible="true" Width="55" />
                            <JQTools:JQGridColumn Alignment="left" Caption="類別名稱" Editor="text" FieldName="ClassName" Format="" Visible="true" Width="100" />
                            <JQTools:JQGridColumn Alignment="left" Caption="起始編號" Editor="text" FieldName="AcnoSNo" Format="" MaxLength="0" Visible="true" Width="55" />
                            <JQTools:JQGridColumn Alignment="left" Caption="終止編號" Editor="text" FieldName="AcnoENo" Format="" MaxLength="0" Visible="true" Width="55" />
                        </Columns>
                        <TooItems>
                            <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                                OnClick="insertItem" Text="新增" />                           
                            <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                                OnClick="openQuery" Text="查詢會計主類" />
                        </TooItems>
                        <QueryColumns>
                            <JQTools:JQQueryColumn AndOr="and" Caption="會計主類" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'BalanceSheetID',textField:'BalanceSheetName',remoteName:'sglAccountItem.glAccountBalanceSheet',tableName:'glAccountBalanceSheet',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="BalanceSheetID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                        </QueryColumns>
                    </JQTools:JQDataGrid>
                    <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="會計類別維護">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="glAccountClass" HorizontalColumnsCount="2" RemoteName="sglAccountItem.glAccountClass" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="會計主類" Editor="infocombobox" FieldName="BalanceSheetID" Format="" Width="110" EditorOptions="valueField:'BalanceSheetID',textField:'BalanceSheetName',remoteName:'sglAccountItem.glAccountBalanceSheet',tableName:'glAccountBalanceSheet',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="類別代號" Editor="numberbox" FieldName="ClassID" Format="" Width="80" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="類別名稱" Editor="text" FieldName="ClassName" Format="" Width="180" NewRow="True" RowSpan="1" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="起始編號" Editor="text" FieldName="AcnoSNo" Format="" maxlength="0" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="終止編號" Editor="text" FieldName="AcnoENo" Format="" maxlength="0" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="BalanceSheetID" RemoteMethod="True" ValidateMessage="請選擇會計主類！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ClassID" RemoteMethod="True" ValidateMessage="請輸入類別代號！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ClassName" RemoteMethod="True" ValidateMessage="請輸入類別名稱！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="AcnoSNo" RemoteMethod="True" ValidateMessage="請輸入起始編號！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="AcnoENo" RemoteMethod="True" ValidateMessage="請輸入終止編號！" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
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
                    <JQTools:JQDataGrid ID="dataGridDetail" runat="server" DataMember="glAccountItem" RemoteName="sglAccountItem.glAccountItem" AllowAdd="True" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="14,20,30,40,50" PageSize="14" Pagination="True" QueryAutoColumn="False" QueryLeft="450px" QueryMode="Window" QueryTitle="會計科目查詢" QueryTop="100px" RecordLock="False" RecordLockMode="None" RowNumbers="True" Title="會計科目" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" ParentObjectID="" EditDialogID="JQDialog2" OnLoadSuccess="OnLoadDetail" >
                        <Columns>
                            <JQTools:JQGridColumn Alignment="left" Caption="公司別" Editor="text" FieldName="CompanyName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="center" Caption="科目" Editor="text" FieldName="Acno" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="55">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="center" Caption="明細" Editor="text" FieldName="SubAcno" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="left" Caption="科目名稱" Editor="text" FieldName="AcnoName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="160">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="center" Caption="成本中心?" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="bCostCenterID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" FormatScript="GridCheckBox">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="center" Caption="有效?" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50" FormatScript="GridCheckBox">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="center" Caption="損益關聯科目" Editor="text" FieldName="ProfitAcno" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="center" Caption="外帳科目" Editor="text" FieldName="ExternalAcno" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="left" Caption="英文名稱" Editor="text" FieldName="AcnoEName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                            </JQTools:JQGridColumn>
                        </Columns>
                        <TooItems>
                            <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                                OnClick="insertItem" Text="新增" />                           
                            <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                                OnClick="openQuery" Text="查詢會計科目" />
                            <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                        </TooItems>
                        <QueryColumns>
                            <JQTools:JQQueryColumn AndOr="and" Caption="科目明細" Condition="%%" DataType="string" Editor="text" FieldName="Acno" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                            <JQTools:JQQueryColumn AndOr="and" Caption="會計科目" Condition="%%" DataType="string" Editor="text" FieldName="AcnoName" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="150" />
                        </QueryColumns>
                    </JQTools:JQDataGrid>
                    <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" DialogLeft="300px" Title="會計科目維護">
                        <JQTools:JQDataForm ID="dataFormDetail" runat="server" ParentObjectID="dataFormMaster" DataMember="glAccountItem" RemoteName="sglAccountItem.glAccountItem" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="1" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApply="OnApplydataFormDetail" >
                            <Columns>
                                <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                                <JQTools:JQFormColumn Alignment="left" Caption="CompanyID" Editor="text" FieldName="CompanyID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                <JQTools:JQFormColumn Alignment="left" Caption="科目" Editor="text" FieldName="Acno" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                                <JQTools:JQFormColumn Alignment="left" Caption="明細" Editor="text" FieldName="SubAcno" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                                <JQTools:JQFormColumn Alignment="left" Caption="科目名稱" Editor="text" FieldName="AcnoName" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="300" />
                                <JQTools:JQFormColumn Alignment="left" Caption="英文名稱" Editor="text" FieldName="AcnoEName" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="400" />
                                <JQTools:JQFormColumn Alignment="left" Caption="成本中心?" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="bCostCenterID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                                <JQTools:JQFormColumn Alignment="left" Caption="有效?" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                                <JQTools:JQFormColumn Alignment="left" Caption="ClassID" Editor="text" FieldName="ClassID" Visible="False" Width="80" />
                                <JQTools:JQFormColumn Alignment="left" Caption="損益關聯科目" Editor="text" FieldName="ProfitAcno" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                                <JQTools:JQFormColumn Alignment="left" Caption="外帳科目" Editor="inforefval" EditorOptions="title:'請選擇科目',panelWidth:390,remoteName:'sglAccountItem.infoExternalAcno',tableName:'infoExternalAcno',columns:[{field:'Acno',title:'科目代號',width:80,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'AcnoName',title:'科目名稱',width:280,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'Acno',textField:'AcnoName',valueFieldCaption:'Acno',textFieldCaption:'AcnoName',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" FieldName="ExternalAcno" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="350" />

                            </Columns>
                        </JQTools:JQDataForm>
                        <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                            <Columns>
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetCompanyID" FieldName="CompanyID" RemoteMethod="False" />
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetClassID" FieldName="ClassID" RemoteMethod="False" />
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="" DefaultValue="1" FieldName="IsActive" RemoteMethod="True" />
                            </Columns>
                        </JQTools:JQDefault>
                        <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                            <Columns>
                                <JQTools:JQValidateColumn CheckNull="True" FieldName="Acno" RemoteMethod="True" ValidateMessage="請輸入科目！" ValidateType="None" />
                                <JQTools:JQValidateColumn CheckNull="True" FieldName="AcnoName" RemoteMethod="True" ValidateMessage="請輸入科目名稱！" ValidateType="None" />
                            </Columns>
                        </JQTools:JQValidate>
                    </JQTools:JQDialog>
                </div>
           </div>
        </div>
        </div>
    </form>
</body>
</html>
