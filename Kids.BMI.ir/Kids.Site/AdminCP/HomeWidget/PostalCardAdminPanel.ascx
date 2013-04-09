<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PostalCardAdminPanel.ascx.cs" Inherits="Site.Kids.bmi.ir.AdminCP.HomeWidget.PostalCardAdminPanel"  %>
<table width="100%">
	<tr>
		<th  align="right" height="25" class="TableHeaderText" style="FILTER:progid:DXImageTransform.Microsoft.Gradient(startColorStr=#FFFFFF, endColorStr=#0865BD, gradientType=1);FONT-FAMILY:Tahoma,Verdana,Arial,Helvetica"
			width="100%">
			<IMG src="/App_Themes/Admin/Images/admin_bullet.gif">&nbsp;مديريت 
            کارت پستالها</th>
	</tr>
	 <tr>
        <td align="right" width="100%">
            <asp:HyperLink ID="AddNewsLnk" runat="server"  NavigateUrl="~/AdminCP/PostalCardAdmin/PostalCardAdmin.aspx?act=new">ایجاد کارت پستال جدید</asp:HyperLink>
        </td>
    </tr>
    <tr>
        <td align="right" width="100%">
            <asp:HyperLink ID="EditNewsLnk" runat="server"  NavigateUrl="~/AdminCP/PostalCardAdmin/PostalCardList.aspx">ویرایش و حذف کارت پستال</asp:HyperLink>
        </td>
    </tr>
	
	
</table>
