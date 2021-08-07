<%@ Page Title="" Language="C#" MasterPageFile="~/WebApplication/Site.master" AutoEventWireup="true" CodeBehind="UkraineRegions.aspx.cs" Inherits="UaFootball.WebApplication.Public.UkraineRegions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Футбольная карта Украины</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div id="raphaelContainer">
        </div>
  
    <div id="regionContainer" />

    <div id="playersTableContainer" style="display:none;">
   
    <asp:UpdatePanel ID="upRegionPlayers" runat="server">
        <ContentTemplate>
            <asp:HiddenField id="hdnRegionName" runat="server" ClientIDMode="Static" />
            <asp:Button ID="btnActivateRegion" runat="server" ClientIDMode="Static" style="display:none;"  OnClick="btnActivateRegion_Click" />
            <table cellpadding="4">
                <tr>
                    <td colspan="3" style="text-align:center;">
                        <asp:Literal ID="ltRegionName" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <tr>
                        <td style="width:50px;">#</td>
                        <td style="width:200px;">Футболист</td>
                        <td>Город</td>
                    </tr>
                </tr>
            <asp:Repeater ID="rptPlayers" runat="server">
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# Container.ItemIndex+1 %>
                        </td>
                        <td>
                            <a href="Player.aspx?playerId=<%#Eval("Player_Id") %>">
                                <%# FormatName(Eval("First_Name"), Eval("Last_Name"), Eval("Display_Name"), 1) %>
                            </a>
                        </td>
                        <td>
                            <%#Eval("UACity_Name") %>
                        </td>
                    </tr>
                    
                </ItemTemplate>
            </asp:Repeater>
            </table>
        </ContentTemplate>
        
    </asp:UpdatePanel>
    </div>
    <script type="text/javascript" src="../Scripts/raphael-2.3.0/raphael.js"></script>
    <script type="text/javascript" src="../Scripts/ukraine-map.js"></script>
</asp:Content>
