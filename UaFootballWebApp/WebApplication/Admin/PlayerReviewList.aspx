<%@ Page Title="" Language="C#" MasterPageFile="~/WebApplication/Site.master" AutoEventWireup="true" CodeBehind="PlayerReviewList.aspx.cs" Inherits="UaFootball.WebApplication.Admin.PlayerReviewList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:DataGrid ID="dgData" runat="server" AutoGenerateColumns="false" CellPadding="5" CssClass="data">
    <Columns>
        <asp:TemplateColumn HeaderText="Имя">
            <ItemTemplate>
                <asp:LinkButton ID="btnEdit" runat="server" CommandArgument='<%#Eval("Player_Id")%>' OnCommand="EditObject" Text='<%#string.Concat(Eval("Last_Name"), ", ", Eval("First_Name"))%>'></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateColumn>
        
        <asp:BoundColumn DataField="Country_Name" HeaderText="Страна"></asp:BoundColumn>

        <asp:TemplateColumn>
            <ItemTemplate>
                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/WebApplication/images/delete.gif" CommandArgument='<%#Bind("Player_Id")%>' OnCommand="DeleteObject" />
            </ItemTemplate>
        </asp:TemplateColumn>
    </Columns>
</asp:DataGrid>
</asp:Content>
