<%@ Page Title="" Language="C#" MasterPageFile="~/WebApplication/Site.master" AutoEventWireup="true" CodeBehind="Game.aspx.cs" Inherits="UaFootball.WebApplication.Game" %>
<%@ Register TagPrefix="UaFootball" TagName="MatchEvent" Src="~/WebApplication/Controls/MatchEvent.ascx" %>
<%@ Register TagPrefix="UaFootball" TagName="MatchNote" Src="~/WebApplication/Controls/MatchNotes.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="Stylesheet" href="../Styles/colorbox.css" />
    <title><%=DataItem.HomeTeamName%> - <%=DataItem.AwayTeamName%> (<%=FormatDate(DataItem.Date)%>)</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<table width="100%">
    <tr>
        <td align="center">
            <table width="80%">
                <tr>
                    <td style="border-bottom: 1px solid black;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <%=DataItem.CompetitionName%><%=string.IsNullOrEmpty(DataItem.CompetitionStageName) ? "" : ". " +  DataItem.CompetitionStageName %>. 
                        <asp:Label ID="lblSpecialNotes" runat="server" CssClass="matchSpecialNote"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%">
                            <tr>
                                <td width="33%" align="center" class="matchReportClubName">
                                    <%=DataItem.HomeTeamName%> 
                                    <UaFootball:MatchNote id="mnHomeLineup" runat="server" NoteTypeCode="genHmeLnup"></UaFootball:MatchNote>
                                </td>
                                <td width="33%" align="center" class="matchReportClubName"><%=FormatScore(DataItem.HomeScore, DataItem.AwayScore, DataItem.HomePenaltyScore, DataItem.AwayPenaltyScore)%></td>
                                <td width="33%" align="center" class="matchReportClubName">
                                    <%=DataItem.AwayTeamName%>
                                    <UaFootball:MatchNote id="mnAwayLineup" runat="server" NoteTypeCode="genAwyLnup"></UaFootball:MatchNote>
                                </td>
                            </tr>
                            <tr>
                                <td  align="center">
                                    <a href="<%=GetTeamUrl(DataItem.HomeClub_Id, DataItem.HomeNationalTeam_Id)%>">
                                        <asp:Image ID="iHomeTeamLogo" runat="server" />
                                    </a>
                                </td>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                Дата
                                            </td>
                                            <td>
                                                <%=FormatDate(DataItem.Date)%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Город
                                            </td>
                                            <td>
                                                <%=DataItem.Stadium.City_Name%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Стадион
                                            </td>
                                            <td>
                                                <a href="Stadium.aspx?objectId=<%=DataItem.Stadium.Stadium_ID%>">
                                                <%=DataItem.Stadium.Stadium_Name%>
                                                </a>
                                                <asp:Label ID="lblNeutralField" runat="server" Text="***" ToolTip="Нейтральное поле" ForeColor="Red" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Зрители
                                                
                                            </td>
                                            <td>
                                                <%=FormatInt(DataItem.Spectators) %>
                                                <UaFootball:MatchNote id="mnSpect" runat="server" NoteTypeCode="genSpect"></UaFootball:MatchNote>
                                                
                                                <asp:Label ID="lblStadiumDisq" runat="server" Text="Без зрителей" Visible="false"></asp:Label><UaFootball:MatchNote id="mnNoSpect" runat="server" NoteTypeCode="noSpect"></UaFootball:MatchNote>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Судья
                                            </td>
                                            <td>
                                                <a href="Referee.aspx?objectId=<%=DataItem.Referee.Referee_Id%>">
                                                <%= FormatName(DataItem.Referee.FirstName, DataItem.Referee.LastName, null, DataItem.Referee.Country_Id) %>
                                                </a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="2"><asp:HyperLink ID="hlPhoto" runat="server" ClientIDMode="Static"></asp:HyperLink>&nbsp;
                                                            <asp:HyperLink ID="hlVideo" runat="server"></asp:HyperLink></td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="center">
                                    <a href="<%=GetTeamUrl(DataItem.AwayClub_Id, DataItem.AwayNationalTeam_Id)%>">
                                        <asp:Image ID="iAwayTeamLogo" runat="server" />
                                    </a>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="border-bottom: 1px solid black;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%" cellpadding="5">
                            <tr>
                                <td colspan="2" align="center"></td>
                                <td colspan="2" align="center"></td>
                            </tr>
                            <asp:Repeater ID="rptLineup" runat="server" 
                                onitemdatabound="rptLineup_ItemDataBound">
                                <ItemTemplate>
                                    <tr id="trPlayerRow" runat="server">
                                        <td width="5%" valign="top">
                                            <asp:Label ID="lblHPlayerNum" runat="server"></asp:Label>
                                        </td>
                                        <td width="45%" valign="top">
                                            <asp:Panel ID="pHomePlayer" runat="server">
                                                <asp:HyperLink ID="aHomePlayer" runat="server" CssClass="default"></asp:HyperLink>
                                            </asp:Panel>
                                            <asp:Panel ID="pHomePlayerSubst" runat="server" Visible="false">
                                                <asp:HyperLink ID="aHomePlayerSubst" runat="server" CssClass="default"></asp:HyperLink>
                                            </asp:Panel>
                                        </td>
                                        <td width="5%" valign="top">
                                            <asp:Label ID="lblAPlayerNum" runat="server"></asp:Label>
                                        </td>
                                        <td width="45%" valign="top">
                                            <asp:Panel ID="pAwayPlayer" runat="server">
                                                <asp:HyperLink ID="aAwayPlayer" runat="server" CssClass="default"></asp:HyperLink>
                                            </asp:Panel>
                                            <asp:Panel ID="pAwayPlayerSubst" runat="server" Visible="false">
                                                <asp:HyperLink ID="aAwayPlayerSubst" runat="server" CssClass="default"></asp:HyperLink>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr>
                                <td colspan="2">
                                    <br />Тренер: 
                                    <asp:HyperLink ID="hlHomeTeamCoach" runat="server"></asp:HyperLink>
                                    <asp:Literal ID="ltHomeCoachInCharge" runat="server" Visible="false"> (и.о.)</asp:Literal>
                                    <UaFootball:MatchNote id="mnHomeCoach" runat="server" NoteTypeCode="homeCoach"></UaFootball:MatchNote>
                                </td>
                                <td colspan="2">
                                    <br />Тренер: 
                                    <asp:HyperLink ID="hlAwayTeamCoach" runat="server"></asp:HyperLink>
                                    <asp:Literal ID="ltAwayCoachInCharge" runat="server" Visible="false"> (и.о.)</asp:Literal>
                                    <UaFootball:MatchNote id="mnAwayCoach" runat="server" NoteTypeCode="awayCoach"></UaFootball:MatchNote>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="border-bottom: 1px solid black;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%">
                            <asp:Repeater ID="rptEvents" runat="server" OnItemDataBound="rptEvents_ItemDataBound">
                                <ItemTemplate>
                                    <tr>
                                        <td width="5%" valign="top">
                                            <UaFootball:MatchEvent ID="meHome" runat="server" Visible="false" />
                                        </td>
                                    
                                        <td width="45%" valign="top">
                                            <asp:Label ID="lblHomeEvent" runat="server"></asp:Label>
                                        </td>
                                    
                                        <td width="5%" valign="top">
                                            <UaFootball:MatchEvent ID="meAway" runat="server" Visible="false" />
                                        </td>
                                    
                                        <td width="45%" valign="top">
                                            <asp:Label ID="lblAwayEvent" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td onclick='window.open(document.location.href.replace("Public/Game", "Admin/MatchEdit"))' style="height:5px; background-color:#EEEEEE;"  >
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Repeater ID="rptNotes" runat="server">
                            <ItemTemplate>
                                <div>
                                    <span id="matchNote_<%#Eval("RowIndex")%>">
                                        <sup>[<%#Eval("RowIndex")%>]</sup>
                                    </span>
                                    <span>
                                        <%#Eval("Text")%>
                                    </span>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<script type="text/javascript">
    $(document).ready(function () {
        $("#hlPhoto").colorbox({ width: "900px", height: "800px" });
    });
</script>
</asp:Content>
