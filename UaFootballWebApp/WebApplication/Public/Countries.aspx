<%@ Page Title="" Language="C#" MasterPageFile="~/WebApplication/Site.master" AutoEventWireup="false" CodeBehind="Countries.aspx.cs" Inherits="UaFootball.WebApplication.Public.Countries" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<ajaxToolkit:ToolkitScriptManager runat="server"></ajaxToolkit:ToolkitScriptManager>
<ajaxToolkit:Accordion runat="server" ID="accAssociations" OnItemDataBound="accAssociations_ItemDataBound" RequireOpenedPane="false" SelectedIndex="-1" CssClass="accordion"
    HeaderCssClass="accordionHeader"
    HeaderSelectedCssClass="accordionHeaderSelected"
    ContentCssClass="accordionContent" >
            
    <HeaderTemplate>
        <%#Eval("FIFAAssociation_Description")%> - <%# Eval("FIFAAssociation_Name")%> (<asp:Label ID="lblCount" runat="server"></asp:Label>)
    </HeaderTemplate>

    <ContentTemplate>
        <table style="margin-left:20px" cellpadding="4">
            <asp:Repeater ID="rptCountries" runat="server" OnItemDataBound="rptCountries_DataItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td><asp:HyperLink ID="hlCountry" runat="server"/></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </ContentTemplate>
</ajaxToolkit:Accordion>
           
</asp:Content>

