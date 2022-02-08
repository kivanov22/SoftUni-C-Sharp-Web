
using BasicWebServer.Demo.Controllers;
using BasicWebServer.Demo.Services;
using BasicWebServer.Server;
using BasicWebServer.Server.Routing;

public class Startup
{
    public static async Task Main()
    {
          var server = new HttpServer(routes => routes
          .MapControllers());

        server.ServiceCollection
            .Add<UserService>();

        await server.Start();
    }
}