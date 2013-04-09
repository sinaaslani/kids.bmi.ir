<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefaultKids.aspx.cs" Inherits="Site.Kids.bmi.ir.DefaultKids" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>جزیره آرزو ها</title>
    <link rel="SHORTCUT ICON" href="<%=ResolveUrl("~/App_Themes/Default/images/Web/bmi.ico") %>" />
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AC_RunActiveContent.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.8.1.min.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/scripts/jquery.msg.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/scripts/jquery.center.min.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/scripts/msgbox.js") %>"></script>
    <meta name="author" content="بانک  ملی ایران" />
    <meta name="keywords" content=" کانون جوانه های بانک ملی ایران " />
    <meta name="description" content="سایت آموزش بانکداری کودکان بانک ملی ایران" />
    <meta name="description" content="حساب آرزو بانک ملی ایران" />
    <meta name="description" content="تسهیلات حساب آرزو بانک ملی ایران" />
    <meta name="description" content="بازی و سرگرمی بانک ملی ایران" />
</head>
<body style="padding: 0; margin: 0; background-color: #FFFFFF; background-image: url('/App_Themes/Default/Master/background.jpg');
    background-repeat: repeat-x; text-align: center">
    <script src="Scripts/AC_RunActiveContent.js" type="text/javascript"></script>
    <table align="center" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center">
                <table width="1018px" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="3" style="height: 125px;">
                        </td>
                        <td rowspan="2">
                            <img alt="" class="style7" src="App_Themes/Default/images/Up/up_03.png" />
                        </td>
                        <td colspan="3" style="height: 125px;">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a href="/contactus.aspx">
                                <img alt="" src="/App_Themes/Default/images/Up/up_06.png" border="0" /></a>
                        </td>
                        <td>
                            <a href="/Register.aspx">
                                <img alt="" src="/App_Themes/Default/images/Up/up_07.png" border="0" /></a>
                        </td>
                        <td>
                            <a href="/پیوندها.aspx">
                                <img alt="" src="/App_Themes/Default/images/Up/up_08.png" border="0" /></a>
                        </td>
                        <td>
                            <a href="/faq.aspx">
                                <img alt="" src="/App_Themes/Default/images/Up/up_09.png" border="0" /></a>
                        </td>
                        <td>
                            <a href="/Poll.aspx">
                                <img alt="" src="/App_Themes/Default/images/Up/up_10.png" border="0" /></a>
                        </td>
                        <td>
                            <a href="/درباره ما.aspx">
                                <img alt="" src="/App_Themes/Default/images/Up/up_11.png" border="0" /></a>
                        </td>
                    </tr>
                </table>
                <div id="divBanner" style="background-image: url('/App_Themes/Default/Master/divBackground-new.png');
                    background-repeat: no-repeat; background-position: center; padding-right: 5px;
                    padding-left: 2px; width: 1002px; height: 641px;" align="center">
                    <asp:Literal ID="lblSWF" runat="server"></asp:Literal>
                </div>
            </td>
        </tr>
    </table>
</body>
</html>
