<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FWCRMOrdersRepeat.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script>       
        var flag = true; //定義一個全域變數，只有第一次執行
        var parameter = "";
        $(document).ready(function () {
            $("input, select, textarea").focus(function () {
                $(this).css("background-color", "#FFFFB5");
            });

            $("input, select, textarea").blur(function () {
                $(this).css("background-color", "white");
            });
        })
        //--------------------------------------★外籍勞工訂單 Order-----------------------------------------------
        function OnLoadSuccessOrders() {
            //預設不顯示-------------------------------------------------
            //挑工進度維護
            $("#linkStickStatus").hide();                     
            //結案日期
            $('#dataFormOrdersCloseDate').closest('td').prev('td').hide();
            $('#dataFormOrdersCloseDate').closest('td').hide();
            //結案原因
            $('#dataFormOrdersCloseType').closest('td').prev('td').hide();
            $('#dataFormOrdersCloseType').closest('td').hide();

            ////取得流程狀態=>控制顯示項目
            parameter = Request.getQueryStringByName("D");

            //--------------------------------------★Need Update-----------------------------------------------
            //parameter = "Select";//Need Update
            //----------------------------------------------------------------------------------------------------

            //國外組挑工
            if (parameter == "Select" || parameter == "Close") {
                OPWorkNoLogs();
            }

            //業務結案確認 --> 顯示結案日期 結案原因 ,其他不可編輯
            if (parameter == "Close") {
                //顯示
                $('#dataFormOrdersCloseDate').closest('td').prev('td').show();
                $('#dataFormOrdersCloseDate').closest('td').show();
                $("#dataFormOrdersCloseDate").closest('td').prev('td').css("color", "red");
                $("#dataFormOrdersCloseDate").datebox('textbox').focus();//日期焦點
                $('#dataFormOrdersCloseType').closest('td').prev('td').show();
                $('#dataFormOrdersCloseType').closest('td').show();
                $("#dataFormOrdersCloseType").closest('td').prev('td').css("color", "red");

            }
                     
        }
        
        function OnLoadSuccessDGDetail() {
            //結案提醒
            if (parameter == "Close") {                
                alert("提醒您，若要結案請輸入結案日期，若無結案日期則會繼續挑工流程。");
            }
        }

        ////國外組挑工=>1. 顯示挑工進度維護,2. 顯示聘工表的修改紀錄=>圖片+tooltip
        function OPWorkNoLogs() {
            //1. 顯示挑工進度維護 select挑工狀態=>可編輯 , Close 結案狀態可觀看
            $("#linkStickStatus").show();
            //2. 顯示聘工表的修改紀錄=>圖片+tooltip            
            var WorkNoLogsLink = '<a href="javascript:void(0)" onclick="OpenWorkNoLogs.call(this)" ><img src="../img/msalert.gif" title="聘工表修改紀錄"></a>';

            var tdWorkImg = $('#dataFormOrdersWorkImg').closest('td');
            tdWorkImg.append("&nbsp;&nbsp;&nbsp;");
            tdWorkImg.append(WorkNoLogsLink);
        }

        //-----------------------------------★訂單明細dataGridDetail  呼叫頁面--------------------------------------------------------------------- 

        //呼叫挑工進度頁面
        function OpenStickStatus() {
            //挑工進度維護 select挑工狀態=>可編輯 , Close 結案狀態可觀看
            if (parameter == "Select") {
                openForm('#JQDialogStickStatus', $('#dataGridDetail').datagrid('getSelected'), "updated", 'dialog');
            }else 
                openForm('#JQDialogStickStatus', $('#dataGridDetail').datagrid('getSelected'), "viewed", 'dialog');
        }
        //呼叫聘工表的修改紀錄頁面
        function OpenWorkNoLogs() {
            openForm('#JQDialogWorkNoLogs', $('#dataGridDetail').datagrid('getSelected'), "updated", 'dialog');
        }        
        
        //-----------------------------------★聘工表修改紀錄--------------------------------------------------------------------- 
        function FormatScriptflag(val, rowData) {
            if (val == "Z") {
                return "";
            } else {
                return "簽核中";
            }
        }
        
        //-----------------------------------★訂單明細dataGridDetail--------------------------------------------------------------------- 

        //訂單GridDetail目前人數Link => 入境確認輸入Confirm(可編輯) & 結案狀態Close(可觀看) 時才有連結
        function PersonQtyFinalLink(value, row, index) {
            if (row.Gender != undefined && (parameter == "Confirm" || parameter == "Close"))//表示最後一筆加總的row && 入境確認輸入時
                return "<a href='javascript: void(0)' onclick='LinkIndateCheck(" + index + ");'> <div style='color:Red;font-weight:bolder;font-size: 16px'>" + value + "</div></a>";
            else return value;
        }

        //-----------------------------------★入境確認維護---------------------------------------------------------------------     
        // open入境確認單畫面 dialog
        function LinkIndateCheck(index, iType) {
            $("#dataGridDetail").datagrid('selectRow', index); //按連結時返回Grid焦點  
            if (parameter == "Confirm") {//入境確認輸入Confirm(可編輯) 
                openForm('#Dialog_IndateCheck', $('#dataGridDetail').datagrid('getSelected'), "updated", 'dialog');
            } else openForm('#Dialog_IndateCheck', $('#dataGridDetail').datagrid('getSelected'), "viewed", 'dialog');//結案狀態Close(可觀看)
        }
        //過濾入境確認維護Grid setWhere
        function OnLoadSuccessDFIndateCheck() {           
            var OrderNo = $("#dataFormDetailOrderNo").val();//訂單編號
            var Item = $("#dataFormDetailItem").val();//批次
            $("#dataGrid_IndateCheck").datagrid('setWhere', "OrderNo = '" + OrderNo + "' and Item=" + Item);           
        }        
        
      
        //上傳檔案處理
        var dataFormIndateImg_onSuccess = function (data) {
            dataFormIndateImg_onSuccess.call($(infoFileUploaddataFormOrdersWorkImg));
        }
        var dataFormIndateImg_onSuccess = function (data) {
            $(this).jbFileUploadWithPhoto();
        }

        //入境單號碼過濾 =>combobx 的 onBeforeLoad 
        function OnBeforeLoadIndateNo(param) {
            var queryWord = new Object();
            OrderNo = $('#dataFormOrdersOrderNo').val();
            queryWord.whereString = "OrderNo ='" + OrderNo + "'";
            param.queryWord = $.toJSONString(queryWord);

        }
        //入境單號碼過濾 =>combobx 的 setWhere
        function OnLoadSuccessIndateCheck() {
            var OrderNo = $("#dataFormDetailOrderNo").val();//訂單編號
            $('#dataFormIndateCheckIndateNo').combobox('setWhere', "OrderNo ='" + OrderNo + "'")
        }
      
        //Grid下載檔案 
        //欄值,row,index
        function downloadScript(val, rowData, index) {
            if (rowData.IndateImg != undefined) {//表示不是最後一筆加總的row
                return '<a href="../handler/JqFileHandler.ashx?File=/FWCRM/Orders/' + val + '">' + val + '</a>';
            }
        }
        //新增後reload
        function OnAppliedIndate() {
            $('#dataGrid_IndateCheck').datagrid("reload");
            $('#dataGridDetail').datagrid("reload");
        }
        //新增前的檢查
        function OnApplyIndateCheck() {
            ////檢查項目之總輸入人數不可大於項目之訂單人數 
            ////↓訂單人數 
            var PersonQtyOriginal = $('#dataFormDetailPersonQtyOriginal').val();//項目之訂單人數

            //↓求得總輸入人數 
            var PersonQty = 0;//項目之總輸入人數
            var selectQty =0;//選取的值(編輯模式才有效)             

            var footerRows = $("#dataGrid_IndateCheck").datagrid('getFooterRows');//Grid尾之sum值
            if (footerRows != undefined) {
                PersonQty = footerRows[0]["PersonQty"];//項目之總輸入人數
            }
            if (getEditMode($("#dataFormIndateCheck")) == 'updated') {//編輯模式減去選取的值
                selectQty = $("#dataGrid_IndateCheck").datagrid('getSelected').PersonQty;
            }
            var PersonQty = PersonQty - selectQty + parseInt($("#dataFormIndateCheckPersonQty").val());

            if (PersonQtyOriginal < PersonQty) {
                alert("目前輸入總人數"+PersonQty+",不可大於訂單人數！");
                return false;
            } else $('#dataGrid_IndateCheck').datagrid("reload");
            
        }
        //-----------------------------------★主檔存檔前檢查 dataFormOrders---------------------------------------------------------------------             
        function checkApplyData() {

            //-------------------------------------國外組挑工--------------------------------------------------------------------------            
            if (parameter == "Select") {
                
                var OrderNo = $('#dataFormOrdersOrderNo').val();

                //1.檢查挑工進度維護筆數
                var cnt;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sFWCRMOrders.FWCRMStickStatus', //連接的Server端，command
                    data: "mode=method&method=" + "ReturnStickStatusCount" + "&parameters=" + OrderNo, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != false) {
                            cnt = $.parseJSON(data);
                        }
                    }
                });

                if ((cnt == "0" || cnt == "undefined")) {
                    alert("無挑工進度資料！");
                    return false;
                } 
            }
            //業務結案確認 --> 檢查結案日期
            if (parameter == "Close") {
                var CloseDate = $('#dataFormOrdersCloseDate').datebox('getValue');
                var DateValidate = $.fn.datebox('parseDate', CloseDate.replace(/\//g, '-'));
                if (DateValidate == "Invalid Date") {
                    alert('結案日期格式錯誤！');
                    return false;
                } else if (CloseDate == "") {
                    var footerRows = $("#dataGridDetail").datagrid('getFooterRows');//Grid尾之sum值
                    if (footerRows != undefined) {
                        var PersonQtyOriginal = footerRows[0]["PersonQtyOriginal"];//訂單人數
                        var PersonQtyFinal = footerRows[0]["PersonQtyFinal"];//目前人數
                    }
                    if (PersonQtyOriginal - PersonQtyFinal == 0) {
                        alert('人數已滿，請輸入結案日期！');
                        return false;
                    } else alert('繼續挑工流程！');
                } else {
                    var value = $('#dataFormOrdersCloseType').options('getCheckedValue');
                    if (value == "") {
                        alert('請選擇結案原因！');
                        return false;
                    }

                }
                    
            }


        }
    </script>  
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sFWCRMOrders.FWCRMOrders" runat="server" AutoApply="True"
                DataMember="FWCRMOrders" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="訂單編號" Editor="text" FieldName="OrderNo" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="原訂單編號" Editor="text" FieldName="FromOrderNo" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聘工表號碼 " Editor="text" FieldName="WorkNo" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="雇主名稱" Editor="text" FieldName="EmployerID" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="引進國別" Editor="text" FieldName="NationalityID" Format="" Visible="true" Width="120" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消" />
                   
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormOrders" Title="外籍勞工訂單" Width="780px" DialogTop="20px" ShowSubmitDiv="False">
                <JQTools:JQDataForm ID="dataFormOrders" runat="server" DataMember="FWCRMOrders" HorizontalColumnsCount="2" RemoteName="sFWCRMOrders.FWCRMOrders" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="True" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="True" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnLoadSuccess="OnLoadSuccessOrders" OnApply="checkApplyData" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="訂單編號" Editor="text" FieldName="OrderNo" Format="" Width="120" ReadOnly="True" NewRow="False" Visible="True" MaxLength="0" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="來源訂單" Editor="text" FieldName="FromOrderNo" Format="" Width="120" EditorOptions="" NewRow="False" ReadOnly="True" Visible="False" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="訂單類型" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:190,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:3,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'入境',value:'1'},{text:'承接',value:'2'},{text:'轉單',value:'3'}]" FieldName="OrderType" NewRow="True" ReadOnly="True" Visible="True" Width="220" />
                        <JQTools:JQFormColumn Alignment="left" Caption="負責業務" Editor="text" FieldName="NAME_C" Width="80" ReadOnly="True" NewRow="False" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="雇主名稱" Editor="infocombobox" FieldName="EmployerID" Format="" Width="180" EditorOptions="valueField:'EmployerID',textField:'EmployerName',remoteName:'sFWCRMOrders.infoEmployerID',tableName:'infoEmployerID',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" ReadOnly="True" MaxLength="0" NewRow="True" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聘工表號碼" Editor="text" EditorOptions="" FieldName="WorkNo" Format="" MaxLength="0" NewRow="False" ReadOnly="True" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="引進國別" Editor="infocombobox" EditorOptions="valueField:'AutoKey',textField:'NationalityName',remoteName:'sFWCRMOrders.infoFWCRMNationality',tableName:'infoFWCRMNationality',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="NationalityID" Format="" MaxLength="0" NewRow="True" ReadOnly="True" Visible="True" Width="180" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聘工表檔案" Editor="text" FieldName="WorkImg" MaxLength="100" Width="190" EditorOptions="" NewRow="False" Format="download,folder:Files/FWCRM/Orders" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" NewRow="False" Visible="False" MaxLength="0" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="國外仲介" Editor="inforefval" FieldName="sup_no" Width="350" Visible="True" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="2" EditorOptions="title:'選擇國外仲介',panelWidth:450,panelHeight:290,remoteName:'sFWCRMOrders.infosup',tableName:'infosup',columns:[{field:'sup_cname',title:'仲介名稱',width:420,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'sup_no',title:'仲介代號',width:90,align:'center',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[{field:'sup_cname',value:'sup_cname'}],whereItems:[],valueField:'sup_no',textField:'sup_cname',valueFieldCaption:'仲介代號',textFieldCaption:'仲介名稱',cacheRelationText:false,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" />
                        <JQTools:JQFormColumn Alignment="left" Caption="sup_cname" Editor="text" FieldName="sup_cname" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="結案日期" Editor="datebox" FieldName="CloseDate" MaxLength="0" NewRow="True" ReadOnly="False" Width="90" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="結案原因" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:190,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'訂單作廢',value:'1'},{text:'不再引進',value:'2'}]" FieldName="CloseType" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="False" DataMember="FWCRMOrdersDetails" EditDialogID="Dialog_IndateCheck" Pagination="False" ParentObjectID="dataFormOrders" RemoteName="sFWCRMOrders.FWCRMOrders" Title="" AllowAdd="True" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="False" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnLoadSuccess="OnLoadSuccessDGDetail" >

                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="批次" Editor="text" FieldName="Item" Format="" Width="30" />
                        <JQTools:JQGridColumn Alignment="left" Caption="預定年月" Editor="text" FieldName="PlanIndate" Format="" Width="56" />
                        <JQTools:JQGridColumn Alignment="right" Caption="訂單人數" Editor="text" FieldName="PersonQtyOriginal" Format="" Width="56" Total="sum" />
                        <JQTools:JQGridColumn Alignment="right" Caption="目前人數" Editor="text" FieldName="PersonQtyFinal" Width="56" Total="sum" FormatScript="PersonQtyFinalLink" />
                        <JQTools:JQGridColumn Alignment="center" Caption="性別" Editor="infocombobox" FieldName="Gender" Format="" Width="38" EditorOptions="items:[{value:'1',text:'女',selected:'false'},{value:'2',text:'男',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="函號" Editor="text" FieldName="org_okno" Format="" Width="83" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="工期" Editor="text" FieldName="WorkTimeText" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="65" />
                        <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="Notes" Format="" Width="151" />
                        <JQTools:JQGridColumn Alignment="left" Caption="OrderNo" Editor="text" FieldName="OrderNo" Width="80" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="OrderNo" ParentFieldName="OrderNo" />
                    </RelationColumns>
                </JQTools:JQDataGrid>

                <JQTools:JQDialog ID="Dialog_IndateCheck" runat="server" BindingObjectID="dataFormDetail" EditMode="Dialog" Title="入境確認維護" Width="620px" DialogLeft="170px" ShowSubmitDiv="False">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" ParentObjectID="dataFormOrders" DataMember="FWCRMOrdersDetails" HorizontalColumnsCount="2" RemoteName="sFWCRMOrders.FWCRMOrders" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="OnLoadSuccessDFIndateCheck" >
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="訂單編號" Editor="text" FieldName="OrderNo" Format="" Width="80" NewRow="False" ReadOnly="True" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="批次" Editor="numberbox" FieldName="Item" Format="" Width="40" NewRow="True" ReadOnly="True" MaxLength="0" RowSpan="1" Span="1" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="項目訂單人數" Editor="text" FieldName="PersonQtyOriginal" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="OrderNo" ParentFieldName="OrderNo" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                    <JQTools:JQDataGrid ID="dataGrid_IndateCheck" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" data-options="pagination:true,view:commandview" DataMember="FWCRMIndateCheck" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialog4" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sFWCRMOrders.FWCRMIndateCheck" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" ParentObjectID="">
                        <Columns>
                            <JQTools:JQGridColumn Alignment="left" Caption="入境單號碼" Editor="text" EditorOptions="" FieldName="IndateNo" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="102">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="right" Caption="人數" Editor="numberbox" FieldName="PersonQty" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Total="sum" Visible="True" Width="68">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="left" Caption="入境單檔案" Editor="text" FieldName="IndateImg" Format="" FormatScript="downloadScript" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="300" />
                            <JQTools:JQGridColumn Alignment="left" Caption="OrderNo" Editor="text" FieldName="OrderNo" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="left" Caption="Item" Editor="text" FieldName="Item" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="left" Caption="iAutokey" Editor="text" FieldName="iAutokey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                            </JQTools:JQGridColumn>
                        </Columns>
                        <TooItems>
                            <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                        </TooItems>
                    </JQTools:JQDataGrid>
                    <JQTools:JQDialog ID="JQDialog4" runat="server" BindingObjectID="dataFormIndateCheck" DialogLeft="190px" DialogTop="170px" EditMode="Dialog" ShowSubmitDiv="True" Title="入境確認輸入" Width="450px">
                        <JQTools:JQDataForm ID="dataFormIndateCheck" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="FWCRMIndateCheck" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="1" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnApplied="OnAppliedIndate" OnApply="OnApplyIndateCheck" OnLoadSuccess="OnLoadSuccessIndateCheck" ParentObjectID="dataFormDetail" RemoteName="sFWCRMOrders.FWCRMIndateCheck" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                            <Columns>
                                <JQTools:JQFormColumn Alignment="left" Caption="入境單號碼" Editor="infocombobox" EditorOptions="valueField:'IndateNo',textField:'IndateNo',remoteName:'sFWCRMOrders.FWCRMIndateNo',tableName:'FWCRMIndateNo',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="IndateNo" NewRow="False" ReadOnly="False" Visible="True" Width="130" />
                                <JQTools:JQFormColumn Alignment="left" Caption="人數" Editor="numberbox" FieldName="PersonQty" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="70" />
                                <JQTools:JQFormColumn Alignment="left" Caption="入境單檔案" Editor="infofileupload" EditorOptions="filter:'docx|xlsx|jpg|jpeg|png|bmp|gif',isAutoNum:true,upLoadFolder:'Files/FWCRM/Orders',showButton:true,showLocalFile:false,onSuccess:dataFormIndateImg_onSuccess,fileSizeLimited:'500'" FieldName="IndateImg" MaxLength="100" NewRow="False" ReadOnly="False" Span="1" Visible="True" Width="100" />
                                <JQTools:JQFormColumn Alignment="left" Caption="iAutokey" Editor="text" FieldName="iAutokey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                <JQTools:JQFormColumn Alignment="left" Caption="OrderNo" Editor="text" FieldName="OrderNo" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                <JQTools:JQFormColumn Alignment="left" Caption="Item" Editor="text" FieldName="Item" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            </Columns>
                            <RelationColumns>
                                <JQTools:JQRelationColumn FieldName="OrderNo" ParentFieldName="OrderNo" />
                                <JQTools:JQRelationColumn FieldName="Item" ParentFieldName="Item" />
                            </RelationColumns>
                        </JQTools:JQDataForm>
                        <JQTools:JQDefault ID="defaultIndateCheck" runat="server" BindingObjectID="dataFormIndateCheck" EnableTheming="True">
                            <Columns>
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="iAutokey" RemoteMethod="True" />
                            </Columns>
                        </JQTools:JQDefault>
                        <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormIndateCheck" EnableTheming="True">
                            <Columns>
                                <JQTools:JQValidateColumn CheckNull="True" FieldName="IndateNo" RemoteMethod="True" ValidateMessage="請選擇入境單號碼！" ValidateType="None" />
                                <JQTools:JQValidateColumn CheckNull="True" FieldName="PersonQty" RemoteMethod="True" ValidateMessage="人數不可空白！" ValidateType="None" />
                                <JQTools:JQValidateColumn CheckNull="True" FieldName="IndateImg" RemoteMethod="True" ValidateMessage="請上傳入境單檔案！" ValidateType="None" />
                            </Columns>
                        </JQTools:JQValidate>
                    </JQTools:JQDialog>
                </JQTools:JQDialog>
            <JQTools:JQDialog ID="JQDialogStickStatus" runat="server" DialogLeft="140px" DialogTop="80px" Title="挑工進度維護" ShowSubmitDiv="False" Width="720px" BindingObjectID="dataFormOrders0">
                 <JQTools:JQDataForm ID="dataFormOrders0" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="FWCRMOrders" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="2" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sFWCRMOrders.FWCRMOrders" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0">
                     <Columns>
                         <JQTools:JQFormColumn Alignment="left" Caption="訂單編號" Editor="text" FieldName="OrderNo" Format="" maxlength="0" NewRow="False" ReadOnly="True" Width="100" />
                     </Columns>
                 </JQTools:JQDataForm>
                 <JQTools:JQDataGrid ID="dataGridStickStatus" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="FWCRMStickStatus" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="False" ParentObjectID="dataFormOrders0" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sFWCRMOrders.FWCRMOrders" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False">
                     <Columns>
                         <JQTools:JQGridColumn Alignment="left" Caption="進度狀態" Editor="infocombobox" EditorOptions="valueField:'iAutoKey',textField:'StatusName',remoteName:'sFWCRMOrders.infoFWCRMSetStatus',tableName:'infoFWCRMSetStatus',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="StatusID" Format="" Width="135" />
                         <JQTools:JQGridColumn Alignment="center" Caption="面試人數" Editor="text" FieldName="PersonQty" Total="" Width="55" />
                         <JQTools:JQGridColumn Alignment="center" Caption="日期" Editor="datebox" FieldName="StatusDate" Format="" Total="" Visible="True" Width="85" />
                         <JQTools:JQGridColumn Alignment="left" Caption="進度/結果" Editor="text" FieldName="StatusResult" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="150" />
                         <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="Notes" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="150" />
                         <JQTools:JQGridColumn Alignment="left" Caption="OrderNo" Editor="text" FieldName="OrderNo" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                         <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                         </JQTools:JQGridColumn>
                         <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                         </JQTools:JQGridColumn>
                         <JQTools:JQGridColumn Alignment="left" Caption="iAutokey" Editor="text" FieldName="iAutokey" Visible="False" Width="80">
                         </JQTools:JQGridColumn>
                     </Columns>
                     <RelationColumns>
                         <JQTools:JQRelationColumn FieldName="OrderNo" ParentFieldName="OrderNo" />
                     </RelationColumns>
                     <TooItems>
                         <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                         <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="存檔" />
                         <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />
                     </TooItems>
                 </JQTools:JQDataGrid>
                 <JQTools:JQDefault ID="defaultDetailStickStatus" runat="server" BindingObjectID="dataGridStickStatus" EnableTheming="True">
                     <Columns>
                         <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                         <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                         <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="iAutokey" RemoteMethod="True" />
                     </Columns>
                 </JQTools:JQDefault>
                 <JQTools:JQValidate ID="validateDetailStickStatus" runat="server" BindingObjectID="dataGridStickStatus" EnableTheming="True">
                     <Columns>
                         <JQTools:JQValidateColumn CheckNull="True" FieldName="StatusID" RemoteMethod="True" ValidateMessage="請選擇進度狀態！" ValidateType="None" />
                     </Columns>
                 </JQTools:JQValidate>
            </JQTools:JQDialog>
                <JQTools:JQDialog ID="JQDialogWorkNoLogs" runat="server" BindingObjectID="dataFormOrders1" DialogLeft="140px" DialogTop="80px" ShowSubmitDiv="False" Title="聘工表修改紀錄" Width="730px">
                    <JQTools:JQDataForm ID="dataFormOrders1" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="FWCRMOrders" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="2" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sFWCRMOrders.FWCRMOrders" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0">
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="訂單編號" Editor="text" FieldName="OrderNo" Format="" maxlength="0" NewRow="False" ReadOnly="True" Width="100" Visible="False" />
                        </Columns>
                    </JQTools:JQDataForm>
                    <JQTools:JQDataGrid ID="dataGridWorkNoLogs" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="FWCRMWorkNoLogs" DeleteCommandVisible="False" DuplicateCheck="False" EditDialogID="JQDialog3" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="False" ParentObjectID="dataFormOrders1" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sFWCRMOrders.FWCRMOrders" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True">
                        <Columns>
                            <JQTools:JQGridColumn Alignment="left" Caption="修改內容" Editor="textarea" EditorOptions="height:70" FieldName="Memo" Visible="True" Width="390" />
                            <JQTools:JQGridColumn Alignment="center" Caption="流程狀態" Editor="text" FieldName="flowflag" FormatScript="FormatScriptflag" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="65">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="center" Caption="建立日期" Editor="text" FieldName="CreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="65" Format="yyyy/mm/dd">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="center" Caption="修改人員" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="65">
                            </JQTools:JQGridColumn>
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="OrderNo" ParentFieldName="OrderNo" />
                        </RelationColumns>                        
                    </JQTools:JQDataGrid>
                    <JQTools:JQDialog ID="JQDialog3" runat="server" BindingObjectID="dataFormWorkNoLogs" DialogLeft="200px" EditMode="Dialog" Title="" Width="600px">
                        <JQTools:JQDataForm ID="dataFormWorkNoLogs" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="FWCRMWorkNoLogs" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="2" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="dataFormOrders1" RemoteName="sFWCRMOrders.FWCRMOrders" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                            <Columns>
                                <JQTools:JQFormColumn Alignment="left" Caption="內容" Editor="textarea" EditorOptions="height:90" FieldName="Memo" NewRow="False" ReadOnly="False" Visible="True" Width="500" />
                                <JQTools:JQFormColumn Alignment="left" Caption="OrderNo" Editor="text" FieldName="OrderNo" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            </Columns>
                            <RelationColumns>
                                <JQTools:JQRelationColumn FieldName="OrderNo" ParentFieldName="OrderNo" />
                            </RelationColumns>
                        </JQTools:JQDataForm>
                    </JQTools:JQDialog>
                </JQTools:JQDialog>
                <br />
                &nbsp;<a class="easyui-linkbutton" ID="linkStickStatus" href="#" onclick="OpenStickStatus()">查看挑工進度維護</a>

            </JQTools:JQDialog>

        </div>
    </form>
</body>
</html>
