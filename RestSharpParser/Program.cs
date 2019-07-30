using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharpParser;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace RestSharpParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var Client = new RestClient("https://apidojo-yahoo-finance-v1.p.rapidapi.com/market/get-summary?region=US&lang=en");
            var request = new RestRequest(Method.GET);

            /* Resource should just be the path
            request.Resource = string.Format("/v3/members);

        // This is how to add parameters
            request.AddParameter("email", email);
            request.AddParameter("password", password);
            request.AddParameter("terms-and-conditions", "true");
            */
            request.AddHeader("X-RapidAPI-Host", "apidojo-yahoo-finance-v1.p.rapidapi.com");
            request.AddHeader("X-RapidAPI-Key", "766253babbmsh7dc7313fc6fb941p1f3b2fjsn3e5e1b4f90ba");



            // There is a chance you need to configure the aws api gateway to accept this content type header on that resource. Depends on how locked down you have it
            request.AddHeader("content-type", "application/json");
            IRestResponse response = Client.Execute(request);
            Console.WriteLine(request);
            var content = response.Content;
            
            object jsonContent = JsonConvert.DeserializeObject(content);
            Console.Write(jsonContent);
            //JArray jsonAsArray = JArray.Parse(content);

            Console.ReadKey();

            //Console.Write(jsonAsArray);

        }
    }
}
