<%@ Page Title="" Language="C#" MasterPageFile="~/WebApplication/Site.master" AutoEventWireup="true" Inherits="UaFootball.WebApplication.ClubEdit" Codebehind="ClubEdit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link rel="Stylesheet" type="text/css" href='<%=JQueryUICssPath%>' />
    <script type="text/javascript" src='<%=JQueryPath%>'></script>
    <script type="text/javascript" src='<%=JQueryUIPath%>'></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table cellpadding="4" class="editform">
        <tr>
            <th colspan="2" align="left">Редактировать клуб</th>
        </tr>
        <tr>
            <td align="right">
                Название
            </td>
            <td>
                <asp:TextBox ID="tbName" runat="server" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="tbName" ErrorMessage="Введите название клуба" CssClass="editFormError"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                Отображать как
            </td>
            <td>
                <asp:TextBox ID="tbDisplayName" runat="server" MaxLength="50"></asp:TextBox>
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
                &nbsp;
            </td>
            <td>
                <asp:Image runat="server" ID="imgLogo" ClientIDMode="Static" />
            </td>
        </tr>
        <tr>
            <td align="right">
                Год основания
            </td>
            <td>
                <asp:TextBox ID="tbYearFound" runat="server"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator2" runat="server" Display="Dynamic" ControlToValidate="tbYearFound" Operator="DataTypeCheck" Type="Integer" ErrorMessage="Год постройки должен быть числом" CssClass="editFormError"></asp:CompareValidator>
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

