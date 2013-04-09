<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ucFAQAdminPanel.ascx.cs" Inherits="Site.Kids.bmi.ir.AdminCP.HomeWidget.PAdminPanel"  %>
<table width="100%" style="WIDTH: 100%;HEIGHT: 199px">
	<tr>
		<th  align="right" height="25" class="TableHeaderText" style="FILTER:progid:DXImageTransform.Microsoft.Gradient(startColorStr=#FFFFFF, endColorStr=#0865BD, gradientType=1);FONT-FAMILY:Tahoma,Verdana,Arial,Helvetica"
			width="100%">
			<IMG src="/App_Themes/Admin/Images/admin_bullet.gif">&nbsp;مدیریت پرسش های متداول</th>
	</tr>
	<tr>
		<td  align="right" width="100%">
			<asp:hyperlink id="AddNewsLnk" runat="server"  NavigateUrl="~/AdminCP/FAQAdmin/FAQAdmin.aspx?act=new">تعریف یک پرسش جدید</asp:hyperlink></td>
	</tr>
	<tr>
		<td  align="right" width="100%" valign="top"> ثبت 
				یک&nbsp;پرسش و پاسخ جدید</td>
	</tr>
	<tr>
		<td  align="right" width="100%">
			<asp:hyperlink id="EditNewsLnk" runat="server"  NavigateUrl="~/AdminCP/FAQAdmin/FAQList.aspx">ویرایش و حذف پرسش ها</asp:hyperlink></td>
	</tr>
	<tr>
		<td  align="right" style="HEIGHT: 21px" width="100%" valign="top">
				فهرست&nbsp;پرسشهای ثبت شده&nbsp;، ویرایش اطلاعات هر&nbsp;پرسش&nbsp;&nbsp;و یا 
				حذف&nbsp;</td>
	</tr>
	<tr>
		<TD align="right"  width="100%">
			<asp:hyperlink id="CreateNewsGroupLnk" runat="server"  NavigateUrl="~/AdminCP/FAQAdmin/PCategoryAdmin.aspx?act=new">ایجاد گروه پرسش</asp:hyperlink></TD>
	</TR>
	<tr>
		<TD align="right"  width="100%" valign="top"> ایجاد 
				گروه&nbsp;برای&nbsp;دسته بندی پرسش ها</TD>
	</tr>
	<tr>
		<TD align="right"  width="100%">
			<asp:hyperlink id="EditNewsCatLnk" runat="server"  NavigateUrl="~/AdminCP/FAQAdmin/PCategoryList.aspx">ویرایش و حذف گروه های پرسش ها</asp:hyperlink></TD>
	</TR>
	<tr>
		<TD align="right"  width="100%" valign="top"> ویرایش&nbsp; 
				و حذف گروه های پرسش های موجود</TD>
	</tr>
</table>
