using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System;

namespace CloudChellaHwaiEn
{
    class Program
    {
        static void Main()
        {
            var regionEndpoint = RegionEndpoint.GetBySystemName("us-west-1");
            using (var client = new AmazonDynamoDBClient(regionEndpoint))
            {
                var labels = Table.LoadTable(client, "label");
                var rcaDoc = labels.GetItem("895");
                using (var context = new DynamoDBContext(regionEndpoint))
                {
                    var rca = context.FromDocument<Label>(rcaDoc);
                    Console.WriteLine(rca.Name);
                    Console.WriteLine();
                    Console.WriteLine(rca.Profile);
                }
                Console.ReadLine();
            }
        }
    }
}
