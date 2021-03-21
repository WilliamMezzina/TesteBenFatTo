<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Log.aspx.cs" Inherits="TesteBenFatoo.Log" %>

<!DOCTYPE html>

<script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
<script src="Scripts/jquery.dynDateTime.min.js" type="text/javascript"></script>
<script src="Scripts/calendar-en.min.js" type="text/javascript"></script>
<link href="Styles/calendar-blue.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(document).ready(function () {
        $("#<%=txtData.ClientID %>").dynDateTime({
            showsTime: true,
            ifFormat: "%Y/%m/%d %H:%M",
            daFormat: "%l;%M %p, %e %m, %Y",
            align: "BR",
            electric: false,
            singleClick: false,
            displayArea: ".siblings('.dtcDisplayArea')",
            button: ".next()"
        });
    });
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Pesquisar</title>
</head>
<body>
    <form runat="server">
        <div>
            <asp:Label Text="IP:" runat="server" />
            <asp:TextBox ID="txtIP" runat="server" MaxLength="15"/><br />
            <asp:Label Text="Usuário:" runat="server" />
            <asp:TextBox ID="txtUsuario" runat="server" /><br />
            <asp:Label Text="Data/Hora:" runat="server" />
            <asp:TextBox ID="txtData" runat="server" /><img src="calender.png" /><br />
        </div>
        <div>
            <asp:Label Text="Comando:" runat="server" />
            <asp:TextBox ID="txtComando" runat="server" /><br />        
            <asp:Label Text="Site:" runat="server" />
            <asp:TextBox ID="txtSite" runat="server" Width="200"/><br />
            <asp:Label Text="Protocolo:" runat="server" />
            <asp:TextBox ID="txtProtocolo" runat="server" /><br />
            <asp:Label Text="Estado anterior:" runat="server" />
            <asp:TextBox ID="txtPreviousState" runat="server" /><br />
            <asp:Label Text="Estado Atual:" runat="server" />
            <asp:TextBox ID="txtActualState" runat="server" /><br />
            <asp:Label Text="Destino:" runat="server" />
            <asp:TextBox ID="txtDestino" runat="server" /><br />
            <asp:Label Text="User agente:" runat="server" />
            <asp:TextBox ID="txtUserAgent" runat="server" Width="600"/><br />


            <asp:Button ID="btSalvar" Text="Salvar" runat="server" OnClick="btSalvar_Click" /><br />
            <asp:Button ID="btVoltar" Text="Voltar" runat="server" OnClick="btVoltar_Click"/>
            <br />            
        </div>
    </form>
</body>
</html>