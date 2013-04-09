<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.Master" ValidateRequest="false"
    AutoEventWireup="True" CodeBehind="AddEditBankExamQuestion.aspx.cs" Inherits="Site.Kids.bmi.ir.AdminCP.BankExamAdmin.AddEditBankExamQuestion"
    Theme="Admin" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%" width="100%" dir="ltr">
        <tr>
            <td align="right">
               <asp:Label ID="pollTitleLbl" runat="server"></asp:Label>
                <img src="/App_Themes/Admin/Images/admin_bullet.gif" id="questionImg" alt=""></td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%; height: 145px" height="145" width="100%" align="right">
                    <tr>
                        <td style="height: 26px" align="right" width="100%" colspan="2" >
                            <img src="/App_Themes/Admin/Images/admin_bullet.gif">
                                <asp:Label ID="HeaderMsgLbl" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; height: 25px" width="100%" height="25" align="right">
                            <asp:TextBox ID="txtQuestionBody" runat="server" Width="350px" 
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td valign="top" align="right">
                            عنوان&nbsp;سوال :
                        </td>
                    </tr>
                    <div runat="server" id="NeedInputTextDiv">
                    </div>
                    <div>
                    </div>
                    <tr>
                        <td style="width: 100%; height: 25px" width="100%" height="25" align="right">
                            <asp:TextBox ID="txtQuestionAnswerA" runat="server" Width="350px" 
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td style="height: 25px" valign="top" height="25" align="right">
                            گزینه اول :</td>
                    </tr>
                    <tr>
                        <td style="width: 100%; height: 25px" width="100%" height="25" align="right">
                            <asp:TextBox ID="txtQuestionAnswerB" runat="server" Width="350px" 
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td style="height: 25px" valign="top" height="25" align="right">
                            گزینه دوم :</td>
                    </tr>
                    <tr>
                        <td style="width: 100%; height: 25px" width="100%" height="25" align="right">
                            <asp:TextBox ID="txtQuestionAnswerC" runat="server" Width="350px" 
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td style="height: 25px" valign="top" height="25" align="right">
                            گزینه سوم :</td>
                    </tr>
                    <tr>
                        <td style="width: 100%; height: 25px" width="100%" height="25" align="right">
                            <asp:TextBox ID="txtQuestionAnswerD" runat="server" Width="350px" 
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td style="height: 25px" valign="top" height="25" align="right">
                            گزینه چهارم :</td>
                    </tr>
                    <tr>
                        <td style="width: 100%; height: 25px" width="100%" height="25" align="right">
                            <asp:DropDownList ID="drpCorrectAnswer" runat="server">
                                <asp:ListItem Selected="True">-------</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="height: 25px" valign="top" height="25" align="right">
                            گزینه صحیح :</td>
                    </tr>
                    <tr>
                        <td style="width: 100%; height: 26px" width="100%" >
                            &nbsp;<asp:Button ID="AddBtn" runat="server" Text="   اضافه   " Font-Names="Tahoma"
                                 OnClick="AddBtn_Click"></asp:Button>
                            <asp:Button ID="editDelBtn" runat="server" Font-Names="Tahoma" 
                                OnClick="editDelBtn_Click"></asp:Button>&nbsp;<asp:Button ID="cancelBtn" runat="server"
                                    Text="  انصراف  " Font-Names="Tahoma"  OnClick="cancelBtn_Click">
                                </asp:Button>&nbsp;
                            <asp:CheckBox ID="HasNextResponseItem" runat="server" Text="ايجاد سوال بعدي"
                                ></asp:CheckBox>
                        </td>
                        <td style="height: 26px" >
                            <img style="width: 126px; height: 1px" height="1" src="blankImg.gif" width="126">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="100%" colspan="2" height="30">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
