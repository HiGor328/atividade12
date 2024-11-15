using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Atividade_12
{
    public partial class Form1 : Form
    {
        Conexao db = new Conexao();

        public Form1()
        {
            InitializeComponent();
            LoadData();
        }

        private void dgvListar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            

        }

        private void LoadData()
        {
            db.OpenConnection();
            string query = "SELECT * FROM Produto";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, db.GetConnection());
            DataTable table = new DataTable();
            adapter.Fill(table);
            dgvListar.DataSource = table;
            db.CloseConnection();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
             CadastroATTcs cadastroATTcs = new CadastroATTcs();
            if (cadastroATTcs.ShowDialog() == DialogResult.OK)
            {
                LoadData(); // Recarrega os dados após adicionar
            }

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvListar.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvListar.SelectedRows[0].Cells["Id"].Value);
                string nome = dgvListar.SelectedRows[0].Cells["Nome"].Value.ToString();
                decimal preco = Convert.ToDecimal(dgvListar.SelectedRows[0].Cells["Preco"].Value);

                CadastroATTcs cadastroATTcs = new CadastroATTcs(id, nome, preco);
                if (cadastroATTcs.ShowDialog() == DialogResult.OK)
                {
                    LoadData(); // Recarrega os dados após atualizar
                }
            }
            else
            {
                MessageBox.Show("Selecione um produto para editar.");
            }
        }


        private void btnExcluir_Click(object sender, EventArgs e)
        {
            {
                if (dgvListar.SelectedRows.Count > 0)
                {
                    int id = Convert.ToInt32(dgvListar.SelectedRows[0].Cells["Id"].Value);
                    db.OpenConnection();
                    string query = "DELETE FROM Produto WHERE Id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, db.GetConnection());
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    db.CloseConnection();
                    LoadData(); // Recarrega os dados após excluir
                }
                else
                {
                    MessageBox.Show("Selecione um produto para excluir.");
                }
            }
        }

    }
}