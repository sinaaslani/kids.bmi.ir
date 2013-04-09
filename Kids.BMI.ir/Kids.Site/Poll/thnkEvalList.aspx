<%@ Page Language="C#" Theme="Default" MasterPageFile="~/Masters/FA.master" AutoEventWireup="True"
    CodeBehind="thnkEvalList.aspx.cs" Inherits="Site.Kids.bmi.ir.Poll.thnkEvalList"
    Title="ليست فرم هاي افکار سنجي" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <table width="100%" height="20">
        <tr>
            <td>
                <div align="center">
                    <asp:GridView ID="ThbkEvalGrid" CellSpacing="1" runat="server" Font-Names="Tahoma"
                        Font-Size="9pt" ForeColor="Black" GridLines="None" Width="100%" CellPadding="2"
                        AutoGenerateColumns="False" PageSize="20" PagerSettings-Position="TopAndBottom"
                        PagerSettings-Mode="NumericFirstLast" Height="30px" ShowHeader="False" 
                        BackColor="White" onrowdatabound="ThbkEvalGrid_RowDataBound">
                        <AlternatingRowStyle Font-Names="Tahoma" Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle">
                        </AlternatingRowStyle>
                        <RowStyle Font-Names="Tahoma" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <HeaderStyle Font-Size="9pt" Font-Names="Tahoma" HorizontalAlign="Center" ForeColor="White"
                            CssClass="forumHeaderBackgroundAlternate" BackColor="#0D66BA"></HeaderStyle>
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkForm" runat="server" NavigateUrl='<%# Eval("FormId", "~/Fa/ShowThnkEval.aspx?frmId={0}") %>'
                                        Text='<%# Eval("Title") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle HorizontalAlign="Center" ForeColor="Blue" Wrap="False"></PagerStyle>
                        <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                
            </td>
        </tr>
    </table>
</asp:Content>
