using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serviços_Empresa
{
    class Conexao
    {
        string conec = "SERVER=localhost; DATABASE=serviços; UID=root; PWD=2003; PORT=;";
        public MySqlConnection con = null;

        public void AbrirConexao()
        {
            try
            {
                con = new MySqlConnection(conec);
                con.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void FecharConexao()
        {
            try
            {
                con = new MySqlConnection(conec);
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
