<%@ Page Title="شرکت در نظر سنجی" Language="C#" Theme="Default" MasterPageFile="~/Masters/Fa.Master" AutoEventWireup="true" CodeBehind="Poll.aspx.cs" Inherits="Site.Kids.bmi.ir.Poll.Poll" %>
<%@ Register src="showPoll.ascx" tagname="showPoll" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:showPoll ID="showPoll1" runat="server" />
</asp:Content>
