<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormSignIn.aspx.cs" Inherits="BeautySalonWebView.FormSignIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Имя&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBoxFirstName" runat="server"></asp:TextBox>
        <br />
        Фамилия
        <asp:TextBox ID="TextBoxSecondName" runat="server"></asp:TextBox>
        <br />
        Пароль&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBoxPassword" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="ButtonSignIn" runat="server" OnClick="ButtonSignIn_Click" Text="Вход" />
        <asp:Button ID="ButtonSignUp" runat="server" OnClick="ButtonSignUp_Click" Text="Регистрация" />
    
    </div>
    </form>
</body>
</html>
