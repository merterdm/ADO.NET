using System.Data.SqlClient;

namespace BaglantiCumlesi
{
    internal class Program
    {

        // Sql Server daki Database'e baglanmaya yarar.
        // Tek gorevi vardir. Sql Server ile programimiz arasinda kopru vazifesi gorur. 
        static SqlConnection connection;


        // Sql Connection String sadecesql server'a degil butun database ler icin gerekli bir yapisi vardir.
        // Yapi : "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword";
        static  string constr = @"Server=(localdb)\mssqllocaldb;Database=Northwind;User Id=ercan;Password=123;";
        
        // Windows Authentication icin asagidaki yapi kullanilir
        static string constr2 = @"Server=(localdb)\mssqllocaldb;Database=Northwind;Trusted_Connection=true;";
        /// <summary>
        /// Server = Sql server'in hangi ip adresinde calistigini bildirir
        /// Database = Calisan sql server uzerinde hangi database'e baglanacagini belirtir
        /// User Id: Sql server uzerinde tanimli kullanici Id'sidir
        /// Password: Bu kullaniciya ait sifre;
        /// </summary>
        /// <param name="args"></param>


        static void Main(string[] args)
        {
            connection = new SqlConnection(constr2);

            //baglantiyi acmak icin kontrol etmek lazim 
            if(connection.State== System.Data.ConnectionState.Closed)
            {

                connection.Open();
                Console.WriteLine("Baglanti Kuruldu");
            }
            else
            {
               Console.WriteLine("Bu baglati zaten acik");
            }

            
        }
    }
}