<%@ Page Title="آزمون" Theme="Default" Language="C#" MasterPageFile="~/Masters/Fa.Master"
    AutoEventWireup="true" CodeBehind="BankStoryExam.aspx.cs" Inherits="Site.Kids.bmi.ir.BankStory._BankStoryExam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Timer ID="ExamTimer" runat="server" Interval="45000" OnTick="ExamTimer_Tick">
    </asp:Timer>
    <asp:Label ID="lblExamTimer" runat="server" Text=""></asp:Label>
    <table style="width: 100%;">
        <tr id="pnlMain" runat="server" visible="false">
            <td colspan="3">
                <asp:GridView ID="dgExamQuestion" runat="server" Width="100%" AutoGenerateColumns="False"
                    ShowHeader="False">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <table style="width: 100%;">
                                    <tr>
                                        <td align="right" colspan="2">
                                            <asp:Label ID="Label1" runat="server" Text='<%#(Container.DataItemIndex + 1)+"-"  %>'></asp:Label>
                                            <asp:Label ID="lblQuestion" runat="server" Text='<%#Eval("QuestionBody")  %>'></asp:Label>
                                            <asp:HiddenField ID="QuestionId" runat="server" Value='<%#Eval("QuestionId") %>'>
                                            </asp:HiddenField>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:RadioButton ID="rdoAnswerA" Text='<%#Eval("AnswerA") %>' GroupName='<%#"grp_"+Eval("QuestionId") %>'
                                                runat="server" />
                                        </td>
                                        <td align="right">
                                            <asp:RadioButton ID="rdoAnswerB" Text='<%#Eval("AnswerB") %>' GroupName='<%#"grp_"+Eval("QuestionId") %>'
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:RadioButton ID="rdoAnswerC" Text='<%#Eval("AnswerC") %>' GroupName='<%#"grp_"+Eval("QuestionId") %>'
                                                runat="server" />
                                        </td>
                                        <td align="right">
                                            <asp:RadioButton ID="rdoAnswerD" Text='<%#Eval("AnswerD") %>' GroupName='<%#"grp_"+Eval("QuestionId") %>'
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="2">
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
                <asp:Button ID="btnFinishExam" runat="server" Text="پایان آزمون و مشاهده نتیجه" OnClick="btnFinishExam_Click" />
            </td>
        </tr>
        <tr id="pnlDetails" runat="server" visible="false">
            <td colspan="3">
                <table style="width: 70%;">
                    <tr>
                        <td align="right" width="110">
                            تعداد پاسخ درست :
                        </td>
                        <td align="right">
                            <asp:Label ID="lblCorrectCount" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            تعداد پاسخ غلط :
                        </td>
                        <td align="right">
                            <asp:Label ID="lblInCorectCount" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            تعداد پاسخ داده نشده :
                        </td>
                        <td align="right">
                            <asp:Label ID="lblNoAnswerCount" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            نمره :
                        </td>
                        <td align="right">
                            <asp:Label ID="lblScore" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
