using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using TesteBenFatoo.Connection;
using TesteBenFatoo.Model;

namespace TesteBenFatoo.BLL
{
    public class LogBLL
    {
        private Conexao _conexao;
        public LogBLL() { }

        /// <summary>
        /// Método para a validação de alguns campos
        /// os que não possuem validação ou está na tela (pois deveria atribuir o valo correto ao objetos)
        /// ou não é necessária uma validação
        /// </summary>
        /// <param name="_log"></param>
        /// <returns></returns>
        public string Validar(LogModel _log)
        {
            string retorno = "";

            if (!IPAddress.TryParse(_log.Ip, out IPAddress ip))
                retorno += "O IP não está em um formato válido (XXX.XXX.XXX.XXX)\\n";
            if(string.IsNullOrEmpty(_log.User))
                retorno += "Preencha o Usuário\\n";
            if (string.IsNullOrEmpty(_log.Comando))
                retorno += "Preencha o Comando\\n";
            if (string.IsNullOrEmpty(_log.Site))
                retorno += "Preencha o Site\\n";
            if (string.IsNullOrEmpty(_log.Protocolo))
                retorno += "Preencha o Protocolo\\n";
            if (!Int32.TryParse(_log.PreviousState, out int valor))
                retorno += "O Estado Anterior só aceita números\\n";
            if (_log.ActualState.Trim() != "-" && !Int32.TryParse(_log.ActualState, out valor))
                retorno += "O Estado Atual só aceita números ou UM traço\\n";

            return retorno;
        }

        /// <summary>
        /// Método para salvar o log
        /// </summary>
        /// <param name="_log">Objeto de log para ser salvo</param>
        /// <returns>Id do log</returns>
        public int Salvar(LogModel _log)
        {
            /**apesar de não parecer ter sentido
             * fiz essa alteração pois caso amanhã ou depois 
             * preciso de algo para alterar, só precisa criar o 
             * método de alterar e inserir a lógica aqui*/
            return Inserir(_log);
        }

        /// <summary>
        /// Método para inserir o log na base
        /// </summary>
        /// <param name="_log">Log para ser inserido na base</param>
        /// <returns>Id do log inserido</returns>
        private int Inserir(LogModel _log)
        {
            int id = 0;
            StringBuilder _sql = new StringBuilder();

            _sql.AppendLine("INSERT INTO Logs VALUES");
            _sql.AppendLine("(");
            _sql.AppendLine("@IP,");
            _sql.AppendLine("@USER,");
            _sql.AppendLine("@HORALOG,");
            _sql.AppendLine("@COMANDO,");
            _sql.AppendLine("@SITE,");
            _sql.AppendLine("@PROTOCOLO,");
            _sql.AppendLine("@PREVIOUS,");
            _sql.AppendLine("@ACTUAL,");
            _sql.AppendLine("@DESTINO,");
            _sql.AppendLine("@USERAGENT");
            _sql.AppendLine(")");
            _sql.AppendLine("SELECT @@IDENTITY;");
            _conexao = new Conexao();
            try
            {

                _conexao.Abrir();

                SqlCommand comando = new SqlCommand(_sql.ToString(), _conexao.Connection);


                comando.Parameters.AddWithValue("@IP", _log.Ip);
                comando.Parameters.AddWithValue("@USER", _log.User);
                comando.Parameters.AddWithValue("@HORALOG", _log.HoraLog);
                comando.Parameters.AddWithValue("@COMANDO", _log.Comando);
                comando.Parameters.AddWithValue("@SITE", _log.Site);
                comando.Parameters.AddWithValue("@PROTOCOLO", _log.Protocolo);
                comando.Parameters.AddWithValue("@PREVIOUS", _log.PreviousState);
                comando.Parameters.AddWithValue("@ACTUAL", _log.ActualState);
                if (string.IsNullOrEmpty(_log.Destino))
                    comando.Parameters.AddWithValue("@DESTINO", DBNull.Value);
                else
                    comando.Parameters.AddWithValue("@DESTINO", _log.Destino);
                if (string.IsNullOrEmpty(_log.UserAgent))
                    comando.Parameters.AddWithValue("@USERAGENT", DBNull.Value);
                else
                    comando.Parameters.AddWithValue("@USERAGENT", _log.UserAgent);
                id = Convert.ToInt32(comando.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_conexao.Estado == System.Data.ConnectionState.Open)
                    _conexao.Fechar();
            }
            return id;
        }

