<%@ Page Title="ساکنین جدید جزیره" Theme="Default" Language="C#" MasterPageFile="~/Masters/Fa.Master" AutoEventWireup="true" CodeBehind="LastSiteUser.aspx.cs" Inherits="Site.Kids.bmi.ir.Registration.LastSiteUser" %>
<%@ Register src="LastSiteUserWidget.ascx" tagname="LastSiteUserWidget" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:LastSiteUserWidget ID="LastSiteUserWidget1" runat="server" 
        EnablePaging="True" PageSize="20" ShowPicture="True" ShowContinue="False" />
</asp:Content>
