<%@ Page Title="آزمونهای سایت" Theme="Default" Language="C#" MasterPageFile="~/Masters/Fa.Master"
    AutoEventWireup="true" CodeBehind="BankStoryExamList.aspx.cs" Inherits="Site.Kids.bmi.ir.BankStory.BankStoryExamList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>لیست آزمونها</legend>
        <asp:GridView ID="dgExamList" runat="server" AutoGenerateColumns="False" 
            onrowdatabound="dgExamList_RowDataBound">
            <Columns>
                <asp:BoundField DataField="ExamName" HeaderText="نام آزمون" />
                <asp:BoundField DataField="Description" HeaderText="شرح آزمون" />
                <asp:BoundField DataField="DurationTime" HeaderText="مدت زمان(دقیقه)" />
                <asp:TemplateField HeaderText="تعداد سوالات">
                   
                    <ItemTemplate>
                        <asp:Label ID="lblExamQuestionCount" runat="server" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:HyperLinkField DataNavigateUrlFields="ExamId" DataNavigateUrlFormatString="~/AttendExam.aspx?exid={0}"
                    Text="شرکت در آزمون" />
            </Columns>
        </asp:GridView>
    </fieldset>
</asp:Content>
