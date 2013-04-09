<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.Master" ValidateRequest="false"
    AutoEventWireup="True" CodeBehind="GameAdmin.aspx.cs" Inherits="Site.Kids.bmi.ir.AdminCP.GameAdmin.GameAdmin"
    Theme="Admin" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%" dir="ltr">
        <tr>
            <td align="right" colspan="2">
                <img src="/App_Themes/Admin/Images/admin_bullet.gif" alt="">
                <asp:Literal ID="lblHead" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td width="170" align="right">
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:TextBox ID="txtTitle" runat="server" Height="31px" Width="390px"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle"
                    ErrorMessage="خطا : عنوان خبر الزامی است" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
            <td align="right">
              :  عنوان بازی 
            </td>
        </tr>
        <tr>
            <td align="right">
                <table>
                    <tr>
                        <td align="right">
                            <img src="/App_Themes/Admin/Images/admin_bullet.gif" alt="">
                            موضوعات&nbsp;مورد نظر را تیک بزنید
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="ScoreTypeGrid" Width="390px" PageSize="20" GridLines="None" CellSpacing="1"
                                Font-Size="9pt" Font-Names="Tahoma" runat="server" AutoGenerateColumns="False"
                                CellPadding="2" DataKeyNames="Id">
                                <AlternatingRowStyle BackColor="#E6F1FF"></AlternatingRowStyle>
                                <RowStyle BackColor="#E6F1FF" />
                                <HeaderStyle ForeColor="White" BackColor="#0D66BA"></HeaderStyle>
                                <Columns>
                                    <asp:BoundField DataField="ScoreFaName" HeaderText="نام امتیاز">
                                        <HeaderStyle HorizontalAlign="Right" Width="100%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <HeaderStyle Width="10px"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server"></asp:CheckBox>
                                            <asp:HiddenField ID="hdnId" Value='<%#Eval("Id") %>' runat="server"></asp:HiddenField>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Label ID="lblSubjectValidator" runat="server" Font-Names="Tahoma" Font-Size="X-Small"
                                Visible="False" ForeColor="Red">انتخاب موضوع الزامی می باشد</asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top" align="right">
               : نوع امتیاز بازی 
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:TextBox ID="summaryCtrl" runat="server" Height="57px" TextMode="MultiLine" Width="390px"></asp:TextBox>
            </td>
            <td valign="top" align="right">
                : توضیحات
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:DropDownList ID="drpRequiredUserState" runat="server">
                </asp:DropDownList>
            </td>
            <td valign="top" height="38" align="right">
                 :حداقل سطح کاربری مورد نیاز 
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:DropDownList ID="drpGameType" runat="server">
                    <asp:ListItem Selected="True" Value="-----"></asp:ListItem>
                    <asp:ListItem Value="False">بازی فلش</asp:ListItem>
                    <asp:ListItem Value="True">بازی اچ تی ام ال در سایت</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td valign="top" height="38" align="right">
                 &nbsp;:نوع بازی 
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:FileUpload runat="server" ID="fupGamePicAddress" />&nbsp;
                <asp:LinkButton ID="GamePicDeleteLnk" runat="server" Visible="False" OnCommand="SmallPicDeleteLnk_Command">حذف فایل</asp:LinkButton>
            </td>
            <td align="right">
                 : تصویر بازی 
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:FileUpload runat="server" ID="fupGameFileAddress" type="file" name="PicAddress" />&nbsp;&nbsp;
                <asp:LinkButton ID="GameFileDeleteLnk" runat="server" Visible="False" OnCommand="LargePicDeleteLnk_Command">حذف فایل</asp:LinkButton>
            </td>
            <td align="right">
                 : فایل فلش بازی
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:TextBox ID="txtGameUrl" runat="server" Width="379px"></asp:TextBox>
            </td>
            <td align="right">
                 &nbsp;:آدرس سایت بازی
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnCancel" runat="server"  Font-Names="Tahoma"
                    Text="  انصراف  " OnClick="btnCancel_Click"></asp:Button>
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSave" runat="server"  Font-Names="Tahoma"
                    OnClick="btnSave_Click"></asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>
