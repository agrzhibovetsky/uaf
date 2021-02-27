<%@ Page Title="" Language="C#" MasterPageFile="~/WebApplication/Site.master" AutoEventWireup="true" Inherits="UaFootball.WebApplication.PlayerList" Codebehind="PlayerList.aspx.cs" %>

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
    </table>
<asp:DataGrid ID="dgData" runat="server" AutoGenerateColumns="false" CellPadding="5" CssClass="data">
    <Columns>
        <asp:TemplateColumn HeaderText="Имя">
            <ItemTemplate>
                <asp:LinkButton ID="btnEdit" runat="server" CommandArgument='<%#Eval("Player_Id")%>' OnCommand="EditObject" Text='<%#string.Concat(Eval("Last_Name"), ", ", Eval("First_Name"))%>'></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateColumn>
        
        <asp:BoundColumn DataField="Country_Name" HeaderText="Страна"></asp:BoundColumn>

        <asp:TemplateColumn>
            <ItemTemplate>
                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/WebApplication/images/delete.gif" CommandArgument='<%#Bind("Player_Id")%>' OnCommand="DeleteObject" />
            </ItemTemplate>
        </asp:TemplateColumn>
    </Columns>
</asp:DataGrid>
<br />
<asp:Button ID="Button1" runat="server" OnClick="AddObject" Text="Добавить" />

</asp:Content>

