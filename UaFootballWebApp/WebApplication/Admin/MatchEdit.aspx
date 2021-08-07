<%@ Page Title="" Language="C#" MasterPageFile="~/WebApplication/Site.master" AutoEventWireup="true" Inherits="UaFootball.WebApplication.MatchEdit" Codebehind="MatchEdit.aspx.cs" %>
<%@ Register TagPrefix="UaFootball" TagName="AutocompleteTextBox" Src="~/WebApplication/Controls/AutocompleteTextBox.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server" />
<script type="text/javascript">
    var notesCount = 0;
    var noteSetups = null;
    function onTeamChanged(sender) {
        var textBoxControl = sender;
        var hdnInputControl = $("#" + sender.id.replace("tb", "hf"));
        var isNational = $("#cbMatchKind")[0].checked;
        var request = (isNational ? "n" : "c") + hdnInputControl.val();
        var coachTbId = sender.id.indexOf("home") > 0 ? "tbactbHomeCoach" : "tbactbAwayCoach";
        var coachHdnId = coachTbId.replace("tb", "hf");
        if (request.length > 1) {
            $.ajax({
                url: "<%=AutoCompletePath%>?type=MostRecentClubCoach&term=" + request,
                data: "",
                dataType: "json",
                success: function (data) {
                    if (data.id > 0) {
                        $("#" + coachTbId).val(data.value);
                        $("#" + coachHdnId).val(data.id);
                    }
                }
            });

            if (sender.id.indexOf("home") > 0) {
                $.ajax({
                    url: "<%=AutoCompletePath%>?type=MostRecentStadium&term=" + request,
                    data: "",
                    dataType: "json",
                    success: function (data) {
                        if (data.id > 0) {
                            $("#ddlStadiums").val(data.id);
                        }
                    }
                });
            }
        }
    }

    function loadLatestLineup(isHome) {

        var isNationalTeamMatch = document.getElementById("cbMatchKind").checked;
        
        if (!isNationalTeamMatch) {
            var teamId = isHome ? '<%=DataItem.HomeClub_Id%>' : '<%=DataItem.AwayClub_Id%>';
            $.ajax({
                url: "Admin.ashx?action=getLatestLineup&clubId=" + teamId,
                data: "",
                dataType: "json",
                success: function (data) {
                    for (var i = 0; i < data.length; i++) {
                        var tbShirtNumber = isHome ? $("#tblLineups").find("input[id$='tbHomePlayerShirtNumber_" + i + "']") : $("#tblLineups").find("input[id$='tbAwayPlayerShirtNumber_" + i + "']")
                        var tbPlayerName = isHome ? $("#tblLineups").find("input[id$='tbhomePlayerAutocomplete" + i + "']") : $("#tblLineups").find("input[id$='awayPlayerAutocomplete" + i + "']");
                        var hfPlayerId = isHome ? $("#tblLineups").find("input[id$='hfhomePlayerAutocomplete" + i + "']") : $("#tblLineups").find("input[id$='hfawayPlayerAutocomplete" + i + "']");
                        tbShirtNumber.val(data[i].shirtNumber);
                        tbPlayerName.val(data[i].playerName);
                        hfPlayerId.val(data[i].playerId);
                        if (data[i].playerFlags > 0)
                        {
                            if ((data[i].playerFlags & 1) > 0)
                            {
                                cbGoalKeeper = isHome ? $("#tblLineups").find("input[id$='cbHGoalkeeper_" + i + "']") : $("#tblLineups").find("input[id$='cbAGoalkeeper_" + i + "']");
                                cbGoalKeeper[0].checked = true;
                            }
                            if ((data[i].playerFlags & 2) > 0)
                            {
                                cbCaptain = isHome ? $("#tblLineups").find("input[id$='cbHCaptain_" + i + "']") : $("#tblLineups").find("input[id$='cbACaptain_" + i + "']");
                                cbCaptain[0].checked = true;
                            }
                        }
                    }
                }
            });
        }
        
    }

    function loadNoteSetup() {
        $.ajax({
                url: "Admin.ashx?action=getNotesTypes",
                dataType: "json",
                success: function (data) {
                    noteSetups = data;
                    addNote();
                }
            });
    }
    function addNote() {
        if (noteSetups == null) {
            loadNoteSetup()
        }
        else {
            var divContainer = $("#divNoteContainer")[0];
            var div = document.createElement("div");
            var noteTypesDropdown = createDropdown("matchNoteCode_" + notesCount, "vertical-align:top;", noteSetups, function (d) { return new Option(d.Description, d.Code) });
            div.appendChild(noteTypesDropdown);
            var noteOptionsDropdown = createDropdown("matchNoteOption_" + notesCount, "vertical-align:top;  margin:0px 0px 0px 5px; max-width:200px; display:none", null, null);
            div.appendChild(noteOptionsDropdown);
            div.appendChild(createTextArea("matchNoteText_" + notesCount, 2, "width:400px; margin:0px 0px 0px 5px;"));
            
            divContainer.appendChild(div);

            $("#matchNoteCode_" + notesCount).change(function () {
                var noteSetup = null;
                var noteId = this.id.split("_")[1];
                var $codeDropdown = $(this);
                var noteOptionsDropdown = $("#matchNoteOption_" + noteId);
                    
                for (var i = 0; i < noteSetups.length; i++) {
                    if (noteSetups[i].Code == $codeDropdown.val()) noteSetup = noteSetups[i];
                }
                if (noteSetup.Options != null) {
                    noteOptionsDropdown.show();
                    updateDropdown("#matchNoteOption_" + noteId, noteSetup.Options, function (d) { return new Option(d,d) });
                }
                else {
                    noteOptionsDropdown.hide();
                }
            });

            $("#matchNoteOption_" + notesCount).change(function () {
                var tbId = this.id.replace("Option", "Text");
                $("#" + tbId).val($(this).val());
            });

            notesCount++;
        }
    }

    function deleteNote(sender) {
        var noteId = sender.id.split("_")[1];

         $.ajax({
                url: "Admin.ashx?action=deleteMatchNote&id=" + noteId,
                dataType: "json",
                success: function (data) {
                    $(sender).text("Удалено");
             }
        });
    }

    $(document).ready(function () {

        $("input[id^='tbhomePlayerAutocomplete'], input[id^='tbawayPlayerAutocomplete']").each(function () {

            var autoCompleteTextBox = eval(this.id.substr(2));
            
            autoCompleteTextBox.getAdditionalParams = function () {
                if ($("#cbMatchKind:checked").length == 1) {
                    var isHomeTeam = this.autocompleteTextBoxId.indexOf("home") > 0;
                    var nationalTeamId = isHomeTeam ? homeTeamAutocomplete.hf.val() : awayTeamAutocomplete.hf.val();
                    return "&nationalTeam=" + nationalTeamId;
                }
                else
                    return ""
            }
        })

        $(".tbPlayerShirtNumberSelector").blur(function () {
            
            if (this.id.indexOf("Home")>0)
            {
                var teamId= '<%=DataItem.HomeClub_Id.HasValue ? DataItem.HomeClub_Id : DataItem.HomeNationalTeam_Id %>';
                var tbName = $(this).parent().parent().find("[id^='tbhomePlayerAutocomplete']");
                var hfId = $(this).parent().parent().find("[id^='hfhomePlayerAutocomplete']");
            }
            else
            {
               var teamId= '<%=DataItem.AwayClub_Id.HasValue ? DataItem.AwayClub_Id : DataItem.AwayNationalTeam_Id %>';
                var tbName = $(this).parent().parent().find("[id^='tbawayPlayerAutocomplete']");
                var hfId = $(this).parent().parent().find("[id^='hfawayPlayerAutocomplete']");
            }
            var shirtNum = this.value;
            var isNationalTeamMatch =  document.getElementById("cbMatchKind").checked;
            var searchBackward = document.getElementById("cbSearchBackwards").checked
            var request = teamId + ";" + shirtNum + ";" + isNationalTeamMatch + ";" + searchBackward;
            var gkCheckboxId = this.id.replace("tbHomePlayerShirtNumber", "cbHGoalkeeper").replace("tbAwayPlayerShirtNumber","cbAGoalkeeper");
            
            if (shirtNum.length > 0 && shirtNum!="0")
            {
            
                $.ajax({
                    url: "<%=AutoCompletePath%>?type=SearchPlayerByShirtNum&term="+request,
                    data: "",
                    dataType: "json",
                    success: function(data){
                        if (data.id > 0) {

                            if (data.value.indexOf("*") == 0) {
                                data.value = data.value.substring(1);
                                $("#" + gkCheckboxId).prop("checked", true);
                            }
                            tbName.val(data.value);
                            tbName.css("border", "1px solid black");
                            hfId.val(data.id);
                        }
                        else {
                            tbName.val("");
                            tbName.css("border", "1px solid red");
                            hfId.val("");
                        }
                    }
                });
            }
            
        });
    });

    

