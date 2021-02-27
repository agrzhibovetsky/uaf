<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MatchList.ascx.cs" Inherits="UaFootball.WebApplication.Controls.MatchList" %>
<%@ Register NameSpace="UaFootball.WebApplication.Controls" Assembly="UaFootball" TagPrefix="UaFootball"%>
<script type="text/javascript">
    $(document).ready(function () {
        $(".trMatchRow").mouseover(function () {
            this.className = "trMatchRowHover"
        });
        $(".trMatchRow").mouseout(function () {
            this.className = "trMatchRow"
        });
        $(".trMatchRow").click(function () {
            $this = $(this);
            var matchId = $this.attr("matchId");
            window.location = "Game.aspx?objectId=" + matchId;
        });
        $

    });
    </script>

<table width="80%" cellpadding="5">
                <tr>
                    <td>
                        <u>Дата</u>
                    </td>
                    <td>
                        <u>Соревнование</u>
                    </td>
                    <td>
                        <u>Стадия</u>
                    </td>
                    <td>
                        <u>Команды</u>
                    </td>
                    <td>
                        <u>Счет</u>
                    </td>
                    <td>
                        <u><asp:Label runat="server" ID="lblExtraColumnName"></asp:Label></u>
                    </td>
                </tr>
                <asp:Repeater ID="rptGames" runat="server" 
                    onitemdatabound="rptGames_ItemDataBound">
                    <ItemTemplate>
                        <tr class="trMatchRow" matchId='<%#Eval("Match_ID")%>'>
                            <td>
                                <%# UIHelper.FormatDate(Eval("Date"))%>
                            </td>
                            <td>
                                <%# Eval("CompetitionName")%>
                            </td>
                            <td>
                                <%#Eval("CompetitionStageName")%>
                            </td>
                            <td>
                                <UaFootball:ClubLabel ID="clHome" runat="server" CountryCode='<%#Eval("HomeTeamCountryCode")%>' Text='<%#Eval("HomeTeamName")%>'/> - 
                                <UaFootball:ClubLabel ID="clAway" runat="server" CountryCode='<%#Eval("AwayTeamCountryCode")%>' Text='<%#Eval("AwayTeamName")%>'/>
                            </td>
                            <td>
                                <%# UIHelper.FormatScore(Eval("HomeScore"), Eval("AwayScore"), Eval("HomePenaltyScore"), Eval("AwayPenaltyScore"))%>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblExtraColumnText"></asp:Label>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>