        /// <summary>
        /// Método para busca de logs, também será usado para o unitário
        /// </summary>
        /// <param name="_log">Objeto de log com filtros</param>
        /// <returns>Lista de logs referentes aos filtros</returns>
        public List<LogModel> Listar(LogModel _log)
        {
            List<LogModel> logs = new List<LogModel>();
            StringBuilder _sql = new StringBuilder();

            _sql.AppendLine("SELECT ");
            _sql.AppendLine("Id, ");
            _sql.AppendLine("IP, ");
            _sql.AppendLine("Usuario, ");
            _sql.AppendLine("HoraLog, ");
            _sql.AppendLine("Comando, ");
            _sql.AppendLine("Site, ");
            _sql.AppendLine("Protocolo, ");
            _sql.AppendLine("PreviousState, ");
            _sql.AppendLine("ActualState, ");
            _sql.AppendLine("Destino, ");
            _sql.AppendLine("UserAgent ");
            _sql.AppendLine("FROM Logs WHERE");


            _sql.AppendLine("(@ID IS NULL OR ID = @ID) AND ");
            _sql.AppendLine("(@IP IS NULL OR IP = @IP) AND ");
            _sql.AppendLine("(@USER IS NULL OR Usuario LIKE '%'+@USER+'%') AND ");
            _sql.AppendLine("(@HORALOG IS NULL OR HoraLog = @HORALOG) ");

            _conexao = new Conexao();
            try
            {
                _conexao.Abrir();

                SqlCommand comando = new SqlCommand(_sql.ToString(), _conexao.Connection);
                if (_log.Id > 0)
                    comando.Parameters.AddWithValue("@ID", _log.Id);
                else
                    comando.Parameters.AddWithValue("@ID", DBNull.Value);

                if(!string.IsNullOrEmpty(_log.Ip))
                    comando.Parameters.AddWithValue("@IP", _log.Ip);
                else
                    comando.Parameters.AddWithValue("@IP", DBNull.Value);

                if (!string.IsNullOrEmpty(_log.User))
                    comando.Parameters.AddWithValue("@USER", _log.User);
                else
                    comando.Parameters.AddWithValue("@USER", DBNull.Value);
                
                if(_log.HoraLog.HasValue)
                    comando.Parameters.AddWithValue("@HORALOG", _log.HoraLog);
                else
                    comando.Parameters.AddWithValue("@HORALOG", DBNull.Value);


                SqlDataReader reader = comando.ExecuteReader();

                while(reader.Read())
                {
                    //logs.Add(
                    LogModel log = new LogModel();
                    //{
                    log.Id = Convert.ToInt32(reader["ID"].ToString());
                    log.Ip = reader["IP"].ToString();
                    log.User = reader["Usuario"].ToString();
                    log.HoraLog = Convert.ToDateTime(reader["HoraLog"].ToString());
                    log.Comando = reader["Comando"].ToString();
                    log.Site = reader["Site"].ToString();
                    log.Protocolo = reader["Protocolo"].ToString();
                    log.PreviousState = reader["PreviousState"].ToString();
                    log.ActualState = reader["ActualState"].ToString();
                    log.Destino = reader["Destino"].ToString();
                    log.UserAgent = reader["UserAgent"].ToString();
                    //}
                    //);
                    logs.Add(log);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_conexao.Estado == System.Data.ConnectionState.Open)
                    _conexao.Fechar();
            }

            return logs;
        }

        /// <summary>
        /// Método que receberá o registro do batch
        /// e vao transformá-lo em um log
        /// </summary>
        /// <param name="_registroBatch">Linha do arquivo batch</param>
        /// <returns>Objeto de log</returns>
        public LogModel TratamentoRegistroBatch(string _registroBatch)
        {
            if (string.IsNullOrEmpty(_registroBatch)) return null;
            LogModel log = new LogModel();

            /**
             * Admito que não consegui entender muito o arquivo de log
             * Mas consegui pegar o básico dele
             * como ele não tem uma estrutura em linhas e colunas
             * observei quer era possível seperar em espaço e aspas
             * para o tratamento*/
            string[] separacaoAspas = _registroBatch.Split('\"');

            /**
             * Dentro da separação com aspas vou ter várias mini separações
             * em espaços que posso tratar separadamente,
             * para poupar memória vou usar o mesmo objeto
             */

            //Ip, usuário e HoraLog
            string[] separacaoEspaco = separacaoAspas[0].Split(' ');
            log.Ip = separacaoEspaco[0];
            log.User = (separacaoEspaco[1] + " " + separacaoEspaco[2]).Trim();
            Regex reg = new Regex(Regex.Escape(":"));
            DateTime dataRegistro = Convert.ToDateTime(reg.Replace(separacaoEspaco[3]," ",1).Replace("[", ""));
            //.Insert(3, ":")
            log.HoraLog = Convert.ToDateTime((dataRegistro.ToString() + " " + separacaoEspaco[4].Replace("]", "")));


            //Comando, site e protocolo
            separacaoEspaco = separacaoAspas[1].Split(' ');
            log.Comando = separacaoEspaco[0];
            log.Site = separacaoEspaco[1];
            log.Protocolo = separacaoEspaco[2];

            //Actual e previous state
            separacaoEspaco = separacaoAspas[2].Split(' ');
            log.PreviousState = separacaoEspaco[1];
            log.ActualState = separacaoEspaco[2];

            /**
             * Os campos de destino e user agente só existirão
             * no caso de o tamanho do objeto de separação de aspas
             * for maior que 3, nesse caso os insiro
             */
            if(separacaoAspas.Length > 3)
            {
                log.Destino = separacaoAspas[3];
                log.UserAgent = separacaoAspas[5];
            }

            return log;
        }


        /// <summary>
        /// Método utilizado para receber um conteudo de um arquivo .log
        /// </summary>
        /// <param name="_conteudoArquivo"></param>
        public void InserirLogViaBatch(Stream _conteudoArquivo)
        {
            using (StreamReader reader = new StreamReader(_conteudoArquivo))
            {
                LogModel log;
                while ((log = TratamentoRegistroBatch(reader.ReadLine())) != null)
                {
                    this.Salvar(log);
                }
            }
        }
    }
}