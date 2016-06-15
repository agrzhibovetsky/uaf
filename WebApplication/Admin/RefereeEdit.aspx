<%@ Page Title="" Language="C#" MasterPageFile="~/WebApplication/Site.master" AutoEventWireup="true" Inherits="UaFootball.WebApplication.RefereeEdit" Codebehind="RefereeEdit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table cellpadding="4" class="editform">
        <tr>
            <th colspan="2" align="left">Редактировать судью</th>
        </tr>
        <tr>
            <td align="right">
                Имя
            </td>
            <td>
                <asp:TextBox ID="tbFirstName" runat="server" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="tbFirstName" ErrorMessage="Введите имя судьи" CssClass="editFormError"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                Фамилия
            </td>
            <td>
                <asp:TextBox ID="tbLastName" runat="server" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="tbLastName" ErrorMessage="Введите фамилию судьи" CssClass="editFormError"></asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td align="right">
                Имя (English)
            </td>
            <td>
                <asp:TextBox ID="tbFirstNameEN" runat="server" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                Фамилия (English)
            </td>
            <td>
                <asp:TextBox ID="tbLastNameEN" runat="server" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        
        <tr>
            <td align="right">
                Дата рождения
            </td>
            <td>
                <asp:TextBox ID="tbDOB" runat="server" MaxLength="10"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator1" runat="server" Display="Dynamic" ControlToValidate="tbDOB" Operator="DataTypeCheck" Type="Date" ErrorMessage="Неверная дата" CssClass="editFormError"></asp:CompareValidator>
            </td>
        </tr>

        <tr>
            <td align="right">
                Страна
            </td>
            <td>
                <asp:DropDownList ID="ddlCountries" runat="server"></asp:DropDownList>
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

