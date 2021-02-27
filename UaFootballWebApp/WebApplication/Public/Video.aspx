<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Video.aspx.cs" Inherits="UaFootball.WebApplication.Public.Video" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Видео</title>
        <script type="text/javascript" src="../Scripts/flowplayer-3.2.13.min.js"></script>
        <script type="text/javascript">
            function invokePlayer(webUrl) {
                var divPlayer = document.getElementById("player");
                var player = flowplayer();
                
                if (player == null) {
                    player = flowplayer("player", "<%=FLOWPLAYER_URL%>", webUrl);
                }
                else {
                    player.play(webUrl);
                }
            }
        </script>
    </head>
    <body>
        
        <table>
        <asp:Repeater ID="rptVideoSelection" runat="server">
            <ItemTemplate>
                <tr>
                    <td><%#Eval("Description")%></td>
                    <td><a class="video_link_selector" href='<%#Eval("URL")%>'>Играть >></a></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        </table>
        <div style="width:425px;height:300px;" id="player"></div>


        <script type="text/javascript">
            $(document).ready(function () {

                $(".video_link_selector").click(function()
                {
                    invokePlayer(this.href);
                    return false;
                });

                var autoplay = <%=AUTOPLAY%>;
                var defaultVideoWebURL = "<%=DEFAULT_VIDEO_URL%>";
                if (autoplay)
                {
                    invokePlayer(defaultVideoWebURL);
                };
            });
        </script>
        
    </body>
</html>