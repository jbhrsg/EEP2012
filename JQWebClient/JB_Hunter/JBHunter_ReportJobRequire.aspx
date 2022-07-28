<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBHunter_ReportJobRequire.aspx.cs" Inherits="Template_JQueryQuery1" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script> 
    <script src="../js/jquery.url.js"></script> 
     <script>   
         $(document).ready(function () {
             //客戶簡稱,產業別,職稱,到職地點加上ToolTip
             $('#CustShortName_Query').attr('title', '請輸入「客戶簡稱」關鍵字範例「耐落,金像....」');
             $('#IndCategory_Query').attr('title', '請輸入「產業別」關鍵字範例「半導體,顧問諮詢....」');
             $('#FunctionName_Query').attr('title', '請輸入「職缺類別」關鍵字範例「生產製造,業務....」');
             $('#sJobWorkArea_Query').attr('title', '請輸入「到職地點」關鍵字範例「台北,桃園....」');
             //從首頁過來帶參數=>10天
             InfoQuery();
         });
         //從首頁過來帶參數=>十日內
         function InfoQuery() {
             //url分析
             if ($.url.param("day") != null) {                                                                                 
                 queryGrid($("#dataGridMaster"));//自動查詢
             }
         }
         //發佈起始日期,給Query Column預設值(最新職缺(十日內) )=>從首頁過來帶參數=>10日內
         function QueryDefault1() {
             //url分析
             if ($.url.param("day") != null) {
                 var aDate = new Date();
                 var aDate2 = new Date($.jbDateAdd('days', -9, aDate));
                 return $.jbjob.Date.DateFormat(aDate2, 'yyyy/MM/dd');                 
             } 
         }
         //發佈終止日期
         function QueryDefault2() {
             //url分析
             if ($.url.param("day") != null) {
                 var aDate = new Date();
                 return $.jbjob.Date.DateFormat(aDate, 'yyyy/MM/dd');
             }
         }
         //兜查詢條件-----------------------------------------------------------
         function queryGrid(dg) {
             if ($(dg).attr('id') == 'dataGridMaster') {
                 var where = $(dg).datagrid('getWhere');
                 //查詢字串取代
                 if (where.length > 0) {
                     var aDate = new Date($("#JobCloseDate_Query").combobox('getValue'));
                     var JobCloseDate = $.jbjob.Date.DateFormat(aDate, 'yyyy-MM-dd');
                     //職缺效期JobCloseDate
                     where = where.replace("JobCloseDate>='" + JobCloseDate+"'", " (JobCloseDate is null or JobCloseDate>=" + JobCloseDate+")");
                     //客戶簡稱(以逗號分割)
                     var CustShortName = $("#CustShortName_Query").val();
                     if (CustShortName != '') {
                         var arrC = CustShortName.split(",");
                         var arr0 = "";
                         $.each(arrC, function (i, val) {
                             arr0 = "CustShortName like '%" + val + "%' or " + arr0;
                         });
                         where = where.replace("CustShortName='" + CustShortName + "'", '(' + arr0.substr(0, arr0.length - 3) + ')');
                     }
                     //產業別(以逗號分割)
                     var IndCategory = $("#IndCategory_Query").val();
                     if (IndCategory != '') {
                         var arrD = IndCategory.split(",");
                         var arr = "";
                         $.each(arrD, function (i, val) {
                             arr = "IndCategory like '%" + val + "%' or " + arr;
                         });
                         where = where.replace("IndCategory='" + IndCategory + "'", '(' + arr.substr(0, arr.length - 3) + ')');
                     }
                     //職缺類別(以逗號分割)
                     var FunctionName = $("#FunctionName_Query").val();
                     if (FunctionName != '') {
                         var arrT = FunctionName.split(",");
                         var arr2 = "";
                         $.each(arrT, function (i, val) {
                             arr2 = "FunctionName like '%" + val + "%' or " + arr2;
                         });
                         where = where.replace("FunctionName='" + FunctionName + "'", '(' + arr2.substr(0, arr2.length - 3) + ')');
                     }
                     //到職地點(以逗號分割)
                     var JobWorkArea = $("#sJobWorkArea_Query").val();
                     if (JobWorkArea != '') {
                         var arrA = JobWorkArea.split(",");
                         var arr3 = "";
                         $.each(arrA, function (i, val) {
                             arr3 = "(Isnull(z.JobAreaName,'')+Isnull(j.JobWorkArea,'')) like '%" + val + "%' or " + arr3;
                         });
                         where = where.replace("sJobWorkArea='" + JobWorkArea + "'", '(' + arr3.substr(0, arr3.length - 3) + ')');
                     }                     
                 }
                 if (where.substr(0, 5) == ' and ') {
                     where = where.substr(5, where.length);
                 }
                 $(dg).datagrid('setWhere', where);                
             }
         }
      </script>
   
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sReportJobRequire.ReportJobRequire" runat="server" AutoApply="True"
                DataMember="ReportJobRequire" Pagination="True" QueryTitle="條件查詢"
                Title="內部職缺通報" AllowDelete="False" AllowInsert="False" AllowUpdate="False" QueryMode="Panel" AlwaysClose="True" ViewCommandVisible="False" AllowAdd="True" CheckOnSelect="True" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" TotalCaption="Total:" UpdateCommandVisible="False" ColumnsHibeable="False" RecordLockMode="None" OnLoadSuccess="queryGrid" ReportFileName="~/JB_Hunter/rJobRequire.rdlc">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="發佈日期" Editor="datebox" FieldName="JobDeclareDate" Width="70" Format="yyyy/mm/dd" Sortable="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="獵才顧問" Editor="text" FieldName="HunterName" MaxLength="0" Width="70" Sortable="False" />
                    <JQTools:JQGridColumn Alignment="center" Caption="等級" Editor="text" FieldName="CustomerGrade" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶簡稱" Editor="text" FieldName="CustShortName" MaxLength="0" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="產業別" Editor="text" FieldName="IndCategory" MaxLength="0" Width="110" />
                    <JQTools:JQGridColumn Alignment="left" Caption="訂單編碼" Editor="text" FieldName="OrderID" Width="110" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職缺類別" Editor="text" FieldName="FunctionName" MaxLength="0" Width="110" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職缺名稱" Editor="text" FieldName="JobName" Width="120" Frozen="False" />
                    <JQTools:JQGridColumn Alignment="center" Caption="需求人數" Editor="text" FieldName="JobNeedCount" MaxLength="0" Width="55" />
                    <JQTools:JQGridColumn Alignment="left" Caption="性別-年齡" Editor="text" FieldName="Age" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="程度-科系" Editor="text" FieldName="EduDepart" MaxLength="0" Width="150" />
                    <JQTools:JQGridColumn Alignment="left" Caption="語文限制" Editor="text" FieldName="LanguageString" MaxLength="0" Width="200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職缺資訊" Editor="text" FieldName="JobWorkContent" MaxLength="0" Width="200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="薪資福利" Editor="text" FieldName="JobFare" MaxLength="0" Width="200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="工作地點" Editor="text" FieldName="sJobWorkArea" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="JobNotes" MaxLength="0" Width="100" />
                </Columns>
                <TooItems>
                  
                    <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Excel輸出" Visible="False" />
                  
                    <JQTools:JQToolItem Enabled="True" Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportReport" Text="報表" Visible="True" />
                  
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn Caption="客戶簡稱" Condition="=" DataType="string" Editor="text" FieldName="CustShortName" NewLine="False" RemoteMethod="False" Width="125" EditorOptions="" />
                    <JQTools:JQQueryColumn Caption="職缺名稱" Condition="%" DataType="string" Editor="text" FieldName="JobName" NewLine="False" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="獵才顧問" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sHunter.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="j.HunterID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="業務單位" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'SalesTeamName',remoteName:'sSalesTeam.HUT_SalesTeam',tableName:'HUT_SalesTeam',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="c.SalesTeamID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="產業別" Condition="=" DataType="string" Editor="text" FieldName="IndCategory" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="職缺類別" Condition="=" DataType="string" Editor="text" FieldName="FunctionName" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="到職地點" Condition="=" DataType="string" Editor="text" FieldName="sJobWorkArea" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="職缺效期" Condition="&gt;=" DataType="datetime" DefaultValue="_today" Editor="datebox" FieldName="JobCloseDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="發佈起始日期" Condition="&gt;=" DataType="datetime" Editor="datebox" FieldName="JobDeclareDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="90" DefaultMethod="QueryDefault1" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="發佈終止日期" Condition="&lt;=" DataType="datetime" DefaultValue="" Editor="datebox" FieldName="JobDeclareDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="90" DefaultMethod="QueryDefault2" />
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>

    </form>
</body>
</html>
