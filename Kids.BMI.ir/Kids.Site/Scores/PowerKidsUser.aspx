<%@ Page Title="قوی ترین ساکنین جزیره" Theme="Default" Language="C#" MasterPageFile="~/Masters/Fa.Master"
    AutoEventWireup="true" CodeBehind="PowerKidsUser.aspx.cs" Inherits="Site.Kids.bmi.ir.Scores.PowerKidsUser" %>

<%@ Register Src="PowerKidsUserWidget.ascx" TagName="PowerKidsUserWidget" TagPrefix="uc1" %>
<%@ Register Src="../Registration/LastSiteUserWidget.ascx" TagName="LastSiteUserWidget"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table align="center">
        <tr>
            <td valign="top">
                <uc1:PowerKidsUserWidget ID="PowerKidsUserWidget1" runat="server" PageSize="20" />
            </td>
            <td valign="top">
                <uc2:LastSiteUserWidget ID="LastSiteUserWidget1" runat="server" PageSize="20" ShowPicture="False"  />
            </td>
        </tr>
    </table>
</asp:Content>
