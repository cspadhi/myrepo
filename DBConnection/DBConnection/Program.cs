using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;

namespace DBConnection
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=MININT-LSVN5SD;Initial Catalog=DBConnectionProjectDB;Integrated Security=True";

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            if(connection.State == System.Data.ConnectionState.Open)
            {
                string query = "Insert into Product (id, name) values ('4', 'Product 4')";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();

                Console.WriteLine("Product table data inserted.");


                SqlCommand cmd = new SqlCommand("Select * from Product", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("{0}, {1}", reader.GetInt32(0), reader.GetString(1));
                }
                reader.Close();
                connection.Close();

                Console.WriteLine("=====================================================");

                SqlCommand cmd1 = new SqlCommand("Select * from Product", connection);
                SqlDataAdapter dataAdopter = new SqlDataAdapter(cmd1);
                DataTable dataTable = new DataTable();
                dataAdopter.Fill(dataTable);

                foreach(DataRow row in dataTable.Rows)
                {
                    Console.WriteLine("{0}, {1}", row["id"], row["name"]);
                }

                if (Debugger.IsAttached)
                {
                    Console.ReadLine();
                }

            }

        }
    }
}
