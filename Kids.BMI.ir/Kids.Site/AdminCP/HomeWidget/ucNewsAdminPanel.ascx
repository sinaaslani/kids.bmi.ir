<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ucNewsAdminPanel.ascx.cs" Inherits="Site.Kids.bmi.ir.AdminCP.HomeWidget.newsAdminPanel"  %>
<table width="100%" style="WIDTH: 100%;">
	<tr>
		<th  align="right" height="25" class="TableHeaderText" style="FILTER:progid:DXImageTransform.Microsoft.Gradient(startColorStr=#FFFFFF, endColorStr=#0865BD, gradientType=1);FONT-FAMILY:Tahoma,Verdana,Arial,Helvetica"
			width="100%">
			<IMG src="/App_Themes/Admin/Images/admin_bullet.gif">&nbsp;مدیریت خبر</th>
	</tr>
	<tr>
		<td  align="right" width="100%">
			<asp:hyperlink id="AddNewsLnk" runat="server"  
                NavigateUrl="~/AdminCP/NewsAdmin/newsAdmin.aspx?act=new">&#1575;&#1610;&#1580;&#1575;&#1583; &#1582;&#1576;&#1585; &#1580;&#1583;&#1610;&#1583;</asp:hyperlink></td>
	</tr>
	<tr>
		<td  align="right" width="100%">
			<asp:hyperlink id="EditNewsLnk" runat="server"  
                NavigateUrl="~/AdminCP/NewsAdmin/newsList.aspx">&#1608;&#1610;&#1585;&#1575;&#1610;&#1588; &#1608; &#1581;&#1584;&#1601; &#1582;&#1576;&#1585;</asp:hyperlink></td>
	</tr>
	<tr>
		<td  align="right" style="HEIGHT: 0px" width="100%" valign="top">
            <br />
            </td>
	</tr>
	<TR>
		<TD align="right"  width="100%">
			<asp:hyperlink id="CreateNewsGroupLnk" runat="server"  
                NavigateUrl="~/AdminCP/NewsAdmin/newsCatAdmin.aspx?act=new">ایجاد موضوع خبری</asp:hyperlink></TD>
	</TR>
	<TR>
		<TD align="right"  width="100%">
			<asp:hyperlink id="EditNewsCatLnk" runat="server"  
                NavigateUrl="~/AdminCP/NewsAdmin/newsCatList.aspx">ویرایش و حذف موضوعات خبری</asp:hyperlink></TD>
	</TR>
	</table>
