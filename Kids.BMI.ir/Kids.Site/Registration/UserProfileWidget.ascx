<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserProfileWidget.ascx.cs"
    Inherits="Site.Kids.bmi.ir.Registration.UserProfileWidget" %>
<%@ Register Src="KidsUserSateWidget.ascx" TagName="KidsUserSateWidget" TagPrefix="uc1" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.7.123, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<script type="text/javascript">
   
    function uploadComplete(sender, args) {
        try {
        var fileExtension = args.get_fileName();
            
            if (<%=SupportedFileTypesScript %>) {
                ShowJQMessageBox('<fieldset style="direction:rtl;width:400px;max-width:600px;min-width: 300px;" align="center"><legend><img  align="middle" src="/App_Themes/MessageBox/warning.png" Width="32" />خطا در ارسال فایل </legend>  <span style="direction:rtl;text-align:right">فرمت فایل ارسالی نا معتبر است</span></fieldset>');
                return;
            }

            
            if (parseInt(args.get_length()) > <%=SupportedFileSize %>) {
                ShowJQMessageBox('<fieldset style="direction:rtl;width:400px;max-width:600px;min-width: 300px;" align="center"><legend><img  align="middle" src="/App_Themes/MessageBox/warning.png" Width="32" />خطا در ارسال فایل </legend>  <span style="direction:rtl;text-align:right">سایز فایل ارسالی نا معتبر است</span></fieldset>');
                return;
            }
            
            document.getElementById('ImgChildPic').src = '/JpegImage.aspx?act=300&d=' + new Date().toString();
            document.getElementById('ImgChildIdentityPic').src = '/JpegImage.aspx?act=301&d=' + new Date().toString();
            document.getElementById('ImgChildNationalCardFaceUp').src = '/JpegImage.aspx?act=302&d=' + new Date().toString();
            document.getElementById('ImgChildNationalCardFaceDown').src = '/JpegImage.aspx?act=303&d=' + new Date().toString();
            
            ShowJQMessageBox('<fieldset style="direction:rtl;width:400px;max-width:600px;min-width: 300px;" align="center"><legend><img  align="middle" src="/App_Themes/MessageBox/Information.png" Width="32" /> ارسال فایل </legend>  <span style="direction:rtl;text-align:right">فایل با موفقیت بارگذاری شد</span></fieldset>');
             
        }
        catch (e) {
            alert(e.message);
        }
    }

    function uploaderror(sender, args) {

        alert(args.get_errorMessage());

    }
