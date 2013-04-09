<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucMenuSide.ascx.cs"
    Inherits="Site.Kids.bmi.ir.Masters.ucMenuSide" %>
    <div style="width: 100%; background-image: url(/App_Themes/Default/images/Menu/menu-back.png); background-position: right; background-repeat: no-repeat;  height: 550px"><br/>
<table style="width: 230px;">
    <tr>
        <td align="center" style="background-image: url(/App_Themes/Default/images/Menu/menu1.png);
            height: 43px; width:212px;  background-repeat: no-repeat; background-position: center">
            <a href="/جزیره آرزوها.aspx" class="Mj_Casablanca" style="color:#332a16; ">جزیره آرزوها</a>
        </td>
    </tr>
    <tr>
        <td align="center" style="background-image: url(/App_Themes/Default/images/Menu/menu2.png);
            height: 43px; width:212px;  background-repeat: no-repeat; background-position: center">
            <a href="/بازی.aspx" class="Mj_Casablanca" style="color:#332a16; ">بازی و سرگرمی</a>
        </td>
    </tr>
    <tr>
        <td align="center" style="background-image: url(/App_Themes/Default/images/Menu/menu3.png);
            height: 43px; width:212px;  background-repeat: no-repeat; background-position: center">
            <a href="/WishAcc.aspx" class="Mj_Casablanca" style="color:#332a16; ">حساب آرزو</a>
        </td>
    </tr>
     <tr>
        <td align="center" style="background-image: url(/App_Themes/Default/images/Menu/menu3.png);
            height: 43px; width:212px;  background-repeat: no-repeat; background-position: center">
            <a href="/PostalCardList.aspx" class="Mj_Casablanca" style="color:#332a16; ">ارسال کارت پستال</a>
        </td>
    </tr>
    <tr>
        <td align="center" style="background-image: url(/App_Themes/Default/images/Menu/menu1.png);
            height: 43px; width:212px;  background-repeat: no-repeat; background-position: center">
            <a href="/Tunel.aspx" class="Mj_Casablanca" style="color:#332a16; ">تونل گنج</a>
        </td>
    </tr>
    <tr>
        <td align="center" style="background-image: url(/App_Themes/Default/images/Menu/menu2.png);
            height: 43px; width:212px;  background-repeat: no-repeat; background-position: center">
            <a href="/BankStory.aspx" class="Mj_Casablanca" style="color:#332a16; ">داستان بانک</a>
        </td>
    </tr>
     <tr>
        <td align="center" style="background-image: url(/App_Themes/Default/images/Menu/menu3.png);
            height: 43px; width:212px;  background-repeat: no-repeat; background-position: center">
            <a href="/Exam.aspx" class="Mj_Casablanca" style="color:#332a16; ">شرکت در آزمون</a>
        </td>
    </tr>
    <tr>
        <td align="center" style="background-image: url(/App_Themes/Default/images/Menu/menu1.png);
            height: 43px; width:212px;  background-repeat: no-repeat;  background-position: center">
            <a href="/InfoBox.aspx" class="Mj_Casablanca" style="color:#332a16; ">صندوقچه اخبار</a>
        </td>
    </tr>
  
   <%-- <tr>
       <td align="center" style="background-image: url(/App_Themes/Default/images/Menu/menu2.png);
            height: 43px; width:212px;  background-repeat: no-repeat;  background-position: center">
            <a href="/Poll/thnkEvalList.aspx" class="Mj_Casablanca" style="color:#332a16; ">تکمیل پرسشنامه</a>
        </td>
    </tr>--%>
      <asp:Literal runat="server" ID="mnuContent"></asp:Literal>
</table>
</div>