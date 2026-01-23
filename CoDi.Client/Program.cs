using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoDi.Client
{
    class Program
    {

        // Clean and Build doesnt end
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }

        private IHostBuilder CreateHostBulder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureServices(services =>
            {
                
            });
        }


    }
}
