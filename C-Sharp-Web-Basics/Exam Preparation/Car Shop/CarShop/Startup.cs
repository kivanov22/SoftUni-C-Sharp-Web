
namespace CarShop
{
    using MyWebServer;
    using MyWebServer.Controllers;
    using MyWebServer.Results.Views;
    using System;
    using System.Threading.Tasks;

    public class Startup
    {
        public static async Task Main()
            => await HttpServer
            .WithRoutes(routes => routes
            .MapStaticFiles()
            .MapControllers())
            .WithServices(services => services
            .Add<IViewEngine, CompilationViewEngine>())
            .Start();
    }
}
