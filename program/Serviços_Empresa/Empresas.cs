using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;

namespace Serviços_Empresa
{
    public partial class Empresas : Form
    {

        Conexao con = new Conexao();
        MySqlCommand cmdVerificar;
        MySqlDataReader reader;
        string sql;
        string Nome, Nif, Morada, Telefone, Email;

        public Empresas()
        {
            InitializeComponent();
        }     

        private void AtualizarDados()
        {
            con.AbrirConexao();
            cmdVerificar = new MySqlCommand("SELECT * FROM empresas WHERE Id_Conta = @Id_Conta", con.con);
            cmdVerificar.Parameters.AddWithValue("@Id_Conta", Program.Id_Conta);
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
                    Program.Id_Empresa = Convert.ToInt32(reader["Id_Empresa"]);
                }
            }
            con.FecharConexao();

            labelNome.Text = Nome;
            labelNif.Text = Nif;
            labelMorada.Text = Morada;
            labelTelefone.Text = Telefone;
            labelEmail.Text = Email;
        }

        private void Empresas_Load(object sender, EventArgs e)
        {
            AtualizarDados();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Serviços_Empresa form = new Serviços_Empresa();
            form.Closed += (s, args) => this.Close();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Pedido_empresa form = new Pedido_empresa();
            form.Closed += (s, args) => this.Close();
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string Nova_Morada;
            Nova_Morada = Interaction.InputBox("Introduza a nova morada");
            if (Nova_Morada == "")
            {
                MessageBox.Show("Voce não introduziu nenhuma morada");
                return;
            }

            con.AbrirConexao();
            sql = "Update empresas SET Morada = @Morada Where Id_Empresa = @Id_Empresa";
            cmdVerificar = new MySqlCommand(sql, con.con);
            cmdVerificar.Parameters.AddWithValue("@Morada", Nova_Morada);
            cmdVerificar.Parameters.AddWithValue("@Id_Empresa", Program.Id_Empresa);
            cmdVerificar.ExecuteNonQuery();
            con.FecharConexao();
            MessageBox.Show("Morada alterada com sucesso", "Morada alterada", MessageBoxButtons.OK);
            AtualizarDados();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string Novo_Telefone;
            Novo_Telefone = Interaction.InputBox("Introduza o novo telefone");
            if(Novo_Telefone == "")
            {
                MessageBox.Show("Voce não introduziu nenhum telefone");
                return;
            }

            con.AbrirConexao();
            sql = "Update empresas SET Telefone = @Telefone Where Id_Empresa = @Id_Empresa";
            cmdVerificar = new MySqlCommand(sql, con.con);
            cmdVerificar.Parameters.AddWithValue("@Telefone", Novo_Telefone);
            cmdVerificar.Parameters.AddWithValue("@Id_Empresa", Program.Id_Empresa);
            cmdVerificar.ExecuteNonQuery();
            con.FecharConexao();
            MessageBox.Show("Telefone alterado com sucesso", "Telefone alterado", MessageBoxButtons.OK);
            AtualizarDados();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");
            string Novo_Email;
            Novo_Email = Interaction.InputBox("Introduza o novo email");
            if (Novo_Email == "")
            {
                MessageBox.Show("Voce não introduziu nenhum email");
                return;
            }
            else
            {
                if (rg.IsMatch(Novo_Email))
                {
                    con.AbrirConexao();
                    sql = "Update empresas SET Email = @Email Where Id_Empresa = @Id_Empresa";
                    cmdVerificar = new MySqlCommand(sql, con.con);
                    cmdVerificar.Parameters.AddWithValue("@Email", Novo_Email);
                    cmdVerificar.Parameters.AddWithValue("@Id_Empresa", Program.Id_Empresa);
                    cmdVerificar.ExecuteNonQuery();
                    con.FecharConexao();
                    MessageBox.Show("Email alterado com sucesso", "Email alterado", MessageBoxButtons.OK);
                    AtualizarDados();
                }
                else
                {
                    MessageBox.Show("Introduza um email valido");
                    return;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Quer mesmo sair da conta?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Hide();
                Login form = new Login();
                form.Closed += (s, args) => this.Close();
                form.Show();
            }
        }
    }
}
