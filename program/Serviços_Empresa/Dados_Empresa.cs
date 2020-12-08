using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Serviços_Empresa
{
    public partial class Dados_Empresa : Form
    {
        Conexao con = new Conexao();
        MySqlCommand cmdVerificar;
        MySqlDataReader reader;
        string Nome, Nif, Morada, Telefone, Email;

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public Dados_Empresa()
        {
            InitializeComponent();
        }

        private void Dados_Empresa_Load(object sender, EventArgs e)
        {
            //Mostrar os dados da empresa
            con.AbrirConexao();
            cmdVerificar = new MySqlCommand("SELECT * FROM empresas WHERE Id_Empresa = @Id_Empresa", con.con);
            cmdVerificar.Parameters.AddWithValue("@Id_Empresa", Program.Id_Empresa);
            reader = cmdVerificar.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Nome = Convert.ToString(reader["Nome"]);
                    Nif = Convert.ToString(reader["Nif"]);
                    Morada = Convert.ToString(reader["Morada"]);
                    Telefone = Convert.ToString(reader["Telefone"]);
                    Email = Convert.ToString(reader["Email"]);
                }
            }
            con.FecharConexao();

            labelNome.Text = Nome;
            labelNif.Text = Nif;
            labelMorada.Text = Morada;
            labelTelefone.Text = Telefone;
            labelEmail.Text = Email;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
