<%@ Control Language="c#" AutoEventWireup="True" Codebehind="BankExamAdminPanel.ascx.cs" Inherits="Site.Kids.bmi.ir.AdminCP.HomeWidget.BankExamAdminPanel"  %>
<table width="100%" style="WIDTH:100%;HEIGHT:118px">
	<tr>
		<th  align="right" height="25" class="TableHeaderText" style="FILTER:progid:DXImageTransform.Microsoft.Gradient(startColorStr=#FFFFFF, endColorStr=#0865BD, gradientType=1);FONT-FAMILY:Tahoma,Verdana,Arial,Helvetica"
			width="100%">
			<IMG src="/App_Themes/Admin/Images/admin_bullet.gif">&nbsp;مديريت 
            آزمونها</th>
	</tr>
	<tr>
		<td  align="right" width="100%">
			<asp:hyperlink id="addPollLnk" runat="server"  NavigateUrl="~/AdminCP/BankExamAdmin/AddEditBankExam.aspx?act=new">تعريف آزمون</asp:hyperlink></td>
	</tr>
	<tr>
		<td  align="right" width="100%" valign="top" style="HEIGHT: 21px">ثبت 
				يک سوال نظر سنجي در سيستم</td>
	</tr>
	<tr>
		<td  align="right" width="100%">
			<asp:hyperlink id="pollListLnk" runat="server"  NavigateUrl="~/AdminCP/BankExamAdmin/BankExamList.aspx">ليست آزمونها</asp:hyperlink></td>
	</tr>
	<tr>
		<td  align="right" style="HEIGHT: 0px" width="100%" valign="top">
				نمايش ليست سوالات و نتايج آنها</td>
	</tr>
</table>
