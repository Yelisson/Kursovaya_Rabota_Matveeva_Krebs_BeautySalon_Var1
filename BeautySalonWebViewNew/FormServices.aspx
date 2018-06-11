<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormServices.aspx.cs" Inherits="BeautySalonWebView.FormServices" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="ButtonUpd" runat="server" Text="Обновить" OnClick="ButtonUpd_Click" />
        <asp:Button ID="ButtonDownloadPrice" runat="server" OnClick="ButtonDownloadPrice_Click" Text="Скачать" />
        <asp:Button ID="ButtonMail" runat="server" OnClick="ButtonMail_Click" Text="Отправить на почту" />
        <asp:GridView ID="dataGridView" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" ShowHeaderWhenEmpty="True">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />                
                <asp:CommandField ShowSelectButton="true" SelectText=">>" />
                <asp:BoundField DataField="ServiceName" HeaderText="ServiceName" SortExpression="ServiceName" />
                <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
            </Columns>
            <SelectedRowStyle BackColor="#CCCCCC" />
        </asp:GridView>
        <br />
        <br />
        <asp:Button ID="ButtonBack" runat="server" Text="Вернуться" OnClick="ButtonBack_Click" />
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetListAvailableForClient" TypeName="BeautySalonWebView.DataSources.ServiceDataSource"></asp:ObjectDataSource>
    
    </div>
    </form>
</body>
</html>
