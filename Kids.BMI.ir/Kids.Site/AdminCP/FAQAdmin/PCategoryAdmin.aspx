<%@ Page Language="c#" AutoEventWireup="True" Theme="Admin" MasterPageFile="~/Masters/Admin.Master"
    CodeBehind="PCategoryAdmin.aspx.cs" Inherits="Site.Kids.bmi.ir.AdminCP.FAQAdmin.PCategoryAdmin"
     %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="border-collapse: collapse" width="520" height="186" dir="ltr">
        <tr>
            <td height="15" width="520" colspan="2"  align="right">
                <br>
                <img src="/App_Themes/Admin/Images/admin_bullet.gif">
                <asp:Literal ID="lblHead" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td height="31" width="415" >
                <asp:TextBox ID="faCatName" runat="server"></asp:TextBox>
            </td>
            <td height="31"  align="right">
                عنوان گروه&nbsp;:
            </td>
        </tr>
        <tr>
            <td height="27" width="415"  align="right">
                <asp:DropDownList ID="SortOrder" runat="server">
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
                    <asp:ListItem Value="28">28</asp:ListItem>
                    <asp:ListItem Value="29">29</asp:ListItem>
                    <asp:ListItem Value="20">20</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td height="27"  align="right">
                الویت نمایش&nbsp;:
            </td>
        </tr>
        <tr>
            <td height="64" width="415"  align="right">
                &nbsp;
                <asp:Button ID="btnSave" runat="server"  OnClick="btnSave_Click">
                </asp:Button>&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="انصراف"  OnClick="btnCancel_Click">
                </asp:Button>
            </td>
            <td height="64"  align="right">
                &nbsp;<img style="width: 127px; height: 1px" height="1" src="/App_Themes/Default/images/blankImg.gif" width="127" alt="">
            </td>
        </tr>
    </table>
</asp:Content>
