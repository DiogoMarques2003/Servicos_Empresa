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

namespace Serviços_Empresa
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Entrar_Click(object sender, EventArgs e)
        {
            Conexao con = new Conexao();
            Conexao con1 = new Conexao();
            Conexao con2 = new Conexao();
            MySqlCommand cmdVerificar;
            MySqlDataReader reader;

            if (txtEmail.Text == "") { MessageBox.Show("Prencha todos os comapos"); txtEmail.Focus(); return; }
            if (txtPassword.Text == "") { MessageBox.Show("Prencha todos os comapos"); txtPassword.Focus(); return; }

            con.AbrirConexao();
            con1.AbrirConexao();
            con2.AbrirConexao();
            cmdVerificar = new MySqlCommand("SELECT * FROM login WHERE Email_conta = @Email_conta AND Password_Conta = @Password_Conta", con.con);
            cmdVerificar.Parameters.AddWithValue("@Email_conta", txtEmail.Text);
            cmdVerificar.Parameters.AddWithValue("@Password_Conta", txtPassword.Text);
            reader = cmdVerificar.ExecuteReader();

            if(reader.HasRows)
            {
                while (reader.Read())
                {
                    Program.nome = Convert.ToString(reader["Nome_Conta"]);
                    Program.Id_Conta = Convert.ToInt32(reader["Id_Conta"]);
                }
                cmdVerificar = new MySqlCommand("SELECT * FROM clientes WHERE Id_Conta = @Id_Conta", con1.con);
                cmdVerificar.Parameters.AddWithValue("@Id_Conta", Program.Id_Conta);
                reader = cmdVerificar.ExecuteReader();
                if(reader.HasRows)
                {
                    MessageBox.Show("Bem-Vindo " + Program.nome + "!", "Login Efectuado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    Cliente form = new Cliente();
                    form.Closed += (s, args) => this.Close();
                    form.Show();
                }
                else
                {
                    cmdVerificar = new MySqlCommand("SELECT * FROM empresas WHERE Id_Conta = @Id_Conta", con2.con);
                    cmdVerificar.Parameters.AddWithValue("@Id_Conta", Program.Id_Conta);
                    reader = cmdVerificar.ExecuteReader();
                    if(reader.HasRows)
                    {
                        MessageBox.Show("Bem-Vindo " + Program.nome + "!", "Login Efectuado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        Empresas form = new Empresas();
                        form.Closed += (s, args) => this.Close();
                        form.Show();
                    }
                    else
                    {
                        MessageBox.Show("Bem-Vindo " + Program.nome + "!\nPorfavor acabe o seu registro", "Login Efectuado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        Tipo_Conta form = new Tipo_Conta();
                        form.Closed += (s, args) => this.Close();
                        form.Show();
                    }
                }
            }
            else
            {
                MessageBox.Show("Dados incorretos", "Erro no login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Text = "";
                txtEmail.Focus();
                txtPassword.Text = "";
            }
            con.FecharConexao();
            con1.FecharConexao();
            con2.FecharConexao();
        }

        private void Button_Criar_Conta_Click(object sender, EventArgs e)
        {
            this.Hide();
            Register form = new Register();
            form.Closed += (s, args) => this.Close();
            form.Show();
        }

        private void Button_Sair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza de fechar o programa?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
