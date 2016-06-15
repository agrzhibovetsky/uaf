<%@ Page Title="" Language="C#" MasterPageFile="~/WebApplication/Site.master" AutoEventWireup="true" Inherits="UaFootball.WebApplication.StadiumEdit" Codebehind="StadiumEdit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<table cellpadding="4" class="editform">
        <tr>
            <th colspan="2" align="left">Редактировать стадион</th>
        </tr>
        <tr>
            <td align="right">
                Название
            </td>
            <td>
                <asp:TextBox ID="tbName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="tbName" ErrorMessage="Введите название стадиона" CssClass="editFormError"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                Город
            </td>
            <td>
                <asp:DropDownList ID="ddlCities" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Вместимость
            </td>
            <td>
                <asp:TextBox ID="tbCapacity" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="tbCapacity" ErrorMessage="Введите вместимость" CssClass="editFormError"></asp:RequiredFieldValidator>
                <asp:CompareValidator runat="server" Display="Dynamic" ControlToValidate="tbCapacity" Operator="DataTypeCheck" Type="Integer" ErrorMessage="Вместимость должна быть числом" CssClass="editFormError"></asp:CompareValidator>
            </td>
        </tr>

        <tr>
            <td align="right">
                Год постройки
            </td>
            <td>
                <asp:TextBox ID="tbYearBuilt" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="tbYearBuilt" ErrorMessage="Введите год постройки" CssClass="editFormError"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" Display="Dynamic" ControlToValidate="tbYearBuilt" Operator="DataTypeCheck" Type="Integer" ErrorMessage="Год постройки должен быть числом" CssClass="editFormError"></asp:CompareValidator>
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

