<%@ Page Title="" Language="C#" MasterPageFile="~/WebApplication/Site.master" AutoEventWireup="true" CodeBehind="Multimedia.aspx.cs" Inherits="UaFootball.WebApplication.Admin.Multimedia" %>
<%@ Register TagPrefix="ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<%@ Register TagPrefix="UaFootball" TagName="AutocompleteTextBox" Src="~/WebApplication/Controls/AutocompleteTextBox.ascx" %>
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

        function onTagAdded() {
            var selectedTypeOption = $("#ddlTagType")[0].selectedOptions[0];
            if ($(selectedTypeOption).attr("isEventType") == "true") {
                var selectedValueOption = $("#ddlTagValue")[0].selectedOptions[0];
                var selectedEventType = $(selectedValueOption).attr("eventType");
                console.log(selectedEventType);
            }
            
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajax:ToolkitScriptManager runat="Server" />
<asp:HiddenField ID="hfFileName" runat="server" ClientIDMode="Static" />    
<table cellpadding="5" width="100%">
    <tr>
        <td width="200">Тип мультимедиа:</td>
        <td align="left"><asp:DropDownList ID="ddlMultimediaSubType" runat="server" 
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
                    <asp:Repeater ID="rptTags" runat="server" OnItemCommand="rptTags_ItemCommand">
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
                            <tr>
                                <td>
                                    <%#Eval("Type")%>
                                </td>
                                <td>
                                    <%#Eval("Description")%>
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/WebApplication/Images/delete.gif" CommandArgument='<%#Eval("tmpId")%>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                        
                    </table><asp:Button ID="btnTest" runat="server" />
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
            <asp:Button runat="server" ID="btnAddTag" Text="Добавить" OnClientClick="onTagAdded()" OnClick="btnAdd_Click" />
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
            <div class="eventFlagsDiv" eventType="G" style="display:noned;">
                <asp:CheckBoxList ID="cblGoalFlags" runat="server"></asp:CheckBoxList>
            </div>
            <div class="eventFlagsDiv" eventType="P" style="display:noned;">
                <asp:CheckBoxList ID="cblPenaltyFlags" runat="server"></asp:CheckBoxList>
            </div>
            <div id="divGoalAssist" style="display:block">
                <UaFootball:AutocompleteTextBox runat="server" ID="actbAssistPlayer" BehaviorId="actbAssistPlayer" AutocompleteType="AssistPlayer" />
            </div>
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
            <asp:Button ID="btnSave" runat="server" Text="Сохранить" ValidationGroup="trueValidation" OnClick="btnSave_Click" />
            <asp:Button ID="btnDelete" runat="server" Text="Удалить" ValidationGroup="trueValidation" OnClick="btnDelete_Click" Visible="false" />
            <asp:Label ID="lblError" Font-Bold="true" CssClass="editFormError" runat="server"></asp:Label>
        </td>
    </tr>
</table>

</asp:Content>
