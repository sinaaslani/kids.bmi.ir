<%@ Page Title="سوالات متداول" Theme="Default" Language="C#" MasterPageFile="~/Masters/Fa.Master" AutoEventWireup="true" CodeBehind="ShowFAQ.aspx.cs" Inherits="Site.Kids.bmi.ir._FAQ.ShowFAQ" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <table id="newsContentTable" height="29" width="100%" border="0" name="newsContentTable"
	style="WIDTH:100%; HEIGHT:29px">
	<tr>
		<TD vAlign="bottom"></TD>
		<TD vAlign="bottom">
			
				<asp:label id="TitleLbl" runat="server"></asp:label>
		</TD>
		<TD vAlign="bottom" class="LangUnAlign"><asp:dropdownlist id="TopCats" 
                AutoPostBack="True" runat="server" 
                onselectedindexchanged="TopCats_SelectedIndexChanged"></asp:dropdownlist>
		</TD>
		<TD vAlign="bottom"></TD>
	</tr>
	<tr>
		<TD vAlign="bottom"></TD>
		<td vAlign="top" width="100%" colSpan="2" height="40" style="HEIGHT: 40px">
			<P align="justify"><BR>
				<asp:label id="BodyLbl" runat="server" ></asp:label><BR>
			</P>
			<P align="left"><asp:HyperLink NavigateUrl="~/FAQ.aspx" runat=server class="linkCadetBlue">برگشت</asp:HyperLink></P>
		</td>
		<TD vAlign="bottom"></TD>
	</tr>
</table>

</asp:Content>
