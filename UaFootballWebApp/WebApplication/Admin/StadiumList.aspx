﻿<%@ Page Title="" Language="C#" MasterPageFile="~/WebApplication/Site.master" AutoEventWireup="true" Inherits="UaFootball.WebApplication.StadiumList" Codebehind="StadiumList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Button ID="Button1" runat="server" OnClick="AddObject" Text="Добавить" />
<asp:DataGrid ID="dgData" runat="server" AutoGenerateColumns="false" CellPadding="5" CssClass="data">
    <Columns>
        <asp:TemplateColumn HeaderText="Название">
            <ItemTemplate>
                <asp:LinkButton ID="btnEdit" runat="server" CommandArgument='<%#Eval("Stadium_ID")%>' OnCommand="EditObject" Text='<%#Eval("Stadium_Name")%>'></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateColumn>
        
        <asp:BoundColumn DataField="Country_Name" HeaderText="Страна"></asp:BoundColumn>

        <asp:BoundColumn DataField="City_Name" HeaderText="Город"></asp:BoundColumn>

        <asp:BoundColumn DataField="Capacity" HeaderText="Вместимость"></asp:BoundColumn>

        <asp:BoundColumn DataField="YearBuilt" HeaderText="Год постройки"></asp:BoundColumn>
        
        <asp:TemplateColumn>
            <ItemTemplate>
                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/WebApplication/images/delete.gif" CommandArgument='<%#Bind("Stadium_ID")%>' OnCommand="DeleteObject" />
            </ItemTemplate>
        </asp:TemplateColumn>
    </Columns>
</asp:DataGrid>
<br />


</asp:Content>

