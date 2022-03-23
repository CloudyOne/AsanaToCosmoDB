using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace AsanaToCosmoDB
{

    internal class Program
    {
        private static AsanaMigrator _importer;
        static async Task Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            _importer = new AsanaMigrator(configuration);
            await _importer.Run();
        }
    }
}
