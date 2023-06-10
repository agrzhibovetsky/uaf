<%@ Page Title="" Language="C#" MasterPageFile="~/WebApplication/Site.master" AutoEventWireup="true" CodeBehind="PlayersIncomplete.aspx.cs" Inherits="UaFootball.WebApplication.Admin.PlayersIncomplete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>Игроки с неизвестным местом рождения</div>
    <asp:Repeater ID="rptNoCity" runat="server">
        <ItemTemplate>
            <div style="display:flex">
                <div style="width:200px; padding: 5px;">
                    <a href='PlayerEdit.aspx?objectId=<%#Eval("Player_Id") %>'>
                        <%#Eval("First_Name")%> <%#Eval("Last_Name")%>
                    </a>
                </div>
                <div style="width:200px;">
                    <%#Eval("UARegion_Name")%>
                </div>
                <div><%#UIHelper.FormatDate(Eval("LastUpdate_DT")) %></div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <br />
    <div>Игроки без фото</div>
    <asp:Repeater ID="rptNoPhoto" runat="server">
        <ItemTemplate>
            <div style="display:flex">
                <div style="width:200px; padding: 5px;">
                    <a href='PlayerEdit.aspx?objectId=<%#Eval("Player_Id") %>'>
                        <%#Eval("First_Name")%> <%#Eval("Last_Name")%>
                    </a>
                </div>
                <div><%#UIHelper.FormatDate(Eval("LastUpdate_DT")) %></div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <br />
    <div>Игроки без роста</div>
    <asp:Repeater ID="rptNoHeight" runat="server">
        <ItemTemplate>
            <div style="display:flex">
                <div style="width:200px; padding: 5px;">
                    <a href='PlayerEdit.aspx?objectId=<%#Eval("Player_Id") %>'>
                        <%#Eval("First_Name")%> <%#Eval("Last_Name")%>
                    </a>
                </div>
                <div><%#UIHelper.FormatDate(Eval("LastUpdate_DT")) %></div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
