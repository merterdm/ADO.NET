using System.Data.SqlClient;

namespace Update_Delete
{
    public partial class Form1 : Form
    {
        static string constr2 = @"Server=(localdb)\mssqllocaldb;Database=Northwind;Trusted_Connection=true;";
        SqlConnection connection = new SqlConnection(constr2);
        SqlCommand command = new SqlCommand();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            KategorileriDoldur();
        }

        private void KategorileriDoldur()
        {
            string sql = "Select [CategoryName],[Description] from Categories";
            command.Connection= connection;
            command.CommandText= sql;
            command.CommandType = System.Data.CommandType.Text;
            try
            {

                if(connection.State== System.Data.ConnectionState.Closed) 
                        connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                //Eger DataReader icersinde Row var ise
                if (reader.HasRows)
                {
                    //ComboBox icerisindeki elemanlari bosalt
                    cmbKategori.Items.Clear();

                    // Result icerisndeki row'lari tek tek oku.
                    while (reader.Read())
                    {
                        cmbKategori.Items.Add(reader["CategoryName"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata Olustu:" + ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
           
        }

        private void cmbKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtKategori.Text = cmbKategori.SelectedItem.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            try { 
            
                    string updatesql = "Update Categories set CategoryName=@catname Where CategoryName=@name";
                    command.Connection = connection;
                    command.CommandType= System.Data.CommandType.Text;
                    command.CommandText = updatesql;
                    command.Parameters.AddWithValue("@catname", txtKategori.Text);
                    command.Parameters.AddWithValue("@name",cmbKategori.SelectedItem.ToString());

                    connection.Open();
                    int etkilenenyitSayisi = command.ExecuteNonQuery();
            
                    //if(etkilenenyitSayisi> 0) 
                    //{

                    //    MessageBox.Show("Guncelleme Basarilidir");
               
                    //}

                    MessageBox.Show(etkilenenyitSayisi > 0 ? "Guncelleme Basarilidir" : "Hata Olustu");
                    // Burayi cagirmaz isek combobox guncellenmez
                    KategorileriDoldur();

            }
            catch(Exception ex) 
            {
                MessageBox.Show("Hata Olustu :" + ex.Message);
            }
            finally 
            { 
            
                connection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                string deletesql = "Delete Categories Where CategoryName=@name";
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = deletesql;
                command.Parameters.AddWithValue("@name", cmbKategori.SelectedItem.ToString());

                connection.Open();
                int etkilenenyitSayisi = command.ExecuteNonQuery();

                //if(etkilenenyitSayisi> 0) 
                //{

                //    MessageBox.Show("Guncelleme Basarilidir");

                //}

                MessageBox.Show(etkilenenyitSayisi > 0 ? "Guncelleme Basarilidir" : "Hata Olustu");
                // Burayi cagirmaz isek combobox guncellenmez
                KategorileriDoldur();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata Olustu :" + ex.Message);
            }
            finally
            {

                connection.Close();
            }
        }
    }
}