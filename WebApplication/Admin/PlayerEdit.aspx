<%@ Page Title="" Language="C#" MasterPageFile="~/WebApplication/Site.master" AutoEventWireup="true" Inherits="UaFootball.WebApplication.PlayerEdit" Codebehind="PlayerEdit.aspx.cs" %>
<%@ Register TagPrefix ="uc" NameSpace="UaFootball.WebApplication.Controls" Assembly="UaFootball"%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    
    <script type="text/javascript">
        function parseDate(tbObj) {
            var dt = tbObj.value;
            var regexp = /(\w{3})\s([0-9]{1,2}),\s([0-9]{4})\s?/gi; //Apr 3, 1984
            
            var a = regexp.exec(dt);
            if (a.length == 4) {
                var m = a[1];
                var d = a[2];
                switch (m) {
                    case "Jan": m = 1; break;
                    case "Feb": m = 2; break;
                    case "Mar": m = 3; break
                    case "Apr": m = 4; break;
                    case "May": m = 5; break;
                    case "Jun": m = 6; break;
                    case "Jul": m = 7; break;
                    case "Aug": m = 8; break;
                    case "Sep": m = 9; break;
                    case "Oct": m = 10; break;
                    case "Nov": m = 11; break;
                    case "Dec": m = 12; break;
                }
                var y = a[3];
                ddlDay.value = d;
                ddlMonth.value = m;
                ddlYear.value = y;

            }
            
        }

        $(document).ready(function () {
            $(".iMoveWord").click(function () {
                var tbSrc = $("#" + $(this).data("tbsrc"));
                var tbDst = $("#" + $(this).data("tbdst"));
                var direction = $("#" + $(this).data("dir"));
                var textParts = tbSrc.val().split(" ");
                var curSrcVal = tbSrc.val();
                var curDstVal = tbDst.val();
                //debugger;
                if (textParts.length > 0) {
                    if (direction = "down") {
                        var lastPart = textParts[textParts.length - 1];
                        var indexOfLastPart = curSrcVal.lastIndexOf(lastPart);
                        var newSrcVal = curSrcVal.substr(0, indexOfLastPart - 1);
                        var newDstVal = lastPart + " " + curDstVal;
                        tbSrc.val(newSrcVal.trim());
                        tbDst.val(newDstVal.trim());
                    }
                    else {
                        var firstPart = textParts[0];
                        var newSrcVal = curSrcVal.substr(firstPart.length);
                        var newDstVal = curDstVal + " " + firstPart;
                        tbSrc.val(newSrcVal.trim());
                        tbDst.val(newDstVal.trim());
                    }
                }
                

            });

            $("#tbFirstNameInt").blur(function () {
                var firstNameInt = $(this).val();
                var lastNameInt = $("#tbLastNameInt").val().trim();
                var firstNameParts = firstNameInt.split(" ");
                if (firstNameParts.length == 2 && lastNameInt.length == 0) {
                    $("#itbFirstNameIntDown").click();
                }
            });
           
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <table cellpadding="4" class="editform">
        <tr>
            <th colspan="2" align="left">Редактировать игрока <a href="/UaFootball/WebApplication/Public/Player.aspx?playerId=<%=DataItem.Player_Id%>">></a></th>
        </tr>
        <tr>
            <td align="right">
                Имя
            </td>
            <td>
                <asp:TextBox ID="tbFirstName" runat="server" autocomplete="off" MaxLength="50"></asp:TextBox>
                <asp:TextBox ID="tbFirstNameInt" ClientIDMode="Static" runat="server" autocomplete="off" MaxLength="50"></asp:TextBox>
                <img class="iMoveWord" id="itbFirstNameIntDown" src="../Images/down_arrow.png" data-tbSrc="tbFirstNameInt" data-tbDst="tbLastNameInt" data-direction="down"/>
            </td>
        </tr>
        <tr>
            <td align="right">
                Фамилия
            </td>
            <td>
                <asp:TextBox ID="tbLastName" runat="server" autocomplete="off" MaxLength="50"></asp:TextBox>
                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="tbLastName" ErrorMessage="Введите фамилию игрока" CssClass="editFormError"></asp:RequiredFieldValidator>
                <asp:TextBox ID="tbLastNameInt" ClientIDMode="Static" autocomplete="off" runat="server" MaxLength="50"></asp:TextBox>
                <img class="iMoveWord" src="../Images/up_arrow.png" data-tbSrc="tbLastNameInt" data-tbDst="tbFirstNameInt" data-direction="up" />
                <asp:CustomValidator ID="cvSymbols" runat="server" OnServerValidate="cvSymbols_ServerValidate"  CssClass="editFormError" ErrorMessage="Недопустимый символ"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                Отчество
            </td>
            <td>
                <asp:TextBox ID="tbMiddleName" runat="server" MaxLength="50"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td align="right">
                Отображать как
            </td>
            <td>
                <asp:TextBox ID="tbDisplayName" autocomplete="off" runat="server" MaxLength="50"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td align="right">
                Дата рождения
            </td>
            <td>
                <asp:DropDownList ID="ddlDay" runat="server" ClientIDMode="Static"></asp:DropDownList>
                <asp:DropDownList ID="ddlMonth" runat="server" ClientIDMode="Static"></asp:DropDownList>
                <asp:DropDownList ID="ddlYear" runat="server" ClientIDMode="Static"></asp:DropDownList>
                <input type="text" id="tbDate" onblur="parseDate(this)" />
                Неизвестно <asp:CheckBox runat="server" ID="cbUnknownDOB" />
                <asp:CustomValidator ID="cvDOB" runat="server" OnServerValidate="cvDOB_ServerValidate" ErrorMessage="Неверная дата" CssClass="editFormError"></asp:CustomValidator>
            </td>
        </tr>

        <tr>
            <td align="right">
                Рост
            </td>
            <td>
                <asp:TextBox ID="tbHeight" autocomplete="off" runat="server" MaxLength="3"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator2" runat="server" Display="Dynamic" ControlToValidate="tbHeight" Operator="DataTypeCheck" Type="Integer" ErrorMessage="Неверный рост" CssClass="editFormError"></asp:CompareValidator>
            </td>
        </tr>

        <tr>
            <td align="right">
                Вес
            </td>
            <td>
                <asp:TextBox ID="tbWeight" autocomplete="off" runat="server" MaxLength="3"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator3" runat="server" Display="Dynamic" ControlToValidate="tbWeight" Operator="DataTypeCheck" Type="Integer" ErrorMessage="Неверный вес" CssClass="editFormError"></asp:CompareValidator>
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
            <td align="right">
                Город
            </td>
            <td>
                <asp:TextBox ID="tbCity" runat="server"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td align="right">
                Область
            </td>
            <td>
                <asp:DropDownList ID="ddlObl" runat="server">
                    <asp:ListItem Text="Выбрать.." Value="0"></asp:ListItem>
                    <asp:ListItem Text="АР Крым"></asp:ListItem>
                    <asp:ListItem Text="Винницкая область"></asp:ListItem>
                    <asp:ListItem Text="Волынская область"></asp:ListItem>
                    <asp:ListItem Text="Днепропетровская область"></asp:ListItem>
                    <asp:ListItem Text="Донецкая область"></asp:ListItem>
                    <asp:ListItem Text="Житомирская область"></asp:ListItem>
                    <asp:ListItem Text="Закарпатская область"></asp:ListItem>
                    <asp:ListItem Text="Запорожская область"></asp:ListItem>
                    <asp:ListItem Text="Ивано-Франковская область"></asp:ListItem>
                    <asp:ListItem Text="Киевская область"></asp:ListItem>
                    <asp:ListItem Text="Кировоградская область"></asp:ListItem>
                    <asp:ListItem Text="Луганская область"></asp:ListItem>
                    <asp:ListItem Text="Львовская область"></asp:ListItem>
                    <asp:ListItem Text="Николаевская область"></asp:ListItem>
                    <asp:ListItem Text="Одесская область"></asp:ListItem>
                    <asp:ListItem Text="Полтавская область"></asp:ListItem>
                    <asp:ListItem Text="Ровненская область"></asp:ListItem>
                    <asp:ListItem Text="Сумская область"></asp:ListItem>
                    <asp:ListItem Text="Тернопольская область"></asp:ListItem>
                    <asp:ListItem Text="Харьковская область"></asp:ListItem>
                    <asp:ListItem Text="Херсонская область"></asp:ListItem>
                    <asp:ListItem Text="Хмельницкая область"></asp:ListItem>
                    <asp:ListItem Text="Черкасская область"></asp:ListItem>
                    <asp:ListItem Text="Черниговская область"></asp:ListItem>
                    <asp:ListItem Text="Черновицкая область"></asp:ListItem>
                    <asp:ListItem Text="Вне Украины"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>&nbsp;</td>
            <td>
                <table>
                    <asp:Repeater ID="rptLogos" runat="server" onitemdatabound="rptLogos_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <a href="Multimedia.aspx?Id=<%#Eval("Multimedia_ID")%>" target="_blank"><asp:Image runat="server" ID="imgLogo" style="max-width:600px;" /></a>
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/WebApplication/images/delete.gif" CommandArgument='<%#Eval("Multimedia_ID")%>' OnCommand="btnDelete_Command"/>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </td>
        </tr>
        
        <tr>
            <td colspan="2">
                <asp:Button ID="btnSave" runat="server" Text="Сохранить" OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Отмена" CausesValidation="false" OnClick="ReturnToObjectList" />
            </td>
        </tr>

        <tr>
            <td colspan="2">
                <asp:Button ID="btnSearchByCountry" runat="server" Text="Искать по стране" OnClick="btnSearchByCountry_Click" CausesValidation="false" />  
            </td>
        </tr>


    </table>

    <div>
        <asp:UpdatePanel ID="upSearchPlayers" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearchByCountry" />
            </Triggers>
            <ContentTemplate>
                <table width="100%">
                    <asp:Repeater ID="rptSearchPlayers" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td width="35%"><%#Eval("Last_Name_Int")%>, <%#Eval("First_Name_Int")%></td>
                                <td width="65%"><a href="/UaFootball/WebApplication/Public/Player.aspx?playerId=<%#Eval("Player_Id")%>">
                                        <%# FormatName(Eval("First_Name"), Eval("Last_Name"), Eval("Display_Name"), (int)Eval("Country_Id"))%>
                                    </a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <table width="100%">
        <asp:Repeater ID="rptDuplicates" runat="server" Visible="false" OnItemDataBound="rptDuplicates_ItemDataBound">
            <ItemTemplate>
                <tr class='duplicate_rank_<%#Eval("Weight")%>'>
                    <td style="width:15px">
                        <%#Eval("Weight")%>
                    </td>
                    <td>
                        <a href='/UaFootball/WebApplication/Public/Player.aspx?playerId=<%#Eval("Player_Id")%>'>
                            <uc:HighlightedLabel runat="server" ID="hl" CssClassForHighlight="editFormError"/>
                        </a>
                    </td>
                    <td><%# FormatName(Eval("First_Name"), Eval("Last_Name"), Eval("Display_Name"), (int)Eval("Country_Id"))%></td>
                    <td><span class="editFormError"><%#CheckAndDisplayDOB(Eval("DOB"))%></span></td>
                    <td><%#Eval("Country.Country_Name")%></td>
                                
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>

    <asp:Button ID="btnSaveOverride" runat="server" Text="Сохранить - не дубликат" OnClick="SaveObject" Visible="false" />
</asp:Content>

