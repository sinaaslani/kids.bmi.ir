<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ucDynamicPageAdminPanel.ascx.cs" Inherits="Site.Kids.bmi.ir.AdminCP.HomeWidget.ucDynamicPageAdminPanel"  %>
<table width="100%" style="WIDTH: 100%;">
	<tr>
		<th  align="right" height="25" class="TableHeaderText" style="FILTER:progid:DXImageTransform.Microsoft.Gradient(startColorStr=#FFFFFF, endColorStr=#0865BD, gradientType=1);FONT-FAMILY:Tahoma,Verdana,Arial,Helvetica"
			width="100%">
			<IMG src="/App_Themes/Admin/Images/admin_bullet.gif">&nbsp;مدیریت 
            صفحات</th>
	</tr>
	<tr>
		<td  align="right" width="100%">
			<asp:hyperlink id="AddNewsLnk" runat="server"  
                NavigateUrl="~/AdminCP/DynamicPages/DynamicPages_Admin.aspx">مدیریت صفحات دینامیک</asp:hyperlink></td>
	</tr>
	<tr>
		<td  align="right" width="100%" valign="top">&nbsp;</td>
	</tr>
	</table>
