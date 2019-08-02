using System;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace RestSharpParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var Client = new RestClient("https://apidojo-yahoo-finance-v1.p.rapidapi.com/market/get-summary?region=US&lang=en");
            RestRequest request = new RestRequest(Method.GET);

            request.AddHeader("X-RapidAPI-Host", "apidojo-yahoo-finance-v1.p.rapidapi.com");
            request.AddHeader("X-RapidAPI-Key", "766253babbmsh7dc7313fc6fb941p1f3b2fjsn3e5e1b4f90ba");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = Client.Execute(request);
            var content = response.Content;

            dynamic jsonDictionary = JObject.Parse(content);

            JArray result = content["result"];

            /*string _connection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = RestSharpTable; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                connection.Open();
                Console.WriteLine("Connection open.");

                foreach(KeyValuePair <string, object> in jsonDictionary)
                {
                    SqlCommand insert = new SqlCommand("INSERT INTO dbo.RestSharpTable(Time_Scraped, Stock_Name, Stock_Symbol, Last_Price, Change, Change_Percent) VALUES(@time_scraped, @stock_symbol, @last_price, @change, @change_percent), _connection;");

                    insert.Parameters.AddWithValue("@time_scraped", DateTime.Now);
                    insert.Parameters.AddWithValue("@stock_name", content["marketsummaryresponse.result.fullExchangeName"]);
                    insert.Parameters.AddWithValue("stock_symbol", content["symbol"]);
                    insert.Parameters.AddWithValue("@last_price", content["regularMarketPricefmt"]);
                    insert.Parameters.AddWithValue("@change", content["regularMarketChange.fmt"]);
                    insert.Parameters.AddWithValue("@change_percent", content["regularMarketChangePercent.fmt"]);

                    insert.ExecuteNonQuery();
                }

                connection.Close();
                Console.WriteLine("Connection closed.");
            }*/
        }

    }
}
            
               

            

    
       /*public static void DeleteTableData()
        {
            string reseed = "DBCC CHECKIDENT ('RestSharpTable', RESEED, 0);";

            using (SqlConnection con = new SqlConnection(_connection))
            {
                con.Open();

                if (con.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand cmd = new SqlCommand(reseed, con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                con.Close();
            }
        }

       public static void ResetAutoIncrementer()
        {
            string reseed = "DBCC CHECKIDENT ('StockHistory', RESEED, 0);";
        }

        /*private static void LastScrapeToDatabase(Scrape lastScrape)
       {
           string lastScrape = @"IF EXISTS(SELECT* FROM LastTable WHERE Stock_Symbol = @stock_symbol)
                                   UPDATE LastTable
                                   SET Time_Scraped = @time_scraped, Last_Price = @last_price, Change = @Change, Change_Percent = @change_percent,
                                   Volume = @volume, Shares = @shares, Average_Volume = @average_volume, Market_Cap = @market_cap
                                   WHERE Stock_Symbol = @stock_symbol
                               ELSE 
                                   INSERT INTO LastTable VALUES (@time_scraped, @stock_symbol, @last_price, @change, @change_percent, @volume, @shares, @average_volume, @market_cap);";

           
       }

       private static void DataToTable(Scrape scrape)
       {

           using (SqlConnection db = new SqlConnection(_connection))
           {
               string insertToTable = "INSERT INTO dbo.StockTable (Time_Scraped, Stock_Symbol, Last_Price, Change, Change_Percent, Volume, Shares, Average_Volume, Market_Cap) VALUES (@time_scraped, @stock_symbol, @last_price, @change, @change_percent, @volume, @shares, @average_volume, @market_cap);";
               {
                   db.Open();

                   Console.WriteLine("Database has been opened");

                   if (db.State == System.Data.ConnectionState.Open)
                   {

                       using (SqlCommand dataToTable = new SqlCommand(insertToTable, db))
                       {

                           dataToTable.Parameters.AddWithValue("@time_scraped", stock.TimeScraped);
                           dataToTable.Parameters.AddWithValue("@stock_symbol", stock.StockSymbol);
                           dataToTable.Parameters.AddWithValue("@last_price", stock.LastPrice);
                           dataToTable.Parameters.AddWithValue("@change", stock.Change);
                           dataToTable.Parameters.AddWithValue("@change_percent", stock.ChangePercent);
                           dataToTable.Parameters.AddWithValue("@volume", stock.Volume);
                           dataToTable.Parameters.AddWithValue("@shares", stock.Shares);
                           dataToTable.Parameters.AddWithValue("@average_volume", stock.AvgVol);
                           dataToTable.Parameters.AddWithValue("@market_cap", stock.MarketCap);

                           dataToTable.ExecuteNonQuery();
                       }
                   }
                   else
                   {
                       Console.WriteLine("No database found. Please check database connection.");
                   }

                   db.Close();

               }
           }

    }
}
*/
