<%@ Page Title="مدیریت تنظیمات" Theme="Admin" Language="C#" MasterPageFile="~/Masters/Admin.Master"
    AutoEventWireup="true" CodeBehind="ConfigsAdmin.aspx.cs" Inherits="Site.Kids.bmi.ir.AdminCP._Config.ConfigsAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%;">
        <tr>
            <td>
                &nbsp;
            </td>
            <td align="right">
                <asp:LinkButton ID="lnkAddNewConfig" runat="server" OnClick="lnkAddNewConfig_Click">افزودن تنظیم جدید</asp:LinkButton>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td dir="ltr">
                <asp:GridView ID="dgConfigs" runat="server" Width="100%" AutoGenerateColumns="False"
                    DataKeyNames="ConfigName" OnRowEditing="dgConfigs_RowEditing" OnRowUpdating="dgConfigs_RowUpdating"
                    CellPadding="4" ForeColor="#333333" 
                    OnRowCancelingEdit="dgConfigs_RowCancelingEdit" 
                    onrowdeleting="dgConfigs_RowDeleting">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField HeaderText="نام" DataField="ConfigName" />
                        <asp:BoundField HeaderText="مقدار" DataField="ConfigValue" />
                        <asp:CommandField ShowEditButton="True" />
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
