<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sql.aspx.cs" Inherits="LiraTaxi.Api.Sql" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <br />
        Enter query:<br />
        <asp:TextBox TextMode="MultiLine" Rows="10" Columns="100" ID="Query" runat="server"></asp:TextBox><br />
       
       <asp:RadioButton ID="rbtnSelect" runat="server" GroupName="btn" /> Select
       <asp:RadioButton ID="rbtnExecute" runat="server" GroupName="btn" />  Execute
         <asp:Button ID="btnExecute" runat="server" OnClick="btnExecute_Click" Text="Execute" /><br />
        <asp:Label ID="lblMessage" runat="server"></asp:Label><br /><br />
        <asp:GridView ID="grid" runat="server"></asp:GridView>
    </div>
    </form>
</body>
</html>
