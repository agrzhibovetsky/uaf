<%@ Page MasterPageFile="~/WebApplication/Site.master"  Language="C#" AutoEventWireup="true" CodeBehind="Players.aspx.cs" Inherits="UaFootball.WebApplication.Players" %>
<%@ Register TagPrefix="UaFootball" TagName="AutocompleteTextBox" Src="~/WebApplication/Controls/AutocompleteTextBox.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table width="90%">
        <tr>
            <td>
                <asp:LinkButton ID="btnA" runat="server" Text="А" CommandArgument="А" OnCommand="btnLetter_Command"></asp:LinkButton>
                <asp:LinkButton ID="btnB" runat="server" Text="Б" CommandArgument="Б" OnCommand="btnLetter_Command"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton1" runat="server" Text="В" CommandArgument="В" OnCommand="btnLetter_Command"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton2" runat="server" Text="Г" CommandArgument="Г" OnCommand="btnLetter_Command"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton3" runat="server" Text="Д" CommandArgument="Д" OnCommand="btnLetter_Command"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton5" runat="server" Text="Е" CommandArgument="Е" OnCommand="btnLetter_Command"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton4" runat="server" Text="Ё" CommandArgument="Ё" OnCommand="btnLetter_Command"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton6" runat="server" Text="Ж" CommandArgument="Ж" OnCommand="btnLetter_Command"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton7" runat="server" Text="З" CommandArgument="З" OnCommand="btnLetter_Command"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton8" runat="server" Text="И" CommandArgument="И" OnCommand="btnLetter_Command"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton9" runat="server" Text="К" CommandArgument="К" OnCommand="btnLetter_Command"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton10" runat="server" Text="Л" CommandArgument="Л" OnCommand="btnLetter_Command"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton11" runat="server" Text="М" CommandArgument="М" OnCommand="btnLetter_Command"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton12" runat="server" Text="Н" CommandArgument="Н" OnCommand="btnLetter_Command"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton13" runat="server" Text="О" CommandArgument="О" OnCommand="btnLetter_Command"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton14" runat="server" Text="П" CommandArgument="П" OnCommand="btnLetter_Command"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton15" runat="server" Text="Р" CommandArgument="Р" OnCommand="btnLetter_Command"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton16" runat="server" Text="С" CommandArgument="С" OnCommand="btnLetter_Command"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton17" runat="server" Text="Т" CommandArgument="Т" OnCommand="btnLetter_Command"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton18" runat="server" Text="У" CommandArgument="У" OnCommand="btnLetter_Command"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton19" runat="server" Text="Ф" CommandArgument="Ф" OnCommand="btnLetter_Command"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton20" runat="server" Text="Х" CommandArgument="Х" OnCommand="btnLetter_Command"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton21" runat="server" Text="Ц" CommandArgument="Ц" OnCommand="btnLetter_Command"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton22" runat="server" Text="Ч" CommandArgument="Ч" OnCommand="btnLetter_Command"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton23" runat="server" Text="Ш" CommandArgument="Ш" OnCommand="btnLetter_Command"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton24" runat="server" Text="Щ" CommandArgument="Щ" OnCommand="btnLetter_Command"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton25" runat="server" Text="Э" CommandArgument="Э" OnCommand="btnLetter_Command"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton26" runat="server" Text="Ю" CommandArgument="Ю" OnCommand="btnLetter_Command"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton27" runat="server" Text="Я" CommandArgument="Я" OnCommand="btnLetter_Command"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <asp:Repeater ID="rptPlayers" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <a href='Player.aspx?playerId=<%#Eval("Player_Id")%>'>
                                <%#Eval("Last_Name")%>,&nbsp;<%#Eval("First_Name")%></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>