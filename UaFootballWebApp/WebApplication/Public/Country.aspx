<%@ Page Title="" Language="C#" MasterPageFile="~/WebApplication/Site.master" AutoEventWireup="false" CodeBehind="Country.aspx.cs" Inherits="UaFootball.WebApplication.Public.Country" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<ajaxToolkit:Accordion runat="server" ID="accCountry" RequireOpenedPane="false" SelectedIndex="-1" CssClass="accordion"
    HeaderCssClass="accordionHeader"
    HeaderSelectedCssClass="accordionHeaderSelected"
    ContentCssClass="accordionContent" >
    <Panes>
        <ajaxToolkit:AccordionPane runat="server" ID="apMatches">
            <Header>Матчи сборных</Header>
            <Content>
                <asp:Repeater ID="rptMatches" runat="server">
                    <ItemTemplate>
                        <a href='/UaFootball/WebApplication/Public/Game.aspx?objectId=<%#Eval("Match_Id")%>'>Game</a><br />
                    </ItemTemplate>
                </asp:Repeater>
            </Content>
        </ajaxToolkit:AccordionPane>

        <ajaxToolkit:AccordionPane ID="apClubs" runat="server">
            <Header>Клубы</Header>
            <Content>
                <asp:Repeater ID="rptClubs" runat="server">
                    <ItemTemplate>
                        <%#Eval("Club_Name") %><br />
                    </ItemTemplate>
                </asp:Repeater>
            </Content>
        </ajaxToolkit:AccordionPane>

        <ajaxToolkit:AccordionPane ID="apPlayers" runat="server">
            <Header>Игроки</Header>
            <Content>
                <asp:Repeater ID="rptPlayers" runat="server">
                    <ItemTemplate>
                        <a href='/UaFootball/WebApplication/Public/Player.aspx?playerId=<%#Eval("Player_Id")%>'><%#Eval("Last_Name")%></a><br />
                    </ItemTemplate>
                </asp:Repeater>
            </Content>
        </ajaxToolkit:AccordionPane>

        <ajaxToolkit:AccordionPane ID="AccordionPane3" runat="server" >
            <Header>Судьи</Header>
            <Content></Content>
        </ajaxToolkit:AccordionPane>

        <ajaxToolkit:AccordionPane ID="AccordionPane4" runat="server">
            <Header>Стадионы</Header>
            <Content></Content>
        </ajaxToolkit:AccordionPane>
    </Panes>
</ajaxToolkit:Accordion>

</asp:Content>
