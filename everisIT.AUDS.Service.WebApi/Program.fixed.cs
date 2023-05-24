using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using everisIT.Fen2Log.Logger;
using Microsoft.Extensions.Logging;

namespace everisIT.AUDS.Service.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Program
    {
        /// <summary>
        /// 
        /// </summary>
        protected Program()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddFen2Log();
                });
    }
}
