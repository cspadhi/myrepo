using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace LocalDatabase
{
    class Program
    {
        static void Main(string[] args)
        {


            //string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\GIT\\SELF\\myrepo\\LocalDatabase\\LocalDatabase\\Database1.mdf;Integrated Security=True";
            string connectionString = GetConnectionString();

            SqlDbConnect con = new SqlDbConnect(connectionString);
            con.SqlQuery("Insert into Employee (Id, Name, Age) values ('4',@NameP , '12')");
            con.Cmd.Parameters.AddWithValue("@NameP", "Sneha");
            con.NonQueryEx();

            con.SqlQuery("Select * from Employee");
            foreach(DataRow row in con.QueryEx().Rows)
            {
                Console.WriteLine("{0}, {1}", row["Id"], row["Name"]);
            }

            /*
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            if (connection.State == System.Data.ConnectionState.Open)
            {
                //string query = "Insert into Employee (Id, Name, Age, IsMarried) values ('3', 'Chandra Sekhar', '40', '1')";
                //SqlCommand command = new SqlCommand(query, connection);
                //command.ExecuteNonQuery();

                //Console.WriteLine("Employee table data inserted.");


                SqlCommand cmd = new SqlCommand("Select * from Employee", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("{0}, {1}, {2}", reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                }
                reader.Close();
                connection.Close();

                //Console.WriteLine("=====================================================");

                //SqlCommand cmd1 = new SqlCommand("Select * from Product", connection);
                //SqlDataAdapter dataAdopter = new SqlDataAdapter(cmd1);
                //DataTable dataTable = new DataTable();
                //dataAdopter.Fill(dataTable);

                //foreach (DataRow row in dataTable.Rows)
                //{
                //    Console.WriteLine("{0}, {1}", row["Id"], row["Name"]);
                //}

                if (Debugger.IsAttached)
                {
                    Console.ReadLine();
                }

            }
            */
        }

        public static string GetConnectionString()
        {
            string conn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            return conn;
        }
    }
}
