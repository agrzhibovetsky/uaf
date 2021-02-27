<%@ Page Title="" Language="C#" MasterPageFile="~/WebApplication/Site.master" AutoEventWireup="true" CodeBehind="Stadium.aspx.cs" Inherits="UaFootball.WebApplication.Public.Stadium" %>
<%@ Register TagPrefix="UaFootball" TagName="MatchList" Src="~/WebApplication/Controls/MatchList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<br />
<table width="90%">
    <tr>
        <td>
            <table width="300">
                <tr>
                    <td>
                        <b>Название:</b>
                    </td>
                    <td>
                        <%=DataItem.Stadium_Name%>
                    </td>
                </tr>

                <tr>
                    <td>
                        <b>Год постройки:</b>
                    </td>
                    <td>
                        <%=DataItem.YearBuilt.ToString()%>
                    </td>
                </tr>

                <tr>
                    <td>
                        <b>Вместимость:</b>
                    </td>
                    <td>
                        <%=DataItem.Capacity.ToString()%>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    
    <tr>
        <td>
            <br />
            <UaFootball:MatchList ID="ml" runat="server" ExtraColumn="Spectators" />
        </td>
    </tr>
</table>
</asp:Content>
