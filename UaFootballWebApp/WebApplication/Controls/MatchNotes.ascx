<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MatchNotes.ascx.cs" Inherits="UaFootball.WebApplication.Controls.MatchNotes" %>
<asp:Repeater id="rptNotes" runat="server">
    <ItemTemplate>
        <a href="#matchNote_<%#Eval("RowIndex")%>" title="<%#Eval("Text")%>"><sup>[<%#Eval("RowIndex")%>]</sup></a>
    </ItemTemplate>
</asp:Repeater>