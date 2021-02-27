<%@ Control Language="C#" AutoEventWireup="true" Inherits="UaFootball.WebApplication.AutocompleteTextBox" Codebehind="AutocompleteTextBox.ascx.cs" %>
<asp:TextBox ID="tb" runat="server" ClientIDMode="Static" CssClass="default"></asp:TextBox>
<asp:HiddenField ID="hf" runat="server" ClientIDMode="Static" />
<asp:CustomValidator runat="server" ID="cvAutocomplete" Display="Dynamic" ErrorMessage="!" CssClass="editFormError" ClientValidationFunction="validateAutocomplete"></asp:CustomValidator>
           