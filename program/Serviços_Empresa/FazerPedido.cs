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
    public partial class FazerPedido : Form
    {
        Conexao con = new Conexao();
        MySqlCommand cmdVerificar;
        MySqlDataReader reader;
        string sql;
        int id_servico, id_servicos_empresa;

        public FazerPedido()
        {
            InitializeComponent();
        }

        //pegar nos serviços que existem
        private void atualizardados()
        {
            con.AbrirConexao();
            sql = "SELECT * FROM servico";
            cmdVerificar = new MySqlCommand(sql, con.con); 
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmdVerificar;
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "Nome_Servico";
            con.FecharConexao();
        }

        //pegar no id da empresa
        private void id_empresa()
        {
            con.AbrirConexao();
            cmdVerificar = new MySqlCommand("SELECT * FROM empresas WHERE Nome = @Nome", con.con);
            cmdVerificar.Parameters.AddWithValue("@Nome", comboBox2.Text);
            reader = cmdVerificar.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Program.Id_Empresa = Convert.ToInt32(reader["Id_Empresa"]);
                }
            }
            con.FecharConexao();
        }

        private void FazerPedido_Load(object sender, EventArgs e)
        {
            atualizardados();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //pegar id do serviço
            con.AbrirConexao();
            cmdVerificar = new MySqlCommand("SELECT * FROM servico WHERE Nome_Servico = @Nome_Servico", con.con);
            cmdVerificar.Parameters.AddWithValue("@Nome_Servico", comboBox1.Text);
            reader = cmdVerificar.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    id_servico = Convert.ToInt32(reader["Id_Servico"]);
                }
            }
            con.FecharConexao();
            //Verificar se existe alguma empresa com o serviço
            con.AbrirConexao();
            cmdVerificar = new MySqlCommand("SELECT * FROM servicos_empresa WHERE Id_Servico = @Id_Servico", con.con);
            cmdVerificar.Parameters.AddWithValue("@Id_Servico", id_servico);
            reader = cmdVerificar.ExecuteReader();

            if (!reader.HasRows)
            {
                MessageBox.Show("Não existe nenhuma empresa com esse serviço.", "Pedimos desculpa", MessageBoxButtons.OK);
                label2.Visible = false;
                comboBox2.Visible = false;
                button2.Visible = false;
                label3.Visible = false;
                dateTimePicker1.Visible = false;
                button3.Visible = false;
                return;
            }
            label2.Visible = true;
            comboBox2.Visible = true;
            button2.Visible = true;
            label3.Visible = true;
            dateTimePicker1.Visible = true;
            button3.Visible = true;
            con.FecharConexao();
            //procurar empresas com o serviço
            con.AbrirConexao();
            sql = "SELECT Nome FROM empresas e,servicos_empresa se WHERE se.Id_Servico = @Id_Servico AND e.Id_Empresa = se.Id_Empresa";
            cmdVerificar = new MySqlCommand(sql, con.con);
            cmdVerificar.Parameters.AddWithValue("@Id_Servico", id_servico);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmdVerificar;
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox2.DataSource = dt;
            comboBox2.DisplayMember = "Nome";
            con.FecharConexao();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            id_empresa();
            //abrir o form para mostrar os dados da empresa
            Dados_Empresa form = new Dados_Empresa();
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Cliente form = new Cliente();
            form.Closed += (s, args) => this.Close();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            id_empresa();
            //verificar se a data não é igual ou inferior a data atual
            if (dateTimePicker1.Value <= DateTime.Now)
            {
                MessageBox.Show("Voce não pode reservar um pedido para uma data inferior ou igual a de hoje", "Erro", MessageBoxButtons.OK);
                return;
            }
            //pegar o Id_Servicos_Empresa
            con.AbrirConexao();
            cmdVerificar = new MySqlCommand("SELECT * FROM  servicos_empresa WHERE Id_Empresa = @Id_Empresa AND Id_Servico = @Id_Servico", con.con);
            cmdVerificar.Parameters.AddWithValue("@Id_Empresa", Program.Id_Empresa);
            cmdVerificar.Parameters.AddWithValue("@Id_Servico", id_servico);
            reader = cmdVerificar.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    id_servicos_empresa = Convert.ToInt32(reader["Id_Servico_Empresa"]);
                }
            }
            con.FecharConexao();
            //Verificar se não existe nenhum pedido para a mesma data
            con.AbrirConexao();
            cmdVerificar = new MySqlCommand("SELECT * FROM  pedido WHERE Data_Realizar = @Data_Realizar AND Id_Servicos_Empresa = @Id_Servicos_Empresa", con.con);
            cmdVerificar.Parameters.AddWithValue("@Data_Realizar", dateTimePicker1.Value);
            cmdVerificar.Parameters.AddWithValue("@Id_Servicos_Empresa", id_servicos_empresa);
            reader = cmdVerificar.ExecuteReader();

            if (reader.HasRows)
            {
                MessageBox.Show("A empresa ja tem um serviço nessa data", "Escolha outra data", MessageBoxButtons.OK);
                return;
            }
            con.FecharConexao();
            //registrar o pedido
            con.AbrirConexao();
            sql = "INSERT INTO pedido (Data_Realizar, Id_Clientes, Id_Servicos_Empresa) VALUES (@Data_Realizar, @Id_Clientes, @Id_Servicos_Empresa)";
            cmdVerificar = new MySqlCommand(sql, con.con);
            cmdVerificar.Parameters.AddWithValue("@Data_Realizar", dateTimePicker1.Value);
            cmdVerificar.Parameters.AddWithValue("@Id_Clientes", Program.Id_Cliente);
            cmdVerificar.Parameters.AddWithValue("@Id_Servicos_Empresa", id_servicos_empresa);
            cmdVerificar.ExecuteNonQuery();
            MessageBox.Show("Pedido realizado com sucesso", "Pedido bem sucessido", MessageBoxButtons.OK);
            con.FecharConexao();
            this.Hide();
            Cliente form = new Cliente();
            form.Closed += (s, args) => this.Close();
            form.Show();
        }
    }
}

