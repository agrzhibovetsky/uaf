<%@ Page Title="" Language="C#" MasterPageFile="~/WebApplication/Site.master" AutoEventWireup="true" CodeBehind="Games.aspx.cs" Inherits="UaFootball.WebApplication.Games" %>
<%@ Register TagPrefix="UaFootball" TagName="MatchList" Src="~/WebApplication/Controls/MatchList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<table width="100%">
    <tr>
        <td>
            <table width="70%">
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlCompetitions" runat="server" CssClass="default"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSeasons" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btnGo_Click" CssClass="default"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnGo" runat="server" Text="Отобразить" onclick="btnGo_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <UaFootball:MatchList ID="ml" runat="server"></UaFootball:MatchList>
        </td>
    </tr>
</table>
</asp:Content>
