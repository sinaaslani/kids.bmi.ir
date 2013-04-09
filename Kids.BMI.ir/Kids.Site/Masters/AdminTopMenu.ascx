<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="AdminTopMenu.ascx.cs"
    Inherits="Site.Kids.bmi.ir.Masters.AdminTopMenu" %>
<table width="100%">
    <tr>
        <td align="left" >
            <asp:HyperLink ID="AdminDefault" NavigateUrl="~/AdminCP/Default.aspx" runat="server"
                >مدیریت سیستم</asp:HyperLink>
            &nbsp; |&nbsp;
            <asp:LinkButton ID="logOffLink" runat="server"  CausesValidation="False"
                OnClick="logOffLink_Click">خروج از سايت</asp:LinkButton>
        </td>
        <td align="right" >
            &nbsp;<asp:Label ID="lblCurrentUser" runat="server" ForeColor="#3366FF"></asp:Label>
            &nbsp;
            <asp:Label ID="Label1" runat="server" Text=":کاربر جاری&nbsp;"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="left"  colspan="2">
            <hr />
        </td>
    </tr>
</table>
