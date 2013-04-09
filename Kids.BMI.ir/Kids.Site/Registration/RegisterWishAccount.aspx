<%@ Page Title="معرفی حساب آرزو" Language="C#" MasterPageFile="~/Masters/Fa.Master"
    Theme="Default" AutoEventWireup="true" CodeBehind="RegisterWishAccount.aspx.cs"
    Inherits="Site.Kids.bmi.ir.Registration.RegisterWishAccount" %>

<%@ Register Src="../Payment/PaymentFactorWidget.ascx" TagName="PaymentFactorWidget"
    TagPrefix="uc1" %>
<%@ Register TagPrefix="uc1" TagName="KidsUserSateWidget" Src="~/Registration/KidsUserSateWidget.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function OnKeyup(obj) {

            if (obj.value.length >= 4)
                obj.blur();
            return true;
        }
    </script>
    <table style="width: 100%;" runat="server" visible="true" id="pnlMain">
        <tr runat="server" id="pnlWaiteForAccCreation" visible="false">
            <td align="center">
                <table style="width: 100%;">
                    <tr>
                        <span style="text-align: justify">کاربر گرامی اطلاعات شمادر حال حاضر در سیستم ثبت گردیده
                            است و در خواست افتتاح حساب شما در حال انجام میباشد.<br />
                            &nbsp;به محض اتمام عملیات سیستمی افتتاح حساب، شما میتوانید با مراجعه به بخش اطلاعات
                            کاربری از اطلاعات حساب خود آگاه گردید.
                            <br />
                            همچنین پس از آن قادر خواهید بود تا با مراجعه به شعب بانک ملی نسبت به فعالسازی امکان
                            برداشت ازحساب خود و دریافت کارت عابر بانک اقدام نمایید
                            <br />
                        </span>
                    </tr>
                    <tr>
                        <td align="right">
                            وضعیت عضویت :
                        </td>
                        <td colspan="4" align="right">
                            <uc1:KidsUserSateWidget ID="ucKidsUserSateWidget" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr runat="server" id="pnlAccRegister" visible="false">
            <td>
                <table style="width: 100%;" runat="server" id="pnlAccRegisterS3" visible="false">
                    <tr id="pnlHaveNewWishAccount1" runat="server">
                        <td width="100%">
                            <span width="100%">کاربر گرامی :<br />
                                جهت استفاده از مزایای سایت و سپرده آرزو، شما میبایست شماره حساب آرزو خود را در این
                                قسمت ثبت نمایید.<br />
                                در صورتیکه شما اقدام به افتتاح حساب ننموده اید، میتوانید به همراه ولی&nbsp; خود
                                با مراجعه به هر یک از شعب بانک ملی ایران در سراسر کشور، نسبت به افتتاح حساب آرزو(سپرده
                                کوتاه مدت سیبا) اقدام نمایید.<br />
                            </span>
                        </td>
                    </tr>
                    <tr id="pnlHaveNewWishAccount2" runat="server">
                        <td>
                            شماره حساب آرزو:
                            <asp:TextBox ID="txtHaveNewWishAccount" runat="server"></asp:TextBox>
                            &nbsp;
                            <asp:Button ID="btnVerifyChildWishAccount" runat="server" EnableTheming="True" Text="بررسی   و ثبت حساب"
                                OnClick="btnVerifyChildWishAccount_Click" />
                        </td>
                    </tr>
                    <tr runat="server" id="pnlHaveNewWishAccount3" visible="false">
                        <td align="center">
                            <asp:Label ID="lblVerifyResult" runat="server"></asp:Label>
                            <br />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;" runat="server" id="pnlAccRegisterS12" visible="false">
                    <tr>
                        <td runat="server" id="pnlSelectInterviewAccountMethod" visible="false" align="right">
                            <asp:RadioButtonList ID="rdoAddAccMethod" runat="server" CellSpacing="20" AutoPostBack="True"
                                OnSelectedIndexChanged="rdoAddAccMethod_SelectedIndexChanged">
                                <asp:ListItem Value="1">اینجانب 
                                {0}
                                به کد ملی {1} 
                                دارای حساب کوتاه مدت جهت استفاده به عنوان حساب آرزو میباشم</asp:ListItem>
                                <asp:ListItem Value="2">اینجانب 
                                {0}
                                به کد ملی {1} 
                                درخواست افتتاح حساب سیستمی از طریق سیستم را دارم</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr runat="server" id="pnlAutoCreatAccountInformationCompletion" visible="false">
                        <td>
                            کاربر محترم : جهت انجام عملیات افتتاح حساب خودکار لازم است با مراجعه به صفحه <a href="/Profile.aspx">
                                پروفایل</a> خود اطلاعات ثبت نام خود را تکمیل نمایید
                        </td>
                    </tr>
                    <tr runat="server" id="pnlAutoCreatAccountWithConfirmation" visible="false">
                        <td style="text-align: right">
                            <fieldset>
                                <legend>توجه</legend>کاربر عزیز :<br />
                                در صورت تایید شما و پرداخت حداقل 100،000 ریال (به عنوان وجه افتتاح حساب)از طریق درگاه
                                پرداخت اینترنتی بانک ملی، درخواست افتتاح حساب خودکار شما در سیستم ثبت خواهد شد و
                                پس از راستی آزمایی اطلاعات کودک و تایید اطلاعات توسط سازمان ثبت احوال، حساب مورد
                                نظر شما افتتاح خواهد شد.<br />
                                لذا پس از گذشت 48 ساعت از ثبت درخواست ضمن مراجعه به این سایت میتوانید از شماره حساب
                                خود آگاه گردید.همچنین شما میتوانید پس از این زمان، با مراجعه به یکی از شعب بانک
                                ملی ایران، ضمن فعالسازی امکان برداشت حساب خود، کارت حساب آرزو خود را نیز دریافت
                                نمایید.<br />
                                <br />
                                <asp:Button ID="btnPayment_CreateAccountWithConfirmation" runat="server" OnClick="btnPayment_CreateAccount_Click"
                                    Text="با موارد بالا موافق هستم و درخواست پرداخت وجه جهت افتتاح حساب جدید آرزو را دارم" />
                                &nbsp;
                                <asp:Button ID="btnCancelCreateAccountWithConfirmation" runat="server" OnClick="btnCancelCreateAccount_Click"
                                    Text="لغو" />
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td runat="server" id="pnlAgreement" visible="false">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <fieldset style="width: 95%">
                                            <legend>فرآیند و مقررات ثبت حساب </legend>
                                            <table width="100%" style="text-align: justify; direction: rtl;">
                                                <tr>
                                                    <td>
                                                        <ol>
                                                            مقررات وشرايط عمومي حساب سپرده سرمايه گذاري كوتاه مدت (سيبا) ‏ < مقدمه: سپرده سرمايه
                                                            گذاري كوتاه مدت به آن دسته از سپرده ها اطلاق ميگردد كه به قصد انتفاع به بانك سپرده
                                                            مي شوند.
                                                            <li>بانك به وكالت از طرف سپرده گذاران با داشتن حق توكيل به غير و وصايت پس از وضع سپرده
                                                                هاي قانوني ، وجوه مربوط را بر طبق قانون عمليات بانكي بدون ربا بطور مشاع بكار گرفته
                                                                و در پايان هر دوره مالي منافع حاصله را پس از كسر حق الوكاله و حق الوصايه با داشتن
                                                                حق مصالحه منافع به تناسب مبلغ و مدت بكارگرفته شده بين خود و سپرده گذاران تقسيم مي
                                                                نمايد . </li>
                                                            <li>اشخاصي كه به سن18 سال شمسي تمام رسيده و يا حكم رشد آنان از دادگاه صالحه صادر شده
                                                                باشد مي توانند بنام خود يا كساني كه تحت ولايت يا قيموميت يا وصايت آنان باشند با
                                                                رعايت مقررات قانوني ، در بانك حساب سپرده سرمايه گذاري كوتاه مدت سيبا افتتاح نمايند
                                                                . </li>
                                                            <li>افتتاح حساب سپرده سرمايه گذاري كوتاه مدت سيبا بنام اشخاص و بموجب وكالتنامه معتبر
                                                                و مورد قبول بانك درصورتيكه موضوع از اختيارات وكيل باشد بلامانع است . </li>
                                                            <li>افتتاح حساب سپرده سرمايه گذاري كوتاه مدت بنام اشخاص حقوقي توسط صاحبان امضاي مجاز
                                                                منوط به پيش بيني موضوع در اساسنامه و با اخذ مدارك و مجوزهاي لازم مربوط به اشخاص
                                                                حقوقي از جمله اساسنامه ، شركت نامه و روزنامه رسمي جمهوري اسلامي ايران متضمن آگهي
                                                                تاسيس و تعيين صاحبان امضاي مجاز شركت و آخرين تغييرات آنها انجام ميپذيرد . </li>
                                                            <li>حداقل مبلغ براي افتتاح حساب سپرده سرمايه گذاري كوتاه مدت براساس دستورالعمل هاي صادره
                                                                مي باشد. </li>
                                                            <li>هنگام افتتاح حساب لازم است بازكننده حساب با تكميل ظهر اين ورقه، نظر خود در مورد
                                                                تمديد حساب در سررسيد را اعلام نمايد. بديهي است بانك حسابهائـي را كه از طرف صاحبان
                                                                حساب مجاز به تمديد آنها نباشد بلافاصله پس از انقضاي سررسيد بسته و موجودي آنها را
                                                                به حساب بستانكاران موقت منتقل خواهد نمود . عدم تكميل يا قلم دويدگي و مخدوشي در قسمت
                                                                مربوطه از نظر بانك به منزله مجوز متقاضي مبني بر تمديد سپرده تلقي مي گردد.</li>
                                                            <li>حساب سپرده سرمايه گذاري كوتاه مدت توسط دارنده حساب يا نماينده قانوني او با ارائه
                                                                دفترچه حساب و كارت شناسايي معتبر (طبق ضوابط و مقررات حاكم) در كليه شعب مجهز به سيبا
                                                                صورت مي گيرد.</li>
                                                            <li>حساب سپرده سرمايه گذاري كوتاه مدت در صورت درخواست صاحب حساب، وكيل ، ولي، قيم يا
                                                                قائم مقام قانوني وي و با تسليم دفترچه ، بسته شده و كليه موجودي و سهم سود متعلقه
                                                                به صاحب يا وكيل و نماينده قانوني حسب مقررات پرداخت مي گردد.</li>
                                                            <li>شرايط تعلق سهم سود به موجودي حسابهاي سپرده سرمايه گذاري كوتاه مدت داشتن حداقل مبلغ
                                                                مانده و مدت زمان ماندگاري طبق ضوابط و مقررات است. </li>
                                                            <li>براي احتساب سهم سود سپرده هاي سرمايه گذاري كوتاه مدت با داشتن گردش عمليات مختلف
                                                                ، كمترين مانده حساب در روز در نظر گرفته مي شود. سهم سود تخصيصي (با رعايت بند9) براساس
                                                                روز افتتاح حساب ، در روز متناظر ماه بعد واريز مي گردد.</li>
                                                            <li>با توجه به اينكه كمترين مانده روز افتتاح و انسداد حساب سپرده سرمايه گذاري كوتاه
                                                                مدت صفر ريال مي باشد ، به مانده روز افتتاح حساب سودي تعلق نمي گيرد، اما مشمول شرط
                                                                توقف مدت زمان معين در بند 10 خواهد بود.</li>
                                                            <li>صاحب حساب مي تواند در طول مدت قرارداد از وجوه متمركز در حساب مفتوحه مبالغي را برداشت
                                                                يا وجوهي را به حساب خود واريز نمايد. ليكن چنانچه گردش عمليات حساب سپرده گذار به
                                                                نحوي باشد كه مانده حساب وي از مبلغ مانده طبق مقررات و ضوابط كمتر گردد با توجه به
                                                                شرايط تعلق سهم سود (مندرج در بند10) حساب مذكور از شرايط تخصيص سهم سود خارج شده و
                                                                برقراري مجدد آن مستلزم تأمين حداقل مبلغ فوق الذكر و استمرار آن به مدت زمان ماندگاري
                                                                طبق مقررات و ضوابط حاكم مي باشد. </li>
                                                            <li>درصورت فوت صاحب حساب ، موجودي حساب و سهم منافع متعلقه در قبال ارائه گواهي حصر وراثت
                                                                و گواهينامه واريز ماليات بر ارث مشروط به اينكه شماره و مانده حساب مربوطه درآن ذكر
                                                                شده باشد و عنداللزوم ساير مدارك قانوني، به وراث قانوني وي قابل پرداخت خواهد بود.</li>
                                                            <li>نظر باينكه سهم سود قطعي متعلقه به وجوه متمركز در حسابهاي سپرده سرمايه گذاري كوتاه
                                                                مدت سيبا در پايان دوره مالي بانك تعيين ميگردد ، لذا سودهاي پرداختي( واريز به حساب
                                                                سپرده گذاران ) در طي دوره مالي بصورت علي الحساب بوده و چنانچه سود علي الحساب با
                                                                سود قطعي تفاوتي پيدا نمايد ، بانك راساً نسبت به تعديل آن اقدام خواهد نمود. </li>
                                                            <li>دارنده حساب يا بازكننده و يا قائم مقام قانوني حسب مورد، مكلف است فقدان و يا سرقت
                                                                دفترچه خود را در اسرع وقت به بانك اطلاع دهد . بديهي است هرگونه ضرر و زيان ناشي از
                                                                عدم اطلاع به بانك برعهده دارنده حساب خواهد بود و هيچگونه مسئوليتي متوجه بانك نخواهد
                                                                بود.</li>
                                                            <li>چنانچه واريز به حساب از شعب غيرسيبا انجام شود ، احتساب سود از زمان واريز وجه به
                                                                حساب در شعبه مجهز به سيبا خواهد بود .
                                                                <li>ضوابط و قوانین بانک ملی ایران را میپذیرم.</li>
                                                        </ol>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnAcceptAgreement" runat="server" Text="شرایط را مطالعه نمودم و باآن موافقت دارم"
                                                            OnClick="btnAcceptAgreement_Click" />
                                                        &nbsp;&nbsp;&nbsp;
                                                        <asp:Button ID="btnRejectAgreement" runat="server" Text="قبول نمیکنم" OnClick="btnRejectAgreement_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server" id="pnlInterviewAccount" visible="false">
                        <td>
                            <asp:TextBox ID="txtHavingAccountWish" runat="server"></asp:TextBox>
                            &nbsp;<asp:Button ID="btnHavingAccountWish" runat="server" OnClick="btnHavingAccountWish_Click"
                                Text="بررسی حساب" />
                        </td>
                    </tr>
                    <tr runat="server" id="pnlAutoCreateAccount" visible="false">
                        <td align="right">
                            <fieldset>
                                <legend>توجه</legend>کاربر عزیز :<br />
                                در صورت تایید شما و پرداخت حداقل 100،000 ریال (به عنوان وجه افتتاح حساب)از طریق درگاه
                                پرداخت اینترنتی بانک ملی، درخواست افتتاح حساب خودکار شما در سیستم ثبت خواهد شد.<br />
                                پس از گذشت 48 ساعت از ثبت درخواست ضمن مراجعه به این سایت میتوانید از شماره حساب
                                خود آگاه گردید.همچنین شما میتوانید پس از این زمان، با مراجعه به یکی از شعب بانک
                                ملی ایران، ضمن فعالسازی امکان برداشت حساب خود، کارت حساب آرزو خود را نیز دریافت
                                نمایید.<br />
                                <br />
                                <asp:Button ID="btnPayment_CreateAccount" runat="server" OnClick="btnPayment_CreateAccount_Click"
                                    Text="با موارد بالا موافق هستم و درخواست پرداخت وجه جهت افتتاح حساب جدید آرزو را دارم" />
                                &nbsp;
                                <asp:Button ID="btnCancelCreateAccount" runat="server" OnClick="btnCancelCreateAccount_Click"
                                    Text="لغو" />
                            </fieldset>
                        </td>
                    </tr>
                    <tr runat="server" id="pnlAutoCreateAccount_PaymentFactor" visible="false">
                        <td>
                            <uc1:PaymentFactorWidget ID="PaymentFactorWidget1" runat="server" />
                        </td>
                    </tr>
                    <tr runat="server" id="pnlAutoCreateAccount_SuccessPayment" visible="false">
                        <td>
                            پرداخت شما با موفقیت صورت پذیرفته و فرآیند افتتاح حساب شما در جریان میباشد
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
