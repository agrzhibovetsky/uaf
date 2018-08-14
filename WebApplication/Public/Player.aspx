<%@ Page Title="" Language="C#" MasterPageFile="~/WebApplication/Site.master" AutoEventWireup="true" CodeBehind="Player.aspx.cs" Inherits="UaFootball.WebApplication.Player" %>
<%@ Register TagPrefix="UaFootball" TagName="MatchEvent" Src="~/WebApplication/Controls/MatchEvent.ascx" %>
<%@ Register TagPrefix="UaFootball" TagName="MatchLogPlayer" Src="~/WebApplication/Controls/MatchLog_Player.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="Stylesheet" type="text/css" href='<%=JQueryUICssPath%>' />
    <link rel="Stylesheet" href="../Styles/colorbox.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<table width="100%">
    <tr>
        <td>
            <%= FormatName(DataItem.First_Name, DataItem.Last_Name, DataItem.Display_Name)%> (<%=DataItem.Country_Name %>)
        </td>
    </tr>
    <tr>
        <td>
            <table>
                <tr>
                    <td>
                        <asp:Image ID="iPlayerLogo" runat="server" />
                    </td>
                    <td>
                        <table style="margin-left : 15px">
                            <tr>
                                <td width="150">
                                    <b>Дата рождения:</b>
                                </td>
                                <td>
                                    <%= FormatDate(DataItem.DOB) %>
                                </td>
                            </tr>
                            
                            <tr>
                                <td>
                                    <b>Город:</b>
                                </td>
                                <td>
                                    <%=FormatUACity(DataItem.UACity, DataItem.UARegion) %>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <b>Рост:</b>
                                </td>
                                <td>
                                    <%= DataItem.Height %>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <b>Вес:</b>
                                </td>
                                <td>
                                    <%= DataItem.Weight %>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="2">
                                    <br />
                                   <a id="hlPhoto" class="colorbox_selector" href='/UaFootball/WebApplication/Public/Photo.aspx?PlayerId=<%= DataItem.Player_Id%>'>Фото (<%=PhotoCount%>)</a> 
                                </td>
                            </tr>

                            <tr>
                                <td colspan="2">
                                    <br />
                                   <a id="hlVideo" class="" href='/UaFootball/WebApplication/Public/Videos.aspx?PlayerId=<%= DataItem.Player_Id%>'>Видео (<%=VideoCount%>)</a> 
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center">
            &nbsp;
        </td>
    </tr>

    <tr>
        <td>
            <div id="tabs">
                <ul>
                    <li><a href="#tabs-1">Последние 20 матчей</a></li>
                    <li><a href="#tabs-2">Игры за сборную (<%=NationalTeamMatchesCount%>)</a></li>
                    <li><a href="#tabs-3">Игры в еврокубках (<%=EuropcupsMatchesCount%>)</a></li>
                </ul>
                <div id="tabs-1">
                    <table cellpadding="4" width="100%">
                        <Uafootball:MatchLogPlayer id="mlpLast20" HideSeasonSeparator="true" runat="server"></Uafootball:MatchLogPlayer>
                    </table>
                </div>
                <div id="tabs-2">
                    <table cellpadding="4" width="100%">
                        <Uafootball:MatchLogPlayer id="mlpSBU" runat="server"></Uafootball:MatchLogPlayer>
                    </table>
                </div>
                <div id="tabs-3">
                    <table cellpadding="4" width="100%">
                        <Uafootball:MatchLogPlayer id="mlpClub" runat="server" HideHeader="true"></Uafootball:MatchLogPlayer>
                    </table>
                </div>
            </div>
        </td>
    </tr>

    <tr>
        <td>
            
        </td>
    </tr>
    
    <tr>
        <td>
            
        </td>
    </tr>
    <tr>
        <td onclick='document.location.href = document.location.href.replace("Public/Player", "Admin/PlayerEdit").replace("playerId","objectId")' style="height:10px; background-color:#FAFAFA;"  >
        </td>
    </tr>
</table>

<script type="text/javascript">
    $(document).ready(function () {
        $("#tabs").tabs();
        $(".colorbox_selector").colorbox({ width: "900px", height: "800px" });
    });
</script>
</asp:Content>
