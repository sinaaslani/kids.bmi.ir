<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PowerKidsUserWidget.ascx.cs"
    Inherits="Site.Kids.bmi.ir.Scores.PowerKidsUserWidget" %>
<table style="width: 100%;" align="center">
    <tr>
        <td align="right">
               قویترین ساکنین جزیره
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:GridView ID="dgPowerKidsUser" runat="server" AutoGenerateColumns="False" 
                ShowHeader="False" BackColor="#DEBA84" BorderColor="#DEBA84" 
                BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
                <Columns>
                    <asp:TemplateField HeaderText="نام">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("ChildName") + " "+Eval("ChildFamily") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="امتیاز">
                        
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%#String.Format("{0:0.00}",Eval("LastCalCulatedScore")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#FFF1D4" />
                <SortedAscendingHeaderStyle BackColor="#B95C30" />
                <SortedDescendingCellStyle BackColor="#F1E5CE" />
                <SortedDescendingHeaderStyle BackColor="#93451F" />
            </asp:GridView>
        </td>
    </tr>
     </table>
