<%@ Page Title="" Language="C#" MasterPageFile="~/WebApplication/Site.master" AutoEventWireup="true" Inherits="UaFootball.WebApplication.MatchEdit" Codebehind="MatchEdit.aspx.cs" %>
<%@ Register TagPrefix="UaFootball" TagName="AutocompleteTextBox" Src="~/WebApplication/Controls/AutocompleteTextBox.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link rel="Stylesheet" type="text/css" href='<%=JQueryUICssPath%>' />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server" />
<script type="text/javascript">

    $(document).ready(function () {
        $(".tbPlayerShirtNumberSelector").blur(function () {
            
            if (this.id.indexOf("Home")>0)
            {
                var teamId= <%=DataItem.HomeClub_Id.HasValue ? DataItem.HomeClub_Id : DataItem.HomeNationalTeam_Id %>;
                var tbName = $(this).parent().parent().find("[id^='tbhomePlayerAutocomplete']");
                var hfId = $(this).parent().parent().find("[id^='hfhomePlayerAutocomplete']");
            }
            else
            {
                var teamId= <%=DataItem.AwayClub_Id.HasValue ? DataItem.AwayClub_Id : DataItem.AwayNationalTeam_Id %>;
                var tbName = $(this).parent().parent().find("[id^='tbawayPlayerAutocomplete']");
                var hfId = $(this).parent().parent().find("[id^='hfawayPlayerAutocomplete']");
            }
            var shirtNum = this.value;
            var isNationalTeamMatch =  document.getElementById("cbMatchKind").checked;
            var searchBackward = document.getElementById("cbSearchBackwards").checked
            var request = teamId + ";"+shirtNum+";"+isNationalTeamMatch+";"+searchBackward;
            
            if (shirtNum.length > 0)
            {
            
                $.ajax({
                    url: "<%=AutoCompletePath%>?type=SearchPlayerByShirtNum&term="+request,
                    data: "",
                    dataType: "json",
                    success: function(data){
                            if (data.id > 0)
                            {
                                 tbName.val(data.value);
								 tbName.css("border", "1px solid black");
                                 hfId.val(data.id);
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
                Команда хозяев
            </td>
            <td>
                <UaFootball:AutocompleteTextBox ID="actbHomeTeam" BehaviorId="homeTeamAutocomplete" IsRequired="true" runat="server" />
            </td>

            <td align="left">
                Команда гостей
            </td>
            <td>
                <UaFootball:AutocompleteTextBox ID="actbAwayTeam" BehaviorId="awayTeamAutocomplete" IsRequired="true" runat="server" />
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
                <table width="100%">

                    <tr>
                        <td colspan="2">Команда хозяев</td>
                        <td>
                            <span style="width:20px">Вр</span>
                            <span style="width:20px">Кэп</span>
                            <span style="width:20px">Деб</span>
                        </td>
                        <td colspan="2">Команда гостей</td>
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
                                    <asp:TextBox ID="tbHomePlayerShirtNumber" autocomplete="off" runat="server" ClientIDMode="Static" CssClass="short1 tbPlayerShirtNumberSelector"></asp:TextBox>
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
                            <asp:HiddenField ID="hfHomeCoachLineupId" runat="server"  />
                        </td>
                        <td colspan="3">
                            <UaFootball:AutocompleteTextBox ID="actbAwayCoach" runat="server" Placeholder="Луи ван Гаал" BehaviorId="actbAwayCoach" AutocompleteType="Coach" />
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
                                            <UaFootball:AutocompleteTextBox runat="server" ID="actbEventPlayer1" AutocompleteType="Player" Text='<%#FormatName(Eval("Player1.First_Name"),Eval("Player1.Last_Name"),Eval("Player1.Display_Name"))%>' Value='<%#Eval("Player1_Id") %>' />
                                        </td>

                                        <td valign="top">
                                            <UaFootball:AutocompleteTextBox runat="server" ID="actbEventPlayer2" AutocompleteType="Player" Text='<%#FormatName(Eval("Player2.First_Name"),Eval("Player2.last_Name"),Eval("Player2.Display_Name"))%>' Value='<%#Eval("Player2_Id") %>'/>
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
                <asp:TextBox TextMode="MultiLine" runat="server" ID="tbSources" Width="400"></asp:TextBox>
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

