<%@ Page Title="" Language="C#" MasterPageFile="~/WebApplication/Site.master" AutoEventWireup="true" Inherits="UaFootball.WebApplication.RefereeList" Codebehind="RefereeList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<asp:DataGrid ID="dgData" runat="server" AutoGenerateColumns="false" CellPadding="5" CssClass="data">
    <Columns>
        <asp:TemplateColumn HeaderText="Имя">
            <ItemTemplate>
                <asp:LinkButton ID="btnEdit" runat="server" CommandArgument='<%#Eval("Referee_Id")%>' OnCommand="EditObject" Text='<%#string.Concat(Eval("LastName"), ", ", Eval("FirstName"))%>'></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateColumn>
        
        <asp:BoundColumn DataField="CountryName" HeaderText="Страна"></asp:BoundColumn>

        <asp:TemplateColumn>
            <ItemTemplate>
                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/WebApplication/images/delete.gif" CommandArgument='<%#Bind("Referee_Id")%>' OnCommand="DeleteObject" />
            </ItemTemplate>
        </asp:TemplateColumn>
    </Columns>
</asp:DataGrid>
<br />
<asp:Button ID="Button1" runat="server" OnClick="AddObject" Text="Добавить" />

</asp:Content>

