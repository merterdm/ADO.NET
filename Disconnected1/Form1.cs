using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Disconnected1
{
    public partial class Form1 : Form
    {

        static string constr2 = @"Server=(localdb)\mssqllocaldb;Database=Northwind;Trusted_Connection=true;";
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UrunleriYukle();
        }

        private void UrunleriYukle()
        {

            //Buradaki kod esasem sql server tarafinda view olarak kayitlidir.
            //Bu sekilde yazilmasi tavsiye edilmez. Bunun yerine view pþusturmak daha sagliklidir

            string invoice = @"SELECT        dbo.Orders.ShipName, dbo.Orders.ShipAddress, dbo.Orders.ShipCity, dbo.Orders.ShipRegion, dbo.Orders.ShipPostalCode, dbo.Orders.ShipCountry, dbo.Orders.CustomerID, dbo.Customers.CompanyName AS CustomerName, 
                         dbo.Customers.Address, dbo.Customers.City, dbo.Customers.Region, dbo.Customers.PostalCode, dbo.Customers.Country, dbo.Employees.FirstName + ' ' + dbo.Employees.LastName AS Salesperson, dbo.Orders.OrderID, 
                         dbo.Orders.OrderDate, dbo.Orders.RequiredDate, dbo.Orders.ShippedDate, dbo.Shippers.CompanyName AS ShipperName, dbo.[Order Details].ProductID, dbo.Products.ProductName, dbo.[Order Details].UnitPrice, 
                         dbo.[Order Details].Quantity, dbo.[Order Details].Discount, CONVERT(money, (dbo.[Order Details].UnitPrice * dbo.[Order Details].Quantity) * (1 - dbo.[Order Details].Discount) / 100) * 100 AS ExtendedPrice, 
                         dbo.Orders.Freight
FROM            dbo.Shippers INNER JOIN
                         dbo.Products INNER JOIN
                         dbo.Employees INNER JOIN
                         dbo.Customers INNER JOIN
                         dbo.Orders ON dbo.Customers.CustomerID = dbo.Orders.CustomerID ON dbo.Employees.EmployeeID = dbo.Orders.EmployeeID INNER JOIN
                         dbo.[Order Details] ON dbo.Orders.OrderID = dbo.[Order Details].OrderID ON dbo.Products.ProductID = dbo.[Order Details].ProductID ON dbo.Shippers.ShipperID = dbo.Orders.ShipVia";

            //Yukaridaki sorgunun view halidir
            string invoice2 = "Select * from invoices";

            string sql = @"Select p.ProductID,p.ProductName,s.CompanyName,c.CategoryName,p.UnitPrice,p.UnitsInStock 
                            from Products p 
                            inner join Categories c on c.CategoryID=p.CategoryID
                            inner join Suppliers s on s.SupplierID=p.SupplierID";

            // Burada SqlConnection Nesnesi scope'larin bittigi yerde imha olur. 
            // Omru biter
            using (SqlConnection conn = new SqlConnection(constr2))
            {
                SqlDataAdapter adaptor = new SqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                adaptor.Fill(ds);

                dataGridView1.DataSource = ds.Tables[0];
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(constr2))
            {
                SqlDataAdapter adaptor = new SqlDataAdapter("Select * from Customers", conn);
                DataSet ds = new DataSet();
                adaptor.Fill(ds);

                dataGridView1.DataSource = ds.Tables[0];
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UrunleriYukle();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(constr2))
            {
                SqlDataAdapter adaptor = new SqlDataAdapter("Select * from Suppliers", conn);
                DataSet ds = new DataSet();
                adaptor.Fill(ds);

                dataGridView1.DataSource = ds.Tables[0];
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //var deger = dataGridView1.CurrentRow.Cells[0].Value;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //DataGrid uzerindeki 0. cell (Hucre) id degerini verir
            var id = dataGridView1.CurrentRow.Cells[0].Value;
            
        }
    }
}