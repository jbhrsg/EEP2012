﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VBWebSingle4.aspx.vb" Inherits="Template_VBWebSingle4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <InfoLight:WebDataSource ID="Master" runat="server" AllowAdd="True" AllowDelete="True"
            AllowPrint="True" AllowUpdate="True" AlwaysClose="False" AutoApply="True" AutoApplyForInsert="False"
            AutoRecordLock="False" AutoRecordLockMode="NoneReload" CacheDataSet="True" CommandName=""
            DataMember="" Eof="True" KeyValues="" LastIndex="-1" Marker="'" MasterDataSource=""
            PacketRecords="100" QuotePrefix="[" QuoteSuffix="]" RemoteName="sEmployees.Employees"
            SelectCommand="" TableName="" WebDataSetID="WMaster">
        </InfoLight:WebDataSource>
    
    </div>
        <InfoLight:WebStatusStrip ID="WebStatusStrip1" runat="server" ContentBackColor=""
            ContentForeColor="White" Font-Bold="True" ForeColor="DarkSlateGray" ShowCompany="False"
            ShowDate="True" ShowEEPAlias="True" ShowNavigatorStatus="True" ShowSolution="False"
            ShowTitle="True" ShowUserID="True" ShowUserName="True" StatusBackColor="White"
            TitleForeColor="White" Width="100%" />
        <br />
        <ajaxTools:AjaxScriptManager ID="AjaxScriptManager1" runat="server" RenderExtShowModelScripts="True">
        </ajaxTools:AjaxScriptManager>
        <ajaxTools:AjaxGridView ID="AjaxGridView1" runat="server" DataSourceID="Master" EditPanelID="ExtModalPanel1">
            <ToolItems>
                <AjaxTools:ExtGridToolItem ButtonName="btnAdd" IconUrl="~/Image/Ext/add.gif" 
                    SysHandlerType="Add" Text="add" />
                <AjaxTools:ExtGridToolItem ButtonName="btnEdit" IconUrl="~/Image/Ext/edit.gif" 
                    SysHandlerType="Edit" Text="edit" />
                <AjaxTools:ExtGridToolItem ButtonName="btnDelete" 
                    IconUrl="~/Image/Ext/delete.gif" SysHandlerType="Delete" Text="delete" />
                <AjaxTools:ExtGridToolItem ToolItemType="Separation" />
                <AjaxTools:ExtGridToolItem ButtonName="btnQuery" 
                    IconUrl="~/Image/Ext/query.gif" SysHandlerType="Query" Text="query" />
                <AjaxTools:ExtGridToolItem ToolItemType="Fill" />
                <AjaxTools:ExtGridToolItem ButtonName="btnRefresh" 
                    IconUrl="~/Image/Ext/refresh.gif" Text="refresh" />
            </ToolItems>
        </ajaxTools:AjaxGridView>
        <ajaxTools:ExtModalPanel ID="ExtModalPanel1" runat="server" Height="188px" Width="621px" OnSubmit="ExtModalPanel1_Submit" OnCancel="ExtModalPanel1_Cancel">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" OnLoad="UpdatePanel1_Load">
            </asp:UpdatePanel>
        </ajaxTools:ExtModalPanel>
        <InfoLight:WebFormView ID="WebFormView1" runat="server" 
        DataSourceID="Master" LayOutColNum="2" Width="100%" SkinID="FormViewSkin1">
            <InsertRowStyle BackColor="DarkTurquoise" />
        </InfoLight:WebFormView>
    </form>
</body>
</html>
