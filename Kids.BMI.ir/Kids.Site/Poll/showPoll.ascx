<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="showPoll.ascx.cs" Inherits="Site.Kids.bmi.ir.Poll.showPoll" %>
<%@ Register Src="showPollResults.ascx" TagName="showPollResults" TagPrefix="uc1" %>
<table style="width: 100%;"  >
    <tr>
        <td align="right" colspan="3">
            <asp:Label ID="questionLbl" runat="server" Css></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="right" id="tdQuestions" runat="server" >
            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                ControlToValidate="rdoPollList" ErrorMessage="يکي از موارد را انتخاب نماييد" Font-Names="Tahoma"
                Font-Size="XX-Small" ValidationGroup="PollSubmit"></asp:RequiredFieldValidator>
            <asp:RadioButtonList ID="rdoPollList" runat="server" EnableViewState="true" 
                CellPadding="0" CellSpacing="0" TextAlign="Left">
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td runat="server" align="center" colspan="3" id="tdActions" visible="false">
            <asp:Button ID="sendResBtn" runat="server" Text=" ارسال "  ValidationGroup="PollSubmit"
                OnClick="sendResBtn_Click" CssClass="btn3"  />
            <asp:Label ID="errorMsgLbl" runat="server" Visible="False" ForeColor="Red" Font-Names="Tahoma"
                Font-Size="XX-Small" Text="يکي از موارد را انتخاب نماييد"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="center" colspan="3" valign="top" id="tdResult" runat="server">
            <asp:Label ID="lblrseult" runat="server"></asp:Label><br />
            <br />
            <uc1:showPollResults ID="ucShowPollResults" runat="server" Visible="false" />
        </td>
    </tr>
</table>
