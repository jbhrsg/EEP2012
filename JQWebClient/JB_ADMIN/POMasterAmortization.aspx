<%@ Page Language="C#" AutoEventWireup="true" CodeFile="POMasterAmortization.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script> 
        //========================================= ready ====================================================================================
        $(document).ready(function () {
          
            //設定攤銷年月       
            var sDate = new Date();
            var vDate = new Date($.jbDateAdd('months', 1, sDate));
            var date1 = $.jbjob.Date.DateFormat(sDate, 'yyyyMMdd').substring(0, 6);
            $("#YearMonth_Query").val(date1);

        });
        
        //------------------------------------------狀態處理-------------------------------------------        
        //1查詢,2計算筆數與加總,3寫入傳票
        function ProcessAmortization(sType) {
            var YearMonth = $('#YearMonth_Query').val();//攤銷年月           
            var CompanyID = $("#CompanyID_Query").combobox('getValue');
            var POAutoKey = $("#POAutoKey_Query").val();//攤銷編號  

            if (YearMonth == "") {
                alert('攤銷年月不可空白！');
                $('#dataGridView').datagrid('loadData', []);//清空Grid資料
            } else {

                //攤銷內容--1查詢,2計算筆數與加總,3寫入傳票
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sPOMasterAmortization.POMasterAmortization', //連接的Server端，command
                    data: "mode=method&method=" + "procInsertPOMasterVoucherM" + "&parameters=" + YearMonth + "," + CompanyID + "," + POAutoKey + "," + sType, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                    cache: false,
                    async: false,
                    success: function (data) {

                        if (sType == "1") {//1查詢
                            var rows = $.parseJSON(data);//將JSon轉會到Object類型提供給Grid顯示
                            if (rows != null) {
                                if (rows.length > 10) {
                                    //通過loadData方法清除掉原有Grid中的舊有資料並填補分頁資料
                                    $('#dataGridView').datagrid({ loadFilter: pagerFilter, url: "" }).datagrid('loadData', rows);
                                } else {
                                    $('#dataGridView').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                                }
                            } else $('#dataGridView').datagrid('loadData', []);//清空Grid資料
                        }
                        else if (sType == "2")
                        {
                            var rows = $.parseJSON(data);
                            if (rows != null) {
                                if (rows.length > 0) {
                                    $('#dataGridView').datagrid('getPanel').panel('setTitle', rows[0].sdata);
                                }
                            } else $('#dataGridView').datagrid('getPanel').panel('setTitle', "");
                        }
                        else if (sType == "3") {
                            alert('寫入傳票完成！');
                            queryGrid($('#dataGridView'));
                        }


                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        alert(xhr.status);
                        alert(thrownError);
                    }
                });
            
            
            
            }

        }

        function queryGrid(dg) {//查詢後添加固定條件
            if ($(dg).attr('id') == 'dataGridView') {               
                ProcessAmortization("1");
                ProcessAmortization("2");
            }            
        }

        function FormatDateString(val, rowData) {           
            return "<div style='color:red;'> " + val + "</div>";            
        }

        //傳票總額連結
        function glAmtLink(value, row, index) {
            if (value != 0)
            return "<a href='javascript: void(0)' onclick='LinkglAmt(" + index + ");'>" + value + "</a>";
            else return value;
        }

        // open傳票總額 dialog
        function LinkglAmt(index) {
            $("#dataGridView").datagrid('selectRow', index);

            var rows = $("#dataGridView").datagrid('getSelected');
            var AutoKey = rows.POAutoKey;
            $("#dataGridDetail").datagrid('setWhere', "MAutoKey = " + AutoKey);
            //$("#JQDialog2").dialog("open");
            openForm('#JQDialog2', {}, 'inserted', 'dialog');

        }
        //
        function GetMAutoKey() {
            var rows = $("#dataGridView").datagrid('getSelected');
            return rows.POAutoKey;
        }
        //-----------------------------------------新增傳票-----------------------------------------------------------
        //  OnLoad DataFormDetail 過濾公司別&已在名單中
        function OnLoadDFDetail() {
            var rows = $("#dataGridView").datagrid('getSelected');
            $('#dataFormDetailVoucherNo').refval('setWhere', "CompanyID=" + rows.CompanyID + " and VoucherNo not in (select VoucherNo from POMasterAmortizationV where MAutoKey=" + rows.AutoKey + " and IsActive=1)");
        }
        // 新增傳票前檢查
        function OnApplyDFDetail() {
            var Check = $("#dataFormDetailVoucherNo").refval('getValue');
            if (Check == "" || Check == undefined) {
                alert('傳票未選擇！');
                return false;
            }
        }
        // 新增傳票後
        function OnAppliedDFDetail() {
            $('#dataGridView').datagrid('reload');
        }
        //-----------------------------------------失效項目-----------------------------------------------------------
        //刪除=>失效
        function OnDeleteDG() {
            var pre = confirm("確認失效此筆?");
            if (pre == true) {
                var AutoKey = $('#dataGridDetail').datagrid('getSelected').AutoKey;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sPOMasterAmortization.POMasterAmortization', //連接的Server端，command
                    data: "mode=method&method=" + "UpdatePOMasterAmortizationVIsActive" + "&parameters=" + AutoKey, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                    cache: false,
                    async: true,
                });
            }
            return false; //取消失效的動作
            $('#dataGridDetail').datagrid('reload');
        }
        //寫入傳票
        function InsertVoucher() {

            var rows = $('#dataGridView').datagrid('getRows');
            if (rows.length > 0) {               
                var pre = confirm("確定寫入傳票?");
                if (pre == true) {
                    ProcessAmortization("3");
                }                
            } else {
                alert('無資料！');
            }
        }
        //--------------清單 報表-----------------------------------------------------------------------------------------------       
        function OpenAmortization() {
            var YearMonth = $('#YearMonth_Query').val();//攤銷年月           
            var CompanyID = $("#CompanyID_Query").combobox('getValue');
            var POAutoKey = $("#POAutoKey_Query").val();//攤銷編號  

            if (YearMonth == "") {
                alert('攤銷年月不可空白！');
                $('#dataGridView').datagrid('loadData', []);//清空Grid資料
            } else {
                var url = "../JB_ADMIN/REPORT/JBGL/AmortizationReport.aspx?YearMonth=" + YearMonth + "&CompanyID=" + CompanyID + "&POAutoKey=" + POAutoKey;

                var height = $(window).height() - 20;
                var height2 = $(window).height() - 90;
                var width = $(window).width() - 230;
                var dialog = $('<div/>')
                .dialog({
                    draggable: false,
                    modal: true,
                    height: height,
                    //top:0,
                    width: width,
                    title: "攤銷清單",
                    //maximizable: true                              
                });
                $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="98%"></iframe>').appendTo(dialog.find('.panel-body'));
                dialog.dialog('open');
            }
        }



    </script> 
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="12月時注意 → 有預留殘值(整年度攤提&lt;=預留殘值) ;  無預留殘值(整年度攤提&lt;=原價/5)"></asp:Label>
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sPOMasterAmortization.POMasterAmortization" runat="server" AutoApply="True"
                DataMember="POMasterAmortization" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title=" " AllowAdd="True" AllowDelete="True" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="110px" QueryMode="Panel" QueryTop="50px" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="攤銷編號" Editor="numberbox" FieldName="POAutoKey" Format="" Visible="True" Width="55" FormatScript="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="公司別" Editor="text" FieldName="CompanyName" Format="" MaxLength="0" Visible="true" Width="98" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="資產名稱" Editor="text" FieldName="AssetName" Format="" MaxLength="0" Visible="true" Width="150" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="取得日期" Editor="datebox" FieldName="AssetGetDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" Format="yyyy/mm/dd" FormatScript=""></JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="取得原價" Editor="numberbox" FieldName="AssetGetAmt" Format="N" Visible="true" Width="68" />
                    <JQTools:JQGridColumn Alignment="right" Caption="預留殘值" Editor="numberbox" FieldName="ScrapValue" Format="N" Visible="true" Width="63" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="整年度攤提" Editor="numberbox" FieldName="AddAmt" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="68" Format="N">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="原價/5" Editor="numberbox" FieldName="FiveAmt" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="58" Format="N">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="已攤金額" Editor="numberbox" FieldName="DeductionAmt" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="67" Format="N">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="傳票總額" Editor="text" FieldName="glAmt" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="67" FormatScript="glAmtLink" Format="N">
                    </JQTools:JQGridColumn>
                   
                    <JQTools:JQGridColumn Alignment="right" Caption="累計提列數" Editor="numberbox" FieldName="sumAmt" Format="N" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="68">
                    </JQTools:JQGridColumn>
                   
                    <JQTools:JQGridColumn Alignment="right" Caption="未折減餘額" Editor="numberbox" FieldName="RemainAmt" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70" Format="N" EditorOptions="">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="提列數" Editor="numberbox" FieldName="MentionAmt" Format="N" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="58">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="傳票金額" Editor="numberbox" FieldName="Amt" Format="N" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Total="" Visible="True" Width="66" FormatScript="FormatDateString">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="借方科目" Editor="text" FieldName="BorrowNo" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="63">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="貸方科目" Editor="text" FieldName="LendNo" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="63">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem ID="JQToolItem2" Icon="icon-print" ItemType="easyui-linkbutton" OnClick="OpenAmortization" Text="匯出清單"  />
                    <JQTools:JQToolItem Icon="icon-next" ItemType="easyui-linkbutton" OnClick="InsertVoucher" Text="寫入傳票" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="攤銷年月" Condition="=" DataType="string" Editor="text" FieldName="YearMonth" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="70" DefaultMethod="" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="公司別" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sglVoucherMaster.glCompany',tableName:'glCompany',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CompanyID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="160" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="攤銷編號" Condition="=" DataType="string" Editor="numberbox" FieldName="POAutoKey" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="60" />
                </QueryColumns>
            </JQTools:JQDataGrid>

                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" Closed="True" DialogLeft="140px" EditMode="Dialog" Title="傳票資訊" Width="375px" ShowSubmitDiv="False">
                    <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="False" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="POMasterAmortizationV" DeleteCommandVisible="False" DuplicateCheck="False" EditDialogID="JQDialog3" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False"  PageList="10,20,30,40,50" PageSize="10" Pagination="True" ParentObjectID="" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sPOMasterAmortization.POMasterAmortizationV" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnDelete="OnDeleteDG">
                        <Columns>
                            <JQTools:JQGridColumn Alignment="left" Caption="傳票編號" Editor="text" FieldName="VoucherNoShow" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="center" Caption="傳票日期" Editor="text" FieldName="VoucherDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="95">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="right" Caption="總金額" Editor="numberbox" FieldName="Amt" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100" Total="sum">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="left" Caption="MAutoKey" Editor="text" FieldName="MAutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                            </JQTools:JQGridColumn>
                        </Columns>
                    </JQTools:JQDataGrid>
                    <JQTools:JQDialog ID="JQDialog3" runat="server" BindingObjectID="dataFormDetail" Width="390px" DialogLeft="160px" DialogTop="130px" Title="新增傳票" ShowModal="True" ShowSubmitDiv="True">
                        <JQTools:JQDataForm ID="dataFormDetail" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="True" DataMember="POMasterAmortizationV" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="5" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnLoadSuccess="OnLoadDFDetail" ParentObjectID="dataGridDetail" RemoteName="sPOMasterAmortization.POMasterAmortization" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnApply="OnApplyDFDetail" OnApplied="OnAppliedDFDetail">
                            <Columns>
                                <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                <JQTools:JQFormColumn Alignment="left" Caption="MAutoKey" Editor="numberbox" FieldName="MAutoKey" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="40" />
                                <JQTools:JQFormColumn Alignment="left" Caption="傳票編號" Editor="inforefval" EditorOptions="title:'選擇傳票',panelWidth:750,remoteName:'sPOMasterAmortization.infoglVoucher',tableName:'infoglVoucher',columns:[{field:'VoucherNoShow',title:'傳票編號',width:110,align:'center',table:'',isNvarChar:false,queryCondition:''},{field:'VoucherDate',title:'傳票日期',width:87,align:'left',table:'',isNvarChar:false,queryCondition:'',formatter:formatValue,format:'yyyy/mm/dd'},{field:'Amt',title:'總金額',width:86,align:'right',table:'',isNvarChar:false,queryCondition:''},{field:'sDescribe',title:'借方摘要',width:200,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'sDescribe2',title:'貸方摘要',width:200,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[{field:'VoucherNo',value:'VoucherNo'}],whereItems:[],valueField:'VoucherNo',textField:'VoucherNo',valueFieldCaption:'VoucherNo',textFieldCaption:'VoucherNo',cacheRelationText:false,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="VoucherNo" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" />
                                <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            </Columns>
                            <RelationColumns>
                                <JQTools:JQRelationColumn FieldName="AutoKey" ParentFieldName="AutoKey" />
                            </RelationColumns>
                        </JQTools:JQDataForm>
                        <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                            <Columns>
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="AutoKey" RemoteMethod="True" />
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                                <JQTools:JQDefaultColumn DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                                <JQTools:JQDefaultColumn DefaultValue="_username" FieldName="LastUpdateBy" RemoteMethod="True" CarryOn="False" />
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="LastUpdateDate" RemoteMethod="True" />
                                <JQTools:JQDefaultColumn CarryOn="False" FieldName="MAutoKey" RemoteMethod="False" DefaultMethod="GetMAutoKey" />
                            </Columns>
                        </JQTools:JQDefault>
                        <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                            <Columns>
                                <JQTools:JQValidateColumn CheckNull="True" FieldName="BorrowLendType" RemoteMethod="True" ValidateMessage="請選擇借貸！" ValidateType="None" />
                                <JQTools:JQValidateColumn CheckNull="True" FieldName="Acno" RemoteMethod="True" ValidateMessage="請選擇科目！" ValidateType="None" />
                                <JQTools:JQValidateColumn CheckMethod="CheckSubAcno" CheckNull="False" FieldName="SubAcno" RemoteMethod="False" ValidateMessage="請選擇明細！" ValidateType="None" />
                                <JQTools:JQValidateColumn CheckNull="True" FieldName="Describe" RemoteMethod="True" ValidateMessage="請填寫摘碼內容！" ValidateType="None" />
                                <JQTools:JQValidateColumn CheckNull="True" FieldName="AmtShow" RemoteMethod="True" ValidateMessage="請填寫金額！" ValidateType="None" />
                            </Columns>
                        </JQTools:JQValidate>
                    </JQTools:JQDialog>
                </JQTools:JQDialog>

        </div>
    </form>
</body>
</html>
