<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_SalesDrawCustomer.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jcanvas.min.js"></script>
    <script src="../js/jbCanvasDrawRect.js"></script>
    <script>
        var CustNO = Request.getQueryStringByName("cn");
        var InvoiceYM = Request.getQueryStringByName("ym");
        //=========================================控制Grid 1=0都做完才顯示Grid=========================================================================================
        //var waitA = false;
        //var myVar = setInterval(function () {
        //    if (waitA == true) {//waitA=true代表第一次載入完
        //        RefreshGrid();//datagrid('setwhere',where)
        //        clearTimeout(myVar);//取消每隔0.1秒監聽waitA
        //    }
        //}, 100);//每隔0.1秒監聽dataGridView是否第一次載入完

        //setTimeout(function () {
        //    clearTimeout(myVar);//載入後6秒取消每隔0.1秒執行myTimer()
        //}, 6000);//最多6秒結束

        //每隔0.1秒監聽waitA是否為true(最長6秒就會結束)，若true，就refresh grid且取消每隔0.1秒監聽waitA
        //目的是dataGrid第一次OnLoadSuccess才會datagrid('setwhere',where)
        
        ///=============================================  ready  ===============================================================================================
        $(document).ready(function () {
            
            

            //日期查詢欄位更新主檔
            $('#JQDate1').datebox({
                width: 90,
                onSelect: function (date) {
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

        //OnLoadSuccessdataGridView
        function MastersetWhere() {
            if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) {//若dataGridView第一次OnLoadSuccess
                //1=0做完
                //waitA = true;//waitA=true代表dataGridView第一次OnLoadSuccess
                RefreshGrid();

                var dt = new Date();
                var aDate = new Date($.jbDateAdd('days', 0, dt));//開始日期今天
                var StartaDate = $.jbGetFirstDate(aDate);
                var LastaDate = $.jbGetLastDate(aDate);
                $("#JQDate1").datebox('setValue', $.jbjob.Date.DateFormat(StartaDate, 'yyyy/MM/dd'));
                $("#JQDate2").datebox('setValue', $.jbjob.Date.DateFormat(LastaDate, 'yyyy/MM/dd'));
            }
        }

        function RefreshGrid() {
            var JQDate1 = $("#JQDate1").combo('textbox').val();//datebox("getBindingValue");//datebox("getValue");                
            var JQDate2 = $("#JQDate2").combo('textbox').val();//datebox("getBindingValue");
            var where = $("#dataGridView").datagrid('getWhere');
            if (CustNO != undefined && CustNO != "") {
                where = where + "m.CustNO='" + CustNO + "'";

                if (InvoiceYM != undefined && InvoiceYM != "") {
                    where = where + " and d.InvoiceYM='" + InvoiceYM + "'";
                    InvoiceYM = "";
                }
                if (JQDate1 != undefined && JQDate1 != "" && JQDate2 != undefined && JQDate2 != "") {
                    where = where + " and d.SalesDate between '" + JQDate1 + "' and '" + JQDate2 + "'";
                }

            } else { where = where + "m.CustNO='99999999999'"; }
            $("#dataGridView").datagrid('setWhere', where);
        }

        //天數提醒(主檔),是否失效(明細)CheckBox=>不可以編輯
        function genCheckBox(val) {
            if (val != "0")
                return "<input  type='checkbox' checked='true' onclick='return false;'/>";
            else
                return "<input  type='checkbox' onclick='return false;'/>";
        }
        //=============================================畫框=========================================================================================

        $(function () {
            //畫畫屬性設定
            $('canvas').jbCanvasDrawRect({ strokeStyle: 'red', strokeWidth: 30, drawGroupName: 'showGroupName', showGroupName: 'showGroupName' });
            $('canvas').jbCanvasDrawRect('drawEnable', false);
            //超連結按鈕
            $('#a1').linkbutton({ text: '下載' })[0];
            $('#a2').linkbutton({ text: '關閉' })[0];
            $('#a3').linkbutton({ text: '列印' })[0];
            $("#image-wrapper").click(function () {
                $('#image1-container, #image2-container').toggle();
            });
        });

        //Grid按下按鈕
        function OnClickPaperButton(index) {
            $("#dataGridView").datagrid('selectRow', index);
            $('#image1').attr({ 'src': '' });//小圖
            $('#image2').attr({ 'src': '' });//大圖
            $('.canvas1').jbCanvasDrawRect('clearAll');
            $('.canvas31').jbCanvasDrawRect('clearAll');
            openForm('#JQDialog1', $("#dataGridView").datagrid('getSelected'), "viewed", 'dialog');
        }

        function FormatScriptButton(val, row, index) {
            if (val != null && val != "" && val != "[]" && row.SalesTypeName.trim() == "求才") { return $('<a>', { href: 'javascript:void(0)', onclick: 'OnClickPaperButton(' + index + ')' }).linkbutton({ text: '&ensp;'+row.SalesTypeName+'&ensp;' })[0].outerHTML; }
            else if (val != null && val != "" && val != "[]") { return $('<a>', { href: 'javascript:void(0)', onclick: 'OnClickPaperButton(' + index + ')' }).linkbutton({ text: row.SalesTypeName })[0].outerHTML; }
        }

        function OnLoadSuccessJQDataForm1(FormData) {
            
            var SalesDate = FormData.SalesDate.substr(0, 10);
            var SalesTypeID = FormData.SalesTypeID;
            var GrantTypeID = FormData.GrantTypeID;
            var AFrameXY = FormData.AFrameXY;
            var BFrameXY = FormData.BFrameXY;
            var CFrameXY = FormData.CFrameXY;
            

            //畫報紙,畫框
            if (SalesTypeID == '1' && GrantTypeID != "+") {//求才
                $('.canvas1').show();//顯示求才畫布
                $('.canvas31').hide();
                if (AFrameXY != "" && AFrameXY != null && AFrameXY != undefined) {
                    var LocationList = $.parseJSON(AFrameXY);
                    var temp = '[[{"x":' + (LocationList[0][0].x) * 4.4 + ',"y":' + (LocationList[0][0].y) * 4.4 + '},{"x":' + (LocationList[0][1].x) * 4.4 + ',"y":' + (LocationList[0][1].y) * 4.4 + '}]]';
                    DrawImage("../Files/JBERP_SalesPaper/SalesType1_A/" + SalesDate + ".jpeg", ".canvas1", temp, SalesDate, SalesTypeID);
                }
                else if (BFrameXY != "" && BFrameXY != null && BFrameXY != undefined) {
                    var LocationList = $.parseJSON(BFrameXY);
                    var temp = '[[{"x":' + (LocationList[0][0].x) * 4.4 + ',"y":' + (LocationList[0][0].y) * 4.4 + '},{"x":' + (LocationList[0][1].x) * 4.4 + ',"y":' + (LocationList[0][1].y) * 4.4 + '}]]';
                    DrawImage("../Files/JBERP_SalesPaper/SalesType1_B/" + SalesDate + ".jpeg", ".canvas1", temp, SalesDate, SalesTypeID);
                }
                else if (CFrameXY != "" && CFrameXY != null && CFrameXY != undefined) {
                    var LocationList = $.parseJSON(CFrameXY);
                    var temp = '[[{"x":' + (LocationList[0][0].x) * 4.4 + ',"y":' + (LocationList[0][0].y) * 4.4 + '},{"x":' + (LocationList[0][1].x) * 4.4 + ',"y":' + (LocationList[0][1].y) * 4.4 + '}]]';
                    DrawImage("../Files/JBERP_SalesPaper/SalesType1_C/" + SalesDate + ".jpeg", ".canvas1", temp, SalesDate, SalesTypeID);
                }
            } else if (SalesTypeID == '31' || (SalesTypeID == '1' && GrantTypeID == "+")) {//便利報
                $('.canvas31').show();
                $('.canvas1').hide();
                if (AFrameXY != "" && AFrameXY != null && AFrameXY != undefined) {
                    var LocationList = $.parseJSON(AFrameXY);
                    var temp = '[[{"x":' + (LocationList[0][0].x) * 6.3 + ',"y":' + (LocationList[0][0].y) * 6.3 + '},{"x":' + (LocationList[0][1].x) * 6.3 + ',"y":' + (LocationList[0][1].y) * 6.3 + '}]]';
                    DrawImage("../Files/JBERP_SalesPaper/SalesType31_A/" + SalesDate + ".jpeg", ".canvas31", temp, SalesDate, SalesTypeID);
                }
                else if (BFrameXY != "" && BFrameXY != null && BFrameXY != undefined) {
                    var LocationList = $.parseJSON(BFrameXY);
                    var temp = '[[{"x":' + (LocationList[0][0].x) * 6.3 + ',"y":' + (LocationList[0][0].y) * 6.3 + '},{"x":' + (LocationList[0][1].x) * 6.3 + ',"y":' + (LocationList[0][1].y) * 6.3 + '}]]';
                    DrawImage("../Files/JBERP_SalesPaper/SalesType31_B/" + SalesDate + ".jpeg", ".canvas31", temp, SalesDate, SalesTypeID);
                }
            }
        }
        function DrawImage(FilePath, Canvas, Coordinate, SalesDate, SalesTypeID) {
            var src;
            //檢查圖檔是否存在，存在就畫
            $.ajax({
                url: FilePath,
                async: false,
                type: "HEAD",//get status code ex:404(not found)
                success: function () {
                    $.messager.progress({ msg: 'Loading...' });//進度條開始
                    $(Canvas).jbCanvasDrawRect('loadImage', FilePath);//畫報
                    setTimeout(function () {
                        if (Coordinate != "") {
                            $(Canvas).jbCanvasDrawRect('setLocationList', $.parseJSON(Coordinate));//畫框
                            $('#a1').attr({ 'download': SalesDate + '_' + SalesTypeID });
                        }
                        CanvasToDataURL(Canvas);//canvas轉成<a>和<image>，canvas隱藏
                        $.messager.progress('close'); //進度條結束
                    }, 5000);
                },
                error: function () { }
            });
        }
        //<canvas>轉成<a><image>，<canvas>隱藏
        function CanvasToDataURL(Canvas) {
            if (Canvas == '.canvas1') {
                var dataurl = $(Canvas).getCanvasImage('jpeg', 0.9);
            } else if (Canvas == '.canvas31') {
                var dataurl = $(Canvas).getCanvasImage('jpeg', 0.5);
            }
                var blob = dataURItoBlob(dataurl);
                $('#a1').attr({ 'href': window.URL.createObjectURL(blob) });
                $('#image1').attr({ 'src': window.URL.createObjectURL(blob) });//小圖
                $('#image2').attr({ 'src': window.URL.createObjectURL(blob) });//大圖
                $('#a3').attr({ 'onclick': 'printCanvas("' + dataurl + '","' + Canvas + '")' });
                $("canvas:not(:hidden)").css({ 'display': 'none' });
                $('#image2-container').hide();
                $('#image1-container').show();
        }

        function dataURItoBlob(dataURI) {
            // convert base64/URLEncoded data component to raw binary data held in a string
            var byteString;
            if (dataURI.split(',')[0].indexOf('base64') >= 0)
                byteString = atob(dataURI.split(',')[1]);
            else
                byteString = unescape(dataURI.split(',')[1]);

            // separate out the mime component
            var mimeString = dataURI.split(',')[0].split(':')[1].split(';')[0];

            // write the bytes of the string to a typed array
            var ia = new Uint8Array(byteString.length);
            for (var i = 0; i < byteString.length; i++) {
                ia[i] = byteString.charCodeAt(i);
            }
            return new Blob([ia], { type: mimeString });
        }

        //沒用上
        function ResizeImage(maxwidth, maxheight) {
            //if ($(image).id == "image1") {
            var image = $('#image1');
                var w = image.width;
                var h = image.height;

                if (w > h) {
                    if (w > maxwidth) image.width(maxwidth);
                }
                else {
                    if (h > maxheight) image.height(maxheight);
                }
            //}
        }

        //OnClick"列印"按鈕
        function printCanvas(dataurl,canvas) {
            var win = window.open();
            if (canvas == '.canvas1') {
                win.document.write("<br><img id='img1' src='" + dataurl + "' width='960'/>");//297*210 2816*1993=1006*711=971*687
            } else {
                win.document.write("<br><img id='img31' src='" + dataurl + "'/>");//297*210
            }
            win.print();
            win.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" AgentDatabase="JBADMIN" AgentSolution="JBADMIN" AgentUser="A0123" />
            <div>
            
            <asp:Label ID="Label2" runat="server" Font-Size="Small" Text="刊登起訖日期:"></asp:Label>
            <JQTools:JQDateBox ID="JQDate1" runat="server" Width="100px" />
            〜<JQTools:JQDateBox ID="JQDate2" runat="server" />
            </div>

            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERPSalesDrawCustomer.ERPSalesDetails" runat="server" AutoApply="True"
                DataMember="ERPSalesDetails" Pagination="True" QueryTitle="Query"
                Title="刊登清單" OnLoadSuccess="MastersetWhere" AllowAdd="False" AllowDelete="False" AllowUpdate="False" ViewCommandVisible="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" EditDialogID="JQDialog1">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="夾報" Editor="text" FieldName="ButtonA" FormatScript="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="A面刊登" Editor="text" FieldName="AFrameXY" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="100">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="B面刊登" Editor="text" FieldName="BFrameXY" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="100">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="共版刊登" Editor="text" FieldName="CFrameXY" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="100">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="SalesMasterNO" Editor="numberbox" FieldName="SalesMasterNO" Format="" Width="50" Visible="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="" FieldName="CustNO" Format="" Width="100" Visible="True" Frozen="False" ReadOnly="True" Sortable="True" IsNvarChar="False" MaxLength="0" QueryCondition="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustShortName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="200">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="項次" Editor="text" FieldName="ItemSeq" Format="" Width="59" ReadOnly="True" Visible="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" />                        
                    <JQTools:JQGridColumn Alignment="center" Caption="交易別" Editor="" FieldName="SalesTypeID" Format="" Width="40" EditorOptions="" Frozen="False" Visible="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                    <JQTools:JQGridColumn Alignment="center" Caption="版別" Editor="infocombogrid" FieldName="DMTypeID" Format="" Width="35" EditorOptions="panelWidth:165,valueField:'DMTypeID',textField:'DMTypeID',remoteName:'sERPSalseDetails.infoERPDMType',tableName:'infoERPDMType',valueFieldCaption:'DMTypeID',textFieldCaption:'DMTypeID',selectOnly:false,checkData:false,columns:[{field:'DMTypeID',title:'代號',width:40,align:'left',sortable:false},{field:'DMTypeName',title:'名稱',width:105,align:'left',sortable:false}],cacheRelationText:false" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" />                   
                    <JQTools:JQGridColumn Alignment="center" Caption="區域" Editor="infocombogrid" FieldName="ViewAreaID" Format="" Width="80" EditorOptions="panelWidth:120,valueField:'ViewAreaID',textField:'ViewAreaName',remoteName:'sERPSalseDetails.infoERPViewArea',tableName:'infoERPViewArea',valueFieldCaption:'ViewAreaID',textFieldCaption:'ViewAreaName',selectOnly:false,checkData:false,columns:[{field:'ViewAreaID',title:'代號',width:40,align:'left',sortable:false},{field:'ViewAreaName',title:'名稱',width:55,align:'left',sortable:false}],cacheRelationText:false" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" />                  
                    <JQTools:JQGridColumn Alignment="center" Caption="刊登日期" Editor="datebox" FieldName="SalesDate" Format="yyyy-mm-dd" Width="120" FormatScript="" Frozen="False" Sortable="True" ReadOnly="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Visible="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="星期" Editor="" FieldName="dWeekday" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="True" Visible="True" Width="50" />
                    <JQTools:JQGridColumn Alignment="center" Caption="電子檔" Editor="text" FieldName="FrameXY" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100" FormatScript="FormatScriptButton">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="匯入" Editor="checkbox" FieldName="IsTransSys" Format="" Width="30" Visible="False" EditorOptions="on:1,off:0" FormatScript="genCheckBox" ReadOnly="True" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="匯入編號" Editor="" FieldName="depositSeq" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="55" />
                    <JQTools:JQGridColumn Alignment="center" Caption="需發票" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsInvoice" FormatScript="genCheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="40" />
                    <JQTools:JQGridColumn Alignment="center" Caption="發票年月" Editor="text" FieldName="InvoiceYM" Format="" Width="100" ReadOnly="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False" Visible="True" />
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
                    <JQTools:JQGridColumn Alignment="center" Caption="贈期" Editor="" FieldName="GrantTypeID" Format="" Width="30" EditorOptions="" Sortable="True" ReadOnly="True" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Visible="False" />
                    <JQTools:JQGridColumn Alignment="center" Caption="單位數" Editor="text" FieldName="SalesQty" Format="" Width="40" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" />                        
                    <JQTools:JQGridColumn Alignment="center" Caption="見刊" Editor="text" FieldName="SalesQtyView" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="30" />
                    <JQTools:JQGridColumn Alignment="left" Caption="有效" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" Frozen="True" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="30" FormatScript="genCheckBox" />
                    <JQTools:JQGridColumn Alignment="left" Caption="出刊備註" Editor="text" FieldName="SalesDescr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PDF檔名" Editor="text" FieldName="Remark1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="100">
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
    <JQTools:JQDefault runat="server" BindingObjectID="dataGridMaster" BorderStyle="NotSet" Enabled="True" EnableTheming="True" ClientIDMode="Inherit" ID="defaultMaster" EnableViewState="True" ViewStateMode="Inherit" >
</JQTools:JQDefault>
<JQTools:JQValidate runat="server" BindingObjectID="dataGridMaster" BorderStyle="NotSet" Enabled="True" EnableTheming="True" ClientIDMode="Inherit" ID="validateMaster" EnableViewState="True" ViewStateMode="Inherit" >
</JQTools:JQValidate>
        <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="JQDataForm1" Title=" " Width="3850px" DialogLeft="2px" DialogTop="2px">
            <a id="a1" href="#" download="" >下載</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <a id="a2" href="#" onclick="closeForm('#JQDialog1')"  >關閉</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <a id="a3" href="#">列印</a>
            &nbsp;&nbsp;&nbsp;&nbsp;<label style='color:blue','font-size:12px'>※點圖可放大縮小</label><br />
            <label style='color:red'><b>※建議使用Google Chrome瀏覽器來開啟本系統，如使用IE瀏覽器不能下載、列印，請按滑鼠右鍵另存圖檔。</b></label>
            <br />
            <JQTools:JQDataForm ID="JQDataForm1" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="ERPSalesDetails" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="5" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sERPSalesDrawCustomer.ERPSalesDetails" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="OnLoadSuccessJQDataForm1">
                <Columns>
                    <JQTools:JQFormColumn Alignment="left" Caption="SalesMasterNO" Editor="text" FieldName="SalesMasterNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="30" />
                    <JQTools:JQFormColumn Alignment="left" Caption="ItemSeq" Editor="text" FieldName="ItemSeq" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="60" />
                    <JQTools:JQFormColumn Alignment="left" Caption="A面框座標" Editor="text" FieldName="AFrameXY" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="150" />
                    <JQTools:JQFormColumn Alignment="left" Caption="B面框座標" Editor="text" FieldName="BFrameXY" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="150" />
                    <JQTools:JQFormColumn Alignment="left" Caption="共版框座標" Editor="text" FieldName="CFrameXY" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="150" />
                    <JQTools:JQFormColumn Alignment="left" Caption="SalesTypeID" Editor="text" FieldName="SalesTypeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                    <JQTools:JQFormColumn Alignment="left" Caption="GrantTypeID" Editor="text" FieldName="GrantTypeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                    <JQTools:JQFormColumn Alignment="left" Caption="SalesDate" Editor="text" FieldName="SalesDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                </Columns>
            </JQTools:JQDataForm>
            <%--<button onClick="javascript:WebBrowser.ExecWB(4,1)">另存新檔...</button><object id="WebBrowser" width=0 height=0 classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></object>--%>
            <canvas class="canvas1" style="width: 2816px; height: 1993px"></canvas>
            <%--求才畫布--%><%--760 360--%>
            <canvas class="canvas31" style="width: 3780px; height: 5078px"></canvas>
            <%--便利畫布--%>
            <%--<img id="saveimage" src=""/>--%>
            <%--<input id="savepngbtn" type="button" value="保存圖片" onclick=>--%>
            <div id="image-wrapper">
                <div id="image1-container">
                    <img id="image1" width="800" />
                </div>
                <div id="image2-container">
                    <img id="image2" />
                </div>
            </div>
        </JQTools:JQDialog>
</form>
</body>

</html>
