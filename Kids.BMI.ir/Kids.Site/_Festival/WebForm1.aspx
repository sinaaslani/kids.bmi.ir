<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Fa.Master" AutoEventWireup="true"
    CodeBehind="WebForm1.aspx.cs" Inherits="Site.Kids.bmi.ir.WebForm1" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.7.123, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Image runat="server" ID="Image1" />
    <asp:Label runat="server" ID="imageTitle"></asp:Label>
     <asp:Label runat="server" ID="imageDescription"></asp:Label>
    <ajaxToolkit:SlideShowExtender ID="SlideShowExtender1" runat="server" TargetControlID="Image1"
        SlideShowServiceMethod="GetSlides" AutoPlay="true" ImageTitleLabelID="imageTitle"
        ImageDescriptionLabelID="imageDescription" NextButtonID="nextButton" PlayButtonText="Play"
        StopButtonText="Stop" PreviousButtonID="prevButton" PlayButtonID="playButton"
        Loop="true" UseContextKey="True" />
        <asp:Button runat="server" ID="playButton" />
        <asp:Button runat="server" ID="nextButton" />
        <asp:Button runat="server" ID="prevButton" />
        <asp:Button runat="server" ID="Stop" />
        
        <br />
        <asp:TextBox ID="txtMovie" runat="server"></asp:TextBox> 
    <asp:AutoCompleteExtender ID="txtMovie_AutoCompleteExtender" runat="server" 
        DelimiterCharacters="" Enabled="True" ServiceMethod="GetCompletionList"  
        ServicePath="" TargetControlID="txtMovie" UseContextKey="True">
    </asp:AutoCompleteExtender>
</asp:Content>
