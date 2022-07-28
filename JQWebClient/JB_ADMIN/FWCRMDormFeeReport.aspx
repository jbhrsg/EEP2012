<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FWCRMDormFeeReport.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        $(document).ready(function () {
                     
            //預設費用年月
            var dt = new Date();
            var aDate = new Date($.jbDateAdd('months', -1, dt));//開始日期明天
            var date1 = $.jbjob.Date.DateFormat(aDate, 'yyyyMMdd').substring(0, 6);
            $("#YearMonth_Query").val(date1);//費用年月
            $("#CompanyID_Query").combobox('setValue', 1);

        });        
       
        function OnLoadSuccessGV() {           
            //Grid隱藏
            $('#dataGridView').datagrid('getPanel').hide();

        }
        function queryGrid(dg) {//查詢後添加固定條件
            
            var CompanyID = $('#CompanyID_Query').combobox('getValue');//公司別
            var YearMonth = $('#YearMonth_Query').val();//費用年月

            var url = "../JB_ADMIN/REPORT/FWCRM/FWCRMDormFeeReport.aspx?CompanyID=" + CompanyID + "&YearMonth=" + YearMonth;

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
                title: "宿舍收支表",
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
            <table>
                <tr>
                    <td colspan="2" class="auto-style1">
                        <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
                        <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sCustomersJobs.HUT_Job" runat="server" AutoApply="True" 
                            DataMember="HUT_Job" Pagination="True" QueryTitle="查詢條件" EditDialogID=""
                            Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" CheckOnSelect="True" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="15,30,45,60,65" PageSize="15" QueryAutoColumn="False" QueryLeft="10px" QueryMode="Panel" QueryTop="10px" RecordLock="False" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" ColumnsHibeable="False" RecordLockMode="None" Width="750px" BufferView="False" NotInitGrid="False" RowNumbers="True" OnLoadSuccess="OnLoadSuccessGV">
                            <Columns>
                                    <JQTools:JQGridColumn Alignment="right" Caption="職缺代號" Editor="numberbox" FieldName="JobID" Frozen="False" MaxLength="0" ReadOnly="True" Sortable="False" Visible="False" Width="60" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="客戶" Editor="text" EditorOptions="" FieldName="CustID" Width="120" Visible="False" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" />
                                    <JQTools:JQGridColumn Alignment="left" Caption="職缺名稱" Editor="text" FieldName="JobName" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="True" Visible="True" Width="180" />
                                    <JQTools:JQGridColumn Alignment="center" Caption="推薦人數" Editor="text" FieldName="iUser" FormatScript="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60"></JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustName" Frozen="False" MaxLength="0" ReadOnly="True" Sortable="True" Visible="True" Width="150" FormatScript="" />
                                    <JQTools:JQGridColumn Alignment="left" Editor="text" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="65" Caption="聯繫人員" FieldName="UpdateBy">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="執案顧問" Editor="text" EditorOptions="" FieldName="HunterName" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="True" Visible="True" Width="80" />
                                    <JQTools:JQGridColumn Alignment="center" Caption="開/關缺日" Editor="text" EditorOptions="" FieldName="DateString" Visible="True" Width="78" Format="" FormatScript="" Sortable="True" />
                                    <JQTools:JQGridColumn Alignment="center" Caption="執案天數" Editor="text" FieldName="sDays" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Total="" Visible="True" Width="55">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="right" Caption="預估營業額" Editor="numberbox" FieldName="Amount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Total="sum" Visible="True" Width="70" Format="N">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="center" Caption="需求數" Editor="numberbox" FieldName="JobNeedCount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="48">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="left" Caption="上班地點" Editor="text" FieldName="JobWorkArea" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                                    </JQTools:JQGridColumn>
                                    <JQTools:JQGridColumn Alignment="center" Caption="更新日期" Editor="datebox" FieldName="LastUpdateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="64" Format="yyyy/mm/dd">
                                    </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="更新人員" Editor="text" EditorOptions="" FieldName="LastUpdateBy" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="True" Visible="True" Width="60" />
                            </Columns>
                            <TooItems>
                                <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增職缺" />
<%--                                <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="查詢條件" />                                                                                                                            --%>
                                <JQTools:JQToolItem ID="JQToolItem2" Icon="icon-print" ItemType="easyui-linkbutton" OnClick="OpenNAR" Text="NAR"  />
                            </TooItems>
                            <QueryColumns>
                                 <JQTools:JQQueryColumn AndOr="and" Caption="公司別" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompName',remoteName:'sPowerData.infoCompID',tableName:'infoCompID',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="CompanyID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="88" />
                                 <JQTools:JQQueryColumn AndOr="and" Caption="費用年月" Condition="=" DataType="string" Editor="text" FieldName="YearMonth" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="60" /> 
                            </QueryColumns>
                        </JQTools:JQDataGrid>

                    </td>

                </tr>
            </table>
        </div>
       
       

    </form>
</body>
</html>
