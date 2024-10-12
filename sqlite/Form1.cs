using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DatabaseCRUD
{
    public partial class Form1 : Form
    {
        private string connectionString = "Data Source=database.db;Version=3;";

        public Form1()
        {
            InitializeComponent();
            CreateDatabase();
            LoadData();
        }

        private void CreateDatabase()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS Users (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT)", connection);
                command.ExecuteNonQuery();
            }
        }

        private void LoadData()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var adapter = new SQLiteDataAdapter("SELECT * FROM Users", connection);
                var table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("INSERT INTO Users (Name) VALUES (@name)", connection);
                command.Parameters.AddWithValue("@name", txtName.Text);
                command.ExecuteNonQuery();
            }
            LoadData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    var command = new SQLiteCommand("UPDATE Users SET Name = @name WHERE Id = @id", connection);
                    command.Parameters.AddWithValue("@name", txtName.Text);
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
                LoadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    var command = new SQLiteCommand("DELETE FROM Users WHERE Id = @id", connection);
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
                LoadData();
            }
        }
    }
}
