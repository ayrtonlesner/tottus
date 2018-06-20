<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MantDashboard.aspx.cs" Inherits="LocalIntranet.reportes.gestionRequerimiento.MantDashboard" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
        <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        </div>

        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="1200px" Width="100%">
        </rsweb:ReportViewer>

    </form>
</body>
</html>
