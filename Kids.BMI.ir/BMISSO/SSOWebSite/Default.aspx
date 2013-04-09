<%@ Page Theme="Default" Language="C#" AutoEventWireup="true" Inherits="SSOWebSite._Default"
    Codebehind="Default.aspx.cs" %>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<%@ Register Src="~/skins/FooterFA.ascx" TagName="FooterFA" TagPrefix="uc1" %>
<%@ Register Src="~/skins/HeaderFA.ascx" TagName="HeaderFA" TagPrefix="uc1" %>
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

    <script>
  window.history.forward();
    </script>

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
                                        <anthem:Panel ID="pnlMain" runat="server" EnabledDuringCallBack="false" AutoUpdateAfterCallBack="true"
                                            Width="100%">
                                            <table id="Table6" cellspacing="1" cellpadding="1" align="center" border="0" width="700">
                                                <tbody>
                                                    <tr>
                                                        <td style="height: 35px; text-align: center" colspan="3">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-right: 10px; height: 20px; text-align: right" colspan="3">
                                                            <a style="font-size:9pt;" href="<%=DefaultRedirect%>">وب سایت بانک ملی ایران</a>&nbsp;&nbsp;<img runat="server"
                                                                id="img2" src="App_Themes/Default/BULLET.gif" />
                                                                <br /><br />
                                                                </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                          <asp:Panel ID="pnlLoginComment" runat="server"></asp:Panel>
                                                        </td>
                                                        <td>&nbsp;&nbsp;&nbsp;</td>
                                                        <td style="height: 245px" valign="top">
                                                            <table id="Table1" height="212" cellspacing="0" cellpadding="2" width="147" align="center"
                                                                style="border-color: #b6c7e5; border-collapse: collapse;" border="2">
                                                                <tr>
                                                                    <td valign="top" width="313" height="208" bgcolor="#ffffff">
                                                                        <table id="Table3" cellspacing="6" cellpadding="6" width="100%" bgcolor="#ffffff"
                                                                            border="0">
                                                                            <tr bgcolor="#eeeeee">
                                                                                <td width="357" height="105" align="center">
                                                                                    ورود كاربر عضو
                                                                                    <br />
                                                                                    <br />
                                                                                    <span style="color: #0000ff"><asp:Literal ID="lblUserTypeTitle" runat="server" Text="مخصوص اعضاء خدمات اينترنتي بانک ملی"></asp:Literal><br />
                                                                                    </span>
                                                                                    <br />
                                                                                    &nbsp;شناسه كاربر و رمز عبور خود را وارد نماييد
                                                                                    <br />
                                                                                    <br />
                                                                                    <table id="Table4" cellspacing="0" cellpadding="4" width="29" border="0">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <table id="Table5" height="1" cellspacing="0" cellpadding="2" border="0">
                                                                                                    <tbody>
                                                                                                        <tr>
                                                                                                            <td align="right" width="154" height="1">
                                                                                                                <font face="Tahoma" color="#ff0000" size="2">*</font>
                                                                                                                <asp:TextBox ID="TxtUserID" TabIndex="1" MaxLength="15" AutoCompleteType="None" runat="server"
                                                                                                                    Width="130px"></asp:TextBox></td>
                                                                                                            <td width="95" height="5">
                                                                                                                <font face="Tahoma" size="2">شناسه كاربر</font>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td align="right">
                                                                                                                <span style="font-size: 10pt; color: #ff0000; font-family: Tahoma">*</span>
                                                                                                                <asp:TextBox ID="TxtPass" TabIndex="2" TextMode="password" MaxLength="15" runat="server"
                                                                                                                    Width="130px"></asp:TextBox></td>
                                                                                                            <td height="1">
                                                                                                                <font face="Tahoma" size="2">رمزعبور</font>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </tbody>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="center">
                                                                                                <hr width="100%" size="1">
                                                                                                <asp:Label ID="lblError" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="height: 24px" align="center">
                                                                                                <a id="lbtnForgetPWD" href="https://ebank.bmi.ir/mbsweb/sadadapps/SignUp/FrmForgetPass.aspx">
                                                                                                    &lt;&lt; رمز عبور را فراموش کرده ام &gt;&gt;</a></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <hr width="100%" size="1">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td nowrap width="274" bgcolor="#eeeeee">
                                                                                                <p align="center">
                                                                                                    <font face="Tahoma">
                                                                                                        <anthem:Button ID="Btn_Login" Style="width: 200px; font-family: Tahoma" TabIndex="3"
                                                                                                            Text="ورود" TextDuringCallBack="....در حال ورود به سیستم" runat="server" OnClick="Login_Click" />&nbsp;</font></p>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 4px" colspan="3">
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </anthem:Panel>
                                        &nbsp;</div>
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
        var wait=2000;
        function navigate(url)
        {    
            window.location.href=url;
        }
        <%=NavigationScript %>       
        </script>

    </form>
</body>
</html>
