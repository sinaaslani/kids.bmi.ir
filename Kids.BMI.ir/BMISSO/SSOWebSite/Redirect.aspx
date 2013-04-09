<%@ Page Theme="Default" Language="C#" AutoEventWireup="true" Inherits="SSOWebSite._Redirect"
    Codebehind="Redirect.aspx.cs" %>
<%@ Register Src="~/skins/FooterFA.ascx" TagName="FooterFA" TagPrefix="uc1" %>
<%@ Register Src="~/skins/HeaderFA.ascx" TagName="HeaderFA" TagPrefix="uc1" %>
<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ورود به بخش خدمات اينترنتي بانک ملي</title>
    <link href="App_Themes/Fa/Fa.css" type="text/css" rel="stylesheet" />
    <!--<link href="App_Themes/Fa/ClickShowHideMenu.css" type="text/css" rel="stylesheet" />-->
    <!--<link href="App_Themes/Fa/menu.css" type="text/css" rel="stylesheet" />-->
    <!--<link href="App_Themes/Fa/style.css" type="text/css" rel="stylesheet" />-->
    <!--<script type="text/javascript" src="http://www.bm,i.ir/scripts/Util.js"></script>-->
    
    <link id="SHORTCUT" rel="SHORTCUT ICON" href="App_Themes/Fa/Images/SiteIcon/BMI.ico" />
    <script src="Util.js" type="text/javascript"></script>

</head>
<body style="margin: 0px 2px 0px   2px; background-image: url(App_Themes/Fa/Images/bodybg.gif)"
    dir="ltr">
    <form id="form1" runat="server">
       
        <div align="center">
            <table cellpadding="0" cellspacing="0" style="width: 986px; height: 100%;">
                <tr>
                    <td rowspan="1" valign="top" style="width: 8px; background-image: url('App_Themes/Fa/Images/bodybg-Left.gif');
                        background-repeat: repeat-y">
                    </td>
                    <td valign="top" align="center">
                        <table style="width: 970px; height: 100%; background-color: White;" cellpadding="0"
                            cellspacing="0">
                            <tr>
                                <td valign="top" style="height: 77px">
                                    <uc1:HeaderFA ID="HeaderFA1" runat="server" />
                                </td>
                            </tr>
                            <tr style="width: 100%; height: 100%">
                                <td align="center">
       
       
       
       
            <div style="left: 50px; vertical-align: super; line-height: 20pt; letter-spacing: normal;
                text-align: right">
                <br />
                &nbsp;<br />
                <anthem:Panel Direction="RightToLeft" ID="pnlResult" Width="100%" HorizontalAlign="center" runat="server" AutoUpdateAfterCallBack="true">
                    <asp:Label ID="lblMessage" runat="server">شما با موفقیت به سیستم وارد شدید</asp:Label>
                    <br />
                    <img align="top" src="App_Themes/Default/indicator_medium.gif">
                    در حال بارگذاری...
                </anthem:Panel>
                <br />
               
                &nbsp;</div>
                 <p dir="rtl">
                <span class="normalTextsmall">  در صورتيكه صفحه مورد نظر پس از چند ثانيه به صورت اتوماتيك&nbsp; بارگذاري
                نشد&nbsp;
                <asp:HyperLink runat="server" ID="lnkRedirectUrl" CssClass="LinkCadetBlue" Text="اينجا كليك كنيد"></asp:HyperLink>
                </span>
                </p>
       
       
        </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <uc1:FooterFA ID="FooterFA" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td rowspan="1" valign="top" style="width: 8px; background-image: url('App_Themes/Fa/Images/bodybg-Right.gif');
                        background-repeat: repeat-y">
                    </td>
                </tr>
            </table>
        </div>
       
        <script>
        var wait=4000;
        function navigate(url)
        {    
            window.location.href=url;
        }
        <%=NavigationScript %>       
        </script>

    </form>
</body>
</html>
