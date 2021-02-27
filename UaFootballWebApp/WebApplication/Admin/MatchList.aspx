<%@ Page Title="" Language="C#" MasterPageFile="~/WebApplication/Site.master" AutoEventWireup="true" Inherits="UaFootball.WebApplication.MatchList" Codebehind="MatchList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:Button ID="Button1" runat="server" OnClick="AddObject" Text="Новый" />
<br /><br />
<asp:DataGrid ID="dgData" runat="server" AutoGenerateColumns="false" CellPadding="5" CssClass="data">
    <Columns>
         <asp:TemplateColumn HeaderText="Дата">
            <ItemTemplate>
                <%#FormatDate(Eval("Date"))%>
            </ItemTemplate>
         </asp:TemplateColumn>

        <asp:TemplateColumn HeaderText="Команды">
            <ItemTemplate>
                <asp:LinkButton ID="btnEdit" runat="server" CommandArgument='<%#Eval("Match_Id")%>' OnCommand="EditObject" Text='<%#string.Concat(Eval("HomeTeamName"), " -  ", Eval("AwayTeamName"))%>'></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateColumn>
        
        <asp:TemplateColumn HeaderText="Счет">
            <ItemTemplate>
                <asp:LinkButton ID="btnEdit2" runat="server" CommandArgument='<%#Eval("Match_Id")%>' OnCommand="EditObject" Text='<%#FormatScore(Eval("HomeScore"), Eval("AwayScore"), Eval("HomePenaltyScore"), Eval("AwayPenaltyScore"))%>'></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateColumn>
        
        <asp:TemplateColumn>
            <ItemTemplate>
                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/WebApplication/images/delete.gif" CommandArgument='<%#Bind("Match_Id")%>' OnCommand="DeleteObject" />
            </ItemTemplate>
        </asp:TemplateColumn>

        <asp:TemplateColumn>
            <ItemTemplate>
                <a href='../Public/Game.aspx?objectId=<%#Eval("Match_Id")%>'>></a>
            </ItemTemplate>
        </asp:TemplateColumn>
    </Columns>
</asp:DataGrid>
<br />

</asp:Content>

