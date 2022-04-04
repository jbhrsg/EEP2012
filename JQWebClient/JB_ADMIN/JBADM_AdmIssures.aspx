<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBADM_AdmIssures.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sADMIssures.AdmIssures" runat="server" AutoApply="True"
                DataMember="AdmIssures" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="需求待辦事項" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="1100px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="IssureType" Editor="numberbox" FieldName="IssureType" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="系統分類" Editor="text" FieldName="IssureTypeName" Format="" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="事項分類" Editor="text" FieldName="SubType" MaxLength="0" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="提出日期" Editor="datebox" FieldName="ApplyDate" Format="yyyy/mm/dd " Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="事項內容" Editor="textarea" FieldName="IssureDescription" Format="" MaxLength="0" Width="400" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyEmpID" Editor="text" FieldName="ApplyEmpID" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="提出人員" Editor="text" FieldName="ApplyName" Format="" MaxLength="0" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="AssumeEmpID" Editor="text" FieldName="AssumeEmpID" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="負責人員" Editor="text" FieldName="AssumeName" Format="" MaxLength="0" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="需求日期" Editor="datebox" FieldName="RequireDate" Format="yyyy/mm/dd" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="預計完成日" Editor="datebox" FieldName="EstimateDate" Format="yyyy/mm/dd" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="實際完成日" Editor="text" FieldName="FinishDate" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Flowflag" Editor="text" FieldName="Flowflag" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="需求待辦事項" Width="660px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="AdmIssures" HorizontalColumnsCount="3" RemoteName="sADMIssures.AdmIssures" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="right" Caption="編號" Editor="numberbox" FieldName="IssureID" Format="" Width="117" ReadOnly="True" Span="3" />
                        <JQTools:JQFormColumn Alignment="left" Caption="提出人員" Editor="infocombobox" FieldName="ApplyEmpID" Format="" Width="120" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sADMIssures.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" maxlength="0" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="提出日期" Editor="datebox" FieldName="ApplyDate" Format="yyyy/mm/dd" Width="100" ReadOnly="True" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="系統分類" Editor="infocombobox" FieldName="IssureType" Format="" Width="120" EditorOptions="valueField:'IssureTypeID',textField:'IssureTypeName',remoteName:'sADMIssures.IssureType',tableName:'IssureType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="事項分類" Editor="text" FieldName="SubType" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="315" />
                        <JQTools:JQFormColumn Alignment="left" Caption="事項內容" Editor="textarea" FieldName="IssureDescription" Format="" maxlength="0" Width="500" EditorOptions="height:160" RowSpan="1" Span="3" />
                        <JQTools:JQFormColumn Alignment="left" Caption="負責人員" Editor="inforefval" FieldName="AssumeEmpID" Format="" Width="120" EditorOptions="title:'負責人員搜尋',panelWidth:350,remoteName:'sADMIssures.Employee',tableName:'Employee',columns:[],columnMatches:[],whereItems:[],valueField:'EmployeeID',textField:'EmployeeName',valueFieldCaption:'EmployeeID',textFieldCaption:'EmployeeName',cacheRelationText:false,checkData:false,showValueAndText:false,selectOnly:false" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="預計完成日" Editor="datebox" FieldName="EstimateDate" Format="yyyy/mm/dd" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="實際完成日" Editor="datebox" FieldName="FinishDate" Format="yyyy/mm/dd" maxlength="0" Width="120" Visible="True" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Flowflag" Editor="text" FieldName="Flowflag" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="需求日期" Editor="datebox" FieldName="RequireDate" Format="yyyy/mm/dd" Visible="False" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="IssureTypeName" Editor="text" FieldName="IssureTypeName" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ApplyName" Editor="text" FieldName="ApplyName" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="AssumeName" Editor="text" FieldName="AssumeName" Format="" maxlength="0" Width="180" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="ApplyEmpID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="ApplyDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="RequireDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="AssumeEmpID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="IssureID" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
