<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="PrintNews.aspx.cs"
    Inherits="Site.Kids.bmi.ir.InfoBox.PrintNews" Theme="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>چاپ خبر - وب سایت کودکان بانک ملی ایران</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <table width="600" align="center">
            <tr>
                <td dir="rtl">
                    <table id="newsContentTable" height="193" width="100%" border="0" name="newsContentTable">
                        <tr>
                            <td valign="top" width="100%" colspan="2" height="3">
                                <asp:Label runat="server" ID="TitleLbl" EnableViewState="False"></asp:Label>
                            </td>
                            <td valign="bottom" align="left">
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" width="100%" height="3">
                </td>
            </tr>
            <tr>
                <td valign="top" width="100%" colspan="4" height="3">
                </td>
            </tr>
            <tr>
                <td valign="top" width="100%" colspan="3" height="66">
                    <table width="100%">
                        <tr>
                            <td width="100%" valign="top">
                                <p id="p1" align="justify">
                                    <asp:Image ImageAlign="Left" ID="newsImage" runat="server" Width="220px" BorderWidth="3px"
                                        BorderColor="#ECF1FF"></asp:Image><asp:Label ID="summaryLbl" runat="server" 
                                            EnableViewState="False"></asp:Label>
                                    <asp:Label ID="BodyLbl" runat="server"  EnableViewState="False"></asp:Label>
                                </p>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" width="100%" height="66">
                    <img style="width: 1px; height: 1px" height="1" src="images/blankImg.gif" width="1">
                </td>
            </tr>
            <tr>
                <td valign="top" width="100%" colspan="3" height="66">
                    <p align="justify">
                        &nbsp;</p>
                </td>
                <td valign="top" width="100%" height="66">
                </td>
            </tr>
        </table>
        <script language="javascript">
	<!--
            function sendNewsToemail() {
                var popWin = window.open('NewsSendTomail.aspx?nurl=' + window.location, null, 'width=600,height=400,scrollbars=yes,status=yes,resizable=no,toolbar=no,menubar=no,location=no');
                popWin.focus();
            }
	//-->
        </script>
        <br />
        <br />
        <hr />
        تمامی حقوق این سايت متعلق به بانک ملی ایران می باشد. <
        <script language="javascript">
            window.print();
        </script>
    </div>
    </form>
</body>
</html>
