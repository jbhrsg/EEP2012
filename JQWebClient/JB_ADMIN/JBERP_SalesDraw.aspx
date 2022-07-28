<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_SalesDraw.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jcanvas.min.js"></script>
    <script src="../js/jbCanvasDrawRect.js"></script>
    <script>
        //=========================================控制Grid 1=0都做完才顯示Grid=========================================================================================
        var waitA = false;
        var myVar = setInterval(function () { myTimer(); }, 100);//每隔0.1秒監聽dataGridView是否第一次OnloadSuccess
        setTimeout(function () { clearTimeout(myVar); }, 6000);//最多6秒結束監聽

        function myTimer() {
            if (waitA == true) {
                RefreshGrid();           
                clearTimeout(myVar);
            }
        }
        ///=============================================  ready  ===============================================================================================
        $(document).ready(function () {
            //寬度調整
            $("#cbSalesEmployeeID").combobox('resize', '105');
            $("#cbCustNO").combobox('resize', '200');
            $("#cbSalesType").combobox('resize', '80');                       
            $("#cbCustNO").combobox('setValue', "");
            $("#cbSalesType").combobox('setValue', "1");
            var dt = new Date();
            var aDate = new Date($.jbDateAdd('days', 0, dt));//取得今天日期
            $("#JQDate1").datebox('setValue', $.jbjob.Date.DateFormat(aDate, 'yyyy/MM/dd'));
            $("#JQDate2").datebox('setValue', $.jbjob.Date.DateFormat(aDate, 'yyyy/MM/dd'));
            //=============================================下面查詢條件=========================================================================================
            $('#JQDate1').datebox({
                width: 90,
                onSelect: function (date) {
                    //$('#JQDate2').datebox({ 'setValue': $.jbjob.Date.DateFormat(date, 'yyyy/MM/dd') });
                    $("#JQDate2").combo('textbox').val($.jbjob.Date.DateFormat(date, 'yyyy/MM/dd'));
                    RefreshGrid();
                }
            }).combo('textbox').blur(function () {
                setTimeout(function () {
                    RefreshGrid();
                }, 500);
            });
            $('#JQDate2').datebox({
                width: 90,
                onSelect: function (date) {
                    RefreshGrid();
                }
            }).combo('textbox').blur(function () {
                setTimeout(function () {
                    RefreshGrid();
                }, 500);
            });
        });
        //OnSelect業務人員
        function cbSalesEmployeeIDRefresh() {
            RefreshGrid();
        }
        //OnSelect客戶代號
        function cbCustNORefresh(rowData) {
            //選完客戶時直接帶入所選擇客戶的業務   
            var setvalue = rowData.SalesID;
            var data = $("#cbSalesEmployeeID").combobox('getData');
            if (data.length > 0) {
                $("#cbSalesEmployeeID").combobox('setValue', setvalue);
            }
            RefreshGrid();
        }
        //OnSelect銷貨類型
        function cbSalesTypeRefresh() {
            RefreshGrid();
        }
        //==================================================上面查詢條件=====================================================================================

        function OnLoadSuccessdataGridView() {
            if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) {
                waitA = true;
            }
        }
        //第一次OnLoadSuccessdataGridView或OnSelect日期、業務人員、客戶代號、銷貨類型
        function RefreshGrid() {
            var SalesEmployeeID = $("#cbSalesEmployeeID").combobox('getValue');
            var CustNO = $("#cbCustNO").combobox('getValue');
            var SalesType = $("#cbSalesType").combobox('getValue');
            var JQDate1 = $("#JQDate1").combo('textbox').val();//datebox("getBindingValue");//datebox("getValue");                
            var JQDate2 = $("#JQDate2").combo('textbox').val();
            var where = $("#dataGridView").datagrid('getWhere');
            where = where + " d.SalesDate between '" + JQDate1 + "' and '" + JQDate2 + "'";
            if (SalesEmployeeID != "") {//員工工號
                where = where + " and d.SalesID='" + SalesEmployeeID + "'";
            }
            if (CustNO != "==不拘==" && CustNO != "") {//客戶編號
                where = where + " and m.CustNO='" + CustNO + "'";
            }
            if (SalesType != "") {//銷貨類型(ex:求才、便利)
                if (SalesType == "1") {
                    where = where + " and d.SalesTypeID=1 and GrantTypeID!='+'";
                } else if (SalesType == "31") {
                    where = where + " and (d.SalesTypeID=31 or (d.SalesTypeID=1 and GrantTypeID='+'))";
                }
            }
            $("#dataGridView").datagrid('setWhere', where);
        }

        //FormatScript匯入、需發票、有效的值回傳checkbox
        function genCheckBox(val) {
            if (val != "0")
                return "<input  type='checkbox' checked='true' onclick='return false;'/>";
            else
                return "<input  type='checkbox' onclick='return false;'/>";
        }
        //=============================================畫框=========================================================================================
        $(function () {
            //畫畫屬性設定
            $('canvas').jbCanvasDrawRect({ strokeStyle: 'red', strokeWidth: 5, drawGroupName: 'showGroupName', showGroupName: 'showGroupName' });
            $('canvas').jbCanvasDrawRect('drawEnable', true);
        });

        //OnClick正面(A)或反面(B)或共版(C)按鈕
        function OnClickPaperButton(index, Frame) {
            $("#dataGridView").datagrid('selectRow', index);
            $('.canvas1').jbCanvasDrawRect('clearAll');
            $('.canvas31').jbCanvasDrawRect('clearAll');
            $('.canvas31-1').jbCanvasDrawRect('clearAll');
            $('.canvas31-2').jbCanvasDrawRect('clearAll');
            $("#JQTextBox1").val(Frame);//代表所按的按鈕是正面(A)或反面(B)或共版(C)
            openForm('#JQDialog1', $("#dataGridView").datagrid('getSelected'), "updated", 'dialog');
        }
            
        function OnLoadSuccessJQDataForm1(FormData) {
            var SalesDate = FormData.SalesDate.substr(0, 10);
            var SalesTypeID = FormData.SalesTypeID;//打開form才會判斷顯示求才還是便利報畫布
            var GrantTypeID = FormData.GrantTypeID;
            var AFrameXY = FormData.AFrameXY;
            var BFrameXY = FormData.BFrameXY;
            var CFrameXY = FormData.CFrameXY;
            var Frame = $("#JQTextBox1").val();
            $("#JQTextBox1").hide();

            var ddSalesDate = new Date(SalesDate);
            var dd31newDate = new Date('2018-03-08');
            var dd31newDate2 = new Date('2018-03-15');
            var dd31newDate1018 = new Date('2018-10-18');
            //var dd31newDate = new Date('2016-10-08');
            //var dd31newDate2 = new Date('2016-10-15');

            //畫報紙,畫框
            if (SalesTypeID == '1' && GrantTypeID != "+") {//求才報
                $('.canvas31').hide();
                $('.canvas31-1').hide();
                $('.canvas31-2').hide();
                $('.canvas1').show();//顯示求才畫布
                if (Frame == 'A') {//畫正面和正面座標
                    DrawImage("../Files/JBERP_SalesPaper/SalesType1_A/" + SalesDate + ".jpeg", ".canvas1", $("#JQDataForm1AFrameXY").val());
                } else if (Frame == 'B') {//畫反面報,反面座標
                    DrawImage("../Files/JBERP_SalesPaper/SalesType1_B/" + SalesDate + ".jpeg", ".canvas1", $("#JQDataForm1BFrameXY").val());
                } else if (Frame == 'C') {//畫共面報,共面座標
                    DrawImage("../Files/JBERP_SalesPaper/SalesType1_C/" + SalesDate + ".jpeg", ".canvas1", $("#JQDataForm1CFrameXY").val());
                }
            } else if ((SalesTypeID == '31' || (SalesTypeID == '1' && GrantTypeID == "+")) && ((ddSalesDate >= dd31newDate2) && (ddSalesDate < dd31newDate1018))) {//新版便利報2
                $('.canvas31').hide();
                $('.canvas1').hide();
                $('.canvas31-1').hide();
                $('.canvas31-2').show();
                if (Frame == 'A') {
                    DrawImage("../Files/JBERP_SalesPaper/SalesType31_A/" + SalesDate + ".jpeg", ".canvas31-2", $("#JQDataForm1AFrameXY").val());
                } else if (Frame == 'B') {
                    DrawImage("../Files/JBERP_SalesPaper/SalesType31_B/" + SalesDate + ".jpeg", ".canvas31-2", $("#JQDataForm1BFrameXY").val());
                }
            } else if ((SalesTypeID == '31' || (SalesTypeID == '1' && GrantTypeID == "+")) && (ddSalesDate >= dd31newDate && ddSalesDate < dd31newDate2)) {//新版便利報
                $('.canvas31').hide();
                $('.canvas1').hide();
                $('.canvas31-2').hide();
                $('.canvas31-1').show();
                if (Frame == 'A') {
                    DrawImage("../Files/JBERP_SalesPaper/SalesType31_A/" + SalesDate + ".jpeg", ".canvas31-1", $("#JQDataForm1AFrameXY").val());
                } else if (Frame == 'B') {
                    DrawImage("../Files/JBERP_SalesPaper/SalesType31_B/" + SalesDate + ".jpeg", ".canvas31-1", $("#JQDataForm1BFrameXY").val());
                }
            } else if ((SalesTypeID == '31' || (SalesTypeID == '1' && GrantTypeID == "+")) && ((ddSalesDate < dd31newDate) || (ddSalesDate >= dd31newDate1018))) {//便利報
                $('.canvas1').hide();
                $('.canvas31-1').hide();
                $('.canvas31-2').hide();
                $('.canvas31').show();
                if (Frame == 'A') {
                    DrawImage("../Files/JBERP_SalesPaper/SalesType31_A/" + SalesDate + ".jpeg", ".canvas31", $("#JQDataForm1AFrameXY").val());
                } else if (Frame == 'B') {
                    DrawImage("../Files/JBERP_SalesPaper/SalesType31_B/" + SalesDate + ".jpeg", ".canvas31", $("#JQDataForm1BFrameXY").val());
                }
            }
        }
        //把畫框座標放到DataForm的座標欄位
        function OnApplyJQDataForm1() {
            var LocationList;
            var temp;
            var SalesTypeID = $("#JQDataForm1SalesTypeID").val();
            var GrantTypeID = $("#JQDataForm1GrantTypeID").val();
            var Frame = $("#JQTextBox1").val();

            var ddSalesDate = new Date($("#JQDataForm1SalesDate").val()).getTime();
            var dd31newDate = new Date("Thu Mar 08 2018 00:00:00 GMT+0800 (Taiper Time)").getTime();
            var dd31newDate2 = new Date("Thu Mar 15 2018 00:00:00 GMT+0800 (Taiper Time)").getTime();
            var dd31newDate1018 = new Date("Thu Oct 18 2018 00:00:00 GMT+0800 (Taiper Time)").getTime();

            if (SalesTypeID == '1' && GrantTypeID != "+") {//求才報
                if (Frame == 'A') {//正面
                    LocationList = $(".canvas1").jbCanvasDrawRect('getLocationList');//取得顯示畫布的畫框座標//LocationList =[object Object],[object Object]
                    //temp = $.toJSONString(LocationList);//temp =[[{"x":245,"y":3},{"x":370,"y":148}]]
                    temp = '[[{"x":' + Math.floor(LocationList[0][0].x) + ',"y":' + Math.floor(LocationList[0][0].y) + '},{"x":' + Math.floor(LocationList[0][1].x) + ',"y":' + Math.floor(LocationList[0][1].y) + '}]]';
                    //rows = $.parseJSON(temp);
                    //把畫框座標放到DataForm正面座標欄位(再存到該筆銷貨單)//不點、點一下或點兩下的座標會放空白到DataForm正面座標欄位
                    if (temp != "[]" && (LocationList[0][0].x != LocationList[0][1].x) && (LocationList[0][0].y != LocationList[0][1].y)) { $("#JQDataForm1AFrameXY").val(temp); }//$.toJSONString(LocationList)=[[{"x":251,"y":154},{"x":306,"y":257}]]
                    else { $("#JQDataForm1AFrameXY").val(""); }
                } else if (Frame == 'B') {
                    LocationList = $(".canvas1").jbCanvasDrawRect('getLocationList');
                    temp = '[[{"x":' + Math.floor(LocationList[0][0].x) + ',"y":' + Math.floor(LocationList[0][0].y) + '},{"x":' + Math.floor(LocationList[0][1].x) + ',"y":' + Math.floor(LocationList[0][1].y) + '}]]';
                    if (temp != "[]" && (LocationList[0][0].x != LocationList[0][1].x) && (LocationList[0][0].y != LocationList[0][1].y)) { $("#JQDataForm1BFrameXY").val(temp); }
                    else { $("#JQDataForm1BFrameXY").val(""); }
                } else if (Frame == 'C') {
                    LocationList = $(".canvas1").jbCanvasDrawRect('getLocationList');
                    temp = '[[{"x":' + Math.floor(LocationList[0][0].x) + ',"y":' + Math.floor(LocationList[0][0].y) + '},{"x":' + Math.floor(LocationList[0][1].x) + ',"y":' + Math.floor(LocationList[0][1].y) + '}]]';
                    if (temp != "[]" && (LocationList[0][0].x != LocationList[0][1].x) && (LocationList[0][0].y != LocationList[0][1].y)) { $("#JQDataForm1CFrameXY").val(temp); }
                    else { $("#JQDataForm1CFrameXY").val(""); }
                }
            } else if ((SalesTypeID == '31' || (SalesTypeID == '1' && GrantTypeID == "+")) && ((ddSalesDate >= dd31newDate2) && (ddSalesDate < dd31newDate1018))) {//新版便利報2
                if (Frame == 'A') {
                    LocationList = $(".canvas31-2").jbCanvasDrawRect('getLocationList');
                    temp = '[[{"x":' + Math.floor(LocationList[0][0].x) + ',"y":' + Math.floor(LocationList[0][0].y) + '},{"x":' + Math.floor(LocationList[0][1].x) + ',"y":' + Math.floor(LocationList[0][1].y) + '}]]';
                    if (temp != "[]" && (LocationList[0][0].x != LocationList[0][1].x) && (LocationList[0][0].y != LocationList[0][1].y)) { $("#JQDataForm1AFrameXY").val(temp); }//$.toJSONString(LocationList)=[[{"x":251,"y":154},{"x":306,"y":257}]]
                    else { $("#JQDataForm1AFrameXY").val(""); }
                } else if (Frame == 'B') {
                    LocationList = $(".canvas31-2").jbCanvasDrawRect('getLocationList');
                    temp = '[[{"x":' + Math.floor(LocationList[0][0].x) + ',"y":' + Math.floor(LocationList[0][0].y) + '},{"x":' + Math.floor(LocationList[0][1].x) + ',"y":' + Math.floor(LocationList[0][1].y) + '}]]';
                    if (temp != "[]" && (LocationList[0][0].x != LocationList[0][1].x) && (LocationList[0][0].y != LocationList[0][1].y)) { $("#JQDataForm1BFrameXY").val(temp); }
                    else { $("#JQDataForm1BFrameXY").val(""); }
                }
            } else if ((SalesTypeID == '31' || (SalesTypeID == '1' && GrantTypeID == "+")) && (ddSalesDate >= dd31newDate && ddSalesDate < dd31newDate2)) {//新版便利報
                if (Frame == 'A') {
                    LocationList = $(".canvas31-1").jbCanvasDrawRect('getLocationList');
                    temp = '[[{"x":' + Math.floor(LocationList[0][0].x) + ',"y":' + Math.floor(LocationList[0][0].y) + '},{"x":' + Math.floor(LocationList[0][1].x) + ',"y":' + Math.floor(LocationList[0][1].y) + '}]]';
                    if (temp != "[]" && (LocationList[0][0].x != LocationList[0][1].x) && (LocationList[0][0].y != LocationList[0][1].y)) { $("#JQDataForm1AFrameXY").val(temp); }//$.toJSONString(LocationList)=[[{"x":251,"y":154},{"x":306,"y":257}]]
                    else { $("#JQDataForm1AFrameXY").val(""); }
                } else if (Frame == 'B') {
                    LocationList = $(".canvas31-1").jbCanvasDrawRect('getLocationList');
                    temp = '[[{"x":' + Math.floor(LocationList[0][0].x) + ',"y":' + Math.floor(LocationList[0][0].y) + '},{"x":' + Math.floor(LocationList[0][1].x) + ',"y":' + Math.floor(LocationList[0][1].y) + '}]]';
                    if (temp != "[]" && (LocationList[0][0].x != LocationList[0][1].x) && (LocationList[0][0].y != LocationList[0][1].y)) { $("#JQDataForm1BFrameXY").val(temp); }
                    else { $("#JQDataForm1BFrameXY").val(""); }
                }
            } else if ((SalesTypeID == '31' || (SalesTypeID == '1' && GrantTypeID == "+")) && ((ddSalesDate < dd31newDate) || (ddSalesDate >= dd31newDate1018))) {//便利報
                if (Frame == 'A') {
                    LocationList = $(".canvas31").jbCanvasDrawRect('getLocationList');
                    temp = '[[{"x":' + Math.floor(LocationList[0][0].x) + ',"y":' + Math.floor(LocationList[0][0].y) + '},{"x":' + Math.floor(LocationList[0][1].x) + ',"y":' + Math.floor(LocationList[0][1].y) + '}]]';
                    if (temp != "[]" && (LocationList[0][0].x != LocationList[0][1].x) && (LocationList[0][0].y != LocationList[0][1].y)) { $("#JQDataForm1AFrameXY").val(temp); }//$.toJSONString(LocationList)=[[{"x":251,"y":154},{"x":306,"y":257}]]
                    else { $("#JQDataForm1AFrameXY").val(""); }
                } else if (Frame == 'B') {
                    LocationList = $(".canvas31").jbCanvasDrawRect('getLocationList');
                    temp = '[[{"x":' + Math.floor(LocationList[0][0].x) + ',"y":' + Math.floor(LocationList[0][0].y) + '},{"x":' + Math.floor(LocationList[0][1].x) + ',"y":' + Math.floor(LocationList[0][1].y) + '}]]';
                    if (temp != "[]" && (LocationList[0][0].x != LocationList[0][1].x) && (LocationList[0][0].y != LocationList[0][1].y)) { $("#JQDataForm1BFrameXY").val(temp); }
                    else { $("#JQDataForm1BFrameXY").val(""); }
                }
            }
            return true;
        }
        //座標欄位存檔完，Grid重load資料
        function OnAppliedJQDataForm1() {
            $("#dataGridView").datagrid('reload');
        }
        
        //DataGrid的A面畫框欄位值回傳(有畫框或沒畫框)的linkbutton
        function FormatScriptButtonA(val, row, index) {
            if (val == null || val == "" || val == "[]") { return $('<a>', { href: 'javascript:void(0)', onclick: 'OnClickPaperButton(' + index + ',"A")', style: '{ color: red }', color: 'red', fontcolor: 'red' }).linkbutton({ width: '200', text: 'A面', color: 'red' })[0].outerHTML; }
            else { return $('<a>', { href: 'javascript:void(0)', onclick: 'OnClickPaperButton(' + index + ',"A")', style: '{ color: red }' }).linkbutton({ text: '    ', iconCls:'icon-ok' })[0].outerHTML; }
        }
        //DataGrid的B面畫框欄位值回傳linkbutton
        function FormatScriptButtonB(val, row, index) {
            if (val == null || val == "" || val == "[]") { return $('<a>', { href: 'javascript:void(0)', onclick: 'OnClickPaperButton(' + index + ',"B")' }).linkbutton({ text: 'B面', width: '200px' })[0].outerHTML; }
            //if (val == null || val == "" || val == "[]") { return "<input type='button'  value='B面' style='border-radius:10px;background-color:pink;width:70px;height:20px;border:2px;' onclick=OnClickPaperButton('" + index + "','B')>" }
            else { return $('<a>', { href: 'javascript:void(0)', onclick: 'OnClickPaperButton(' + index + ',"B")' }).linkbutton({ text: '    ', iconCls: 'icon-ok' })[0].outerHTML; }
        }
        //DataGrid的C面畫框欄位值回傳linkbutton
        function FormatScriptButtonC(val, row, index) {
            if ($("#cbSalesType").combo('getValue') != '31') {
                if (val == null || val == "" || val == "[]") { return $('<a>', { href: 'javascript:void(0)', onclick: 'OnClickPaperButton(' + index + ',"C")' }).linkbutton({ text: '共版' })[0].outerHTML; }
                else { return $('<a>', { href: 'javascript:void(0)', onclick: 'OnClickPaperButton(' + index + ',"C")' }).linkbutton({ text: '     ', iconCls: 'icon-ok' })[0].outerHTML; }
            }
        }
        function DrawImage(FilePath, Canvas, Coordinate) {
            //查詢夾報圖檔(使用ajax)是否存在，存在就畫報
            $.ajax({
                url: FilePath,
                async: false,
                type: "HEAD",//get status code ex:404(not found)
                success: function () {//FilePath存在
                    $(Canvas).jbCanvasDrawRect('loadImage', FilePath);//畫報
                    setTimeout(function () {
                        if (Coordinate != "") { $(Canvas).jbCanvasDrawRect('setLocationList', $.parseJSON(Coordinate)); }//畫框
                    }, 1000);
                },
                error: function () {//FilePath不存在
                    alert("夾報不存在");
                    closeForm('#JQDialog1');
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <div>
            <asp:Label ID="Label3" runat="server" Font-Size="Small" Text="客戶代號:"></asp:Label>
            <JQTools:JQComboBox ID="cbCustNO" runat="server" DisplayMember="CustShortName" RemoteName="sERPSalesDraw.infoCustomersAll" ValueMember="CustNO" OnSelect="cbCustNORefresh">
            </JQTools:JQComboBox>           
            &nbsp; <asp:Label ID="Label1" runat="server" Font-Size="Small" Text="業務:"></asp:Label>
            <JQTools:JQComboBox ID="cbSalesEmployeeID" runat="server" DisplayMember="SalesName" PanelHeight="150" RemoteName="sERPSalesDraw.infoSalesMan" ValueMember="SalesID" Width="50px" OnSelect="cbSalesEmployeeIDRefresh">
            </JQTools:JQComboBox>
&nbsp;<asp:Label ID="Label4" runat="server" Font-Size="Small" Text="交易別:"></asp:Label>
            <JQTools:JQComboBox ID="cbSalesType" runat="server" DisplayMember="SalesTypeName" RemoteName="sERPSalesDraw.infoERPSalesType" ValueMember="SalesTypeID" OnSelect="cbSalesTypeRefresh">
            </JQTools:JQComboBox>
            <asp:Label ID="Label2" runat="server" Font-Size="Small" Text="起訖日期:"></asp:Label>
            <JQTools:JQDateBox ID="JQDate1" runat="server" Width="100px" />
            〜<JQTools:JQDateBox ID="JQDate2" runat="server" />
            </div>

            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERPSalesDraw.ERPSalesDetails" runat="server" AutoApply="True"
                DataMember="ERPSalesDetails" Pagination="True" QueryTitle="Query"
                Title="銷貨清單" OnLoadSuccess="OnLoadSuccessdataGridView" AllowAdd="False" AllowDelete="False" AllowUpdate="False" ViewCommandVisible="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" EditDialogID="JQDialog1">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="" FieldName="CustNO" Format="" Width="90" Visible="True" Frozen="False" ReadOnly="True" Sortable="True" IsNvarChar="False" MaxLength="0" QueryCondition="" />
                    <JQTools:JQGridColumn Alignment="right" Caption="SalesMasterNO" Editor="numberbox" FieldName="SalesMasterNO" Format="" Width="50" Visible="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                    <JQTools:JQGridColumn Alignment="center" Caption="項次" Editor="text" FieldName="ItemSeq" Format="" Width="59" ReadOnly="True" Visible="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" />                        
                    <JQTools:JQGridColumn Alignment="center" Caption="客戶名稱" Editor="text" FieldName="CustShortName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="140">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="區域" Editor="infocombogrid" FieldName="ViewAreaID" Format="" Width="80" EditorOptions="panelWidth:120,valueField:'ViewAreaID',textField:'ViewAreaName',remoteName:'sERPSalseDetails.infoERPViewArea',tableName:'infoERPViewArea',valueFieldCaption:'ViewAreaID',textFieldCaption:'ViewAreaName',selectOnly:false,checkData:false,columns:[{field:'ViewAreaID',title:'代號',width:40,align:'left',sortable:false},{field:'ViewAreaName',title:'名稱',width:55,align:'left',sortable:false}],cacheRelationText:false" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" />                  
                    <JQTools:JQGridColumn Alignment="center" Caption="銷貨日期" Editor="datebox" FieldName="SalesDate" Format="yyyy-mm-dd" Width="65" FormatScript="" Frozen="False" Sortable="True" ReadOnly="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Visible="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="星期" Editor="" FieldName="dWeekday" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="True" Visible="True" Width="30" />
                    <JQTools:JQGridColumn Alignment="center" Caption="A面畫框" Editor="text" FieldName="AFrameXY" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" FormatScript="FormatScriptButtonA">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="B面畫框" Editor="text" FieldName="BFrameXY" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" FormatScript="FormatScriptButtonB">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="共版畫框" Editor="text" FieldName="CFrameXY" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" FormatScript="FormatScriptButtonC">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="交易別" Editor="" FieldName="SalesTypeID" Format="" Width="40" EditorOptions="" Frozen="False" Visible="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                    <JQTools:JQGridColumn Alignment="center" Caption="版別" Editor="infocombogrid" FieldName="DMTypeID" Format="" Width="35" EditorOptions="panelWidth:165,valueField:'DMTypeID',textField:'DMTypeID',remoteName:'sERPSalseDetails.infoERPDMType',tableName:'infoERPDMType',valueFieldCaption:'DMTypeID',textFieldCaption:'DMTypeID',selectOnly:false,checkData:false,columns:[{field:'DMTypeID',title:'代號',width:40,align:'left',sortable:false},{field:'DMTypeName',title:'名稱',width:105,align:'left',sortable:false}],cacheRelationText:false" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" />                   
                    <JQTools:JQGridColumn Alignment="center" Caption="匯入" Editor="checkbox" FieldName="IsTransSys" Format="" Width="30" Visible="False" EditorOptions="on:1,off:0" FormatScript="genCheckBox" ReadOnly="True" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="匯入編號" Editor="" FieldName="depositSeq" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="55" />
                    <JQTools:JQGridColumn Alignment="center" Caption="需發票" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsInvoice" FormatScript="genCheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40" />
                    <JQTools:JQGridColumn Alignment="center" Caption="發票年月" Editor="text" FieldName="InvoiceYM" Format="" Width="50" ReadOnly="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" Visible="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="報" Editor="infocombogrid" FieldName="NewsTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="30" EditorOptions="panelWidth:165,valueField:'NewsTypeID',textField:'NewsTypeID',remoteName:'sERPSalseDetails.infoERPNewsType',tableName:'infoERPNewsType',valueFieldCaption:'NewsTypeID',textFieldCaption:'NewsTypeID',selectOnly:false,checkData:false,columns:[{field:'NewsTypeID',title:'代號',width:40,align:'left',sortable:false},{field:'NewsTypeName',title:'名稱',width:105,align:'left',sortable:false}],cacheRelationText:false" />
                    <JQTools:JQGridColumn Alignment="center" Caption="版" Editor="infocombogrid" FieldName="NewsAreaID" Format="" Width="30" Visible="False" EditorOptions="panelWidth:165,valueField:'NewsAreaID',textField:'NewsAreaID',remoteName:'sERPSalseDetails.infoERPNewsArea',tableName:'infoERPNewsArea',valueFieldCaption:'NewsAreaID',textFieldCaption:'NewsAreaID',selectOnly:false,checkData:false,columns:[{field:'NewsAreaID',title:'代號',width:40,align:'left',sortable:false},{field:'NewsAreaName',title:'名稱',width:105,align:'left',sortable:false}],cacheRelationText:false" />
                    <JQTools:JQGridColumn Alignment="center" Caption="發" Editor="infocombogrid" FieldName="NewsPublishID" Format="" Width="30" Visible="False" EditorOptions="panelWidth:165,valueField:'NewsPublishID',textField:'NewsPublishID',remoteName:'sERPSalseDetails.infoERPNewsPublish',tableName:'infoERPNewsPublish',valueFieldCaption:'NewsPublishID',textFieldCaption:'NewsPublishID',selectOnly:false,checkData:false,columns:[{field:'NewsPublishID',title:'代號',width:40,align:'left',sortable:false},{field:'NewsPublishName',title:'名稱',width:105,align:'left',sortable:false}],cacheRelationText:false" />
                    <JQTools:JQGridColumn Alignment="center" Caption="段" Editor="numberbox" FieldName="Sections" Format="" Width="20" Visible="False" ReadOnly="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="行數" Editor="numberbox" FieldName="OfficeLines" Format="" Width="28" Visible="False" ReadOnly="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="社單價" Editor="numberbox" FieldName="OfficePrice" Format="" Width="40" Visible="False" ReadOnly="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="社總價" Editor="numberbox" FieldName="OfficeAmt" Format="" Width="40" Visible="False" ReadOnly="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False"  />
                    <JQTools:JQGridColumn Alignment="center" Caption="客行數" Editor="numberbox" FieldName="CustLines" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="28" />
                    <JQTools:JQGridColumn Alignment="right" Caption="客單價" Editor="numberbox" FieldName="CustPrice" Format="" Width="40" ReadOnly="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" Visible="False" OnTotal="" />                        
                    <JQTools:JQGridColumn Alignment="right" Caption="客總額" Editor="numberbox" FieldName="CustAmt" Format="" Width="40" FormatScript="" ReadOnly="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" Visible="False" />  
                    <JQTools:JQGridColumn Alignment="center" Caption="贈期" Editor="" FieldName="GrantTypeID" Format="" Width="30" EditorOptions="" Sortable="True" ReadOnly="True" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Visible="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="單位數" Editor="text" FieldName="SalesQty" Format="" Width="40" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" />                        
                    <JQTools:JQGridColumn Alignment="center" Caption="見刊" Editor="text" FieldName="SalesQtyView" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30" />
                    <JQTools:JQGridColumn Alignment="left" Caption="有效" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="30" FormatScript="genCheckBox" />
                    <JQTools:JQGridColumn Alignment="center" Caption="出刊備註" Editor="text" FieldName="SalesDescr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100" />
                    <JQTools:JQGridColumn Alignment="center" Caption="PDF檔名" Editor="text" FieldName="Remark1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡備註" Editor="text" FieldName="ContractDescr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="100" />
                    <JQTools:JQGridColumn Alignment="right" Caption="佣金" Editor="text" FieldName="Commission" Format="" Width="30" Visible="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />   
                    <JQTools:JQGridColumn Alignment="left" Caption="業務代碼" Editor="text" FieldName="SalesEmployeeID" Format="" Width="60" Visible="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />                        
                    <JQTools:JQGridColumn Alignment="center" Caption="提醒日期" Editor="datebox" FieldName="SalesDescrDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="60" FormatScript="" Format="yyyy-mm-dd" />
                </Columns>                
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>
    <JQTools:JQDefault runat="server" BindingObjectID="JQDataForm1" BorderStyle="NotSet" Enabled="True" EnableTheming="True" ClientIDMode="Inherit" ID="defaultMaster" EnableViewState="True" ViewStateMode="Inherit" >
</JQTools:JQDefault>
<JQTools:JQValidate runat="server" BindingObjectID="JQDataForm1" BorderStyle="NotSet" Enabled="True" EnableTheming="True" ClientIDMode="Inherit" ID="validateMaster" EnableViewState="True" ViewStateMode="Inherit" >
</JQTools:JQValidate>
         <JQTools:JQDialog ID="JQDialog1" runat="server"  BindingObjectID="JQDataForm1" Title="" Width="1400px" DialogLeft="2px" DialogTop="2px" ScrollBars="None"><%-- 740 1150 --%>
             <JQTools:JQDataForm ID="JQDataForm1" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="ERPSalesDetails" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="5" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sERPSalesDraw.ERPSalesDetails" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApplied="OnAppliedJQDataForm1" OnApply="OnApplyJQDataForm1" OnLoadSuccess="OnLoadSuccessJQDataForm1">
                            <Columns>
                                <JQTools:JQFormColumn Alignment="left" Caption="SalesMasterNO" Editor="text" FieldName="SalesMasterNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="30" />
                                <JQTools:JQFormColumn Alignment="left" Caption="ItemSeq" Editor="text" FieldName="ItemSeq" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="60" />
                                <JQTools:JQFormColumn Alignment="left" Caption="A面框座標" Editor="text" FieldName="AFrameXY" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="150" />
                                <JQTools:JQFormColumn Alignment="left" Caption="B面框座標" Editor="text" FieldName="BFrameXY" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="150" />
                                <JQTools:JQFormColumn Alignment="left" Caption="共版框座標" Editor="text" FieldName="CFrameXY" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="150" />
                                <JQTools:JQFormColumn Alignment="left" Caption="SalesTypeID" Editor="text" FieldName="SalesTypeID" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                <JQTools:JQFormColumn Alignment="left" Caption="GrantTypeID" Editor="text" FieldName="GrantTypeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="10" />
                                <JQTools:JQFormColumn Alignment="left" Caption="SalesDate" Editor="text" FieldName="SalesDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            </Columns>
                        </JQTools:JQDataForm>
             <JQTools:JQTextBox ID="JQTextBox1" runat="server" Enabled="False" />
             <canvas class="canvas1" style="width: 640px; height: 453px"></canvas><%--求才畫布--%><%--760 360--%><%--2859 2024--%>
             <canvas class="canvas31" style="width: 600px; height: 806px"></canvas><%--便利畫布--%><%-- 3785 5084--%>
             <canvas class="canvas31-1" style="width: 1093px; height: 754px"></canvas><%--新版便利畫布--%><%--1093 754 875 603 673 464 3500 2413--%>
             <canvas class="canvas31-2" style="width: 1346px; height: 928px"></canvas><%--新版便利畫布--%><%--1093 754 875 603 673 464 3500 2413--%>
            </JQTools:JQDialog>
</form>
</body>
</html>