</script>
<table cellpadding="4" cellspacing="2" class="editform" border="0" width="100%">
        <tr>
            <th colspan="4" align="left">Редактировать матч</th>
        </tr>
        <tr>
            <td colspan="4" align="left" style="color: red; font-weight: bold;">
                Основная информация
            </td>
        </tr>
        <tr>
            <td align="right">
                Это матч уровня сборных?
            </td>
            <td>
                <asp:CheckBox ID="cbMatchKind" runat="server" ClientIDMode="Static" AutoPostBack="true"
                    oncheckedchanged="cbMatchKind_CheckedChanged" />
            </td>
            <td colspan="2">
               &nbsp;
            </td>
            
        </tr>

        <tr>
            <td align="right">
                Турнир
            </td>
            
            <td>
                <asp:DropDownList ID="ddlCompetitions" runat="server" CssClass="default" AutoPostBack="true" 
                    onselectedindexchanged="ddlCompetitions_SelectedIndexChanged" ></asp:DropDownList>
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
                Стадия
            </td>
            
            <td>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlStage" runat="server" CssClass="default"></asp:DropDownList>   
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlCompetitions" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            
            <td colspan="2"></td>
            
        </tr>

        

        <tr>
            <td align="right">
                Команда хозяев
            </td>
            <td>
                <UaFootball:AutocompleteTextBox ID="actbHomeTeam" BehaviorId="homeTeamAutocomplete" OnChange="javascript:onTeamChanged(this);" IsRequired="true" runat="server" />
            </td>

            <td align="left">
                Команда гостей
            </td>
            <td>
                <UaFootball:AutocompleteTextBox ID="actbAwayTeam" BehaviorId="awayTeamAutocomplete"  OnChange="javascript:onTeamChanged(this);" IsRequired="true" runat="server" />
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
                <asp:DropDownList ID="ddlStadiums" runat="server" ClientIDMode="Static" CssClass="default"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvStadiums" runat="server" ControlToValidate="ddlStadiums" Display="Dynamic" ErrorMessage="!" CssClass="editFormError"></asp:RequiredFieldValidator>
            </td>
            
            <td colspan="2"></td>
            
        </tr>
        
        <tr>
            <td align="right">
                Зрители
            </td>
            
            <td>
                <asp:TextBox ID="tbSpecatators" runat="server" MaxLength="6" CssClass="default" autocomplete="off"></asp:TextBox>
                <asp:CompareValidator runat="server" ControlToValidate="tbSpecatators" Display="Dynamic" ErrorMessage="!" CssClass="editFormError" Type="Integer" Operator="DataTypeCheck"></asp:CompareValidator>
            </td>
            
            <td colspan="2"></td>
            
        </tr>

        <tr>
            <td align="right">
                Судья
            </td>
            
            <td>
                <UaFootball:AutocompleteTextBox ID="actbReferee" BehaviorId="refereeAutocomplete" AutocompleteType="Referee" IsRequired="false" runat="server" />
            </td>
            
            <td colspan="2"></td>
            
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
                            <asp:TextBox ID="tbHomeTeamScore" runat="server" ClientIDMode="Static" autocomplete="off" Columns="2"></asp:TextBox>
                            <asp:CompareValidator ID="tbHomeTeamScoreValidator" runat="server" Display="Dynamic" ControlToValidate="tbHomeTeamScore" Operator="DataTypeCheck" Type="Integer" ErrorMessage="!" CssClass="editFormError"></asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="tbHomeTeamScore" ErrorMessage="!" CssClass="editFormError"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                        гости
                        </td>
                        <td>
                            <asp:TextBox ID="tbAwayTeamScore" runat="server" ClientIDMode="Static" autocomplete="off" MaxLength="2" Columns="2"></asp:TextBox>
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
            <td colspan="3" align="left" style="color: red; font-weight: bold;">
                Составы команд
            </td>
            <td>
                Поиск назад<input type="checkbox" id="cbSearchBackwards" />
            </td>
        </tr>

        <tr>
            <td colspan="4">
                <table width="100%" id="tblLineups">

                    <tr>
                        <td colspan="2">
                            <a onclick="loadLatestLineup(true)"> Команда хозяев</a>

                        </td>
                        <td>
                            <span style="width:20px">Вр</span>
                            <span style="width:20px">Кэп</span>
                            <span style="width:20px">Деб</span>
                        </td>
                        <td colspan="2">
                            <a onclick="loadLatestLineup(false)">Команда гостей</a>
                                </td>
                        <td>
                            <span style="width:20px">Вр</span>
                            <span style="width:20px">Кэп</span>
                            <span style="width:20px">Деб</span>
                        </td>
                    </tr>

                    <asp:Repeater runat="server" ID="rptLineup" 
                        onitemcreated="rptLineup_ItemCreated" onitemdatabound="rptLineup_ItemDataBound">
                        <ItemTemplate>
                            <tr runat="server" id="trLinup">
                                <td width="5%">
                                    <asp:TextBox ID="tbHomePlayerShirtNumber" autocomplete="off" runat="server" CssClass="short1 tbPlayerShirtNumberSelector"></asp:TextBox>
                                </td>
                                <td width="30%">
                                    <UaFootball:AutocompleteTextBox runat="server" ID="actbHomePlayer" AutocompleteType="Player" />
                                    <asp:HiddenField ID="hfHomePlayerLineupId" runat="server" />
                                </td>
                                <td width="15%">
                                    <span style="width:20px"><asp:CheckBox ID="cbHGoalkeeper" runat="server" /></span>
                                    <span style="width:20px"><asp:CheckBox ID="cbHCaptain" runat="server" /></span>
                                    <span style="width:20px"><asp:CheckBox ID="cbHDebut" runat="server" /></span>
                                </td>
                                <td width="5%">
                                    <asp:TextBox ID="tbAwayPlayerShirtNumber" autocomplete="off"  runat="server" CssClass="short1 tbPlayerShirtNumberSelector"></asp:TextBox>
                                </td>
                                <td width="30%">
                                    <UaFootball:AutocompleteTextBox ID="actbAwayPlayer" runat="server" AutocompleteType="Player" />
                                    <asp:HiddenField ID="hfAwayPlayerLineupId" runat="server" />
                                </td>
                                <td width="15%">
                                    <asp:CheckBox ID="cbAGoalkeeper" runat="server" />
                                    <asp:CheckBox ID="cbACaptain" runat="server" />
                                    <asp:CheckBox ID="cbADebut" runat="server" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>

                     <tr>
                        <td colspan="3">
                            <UaFootball:AutocompleteTextBox runat="server" ID="actbHomeCoach" Placeholder="Валерий Лобановский" BehaviorId="actbHomeCoach" AutocompleteType="Coach" />
                            (и.о. <asp:CheckBox ID="cbHomeCoachInCharge" runat="server" />)
                            <asp:HiddenField ID="hfHomeCoachLineupId" runat="server"  />
                        </td>
                        
                        <td colspan="3">
                            <UaFootball:AutocompleteTextBox ID="actbAwayCoach" runat="server" Placeholder="Луи ван Гаал" BehaviorId="actbAwayCoach" AutocompleteType="Coach" />
                            (и.о. <asp:CheckBox ID="cbAwayCoachInCharge" runat="server" />)
                            <asp:HiddenField ID="hfAwayCoachLineupId" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        

        <tr>
            <td colspan="4" align="left" style="color: red; font-weight: bold;">
                События матча
            </td>
        </tr>

        <tr>
            <td colspan="4">
                
                <table width="100%">
                    <tr>
                        <td>
                            Минута
                        </td>
                        <td>
                            Тип
                        </td>

                        <td>
                            Игрок 1
                        </td>

                        <td>
                            Игрок 2
                        </td>

                        <td>
                            флаги
                        </td>

                        
                    </tr>
                    
                            <asp:Repeater ID="rptEvents" runat="server" OnItemCreated="rptEvents_ItemCreated" OnItemDataBound="rptEvents_ItemDataBound">
                                <ItemTemplate>
                                    <tr>
                                        <td valign="top">
                                            <asp:TextBox ID="tbMinute" runat="server" autocomplete="off" CssClass="short1" Text='<%#Eval("Minute")%>'></asp:TextBox>
                                            <asp:CompareValidator ID="cvMinute" runat="server" ControlToValidate="tbMinute" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" ErrorMessage="!" CssClass="editFormError"></asp:CompareValidator>
                                            <asp:HiddenField ID="hfMatchEventId" runat="server" Value='<%#Eval("MatchEvent_Id")%>' />
                                        </td>
                                        <td valign="top">
                                            <asp:DropDownList ID="ddlEventTypeCd" ClientIDMode="Predictable" runat="server" CssClass="default" AutoPostBack="true" OnSelectedIndexChanged="ddlEventTypeCd_SelectedIndexChanged" ></asp:DropDownList>
                                        </td>
                                        <td valign="top">
                                            <UaFootball:AutocompleteTextBox runat="server" ID="actbEventPlayer1" AutocompleteType="Player" Text='<%#FormatName(Eval("Player1.First_Name"),Eval("Player1.Last_Name"),Eval("Player1.Display_Name"), (int)Eval("Player1.Country_Id"))%>' Value='<%#Eval("Player1_Id") %>' />
                                        </td>

                                        <td valign="top">
                                            <UaFootball:AutocompleteTextBox runat="server" ID="actbEventPlayer2" AutocompleteType="Player" Text='<%#FormatName(Eval("Player2.First_Name"),Eval("Player2.last_Name"),Eval("Player2.Display_Name"), (int)Eval("Player2.Country_Id"))%>' Value='<%#Eval("Player2_Id") %>'/>
                                        </td>
                                        <td valign="top">
                                            <div id="cardFlags" runat="server">
                                                <asp:UpdatePanel ID="upEventFlags" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlEventTypeCd" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:CheckBoxList ID="cblEventFlags" runat="server" OnDataBound="cblEventFlags_DataBound"/>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>                                            
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr>
                                <td colspan="5" align="left">
                                    <asp:Button Text="Добавить" runat="server" ID="btnAddEvent" OnClick="btnAddEvent_Click" />
                                </td>
                            </tr>        
                        
                </table>
            </td>
        </tr>
        
        <tr>
            <td colspan="4" align="left">
                <asp:CheckBoxList runat="server" ID="cblMatchFlags" RepeatDirection="Horizontal" RepeatLayout="Table">
                    
                </asp:CheckBoxList>
            </td>
        </tr>

        <tr>
            <td colspan="4">
                <div>Примечания:</div>
                <div>
                    <table>
                    <asp:Repeater ID="rptNotes" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("CodeDescription")%></td>
                                <td><%#Eval("Text")%></td>
                                <td><a href="javascript:void(0)"; onclick="deleteNote(this)" id="btnDelNote_<%#Eval("MatchNote_Id")%>">Удалить</a></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    </table>
                </div>
                <div id="divNoteContainer">
                    
                </div>
                <input type="button" value="Добавить" onclick="addNote()" />
            </td>

        </tr>

         <tr>
            <td colspan="4">
                <div>Особые отметки:</div>
                <asp:TextBox TextMode="MultiLine" runat="server" ID="tbSpecialNotes" Width="400"></asp:TextBox>
            </td>

        </tr>
        <tr>
            <td colspan="4">
                <div>Прочие отметки:</div>
                <asp:TextBox TextMode="MultiLine" runat="server" ID="tbAdminNotes" Width="400"></asp:TextBox>
            </td>

        </tr>
        <tr>
            <td colspan="4">
                <div>Источники:</div>
                <asp:TextBox TextMode="MultiLine" runat="server" ID="tbSources" Width="90%" Height="150"></asp:TextBox>
            </td>

        </tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="btnSave" runat="server" Text="Сохранить" OnClick="SaveObject" />
                <asp:Button ID="btnCancel" runat="server" Text="Отмена" CausesValidation="false" OnClick="ReturnToObjectList" />
                <asp:CustomValidator ID="cvDuplicateShirtNums" ClientIDMode="Static" runat="server" ClientValidationFunction="checkDuplicates" Text="Duplicate Shirt Numbers: " ForeColor="Red" Display="Dynamic"></asp:CustomValidator>
            </td>
        </tr>

       


</table>
    <script type="text/javascript">
        function checkDuplicates(source, arguments)
        {
            var homeShirtNumsControls = $("[class*='tbPlayerShirtNumberSelector'][id*='Home']");
            var awayShirtNumsControls = $("[class*='tbPlayerShirtNumberSelector'][id*='Away']");
            var homenums = [];
            var awaynums = [];
            for (var i=0; i<homeShirtNumsControls.length;i++)
            {
                var homenum = $(homeShirtNumsControls[i]).val();
                var awaynum = $(awayShirtNumsControls[i]).val();

                if (homenums.indexOf(homenum)>-1 || awaynums.indexOf(awaynum)>-1)
                {
                    arguments.IsValid = false;
                    cvDuplicateShirtNums.textContent += homenums.indexOf(homenum)>-1 ? homenum + " (Home)" : awaynum + " (Away)";
                }
                else
                {
                    if (parseInt(homenum)>0)
                        homenums.push(homenum);
                    if (parseInt(awaynum)>0)
                        awaynums.push(awaynum);
                }
            }
        }
    </script>
</asp:Content>

