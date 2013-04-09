<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ScoreList.ascx.cs" Inherits="Site.Kids.bmi.ir.Scores.ScoreList" %>
<asp:GridView ID="dgDailyScoreList" runat="server" AutoGenerateColumns="False" OnRowDataBound="dgDailyScoreList_RowDataBound"
    ShowFooter="True" ShowHeaderWhenEmpty="True" BackColor="White" BorderColor="#E7E7FF"
    BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal">
    <AlternatingRowStyle BackColor="#F7F7F7" />
    <Columns>
        <asp:TemplateField HeaderText="نوع امتیاز">
            <ItemTemplate>
                <asp:Label ID="lblScoreName" runat="server"></asp:Label>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Right" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="تاریخ">
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Date").ToString().Substring(0,4)+"/"+Eval("Date").ToString().Substring(4,2)+"/"+Eval("Date").ToString().Substring(6,2)%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="امتیاز کسب شده">
            <ItemTemplate>
                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Sum_NotFiltered") %>'></asp:Label>
            </ItemTemplate>
            <FooterTemplate>
                <asp:Label ID="lblSum_NotFiltered" runat="server"></asp:Label>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="امتیاز کسب شده موثر">
            <ItemTemplate>
                <asp:Label ID="Label3" runat="server" Text='<%# Bind("Sum_Filtered") %>'></asp:Label>
            </ItemTemplate>
            <FooterTemplate>
                <asp:Label ID="lblSum_Filtered" runat="server"></asp:Label>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="حداکثر روزانه">
            <ItemTemplate>
                <asp:Label ID="lblMaxPerDay" runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="ضریب محاسبه امتیاز">
            <ItemTemplate>
                <asp:Label ID="lblCoefficentValue" runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
    <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
    <SortedAscendingCellStyle BackColor="#F4F4FD" />
    <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
    <SortedDescendingCellStyle BackColor="#D8D8F0" />
    <SortedDescendingHeaderStyle BackColor="#3E3277" />
</asp:GridView>
<br />
<asp:GridView ID="dgMonthlyScoreList" runat="server" AutoGenerateColumns="False"
    OnRowDataBound="dgMonthlyScoreList_RowDataBound" ShowFooter="True" ShowHeaderWhenEmpty="True"
    BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px"
    CellPadding="3" GridLines="Horizontal">
    <AlternatingRowStyle BackColor="#F7F7F7" />
    <Columns>
        <asp:TemplateField HeaderText="نوع امتیاز">
            <ItemTemplate>
                <asp:Label ID="lblScoreName" runat="server"></asp:Label>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Right" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="تاریخ">
            <ItemTemplate>
                <asp:Label ID="Label4" runat="server" Text='<%# Eval("Date").ToString().Substring(0,4)+"/"+Eval("Date").ToString().Substring(4,2)%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="امتیاز کسب شده">
            <ItemTemplate>
                <asp:Label ID="Label5" runat="server" Text='<%# Bind("Sum_NotFiltered") %>'></asp:Label>
            </ItemTemplate>
            <FooterTemplate>
                <asp:Label ID="lblSum_NotFiltered" runat="server"></asp:Label>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="امتیاز کسب شده موثر">
            <ItemTemplate>
                <asp:Label ID="Label6" runat="server" Text='<%# Bind("Sum_Filtered") %>'></asp:Label>
            </ItemTemplate>
            <FooterTemplate>
                <asp:Label ID="lblSum_Filtered" runat="server"></asp:Label>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="حداکثر ماهانه">
            <ItemTemplate>
                <asp:Label ID="lblMaxPerMonth" runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="ضریب محاسبه امتیاز">
            <ItemTemplate>
                <asp:Label ID="lblCoefficentValue" runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
    <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
    <SortedAscendingCellStyle BackColor="#F4F4FD" />
    <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
    <SortedDescendingCellStyle BackColor="#D8D8F0" />
    <SortedDescendingHeaderStyle BackColor="#3E3277" />
</asp:GridView>
