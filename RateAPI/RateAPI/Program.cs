using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Couchbase;
using Couchbase.Configuration.Client;
using System.Collections.Generic;
using Couchbase.Authentication;
using Couchbase.N1QL;

namespace RateAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "API";

            /*var cluster = new Cluster(new ClientConfiguration
            {
                Servers = new List<Uri> { new Uri("couchbase://localhost")}
            });

            var auth = new PasswordAuthenticator("admin", "adminas");
            cluster.Authenticate(auth);

            var bucket = cluster.OpenBucket("Game");
            var doc = new Document<dynamic>
            {
                Id = "9521",
                Content = new
                {
                    type = "Game"
                }
            };
            bucket.Insert(doc);

            var query = QueryRequest.Create("SELECT b.* FROM `Game` b WHERE b.type == $1");
            query.AddPositionalParameter("Game");
            Console.WriteLine(query.GetPreparedPayload());
            query.ScanConsistency(ScanConsistency.RequestPlus);
            var result = bucket.Query<dynamic>(query);

            if(!result.Success)
            {
                Console.WriteLine(result.Message);
                Console.WriteLine(result.Exception);
                Console.WriteLine(result?.Exception);
                result.Errors.ForEach(e => Console.WriteLine(e?.Message));
            }*/

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
