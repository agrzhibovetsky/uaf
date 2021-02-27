<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NameSymbols.aspx.cs" Inherits="UaFootball.WebApplication.Temp.NameSymbols" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
        <tr>
                    <td>
                       Symbol 
                    </td>
                    <td>
                        Count
                    </td>
                </tr>
        <asp:ListView ID="lvSymb" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <%#Eval("Key")%>
                    </td>
                    <td>
                        <%#Eval("Value")%>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
        </table>
    </div>

    

    <asp:Label ID="lbl" runat="server"></asp:Label>
    </form>
</body>
</html>