</script>
<table width="100%">
    <tr>
        <td>
            <fieldset>
                <legend>اطلاعات کودک</legend>
                <table style="width: 100%;">
                    <tr>
                        <td width="150" align="right">
                            نام کاربری کودک:
                        </td>
                        <td align="right">
                            <asp:Label ID="lblSSOUserName" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                        <td align="left" colspan="2">
                            نام معرف:
                        </td>
                        <td align="right">
                            <asp:Label ID="lblIntroducer" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            وضعیت عضویت :
                        </td>
                        <td colspan="4" align="right">
                            <uc1:KidsUserSateWidget ID="ucKidsUserSateWidget" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="5">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            شماره حساب آرزو :
                        </td>
                        <td>
                            <asp:Label ID="lblWishAccountnumber" runat="server"></asp:Label>
                        </td>
                        <td>
                            مبلغ مانده حساب آرزو :
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblWishAccountRemain" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            کد شعبه افتتاح حساب :
                        </td>
                        <td align="left">
                            <asp:Label ID="lblWishAccountBranch" runat="server"></asp:Label>
                        </td>
                        <td width="120" align="left" colspan="2">
                            &nbsp;
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="5">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            نام کودک:
                        </td>
                        <td align="right">
                            <asp:Label ID="lblChildName" runat="server"></asp:Label>
                        </td>
                        <td align="right" colspan="2" width="50">
                            نام خانوادگی کودک :
                        </td>
                        <td align="right">
                            <asp:Label ID="lblChildFamily" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            جنسیت :
                        </td>
                        <td align="right">
                            <asp:Label ID="lblSex" runat="server"></asp:Label>
                        </td>
                        <td colspan="2">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            نام پدر :
                        </td>
                        <td align="right">
                            <asp:Label ID="lblChildFatherName" runat="server"></asp:Label>
                            &nbsp;
                        </td>
                        <td align="right" colspan="2">
                            محل تولد :
                        </td>
                        <td align="right">
                            <asp:Label ID="lblChildBirthLocation" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            شماره شناسنامه :&nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblChildIdentityNo" runat="server"></asp:Label>
                            &nbsp;
                        </td>
                        <td align="right" colspan="2">
                            کدملی :
                        </td>
                        <td align="right">
                            <asp:Label ID="lblChildMelliCode" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            سریال شناسنامه :
                        </td>
                        <td colspan="4" align="right">
                            <asp:Label ID="lblChildIdentitySerial" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            تاریخ تولد :
                        </td>
                        <td colspan="4" align="right">
                            <asp:Label ID="lblBirthDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            شماره تماس کودک :
                        </td>
                        <td align="right">
                            <asp:Label ID="lblChildPhoneNumber" runat="server" TextMode="Number"></asp:Label>
                        </td>
                        <td align="left" colspan="2">
                            شماره تلفن همراه کودک :
                        </td>
                        <td align="right">
                            <asp:Label ID="lblChildMobileNumber" runat="server" TextMode="Phone"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            آدرس ایمیل کودک(معتبر)
                        </td>
                        <td align="right">
                            <asp:Label ID="lblChildEmailAddress" runat="server" TextMode="Email"></asp:Label>
                            &nbsp;
                        </td>
                        <td colspan="2">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            کد پستی کودک :
                        </td>
                        <td align="right">
                            <asp:Label ID="lblChildPostCode" runat="server" TextMode="Number"></asp:Label>
                            &nbsp;
                        </td>
                        <td colspan="2">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            آدرس پستی محل سکونت کودک :
                        </td>
                        <td colspan="4" align="right">
                            <asp:Label ID="lblChildAddress" runat="server" TextMode="MultiLine"></asp:Label>
                        </td>
                    </tr>
                    <tr >
                        <td colspan="5">
                            <table>
                                <tr>
                                    <td align="right">
                                        تصویر کودک :
                                    </td>
                                    <td colspan="2" align="right">
                                        <asp:AsyncFileUpload UploaderStyle="Modern" ID="fupChildPic" runat="server" OnUploadedComplete="AsyncFileUpload_UploadedComplete"
                                            OnUploadedFileError="AsyncFileUpload_UploadedFileError" OnClientUploadComplete="uploadComplete"
                                            OnClientUploadError="uploaderror" UploadingBackColor="#66CCFF" ThrobberID="Throbber1" />
                                        (حداکثر 500 کیلو بایت)
                                        <asp:Label ID="Throbber1" runat="server" Style="display: none">
<img src="/App_Themes/ajax-loader.gif" align="absmiddle" alt="loading" />
                                        </asp:Label>
                                    </td>
                                    <td colspan="2" align="right">
                                        <asp:Image ID="ImgChildPic" runat="server" Height="48px" ImageUrl="~/JpegImage.aspx?act=300"
                                            Width="48px" ImageAlign="Middle" ClientIDMode="Static" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        تصویر صفحه اول شناسنامه :
                                    </td>
                                    <td colspan="2" align="right">
                                        <asp:AsyncFileUpload UploaderStyle="Modern" ID="fupChildIdentityPic" runat="server"
                                            OnUploadedComplete="AsyncFileUpload_UploadedComplete" OnUploadedFileError="AsyncFileUpload_UploadedFileError"
                                            OnClientUploadComplete="uploadComplete" OnClientUploadError="uploaderror" UploadingBackColor="#66CCFF"
                                            ThrobberID="Throbber2" />
                                        (حداکثر 500 کیلو بایت)
                                        <asp:Label ID="Throbber2" runat="server" Style="display: none">
