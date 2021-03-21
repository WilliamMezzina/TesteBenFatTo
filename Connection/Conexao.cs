using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TesteBenFatoo.Connection
{
    public class Conexao
    {
        //readonly string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=App_Data\TesteBenFatto;Integrated Security = SSPI;";
        //readonly string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=App_Data\TesteBenFatto.mdf;Integrated Security = SSPI";
        //providerName="System.Data.SqlClient
        public SqlConnection Connection;        

        public Conexao()
        {
            string conexao = System.Configuration.ConfigurationManager.ConnectionStrings["BancoInterno"].ConnectionString;
            Connection = new SqlConnection(conexao);
        }

        public void Abrir()
        {
            Connection.Open();
        }

        public void Fechar()
        {
            Connection.Close();
        }

        public ConnectionState Estado
        {
            get => Connection.State;
        }
    }
}