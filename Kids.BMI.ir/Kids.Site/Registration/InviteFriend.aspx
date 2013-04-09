<%@ Page Title="دعوت از دوستان" Language="C#" MasterPageFile="~/Masters/Fa.Master" AutoEventWireup="true"
    Theme="Default" CodeBehind="InviteFriend.aspx.cs" Inherits="Site.Kids.bmi.ir.Registration.InviteFriend" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%;" runat="server" id="pnlMain">
        <tr>
            <td width="100">
                نام ونام خانوادگی :
            </td>
            <td align="right">
                <asp:TextBox ID="txtFriendName" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                 &nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFriendName"
                    Display="Dynamic" ErrorMessage="!" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                آدرس ایمیل :
            </td>
            <td align="right">
                <asp:TextBox ID="txtFriendEmailAddress" runat="server" 
                    AutoCompleteType="Disabled"></asp:TextBox>
                &nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFriendEmailAddress"
                    Display="Dynamic" ErrorMessage="!" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFriendEmailAddress"
                    ErrorMessage="آدرس ایمیل نامعتبراست" ForeColor="Red" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
                شماره همراه :
            </td>
            <td align="right">
                <asp:TextBox ID="txtFriendMobileNumber" runat="server" 
                    AutoCompleteType="Disabled"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFriendMobileNumber"
                    Display="Dynamic" ErrorMessage="!" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnInviteFriend" runat="server" OnClick="btnInviteFriend_Click" Text="ارسال دعوتنامه" />
            </td>
        </tr>
    </table>
</asp:Content>
