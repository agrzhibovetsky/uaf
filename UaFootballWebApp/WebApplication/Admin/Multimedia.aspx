<%@ Page Title="" Language="C#" MasterPageFile="~/WebApplication/Site.master" AutoEventWireup="true" CodeBehind="Multimedia.aspx.cs" Inherits="UaFootball.WebApplication.Admin.Multimedia" %>
<%@ Register TagPrefix="ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<%@ Register TagPrefix="UaFootball" TagName="AutocompleteTextBox" Src="~/WebApplication/Controls/AutocompleteTextBox.ascx" %>
<%@ Register TagPrefix="UaFootball" TagName="AdminEventPopup" Src="~/WebApplication/Controls/AdminEventPopup.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .tmpAsyncFileUploadBugFix input
        {
            width:100%!important;
        }
    </style>

    <script type="text/javascript" src="../Scripts/flowplayer-3.2.13.min.js"></script>

    <script type="text/javascript">

        function onUploadComplete(sender, args) {
            var contentType = args.get_contentType();
            var fileName = args.get_fileName();
            var divPlayer = document.getElementById("player");
            var hfFileName = document.getElementById("hfFileName");
            var webUrl = '<%=TmpUploadWebPath%>' + fileName;
            hfFileName.value = fileName;
            if (contentType.indexOf("image") > -1) 
            {
                divPlayer.style.display = "none";
                document.getElementById("imgMultimedia").src = webUrl;
            }
            else 
            {
                divPlayer.style.display = "block";
                $("#vVideo")[0].src = webUrl;
                //var player = flowplayer();
                //if (player == null) {
                //    player = flowplayer("player", "/UaFootball/WebApplication/Multimedia/flowplayer-3.2.18.swf", webUrl);
                //}
                //else {
                //    player.play(webUrl);
                //}
            };
        }

        function openEditEventPopup(sender) {
            var eventId = $(sender).attr("eventId");
            $("#hdnAdminEventPopupEventId").val(eventId);
            $.ajax({
                url: "<%=AdminApiPath%>/GetMatchEvent?matchEventId="+eventId,
                data: "",
                dataType: "json",
                success: function (data) {
                    $(".eventFlagsDiv").hide();
                    var eventFlagsDiv = $(".eventFlagsDiv[eventType='" + data.event_Cd.toUpperCase() + "']");
                    eventFlagsDiv.show();
                    actbPlayer1.tb.val(data.player1.first_Name + " " + data.player1.last_Name);
                    actbPlayer1.hf.val(data.player1_Id);

                    actbPlayer2.getAdditionalParams = function () {
                        return "&eventId=" + eventId;        
                    }

                    if (data.player2_Id != null) {
                        actbPlayer2.tb.val(data.player2.first_Name + " " + data.player2.last_Name);
                        actbPlayer2.hf.val(data.player2_Id);
                    }
                    else {
                        actbPlayer2.tb.val("");
                        actbPlayer2.hf.val("");
                    }

                    $("#eventFlagsContainer input").prop("checked", "");
                    var flagCheckboxes = eventFlagsDiv.find("input");
                    for (var i = 0; i < flagCheckboxes.length; i++) {
                        var checkbox = $(flagCheckboxes[i]);
                        var checked = (checkbox.val() & data.eventFlags) > 0 ? "checked" : "";
                        checkbox.prop("checked", checked);
                    }
                    $find("mpe1").show();
                }
            });
            
            
        }

        function closeEditEventPopup() {
            $find("mpe1").hide();
        }

        function saveEvent() {
            var eventId = $("#hdnAdminEventPopupEventId").val();
            var player2Id = actbPlayer2.hf.val();
            var eventFlags = 0;
            var flagCheckboxes = $("#eventFlagsContainer input");
            for (var i = 0; i < flagCheckboxes.length; i++) {
                var checkbox = $(flagCheckboxes[i]);
                if (checkbox.prop("checked")) eventFlags = eventFlags | checkbox.val();
            }

            $.ajax({
                url: "<%=AdminApiPath%>/UpdateMatchEvent?matchEventId=" + eventId + "&player2Id=" + player2Id + "&flags=" + eventFlags,
                data: "",
                dataType: "json",
                success: function (data) {
                    $find("mpe1").hide();
                }
            });
            
                    
        }

        function checkTags() {
            var mmMode = $("#ddlMultimediaSubType").val();
            var ret = true;
            if (mmMode == "PL") {
                var footballerRows = $(".tagRow[mmtype='Футболист']");
                var clubRows = $(".tagRow[mmtype='Клуб']");
                if (footballerRows.length == 0 || clubRows.length == 0)
                    ret = confirm("Футболист или клуб не заданы, сохранить?")
            }

            return ret;
        }
               
        
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajax:ToolkitScriptManager runat="Server" />
<asp:HiddenField ID="hfFileName" runat="server" ClientIDMode="Static" />    
<table cellpadding="5" width="100%">
    <tr>
        <td width="200">Тип мультимедиа:</td>
        <td align="left"><asp:DropDownList ID="ddlMultimediaSubType" runat="server" ClientIDMode="Static"
                onselectedindexchanged="ddlMultimediaSubType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>Файл:</td>
        <td align="left">
            <ajax:AsyncFileUpload ID="afuUploader" runat="server" CssClass="tmpAsyncFileUploadBugFix" 
                UploaderStyle="Traditional" 
                onuploadedcomplete="afuUploader_UploadedComplete" OnClientUploadComplete="onUploadComplete"/>
        </td>
    </tr>

    <tr>
        <td>Тэги</td>
        <td align="left">
            <asp:UpdatePanel ID="upTags" runat="server">
                <ContentTemplate>
                    <table cellpadding="3" width="100%" style="border: 1px solid gray" >
                    <asp:Repeater ID="rptTags" runat="server" OnItemCommand="rptTags_ItemCommand" OnItemDataBound="rptTags_ItemDataBound">
                        <HeaderTemplate>
                            <tr>
                                <td>
                                    Тип
                                </td>
                                <td>
                                    Значение
                                </td>
                                <td>
                                    Удалить
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="tagRow" mmType="<%#Eval("Type")%>">
                                <td>
                                    <%#Eval("Type")%>
                                </td>
                                <td>
                                    <%#Eval("Description")%>
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/WebApplication/Images/delete.gif" CommandArgument='<%#Eval("tmpId")%>' />
                                </td>
                                <td>
                                    <a href="javascript:void(0)" class="aEventEditPopup" onclick="javascript:openEditEventPopup(this); return false;" eventId="<%#Eval("MatchEvent_ID")%>">
                                        <asp:Image ID="btnEventEdit" runat="server" ImageUrl="~/WebApplication/Images/edit.gif" />
                                    </a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                        
                    </table>
                </ContentTemplate>
                <Triggers>
                   
                    <asp:AsyncPostBackTrigger ControlID="btnAddTag" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td>Добавить тэг</td>
        <td align="left">
            <asp:UpdatePanel runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <asp:DropDownList runat="server" ID="ddlTagType" ClientIDMode="Static" AutoPostBack="true" onselectedindexchanged="ddlTagType_SelectedIndexChanged"></asp:DropDownList>
                    <asp:UpdatePanel runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <asp:DropDownList runat="server" ID="ddlTagValue" ClientIDMode="Static" ></asp:DropDownList>
                            <asp:TextBox ID="tbObjectId" runat="server" OnTextChanged="tbObjectId_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlTagType" />
                        </Triggers>
                    </asp:UpdatePanel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlMultimediaSubType" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:Button runat="server" ID="btnAddTag" Text="Добавить" OnClick="btnAdd_Click" />
            </td>
    </tr>
    <tr>
        <td colspan="2">
            Источник: <asp:TextBox ID="tbSource" runat="server" MaxLength="50"></asp:TextBox>
            Автор: <asp:TextBox ID="tbAuthor" runat="server" MaxLength="50"></asp:TextBox>
            Дата фото/видео: <asp:TextBox ID="tbPhotoDate" runat="server" MaxLength="50"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            Описание: <asp:TextBox ID="tbDescription" runat="server" Width="500" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            Флаги: <asp:CheckBoxList ID="cbl1" runat="server">
                       
                   </asp:CheckBoxList>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Image ID="imgMultimedia" runat="server" ClientIDMode="Static" style="max-width:90%" />
            <div style="display: none;" id="player">
                <video id="vVideo" style="width:600px" controls="true"></video>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnSave" runat="server" Text="Сохранить" ValidationGroup="trueValidation" OnClientClick="return checkTags();" OnClick="btnSave_Click" />
            <asp:Button ID="btnDelete" runat="server" Text="Удалить" ValidationGroup="trueValidation" OnClick="btnDelete_Click" Visible="false" />
            <asp:Label ID="lblError" Font-Bold="true" CssClass="editFormError" runat="server"></asp:Label>
        </td>
    </tr>
</table>
    <asp:LinkButton ID="lbOpenAdminEventPopup" runat="server" />
    <ajax:ModalPopupExtender ID="mpe1" BehaviorID="mpe1" ClientIDMode="Static" runat="server" BackgroundCssClass="modalPopupBackground" TargetControlID="lbOpenAdminEventPopup" PopupControlID="pAdminEventPopup"></ajax:ModalPopupExtender>
    <asp:Panel id="pAdminEventPopup" runat="server">
        <UaFootball:AdminEventPopup ID="evPopup1" runat="server" />
    </asp:Panel>
</asp:Content>
