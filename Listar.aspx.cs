using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TesteBenFatoo.BLL;
using TesteBenFatoo.Model;

namespace TesteBenFatoo
{
    public partial class Listar : System.Web.UI.Page
    {
        private List<LogModel> Lista
        {
            set { ViewState["logs"] = value; }
            get { return ViewState["logs"] as List<LogModel>; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void Inserir_Click(object sender, EventArgs e)
        {
            Response.Redirect("Log.aspx");
        }

        protected void gdvLogs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Abrir")
            {
                Label _teste = gdvLogs.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblId") as Label;
                Response.Redirect("Log.aspx?id=" + _teste.Text);
            }
        }

        protected void InserirBatch_Click(object sender, EventArgs e)
        {
            if(fluBatch.HasFile && fluBatch.FileName.EndsWith(".log"))
            {
                new LogBLL().InserirLogViaBatch(fluBatch.FileContent);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Sucesso", "alert('Batch Inserido com sucesso')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Sucesso", "alert('O Arquivo selecionado não é válido')", true);
            }
        }

        protected void btPesquisar_Click(object sender, EventArgs e)
        {
            LogModel log = new LogModel();

            log.Ip = txtIP.Text;
            log.User = txtUsuario.Text;
            try
            {
                log.HoraLog = DateTime.Parse(Request.Form[txtData.UniqueID]);
            }
            catch
            {
                log.HoraLog = null;
            }
            Lista = new LogBLL().Listar(log);
            gdvLogs.DataSource = Lista;
            gdvLogs.DataBind();
        }

        protected void gdvLogs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvLogs.PageIndex = e.NewPageIndex;
            gdvLogs.DataSource = Lista;
            gdvLogs.DataBind();
        }
    }
}