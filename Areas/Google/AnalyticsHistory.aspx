<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AnalyticsHistory.aspx.cs" Inherits="atlasNOC.Google.AnalyticsHistory" %>

<%@ Register Src="~/Controls/report.ascx" TagName="report" TagPrefix="ga" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

<div style="width:1175px;">
        <!-- Must have scripts in correct order -->
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/js/jquery-1.9.1.min.js" />
                <asp:ScriptReference Path="https://www.google.com/jsapi" />
                <asp:ScriptReference Path="~/js/chart-config.js" />
            </Scripts>
        </asp:ScriptManager>

        <!-- All reports should be added between the ScriptManager and the init-analytics script -->
        <div style="margin-bottom:10px;height:330px;width:100%;">
            <div style="width:340px; height:330px; display:inline-block; border: 1px solid blue; margin-right:10px;">
                <ga:report id="report_Region" runat="server" />
            </div>
            <div style="width:780px; height:330px; display:inline-block; border: 1px solid blue; ">
                <ga:report id="report_RegionMap" runat="server" />
            </div>
        </div>
        <div>
<%--            <div style="width:300px; display:inline-block; border: 1px solid blue; margin-right:10px;">
            </div>--%>
            <div style="width:340px; height:305px; display:inline-block; border: 1px solid blue; margin-right:8px;">
                <ga:report id="report_Device" runat="server" />
            </div>
            <div style="width:340px; height:305px; display:inline-block; border: 1px solid blue; margin-right:8px;">
                <ga:report id="report_BrowserOS" runat="server" />
            </div>
            <div style="width:428px; height:305px; display:inline-block; border: 1px solid blue; ">
                <ga:report id="report_Pages" runat="server" />
            </div>
        </div>


        <!-- do not forget to add init-analytics script as last script at end of body -->
        <script type="text/javascript" src="/js/init-analytics.js"></script>
    </div>
    </form>
</body>
</html>
