<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/WebApplication/Site.master" CodeBehind="Videos.aspx.cs" Inherits="UaFootball.WebApplication.Public.Videos" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
        <link rel="Stylesheet" type="text/css" href='<%=JQueryUICssPath%>' />
         <script type="text/javascript" src='<%=JQueryPath%>'></script>
        <script type="text/javascript" src='<%=JQueryUIPath%>'></script>
    </asp:Content>

    <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

         <script type="text/javascript">
             $(document).ready(function () {
                 
                 $("#cbGoal").click(function ()
                 {

                     if ($("#cbGoal")[0].checked)
                     {
                         $("tr[videoType!='Пас']").show();
                     }
                     else
                     {
                         $("tr[videoType!='Пас']").hide();
                     }
                 });


                 $("#cbPass").click(function () {
                     if ($("#cbPass")[0].checked) {
                         $("tr[videoType='Пас']").show();
                     }
                     else {
                         $("tr[videoType='Пас']").hide();
                     }
                 });

                 $("#cbEurocups").click(function () {
                     if ($("#cbEurocups")[0].checked) {
                         $("tr[complevelcode='C']").show();
                     }
                     else {
                         $("tr[complevelcode='C']").hide();
                     }
                 });

                 $("#cbNational").click(function () {
                     if ($("#cbNational")[0].checked) {
                         $("tr[complevelcode='N']").show();
                     }
                     else {
                         $("tr[complevelcode='N']").hide();
                     }
                 });
             });
    </script>

    <div>
        Голы <input type="checkbox" id="cbGoal" checked="checked" />
        Пасы <input type="checkbox" id="cbPass" checked="checked"  />
        Сборная <input type="checkbox" id="cbNational" checked="checked"  />
        Еврокубки <input type="checkbox" id="cbEurocups" checked="checked"  /> <br /><br />
        <table style="width:100%">
           <asp:Repeater ID="rptVideos" runat="server">
               <HeaderTemplate>
                   <tr>
                       
                       <td width="100">Дата</td>
                       <td width="200">Матч</td>
                       <td width="50">Тип</td>
                       <td width="150">Чем</td>
                       <td width="150">Оценка</td>
                       <td width="250">Откуда</td>
                       <td>Прочее</td>
                   </tr>
               </HeaderTemplate>
               <ItemTemplate>
                   <tr compLevelCode="<%#Eval("MatchType")%>" videoType="<%#Eval("VideoType") %>">
                       
                       <td><%#FormatDate(Eval("MatchDate"))%></td>
                       <td><%#Eval("HomeTeam")%> - <%#Eval("AwayTeam")%></td>
                       <td><a href="<%# UaFootball.AppCode.PathHelper.GetFullWebPath("Multimedia", (string)Eval("File.FilePath"), (string)Eval("File.FileName"))%>"><%#Eval("VideoType")%></a></td>
                       <td>&nbsp;<%#FormatEventFlags((string)Eval("EventTypeCode"),(long)Eval("EventFlags"), (string)Eval("VideoType"), 1)%></td>
                       <td>&nbsp;<%#FormatEventFlags((string)Eval("EventTypeCode"),(long)Eval("EventFlags"), (string)Eval("VideoType"), 2)%></td>
                       <td>&nbsp;<%#FormatEventFlags((string)Eval("EventTypeCode"),(long)Eval("EventFlags"), (string)Eval("VideoType"), 3)%></td>
                       <td>&nbsp;<%#FormatEventFlags((string)Eval("EventTypeCode"),(long)Eval("EventFlags"), (string)Eval("VideoType"), 4)%></td>
                   </tr>
               </ItemTemplate>
           </asp:Repeater>
        </table>
    </div>
</asp:Content>
