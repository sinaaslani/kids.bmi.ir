<%@ Page Title="تولدت مبارک" Theme="Default" Language="C#" MasterPageFile="~/Masters/Fa.Master"
    AutoEventWireup="true" CodeBehind="KidsBirthday.aspx.cs" Inherits="Site.Kids.bmi.ir.Registration.KidsBirthday" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table  cellpadding="0" cellspacing="0" width="100%"
        align="center" style="text-align: center">
        <tr runat="server">
            <td align="right" valign="top">
                <asp:Image ID="imgKidPic" runat="server" EnableViewState="False" ImageAlign="TextTop"
                    Width="70px" BorderWidth="0px" />
              
            </td>
            <td align="center" valign="top" width="520" rowspan="2">
                <asp:Literal ID="lblSWF" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr runat="server">
            <td align="right" valign="top">
                  دوست عزیزم :
               <asp:Label ID="lblKidsUserName" runat="server"></asp:Label>
                <br />
                تولد
                <asp:Label ID="lblKidsUserAge" runat="server"></asp:Label>
                سالگیت مبارک !</td>
        </tr>
    </table>
</asp:Content>