<img src="/App_Themes/ajax-loader.gif" align="absmiddle" alt="loading" />
                                        </asp:Label>
                                    </td>
                                    <td colspan="2" align="right">
                                        <asp:Image ID="ImgChildIdentityPic" runat="server" Height="48px" ImageUrl="~/JpegImage.aspx?act=301"
                                            Width="48px" ImageAlign="Middle" ClientIDMode="Static" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        تصویر روی کارت ملی :
                                    </td>
                                    <td colspan="2" align="right">
                                        <asp:AsyncFileUpload UploaderStyle="Modern" ID="fupChildNationalCardFaceUp" runat="server"
                                            OnUploadedComplete="AsyncFileUpload_UploadedComplete" OnUploadedFileError="AsyncFileUpload_UploadedFileError"
                                            OnClientUploadComplete="uploadComplete" OnClientUploadError="uploaderror" UploadingBackColor="#66CCFF"
                                            ThrobberID="Throbber3" />
                                        (حداکثر 500 کیلو بایت)
                                        <asp:Label ID="Throbber3" runat="server" Style="display: none">
<img src="/App_Themes/ajax-loader.gif" align="absmiddle" alt="loading" />
                                        </asp:Label>
                                    </td>
                                    <td colspan="2" align="right">
                                        <asp:Image ID="ImgChildNationalCardFaceUp" runat="server" Height="48px" ImageUrl="~/JpegImage.aspx?act=302"
                                            Width="48px" ImageAlign="Middle" ClientIDMode="Static" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        تصویر پشت کارت ملی :
                                    </td>
                                    <td colspan="2" align="right">
                                        <asp:AsyncFileUpload UploaderStyle="Modern" ID="fupChildNationalCardFaceDown" runat="server"
                                            OnUploadedComplete="AsyncFileUpload_UploadedComplete" OnUploadedFileError="AsyncFileUpload_UploadedFileError"
                                            OnClientUploadComplete="uploadComplete" OnClientUploadError="uploaderror" UploadingBackColor="#66CCFF"
                                            ThrobberID="Throbber4" />
                                        (حداکثر 500 کیلو بایت)
                                        <asp:Label ID="Throbber4" runat="server" Style="display: none">
<img src="/App_Themes/ajax-loader.gif" align="absmiddle" alt="loading" />
                                        </asp:Label>
                                    </td>
                                    <td colspan="2" align="right">
                                        <asp:Image ID="ImgChildNationalCardFaceDown" runat="server" Height="48px" ImageUrl="~/JpegImage.aspx?act=303"
                                            Width="48px" ImageAlign="Middle" ClientIDMode="Static" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </td>
    </tr>
    <tr>
        <td>
            <fieldset>
                <legend>اطلاعات ولی کودک</legend>
                <table style="width: 100%;">
                    <tr>
                        <td align="right" width="150">
                            نام :
                        </td>
                        <td align="right">
                            <asp:Label ID="lblParentName" runat="server"></asp:Label>
                        </td>
                        <td align="right" width="110">
                            نام خانوادگی :
                        </td>
                        <td align="right">
                            <asp:Label ID="lblParentFamily" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            نسبت با کودک :
                        </td>
                        <td align="right">
                            <asp:Label ID="lblParentRelationId" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            کد ملی :
                        </td>
                        <td align="right">
                            <asp:Label ID="lblParentMelliCode" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            شماره شناسنامه :
                        </td>
                        <td align="right">
                            <asp:Label ID="lblParentIdentityNo" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            شماره حساب :
                        </td>
                        <td align="right">
                            <asp:Label ID="lblParentAccNo" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            آدرس ایمیل&nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblParentEmailAddress" runat="server" TextMode="Email"></asp:Label>
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            شماره تماس :
                        </td>
                        <td align="right">
                            <asp:Label ID="lblParentPhoneNumber" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            &nbsp; شماره تلفن همراه :
                        </td>
                        <td align="right">
                            <asp:Label ID="lblParentMobileNumber" runat="server"></asp:Label>
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            کد پستی :
                        </td>
                        <td align="right">
                            <asp:Label ID="lblParentPostCode" runat="server" TextMode="Number"></asp:Label>
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            آدرس پستی محل سکونت :
                        </td>
                        <td colspan="3" align="right">
                            <asp:Label ID="lblParentAddress" runat="server" TextMode="MultiLine"></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </td>
    </tr>
</table>
