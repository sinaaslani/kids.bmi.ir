<%@ Page Title="خبر" Language="C#" MasterPageFile="~/Masters/Fa.Master" AutoEventWireup="true"
    CodeBehind="ShowNews.aspx.cs" Inherits="Site.Kids.bmi.ir.InfoBox.ShowNews" Theme="Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script language="JavaScript">
        function printNewsContent() {
            window.print();

        }


    </script>
    <table id="newsContentTable"  width="100%" border="0" name="newsContentTable">
        <tr>
            <td valign="top" width="100%" colspan="2" height="3">
                <asp:Label runat="server" ID="TitleLbl" EnableViewState="False"></asp:Label>
            </td>
            <td valign="bottom" align="left">
                <asp:DropDownList ID="TopCatNews" runat="server" AutoPostBack="True" OnSelectedIndexChanged="TopCatNews_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td valign="top" width="100%" height="3">
            </td>
        </tr>
        <tr>
            <td background="line.gif" valign="top" width="100%" colspan="4" height="3">
            </td>
        </tr>
        <tr>
            <td valign="top" width="100%" colspan="3" height="66">
                <table width="100%">
                    <tr>
                        <td width="100%" valign="top">
                            <asp:Image ImageAlign="Left" ID="newsImage" runat="server" Width="260px" BorderWidth="4"
                                BorderColor="#efefef"></asp:Image>
                            <p id="p1" style="text-align: justify;">
                                <asp:Literal ID="summaryLbl" runat="server" EnableViewState="False"></asp:Literal>
                                <asp:Literal ID="BodyLbl" runat="server" EnableViewState="False"></asp:Literal>
                            </p>
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top" width="100%" height="66">
                <img style="width: 1px; height: 1px" height="1" src="App_Themes/Default/images/blankImg.gif"
                    width="1">
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
        <tr>
            <td valign="top"  width="100%" colspan="3" height="10">
                <br>
                <img src="../App_Themes/Default/images/printer.gif"><asp:HyperLink ID="lnkPrintNews"
                    runat="server" NavigateUrl="~/InfoBox/PrintNews.aspx?id=" 
                    Target="_blank">چاپ خبر</asp:HyperLink>
                <asp:Label ID="bodyFileDivLbl" runat="server" ForeColor="#B72121">|</asp:Label><asp:Image
                    ID="bodyFileImg" runat="server" ImageUrl="~/App_Themes/Default/images/download.gif"
                    Visible="False"></asp:Image><asp:HyperLink ID="bodyFileLnk" runat="server" 
                        Target="_blank">فايل متن خبر</asp:HyperLink>
                <asp:Label ID="mediaFileDivLbl" runat="server" ForeColor="#B72121">|</asp:Label><asp:HyperLink
                    ID="MediaFileLnk" runat="server"  Target="_blank">گزارش تصویری</asp:HyperLink>
                <asp:Label ID="realFileDivLbl" runat="server" ForeColor="#B72121">|</asp:Label><asp:HyperLink
                    ID="realFileLnk" runat="server"  Target="_blank">گزارش صوتی</asp:HyperLink>
                <asp:Label ID="Label1" runat="server" ForeColor="#B72121">|</asp:Label>
                <asp:HyperLink ID="lnkSendEmail" runat="server" NavigateUrl="javascript:sendNewsToemail()"
                    >ارسال به دیگران</asp:HyperLink>
                <br />
                <br />
            </td>
            <td valign="top" width="100%" height="10">
            </td>
        </tr>
    </table>
    <script language="javascript">
	<!--
        function sendNewsToemail() {
            var popWin = window.open('/InfoBox/NewsSendTomail.aspx?id=' + <%=NewsId %>, null, 'width=680,height=550,scrollbars=yes,status=yes,resizable=no,toolbar=no,menubar=no,location=no');
            popWin.focus();
        }
	//-->
    </script>
</asp:Content>
