<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminEventPopup.ascx.cs" Inherits="UaFootball.WebApplication.Controls.AdminEventPopup" %>
<%@ Register TagPrefix="UaFootball" TagName="AutocompleteTextBox" Src="~/WebApplication/Controls/AutocompleteTextBox.ascx" %>
<div id="adminEventPopupContainer" style="background-color:white;">
    <div style="padding:50px;">
        <div style="display:block">
            Игрок 1: <UaFootball:AutocompleteTextBox runat="server" ID="actbPlayer1" BehaviorId="actbPlayer1" AutocompleteType="EventPlayer" />
        </div>

        <div style="display:block">
            Игрок 2: <UaFootball:AutocompleteTextBox runat="server" ID="actbPlayer2" BehaviorId="actbPlayer2" AutocompleteType="EventPlayer" />
        </div>

        <div id="eventFlagsContainer">
            <div class="eventFlagsDiv" eventType="G" style="display:none;">
                <table>
                    <tr>
                        <td style="vertical-align:top;"><asp:CheckBoxList ID="cblGoalFlags1" runat="server"></asp:CheckBoxList></td>
                        <td style="vertical-align:top;"><asp:CheckBoxList ID="cblGoalFlags2" runat="server"></asp:CheckBoxList></td>
                        <td style="vertical-align:top;"><asp:CheckBoxList ID="cblGoalFlags3" runat="server"></asp:CheckBoxList></td>
                        <td style="vertical-align:top;"><asp:CheckBoxList ID="cblGoalFlags4" runat="server"></asp:CheckBoxList></td>
                    </tr>
                </table>
                    
            </div>

            <div class="eventFlagsDiv" eventType="P" style="display:none;">
            <asp:CheckBoxList ID="cblPenaltyFlags" runat="server"></asp:CheckBoxList>
        </div>
        </div>  
        
        <div style="text-align:center">
            <a href="javascript:closeEditEventPopup();">Close</a>
            &nbsp;&nbsp;&nbsp;
            <a href="javascript:saveEvent()">Save</a>
        </div>

        <input type="hidden" id="hdnAdminEventPopupEventId" />
    </div>    
</div>