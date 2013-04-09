<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentList.ascx.cs"
    Inherits="Site.Kids.bmi.ir.Payment.PaymentList" %>
<%@ Import Namespace="Kids.Utility" %>
<asp:GridView ID="dgPaymentList" runat="server" AutoGenerateColumns="False" >
    <Columns>
        <asp:BoundField DataField="RetrivalRefNo" HeaderText="شماره مرجع" />
        <asp:BoundField DataField="CustomerCardNo" HeaderText="شماره کارت" />
         <asp:BoundField DataField="Amount" HeaderText="مبلغ" />
        <asp:BoundField DataField="SystemTraceNo" HeaderText="شماره پیگیری" />
        <asp:BoundField DataField="OrderId" HeaderText="شماره سفارش" />
        <asp:BoundField DataField="AppStatusCode" HeaderText="کد پاسخ" />
        <asp:BoundField DataField="AppStatusDescription" HeaderText="شرح پاسخ" />
        <asp:BoundField DataField="UserIPAddress" HeaderText="IP کاربر" />
        <asp:TemplateField HeaderText="تاریخ ثبت">
            <ItemTemplate>
                <asp:Label ID="lblCreateDateTime" runat="server" Text='<%#PersianDateTime.MiladiToPersian((DateTime)Eval("CreateDateTime")).ToLongDateTimeString() %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="تاریخ بروزرسانی">
            <ItemTemplate>
                <asp:Label ID="lblLastUpdateDateTime" runat="server" Text='<%#Eval("LastUpdateDateTime")==null?null: PersianDateTime.MiladiToPersian((DateTime)Eval("LastUpdateDateTime")).ToLongDateTimeString() %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
