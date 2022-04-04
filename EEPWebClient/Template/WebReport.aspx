﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebReport.aspx.cs" Inherits="Template_WebReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <InfoLight:WebDataSource ID="Master" runat="server" WebDataSetID="WMaster">
        </InfoLight:WebDataSource>
        <InfoLight:WebClientQuery ID="WebClientQuery1" runat="server" DataSourceID="Master">
        </InfoLight:WebClientQuery>
        <asp:Panel ID="Panel1" runat="server" Height="133px" Width="100%">
        </asp:Panel>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Query" Width="94px" />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Clear" Width="89px" />
    </div>
        <rsweb:ReportViewer id="ReportViewer1" runat="server" width="100%"></rsweb:ReportViewer>
    </form>
</body>
</html>
