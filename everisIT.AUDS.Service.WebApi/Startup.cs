using Microsoft.Extensions.DependencyInjection;

namespace everisIT.AUDS.Service.WebApi
{
    public partial class Startup
    {
        //TODO Set Id Application
        private const int c_int_idApp = 162;
        /// <summary>
        /// Access Token For Integration Test
        /// </summary>
        public static string AccessTokenForTestIntegration { get; set; }

        private void AddOtherServices(IServiceCollection services)
        {
            //add other services.
        }
    }
}
