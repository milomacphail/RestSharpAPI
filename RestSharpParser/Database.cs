using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace RestSharpParser
{
    class Database
    {
      private static string _connection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = RestSharpTable; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static void InsertScrapeToDatabase(RSStock stock)
        {
            DataToTable(stock);
        }

        public static void LatestScrapeToDatabase(RSStock stock)
        {
            LastScrapeToDatabase(stock);
        }

        private static void LastScrapeToDatabase(RSStock stock)
        {
            string lastScrape = @"IF EXISTS(SELECT* FROM RestSharpTable WHERE Stock Symbol = @stock_symbol)
                                    UPDATE HAPStockTable
                                    SET Time_Scraped=@time_scraped, Last_Price = @last_price, Change = @change, Change_Percent = @change_percent;
                                    WHERE Stock_Symbol = @stock_symbol
                                ELSE 
                                    INSERT INTO RestSharpTable VALUES (@time_scraped, @stock_symbol, @last_price, @change, @change_percent);";

            using (SqlConnection db = new SqlConnection(_connection))
            {

                db.Open();

                Console.WriteLine("Database has been opened.");

                if (db.State == System.Data.ConnectionState.Open)
                {

                    using (SqlCommand command = new SqlCommand(lastScrape, db))
                    {
                        command.Parameters.AddWithValue("@time_scraped", stock.TimeScraped);
                        command.Parameters.AddWithValue("@stock_symbol", stock.StockSymbol);
                        command.Parameters.AddWithValue("@last_price", stock.LastPrice);
                        command.Parameters.AddWithValue("@change", stock.Change);
                        command.Parameters.AddWithValue("@change_percent", stock.ChangePercent);

                        command.ExecuteNonQuery();
                    }
                }
                else
                {
                    Console.WriteLine("No database found. Please check database connection.");
                }

                db.Close();
            }
        }

        private static void DataToTable(RSStock stock)
        {
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                connection.Open();
                Console.WriteLine("Connection open.");

                SqlCommand insert = new SqlCommand("INSERT INTO dbo.RestSharpTable(Time_Scraped, Stock_Name, Stock_Symbol, Last_Price, Change, Change_Percent) VALUES(@time_scraped, @stock_symbol, @last_price, @change, @change_percent), _connection;");

                insert.Parameters.AddWithValue("@time_scraped", DateTime.Now);
                insert.Parameters.AddWithValue("@stock_name", stock.TimeScraped);
                insert.Parameters.AddWithValue("stock_symbol", stock.StockSymbol);
                insert.Parameters.AddWithValue("@last_price", stock.LastPrice);
                insert.Parameters.AddWithValue("@change", stock.Change);
                insert.Parameters.AddWithValue("@change_percent", stock.ChangePercent);

                insert.ExecuteNonQuery();

                connection.Close();
                Console.WriteLine("Connection closed.");
            }

                
        }
    }
}
