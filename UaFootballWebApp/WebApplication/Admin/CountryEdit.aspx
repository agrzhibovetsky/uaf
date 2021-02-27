<%@ Page Title="" Language="C#" MasterPageFile="~/WebApplication/Site.master" AutoEventWireup="true" Inherits="UaFootball.WebApplication.CountryEdit" Codebehind="CountryEdit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <table cellpadding="4" class="editform">
        <tr>
            <th colspan="2" align="left">Редактировать страну</th>
        </tr>
        <tr>
            <td align="right">
                Название
            </td>
            <td>
                <asp:TextBox ID="tbName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="tbName" ErrorMessage="Введите название страны" CssClass="editFormError"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                Код
            </td>
            <td>
                <asp:TextBox ID="tbCode" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                Конфедерация
            </td>
            <td>
                <asp:DropDownList ID="ddlConfedereations" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnSave" runat="server" Text="Сохранить" OnClick="SaveObject" />
                <asp:Button ID="btnCancel" runat="server" Text="Отмена" CausesValidation="false" OnClick="ReturnToObjectList" />
            </td>
        </tr>
    </table>
        
        
</asp:Content>

