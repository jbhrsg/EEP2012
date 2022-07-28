<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryReport.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script>
         ///=============================================  ready  ===============================================================================================
         $(document).ready(function () {
             var dt = new Date();             
             $("#AddDate_Query").val(dt.getFullYear().toString());
         });
         //是否失效(明細)CheckBox=>不可以編輯
         function genStats(val) {
             if (val == "0")
                 return "未完成";
             else if (val == "1")
                 return "OK";
         }
         function queryGrid(dg) {//查詢後添加固定條件 
             if ($(dg).attr('id') == 'dataGridMaster') {
                 var result = [];
                 var syear = $("#AddDate_Query").val();//年份               
                 if (syear != '') result.push("DATEPART(yyyy, LastLoginDate) = " + syear);

                 var CompanyCName = $("#CompanyCName_Query").val();//公司名稱
                 if (CompanyCName != '') result.push("CompanyCName like '%" + CompanyCName + "%'");

                 var ValidateCount = $("#ValidateCount_Query").checkbox('getValue');//完成?
                 if (ValidateCount == false) {
                     result.push("ValidateCount!=9");
                 } else result.push("ValidateCount=9");

                 $(dg).datagrid('setWhere', result.join(' and '));
             }
         }


     </script> 



</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sSalaryReport.SalaryReport" runat="server" AutoApply="True"
                DataMember="SalaryReport" Pagination="False" QueryTitle="查詢條件"
                Title="客戶填寫進度" AllowAdd="True" AllowDelete="True" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="13,26,39,52,65" PageSize="13" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="註冊日期" Editor="datebox" FieldName="AddDate" Format="" MaxLength="0" Width="65" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="公司統編" Editor="text" FieldName="CompanyID" Format="" MaxLength="0" Width="62" Frozen="True" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="公司名稱" Editor="text" FieldName="CompanyCName" Format="" MaxLength="0" Width="140" Frozen="True" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="填寫人" Editor="text" FieldName="ContactName" Format="" MaxLength="0" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="填寫人職稱" Editor="text" FieldName="ContactTitle" Format="" MaxLength="0" Width="75" />
                    <JQTools:JQGridColumn Alignment="left" Caption="填寫人電話" Editor="text" FieldName="ContactTel" Format="" MaxLength="0" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="分機" Editor="text" FieldName="ContactTelExt" Format="" MaxLength="0" Width="40" />
                    <JQTools:JQGridColumn Alignment="left" Caption="填寫人Mail" Editor="text" FieldName="ContactMail" Format="" MaxLength="0" Width="170" />
                    <JQTools:JQGridColumn Alignment="center" Caption="完成數" Editor="numberbox" FieldName="ValidateCount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="55">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="基本資料" Editor="text" FieldName="Validate0" FormatScript="genStats" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="工作條件" Editor="text" FieldName="Validate" Format="" Width="60" EditorOptions="" FormatScript="genStats" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="薪資制度" Editor="text" FieldName="Validate1" Format="" Width="60" FormatScript="genStats" Sortable="True"/>
                    <JQTools:JQGridColumn Alignment="center" Caption="新進薪資I" Editor="text" FieldName="Validate2" Format="" Width="70" FormatScript="genStats" Sortable="True"/>
                    <JQTools:JQGridColumn Alignment="center" Caption="新進薪資II" Editor="text" FieldName="Validate3" Format="" Width="70" FormatScript="genStats" Sortable="True"/>
                    <JQTools:JQGridColumn Alignment="center" Caption="新進薪資III" Editor="text" FieldName="Validate4" Format="" Width="70" FormatScript="genStats" Sortable="True"/>
                    <JQTools:JQGridColumn Alignment="center" Caption="現有薪資" Editor="text" FieldName="Validate5" Format="" Width="60" FormatScript="genStats" Sortable="True"/>
                    <JQTools:JQGridColumn Alignment="center" Caption="福利制度" Editor="text" FieldName="Validate6" Format="" Width="60" FormatScript="genStats" Sortable="True"/>
                    <JQTools:JQGridColumn Alignment="center" Caption="外籍勞工福利" Editor="text" FieldName="Welfare" Format="" Width="78" FormatScript="genStats" Sortable="True"/>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="年份" Condition="=" DataType="string" Editor="text" FieldName="AddDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="50" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="公司名稱" Condition="%" DataType="string" Editor="text" FieldName="CompanyCName" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="190" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="完成?" Condition="=" DataType="string" Editor="checkbox" FieldName="ValidateCount" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="25" />
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>
    <JQTools:JQDefault runat="server" BindingObjectID="dataGridMaster" EnableTheming="True" ID="defaultMaster" >
</JQTools:JQDefault>
<JQTools:JQValidate runat="server" BindingObjectID="dataGridMaster" BorderStyle="NotSet" Enabled="True" EnableTheming="True" ClientIDMode="Inherit" ID="validateMaster" EnableViewState="True" ViewStateMode="Inherit" >
</JQTools:JQValidate>
</form>
</body>
</html>
