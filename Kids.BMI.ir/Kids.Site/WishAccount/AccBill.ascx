<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AccBill.ascx.cs" Inherits="Site.Kids.bmi.ir.WishAccount.AccBill" %>
<%@ Import Namespace="Kids.Utility" %>
<%@ Import Namespace="Kids.Utility.UtilExtension.StringExtensions" %>
<%@ Register Src="../UserControls/ucDatePicker.ascx" TagName="ucDatePicker" TagPrefix="uc1" %>
<fieldset style="width: 98%">
    <legend>صورتحساب آرزو</legend>
    <table style="width: 100%;">
        <tr>
            <td colspan="4">
                شماره حساب :<asp:Label ID="lblAccNumber" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 50px" valign="bottom">
                از تاریخ :
            </td>
            <td align="right">
                <uc1:ucDatePicker ID="ucFromDate" runat="server" ShowTime="False" />
            </td>
            <td style="width: 50px;" align="left" valign="bottom">
                تا تاریخ:
            </td>
            <td align="right">
                <uc1:ucDatePicker ID="ucToDate" runat="server" ShowTime="False" />
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Button ID="btnGetAccList" runat="server" Text="مشاهده صورت حساب" OnClick="btnGetAccList_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:GridView ID="dgAccList" runat="server" AutoGenerateColumns="False" Width="100%"
                    CellPadding="4" ForeColor="#333333" GridLines="None" 
                    OnRowDataBound="dgAccList_RowDataBound" AllowPaging="True" 
                    onpageindexchanging="dgAccList_PageIndexChanging" PageSize="20">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="ردیف">
                            <ItemTemplate>
                                <asp:Label ID="lblRowId" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="تاریخ ">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%#new PersianDateTime(Eval("tx_Date").ToString()).ToShortDateString().ToPersinDigit()  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="زمان ">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("tx_Time").ToString().AddSplitter(":",2).ToPersinDigit() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="نوع ">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("tx_Type").ToString()=="C"?"واریز":"برداشت" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="شعبه">
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("brno").ToString().ToPersinDigit()  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ش.پیگیری">
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("tx_trace").ToString().ToPersinDigit() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="شرح ">
                            <ItemTemplate>
                                <asp:Label ID="Label8" runat="server" Text='<%# Eval("sharh").ToString().ToPersinDigit() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="مبلغ">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("tx_amount").ToString().Replace(".0000","").Money3Dispaly().ToPersinDigit()  %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="مانده ">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("tx_cbalance").ToString().Replace(".0000","").Money3Dispaly().ToPersinDigit() %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </asp:TemplateField>
                    </Columns>
                    <AlternatingRowStyle CssClass="DgAlt" />
                    <RowStyle CssClass="DgItem" />
                    <FooterStyle Font-Bold="True" />
                    <EditRowStyle BackColor="#2461BF" />
                    <SelectedRowStyle Font-Bold="True" CssClass="DgSItem" />
                    <PagerStyle HorizontalAlign="Center" CssClass="DgPager"  />
                    <HeaderStyle CssClass="DgHeader" Font-Bold="True" />
                    <PagerSettings Mode="NumericFirstLast" />
                </asp:GridView>
            </td>
        </tr>
    </table>
</fieldset>
