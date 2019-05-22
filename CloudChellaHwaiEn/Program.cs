using Amazon;
using Amazon.DynamoDBv2.DataModel;
using System;

namespace CloudChellaHwaiEn
{
    class Program
    {
        static void Main()
        {
            var regionEndpoint = RegionEndpoint.GetBySystemName("us-west-1");
            using (var context = new DynamoDBContext(regionEndpoint))
            {
                var rca = context.Load<Label>("895");
                Console.WriteLine(rca.Name);
                Console.WriteLine();
                Console.WriteLine(rca.ContactInfo);
                Console.WriteLine();
                Console.WriteLine();

                var rickAstley = context.Load<Artist>("72872");
                Console.WriteLine(rickAstley.Name);
                Console.WriteLine(rickAstley.Profile);
                Console.WriteLine();
                Console.WriteLine();

                var song = context.Load<Release>("249504");
                Console.WriteLine(song.Title);
                Console.WriteLine(song.Country);
                Console.WriteLine(song.Year);
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
