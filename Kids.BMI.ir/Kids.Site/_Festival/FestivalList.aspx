<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Fa.Master" AutoEventWireup="true"
    CodeBehind="FestivalList.aspx.cs" Theme="Default" Inherits="Site.Kids.bmi.ir._Festival.FestivalList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%;">
        <tr>
            <td runat="server" id="lblCurrentFestival" visible="true">
                <fieldset>
                    <legend>
                        <asp:Label runat="server" ID="lblCurrentFestivalTitle"></asp:Label>
                    </legend>
                    <table style="width: 100%;">
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
                            <td colspan="3" align="center">
                                <asp:DataList ID="lstgalleryDataList" RepeatDirection="Horizontal" RepeatColumns="5"
                                    runat="server" ShowFooter="False" ShowHeader="False">
                                    <ItemTemplate>
                                        <a href='<%# Eval("PicAddress","/AdminCP/Files/Festival/{0}")%>' rel="prettyPhoto[pp_gal]"
                                            title='<%# Eval("PicDescription")%>'>
                                            <img src='<%# Eval("PicAddress","/AdminCP/Files/Festival/{0}")%>' width="120" alt='<%# Eval("PicDescription") %>' />
                                        </a>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: right">
                                برای انتخاب عکس مورد علاقه خود بر روی آن کلیک کنید
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <fieldset>
                                    <legend>عکسهای منتخب شما </legend>
                                    <table style="width: 100%;">
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
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Image ID="Image5" runat="server" />
                                            </td>
                                            <td>
                                                <asp:Image ID="Image4" runat="server" />
                                            </td>
                                            <td>
                                                <asp:Image ID="Image3" runat="server" />
                                            </td>
                                            <td>
                                                <asp:Image ID="Image2" runat="server" />
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
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DataList ID="lstFestivalList" RepeatDirection="Horizontal" RepeatColumns="5"
                    runat="server" ShowFooter="False" ShowHeader="False">
                    <ItemTemplate>
                        <a href='<%# Eval("FestivalId","FestivalShow.aspx?id={0}")%>' rel="prettyPhoto[pp_gal]"
                            title='<%# Eval("Description")%>'>
                            <img src='<%# Eval("FestivalThumbPic","/AdminCP/Files/Festival/{0}")%>' width="120" alt='<%# Eval("Description") %>' />
                            <%# Eval("Name")%>'
                        </a>
                    </ItemTemplate>
                </asp:DataList>
            </td>
        </tr>
    </table>
    <script type="text/javascript" charset="utf-8">
        $(document).ready(function () {
            $("a[rel^='prettyPhoto']").prettyPhoto({ theme: 'facebook', slideshow: 5000, autoplay_slideshow: true });
        });
    </script>
</asp:Content>
