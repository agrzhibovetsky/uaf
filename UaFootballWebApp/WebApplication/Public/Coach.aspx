<%@ Page Title="" Language="C#" MasterPageFile="~/WebApplication/Site.master" AutoEventWireup="true" CodeBehind="Coach.aspx.cs" Inherits="UaFootball.WebApplication.Public.Coach" %>
<%@ Register TagPrefix="UaFootball" TagName="MatchList" Src="~/WebApplication/Controls/MatchList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<table width="90%">
    <tr>
        <td>
            <%= FormatName(DataItem.FirstName, DataItem.LastName, null, DataItem.Country_Id)%> 
        </td>
    </tr>
    <tr>
        <td>
            <table>
                <tr>
                    <td>
                        <asp:Image ID="iRefereeLogo" ImageUrl="~/WebApplication/Images/nofoto.jpg" runat="server" />
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
                                <td width="150">
                                    <b>Имя:</b>
                                </td>
                                <td>
                                    <%= FormatName(DataItem.FirstName_EN, DataItem.LastName_EN, null, DataItem.Country_Id)%> 
                                </td>
                            </tr>

                            <tr>
                                <td width="150">
                                    <b>Страна:</b>
                                </td>
                                <td>
                                    <%= DataItem.CountryName %> 
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
            <UaFootball:MatchList ID="ml" runat="server" />
        </td>
    </tr>
</table>
</asp:Content>
