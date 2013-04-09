<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.Master" ValidateRequest="false"
    AutoEventWireup="True" CodeBehind="AddEditPoll.aspx.cs" Inherits="Site.Kids.bmi.ir.AdminCP.PollsAdmin.AddEditPoll"
    Theme="Admin" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%">
        <tr>
            <td>
                <div runat="server" id="AddChapterSkin">
                    <table style="width: 100%;" height="145" width="100%" align="right">
                        <tr>
                            <td style="height: 26px" align="right" width="100%" colspan="2" >
                                <img src="/App_Themes/Admin/Images/admin_bullet.gif">&nbsp;
                                    <asp:Label ID="HeaderMsgLbl" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="right" width="50">
                                عنوان&nbsp;سوال :
                            </td>
                            <td  align="right">
                                <asp:TextBox ID="TitleCtrl" runat="server" Width="280px" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Css
                                    ControlToValidate="TitleCtrl" ErrorMessage="RequiredFieldValidator"> خطا : عنوان الزامي است</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                وضعیت :</td>
                            <td align="right">
                                <asp:CheckBox ID="chkIsActive" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                دارای امتیاز :
                            </td>
                            <td align="right">
                                <asp:CheckBox ID="chkHasScore" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 28px" height="28" align="right">
                                نمایش نتیجه نظر سنجی به کاربران :</td>
                            <td align="right">
                                <asp:CheckBox ID="chkBoxResultViewStatus" runat="server"></asp:CheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 17px" width="100%" height="17" colspan="2">
                                &nbsp;<asp:Button ID="AddBtn" runat="server"  Font-Names="Tahoma"
                                    Text="   اضافه   " OnClick="AddBtn_Click"></asp:Button>
                                <asp:Button ID="editDelBtn" runat="server"  Font-Names="Tahoma"
                                    OnClick="editDelBtn_Click"></asp:Button>
                                <asp:Button ID="cancelBtn" runat="server"  Text="  انصراف  "
                                    Font-Names="Tahoma" CausesValidation="False" OnClick="cancelBtn_Click"></asp:Button>
                            &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right" width="100%" colspan="2" height="30">
                                &nbsp;
                                <asp:HyperLink ID="EditresponseItems" runat="server" >ويرايش گزينه هاي سوال</asp:HyperLink>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
