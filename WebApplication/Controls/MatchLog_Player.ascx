<%@ Control Language="C#" CodeBehind="MatchLog_Player.ascx.cs" Inherits="UaFootball.WebApplication.Controls.MatchLog_Player" %>
<%@ Register TagPrefix="UaFootball" TagName="MatchEvent" Src="~/WebApplication/Controls/MatchEvent.ascx" %>
<asp:Repeater ID="rptMatchLog" runat="server"  onitemdatabound="rptMatches_ItemDataBound">
    <HeaderTemplate>
        <tr>
            <td>Дата</td>
            <td>Матч</td>
            <td align="center">Номер</td>
            <td align="center">Минуты</td>
            <td>События</td>
            <td>Фото</td>
        </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr class="trMatchRow" matchId='<%#Eval("Match_ID")%>' id="matchRow" runat="server">
            <td width="12%"><%# UIHelper.FormatDate(Eval("Date"))%></td>
            <td width="38%"><%#Eval("HomeTeamName")%> - <%#Eval("AwayTeamName")%> <%#UIHelper.FormatScore(Eval("HomeScore"),Eval("AwayScore"),Eval("HomePenaltyScore"),Eval("AwayPenaltyScore")) %></td>
            <td width="11%"  align="center"><%#Eval("Lineup[0].ShirtNum")%></td>
            <td width="13%" align="center"><asp:Label ID="lblMinute" runat="server"></asp:Label></td>
            <td width="15%">
                <asp:Repeater ID="rptEvents" runat="server">
                    <ItemTemplate>
                        <UaFootball:MatchEvent ID="me" runat="server" EventType_CD='<%#Eval("Event_Cd")%>' Minute='<%#Eval("Minute")%>' EventFlags='<%#Eval("EventFlags")%>' AppliesToSecondPlayer='<%#Eval("AppliesToSecondPlayer")%>' HasVideo='<%#Eval("HasVideo")%>' EventId='<%#Eval("MatchEvent_Id")%>'></UaFootball:MatchEvent>
                    </ItemTemplate>
                </asp:Repeater>
            </td>
            <td width="11%">
                <asp:HyperLink Id="hlPhoto" onclick="open_colorbox(this, event); return false;"  runat="server" ImageUrl="~/WebApplication/Images/photo.gif"></asp:HyperLink> &nbsp;<asp:Image Id="iVideo" runat="server" />
            </td>
        </tr>
    </ItemTemplate>
</asp:Repeater>