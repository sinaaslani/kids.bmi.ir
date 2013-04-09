<%@ Page Language="c#"  Theme="Default" AutoEventWireup="True" CodeBehind="ContactUs.aspx.cs" Inherits="Site.Kids.bmi.ir.Contact.ContactUs"
    MasterPageFile="~/Masters/Fa.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table border="0" width="100%" cellpadding="0" cellspacing="0" dir="ltr" style=" font-family:Mj_Two; font-size:13px; line-height: 28px;">
        <tr>
            <td align="right" colspan="3" >
               دوستان خوبم اگر برای شما مشكلی پيش آمد و
يا سوالی داشتيد اول قسمت از من بپرس را
خوب بخوانيد و سعی كنيد جواب سوالتان
راپيدا كنيد.
اگر پاسخ شما در آن جا نبود برای ما پيام
بفرستيد تا مشكل و يا سوال شما را بررسی و
پاسخ دهيم
               
            </td>
        </tr>
        <tr>
            <td align="right"  colspan="3" height="10">
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center">
                <table id="suggestTbl" style="width: 100%" cellspacing="2" cellpadding="2" border="0"
                    runat="server">
                    <tr>
                        <td colspan="2" style="width: 100%" align="right">
                            <p align="right" class="normalTextSmall">
                                پرکردن فيلدهای نام و نام خانوادگی ، تلفن و آدرس الزامی نبوده ولی در صورت تکميل ،
                                رسيدگی را سريعتر و راحت تر خواهد ساخت</p>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <font dir="ltr">
                                <asp:RegularExpressionValidator SetFocusOnError="True" ID="RegularExpressionValidator2"
                                    runat="server" CssClass="validationWarningSmall" ControlToValidate="fromAddressTxt"
                                    ErrorMessage="RegularExpressionValidator" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    Font-Size="8pt" Font-Names="Tahoma"> صحیح نمی باشد</asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="emailValidator" runat="server"
                                    CssClass="validationWarningSmall" ControlToValidate="fromAddressTxt" ErrorMessage="RequiredFieldValidator"
                                    Font-Size="8pt" Font-Names="Tahoma">الزامی</asp:RequiredFieldValidator>
                                <asp:TextBox CssClass="TextBox2" ID="fromAddressTxt" runat="server" 
                                Width="190px"></asp:TextBox>
                            </font>
                        </td>
                        <td align="right" >
                            ايميل فرستنده
                        </td>
                    </tr>
                    <tr >
                        <td align="right">
                            <asp:TextBox CssClass="TextBox2" ID="subjectTxt" runat="server" Width="190px"></asp:TextBox>
                        </td>
                        <td align="right">
                            موضوع
                        </td>
                    </tr>
                    <tr >
                        <td align="right">
                            <asp:TextBox CssClass="TextBox2" ID="txtFnameLname" runat="server" 
                                Width="190px"></asp:TextBox>
                        </td>
                        <td align="right">
                            نام و نام خانوادگی
                        </td>
                    </tr>
                    <tr >
                        <td align="right">
                            <asp:TextBox CssClass="TextBox2" ID="txtPhone" runat="server" Width="190px"></asp:TextBox>
                        </td>
                        <td align="right">
                            تلفن
                        </td>
                    </tr>
                    <tr >
                        <td align="right" >
                            <asp:TextBox CssClass="TextBox2" ID="txtAddress" runat="server" Width="190px" 
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td align="right">
                            آدرس
                        </td>
                    </tr>
                    <tr >
                        <td align="right" >
                            <asp:TextBox CssClass="TextBox2" ID="bodyTxt" runat="server" Width="400px" MaxLength="4000"
                                TextMode="MultiLine" Height="150px"></asp:TextBox>
                        </td>
                        <td align="right">
                            &nbsp;متن پيام
                        </td>
                    </tr>
                    <tr >
                        <td  align="right">
                            <p align="right">
                                &nbsp;<asp:Button ID="sendBtn" runat="server" CssClass="btn3" Text="   ارسال   "
                                    OnClick="sendBtn_Click" textduringcallback="...در حال ارسال" /></p>
                        </td>
                        <td >
                           </td>
                    </tr>
                    <tr >
                        <td  colspan="2">
                            <br>
                        </td>
                    </tr>
                </table>
                <asp:Label ID="statusMsgLbl" runat="server" CssClass="NormalTextSmall"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
