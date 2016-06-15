<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="Admin_MatchEdit" Codebehind="MatchEdit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link rel="Stylesheet" type="text/css" href='<%=JQueryUICssPath%>' />
    <script type="text/javascript" src='<%=JQueryPath%>'></script>
    <script type="text/javascript" src='<%=JQueryUIPath%>'></script>
    <script type="text/javascript">

        function validateTeam(source, arguments) {

            var hfObj = document.getElementById(source.valueHolderId);
            arguments.IsValid = hfObj.value.length > 0;
        }

        function setTeamAutoCompletePath() {
            var tbHomeTeam = $("#tbHomeTeam");
            var tbAwayTeam = $("#tbAwayTeam");
            var isNationalTeamMatch = $("#cbMatchKind")[0].checked;
            var autocompleteType = "club";
            if (isNationalTeamMatch) autocompleteType = "nationalTeam";
            var autocompletePath = "http://localhost/UaFootball/Admin/Autocomplete.ashx?type=" + autocompleteType;
            tbHomeTeam.autocomplete("option", "source", autocompletePath);
            tbAwayTeam.autocomplete("option", "source", autocompletePath);    
        }

        $(document).ready(function () {

            var tbHomeTeam = $("#tbHomeTeam");

            var tbAwayTeam = $("#tbAwayTeam");
            $("#cbMatchKind").change(function () {
                setTeamAutoCompletePath();
            });

            tbHomeTeam.autocomplete
            ({
                source: "http://localhost/UaFootball/Admin/Autocomplete.ashx?type=club",
                minLength: 2,
                select: function (event, ui) {
                    $("#tbHomeTeam").val(ui.item.value);
                    $("#hfHomeTeamId").val(ui.item.id);
                }
            })

            tbAwayTeam.autocomplete
            ({
                source: "http://localhost/UaFootball/Admin/Autocomplete.ashx?type=club",
                minLength: 2,
                select: function (event, ui) {
                    $("#tbAwayTeam").val(ui.item.value);
                    $("#hfAwayTeamId").val(ui.item.id);
                }
            })

            setTeamAutoCompletePath();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<table cellpadding="4" cellspacing="2" class="editform" border="0" width="100%">
        <tr>
            <th colspan="4" align="left">Редактировать матч</th>
        </tr>
        <tr>
            <td align="right">
                Это матч уровня сборных?
            </td>
            <td>
                <asp:CheckBox ID="cbMatchKind" runat="server" ClientIDMode="Static" AutoPostBack="true"
                    oncheckedchanged="cbMatchKind_CheckedChanged" />
            </td>
            <td colspan="2"></td>
            
        </tr>

        <tr>
            <td align="right">
                Турнир
            </td>
            
            <td>
                <asp:DropDownList ID="ddlCompetitions" runat="server" CssClass="default" ></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvCompetitions" runat="server" ControlToValidate="ddlCompetitions" Display="Dynamic" ErrorMessage="!" CssClass="editFormError"></asp:RequiredFieldValidator>
            </td>
            
            <td align="left">
                Сезон
            </td>
            
            <td>
                <asp:DropDownList ID="ddlSeasons" runat="server" CssClass="default"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvSeasons" runat="server" ControlToValidate="ddlSeasons" Display="Dynamic" ErrorMessage="!" CssClass="editFormError"></asp:RequiredFieldValidator>
            
            </td>
            
        </tr>

        <tr>
            <td align="right">
                Дата проведения матча
            </td>
            <td>
                <asp:TextBox ID="tbDate" runat="server" MaxLength="10" CssClass="default"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator1" runat="server" Display="Dynamic" ControlToValidate="tbDate" Operator="DataTypeCheck" Type="Date" ErrorMessage="Неверная дата" CssClass="editFormError"></asp:CompareValidator>
                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="tbDate" ErrorMessage="!"></asp:RequiredFieldValidator>
            </td>
            <td colspan="2"></td>
        </tr>

        <tr>
            <td align="right">
                Стадион
            </td>
            
            <td>
                <asp:DropDownList ID="ddlStadiums" runat="server" CssClass="default"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvStadiums" runat="server" ControlToValidate="ddlStadiums" Display="Dynamic" ErrorMessage="!" CssClass="editFormError"></asp:RequiredFieldValidator>
            
            </td>
            
            <td colspan="2"></td>
            
        </tr>

        <tr>
            <td align="right">
                Команда хозяев
            </td>
            <td>
                <asp:TextBox ID="tbHomeTeam" runat="server" ClientIDMode="Static" CssClass="default"></asp:TextBox>
                <asp:HiddenField ID="hfHomeTeamId" runat="server" ClientIDMode="Static" />
                <asp:CustomValidator runat="server" ID="cvHomeTeam" Display="Dynamic" ErrorMessage="!" CssClass="editFormError" ClientValidationFunction="validateTeam"></asp:CustomValidator>
            </td>

            <td align="left">
                Команда гостей
            </td>
            <td>
                <asp:TextBox ID="tbAwayTeam" runat="server" ClientIDMode="Static" CssClass="default"></asp:TextBox>
                <asp:HiddenField ID="hfAwayTeamId" runat="server"  ClientIDMode="Static"/>
                <asp:CustomValidator runat="server" ID="cvAwayTeam" Display="Dynamic" ErrorMessage="!" CssClass="editFormError" ClientValidationFunction="validateTeam"></asp:CustomValidator>
            </td>

        </tr>

        <tr>
            <td align="right">
                Голы:
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                        хозяева
                        </td>
                        <td>
                            <asp:TextBox ID="tbHomeTeamScore" runat="server" ClientIDMode="Static" Columns="2"></asp:TextBox>
                            <asp:CompareValidator ID="tbHomeTeamScoreValidator" runat="server" Display="Dynamic" ControlToValidate="tbHomeTeamScore" Operator="DataTypeCheck" Type="Integer" ErrorMessage="!" CssClass="editFormError"></asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="tbHomeTeamScore" ErrorMessage="!" CssClass="editFormError"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                        гости
                        </td>
                        <td>
                            <asp:TextBox ID="tbAwayTeamScore" runat="server" ClientIDMode="Static" MaxLength="2" Columns="2"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator3" runat="server" Display="Dynamic" ControlToValidate="tbAwayTeamScore" Operator="DataTypeCheck" Type="Integer" ErrorMessage="!" CssClass="editFormError"></asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="tbAwayTeamScore" ErrorMessage="!" CssClass="editFormError"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </td>
            <td align="left">
                Пенальти:
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            хозяева
                        </td>
                        <td>
                            <asp:TextBox ID="tbHomeTeamPenaltyScore" runat="server" ClientIDMode="Static" Columns="2"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" Display="Dynamic" ControlToValidate="tbHomeTeamPenaltyScore" Operator="DataTypeCheck" Type="Integer" ErrorMessage="!" CssClass="editFormError"></asp:CompareValidator>
                        </td>
                        <td>
                            гости
                        </td>
                        <td>
                            <asp:TextBox ID="tbAwayTeamPenaltyScore" runat="server" ClientIDMode="Static" MaxLength="2" Columns="2"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator4" runat="server" Display="Dynamic" ControlToValidate="tbAwayTeamPenaltyScore" Operator="DataTypeCheck" Type="Integer" ErrorMessage="!" CssClass="editFormError"></asp:CompareValidator>
                        </td>
                    </tr>
                </table>
            </td>
            
            
            
        </tr>

        <tr>
            <td colspan="4">
                <asp:Button ID="btnSave" runat="server" Text="Сохранить" OnClick="SaveObject" />
                <asp:Button ID="btnCancel" runat="server" Text="Отмена" CausesValidation="false" OnClick="ReturnToObjectList" />
            </td>
        </tr>
</table>
</asp:Content>

