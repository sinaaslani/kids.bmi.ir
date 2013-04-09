<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="ftb.imagegallery.aspx.cs"
    Inherits="Site.Kids.bmi.ir.AdminCP.WebForm1"  %>


<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<html>
<head>
    <title>Image Gallery</title>
</head>
<body>
    <form id="Form1" runat="server" enctype="multipart/form-data" dir="rtl" >
    <FTB:ImageGallery ID="ImageGallery1" CurrentImagesFolder="~/AdminCP/Files/"
        AllowImageDelete="false" AllowImageUpload="true" AllowDirectoryCreate="true"
        AllowDirectoryDelete="false" runat="Server" RootImagesFolder="~/AdminCP/Files/"
        UtilityImagesLocation="InternalResource"   />
    </form>
</body>
</html>
