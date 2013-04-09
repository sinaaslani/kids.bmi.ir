<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="NewsSendToMail.aspx.cs"
    Inherits="Site.Kids.bmi.ir.InfoBox.NewsSendToMail" Theme="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ارسال به دیگران</title>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <table width="600" align="center">
            <tr>
                <td dir="rtl">
                    <div id="ExternalDiv" align="center" runat="server">
                        <table id="table1" style="width: 100%;" cellspacing="0" cellpadding="0" border="0"
                            dir="ltr">
                            <tr>
                                <td style="width: 100%" align="right" width="100%" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="100%" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td dir="rtl" style="width: 100%" align="right" width="100%" background="" colspan="2">
                                   ارسال خبر به ايميل 
                                </td>
                            </tr>
                            <tr>
                                <td dir="rtl" style="width: 100%; height: 3px" align="right" background="/images/line.gif"
                                    colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 20px" align="right" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td dir="rtl" align="right">
                                    <font dir="ltr">&nbsp;<asp:TextBox CssClass="TextBox" ID="txtFromAddress" runat="server"></asp:TextBox>&nbsp;</font>
                                    <asp:RegularExpressionValidator SetFocusOnError="True" ID="RegularExpressionValidator2"
                                        runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="txtFromAddress"
                                        ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="validationWarningSmall">آدرس ايميل معتبري را وارد نماييد</asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="emailValidator" runat="server"
                                        ErrorMessage="RequiredFieldValidator" ControlToValidate="txtFromAddress" CssClass="validationWarningSmall">&#1570;&#1583;&#1585;&#1587; &#1575;&#1610;&#1605;&#1610;&#1604; &#1575;&#1604;&#1586;&#1575;&#1605;&#1740; &#1575;&#1587;&#1578;</asp:RequiredFieldValidator>
                                </td>
                                <td dir="rtl" style="width: 115px; height: 29px" valign="top" align="right" width="115">
                                    آدرس فرستنده خبر:
                                </td>
                            </tr>
                            <tr>
                                <td dir="rtl" align="right">
                                    <font dir="ltr">
                                        <asp:TextBox CssClass="TextBox" ID="txtToAddress" runat="server"></asp:TextBox>&nbsp;</font>
                                    <asp:RegularExpressionValidator SetFocusOnError="True" ID="Regularexpressionvalidator1"
                                        runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="txtToAddress"
                                        ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="validationWarningSmall">آدرس ايميل معتبري را وارد نماييد</asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="Requiredfieldvalidator1" runat="server"
                                        ErrorMessage="RequiredFieldValidator" ControlToValidate="txtToAddress" Font-Names="Tahoma"
                                        CssClass="validationWarningSmall">&#1570;&#1583;&#1585;&#1587; &#1575;&#1610;&#1605;&#1610;&#1604; &#1575;&#1604;&#1586;&#1575;&#1605;&#1740; &#1575;&#1587;&#1578;</asp:RequiredFieldValidator>
                                </td>
                                <td dir="rtl" style="width: 115px; height: 24px" valign="top" align="right" width="115">
                                    آدرس گيرنده خبر:
                                </td>
                            </tr>
                            <tr>
                                <td dir="ltr" align="right">
                                    <asp:TextBox CssClass="TextBox" ID="txtSubject" runat="server"></asp:TextBox>&nbsp;
                                </td>
                                <td dir="rtl" style="width: 115px; height: 9px" align="right" width="115">
                                    موضوع:
                                </td>
                            </tr>
                            <tr>
                                <td dir="rtl" align="right">
                                    <asp:TextBox CssClass="TextBoxBIg" ID="txtBody" runat="server" Height="154px" Width="300px"
                                        TextMode="MultiLine" MaxLength="256" Style="margin-left: 107px"></asp:TextBox>&nbsp;
                                </td>
                                <td dir="rtl" style="width: 115px; height: 45px" align="right" width="115">
                                    پيام:
                                </td>
                            </tr>
                            <tr>
                                <td dir="rtl" style="width: 100%; height: 25px" align="right">
                                    &nbsp;<asp:Button ID="btnSend" runat="server" Text="   ارسال   " 
                                        OnClick="btnSend_Click"></asp:Button>
                                </td>
                                <td dir="rtl" style="width: 115px; height: 25px" align="right" width="115">
                                    <img style="width: 114px; height: 2px" height="1" src="/images/blnkimg.gif" width="114">
                                </td>
                            </tr>
                            <tr>
                                <td dir="rtl" style="width: 100%" align="right" width="100%" colspan="2">
                                    <br>
                                    <font face="Tahoma" color="dimgray" size="1">اطلاعاتي که در اين صفحه وارد مي کنيد براي
                                        ارسال نامه هاي الکترونيکي استفاده نشده و به افراد ثالث هم داده نمي شود. </font>
                                    <br>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div align="center">
                        <asp:Label ID="newLinkAddress" runat="server" Visible="False"></asp:Label></div>
                    <br />
                    <br />
                    <hr />
                    تمامی حقوق این سايت متعلق به بانک ملی ایران می باشد.
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
