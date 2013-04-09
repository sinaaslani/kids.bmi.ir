<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Fa.Master" AutoEventWireup="True"
    CodeBehind="DynamicPage.aspx.cs" Inherits="Site.Kids.bmi.ir.DynPage._DynamicPage"
    Theme="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table id="newsContentTable" border="0" style="width: 100%; height: 100%; border: 0px solid red;">
        <tr>
            <td valign="top" align="right" style="height: 40px">
                <img alt="" src="/App_Themes/Default/images/bullet.jpg" align="top" />
                <asp:Label ID="TitleLbl" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td valign="top" style="height: 90%" >
                <p  class="NormalTextSmall">
                    <asp:Literal ID="BodyLbl" runat="server" EnableViewState="False"></asp:Literal><br>
                </p>
            </td>
        </tr>
    </table>
</asp:Content>
