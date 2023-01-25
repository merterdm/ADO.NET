using System.Data.SqlClient;

namespace Insert
{
    public partial class Form1 : Form
    {
        static string constr2 = @"Server=(localdb)\mssqllocaldb;Database=Northwind;Trusted_Connection=true;";
        SqlConnection connection = new SqlConnection(constr2);
        SqlCommand command = new SqlCommand();
        string select_employee = "Select * from EmpYedek";
       




        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            string insert_employee = "insert into EmpYedek (firstName,LastNAme,City) ";
            insert_employee += "  Values('" + txtAd.Text + "','" + txtSoyad.Text + "','" + txtSehir.Text + "')";
            command.Connection= connection;
            command.CommandText = insert_employee;
            command.CommandType= System.Data.CommandType.Text;

            try
            {
                connection.Open();
                int kayitSayisi = command.ExecuteNonQuery();
                if(kayitSayisi > 0) 
                    MessageBox.Show("Kayit Basarili Sekilde eklenmistir.");
                else
                    MessageBox.Show("Bilinmeyen bir hata olustu. ");

            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata Olustu:" + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            // Parametre ile kayit
            //command.Parameters.AddWithValue("parametre_Adi",deger);
            string insert_employee = "insert into EmpYedek (firstName,LastNAme,City) Values(@ad,@Soyad,@sehir) ";
            //insert_employee += "  Values('" + txtAd.Text + "','" + txtSoyad.Text + "','" + txtSehir.Text + "')";
            command.Connection = connection;
            command.CommandText = insert_employee;
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.AddWithValue("@ad",txtAd.Text);
            command.Parameters.AddWithValue("@Soyad", txtSoyad.Text);
            command.Parameters.AddWithValue("@sehir", txtSehir.Text);

            try
            {
                connection.Open();
                int kayitSayisi = command.ExecuteNonQuery();
                if (kayitSayisi > 0)
                    MessageBox.Show("Kayit Basarili Sekilde eklenmistir.");
                else
                    MessageBox.Show("Bilinmeyen bir hata olustu. ");

            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata Olustu:" + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string insert_employee = "insert into EmpYedek (firstName,LastNAme,City) Values(@ad,@Soyad,@sehir) Select @@IDENTITY";
            //insert_employee += "  Values('" + txtAd.Text + "','" + txtSoyad.Text + "','" + txtSehir.Text + "')";
            command.Connection = connection;
            command.CommandText = insert_employee;
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.AddWithValue("@ad", txtAd.Text);
            command.Parameters.AddWithValue("@Soyad", txtSoyad.Text);
            command.Parameters.AddWithValue("@sehir", txtSehir.Text);

            try
            {
                connection.Open();
                decimal kayitSayisi = (decimal)command.ExecuteScalar();
                if (kayitSayisi > 0)
                    MessageBox.Show("Kayit Basarili Sekilde eklenmistir.KAyit Numaraniz:"+ kayitSayisi);
                else
                    MessageBox.Show("Bilinmeyen bir hata olustu. ");

            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata Olustu:" + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string insert_employee = "CalisanEkle";
            //insert_employee += "  Values('" + txtAd.Text + "','" + txtSoyad.Text + "','" + txtSehir.Text + "')";
            command.Connection = connection;
            command.CommandText = insert_employee;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Ad",txtAd.Text );
            command.Parameters.AddWithValue("@Soyad", txtSoyad.Text);
            command.Parameters.AddWithValue("@Sehir", txtSehir.Text);
            SqlParameter outputPara = new SqlParameter();
            outputPara.ParameterName = "@EmpId";
            outputPara.Direction = System.Data.ParameterDirection.Output;
            outputPara.SqlDbType = System.Data.SqlDbType.Int;
            command.Parameters.Add(outputPara);

            try
            {
                connection.Open();
                int kayitSayisi = command.ExecuteNonQuery();
                if ((int)outputPara.Value > 0)
                    MessageBox.Show("Kayit Basarili Sekilde eklenmistir.KAyit Numaraniz:" + kayitSayisi);
                else
                    MessageBox.Show("Bilinmeyen bir hata olustu. ");

            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata Olustu:" + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}