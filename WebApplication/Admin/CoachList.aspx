<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/WebApplication/Site.master"  CodeBehind="CoachList.aspx.cs" Inherits="UaFootball.WebApplication.CoachList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<asp:DataGrid ID="dgData" runat="server" AutoGenerateColumns="false" CellPadding="5" CssClass="data">
    <Columns>
        <asp:TemplateColumn HeaderText="Имя">
            <ItemTemplate>
                <asp:LinkButton ID="btnEdit" runat="server" CommandArgument='<%#Eval("CoachId")%>' OnCommand="EditObject" Text='<%#string.Concat(Eval("LastName"), ", ", Eval("FirstName"))%>'></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateColumn>
        
        <asp:BoundColumn DataField="CountryName" HeaderText="Страна"></asp:BoundColumn>

        <asp:TemplateColumn>
            <ItemTemplate>
                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/WebApplication/images/delete.gif" CommandArgument='<%#Bind("CoachId")%>' OnCommand="DeleteObject" />
            </ItemTemplate>
        </asp:TemplateColumn>
    </Columns>
</asp:DataGrid>
<br />
<asp:Button ID="Button1" runat="server" OnClick="AddObject" Text="Добавить" />

</asp:Content>

