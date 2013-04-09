<%@ Page Language="c#" Theme="Admin" AutoEventWireup="True"  MasterPageFile="~/Masters/Admin.Master" Codebehind="FAQAdmin.aspx.cs" Inherits="Site.Kids.bmi.ir.AdminCP.FAQAdmin.FAQAdmin"  %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table height="60" width="529" style="WIDTH: 529px; HEIGHT: 60px" dir="ltr">
	<tr>
		<td  style="HEIGHT: 26px" align="right" width="100%" colSpan="2" >&nbsp;
			<IMG src="/App_Themes/Admin/Images/admin_bullet.gif">
			
            <asp:Literal id="lblHead" runat="server" ></asp:Literal>
            
            </td>
	</tr>
	<tr>
		<td  style="WIDTH: 414px" width="414" ></td>
		<td  ></td>
	</tr>
	<tr>
		<td  style="WIDTH:100%" width="100%" ><asp:textbox id="txtTitle" runat="server" Height="31px" TextMode="MultiLine" Width="390px"></asp:textbox><br>
			<asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle"
				ErrorMessage="RequiredFieldValidator">خطا : عنوان سوال الزامی است</asp:requiredfieldvalidator></td>
		<td  vAlign="top" >عنوان پرسش:</td>
	</tr>
	<tr>
		<TD style="WIDTH: 414px" width="414"  >
			<asp:DropDownList id="ddlCatList" runat="server"></asp:DropDownList></TD>
		<TD vAlign="top"  >گروه پرسش:</TD>
	</TR>
	<tr>
		<TD style="WIDTH: 414px" width="414"  >
			<asp:textbox id="summaryctrl" runat="server" Width="390px" TextMode="MultiLine" Visible="False"></asp:textbox></TD>
		<TD vAlign="top"  ></TD>
	</TR>
	<tr>
		<td  style="WIDTH: 414px" align="right" width="414" height="38"><asp:textbox id="txtBody" runat="server" Height="277px" TextMode="MultiLine" Width="390px"></asp:textbox></td>
		<td  vAlign="top" height="38">پاسخ :</td>
	</tr>
	<tr>
		<td  style="WIDTH: 414px" width="414" height="35"><asp:dropdownlist id="SortOrder" runat="server">
				<asp:ListItem Value="1">1</asp:ListItem>
				<asp:ListItem Value="2">2</asp:ListItem>
				<asp:ListItem Value="3">3</asp:ListItem>
				<asp:ListItem Value="4">4</asp:ListItem>
				<asp:ListItem Value="5">5</asp:ListItem>
				<asp:ListItem Value="6">6</asp:ListItem>
				<asp:ListItem Value="7">7</asp:ListItem>
				<asp:ListItem Value="8">8</asp:ListItem>
				<asp:ListItem Value="9">9</asp:ListItem>
				<asp:ListItem Value="10">10</asp:ListItem>
				<asp:ListItem Value="11">11</asp:ListItem>
				<asp:ListItem Value="12">12</asp:ListItem>
				<asp:ListItem Value="13">13</asp:ListItem>
				<asp:ListItem Value="14">14</asp:ListItem>
				<asp:ListItem Value="15">15</asp:ListItem>
				<asp:ListItem Value="16">16</asp:ListItem>
				<asp:ListItem Value="17">17</asp:ListItem>
				<asp:ListItem Value="18">18</asp:ListItem>
				<asp:ListItem Value="19">19</asp:ListItem>
				<asp:ListItem Value="20">20</asp:ListItem>
			</asp:dropdownlist></td>
		<td  height="35">الویت در ترتیب نمایش:</td>
	</tr>
	<tr>
		<td  style="WIDTH: 414px" width="414"><asp:button id="btnSave" 
                runat="server"  Font-Names="Tahoma" onclick="btnSave_Click"></asp:button>&nbsp;&nbsp;&nbsp;
			<asp:button id="btnCancel" runat="server"  
                Font-Names="Tahoma" Text="  انصراف  "
				CausesValidation="False" onclick="btnCancel_Click"></asp:button></td>
		<td  height="68"><IMG style="WIDTH: 126px; HEIGHT: 1px" height="1" src="blankImg.gif"
				width="126"></td>
	</tr>
	<tr>
		<td width="100%" colSpan="2" height="30">
			<P align="right">&nbsp;</P>
		</td>
	</tr>
</table>
</asp:Content>