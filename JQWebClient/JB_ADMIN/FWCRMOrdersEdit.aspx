﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FWCRMOrdersEdit.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script>       
        var parameter = "";

        function OnLoadSuccessOrders() {

            //是否駐廠 + 天數
            var IsOnSite = $('#dataFormOrdersIsOnSite').closest('td');
            var SiteDays = $('#dataFormOrdersOnSiteDays').closest('td').children();
            IsOnSite.append("駐廠天數&nbsp;&nbsp;").append(SiteDays).append("&nbsp;天");

            //付費模式 + 雇主支付費用分攤連結
            var HotelType = $('#dataFormOrdersHotelType').closest('td');
            var HotelEmployer = $('#dataFormOrdersHotelEmployer').closest('td').children();
            var HotelAgent = $('#dataFormOrdersHotelAgent').closest('td').children();
            var HotelEmployee = $('#dataFormOrdersHotelEmployee').closest('td').children();
            HotelType.append("&nbsp;").append(HotelEmployer).append("&nbsp;元、仲介支付").append(HotelAgent).append("&nbsp;元、移工支付").append(HotelEmployee).append("&nbsp;元");

            //接工方式連接
            var TakeTypeOne = $('#dataFormOrdersTakeTypeOne').closest('td');
            var TypeOneNo = $('#dataFormOrdersTypeOneNo').closest('td').children();
            var TakeTypeTree = $('#dataFormOrdersTakeTypeTree').closest('td').children();
            TakeTypeOne.append("第一順位，核函號碼：").append(TypeOneNo).append("&nbsp;&nbsp;&nbsp;").append(TakeTypeTree).append("第三順位");

            //預設不顯示
            //1.國外仲介
            $("#dataFormOrderssup_no").closest('td').prev('td').hide();
            $("#dataFormOrderssup_no").closest('td').hide();
            //2.結案日期	
            $("#dataFormOrdersCloseDate").closest('td').prev('td').hide();
            $("#dataFormOrdersCloseDate").closest('td').hide();


            ////取得流程狀態=>控制顯示項目
            parameter = Request.getQueryStringByName("D");


            //--------------------------------------★Need Update-----------------------------------------------
            //parameter = "Print2";//Need Update
            //----------------------------------------------------------------------------------------------------
            
            //--------------------------------------入境 控管-----------------------------------------------------------------------------------
            var sUserID = getClientInfo("UserID");

            //入境 主管審核(角色為協理時)=>顯示 國外仲介,顯示紅色必選狀態 005=>副總
            if (sUserID == "005") {//入境主管審核=>顯示 國外仲介,顯示紅色必選狀態
                $("#dataFormOrderssup_no").closest('td').prev('td').show();
                $("#dataFormOrderssup_no").closest('td').show();
                $('#dataFormOrderssup_no').closest('td').prev('td').css("color", "red");
                $("#dataFormOrderssup_no").refval("enable");
            }

          

            //入境 => 顯示業務列印訂單 ; 轉接或承接 => 顯示業務列印訂單
            if (parameter == "Print" || parameter == "Print2") {
                var PrintLink = $('<a>', { href: 'javascript:void(0)', name: 'OrdersPrintLink', onclick: 'OpeneReportOrders.call(this)' }).linkbutton({ plain: false, text: '訂單列印' })[0].outerHTML
                var tdOrderNo = $('#dataFormOrdersWorkImg').closest('td');
                tdOrderNo.append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + PrintLink);
            }

            //--------------------------------------轉接或承接 控管-------------------------------------------------------------------------

            //轉接或承接 外勞會計結案 => 顯示結案日期
            if (parameter == "FwcrmClose") {//入境主管審核=>顯示 國外仲介,顯示紅色必選狀態
                $("#dataFormOrdersCloseDate").closest('td').prev('td').show();
                $("#dataFormOrdersCloseDate").closest('td').show();
                $('#dataFormOrdersCloseDate').closest('td').prev('td').css("color", "red");
            }

            //訂單類型控制=>續聘文件
            var value = $('#dataFormOrdersOrderType').options('getCheckedValue');
            if (value != 4) {
                //隱藏
                $('#dataFormOrdersContinueFile').closest('td').prev('td').hide();
                $('#dataFormOrdersContinueFile').closest('td').hide();
            } else {
                //顯示
                $('#dataFormOrdersContinueFile').closest('td').prev('td').show();
                $('#dataFormOrdersContinueFile').closest('td').show();
            }
            //訂單類型控制=>入境=>才顯示國外仲介
            var value = $('#dataFormOrdersOrderType').options('getCheckedValue');
            if (value == 1) {
                //顯示
                $('#dataFormOrderssup_no').closest('td').prev('td').show();
                $('#dataFormOrderssup_no').closest('td').show();                
            } else {
                //隱藏
                $('#dataFormOrderssup_no').closest('td').prev('td').hide();
                $('#dataFormOrderssup_no').closest('td').hide();
            }

        }
        

        //Grid明細修改權限判斷
        function OnUpdateDetail() {
            //入境 函號輸入
            if (parameter != "Input") {//函號輸入
                alert('不可編輯！')
                return false;
            }
        }

        //存檔前檢查
        function checkApplyData() {

            //--------------------------------------入境 檢查-----------------------------------------------------------------------------------
            //1.主管審核時 => 檢查國外仲介必選

            var sUserID = getClientInfo("UserID");
            if (sUserID == "005") {//夏玉萍
                //1.檢查國外仲介必選
                var sup_no = $("#dataFormOrderssup_no").refval('getValue');
                if (sup_no == "") {
                    var value = $('#dataFormOrdersOrderType').options('getCheckedValue');
                    //訂單類型1入境 =>需選擇
                    if (value == 1) {
                        alert("請選擇國外仲介！");
                        $("#dataFormOrderssup_no").data("inforefval").refval.find("input.refval-text").focus();
                        return false;
                    }
                }
            }

            parameter = Request.getQueryStringByName("D");
            if (parameter == "Print2" && sUserID != "005") {
                $("#dataFormOrderssup_no").refval("disable");
                alert("提醒您，請選擇發送者角色: 業務專員 。");
            }
            //2.函號輸入 => 檢查函號是否都輸入
            //if (parameter == "Input") {
            //    var iCount = 0;
            //    var data = $('#dataGridDetail').datagrid('getData');
            //    var rows = data.rows;
            //    for (var i = 0; i < data.total; i++) {
            //        if (rows[i].org_okno == " ") {
            //            iCount = iCount+1;
            //        }                    
            //    }
            //    if (iCount != 0) {
            //        alert("請填寫函號！");
            //        return false;
            //    }
            //}

            //--------------------------------------轉接或承接 檢查-------------------------------------------------------------------------
            //1.外勞會計結案 => 檢查結案日期必選

            if (parameter == "FwcrmClose") {
                var CloseDate = $("#dataFormOrdersCloseDate").datebox('getValue');
                if (CloseDate == "" || CloseDate == undefined) {
                    alert("請選擇結案日期！");
                    return false;
                }
            }
        }

        
        //呼叫Report視窗
        function OpeneReportOrders() {
            var OrderNo = $('#dataFormOrdersOrderNo').val();
            var OrderType = $('#dataFormOrdersOrderType').options('getValue');//訂單類型	
            var url = "../JB_ADMIN/REPORT/FWCRM/OrdersReportView.aspx?OrderNo=" + OrderNo + "&OrderType=" + OrderType;           

            var height = $(window).height() - 40;
            var height2 = $(window).height() - 80;
            var width = $(window).width() - 90;
            var dialog = $('<div/>')
            .dialog({
                draggable: false,
                modal: true,
                height: height,
                //top:0,
                width: width,
                title: "訂單列印",
                //maximizable: true                              
            });
            $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="' + height2 + '"></iframe>').appendTo(dialog.find('.panel-body'));
            dialog.dialog('open');

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

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormOrders" Title="外籍勞工訂單" Width="800px" DialogTop="40px">
                <JQTools:JQDataForm ID="dataFormOrders" runat="server" DataMember="FWCRMOrders" HorizontalColumnsCount="4" RemoteName="sFWCRMOrders.FWCRMOrders" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="True" IsAutoPause="False" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="True" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnLoadSuccess="OnLoadSuccessOrders" OnApply="checkApplyData" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="訂單編號" Editor="text" FieldName="OrderNo" Format="" Width="120" ReadOnly="True" NewRow="False" Visible="True" Span="3" />
                        <JQTools:JQFormColumn Alignment="left" Caption="來源訂單" Editor="text" FieldName="FromOrderNo" Format="" Width="120" EditorOptions="" NewRow="False" ReadOnly="True" Visible="False" MaxLength="0" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="訂單類型" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:320,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:5,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'入境',value:'1'},{text:'承接',value:'2'},{text:'轉單',value:'3'},{text:'轉單續聘',value:'4'},{text:'代招',value:'5'}]" FieldName="OrderType" MaxLength="0" NewRow="True" ReadOnly="True" RowSpan="1" Span="3" Visible="True" Width="250" />
                        <JQTools:JQFormColumn Alignment="left" Caption="負責業務" Editor="text" FieldName="NAME_C" Width="80" ReadOnly="True" NewRow="False" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="訂單狀態" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:270,remoteName:'sFWCRMOrders.infoOrderStatus',tableName:'infoOrderStatus',valueField:'ID',textField:'Name',columnCount:3,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[]" FieldName="OrderStatus" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="150" />
                        <JQTools:JQFormColumn Alignment="left" Caption="訂單年度" Editor="numberbox" FieldName="OrderYear" MaxLength="0" ReadOnly="False" Visible="True" Width="60" />
                        <JQTools:JQFormColumn Alignment="left" Caption="雇主名稱" Editor="infocombobox" FieldName="EmployerID" Format="" Width="180" EditorOptions="valueField:'EmployerID',textField:'EmployerName',remoteName:'sFWCRMOrders.infoEmployerID',tableName:'infoEmployerID',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" ReadOnly="True" MaxLength="0" NewRow="True" Span="3" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聘工表號碼" Editor="text" EditorOptions="" FieldName="WorkNo" Format="" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="引進國別" Editor="infocombobox" EditorOptions="valueField:'AutoKey',textField:'NationalityName',remoteName:'sFWCRMOrders.infoFWCRMNationality',tableName:'infoFWCRMNationality',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="NationalityID" Format="" MaxLength="0" NewRow="True" ReadOnly="True" Visible="True" Width="180" Span="3" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聘工表檔案" Editor="text" FieldName="WorkImg" MaxLength="100" Width="150" EditorOptions="" NewRow="False" Visible="True" Format="download,folder:Files/FWCRM/Orders" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="續聘文件" Editor="text" FieldName="ContinueFile" MaxLength="100" Width="190" EditorOptions="" NewRow="False" Visible="True" Format="download,folder:Files/FWCRM/Orders" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="是否駐廠" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsOnSite" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="50" />
                        <JQTools:JQFormColumn Alignment="left" Caption="防疫旅館需求" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsHotel" MaxLength="0" ReadOnly="False" Visible="True" Width="50" NewRow="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="numberbox" FieldName="OnSiteDays" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="60" />
                        <JQTools:JQFormColumn Alignment="left" Caption="付費模式" Editor="infocombobox" EditorOptions="items:[{value:'1',text:'雇主全額支付',selected:'false'},{value:'2',text:'移工全額支付',selected:'false'},{value:'3',text:'雇主支付',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:130" FieldName="HotelType" NewRow="False" Span="4" Visible="True" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="numberbox" FieldName="HotelEmployer" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="numberbox" FieldName="HotelAgent" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="numberbox" FieldName="HotelEmployee" MaxLength="0" NewRow="False" ReadOnly="False" Span="1" Visible="True" Width="50" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="接工方式" Editor="checkbox" FieldName="TakeTypeOne" NewRow="True" ReadOnly="False" Span="4" Visible="True" Width="30" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="TypeOneNo" MaxLength="0" NewRow="True" ReadOnly="False" Span="1" Width="135" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="checkbox" FieldName="TakeTypeTree" MaxLength="0" NewRow="False" ReadOnly="False" Span="1" Visible="True" Width="30" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" NewRow="False" Visible="False" MaxLength="0" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="國外仲介" Editor="inforefval" FieldName="sup_no" Width="350" Visible="True" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="4" EditorOptions="title:'選擇國外仲介',panelWidth:450,panelHeight:290,remoteName:'sFWCRMOrders.infosup',tableName:'infosup',columns:[{field:'sup_cname',title:'仲介名稱',width:420,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'sup_no',title:'仲介代號',width:90,align:'center',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[{field:'sup_cname',value:'sup_cname'}],whereItems:[],valueField:'sup_no',textField:'sup_cname',valueFieldCaption:'仲介代號',textFieldCaption:'仲介名稱',cacheRelationText:false,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" />
                        <JQTools:JQFormColumn Alignment="left" Caption="sup_cname" Editor="text" FieldName="sup_cname" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="結案日期" Editor="datebox" FieldName="CloseDate" MaxLength="0" NewRow="True" ReadOnly="False" Width="90" />
                    </Columns>
                </JQTools:JQDataForm>

                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="False" DataMember="FWCRMOrdersDetails" EditDialogID="JQDialog2" Pagination="False" ParentObjectID="dataFormOrders" RemoteName="sFWCRMOrders.FWCRMOrders" Title="" AllowAdd="True" AllowDelete="False" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnUpdate="OnUpdateDetail" >

                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="批次" Editor="text" FieldName="Item" Format="" Width="30" />
                        <JQTools:JQGridColumn Alignment="left" Caption="預定年月" Editor="text" FieldName="PlanIndate" Format="" Width="56" />
                        <JQTools:JQGridColumn Alignment="right" Caption="訂單人數" Editor="text" FieldName="PersonQtyOriginal" Format="" Width="56" Total="sum" />
                        <JQTools:JQGridColumn Alignment="right" Caption="目前人數" Editor="text" FieldName="PersonQtyFinal" Width="56" Total="sum" Visible="True" />
                        <JQTools:JQGridColumn Alignment="center" Caption="性別" Editor="infocombobox" FieldName="Gender" Format="" Width="38" EditorOptions="items:[{value:'1',text:'女',selected:'false'},{value:'2',text:'男',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQGridColumn Alignment="left" Caption="函號" Editor="text" FieldName="org_okno" Format="" Width="83" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="工期" Editor="text" FieldName="WorkTimeText" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="Notes" Format="" Width="200" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="OrderNo" Editor="text" FieldName="OrderNo" Visible="False" Width="80" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="sgn_no" Editor="text" FieldName="sgn_no" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="sgn_type" Editor="text" FieldName="sgn_type" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="OrderNo" ParentFieldName="OrderNo" />
                    </RelationColumns>
                </JQTools:JQDataGrid>

                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" EditMode="Dialog" Title="訂單資訊">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" ParentObjectID="dataFormOrders" DataMember="FWCRMOrdersDetails" HorizontalColumnsCount="2" RemoteName="sFWCRMOrders.FWCRMOrders" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="批次" Editor="text" FieldName="Item" Format="" Width="40" NewRow="True" ReadOnly="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="預計入境年月" Editor="text" FieldName="PlanIndate" Format="" Width="80" NewRow="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="訂單人數" Editor="numberbox" FieldName="PersonQtyOriginal" Format="" Width="80" NewRow="True" OnBlur="" ReadOnly="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="目前人數" Editor="numberbox" FieldName="PersonQtyFinal" Width="80" ReadOnly="True" NewRow="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="性別" Editor="infooptions" FieldName="Gender" Width="120" EditorOptions="title:'JQOptions',panelWidth:120,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'女',value:'1'},{text:'男',value:'2'}]" Format="" NewRow="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="函號" Editor="inforefval" FieldName="org_okno" Format="" Width="140" EditorOptions="title:'選擇函號',panelWidth:300,remoteName:'sFWCRMOrders.infoOrg_okno',tableName:'infoOrg_okno',columns:[{field:'cus_name',title:'雇主名稱',width:150,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'org_okno',title:'函號',width:110,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[{field:'sgn_no',value:'sgn_no'},{field:'sgn_type',value:'sgn_type'}],whereItems:[],valueField:'org_okno',textField:'org_okno',valueFieldCaption:'函號',textFieldCaption:'函號',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="textarea" FieldName="Notes" Format="" Width="460" EditorOptions="height:50" Span="2" NewRow="False" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="120" NewRow="True" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="OrderNo" Editor="text" FieldName="OrderNo" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="sgn_no" Editor="text" FieldName="sgn_no" NewRow="False" ReadOnly="False" Width="80" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="sgn_type" Editor="text" FieldName="sgn_type" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="OrderNo" ParentFieldName="OrderNo" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                    <JQTools:JQAutoSeq ID="JQAutoSeq1" runat="server" BindingObjectID="dataFormDetail" FieldName="Item" NumDig="0" />
                </JQTools:JQDialog>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormOrders" EnableTheming="True">
                    <Columns>
                       
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormOrders" EnableTheming="True">
                </JQTools:JQValidate>

            </JQTools:JQDialog>

        </div>
    </form>
</body>
</html>
