<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBRPT_ShortTermRepoAcct.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script> 
    <script type="text/javascript">
        $(document).ready(function () {
            //將Focus 欄位背景顏色改為黃色
            $(function () {
                $("input, select, textarea").focus(function () {
                    $(this).css("background-color", "yellow");
                });
                $("input, select, textarea").blur(function () {
                    $(this).css("background-color", "white");
                });
            });
            //設定 Grid QueryColunm Windows width=320px
            var dgid = $('#dataGridView');
            var queryPanel = getInfolightOption(dgid).queryDialog;
            if (queryPanel)
                $(queryPanel).panel('resize', { width: 410 });
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sShortTermRepoAcct.ShortTerm" runat="server" AutoApply="True"
                DataMember="ShortTerm" Pagination="True" QueryTitle="輸出條件" EditDialogID="JQDialog1"
                Title="暫借款未結案報表" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="50px" QueryMode="Window" QueryTop="50px" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True" Width="920px" ReportFileName="~/JB_ADMIN/rShortTermAcct.rdlc">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="暫借單號" Editor="text" FieldName="ShortTermNo" Format="" MaxLength="20" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請人" Editor="text" FieldName="EmployeeName" Format="" MaxLength="0" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ShortTermDate" Format="yyyy/mm/dd" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="暫借主旨" Editor="text" FieldName="ShortTermGist" Format="" MaxLength="30" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請事由" Editor="text" FieldName="ShortTermDescr" Format="" MaxLength="200" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="需求日期" Editor="datebox" FieldName="RequestDate" Format="yyyy/mm/dd" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="預計歸還日" Editor="datebox" FieldName="PlanPayDate" Format="yyyy/mm/dd" Width="70" />
                    <JQTools:JQGridColumn Alignment="right" Caption="金額" Editor="numberbox" FieldName="ShortTermAmount" Format="" Width="60" />
                    <JQTools:JQGridColumn Alignment="right" Caption="給付方式" Editor="numberbox" FieldName="PayTypeID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="給付方式" Editor="text" FieldName="PayTypeName" Format="" MaxLength="0" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="工號" Editor="text" FieldName="EmployeeID" Format="" MaxLength="20" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="公司" Editor="numberbox" FieldName="CompanyID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="公司別" Editor="text" FieldName="CompanyName" Format="" MaxLength="0" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PayTo" Editor="text" FieldName="PayTo" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="給付對象" Editor="text" FieldName="VendName" Format="" MaxLength="0" Width="210" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="報表條件" />
                    <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportReport"  Text="列印輸出"  />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="公司別" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sShortTermRepoAcct.Company',tableName:'Company',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CompanyID" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>
            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="暫借款未結案報表">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="ShortTerm" HorizontalColumnsCount="2" RemoteName="sShortTermRepoAcct.ShortTerm" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="暫借單號" Editor="text" FieldName="ShortTermNo" Format="" maxlength="20" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="暫借主旨" Editor="text" FieldName="ShortTermGist" Format="" maxlength="30" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請事由" Editor="text" FieldName="ShortTermDescr" Format="" maxlength="200" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="預計歸還日" Editor="datebox" FieldName="PlanPayDate" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="金額" Editor="numberbox" FieldName="ShortTermAmount" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="給付方式" Editor="numberbox" FieldName="PayTypeID" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="PayTypeName" Editor="text" FieldName="PayTypeName" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="需求日期" Editor="datebox" FieldName="RequestDate" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ShortTermDate" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="工號" Editor="text" FieldName="EmployeeID" Format="" maxlength="20" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="EmployeeName" Editor="text" FieldName="EmployeeName" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司" Editor="numberbox" FieldName="CompanyID" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CompanyName" Editor="text" FieldName="CompanyName" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="PayTo" Editor="text" FieldName="PayTo" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="VendName" Editor="text" FieldName="VendName" Format="" maxlength="0" Width="180" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
