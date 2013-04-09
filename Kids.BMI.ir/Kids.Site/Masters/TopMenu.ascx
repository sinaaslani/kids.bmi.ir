<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="TopMenu.ascx.cs" Inherits="Site.Kids.bmi.ir.Masters.TopMenu" %>
<%@ Register Src="../Scores/ScoreWidget.ascx" TagName="ScoreWidget" TagPrefix="uc1" %>
<%@ Register Src="../WishAccount/WishWidget.ascx" TagName="WishWidget" TagPrefix="uc2" %>
<%@ Register Src="../Registration/KidsBirthDayWidget.ascx" TagName="KidsBirthDayWidget"
    TagPrefix="uc3" %>
<style type="text/css">
    .style1
    {
        width: 262px;
    }
    .style2
    {
        width: 100px;
    }
</style>
<script type="text/javascript">
    $(function () {
        $('#lnkProfile').powerTip({ placement: 'w' });

    });
</script>
<table style="width: 100%; font-family: Mj_Two; padding:3px; padding-top:8px; padding-left:20px;" dir="ltr" border="0" bordercolor="red" cellpadding="0" cellspacing="0">
    <tr>
        <td valign="middle" style="background-color:#fffee7; text-align:center" 
            class="style2">
               <div style=" border:1px dashed  #cecb6d; height:90px;">
            <asp:HyperLink ID="lnkRegister" Visible="false" runat="server" EnableViewState="False"
                NavigateUrl="~/Register.aspx">عضویت</asp:HyperLink>
           <br />
            <asp:LinkButton ID="lnkLogout" runat="server" OnClick="lnkLogout_Click" Visible="False"
                EnableViewState="False" CausesValidation="False">خروج</asp:LinkButton>
             <br />
            <asp:LinkButton ID="lnkLogin" runat="server" EnableViewState="False" OnClick="lnkLogin_Click"
                Visible="False" CausesValidation="False">ورود</asp:LinkButton>
               <br />
            <asp:HyperLink ID="lnkInviteFriend" runat="server" OnClick="lnkLogout_Click" Visible="False"
                NavigateUrl="~/InviteFriend.aspx" EnableViewState="False">دعوت از دوستان</asp:HyperLink>
                    <br />
            <asp:HyperLink ID="lnkRegisterAccount" runat="server" NavigateUrl="~/RegisterAcc.aspx"
                Visible="False" EnableViewState="False">معرفی حساب آرزو</asp:HyperLink>
                </div>
        </td>
        <td align="center"><img src="/App_Themes/Default/images/Seprator.png"/></td>
        <td valign="middle"  width="80" align="center" 
            style=" background-color:#fff6f6;" >
            <div style=" border:1px dashed  #ffbeab;">
            <asp:HyperLink ID="lnkOtherBirthDay" runat="server" NavigateUrl="~/Registration/TodayKidsBirthDay.aspx"> 
                  <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Default/images/BirthDay/cake.png" BorderWidth="0"  Width="70px"/><br/> 
               تولد کیاست؟
            </asp:HyperLink></div>
        </td>
         <td align="center"><img src="/App_Themes/Default/images/Seprator.png" /></td>
        <td valign="middle"  align="center" style=" background-color:#e9fdec;" >
              <div style=" border:1px dashed  #a5e3a5;">
            <asp:HyperLink ID="lnkOtherNewUser" runat="server" NavigateUrl="~/Scores/PowerKidsUser.aspx"> 
                <asp:Image runat="server" ImageUrl="~/App_Themes/Default/images/strong.png" BorderWidth="0" /><br/>
                ساکنین قوی
            </asp:HyperLink></div>
        </td>
        <td align="center"><img src="/App_Themes/Default/images/Seprator.png" /></td>
        <td valign="middle" style=" background-color:#e9fdfd;" 
            class="style1" >
             <div style=" border:1px dashed  #7edae0; height:90px;">
            <uc2:WishWidget ID="ucWishWidget" runat="server" Visible="false" />
            <uc3:KidsBirthDayWidget ID="ucKidsBirthDayWidget" runat="server" Visible="false" /></div>
        </td>
        <td align="center"><img src="/App_Themes/Default/images/Seprator.png" /></td>
        <td align="right" valign="middle" style=" background-color:#eff3fd;" >
            <div style=" border:1px dashed  #b8baf0; height:90px;">
            <a id="lnkProfile" visible="False" runat="server" clientidmode="Static" title="ویرایش اطلاعات کاربری"
                href="~/Profile.aspx">
                <table style="width: 100%;">
                    <tr>
                        <td align="right">
                            <asp:Label runat="server" ID="lblCurrentUser"></asp:Label>
                        </td>
                        <td rowspan="2" style="padding-left:5px;">
                            <asp:Image ID="imgKidPic" runat="server" Visible="False" EnableViewState="False"
                                ImageAlign="Middle" Width="65px" BorderWidth="0px" />
                        </td>
                    </tr>
                 
                    <tr>
                        <td align="right">
            <asp:Label runat="server" ID="lblCurrentGuestUser"></asp:Label>
                            <uc1:ScoreWidget ID="ScoreWidget1" runat="server" />
                        </td>
                    </tr>
                </table>
            </a></div>
        </td>
       
    </tr>
</table>
