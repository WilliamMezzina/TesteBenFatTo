<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Listar.aspx.cs" Inherits="TesteBenFatoo.Listar" %>

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
            <asp:TextBox ID="txtIP" runat="server" /><br />
            <asp:Label Text="Usuário:" runat="server" />
            <asp:TextBox ID="txtUsuario" runat="server" /><br />
            <asp:Label Text="Data/Hora:" runat="server" />
            <asp:TextBox ID="txtData" runat="server" /><img src="calender.png" /><br />

            <asp:Button ID="Inserir" Text="Inserir Log Manual" runat="server" OnClick="Inserir_Click" /><br />

            <asp:Label Text="Para inserir um batch, use o campo abaixo" runat="server" /><br />
            <asp:FileUpload ID="fluBatch" runat="server" /><br />
            <asp:Button ID="InserirBatch" Text="Inserir Log Batch" runat="server" OnClick="InserirBatch_Click" /><br />
            <asp:Button ID="btPesquisar" Text="Pesquisar" runat="server" OnClick="btPesquisar_Click" />
            <br />
            <asp:GridView ID="gdvLogs" runat="server" Width="250"
                AllowPaging="true" PageSize="10" OnRowCommand="gdvLogs_RowCommand"
                AutoGenerateColumns="false" OnPageIndexChanging="gdvLogs_PageIndexChanging">
                <Columns>
                    <asp:ButtonField Text="Abrir" ButtonType="Button" CommandName="Abrir" />
                    <asp:TemplateField Visible ="false">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server"
                                Text='<%# Eval("Id")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="IP" Visible="true" HeaderText="IP" />
                    <asp:BoundField DataField="User" Visible="true" HeaderText="Usuário" />
                    <asp:BoundField DataField="HoraLog" Visible="true" HeaderText="Data/Hora" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
