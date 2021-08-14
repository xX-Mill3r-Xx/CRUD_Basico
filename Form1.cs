using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.IO;

namespace CRUD_Basico
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Carregamento da form
            string bancoDados = Application.StartupPath + @"\db\banco.sdf";
            string strConection = @"DataSource = " + bancoDados + "; Password = '1234'";
            SqlCeEngine db = new SqlCeEngine(strConection);
            if (File.Exists(bancoDados))
            {
                lb_Status.ForeColor = Color.Green;
                lb_Status.Text = "Conectado";
                CriarTabela.Enabled = false;
                tsb_Manutencao.Enabled = false;
            }

            // passa os campos pra false;
            
            txtId.Enabled = false;
            tsbNovo.Enabled = true;
            tsbSalvar.Enabled = false;
            tsbCancelar.Enabled = false;
            tsbExcluir.Enabled = false;
            tst_Id.Enabled = false;
            tsbBuscar.Enabled = false;
            txtNome.Enabled = false;
            txtEndereco.Enabled = false;
            mskCep.Enabled = false;
            txtBairro.Enabled = false;
            txtCidade.Enabled = false;
            txt_Uf.Enabled = false;
            mskTelefone.Enabled = false;
            txt_Email.Enabled = false;
        }

        private void tsb_Manutencao_Click(object sender, EventArgs e)
        {
            // Cria a Data Base do sistema
            #region
            string bancoDados = Application.StartupPath + @"\db\banco.sdf";
            string strConection = @"DataSource = " + bancoDados + "; Password = '1234'";
            SqlCeEngine db = new SqlCeEngine(strConection);
            if (!File.Exists(bancoDados))
            {
                db.CreateDatabase();
                lb_Status.ForeColor = Color.Green;
                lb_Status.Text = "DataBase Criada";
                tsb_Manutencao.Enabled = false;
            }
            db.Dispose();
            SqlCeConnection conexao = new SqlCeConnection(strConection);
            try
            {
                conexao.Open();
                lb_Status.ForeColor = Color.Green;
                lb_Status.Text = "Conexão Estabelecida";
            }
            catch(Exception ex)
            {
                lb_Status.ForeColor = Color.Red;
                lb_Status.Text = "Falha na conexão" + ex.Message;
            }
            finally
            {
                conexao.Close();
            }
            #endregion
        }

        private void tsbNovo_Click(object sender, EventArgs e)
        {
            // passa os campos pra true;

            tsbNovo.Enabled = false;
            txtId.Enabled = true;
            tsbSalvar.Enabled = true;
            tsbCancelar.Enabled = true;
            tsbExcluir.Enabled = false;
            tst_Id.Enabled = false;
            tsbBuscar.Enabled = false;
            txtNome.Enabled = true;
            txtEndereco.Enabled = true;
            mskCep.Enabled = true;
            txtBairro.Enabled = true;
            txtCidade.Enabled = true;
            txt_Uf.Enabled = true;
            mskTelefone.Enabled = true;
            txt_Email.Enabled = true;
            txtNome.Focus();
            tsbNovo.Enabled = true;
        }

        private void tsbSalvar_Click(object sender, EventArgs e)
        {
            // Salva os dados no sistema;
            #region
            string bancoDados = Application.StartupPath + @"\db\banco.sdf";
            string strConection = @"DataSource = " + bancoDados + "; Password = '1234'";
            SqlCeConnection conexao = new SqlCeConnection(strConection);
            try
            {
                conexao.Open();
                SqlCeCommand comando = new SqlCeCommand();
                comando.Connection = conexao;
                int Tid = new Random(DateTime.Now.Millisecond).Next(0, 1000);
                txtId.Text = Tid.ToString();
                string nome = txtNome.Text;
                string endereco = txtEndereco.Text;
                string cep = mskCep.Text;
                string bairro = txtBairro.Text;
                string cidade = txtCidade.Text;
                string uf = txt_Uf.Text;
                string telefone = mskTelefone.Text;
                string email = txt_Email.Text;
                if(nome == "" && email == "")
                {
                    lb_Status.ForeColor = Color.Red;
                    lb_Status.Text = "Os campos não podem ser vazios";
                    return;
                }
                
                comando.CommandText = "INSERT INTO cliente VALUES(" + Tid + ", '" + nome + "', '" + endereco + "', '" + cep + "', '" + bairro + "', '" + cidade + "', '" + uf + "', '" + telefone + "', '" + email + "')";
                comando.ExecuteNonQuery();
                lb_Status.ForeColor = Color.Green;
                lb_Status.Text = "Registro incluído na base de dados do sistema";
                comando.Dispose();
            }
            catch(Exception ex)
            {
                lb_Status.ForeColor = Color.Red;
                lb_Status.Text = "Falha na execução" + ex.Message;
            }
            finally
            {
                conexao.Close();
            }
            #endregion
        }

        private void CriarTabela_Click(object sender, EventArgs e)
        {
            string bancoDados = Application.StartupPath + @"\db\banco.sdf";
            string strConection = @"DataSource = " + bancoDados + "; Password = '1234'";
            SqlCeConnection conexao = new SqlCeConnection(strConection);
            try
            {
                conexao.Open();
                SqlCeCommand comando = new SqlCeCommand();
                comando.Connection = conexao;
                comando.CommandText = "CREATE TABLE cliente (id INT NOT NULL PRIMARY KEY, nome NVARCHAR(50), endereco NVARCHAR(50), cep NVARCHAR(50), bairro NVARCHAR(50), cidade NVARCHAR(50), uf NVARCHAR(2), telefone NVARCHAR(50), email NVARCHAR(50))";
                comando.ExecuteNonQuery();
                lb_Status.ForeColor = Color.Green;
                lb_Status.Text = "Tabela Criada";
                CriarTabela.Enabled = false;
                comando.Dispose();
            }
            catch (Exception ex)
            {
                lb_Status.ForeColor = Color.Red;
                lb_Status.Text = "Falha na execução" + ex.Message;
            }
            finally
            {
                conexao.Close();
            }
        }

        private void tsbCancelar_Click(object sender, EventArgs e)
        {
            tsbNovo.Enabled = true;
            tsbSalvar.Enabled = false;
            tsbCancelar.Enabled = false;
            tsbExcluir.Enabled = false;
            tst_Id.Enabled = true;
            tsbBuscar.Enabled = true;
            txtNome.Enabled = false;
            txtEndereco.Enabled = false;
            mskCep.Enabled = false;
            txtBairro.Enabled = false;
            txtCidade.Enabled = false;
            txt_Uf.Enabled = false;
            mskTelefone.Enabled = false;
            txt_Email.Enabled = false;
            txtId.Text = "";
            txtNome.Text = "";
            txtEndereco.Text = "";
            mskCep.Text = "";
            txtBairro.Text = "";
            txtCidade.Text = "";
            txt_Uf.Text = "";
            mskTelefone.Text = "";
            txt_Email.Text = "";
        }

        private void tsbExcluir_Click(object sender, EventArgs e)
        {
            string bancoDados = Application.StartupPath + @"\db\banco.sdf";
            string strConection = @"DataSource = " + bancoDados + "; Password = '1234'";
            SqlCeConnection conexao = new SqlCeConnection(strConection);
            try
            {
                conexao.Open();
                SqlCeCommand comando = new SqlCeCommand();
                comando.Connection = conexao;
                int id = int.Parse(txtId.Text);
                comando.CommandText = "DELETE FROM cliente WHERE id = '" + id + "'";
                comando.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                lb_Status.ForeColor = Color.Red;
                lb_Status.Text = "Falha na execução" + ex.Message;
            }
            finally
            {
                conexao.Close();
            }

            tsbNovo.Enabled = true;
            tsbSalvar.Enabled = false;
            tsbCancelar.Enabled = false;
            tsbExcluir.Enabled = false;
            tst_Id.Enabled = true;
            tsbBuscar.Enabled = true;
            txtNome.Enabled = false;
            txtEndereco.Enabled = false;
            mskCep.Enabled = false;
            txtBairro.Enabled = false;
            txtCidade.Enabled = false;
            txt_Uf.Enabled = false;
            mskTelefone.Enabled = false;
            txt_Email.Enabled = false;
            txtId.Text = "";
            txtNome.Text = "";
            txtEndereco.Text = "";
            mskCep.Text = "";
            txtBairro.Text = "";
            txtCidade.Text = "";
            txt_Uf.Text = "";
            mskTelefone.Text = "";
            txt_Email.Text = "";
        }

        private void tsbBuscaPorId_Click(object sender, EventArgs e)
        {
            tst_Id.Enabled = true;
            tsbBuscar.Enabled = true;
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            lista.Rows.Clear();
            string bancoDados = Application.StartupPath + @"\db\banco.sdf";
            string strConection = @"DataSource = " + bancoDados + "; Password = '1234'";
            SqlCeConnection conexao = new SqlCeConnection(strConection);
            try
            {
                string query = "SELECT * FROM cliente";
                string id = tst_Id.Text;
                if (id != "")
                {
                    query = "SELECT * FROM cliente WHERE id LIKE '%" + id + "%'";
                }
                
                DataTable dados = new DataTable();
                SqlCeDataAdapter adapter = new SqlCeDataAdapter(query, strConection);
                conexao.Open();
                adapter.Fill(dados);
                foreach(DataRow linha in dados.Rows)
                {
                    lista.Rows.Add(linha.ItemArray);
                }
            }
            catch (Exception ex)
            {
                lb_Status.ForeColor = Color.Red;
                lb_Status.Text = "Falha na execução" + ex.Message;
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}
