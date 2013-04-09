<%@ Page Language="C#" Theme="Admin" MasterPageFile="~/Masters/Admin.Master"
    AutoEventWireup="True" Inherits="Site.Kids.bmi.ir.AdminCP.Logging.ViewLogs" Title="مشاهده خطاها"
    CodeBehind="ViewLogs.aspx.cs" %>
<%@ Import Namespace="Kids.Utility" %>


<%@ Register TagPrefix="uc2" TagName="ucDatePicker" Src="~/UserControls/ucDatePicker.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatepnlMain" runat="server">
        <ContentTemplate>
            <script type="text/javascript">
                function SetMessage(Message) {
                    document.getElementById("<%=lblMessage.ClientID %>").innerHTML = Message;
                }
            </script>
            <table  width="100%">
                <tr>
                    <td align="center">
                        <asp:Panel runat="server" ID="pnlSearch" DefaultButton="Search">
                            <table width="100%" style="border-right: black 1px solid; border-top: black 1px solid;
                                border-left: black 1px solid; border-bottom: black 1px solid">
                                <tr>
                                    <td align="left" width="100">
                                        شرح خطا
                                    </td>
                                    <td width="10%">
                                        <asp:TextBox ID="txtErrorDescription" runat="server" BackColor="#DEDFDE"></asp:TextBox>
                                    </td>
                                    <td align="left" width="100">
                                        <asp:Label ID="Label3" runat="server" Text=" تاریخ"></asp:Label>
                                        &nbsp;
                                    </td>
                                    <td style="width: 161px" align="right">
                                        &nbsp;<uc2:ucDatePicker ID="ucDatePicker1" runat="server" IsRequired="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="4">
                                        <table border="0"  width="100%">
                                            <tr>
                                                <td align="right" style="width: 80%" width="40%">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Button ID="Search" runat="server" EnabledDuringCallBack="false" OnClick="Search_Click"
                                                        Text="جستجو"  Style="height: 26px" />
                                                    &nbsp;
                                                    <asp:Button ID="Clear" runat="server"  OnClick="Clear_Click"
                                                        Text="پاک" Width="34px" />
                                                    <br />
                                                    <asp:Label ID="lblMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel runat="server" ID="pnlGrids" Width="100%">
                            <asp:GridView ID="dgErrorList" runat="server" AllowPaging="True" BackColor="White"
                                BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                PageSize="30" Width="100%" AutoGenerateColumns="False" 
                                onpageindexchanging="dgErrorList_PageIndexChanging">
                                <AlternatingRowStyle BackColor="#F7F7F7" />
                                <Columns>
                                    <asp:BoundField DataField="Location" HeaderText="محل" >
                                    <ItemStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MethodName" HeaderText="متد" >
                                    <ItemStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ErrorDescription" HeaderText="شرح" >
                                    <ItemStyle Width="60%" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="زمان">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# PersianDateTime.MiladiToPersian((DateTime) Eval("LogDateTime")).ToLongDateTimeString() %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ErrorType" HeaderText="نوع" >
                                    <ItemStyle Width="10%" />
                                    </asp:BoundField>
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
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
