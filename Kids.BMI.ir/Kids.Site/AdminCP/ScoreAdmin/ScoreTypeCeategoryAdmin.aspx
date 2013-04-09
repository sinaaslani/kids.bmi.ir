<%@ Page Language="c#" AutoEventWireup="True" Theme="Admin" MasterPageFile="~/Masters/Admin.Master"
    CodeBehind="ScoreTypeCeategoryAdmin.aspx.cs" Inherits="Site.Kids.bmi.ir.AdminCP.ScoreAdmin.ScoreTypeCeategoryAdmin" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table dir="ltr">
        <tr>
            <td colspan="2" align="right">
                <img src="/App_Themes/Admin/Images/admin_bullet.gif" alt="">
                <asp:Literal ID="lblHead" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td width="415" align="right">
                <asp:TextBox ID="faCatName" runat="server"></asp:TextBox>
            </td>
            <td align="right">
                عنوان گروه&nbsp;:
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Button ID="btnSave" runat="server"  OnClick="btnSave_Click">
                </asp:Button>&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="انصراف"  OnClick="btnCancel_Click">
                </asp:Button>
            </td>
            <td align="right">
                <img style="width: 127px; height: 1px" height="1" src="/App_Themes/Admin/Images/blankImg.gif"
                    width="127" alt="">
            </td>
        </tr>
    </table>
</asp:Content>
