<%@ Control Language="c#" AutoEventWireup="True" Codebehind="WishesAdminPanel.ascx.cs" Inherits="Site.Kids.bmi.ir.AdminCP.HomeWidget.WishesAdminPanel"  %>
<table width="100%">
	<tr>
		<th  align="right" height="25" class="TableHeaderText" style="FILTER:progid:DXImageTransform.Microsoft.Gradient(startColorStr=#FFFFFF, endColorStr=#0865BD, gradientType=1);FONT-FAMILY:Tahoma,Verdana,Arial,Helvetica"
			width="100%">
			<IMG src="/App_Themes/Admin/Images/admin_bullet.gif">&nbsp;مديريت 
            آرزوها</th>
	</tr>
	<tr>
		<td  align="right" width="100%">
			<asp:hyperlink id="addPollLnk" runat="server"  NavigateUrl="~/AdminCP/WishesAdmin/WishesAdmin_Admin.aspx">تعريف و مدیریت آرزو ها</asp:hyperlink></td>
	</tr>
	
	
</table>
