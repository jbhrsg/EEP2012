<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBHunter_ReportJobRequire.aspx.cs" Inherits="Template_JQueryQuery1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
   <%-- <style>
    #querydataGridMaster table td:nth-child(even)
    {
    width:300px;    
    }
    </style> --%>
     <script>   
         //function columnMatch() {
         //    var refRow = $(this).data("inforefval").panel.find('table.refval-grid').datagrid('getSelected');//取得refal當前選中的資料row
         //    var row = $('#dataGridDetail').datagrid('getSelected');
         //    var rowIndex = $('#dataGridMaster').datagrid('getRowIndex', row);//找到被編輯的grid的rowindex
         //    //var editor = $('#dataGridMaster').datagrid('getEditor', { index: rowIndex, field: 'UnitPrice' });//找到UnitPrice的編輯元件
         //    //editor.actions.setValue(editor.target, refRow.UnitPrice);//給值

         //}
      </script>
   
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sReportJobRequire.ReportJobRequire" runat="server" AutoApply="True"
                DataMember="ReportJobRequire" Pagination="True" QueryTitle="條件查詢"
                Title="內部職缺通報" AllowDelete="False" AllowInsert="False" AllowUpdate="False" QueryMode="Panel" AlwaysClose="True" ViewCommandVisible="False" AllowAdd="True" CheckOnSelect="True" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" TotalCaption="Total:" UpdateCommandVisible="False">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="發佈日期" Editor="datebox" FieldName="CreateDate" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="負責顧問" Editor="text" FieldName="HunterName" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustName" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="產業別" Editor="text" FieldName="IndCategory" MaxLength="0" Width="110" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職務類別" Editor="text" FieldName="FunctionName" MaxLength="0" Width="110" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職務名稱" Editor="text" FieldName="JobName" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="缺額" Editor="text" FieldName="JobNeedCount" MaxLength="0" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職缺性別" Editor="text" FieldName="Gender" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職缺年齡" Editor="text" FieldName="Age" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職缺教育程度" Editor="text" FieldName="EducationLevelText" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職缺需求學類" Editor="text" FieldName="EduSubject" MaxLength="0" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職缺需求科系" Editor="text" FieldName="EduDepart" MaxLength="0" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職缺語文限制" Editor="text" FieldName="LanguageString" MaxLength="0" Width="200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職務內容" Editor="text" FieldName="JobWorkContent" MaxLength="0" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="待遇福利" Editor="text" FieldName="JobFare" MaxLength="0" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="到職地點" Editor="text" FieldName="JobWorkArea" MaxLength="0" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="JobNotes" MaxLength="0" Width="100" />
                </Columns>
                <TooItems>
                  
                    <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Excel輸出" Visible="True" />
                  
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn Caption="客戶名稱" Condition="%%" DataType="string" Editor="inforefval" FieldName="j.CustID" NewLine="True" RemoteMethod="False" Width="125" EditorOptions="title:'選擇客戶',panelWidth:350,remoteName:'sCustomersJobs.HUT_Customer',tableName:'HUT_Customer',columns:[],columnMatches:[],whereItems:[],valueField:'CustID',textField:'CustShortName',valueFieldCaption:'CustID',textFieldCaption:'CustShortName',cacheRelationText:true,checkData:true,showValueAndText:false,selectOnly:false" />
                    <JQTools:JQQueryColumn Caption="是否有效" Condition="&gt;=" DataType="datetime" DefaultValue="_today" Editor="datebox" FieldName="JobDateEnd" NewLine="True" RemoteMethod="False" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>

    </form>
</body>
</html>
