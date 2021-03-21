using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TesteBenFatoo.BLL;
using TesteBenFatoo.Model;

namespace TesteBenFatoo
{
    public partial class Log : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if(Request.QueryString["id"] != null && !string.IsNullOrEmpty(Request.QueryString["id"].ToString()))
                {
                    LogModel log = new LogBLL().Listar(new LogModel() { Id = Convert.ToInt32(Request.QueryString["id"].ToString()) }).FirstOrDefault();

                    txtIP.Text = log.Ip;
                    txtUsuario.Text = log.User;
                    txtData.Text = log.HoraLog.Value.ToString("dd/MM/yyyy hh:mm:ss") ;
                    txtComando.Text = log.Comando;
                    txtSite.Text = log.Site;
                    txtProtocolo.Text = log.Protocolo;
                    txtActualState.Text = log.ActualState;
                    txtPreviousState.Text = log.PreviousState;
                    txtDestino.Text = log.Destino;
                    txtUserAgent.Text = log.UserAgent;

                    btSalvar.Enabled = true;
                    btSalvar.Visible = false;
                }
                    
            }
        }

        protected void btSalvar_Click(object sender, EventArgs e)
        {
            LogModel log = new LogModel();

            log.Ip = txtIP.Text;
            log.User = txtUsuario.Text;
            if (DateTime.TryParse(txtData.Text, out DateTime date))
                log.HoraLog = DateTime.Parse(txtData.Text);
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Erro", "alert('A data informada não é válida')", true);
                return;
            }
            log.Comando = txtComando.Text;
            log.Site = txtSite.Text;
            log.Protocolo = txtProtocolo.Text;
            log.ActualState = txtActualState.Text;
            log.PreviousState = txtPreviousState.Text;
            log.Destino = txtDestino.Text;
            log.UserAgent = txtUserAgent.Text;

            LogBLL logBLL = new LogBLL();

            string resposta = logBLL.Validar(log);

            if(!string.IsNullOrEmpty(resposta))
            {
                
                resposta = "Não foi possível salvar devido aos seguintes erros \\n" + resposta;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Erro", "alert('"+resposta+"')", true);
                return;
            }
            else
            {
                if (logBLL.Salvar(log) > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Sucesso", "alert('Log salvo com sucesso')", true);
                    btVoltar_Click(null, null);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Erros", "alert('Erro para salvar não identificado, favor informar a equipe técnica')", true);
                    return;
                }

            }
        }

        protected void btVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Listar.aspx");
        }
    }
}