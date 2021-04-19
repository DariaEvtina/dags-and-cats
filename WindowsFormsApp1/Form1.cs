using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string connectionString;
        SqlConnection connection;
        public Form1()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["WindowsFormsApp1.Properties.Settings.PetsConnectionString"].ConnectionString;
        }
        private void PopulatePetsTabel()
        {
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM PetType", connection))
            {
                DataTable petsTable = new DataTable();
                adapter.Fill(petsTable);
                listPets.DisplayMember = "PetTypeName";
                listPets.ValueMember = "Id";
                listPets.DataSource = petsTable;
            }

        }
        public void PopulatePetNames()
        {
            string query = "SELECT Pet.Name FROM PetType INNER JOIN Pet ON Pet.TypeId = PetType.Id WHERE PetType.Id = @TypeId";
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@TypeId", listPets.SelectedValue);
                DataTable petNameTable = new DataTable();
                adapter.Fill(petNameTable);
                listPetsNames.DisplayMember = "Name";
                listPetsNames.ValueMember = "Id";
                listPetsNames.DataSource = petNameTable;

            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            PopulatePetsTabel();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listPets_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulatePetNames();
        }
    }
}
