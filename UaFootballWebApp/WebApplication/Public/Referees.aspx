<%@ Page Language="C#" MasterPageFile="~/WebApplication/Site.master" AutoEventWireup="true" CodeBehind="Referees.aspx.cs" Inherits="UaFootball.WebApplication.Referees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table width="90%">
        <asp:Repeater ID="rptCountries" OnItemDataBound="rptCountries_ItemDataBound" runat="server">
            <ItemTemplate>
                <tr>
                    <td><asp:Literal ID="ltCountryName" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td>
                        <table style="margin-left:50px;">
                            <asp:Repeater ID="rptCountryReferees" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td style="padding:5px; width:500px;" ><a href="/uafootball/webapplication/public/Referee.aspx?objectId=<%#Eval("Referee_Id")%>"> <%#Eval("LastName")%>, <%#Eval("FirstName")%> </a> (<%#Eval("matchesCount")%>)</td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Content>