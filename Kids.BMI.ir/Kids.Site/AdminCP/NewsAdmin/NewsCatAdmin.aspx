<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.Master" AutoEventWireup="True"
    CodeBehind="~/AdminCP/NewsAdmin/NewsCatAdmin.aspx.cs" Inherits="Site.Kids.bmi.ir.AdminCP.NewsAdmin.NewsCatAdmin" Theme="Admin" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="border-collapse: collapse" height="186" width="520">
        <tr>
            <td  align="right" width="520" colspan="2" height="15">
                <br>
                <img src="/App_Themes/Admin/Images/admin_bullet.gif">
               
                    <asp:Literal ID="lblHead" runat="server"></asp:Literal>
                
            </td>
        </tr>
        <tr>
             <td  align="right" height="31">
                موضوع&nbsp;:
            </td>
            <td  width="100%" height="31">
                <asp:TextBox ID="CatName" runat="server"></asp:TextBox><br>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Css
                    runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="CatName">خطا : موضوع الزامی است</asp:RequiredFieldValidator>
            </td>
           
        </tr>
        <tr>
            <td  align="right" height="41">
                توضیحات :
            </td>
            <td  width="415" height="41">
                <asp:TextBox ID="comment" runat="server" Width="306px" TextMode="MultiLine"></asp:TextBox>
            </td>
            
        </tr>
        <tr>
            <td  align="right" height="41">
                قابل مشاهده :</td>
            <td  width="415" height="41" align="right">
                <asp:CheckBox ID="chkIsVisible" runat="server" />
            </td>
            
        </tr>
        <tr>
             <td  align="right" height="27">
                الویت نمایش&nbsp;:
            </td>
            <td  align="right" width="415" height="27">
                <asp:DropDownList ID="SortOrder" runat="server">
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="3">3</asp:ListItem>
                    <asp:ListItem Value="4">4</asp:ListItem>
                    <asp:ListItem Value="5">5</asp:ListItem>
                    <asp:ListItem Value="6">6</asp:ListItem>
                    <asp:ListItem Value="7">7</asp:ListItem>
                    <asp:ListItem Value="8">8</asp:ListItem>
                    <asp:ListItem Value="9">9</asp:ListItem>
                    <asp:ListItem Value="10">10</asp:ListItem>
                    <asp:ListItem Value="11">11</asp:ListItem>
                    <asp:ListItem Value="12">12</asp:ListItem>
                    <asp:ListItem Value="13">13</asp:ListItem>
                    <asp:ListItem Value="14">14</asp:ListItem>
                    <asp:ListItem Value="15">15</asp:ListItem>
                    <asp:ListItem Value="16">16</asp:ListItem>
                    <asp:ListItem Value="17">17</asp:ListItem>
                    <asp:ListItem Value="28">28</asp:ListItem>
                    <asp:ListItem Value="29">29</asp:ListItem>
                    <asp:ListItem Value="20">20</asp:ListItem>
                </asp:DropDownList>
            </td>
           
        </tr>
        <tr>
            <td  align="right" width="415" height="64" colspan="2">
                &nbsp;
                <asp:Button ID="btnSave"  runat="server" 
                    onclick="btnSave_Click"></asp:Button>&nbsp;
                <asp:Button ID="btnCancel"  runat="server" Text="انصراف" 
                    CausesValidation="False" onclick="btnCancel_Click">
                </asp:Button>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
