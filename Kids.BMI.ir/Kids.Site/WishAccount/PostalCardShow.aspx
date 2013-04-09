<%@ Page Title="ارسال کارت پستال" Theme="Default" Language="C#" MasterPageFile="~/Masters/Fa.Master"
    AutoEventWireup="true" CodeBehind="PostalCardShow.aspx.cs" Inherits="Site.Kids.bmi.ir.WishAccount.PostalCardShow" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function colorChanged(sender) {
            sender.get_element().style.color =
       "#" + sender.get_selectedColor();
        }
    </script>
    <table runat="server" id="pnlMain" visible="false" style="width: 100%;">
        <tr>
            <td>
                <fieldset runat="server" id="pnlCardInfo">
                    <legend>تکمیل اطلاعات کارت پستال</legend>
                    <table width="100%">
                        <tr>
                            <td>
                                تصویر کارت پستال
                            </td>
                            <td align="right" colspan="3">
                                <asp:Image ID="imgPostalCardSmallPic" runat="server" 
                                    Width="278px" Height="170px" />
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 34px" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                نام دوست شما
                            </td>
                            <td align="right">
                                <asp:TextBox ID="txtFriendName" runat="server"></asp:TextBox>
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td align="right">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                آدرس ایمیل دوست شما
                            </td>
                            <td align="right">
                                <asp:TextBox ID="txtFriendEmailAddress" runat="server"></asp:TextBox>
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td align="right">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                متن روی کارت پستال
                            </td>
                            <td align="right" colspan="3">
                                <asp:TextBox ID="txtPostalCardText" runat="server" Height="67px" TextMode="MultiLine"
                                    Width="337px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="right">
                                <asp:CheckBox ID="chkSendTome" runat="server" Text="کارت پستال به خودم هم ارسال شود"
                                    AutoPostBack="True" OnCheckedChanged="SendTome_CheckedChanged" />
                                &nbsp;<asp:TextBox ID="txtMyEmailAddress" runat="server" Visible="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 32px" align="right">
                                فونت متن
                            </td>
                            <td align="right" style="height: 32px">
                                <asp:DropDownList ID="drpFontName" runat="server">
                                    <asp:ListItem Selected="True">-----</asp:ListItem>
                                    <asp:ListItem>B Zar</asp:ListItem>
                                    <asp:ListItem>B Nazanin</asp:ListItem>
                                    <asp:ListItem>Tahoma</asp:ListItem>
                                    <asp:ListItem> B Koodak</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left" style="height: 32px">
                                اندازه فونت متن
                            </td>
                            <td align="right" style="height: 32px">
                                <asp:TextBox ID="txtFontSize" runat="server" Width="60px"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="txtFontSize_FilteredTextBoxExtender" runat="server"
                                    Enabled="True" FilterType="Numbers" TargetControlID="txtFontSize">
                                </asp:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                رنگ متن
                            </td>
                            <td align="right" colspan="3">
                                <asp:TextBox ID="txtColor" runat="server" Width="60px"></asp:TextBox>
                                <asp:ColorPickerExtender ID="txtColor_ColorPickerExtender" runat="server" Enabled="True"
                                    TargetControlID="txtColor" OnClientColorSelectionChanged="colorChanged">
                                </asp:ColorPickerExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                فاصله از راست
                            </td>
                            <td align="right">
                                <asp:TextBox ID="txtRight" runat="server" Width="60px"></asp:TextBox> (پیکسل)
                                <asp:FilteredTextBoxExtender ID="txtRight_FilteredTextBoxExtender" runat="server"
                                    Enabled="True" FilterType="Numbers" TargetControlID="txtRight">
                                </asp:FilteredTextBoxExtender>
                            </td>
                            <td align="left">
                                فاصله از بالا
                            </td>
                            <td align="right">
                                <asp:TextBox ID="txtTop" runat="server" Width="60px"></asp:TextBox> (پیکسل)
                                <asp:FilteredTextBoxExtender ID="txtTop_FilteredTextBoxExtender" runat="server" Enabled="True"
                                    FilterType="Numbers" TargetControlID="txtTop">
                                </asp:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td align="right" colspan="3">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                &nbsp;
                                <asp:Button ID="btnPreview" runat="server" OnClick="btnPreview_Click" Text="مشاهده پیش نمایش" />
                                &nbsp;
                                <asp:Button ID="btnSend" runat="server" OnClick="btnSend_Click" Text="ارسال کارت پستال" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td runat="server" id="pnlPreview" visible="false">
                <fieldset>
                    <legend>پیش نمایش کارت پستال</legend>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Image ID="imgPreview" runat="server" Width="95%" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="لغو" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td runat="server" id="pnlSend" visible="false">
                <fieldset>
                    <legend>ارسال کارت پستال</legend>
                    <table style="width: 100%;">
                        <tr>
                            <td width="250" align="right">
                                &nbsp;مشخصات گیرنده کارت پستال :
                            </td>
                            <td align="right">
                                <asp:Label ID="lblFriendInfo" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="200" align="right">
                                امتیاز کسر شده بابت ارسال کارت پستال :</td>
                            <td align="right">
                                <asp:Label ID="lblPostalCardScore" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Image ID="imgSendPostalCard" runat="server" Width="30%" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnFinalSend" runat="server" OnClick="btnFinalSend_Click" Text="اطلاعات را تایید میکنم و میخواهم کارت پستال ارسال شود" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnCancelSend" runat="server" OnClick="btnCancelSend_Click" Text="لغو" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
