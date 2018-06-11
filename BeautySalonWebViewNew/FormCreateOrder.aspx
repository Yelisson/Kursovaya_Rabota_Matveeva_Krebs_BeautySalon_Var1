<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormCreateOrder.aspx.cs" Inherits="BeautySalonWebView.FormCreateOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="id" SortExpression="id" />
                <asp:BoundField DataField="serviceName" HeaderText="serviceName" SortExpression="serviceName" />
                <asp:BoundField DataField="price" HeaderText="price" SortExpression="price" />
                <asp:BoundField DataField="count" HeaderText="count" SortExpression="count" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="getListOrderServices" TypeName="BeautySalonWebView.FormCreateOrder"></asp:ObjectDataSource>
        <asp:Button ID="ButtonAdd" runat="server" OnClick="ButtonAdd_Click" Text="Добавить услугу" />
        <asp:Button ID="ButtonSave" runat="server" OnClick="ButtonSave_Click" Text="Сохранить" />
        <asp:Button ID="ButtonCancel" runat="server" OnClick="ButtonCancel_Click" Text="Вернуться" />
    </div>
    </form>
</body>
</html>
