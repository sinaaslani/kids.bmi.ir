<%@ Page Language="C#" Theme="Default" MasterPageFile="~/Masters/FA.master" AutoEventWireup="True"
  CodeBehind="ShowThnkEval.aspx.cs" Inherits="Site.Kids.bmi.ir.Poll.ShowThnkEval"
    Title="افکار سنجي" %>
    

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
<table width="100%" >
	<tr>
		<td  style="HEIGHT: 26px" align="right" width="100%" colSpan="2" >
			 <h1 >
				<asp:Literal ID="questionnaireFormLbl" Runat="server"></asp:Literal>
				</h1>
		</td>
	</tr>
	<tr>
		<td >
			<asp:Label ID="qFormDate" Runat="server" ></asp:Label>
			<br>
			<asp:Label ID="qFormIntroductionLbl" Runat="server" ></asp:Label>
           
            <div align="center">
                <asp:Label ID="lblMessage" runat="server" BackColor="White" 
                    Font-Bold="True" ForeColor="Red"></asp:Label>
                </div>
             </td>
	</tr>
	<tr>
		<td  style="HEIGHT: 26px" align="right" width="100%" colSpan="2" >
			 <img src="/App_Themes/Fa/images/t_bullet.gif" align="top"> سوالات 
		</td>
	</tr>
	<tr>
		<td  align="right">
            <asp:GridView ID="dgQuestions" runat="server" AutoGenerateColumns="False" BorderColor="#404040"
                BorderStyle="Solid" BorderWidth="0px" GridLines="Horizontal" OnRowDataBound="dgQuestions_RowDataBound"
                ShowHeader="False" Width="99%">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HiddenField ID="hdQuestionId" runat="server" />
                            <asp:Label ID="lblQuestion" runat="server"></asp:Label><asp:Panel ID="pnlAnswers"
                                runat="server">
                            </asp:Panel>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
			<asp:Label id="questionCountLbl" runat="server" Visible="False"></asp:Label>
		</td>
	</tr>
	<tr>
		<td  align="center">
			<asp:Button id="SendBtn" runat="server" Text="  ارسال فرم  " 
                 onclick="SendBtn_Click"></asp:Button>&nbsp;
			<asp:Button id="cancelBtn" runat="server" Text="  انصراف  " 
                 onclick="cancelBtn_Click"></asp:Button></td>
	</tr>
    <tr>
        <td align="center"  height="2">
        </td>
    </tr>
</table>

</asp:Content>
