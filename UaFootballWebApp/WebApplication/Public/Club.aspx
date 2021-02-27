<%@ Page Title="" Language="C#" MasterPageFile="~/WebApplication/Site.master" AutoEventWireup="true" CodeBehind="Club.aspx.cs" Inherits="UaFootball.WebApplication.Public.Club" %>
<%@ Register TagPrefix="UaFootball" TagName="MatchList" Src="~/WebApplication/Controls/MatchList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title><%=DataItem.Club_Name%> (<%=DataItem.City_Name%>)</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div style="width:90%; display:flex">
    
    <div style="min-width:200px; min-height: 200px;">
        <asp:Image ID="iClubLogo" runat="server" />
    </div>
    <div>
        <p><b><%=DataItem.Club_Name%></b> (<%=DataItem.City_Name%>)</p>
        <p>Основан: <%=DataItem.Year_Found %> (<%=ClubAge%>)</p>
    </div>
</div>
<div>
    <div id="tabs">
        <ul>
            <li><a href="#tabs-1">Матчи</a></li>
            <li><a href="#tabs-2">Игроки</a></li>
            <li><a href="#tabs-3">Тренеры</a></li>
        </ul>
        <div id="tabs-1">
            <div>
                <UaFootball:MatchList ID="ml" runat="server" />
            </div>
        </div>
        <div id="tabs-2">
            <div>
                <table>
                <asp:Repeater id="rptPlayers" runat="server">
                    <HeaderTemplate>
                        <td>
                            Футболист
                        </td>

                        <td>
                            Матчей (в заявке)
                        </td>

                        <td>
                            Минут
                        </td>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <a href="Player.aspx?playerId=<%# Eval("Player_ID")%>">
                                    <%# FormatName(Eval("First_Name"), Eval("Last_Name"), Eval("Display_Name"), 1)%>
                                </a>
                            </td>

                            <td>
                                <%#Eval("TotalMatches")%> (<%#Eval("TotalLineups")%>)
                            </td>

                            <td>
                                <%#Eval("TotalMinutes")%> 
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                </table>
                <asp:DataGrid ID="dgPlayers" runat="server" AutoGenerateColumns="false">
                    
                    <Columns>
                        <asp:TemplateColumn HeaderText="Футболист">
                            <ItemTemplate>
                                
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Матчей">
                            <ItemTemplate>
                                <%#Eval("TotalMatches")%> (<%#Eval("TotalLineups")%>)
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn  HeaderText="Минут" DataField="TotalMinutes"></asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
            </div>
        </div>
        <div id="tabs-3">
            <div>
                Тренеры 
            </div>
        </div>
    </div>
</div>


    
<script type="text/javascript">
    $(document).ready(function () {
        $("#tabs").tabs();
    });
</script>

</asp:Content>


