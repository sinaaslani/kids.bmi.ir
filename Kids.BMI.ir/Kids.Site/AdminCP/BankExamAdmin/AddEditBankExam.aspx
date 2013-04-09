<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.Master" ValidateRequest="false"
    AutoEventWireup="True" CodeBehind="AddEditBankExam.aspx.cs" Inherits="Site.Kids.bmi.ir.AdminCP.BankExamAdmin.AddEditBankExam"
    Theme="Admin" %>

<%@ Register src="~/UserControls/ucDatePicker.ascx" tagname="ucDatePicker" tagprefix="uc1" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%">
        <tr>
            <td>
                <div runat="server" id="AddChapterSkin">
                    <table style="width: 100%;" height="145" width="100%" align="right">
                        <tr>
                            <td style="height: 26px" align="right" width="100%" colspan="2" >
                                <img src="/App_Themes/Admin/Images/admin_bullet.gif">
                                    <asp:Label ID="HeaderMsgLbl" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="right" width="50">
                                عنوان&nbsp;آزمون :
                            </td>
                            <td  align="right">
                                <asp:TextBox ID="txtExamName" runat="server" Width="280px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="txtExamName" ErrorMessage="RequiredFieldValidator"> خطا : عنوان الزامي است</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                مدت زمان آزمون :</td>
                            <td align="right">
                                <asp:TextBox ID="txtExamDuration" runat="server"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="txtExamDuration_FilteredTextBoxExtender" 
                                    runat="server" Enabled="True" FilterType="Numbers" 
                                    TargetControlID="txtExamDuration">
                                </asp:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                شرح آزمون :
                            </td>
                            <td align="right">
                                <asp:TextBox ID="txtExamDescription" runat="server" Width="280px" 
                                    TextMode="MultiLine"></asp:TextBox>
                                </td>
                        </tr>
                        <tr>
                            <td align="right">
                                فغال از تاریخ :</td>
                            <td align="right">
                                <uc1:ucDatePicker ID="ucActiveFromDate" runat="server" ShowTime="True" />
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 28px" height="28" align="right">
                                فعال تا تاریخ :</td>
                            <td align="right">
                                <uc1:ucDatePicker ID="ucActiveToDate" runat="server" ShowTime="True" />
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 28px" height="28" align="right">
                                نوع امتیاز :</td>
                            <td align="right">
                                <asp:DropDownList ID="drpScoreTypeId" runat="server">
                                </asp:DropDownList>
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
                                <asp:HyperLink ID="EditresponseItems" runat="server" >ويرايش سوالات آزمون</asp:HyperLink>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
