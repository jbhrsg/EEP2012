<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OutWorkList.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script type="text/javascript">
         $(document).ready(function () {
             //查詢條件預設值
             var dt = new Date();
             var FirstDate = new Date($.jbGetFirstDate(dt));
             $("#OWDateS_Query").datebox('setValue', $.jbjob.Date.DateFormat(FirstDate, 'yyyy/MM/dd'));
             var LastDate = new Date($.jbGetLastDate(dt));
             $("#OWDateT_Query").datebox('setValue', $.jbjob.Date.DateFormat(LastDate, 'yyyy/MM/dd'));
         });

         function queryGrid(dg) {//查詢後添加固定條件
             if ($(dg).attr('id') == 'dataGridMaster') {
                 //查詢條件
                 var result = [];
                 var SDate = $('#OWDateS_Query').datebox('getValue');//開始日期
                 var EDate = $('#OWDateT_Query').datebox('getValue');//結束日期   
                 var ORG_NO = $('#ORG_NO_Query').combobox('getValue');
                 
                 if (SDate != '') result.push("OWDateS between '" + SDate + "' and '" + EDate + "'");
                 if (ORG_NO != '') result.push("ORG_NO ='" + ORG_NO + "'");

                 $(dg).datagrid('setWhere', result.join(' and '));
             }
         }



    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sOutWorkList.OutWorkMaster" runat="server" AutoApply="True"
                DataMember="OutWorkMaster" Pagination="True" QueryTitle="查詢"
                Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="組別" Editor="text" FieldName="ORG_DESC" Format="" MaxLength="0" Width="100" />
                    <JQTools:JQGridColumn Alignment="center" Caption="員工姓名" Editor="text" FieldName="USERNAME" Format="" MaxLength="0" Width="80" />
                    <JQTools:JQGridColumn Alignment="center" Caption="日期" Editor="datebox" FieldName="OWDateS" Format="" Width="100" />
                    <JQTools:JQGridColumn Alignment="center" Caption="時間" Editor="text" FieldName="OWDateT" Format="" MaxLength="0" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請內容" Editor="text" FieldName="ApplyGist" Format="" MaxLength="256" Width="600" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportXlsx" Text="匯出" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="起始日期" Condition="%" DataType="string" Editor="datebox" FieldName="OWDateS" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="～" Condition="%" DataType="string" Editor="datebox" FieldName="OWDateT" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="組別" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sOutWorkList.infoOutWorkORG_NO',tableName:'infoOutWorkORG_NO',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ORG_NO" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>
    <JQTools:JQDefault runat="server" BindingObjectID="dataGridMaster" BorderStyle="NotSet" Enabled="True" EnableTheming="True" ClientIDMode="Inherit" ID="defaultMaster" EnableViewState="True" ViewStateMode="Inherit" >
</JQTools:JQDefault>
<JQTools:JQValidate runat="server" BindingObjectID="dataGridMaster" BorderStyle="NotSet" Enabled="True" EnableTheming="True" ClientIDMode="Inherit" ID="validateMaster" EnableViewState="True" ViewStateMode="Inherit" >
</JQTools:JQValidate>
</form>
</body>
</html>
