<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="ucScoreTypeAdminPanel.ascx.cs"
    Inherits="Site.Kids.bmi.ir.AdminCP.HomeWidget.ucScoreTypeAdminPanel" %>
<table width="100%" style="width: 100%; height: 199px">
    <tr>
        <th  align="right" height="25" class="TableHeaderText" style="filter: progid:DXImageTransform.Microsoft.Gradient(startColorStr=#FFFFFF, endColorStr=#0865BD, gradientType=1);
            font-family: Tahoma,Verdana,Arial,Helvetica" width="100%">
           
                <img src="/App_Themes/Admin/Images/admin_bullet.gif" alt="">&nbsp;مدیریت انواع امتیازات
        </th>
    </tr>
    <tr>
        <td  align="right" width="100%">
            <asp:HyperLink ID="AddNewsLnk" runat="server"  NavigateUrl="~/AdminCP/ScoreAdmin/ScoreTypeAdmin.aspx?act=new">تعریف یک نوع امتیاز جدید</asp:HyperLink>
        </td>
    </tr>
    <tr>
        <td  align="right" width="100%" valign="top">
            ثبت نوع امتیاز جدید
        </td>
    </tr>
    <tr>
        <td  align="right" width="100%">
            <asp:HyperLink ID="EditNewsLnk" runat="server"  NavigateUrl="~/AdminCP/ScoreAdmin/ScoreTypeList.aspx">ویرایش و حذف انواع امتیازها</asp:HyperLink>
        </td>
    </tr>
    <tr>
        <td  align="right" style="height: 21px" width="100%" valign="top">
            فهرست&nbsp;انواع امتیازها&nbsp;
        </td>
    </tr>
    <tr>
        <td align="right"  width="100%">
            <asp:HyperLink ID="CreateNewsGroupLnk" runat="server"  NavigateUrl="~/AdminCP/ScoreAdmin/ScoreTypeCeategoryAdmin.aspx?act=new">ایجاد گروه نوعهای امتیاز</asp:HyperLink>
        </td>
    </tr>
    <tr>
        <td align="right"  width="100%" valign="top">
            ایجاد گروه&nbsp;برای&nbsp;دسته بندی انواع امتیازها
        </td>
    </tr>
    <tr>
        <td align="right"  width="100%">
            <asp:HyperLink ID="EditNewsCatLnk" runat="server"  NavigateUrl="~/AdminCP/ScoreAdmin/ScoreTypeCategoryList.aspx">ویرایش و حذف گروه های انواع امتیازها</asp:HyperLink>
        </td>
    </tr>
    <tr>
        <td align="right"  width="100%" valign="top">
            ویرایش&nbsp;و حذف گروه های انواع امتیازهای موجود
        </td>
    </tr>
</table>
