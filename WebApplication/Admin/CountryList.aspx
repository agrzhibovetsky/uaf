<%@ Page Title="" Language="C#" MasterPageFile="~/WebApplication/Site.master" AutoEventWireup="true" Inherits="UaFootball.WebApplication.CountryList" Codebehind="CountryList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:DataGrid ID="dgData" runat="server" AutoGenerateColumns="false" CellPadding="5" CssClass="data">
    <Columns>
        <asp:TemplateColumn HeaderText="Название">
            <ItemTemplate>
                <asp:LinkButton ID="btnEdit" runat="server" CommandArgument='<%#Eval("Country_ID")%>' OnCommand="EditObject" Text='<%#Eval("Country_Name")%>'></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateColumn>
        
        <asp:BoundColumn DataField="FIFAAssociation_Name" HeaderText="Конфедерация">
        </asp:BoundColumn>
        
        <asp:TemplateColumn>
            <ItemTemplate>
                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/WebApplication/images/delete.gif" CommandArgument='<%#Bind("Country_ID")%>' OnCommand="DeleteObject" />
            </ItemTemplate>
        </asp:TemplateColumn>
    </Columns>
</asp:DataGrid>
<br />
<asp:Button ID="btnNewCountry" runat="server" OnClick="AddObject" Text="Добавить" />

</asp:Content>

