<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.Master" AutoEventWireup="True"
    CodeBehind="NewsList.aspx.cs" Inherits="Site.Kids.bmi.ir.AdminCP.NewsAdmin.NewsList"
    Theme="Admin" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%@ register src="~/UserControls/ucDatePicker.ascx" tagname="ucDatePicker" tagprefix="uc1" %>
    <table width="100%">
        <tr>
            <td  width="100%">
                <img src="/App_Themes/Admin/Images/admin_bullet.gif">&nbsp;
                    لیست اخبار&nbsp;
            </td>
        </tr>
        <tr>
            <td style="height: 103px" width="100%">
                <table width="100%">
                    <tr>
                        <td align="right"  style="height: 20px">
                            موضوع :
                        </td>
                        <td style="width: 538px; height: 20px"  align="right">
                            <asp:DropDownList ID="NewsCatCtrl" runat="server">
                               
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td  align="right">
                            از تاریخ&nbsp;:
                        </td>
                        <td  style="width: 538px" align="right">
                            <uc1:ucDatePicker ID="ucFromDate" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td  style="height: 6px">
                            تا تاریخ&nbsp;:
                        </td>
                        <td  style="width: 538px; height: 6px" align="right">
                            <uc1:ucDatePicker ID="ucToDate" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 6px"  align="right">
                            عبارت&nbsp;:
                        </td>
                        <td style="width: 538px; height: 6px"  align="right">
                            <asp:TextBox ID="searchKeyTxt" runat="server" Width="207px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            &nbsp;<img style="width: 57px; height: 2px" height="2" src="../../App_Themes/Default/images/blankImg.gif"
                                width="57">
                        </td>
                        <td  style="width: 538px">
                            <asp:Button ID="btnSearch" runat="server"  Text="   جستجو   "
                                OnClick="btnSearch_Click"></asp:Button>&nbsp;&nbsp;
                            <asp:Label ID="InvalisSearchMsg" runat="server" CssClass="validationWarningSmall"
                                Visible="False">خطا : تاریخ شروع بعد از تاریخ انتها می باشد</asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td  align="right" width="100%" style="height: 21px">
                <font face="Tahoma" size="2">تعداد نتایج یافت شده&nbsp;:</font>
                <asp:Label ID="ResultNumberLbl"  runat="server"></asp:Label>&nbsp;<font
                    face="Tahoma" size="2">مورد</font>
            </td>
        </tr>
        <tr>
            <td  align="center" width="100%">
                <div align="center">
                    <asp:GridView ID="newsGrid" Height="30px" runat="server" AllowPaging="True" PageSize="20"
                        AutoGenerateColumns="False" CellPadding="2" Width="100%" GridLines="None" ForeColor="Black"
                        Font-Size="9pt" Font-Names="Tahoma" CellSpacing="1" 
                        onpageindexchanging="newsGrid_PageIndexChanging">
                        <AlternatingRowStyle Font-Names="Tahoma" HorizontalAlign="Center" VerticalAlign="Middle"
                            BackColor="#F0F7FF"></AlternatingRowStyle>
                        <RowStyle Font-Names="Tahoma" HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#F0F7FF" />
                        
                        <HeaderStyle Font-Size="9pt" Font-Names="Tahoma" HorizontalAlign="Center" ForeColor="White"
                            CssClass="forumHeaderBackgroundAlternate" BackColor="#0D66BA"></HeaderStyle>
                        <Columns>
                            <asp:BoundField DataField="PicAddress" HtmlEncode="false" HeaderText="وضعیت خبر">
                                <HeaderStyle Width="30px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Title" ItemStyle-Width="100%" HtmlEncode="false">
                                <HeaderStyle HorizontalAlign="Right" Width="1000%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" BackColor="#E6F1FF"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SmallPicAddress" HtmlEncode="false"  >
                                <HeaderStyle Width="90px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                        <PagerStyle HorizontalAlign="Center" ForeColor="Blue"  Wrap="False"></PagerStyle>
                        <asp:PagerSettings Position="TopAndBottom" Mode="NumericFirstLast" />
                    </asp:GridView></div>
                 
            </td>
        </tr>
        <tr>
            <td  align="right" width="100%">
                <asp:HyperLink ID="lnkAdd" Text="ایجاد مورد جدید" runat="server" 
                    NavigateUrl="~/AdminCP/NewsAdmin/NewsAdmin.aspx?act=new"></asp:HyperLink>
            </td>
        </tr>
    </table>
</asp:Content>
