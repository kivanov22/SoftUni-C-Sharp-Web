﻿
using BasicWebServer.Demo.Controllers;
using BasicWebServer.Server;
using BasicWebServer.Server.HTTP;
using BasicWebServer.Server.Responses;
using BasicWebServer.Server.Routing;
using System.Text;
using System.Web;

public class Startup
{

    private const string LoginForm = @"<form action='/Login' method='POST'>
   Username: <input type='text' name='Username'/>
   Password: <input type='text' name='Password'/>
   <input type='submit' value ='Log In' /> 
</form>";

    private const string Username = "user";
    private const string Password = "user123";

    public static async Task Main()
           => await new HttpServer(routes => routes
         .MapGet<HomeController>("/", c => c.Index())
         .MapGet<HomeController>("/Redirect", c => c.Redirect())
         .MapGet<HomeController>("/HTML", c => c.Html())
         .MapPost<HomeController>("/HTML", c => c.HtmlFormPost())
         .MapGet<HomeController>("/Content", c => c.Content())
         .MapPost<HomeController>("/Content", c=>c.DownloadContent())
         .MapGet<HomeController>("/Cookies", c=>c.Cookies())
         .MapGet<HomeController>("/Session", c=>c.Session()))
          .Start();

    //.MapGet("/HTML", new HtmlResponse(Startup.HtmlForm))
    //.MapGet("/Content", new HtmlResponse(Startup.DownloadForm))
    //.MapPost("/Content", new TextFileResponse(Startup.FileName))
    //.MapGet("/Cookies", new HtmlResponse("",Startup.AddCookiesAction))
    //.MapGet("/Session",new TextResponse("",Startup.DisplaySessionInfoAction))
    //.MapGet("/Login",new HtmlResponse(Startup.LoginForm))
    //.MapPost("/Login",new HtmlResponse("",Startup.LoginAction))
    //.MapGet("/Logout", new HtmlResponse("",Startup.LogoutAction))
    //.MapGet("/UserProfile", new HtmlResponse("",Startup.GetUserDataAction)));




    private static void GetUserDataAction(Request request, Response response)
    {
        if (request.Session.ContainsKey(Session.SessionUserKey))
        {
            response.Body = "";
            response.Body += $"<h3>Currently logged-in user" + $"is with username '{Username}'</h3>";
        }
        else
        {
            response.Body = "";
            response.Body += "<h3>You should first log in" + "- <a href='/Login'>Login</a></h3>";
        }
    }

    private static void LogoutAction(Request request, Response response)
    {
        //var sessionBeforeClear = request.Session;

        request.Session.Clear();

        //var sessionAfterClear = request.Session;

        response.Body = "";
        response.Body += "<h3>Logged out succesfully!</h3>";
    }

    private static void LoginAction(Request request, Response response)
    {
        request.Session.Clear();

        //var sessionBeforeLogin = request.Session;

        var bodyText = "";

        var usernameMatches = request.Form["Username"] == Startup.Username;
        var passwordMatches = request.Form["Password"] == Startup.Password;

        if (usernameMatches && passwordMatches)
        {
            request.Session[Session.SessionUserKey] = "MyUserId";
            response.Cookies.Add(Session.SessionCookieName, request.Session.Id);

            bodyText = "<h3>Logged successfully!</h3>";
        }
        else
        {
            bodyText = Startup.LoginForm;
        }
        response.Body = "";
        response.Body += bodyText;
    }

    //private static void DisplaySessionInfoAction
    //    (Request request, Response response)
    //{
    //    var sessionExists = request.Session
    //        .ContainsKey(Session.SessionCurrentDateKey);

    //    var bodyText = "";

    //    if (sessionExists)
    //    {
    //        var currentDate = request.Session[Session.SessionCurrentDateKey];
    //        bodyText = $"Stored date: {currentDate}!";
    //    }
    //    else
    //    {
    //        bodyText = "Current date stored!";
    //    }

    //    response.Body = "";
    //    response.Body += bodyText;
    //}



    //private static void AddCookiesAction(Request request, Response response)
    //{
    //    var requestHasCookies = request.Cookies
    //        .Any(c => c.Name != Session.SessionCookieName);

    //    var bodyText = "";

    //    if (requestHasCookies)
    //    {
    //        var cookieText = new StringBuilder();
    //        cookieText.AppendLine("<h1>Cookies</h1>");

    //        cookieText
    //            .Append("<table border='1'><tr><th>Name</th><th>Value</th></tr>");

    //        foreach (var cookie in request.Cookies)
    //        {
    //            cookieText.Append("<tr>");
    //            cookieText
    //                .Append($"<td>{HttpUtility.HtmlEncode(cookie.Name)}</td>");
    //            cookieText
    //                .Append($"<td>{HttpUtility.HtmlEncode(cookie.Value)}</td>");
    //            cookieText.Append("</table>");
    //        }
    //        cookieText.Append("</table>");
    //        bodyText = cookieText.ToString();
    //    }
    //    else
    //    {
    //        bodyText = "<h1>Cookies set!</h1>";
    //    }
    //    if (!requestHasCookies)
    //    {
    //        response.Cookies.Add("My-Cookie", "My-Value");
    //        response.Cookies.Add("My-Second-Cookie", "My-Second-Value");
    //    }
    //}

    //private static async Task<string> DownloadWebSiteContent(string url)
    //{
    //    var httpClient = new HttpClient();

    //    using (httpClient)
    //    {
    //        var response = await httpClient.GetAsync(url);

    //        var html = await response.Content.ReadAsStringAsync();

    //        return html.Substring(0, 2000);
    //    }
    //}

    //private static async Task DownloadSitesAsTextFile(string fileName, string[] urls)
    //{
    //    var downloads = new List<Task<string>>();

    //    foreach (var url in urls)
    //    {
    //        downloads.Add(DownloadWebSiteContent(url));
    //    }

    //    var responses = await Task.WhenAll(downloads);

    //    var reponsesString = string.Join(
    //        Environment.NewLine + new String('-', 100), responses);

    //    await File.WriteAllTextAsync(fileName, reponsesString);
    //}
}