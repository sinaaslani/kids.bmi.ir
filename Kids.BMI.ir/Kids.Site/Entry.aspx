<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Entry.aspx.cs" Inherits="Site.Kids.bmi.ir.Entry"
    Theme="Default" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>کانون جوانه های بانک ملی ایران</title>
</head>
<head>
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
<body style="background-color: #e3e3e3; padding: 0px; margin: 0px;">
    <form id="form1" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0" align="center">
        <tr>
            <td align="center" valign="top">
                <table width="835px" border="0" cellpadding="0" cellspacing="0" align="center" style="height: 731px;">
                    <tr>
                        <td colspan="5" style="background-image: url('/App_Themes/Default/images/Entry/entry_01.jpg');
                            background-repeat: no-repeat; width: 835px; height: 64px; font-family: tahoma;
                            font-size: 11px; color: black; text-align: right; padding-top: 17px; padding-right: 68px;">
                            <a href="http://bmi.ir" style="color: black" target="_blank">سایت بانک ملی ایران</a>
                            | <a href="http://سخنی%20با%20والدین.aspx" style="color: black">سخنی با والدین</a>
                            | <a href="http://سوالات%20متداول.aspx" style="color: black">سوالات متداول</a> |
                            <a href="http://تماس%20با%20ما.aspx" style="color: black">تماس با ما</a>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="background-image: url('/App_Themes/Default/images/Entry/entry_02.jpg');
                            background-repeat: no-repeat; width: 835px; height: 253px;">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img src="/App_Themes/Default/images/Entry/entry_03.jpg" width="147" height="205"
                                alt="" border="0" />
                        </td>
                        <td>
                            
                                <asp:ImageButton ImageUrl="/App_Themes/Default/images/Entry/entry_04.jpg"  
                                    width="240" height="205"
                                    alt="" border="0" runat=server ID="btnKidsIsland" 
                                    onclick="btnKidsIsland_Click" />
                        </td>
                        <td>
                            <img src="/App_Themes/Default/images/Entry/entry_05.jpg" width="29" height="205"
                                alt="" border="0" />
                        </td>
                        <td>
                            <asp:ImageButton src="/App_Themes/Default/images/Entry/entry_06.jpg" Width="242"
                                Height="205" alt="" border="0" runat="server" ID="btnJavanan" OnClick="btnJavanan_Click" />
                        </td>
                        <td>
                            <img src="/App_Themes/Default/images/Entry/entry_07.jpg" width="177" height="205"
                                alt="" border="0" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="background-image: url('/App_Themes/Default/images/Entry/entry_08.jpg');
                            background-repeat: no-repeat; width: 835px; height: 29px;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="background-image: url('/App_Themes/Default/images/Entry/entry_09.jpg');
                            background-repeat: no-repeat; width: 835px; height: 144px; text-align: center;
                            padding-top: 3px;" align="center">
                            <img alt="" src="/App_Themes/Default/images/Entry/ads.jpg" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="background-image: url('/App_Themes/Default/images/Entry/entry_10.jpg');
                            background-repeat: no-repeat; width: 835px; height: 46px; font-family: tahoma;
                            font-size: 11px; color: #6b6b6b; text-align: left; padding-bottom: 18px; padding-left: 30px;">
                            Copyright © 1998-2013 Bank Melli Iran, Inc. All rights reserved.
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
