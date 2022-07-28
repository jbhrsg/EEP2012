<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Media_Report_CustomerGroupSalesList.aspx.cs" Inherits="Template_JQueryQuery1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script>
        function dataGridMaster_OnLoadSuccess() {
            //panel寬度調整
            var dgid = $('#dataGridMaster');
            var queryPanel = getInfolightOption(dgid).queryDialog;
            if (queryPanel)
                $(queryPanel).panel('resize', { width: 600 });
            //Grid隱藏
            $('#dataGridMaster').datagrid('getPanel').hide();
        }

        function queryGrid(dg) {//查詢後添加固定條件

            var ReportType = $('#ReportType_Query').options('getValue');
            if (ReportType == "") {
                alert("請選取「報表格式」");
                return false;
            }

            var CustNO = $("#CustNO_Query").refval('getValue');
            var arrSalesID = $("#SalesID_Query").combogrid("getValues");
            var SalesDateFrom = $('#SalesDate_Query').datebox('getValue');
            var SalesDateTo = $('#SalesDate_Query[infolight-options*="銷貨日期迄"] ').datebox('getValue');
            var arrSalesTypeID = $('#SalesTypeID_Query').combogrid('getValues');
            var arrDMTypeID = $("#DMTypeID_Query").combogrid("getValues");//版別(求才便利)
            var arrViewAreaID = $("#ViewAreaID_Query").combogrid("getValues");
            var ReportType = $('#ReportType_Query').options('getValue');//報表格式	1明細 2彙總
            var arrNewsAreaID = $("#NewsAreaID_Query").combogrid("getValues");//版別(報紙)
            var LetterType = $("#LetterType_Query").combobox("getValues");//信封備註

            for (var i = 0; i < arrSalesID.length; i++) {
                arrSalesID[i] = "'" + arrSalesID[i] + "'";
            }
            var SalesID = arrSalesID.join("*");

            for (var i = 0; i < arrSalesTypeID.length; i++) {
                arrSalesTypeID[i] = "'" + arrSalesTypeID[i] + "'";
            }
            var SalesTypeID = arrSalesTypeID.join("*");

            for (var i = 0; i < arrDMTypeID.length; i++) {
                arrDMTypeID[i] = "'" + arrDMTypeID[i] + "'";
            }
            var DMTypeID = arrDMTypeID.join("*");

            for (var i = 0; i < arrViewAreaID.length; i++) {
                arrViewAreaID[i] = "'" + arrViewAreaID[i] + "'";
            }
            var ViewAreaID = arrViewAreaID.join("*");

            for (var i = 0; i < arrNewsAreaID.length; i++) {
                arrNewsAreaID[i] = "'" + arrNewsAreaID[i] + "'";
            }
            var NewsAreaID = arrNewsAreaID.join("*");

            var url = "../JB_ADMIN/REPORT/Media/Media_Report_CustomerGroupSalesList_RV.aspx?CustNO=" + CustNO + "&SalesID=" + SalesID + "&SalesDateFrom=" + SalesDateFrom + "&SalesDateTo=" + SalesDateTo + "&SalesTypeID=" + SalesTypeID + "&DMTypeID=" + DMTypeID + "&ViewAreaID=" + ViewAreaID + "&ReportType=" + ReportType + "&NewsAreaID=" + NewsAreaID + "&LetterType=" + LetterType;// + "&iType=" + iType + "&CompanyText=" + CompanyText;

            var height = $(window).height() - 20;
            var height2 = $(window).height() - 90;
            var width = $(window).width() - 20;
            var dialog = $('<div/>')
            .dialog({
                draggable: false,
                modal: true,
                height: height,
                //top:0,
                width: width,
                title: "Report",
                //maximizable: true                              
            });
            $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="98%"></iframe>').appendTo(dialog.find('.panel-body'));
            dialog.dialog('open');
        }

        function clearQuery(dgid) {
            var isQueryAutoColumn = getInfolightOption($(dgid)).queryAutoColumn;
            if (isQueryAutoColumn == true) {
                var pnid = getInfolightOption($(dgid)).queryDialog;
                var queryTr = $('#queryTr_' + $(dgid)[0].id);
                var queryParams = $(dgid).datagrid('options').queryParams;
                var queryWord = new Object();

                var where = '';
                $(":text,select", queryTr).each(function () {
                    var text = $(this);
                    text.val('');
                });
            }

            var pnid = getInfolightOption($(dgid)).queryDialog;
            if (pnid != undefined) {
                var queryParams = $(dgid).datagrid('options').queryParams;
                var queryWord = new Object();

                var where = '';
                //jbjob edit by serlina 增加textarea
                $(":text,select,textarea", pnid).each(function () {
                    var text = $(this);
                    text.val('');
                    var controlClass = $(this).attr('class');
                    if (controlClass != undefined) {
                        if (controlClass.indexOf('easyui-datebox') == 0) {
                            text.datebox('setValue', '');
                        }
                        else if (controlClass.indexOf('easyui-datetimebox') == 0) {
                            text.datetimebox('setValue', '');
                        }
                        else if (controlClass.indexOf('info-combobox') == 0) {
                            text.combobox('setValue', '');
                        }
                        else if (controlClass.indexOf('info-combogrid') == 0) {
                            text.combogrid('setValue', '');
                            text.combogrid('clear');
                            text.combogrid('setWhere', '');
                        }
                        else if (controlClass.indexOf('combo-text') == 0) {
                            value = '';
                        }
                        else if (controlClass.indexOf('info-refval') == 0) {
                            text.refval('setValue', '');
                        }
                        else if (controlClass.indexOf('info-autocomplete') == 0) {
                            text.combobox('setValue', '');
                        }
                    }
                });
                $(":radio,:checkbox", pnid).each(function () {
                    $(this).prop("checked", false);
                });
            }
        }
   </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sMedia_Report_CustomerGroupSalesList.QueryColumn" runat="server" AutoApply="True"
                DataMember="QueryColumn" Pagination="True" QueryTitle="查詢條件"
                Title="" AllowDelete="False" AllowInsert="False" AllowUpdate="False" QueryMode="Panel" AlwaysClose="True" AllowAdd="False" ViewCommandVisible="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" OnLoadSuccess="dataGridMaster_OnLoadSuccess">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="CustTelNO" Editor="text" FieldName="CustNO" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesID" Editor="text" FieldName="SalesID" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesDate" Editor="text" FieldName="SalesDate" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesTypeID" Editor="text" FieldName="SalesTypeID" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="DMTypeID" Editor="text" FieldName="DMTypeID" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ViewAreaID" Editor="text" FieldName="ViewAreaID" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ReportType" Editor="text" FieldName="ReportType" MaxLength="0" Width="90" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="Insert" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="Update" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="Delete" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="Apply" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-cancel" ItemType="easyui-linkbutton" OnClick="cancel" Text="Cancel" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="Query" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Export" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶" Condition="%" DataType="string" Editor="inforefval" FieldName="CustNO" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="150" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'sMedia_Report_CustomerGroupSalesList.infoCustomers',tableName:'infoCustomers',columns:[{field:'CustNO',title:'代碼',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'CustName',title:'名稱',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'CustTelNO',title:'電話',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'CustNO',textField:'CustName',valueFieldCaption:'CustNO',textFieldCaption:'CustName',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="業務" Condition="%" DataType="string" Editor="infocombogrid" FieldName="SalesID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="200" EditorOptions="panelWidth:350,valueField:'SalesID',textField:'SalesName',remoteName:'sMedia_Report_CustomerGroupSalesList.infoSalesMan',tableName:'infoSalesMan',valueFieldCaption:'代碼',textFieldCaption:'業務',selectOnly:false,checkData:false,columns:[],cacheRelationText:false,multiple:true" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="銷貨日期起" Condition="%" DataType="string" Editor="datebox" FieldName="SalesDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="銷貨日期迄" Condition="%" DataType="string" Editor="datebox" FieldName="SalesDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="銷貨類別" Condition="%" DataType="string" Editor="infocombogrid" FieldName="SalesTypeID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="200" EditorOptions="panelWidth:350,valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sMedia_Report_CustomerGroupSalesList.infoERPSalesType',tableName:'infoERPSalesType',valueFieldCaption:'代碼',textFieldCaption:'銷貨類別',selectOnly:false,checkData:false,columns:[],cacheRelationText:false,multiple:true" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="版別(求才、便利)" Condition="%" DataType="string" Editor="infocombogrid" FieldName="DMTypeID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="200" EditorOptions="panelWidth:350,valueField:'DMTypeID',textField:'DMTypeName',remoteName:'sMedia_Report_CustomerGroupSalesList.infoERPDMType',tableName:'infoERPDMType',valueFieldCaption:'代碼',textFieldCaption:'版別',selectOnly:false,checkData:false,columns:[],cacheRelationText:false,multiple:true" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="版別(報紙)" Condition="%" DataType="string" Editor="infocombogrid" EditorOptions="panelWidth:350,valueField:'NewsAreaID',textField:'NewsAreaName',remoteName:'sMedia_Report_CustomerGroupSalesList.infoERPNewsArea',tableName:'infoERPNewsArea',valueFieldCaption:'代碼',textFieldCaption:'版別',selectOnly:false,checkData:false,columns:[],cacheRelationText:false,multiple:true" FieldName="NewsAreaID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="區域(求才、便利)" Condition="%" DataType="string" Editor="infocombogrid" FieldName="ViewAreaID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="200" EditorOptions="panelWidth:350,valueField:'ViewAreaID',textField:'ViewAreaName',remoteName:'sMedia_Report_CustomerGroupSalesList.infoERPViewArea',tableName:'infoERPViewArea',valueFieldCaption:'代碼',textFieldCaption:'區域',selectOnly:false,checkData:false,columns:[],cacheRelationText:false,multiple:true" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="信封備註" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'LetterTypeID',textField:'LetterTypeName',remoteName:'sMedia_Report_CustomerGroupSalesList.ERPLetterType',tableName:'ERPLetterType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="LetterType" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="報表格式" Condition="%" DataType="string" Editor="infooptions" FieldName="ReportType" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="2" Width="80" EditorOptions="title:'JQOptions',panelWidth:300,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:3,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'明細',value:'1'},{text:'彙總',value:'2'}]" />
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>

    </form>
</body>
</html>
