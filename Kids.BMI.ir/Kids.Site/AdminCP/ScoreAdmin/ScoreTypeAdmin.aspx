<%@ Page Language="c#" Theme="Admin" AutoEventWireup="True"  MasterPageFile="~/Masters/Admin.Master" Codebehind="ScoreTypeAdmin.aspx.cs" Inherits="Site.Kids.bmi.ir.AdminCP.ScoreAdmin.ScoreTypeAdmin"  %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table height="60" width="529" style="WIDTH: 529px; HEIGHT: 60px" dir="ltr">
	<tr>
		<td  style="HEIGHT: 26px" align="right" width="100%" colspan="2" >&nbsp;
			<img src="/App_Themes/Admin/Images/admin_bullet.gif">
			
            <asp:Literal id="lblHead" runat="server" ></asp:Literal>
            
            </td>
	</tr>
	<tr>
		<td  style="WIDTH: 414px" width="414" ></td>
		<td  ></td>
	</tr>
	<tr>
		<td  style="WIDTH:100%" width="100%"  align="right">
            <asp:textbox id="txtScoreFAName" runat="server" Width="390px"></asp:textbox><br>
			<asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtScoreFAName"
				ErrorMessage="RequiredFieldValidator">خطا : عنوان  الزامی است</asp:requiredfieldvalidator></td>
		<td  valign="top" >عنوان 
            امتیاز:</td>
	</tr>
	<tr>
		<td  style="WIDTH:100%" width="100%"  align="right">
            <asp:textbox id="txtScoreEnName" runat="server" 
                Width="390px"></asp:textbox></td>
		<td  valign="top" >عنوان سیستمی 
            امتیاز:</td>
	</tr>
	<tr>
		<td style="WIDTH: 414px" width="414"   align="right">
			<asp:DropDownList id="drpScoreTypeCategoryId" runat="server"></asp:DropDownList></TD>
		<td valign="top"  >گروه 
            امتیاز:</TD>
	</TR>
	<tr>
		<td style="WIDTH: 414px" width="414"  align="right" >
			<asp:textbox id="txtCoefficentValue" runat="server"></asp:textbox></TD>
		<td valign="top"  >ضریب</TD>
	</TR>
	<tr>
		<td  style="WIDTH: 414px" align="right" width="414" height="38">
            <asp:textbox id="txtMaxPerDay" runat="server"></asp:textbox></td>
		<td  valign="top" height="38">ماکزیمم 
            در روز :</td>
	</tr>
	<tr>
		<td  style="WIDTH: 414px" align="right" width="414" height="38">
            <asp:textbox id="txtMaxPerMonth" runat="server"></asp:textbox></td>
		<td  valign="top" height="38">ماکزیمم 
            در ماه :</td>
	</tr>
	<tr>
		<td  style="WIDTH: 414px" width="414"><asp:button id="btnSave" 
                runat="server"  Font-Names="Tahoma" onclick="btnSave_Click"></asp:button>&nbsp;&nbsp;&nbsp;
			<asp:button id="btnCancel" runat="server"  
                Font-Names="Tahoma" Text="  انصراف  "
				CausesValidation="False" onclick="btnCancel_Click"></asp:button></td>
		<td  height="68"><img style="WIDTH: 126px; HEIGHT: 1px" height="1" src="/App_Themes/Admin/Images/blankImg.gif"
				width="126"></td>
	</tr>
	<tr>
		<td width="100%" colSpan="2" height="30">
			<P align="right">&nbsp;</P>
		</td>
	</tr>
</table>
</asp:Content>