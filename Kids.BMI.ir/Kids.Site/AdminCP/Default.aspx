<%@ Page Title="پنل مدیریت " Language="C#" MasterPageFile="~/Masters/Admin.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Site.Kids.bmi.ir.AdminCP._Default"
    Theme="Admin" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div align="center">
        <table width="930px" bgcolor="white" align="center">
            <tr>
                <td  width="400" align="left" height="10">
                </td>
                <td  width="550" height="10">
                    &nbsp;&nbsp;<asp:Label runat="server" ID="lblHeaderAdminPanel" CssClass="RedTitle"></asp:Label>
                </td>
            </tr>
            <tr>
                <td  colspan="2" align="center">
                    <asp:Table runat="server" ID="adminPanelTable" CssClass="TableBorder" Height="5"
                        Width="910px" BorderWidth="0">
                    </asp:Table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
