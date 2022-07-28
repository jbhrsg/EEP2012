<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_SalesTarget.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        var dfOnloadSuccess = false;
        var userid = getClientInfo("UserID");
        $(function () {
            //排按鈕
            $('#SubClass_Query').closest('td').append($('.infosysbutton-q', '#querydataGridDay').closest('td').contents());
            $($('#queryJQDataGrid4').find('input')[6]).closest('td').append($('.infosysbutton-q', '#queryJQDataGrid4').closest('td').contents());
            //var ud=getClientInfo("userid");

            $('#Sales_Query').combobox('setValue', userid);
            $($('#queryJQDataGrid4').find('input')[3]).combobox('setValue', userid);
        });
        function OnLoadSuccessdataFormYear(row) {//按"查詢"不會觸發，切換tab也不觸發
            setTimeout(function () {//頁籤切到"首頁"
                $("#JQTab1").tabs('select', 0);
            }, 100);
            var mode = getEditMode($('#dataFormYear'));
            if (mode == 'inserted') {
                $("#divTab").hide();
                $("#JQDialogYear").find(".infosysbutton-s").show();
                $('#dataFormYearblank1').closest('td').prev('td').hide();
                $('#dataFormYearblank2').closest('td').prev('td').hide();
                $('#dataFormYearblank1').closest('td').hide();
                $('#dataFormYearblank2').closest('td').hide();
                $('#dataFormYearTarget1').attr('disabled', false);
                $('#dataFormYearTarget2').attr('disabled', false);
            } else if (mode == 'updated' && (userid == '002' || userid == '335' || userid == '099' || userid == '141')) {
                $("#divTab").show();
                $("#JQDialogYear").find(".infosysbutton-s").show();
                $('#dataFormYearTarget1').attr('disabled', false);
                $('#dataFormYearTarget2').attr('disabled', false);
            } else {
                $("#divTab").show();
                $("#JQDialogYear").find(".infosysbutton-s").hide();
                $('#dataFormYearblank1').closest('td').prev('td').show();
                $('#dataFormYearblank2').closest('td').prev('td').show();
                $('#dataFormYearblank1').closest('td').show();
                $('#dataFormYearblank2').closest('td').show();
            }
            if (mode != 'inserted') {
                var year = row.Year;
                if (year != '') {//dataformYear上的四個dataGrid在dataGridYear畫面時，就全都onloadsuccess，不管alwayscolse是true或false
                    //if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) { //只執行第一次
                    setTimeout(function () {
                        $("#dataGridMonth").datagrid("setWhere", "Year =" + year + "and SalesTypeID=1");
                        if (userid != '' || userid != 'undefined') {
                            $("#dataGridDay").datagrid("setWhere", "Sales='" + userid + "' and Year =" + year + "and SalesTypeID=1");
                        } else { $("#dataGridDay").datagrid("setWhere", "Year =" + year + "and SalesTypeID=1"); }
                    }, 600);
                    setTimeout(function () {
                        $("#JQDataGrid3").datagrid("setWhere", "Year =" + year + "and SalesTypeID=31");
                        if (userid != '' || userid != 'undefined') {
                            $("#JQDataGrid4").datagrid("setWhere", "Sales='" + userid + "' and Year =" + year + "and SalesTypeID=31");
                        } else { $("#JQDataGrid4").datagrid("setWhere", "Year =" + year + "and SalesTypeID=31"); }
                    }, 2000);
                }
            }
            dfOnloadSuccess = true;//因為OnSelectJQTab1會執行早於OnLoadSuccessdataFormYear
        }
        function OnLoadSuccessdataFormMonth() {
            if (getEditMode($('#dataFormMonth')) == 'inserted') {
                $('#dataFormMonthSales').refval('setValue', userid);
            }
        }
        
        function OnTotaldataGridMonthSigma(rows) {
            $('#dataFormYearblank1').val(rows.sigma);
        }
        function OnTotalJQDataGrid3Sigma(rows) {
            $('#dataFormYearblank2').val(rows.sigma);
        }
        function DefaultMethodYear() {
            var year = $("#dataFormYearYear").val();
            return year;
        }
        function DefaultMethodSalesTypeID() {
            var tab = $("#JQTab1").tabs('getSelected');
            var Index = $("#JQTab1").tabs('getTabIndex', tab); //取得目前Tab的index
            var salesTypeID;
            if (Index == 0) { salesTypeID = 1 }
            else if (Index == 1) { salesTypeID = 31 }
            return salesTypeID;
        }

        function queryGrid(dg) {//按"查詢"會觸發
            var where = $(dg).datagrid("getWhere");//抓取query欄位值
            var dgLength = dg.length;
            var dg1 = dg.substr(1, dgLength);
            var month = $($('#query' + dg1).find('input')[1]).val().trim();
            var sales = $($('#query' + dg1).find('input')[4]).val();
            var year = $("#dataFormYearYear").val();
            //alert(sales);
            if (month != '' || sales != '') {
                if (dg == "#dataGridDay") {//求才
                    where += " and Year =" + year + " and SalesTypeID=1";
                } else if (dg == "#JQDataGrid4") {//便利
                    where += " and Year =" + year + " and SalesTypeID=31";
                }
            } else {//空白
                if (dg == "#dataGridDay") {
                    where = "Year =" + year + " and SalesTypeID=1";
                } else if (dg == "#JQDataGrid4") {
                    where = "Year =" + year + " and SalesTypeID=31";
                }
            }
            //alert(where);
            $(dg).datagrid("setWhere", where);
        }
        //自己才能編輯或刪除自己的月目標和日目標//338郁欣、095雪雲可以改刪郁欣J的日目標、370潔心可以改刪郁欣P的日目標
        function OnUpdatedataGridMonth(rows) { if (  userid=='335' || userid=='141' || rows.Sales == userid  ) { return true; } else { return false; } }
        function OnUpdatedataGridDay(rows) { if (rows.Sales == userid || (rows.Sales == '338' && rows.SubClass == 'J' && (userid == '095')) || (rows.Sales == '338' && rows.SubClass == 'P' && (userid == '370'))) { return true; } else { return false; } }
        function OnUpdateJQDataGrid3(rows) { if (userid=='141' || rows.Sales == userid ) { return true; } else { return false; } }
        function OnUpdateJQDataGrid4(rows) { if (rows.Sales == userid || (rows.Sales == '338' && rows.SubClass == 'J' && (userid == '095')) || (rows.Sales == '338' && rows.SubClass == 'P' && (userid == '370'))) { return true; } else { return false; } }

        function OnDeletedataGridMonth(rows) { if (userid == '141' || rows.Sales == userid) { return true; } else { return false; } }
        function OnDeletedataGridDay(rows) { if (rows.Sales == userid || (rows.Sales == '338' && rows.SubClass == 'J' && (userid == '095')) || (rows.Sales == '338' && rows.SubClass == 'P' && (userid == '370'))) { return true; } else { return false; } }
        function OnDeleteJQDataGrid3(rows) { if (userid == '141' || rows.Sales == userid) { return true; } else { return false; } }
        function OnDeleteJQDataGrid4(rows) { if (rows.Sales == userid || (rows.Sales == '338' && rows.SubClass == 'J' && (userid == '095')) || (rows.Sales == '338' && rows.SubClass == 'P' && (userid == '370'))) { return true; } else { return false; } }
        //002才能新增或刪除年目標
        function OnInsertdataGridYear() { if (userid == '002' || userid == '335' || userid == '099' || userid =='141') { return true; } else { return false; } }
        function OnDeletedataGridYear() { if (userid == '002' || userid == '335' || userid == '099' || userid == '141') { return true; } else { return false; } }
        //月份必填
        function OnApplydataFormDay() {
            if ($("#dataFormDayMonth").combobox('getValue') == '') { alert('"月"未填'); return false;}
        }

        //reload月目標和日目標的grid
        function OnApplieddataFormMonth() {
            $("#dataGridMonth").datagrid("reload");//$("#dataGridMonth").datagrid("setWhere", $("#dataGridDay").datagrid("getWhere"));
            $("#JQDataGrid3").datagrid("reload");//reload會抓query欄位setwhere
        }
        function OnApplieddataFormDay() {
            $("#dataGridDay").datagrid("reload");
            $("#JQDataGrid4").datagrid("reload");
            $("#dataGridMonth").datagrid("reload");
            $("#JQDataGrid3").datagrid("reload");
            $($('#querydataGridDay').find('input')[3]).combobox('reload');
            $($('#queryJQDataGrid4').find('input')[3]).combobox('reload');
        }
        //日目標新增
        function OnClickdg(dgrid) { insertItem(dgrid); }//OnLoadSuccessdataFormDay較$("#dataFormDayMonth").val('1')快執行

        function OnSelectJQTab1() {//網頁一開始載入(比OnloadSuccessdataForm還早) 和 TabOnSelect 會觸發
            var tab;
            var Index;
            var where;
            var dg;
            var dgm;
            var typeid;
            var year = $("#dataFormYearYear").val();
            if (dfOnloadSuccess == true) {//為了在OnloadSuccessdataForm之後執行
                tab = $("#JQTab1").tabs('getSelected');
                Index = $("#JQTab1").tabs('getTabIndex', tab); //取得目前Tab的index
                //alert(Index);
                if (Index == '0') {//求才
                    dg = "#dataGridDay";
                    dgm = "#dataGridMonth";
                    typeid = "1";
                } else if (Index == '1') {//便利
                    dg = "#JQDataGrid4";
                    dgm = "#JQDataGrid3";
                    typeid = "31";
                }
                var where = $(dg).datagrid("getWhere");//抓取query欄位值
                var dgLength = dg.length;
                var dg1 = dg.substr(1, dgLength);
                var month = $($('#query' + dg1).find('input')[1]).val();
                var year = $("#dataFormYearYear").val();
                //alert(month);
                if (month != 0 && month != '') {
                    where += " and Year =" + year + " and SalesTypeID=" + typeid;
                } else {//月份不拘(0)或空白
                    where = " Year =" + year + " and SalesTypeID=" + typeid;
                }
                //alert(where);
                //$(dg).datagrid("setWhere", where);
                //$(dgm).datagrid("setWhere", "Year =" + year + " and SalesTypeID=" + typeid);
            }
        }
        function OnLoadSuccessdataFormDay() {
            if (getEditMode($('#dataFormDay')) == 'inserted') {
                //$('#dataFormDaySales').val(userid);
                $('#dataFormDaySales').refval('setValue', userid);
                //dataFormDay預設別名
                DefaultCaptiondataFormDay();
            } else {
                var year = $("#dataFormYearYear").val();
                var mth = $("#dataFormDayMonth").combobox('getValue');
                //dataFormDay顯示或隱藏欄位
                HideShowFielddataFormDay(year, mth);
                //dataFormDay重設別名
                ResetCaptiondataFormDay(year, mth);
            }

        }
        function OnSelectDataFormDay_Month() {
            var year = $("#dataFormYearYear").val();
            var mth = $("#dataFormDayMonth").combobox('getValue');
            //dataFormDay顯示或隱藏欄位
            HideShowFielddataFormDay(year, mth)
            //dataFormDay重設別名
            ResetCaptiondataFormDay(year, mth);
        }

        function OnSelect_dataFormDaySales(row) {
            if (row.USERID == '338' && getClientInfo('_usercode')=='095') {
                $("#dataFormDaySubClass").val('J');
            } else if (row.USERID == '338' && getClientInfo('_usercode') == '370') {
                $("#dataFormDaySubClass").val('P');
            }

        }

        //-------------工具------------------------------------
        //dataFormDay顯示或隱藏欄位
        function HideShowFielddataFormDay(year,mth) {
            $("#dataFormDay二十九日").show(); $("#dataFormDay二十九日").closest("td").prev("td").show();
            $("#dataFormDay三十日").show(); $("#dataFormDay三十日").closest("td").prev("td").show();
            $("#dataFormDay三十一日").show(); $("#dataFormDay三十一日").closest("td").prev("td").show();
            if (mth == 2) {
                if (leapYear(year) == false) {//是否為閏年
                    $("#dataFormDay二十九日").hide(); $("#dataFormDay二十九日").closest("td").prev("td").hide();
                }
                $("#dataFormDay三十日").hide(); $("#dataFormDay三十日").closest("td").prev("td").hide();
                $("#dataFormDay三十一日").hide(); $("#dataFormDay三十一日").closest("td").prev("td").hide();
            } else if (mth == 4 || mth == 6 || mth == 9 || mth == 11) {
                $("#dataFormDay三十一日").hide(); $("#dataFormDay三十一日").closest("td").prev("td").hide();
            }
        }
        //dataFormDay重設別名
        function ResetCaptiondataFormDay(year, mth) {
            var daylist = ['日', '一', '二', '三', '四', '五', '六'];
            var arrayDay = ['一', '二', '三', '四', '五', '六', '七', '八', '九', '十', '十一', '十二', '十三', '十四', '十五', '十六', '十七', '十八', '十九', '二十', '二十一', '二十二', '二十三', '二十四', '二十五', '二十六', '二十七', '二十八', '二十九', '三十', '三十一'];
            for (i = 0; i < 31;i++){
                $("#dataFormDay" + arrayDay[i] + "日").closest("td").prev("td").html(i + 1);
                $("#dataFormDay" + arrayDay[i] + "日").closest("td").prev("td").html($("#dataFormDay" + arrayDay[i] + "日").closest("td").prev("td").html() + '(' + daylist[new Date("'" + year + "-" + mth + "-"+(i+1)+"'").getDay()] + ')');
            }  
        }
        //dataFormDay預設別名
        function DefaultCaptiondataFormDay() {
            var arrayDay = ['一', '二', '三', '四', '五', '六', '七', '八', '九', '十', '十一', '十二', '十三', '十四', '十五', '十六', '十七', '十八', '十九', '二十', '二十一', '二十二', '二十三', '二十四', '二十五', '二十六', '二十七', '二十八', '二十九', '三十', '三十一'];
            for (i = 0; i < 31; i++) {
                $("#dataFormDay" + arrayDay[i] + "日").closest("td").prev("td").html(i + 1);
            }
        }
        //閏年(二月閏月)判斷
        function leapYear(year) {
            return ((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0);
        }


    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" AgentDatabase="" AgentSolution="" AgentUser="" />
            <JQTools:JQDataGrid ID="dataGridYear" runat="server" AllowAdd="True" AllowDelete="False" AllowUpdate="True" AlwaysClose="False" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="SalesYearTarget" DeleteCommandVisible="False" DuplicateCheck="False" EditDialogID="JQDialogYear" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sSalesTarget.SalesYearTarget" RowNumbers="True" Title="年目標" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" OnInsert="OnInsertdataGridYear" OnDelete="OnDeletedataGridYear">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="年" Editor="text" FieldName="Year" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="求才目標" Editor="text" FieldName="Target1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="月目標加總" Editor="text" FieldName="SumTarget1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="便利目標" Editor="text" FieldName="Target2" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="月目標加總" Editor="text" FieldName="SumTarget2" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Enabled="True" Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增年目標" Visible="True" />
                </TooItems>
            </JQTools:JQDataGrid>
            <JQTools:JQDialog ID="JQDialogYear" runat="server" BindingObjectID="dataFormYear" Title="" DialogLeft="2px" DialogTop="2px" Width="1100px">
                <JQTools:JQDataForm ID="dataFormYear" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="SalesYearTarget" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="2" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sSalesTarget.SalesYearTarget" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="OnLoadSuccessdataFormYear">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="編號" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="True" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="年" Editor="text" FieldName="Year" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="40" />
                        <JQTools:JQFormColumn Alignment="left" Caption="求才目標" Editor="text" FieldName="Target1" MaxLength="0" NewRow="True" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="月目標加總" Editor="text" FieldName="blank1" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="便利目標" Editor="text" FieldName="Target2" MaxLength="0" NewRow="True" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="月目標加總" Editor="text" FieldName="blank2" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="Year" />
                    </RelationColumns>
                </JQTools:JQDataForm>

                <div id ="divTab">
                <JQTools:JQTab ID="JQTab1" runat="server"  Width="">
                    <JQTools:JQTabItem ID="JQTabItem1" runat="server" PreLoad="True" Title="求才"><JQTools:JQDataGrid ID="dataGridMonth" data-options="pagination:true,view:commandview" RemoteName="sSalesTarget.SalesMonthTarget" runat="server" AutoApply="True"
                            DataMember="SalesMonthTarget" Pagination="False" QueryTitle="Query" EditDialogID="JQDialogMonth"
                            Title="月目標" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="True" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="合計:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="1044px" ParentObjectID="" OnUpdate="OnUpdatedataGridMonth" OnDelete="OnDeletedataGridMonth"><Columns><JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" Visible="False" Width="90" /><JQTools:JQGridColumn Alignment="left" Caption="SalesTypeID" Editor="text" FieldName="SalesTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="年" Editor="text" FieldName="Year" Visible="true" Width="40" Frozen="True" /><JQTools:JQGridColumn Alignment="left" Caption="業務" Editor="infocombobox" FieldName="Sales" Visible="true" Width="60" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sSalesTarget.USERS',tableName:'USERS',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Frozen="True" /><JQTools:JQGridColumn Alignment="left" Caption="業務子類" Editor="text" FieldName="SubClass" Frozen="True" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="月目標加總" Editor="text" FieldName="sigma" MaxLength="0" Total="sum" Visible="true" Width="80" OnTotal="OnTotaldataGridMonthSigma" Frozen="True"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="center" Caption="1" Editor="text" FieldName="一月" Visible="True"  Width="50" Total="sum" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" /><JQTools:JQGridColumn Alignment="left" Caption="日目標加總" Editor="text" FieldName="SumDT1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True"  Width="68"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="center" Caption="2" Editor="text" FieldName="二月" Visible="true"  Width="50" Total="sum" /><JQTools:JQGridColumn Alignment="left" Caption="日目標加總" Editor="text" FieldName="SumDT2" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True"  Width="68"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="center" Caption="3" Editor="text" FieldName="三月" Visible="true"  Width="50" Total="sum" /><JQTools:JQGridColumn Alignment="left" Caption="日目標實填" Editor="text" FieldName="SumDT3" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True"  Width="68"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="center" Caption="4" Editor="text" FieldName="四月" Visible="true"  Width="50" Total="sum" /><JQTools:JQGridColumn Alignment="left" Caption="日目標加總" Editor="text" FieldName="SumDT4" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True"  Width="68"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="center" Caption="5" Editor="text" FieldName="五月" Visible="true"  Width="50" Total="sum" /><JQTools:JQGridColumn Alignment="left" Caption="日目標加總" Editor="text" FieldName="SumDT5" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True"  Width="68"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="center" Caption="6" Editor="text" FieldName="六月" Visible="true"  Width="50" Total="sum" /><JQTools:JQGridColumn Alignment="left" Caption="日目標加總" Editor="text" FieldName="SumDT6" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True"  Width="68"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="center" Caption="7" Editor="text" FieldName="七月" Visible="true"  Width="50" Total="sum" /><JQTools:JQGridColumn Alignment="left" Caption="日目標加總" Editor="text" FieldName="SumDT7" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True"  Width="68"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="center" Caption="8" Editor="text" FieldName="八月" Visible="true"  Width="50" Total="sum" /><JQTools:JQGridColumn Alignment="left" Caption="日目標加總" Editor="text" FieldName="SumDT8" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True"  Width="68"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="center" Caption="9" Editor="text" FieldName="九月" Visible="True"  Width="50" Total="sum" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" /><JQTools:JQGridColumn Alignment="left" Caption="日目標加總" Editor="text" FieldName="SumDT9" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True"  Width="68"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="center" Caption="10" Editor="text" FieldName="十月" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True"  Width="50" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="日目標加總" Editor="text" FieldName="SumDT10" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True"  Width="68"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="center" Caption="11" Editor="text" FieldName="十一月" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True"  Width="50" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="日目標加總" Editor="text" FieldName="SumDT11" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True"  Width="68"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="center" Caption="12" Editor="text" FieldName="十二月" TableName="" Total="sum" Visible="true"  Width="50"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="日目標加總" Editor="text" FieldName="SumDT12" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True"  Width="68"></JQTools:JQGridColumn></Columns><TooItems><JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                                    OnClick="insertItem" Text="新增月目標" /></TooItems></JQTools:JQDataGrid><br /><JQTools:JQDataGrid ID="dataGridDay" runat="server" DataMember="SalesDayTarget" RemoteName="sSalesTarget.SalesDayTarget" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialogDay" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="False" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTitle="查詢" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" Title="日目標" TotalCaption="合計:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="1044px" ParentObjectID="" OnUpdate="OnUpdatedataGridDay" OnDelete="OnDeletedataGridDay"><Columns><JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="SalesTypeID" Editor="text" FieldName="SalesTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="年" Editor="text" FieldName="Year" Frozen="True" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="月" Editor="text" FieldName="Month" Frozen="True" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="業務" Editor="infocombobox" FieldName="Sales" Frozen="True" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sSalesTarget.USERS',tableName:'USERS',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="業務子類" Editor="text" FieldName="SubClass" Frozen="True" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="日目標加總" Editor="text" FieldName="sigma" Frozen="True" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="1" Editor="text" FieldName="一日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="2" Editor="text" FieldName="二日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="3" Editor="text" FieldName="三日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="4" Editor="text" FieldName="四日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="5" Editor="text" FieldName="五日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="6" Editor="text" FieldName="六日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="7" Editor="text" FieldName="七日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="8" Editor="text" FieldName="八日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="9" Editor="text" FieldName="九日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="10" Editor="text" FieldName="十日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="11" Editor="text" FieldName="十一日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="12" Editor="text" FieldName="十二日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="13" Editor="text" FieldName="十三日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="14" Editor="text" FieldName="十四日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="15" Editor="text" FieldName="十五日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="16" Editor="text" FieldName="十六日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="17" Editor="text" FieldName="十七日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="18" Editor="text" FieldName="十八日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="19" Editor="text" FieldName="十九日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="20" Editor="text" FieldName="二十日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="21" Editor="text" FieldName="二十一日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="22" Editor="text" FieldName="二十二日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="23" Editor="text" FieldName="二十三日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="24" Editor="text" FieldName="二十四日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="25" Editor="text" FieldName="二十五日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="26" Editor="text" FieldName="二十六日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="27" Editor="text" FieldName="二十七日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="28" Editor="text" FieldName="二十八日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="29" Editor="text" FieldName="二十九日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="30" Editor="text" FieldName="三十日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="31" Editor="text" FieldName="三十一日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn></Columns><TooItems><JQTools:JQToolItem Enabled="True" Icon="icon-add" ItemType="easyui-linkbutton" OnClick="OnClickdg" Text="新增日目標" Visible="True" /></TooItems><QueryColumns><JQTools:JQQueryColumn AndOr="and" Caption="月份" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'month',textField:'month',remoteName:'sSalesTarget.month',tableName:'month',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="Month" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" DefaultValue="" /><JQTools:JQQueryColumn AndOr="and" Caption="業務" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'Sales',textField:'USERNAME',remoteName:'sSalesTarget.SalesDayTargetSales',tableName:'SalesDayTargetSales',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="Sales" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                            <JQTools:JQQueryColumn AndOr="and" Caption="業務子類" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'},{value:'J',text:'J',selected:'false'},{value:'P',text:'P',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SubClass" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="40" />
                            </QueryColumns></JQTools:JQDataGrid></JQTools:JQTabItem>
                    <JQTools:JQTabItem ID="JQTabItem2" runat="server" PreLoad="True" Title="便利"><JQTools:JQDataGrid ID="JQDataGrid3" data-options="pagination:true,view:commandview" RemoteName="sSalesTarget.SalesMonthTarget" runat="server" AutoApply="True"
                            DataMember="SalesMonthTarget" Pagination="False" QueryTitle="Query" EditDialogID="JQDialogMonth"
                            Title="月目標" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="合計:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="1044px" ParentObjectID="" OnUpdate="OnUpdateJQDataGrid3" OnDelete="OnDeleteJQDataGrid3"><Columns><JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" Visible="False" Width="90" /><JQTools:JQGridColumn Alignment="left" Caption="SalesTypeID" Editor="text" FieldName="SalesTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="年" Editor="text" FieldName="Year" Visible="true" Width="40" Frozen="True" /><JQTools:JQGridColumn Alignment="left" Caption="業務" Editor="infocombobox" FieldName="Sales" Visible="true" Width="60" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sSalesTarget.USERS',tableName:'USERS',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Frozen="True" /><JQTools:JQGridColumn Alignment="left" Caption="業務子類" Editor="text" FieldName="SubClass" Frozen="True" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="月目標加總" Editor="text" FieldName="sigma" MaxLength="0" Total="sum" Visible="true" Width="80" OnTotal="OnTotalJQDataGrid3Sigma" Frozen="True"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="center" Caption="1" Editor="text" FieldName="一月" Visible="True"  Width="50" Total="sum" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" /><JQTools:JQGridColumn Alignment="left" Caption="日目標加總" Editor="text" FieldName="SumDT1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True"  Width="68"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="center" Caption="2" Editor="text" FieldName="二月" Visible="true"  Width="50" Total="sum" /><JQTools:JQGridColumn Alignment="left" Caption="日目標加總" Editor="text" FieldName="SumDT2" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True"  Width="68"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="center" Caption="3" Editor="text" FieldName="三月" Visible="true"  Width="50" Total="sum" /><JQTools:JQGridColumn Alignment="left" Caption="日目標加總" Editor="text" FieldName="SumDT3" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True"  Width="68"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="center" Caption="4" Editor="text" FieldName="四月" Visible="true"  Width="50" Total="sum" /><JQTools:JQGridColumn Alignment="left" Caption="日目標加總" Editor="text" FieldName="SumDT4" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True"  Width="68"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="center" Caption="5" Editor="text" FieldName="五月" Visible="true"  Width="50" Total="sum" /><JQTools:JQGridColumn Alignment="left" Caption="日目標加總" Editor="text" FieldName="SumDT5" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True"  Width="68"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="center" Caption="6" Editor="text" FieldName="六月" Visible="true"  Width="50" Total="sum" /><JQTools:JQGridColumn Alignment="left" Caption="日目標加總" Editor="text" FieldName="SumDT6" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True"  Width="68"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="center" Caption="7" Editor="text" FieldName="七月" Visible="true"  Width="50" Total="sum" /><JQTools:JQGridColumn Alignment="left" Caption="日目標加總" Editor="text" FieldName="SumDT7" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True"  Width="68"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="center" Caption="8" Editor="text" FieldName="八月" Visible="true"  Width="50" Total="sum" /><JQTools:JQGridColumn Alignment="left" Caption="日目標加總" Editor="text" FieldName="SumDT8" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True"  Width="68"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="center" Caption="9" Editor="text" FieldName="九月" Visible="True"  Width="50" Total="sum" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" /><JQTools:JQGridColumn Alignment="left" Caption="日目標加總" Editor="text" FieldName="SumDT9" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True"  Width="68"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="center" Caption="10" Editor="text" FieldName="十月" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True"  Width="50" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="日目標加總" Editor="text" FieldName="SumDT10" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True"  Width="68"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="center" Caption="11" Editor="text" FieldName="十一月" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True"  Width="50" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="日目標加總" Editor="text" FieldName="SumDT11" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True"  Width="68"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="center" Caption="12" Editor="text" FieldName="十二月" TableName="" Total="sum" Visible="true"  Width="50"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="日目標加總" Editor="text" FieldName="SumDT12" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True"  Width="68"></JQTools:JQGridColumn></Columns><TooItems><JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                                    OnClick="insertItem" Text="新增月目標" /></TooItems></JQTools:JQDataGrid><br /><JQTools:JQDataGrid ID="JQDataGrid4" runat="server" DataMember="SalesDayTarget" RemoteName="sSalesTarget.SalesDayTarget" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialogDay" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="False" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTitle="查詢" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" Title="日目標" TotalCaption="合計:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="1044px" ParentObjectID="" OnUpdate="OnUpdateJQDataGrid4" OnDelete="OnDeleteJQDataGrid4"><Columns><JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="SalesTypeID" Editor="text" FieldName="SalesTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="年" Editor="text" FieldName="Year" Frozen="True" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="月" Editor="text" FieldName="Month" Frozen="True" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="業務" Editor="infocombobox" FieldName="Sales" Frozen="True" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sSalesTarget.USERS',tableName:'USERS',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="業務子類" Editor="text" FieldName="SubClass" Frozen="True" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="日目標加總" Editor="text" FieldName="sigma" Frozen="True" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="1" Editor="text" FieldName="一日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="2" Editor="text" FieldName="二日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="3" Editor="text" FieldName="三日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="4" Editor="text" FieldName="四日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="5" Editor="text" FieldName="五日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="6" Editor="text" FieldName="六日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="7" Editor="text" FieldName="七日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="8" Editor="text" FieldName="八日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="9" Editor="text" FieldName="九日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="10" Editor="text" FieldName="十日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="11" Editor="text" FieldName="十一日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="12" Editor="text" FieldName="十二日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="13" Editor="text" FieldName="十三日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="14" Editor="text" FieldName="十四日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="15" Editor="text" FieldName="十五日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="16" Editor="text" FieldName="十六日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="17" Editor="text" FieldName="十七日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="18" Editor="text" FieldName="十八日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="19" Editor="text" FieldName="十九日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="20" Editor="text" FieldName="二十日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="21" Editor="text" FieldName="二十一日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="22" Editor="text" FieldName="二十二日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="23" Editor="text" FieldName="二十三日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="24" Editor="text" FieldName="二十四日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="25" Editor="text" FieldName="二十五日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="26" Editor="text" FieldName="二十六日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="27" Editor="text" FieldName="二十七日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="28" Editor="text" FieldName="二十八日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="29" Editor="text" FieldName="二十九日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="30" Editor="text" FieldName="三十日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn><JQTools:JQGridColumn Alignment="left" Caption="31" Editor="text" FieldName="三十一日" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" Total="sum"></JQTools:JQGridColumn></Columns><TooItems><JQTools:JQToolItem Enabled="True" Icon="icon-add" ItemType="easyui-linkbutton" OnClick="OnClickdg" Text="新增日目標" Visible="True" /></TooItems><QueryColumns><JQTools:JQQueryColumn AndOr="and" Caption="月份" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'month',textField:'month',remoteName:'sSalesTarget.month',tableName:'month',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="Month" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" DefaultValue="" /><JQTools:JQQueryColumn AndOr="and" Caption="業務" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'Sales',textField:'USERNAME',remoteName:'sSalesTarget.SalesDayTargetSales',tableName:'SalesDayTargetSales',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="Sales" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                            <JQTools:JQQueryColumn AndOr="and" Caption="業務子類" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'},{value:'J',text:'J',selected:'false'},{value:'P',text:'P',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SubClass" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="40" />
                            </QueryColumns></JQTools:JQDataGrid></JQTools:JQTabItem>
                </JQTools:JQTab>
                </div>
                
            </JQTools:JQDialog>
            <JQTools:JQDialog ID="JQDialogMonth" runat="server" BindingObjectID="dataFormMonth" Title="" Width="800px" Icon="" ShowModal="True" ShowSubmitDiv="True">
                <JQTools:JQDataForm ID="dataFormMonth" runat="server" DataMember="SalesMonthTarget" HorizontalColumnsCount="6" RemoteName="sSalesTarget.SalesMonthTarget" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="True" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApplied="OnApplieddataFormMonth" ParentObjectID="" ChainDataFormID="" OnLoadSuccess="OnLoadSuccessdataFormMonth" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Width="80" ReadOnly="True" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="交易別" Editor="inforefval" FieldName="SalesTypeID" maxlength="0" NewRow="False" Width="80" ReadOnly="True" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'sSalesTarget.SalesTypeName',tableName:'SalesTypeName',columns:[],columnMatches:[],whereItems:[],valueField:'SalesTypeID',textField:'SalesTypeName',valueFieldCaption:'SalesTypeID',textFieldCaption:'SalesTypeName',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" />
                        <JQTools:JQFormColumn Alignment="left" Caption="年" Editor="text" FieldName="Year" Width="80" maxlength="0" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="業務" Editor="inforefval" FieldName="Sales" Width="80" NewRow="False" ReadOnly="False" EditorOptions="title:'JQRefval',panelWidth:350,panelHeight:200,remoteName:'sSalesTarget.USERS',tableName:'USERS',columns:[],columnMatches:[],whereItems:[],valueField:'USERID',textField:'USERNAME',valueFieldCaption:'USERID',textFieldCaption:'USERNAME',cacheRelationText:false,checkData:false,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" />
                        <JQTools:JQFormColumn Alignment="left" Caption="業務子類" Editor="text" FieldName="SubClass" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="1" Editor="text" FieldName="一月" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="2" Editor="text" FieldName="二月" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="3" Editor="text" FieldName="三月" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="4" Editor="text" FieldName="四月" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="5" Editor="text" FieldName="五月" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="6" Editor="text" FieldName="六月" Width="80" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="7" Editor="text" FieldName="七月" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="8" Editor="text" FieldName="八月" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="9" Editor="text" FieldName="九月" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="10" Editor="text" FieldName="十月" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="11" Editor="text" FieldName="十一月" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="12" Editor="text" FieldName="十二月" Width="80" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMonth" runat="server" BindingObjectID="dataFormMonth" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="AutoKey" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="DefaultMethodYear" FieldName="Year" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="DefaultMethodSalesTypeID" FieldName="SalesTypeID" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="" FieldName="SubClass" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMonth" runat="server" BindingObjectID="dataFormMonth" EnableTheming="True">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
            <JQTools:JQDialog ID="JQDialogDay" runat="server" BindingObjectID="dataFormDay" Title="" Width="700px">
                <JQTools:JQDataForm ID="dataFormDay" runat="server" DataMember="SalesDayTarget" HorizontalColumnsCount="5" RemoteName="sSalesTarget.SalesDayTarget" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="True" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApplied="OnApplieddataFormDay" ParentObjectID="" OnLoadSuccess="OnLoadSuccessdataFormDay" OnApply="OnApplydataFormDay" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Width="80" NewRow="False" ReadOnly="True" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="交易別" Editor="inforefval" FieldName="SalesTypeID" maxlength="0" NewRow="False" Width="80" ReadOnly="True" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'sSalesTarget.SalesTypeName',tableName:'SalesTypeName',columns:[],columnMatches:[],whereItems:[],valueField:'SalesTypeID',textField:'SalesTypeName',valueFieldCaption:'SalesTypeID',textFieldCaption:'SalesTypeName',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" />
                        <JQTools:JQFormColumn Alignment="left" Caption="年" Editor="text" FieldName="Year" Width="80" maxlength="0" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="業務" Editor="inforefval" FieldName="Sales" Width="80" ReadOnly="False" EditorOptions="title:'JQRefval',panelWidth:350,panelHeight:200,remoteName:'sSalesTarget.USERS',tableName:'USERS',columns:[],columnMatches:[],whereItems:[],valueField:'USERID',textField:'USERNAME',valueFieldCaption:'USERID',textFieldCaption:'USERNAME',cacheRelationText:false,checkData:false,showValueAndText:false,dialogCenter:false,onSelect:OnSelect_dataFormDaySales,selectOnly:true,capsLock:'none',fixTextbox:'false'" />
                        <JQTools:JQFormColumn Alignment="left" Caption="業務子類" Editor="text" FieldName="SubClass" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="月" Editor="infocombobox" FieldName="Month" Width="84" NewRow="False" ReadOnly="False" EditorOptions="valueField:'month',textField:'month',remoteName:'sSalesTarget.month',tableName:'month',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:OnSelectDataFormDay_Month,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="1" Editor="text" FieldName="一日" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="2" Editor="text" FieldName="二日" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="3" Editor="text" FieldName="三日" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="4" Editor="text" FieldName="四日" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="5" Editor="text" FieldName="五日" Width="80" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="6" Editor="text" FieldName="六日" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="7" Editor="text" FieldName="七日" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="8" Editor="text" FieldName="八日" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="9" Editor="text" FieldName="九日" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="10" Editor="text" FieldName="十日" Width="80" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="11" Editor="text" FieldName="十一日" Width="80" NewRow="False" MaxLength="0" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="12" Editor="text" FieldName="十二日" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="13" Editor="text" FieldName="十三日" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="14" Editor="text" FieldName="十四日" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="15" Editor="text" FieldName="十五日" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="16" Editor="text" FieldName="十六日" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="17" Editor="text" FieldName="十七日" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="18" Editor="text" FieldName="十八日" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="19" Editor="text" FieldName="十九日" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="20" Editor="text" FieldName="二十日" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="21" Editor="text" FieldName="二十一日" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="22" Editor="text" FieldName="二十二日" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="23" Editor="text" FieldName="二十三日" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="24" Editor="text" FieldName="二十四日" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="25" Editor="text" FieldName="二十五日" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="26" Editor="text" FieldName="二十六日" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="27" Editor="text" FieldName="二十七日" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="28" Editor="text" FieldName="二十八日" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="29" Editor="text" FieldName="二十九日" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="30" Editor="text" FieldName="三十日" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="31" Editor="text" FieldName="三十一日" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultDay" runat="server" BindingObjectID="dataFormDay" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="AutoKey" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="DefaultMethodYear" FieldName="Year" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="DefaultMethodSalesTypeID" FieldName="SalesTypeID" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" FieldName="SubClass" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateDay" runat="server" BindingObjectID="dataFormDay" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
