<%@ Page Title="" Language="C#" MasterPageFile="~/WebApplication/Site.master" AutoEventWireup="true" CodeBehind="Tasks.aspx.cs" Inherits="UaFootball.WebApplication.Admin.Tasks" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="border:1px solid">
        <asp:Repeater ID="rptTasks" runat="server">
            <ItemTemplate>
                <tr>
                    <!--<td>
                        <%#Eval("Description") %>
                    </td>
                        <td>
                        <%#Eval("Status_CD") %>
                    </td>
                    <td>
                        <%#Eval("Type_CD") %>
                    </td>-->
                    <td>
                        <%#Eval("Comments") %>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Content>
