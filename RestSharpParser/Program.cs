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
            var apiScrape = new CallData();
            scrapeAPI.getScrape(apiScrape);
        }

        class scrapeAPI : Database
        {
            public static void getScrape(CallData scrape)
            {

                var Client = new RestClient("https://www.worldtradingdata.com/api/v1/stock");
                var Key = "x6PxBoRCzbHQ89HcQQiLjmmyAyicUoAuWpIyiFAVKaouhZSJQimIJiCi6gaz";

                RestRequest request = new RestRequest("stock?symbol=AAPL,MSFT,HSBA.L&api_token={token_key}", Method.GET);
                request.AddParameter("token_key", Key);
                request.AddHeader("content-type", "application/json");

                IRestResponse response = Client.Execute(request);
                var content = JsonConvert.DeserializeObject<dynamic>(response.Content);


                for (int stockResponseIndex = 0; stockResponseIndex < scrape.Stocks.Count; stockResponseIndex++)
                {
                    dynamic timescraped = DateTime.Now;
                    dynamic symbol = content.data[stockResponseIndex].symbol.ToString();
                    dynamic lastprice = content.data[stockResponseIndex].lastPrice.ToString();
                    dynamic change = content.data[stockResponseIndex].change.ToString();
                    dynamic changepercent = content.data[stockResponseIndex].change_percent.ToString();

                    var responseObject = new RSStock(timescraped, symbol, lastprice, change, changepercent);
                    scrape.StockList.Add(responseObject);

                    InsertScrapeToDatabase(responseObject);
                    LatestScrapeToDatabase(responseObject);
                }


            }

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
