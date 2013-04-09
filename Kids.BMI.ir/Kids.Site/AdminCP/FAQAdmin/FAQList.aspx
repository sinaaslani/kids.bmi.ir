<%@ Page Language="c#" Theme="Admin"   MasterPageFile="~/Masters/Admin.Master" AutoEventWireup="True" Codebehind="FAQList.aspx.cs" Inherits="Site.Kids.bmi.ir.AdminCP.FAQAdmin.FAQList"  %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<table width="100%">
	<tr>
		<td  style="HEIGHT: 26px" align="right" width="100%" colSpan="2" >
			<IMG src="/App_Themes/Admin/Images/admin_bullet.gif">&nbsp; &nbsp;فهرست&nbsp;پرسش 
				های متداول&nbsp;&nbsp;
		</td>
	</tr>
	<tr>
		<td >
		</td>
	</tr>
	<tr>
		<td  style="HEIGHT: 26px" align="right" width="100%" colSpan="2" >
			<DIV align="center">
                <asp:GridView id="ListGrid" runat="server" Height="30px" 
                    Width="100%" CellSpacing="1" Font-Names="Tahoma"
					Font-Size="9pt" ForeColor="Black" GridLines="None" CellPadding="2" 
                    AutoGenerateColumns="False" AllowPaging="True"
					PageSize="30" onpageindexchanging="ListGrid_PageIndexChanging">
					<AlternatingRowStyle Font-Names="Tahoma"  HorizontalAlign="Center" VerticalAlign="Middle"
						BackColor="#F0F7FF" />
					<RowStyle Font-Names="Tahoma" HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#F0F7FF" />
					<HeaderStyle Font-Size="9pt" Font-Names="Tahoma" HorizontalAlign="Center" ForeColor="White" CssClass="forumHeaderBackgroundAlternate"
						BackColor="#0D66BA"></HeaderStyle>
					<Columns>
						<asp:BoundField DataField="Title" HeaderText="عنوان">
							<HeaderStyle HorizontalAlign="Right" Width="300px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right" VerticalAlign="Top" BackColor="#E6F1FF"></ItemStyle>
						</asp:BoundField>
						<asp:HyperLinkField Text="ویرایش" DataNavigateUrlFields="FAQId" 
                            DataNavigateUrlFormatString="~/AdminCP/FAQAdmin/FAQAdmin.aspx?act=edit&amp;pid={0}" >
						<ItemStyle Width="30px" />
                        </asp:HyperLinkField>
						<asp:HyperLinkField Text="حذف" DataNavigateUrlFields="FAQId" 
                            DataNavigateUrlFormatString="~/AdminCP/FAQAdmin/FAQAdmin.aspx?act=del&amp;pid={0}" >
					    <ItemStyle Width="30px" />
                        </asp:HyperLinkField>
					</Columns>
					<PagerStyle HorizontalAlign="Center" ForeColor="Blue"  Wrap="False" ></PagerStyle>
                    <PagerSettings Position="TopAndBottom" Mode="NextPreviousFirstLast"/>
				</asp:GridView></DIV>
		</td>
	</tr>
	<tr>
		<td >
			<asp:HyperLink id="AddNewLnk" runat="server"  NavigateUrl="~/AdminCP/FAQAdmin/FAQAdmin.aspx?act=new"> ایجاد پرسش جدید</asp:HyperLink>
		</td>
	</tr>
</table>
</asp:Content>