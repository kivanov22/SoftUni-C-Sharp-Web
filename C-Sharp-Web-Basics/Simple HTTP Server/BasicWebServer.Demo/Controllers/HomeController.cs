using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using System.Text;
using System.Web;

namespace BasicWebServer.Demo.Controllers
{
    public class HomeController : Controller
    {
        private const string HtmlForm = @"<form action='/HTML' method='POST'>
   Name: <input type='text' name='Name'/>
   Age: <input type='number' name ='Age'/>
<input type='submit' value ='Save' />
</form>";

        private const string DownloadForm = @"<form action='/Content' method='POST'>
   <input type='submit' value ='Download Sites Content' /> 
</form>";

        private const string FileName = "content.txt";


        public HomeController(Request request)
            : base(request)
        {
        }

        public Response Index() => Text("Hello from the server!");

        public Response Redirect() => Redirect("https://softuni.org/");

        public Response Html() => Html(HomeController.HtmlForm);

        public Response HtmlFormPost()
        {
            string formData = string.Empty;

            foreach (var (key, value) in this.Request.Form)
            {
                formData += $"{key} - {value}";
                formData += Environment.NewLine;
            }

            return Text(formData);
        }

        public Response Content() => Html(HomeController.DownloadForm);


        public Response DownloadContent()
        {
            DownloadSitesAsTextFile(HomeController.FileName,
                new string[] { "https://judge.softuni.org/", "https://softuni.org/" })
                .Wait();

            return File(HomeController.FileName);
        }

        public Response Cookies()
        {
            if (this.Request.Cookies.Any(c => c.Name !=
            BasicWebServer.Server.HTTP.Session.SessionCookieName))
            {
                var cookieText = new StringBuilder();

                cookieText.AppendLine("<h1>Cookies</h1>");

                cookieText.Append("<table border='1'><tr><th>Name</th><th>Value</th></tr>");

                foreach (var cookie in this.Request.Cookies)
                {
                    cookieText.Append("<tr>");
                    cookieText
                        .Append($"<td>{HttpUtility.HtmlEncode(cookie.Name)}</td>");
                    cookieText
                        .Append($"<td>{HttpUtility.HtmlEncode(cookie.Value)}</td>");
                    cookieText.Append("</table>");
                }
                cookieText.Append("</table>");
                return Html(cookieText.ToString());
            }

            var cookies = new CookieCollection();
            cookies.Add("My-Cookie", "My-Value");
            cookies.Add("My-Second-Cookie", "My-Second-Value");

            return Html("<h1>Cookies set!</h1>", cookies);
        }

        public Response Session()
        {
            string currentDateKey = "CurrentDate";
            var sessionExists = this.Request.Session.ContainsKey(currentDateKey);

            if (sessionExists)
            {
                var currentDate = this.Request.Session[currentDateKey];
                return Text($"Stored date: {currentDate}!");
            }

            return Text("Current date stored!");
        }


        private static async Task<string> DownloadWebSiteContent(string url)
        {
            var httpClient = new HttpClient();

            using (httpClient)
            {
                var response = await httpClient.GetAsync(url);

                var html = await response.Content.ReadAsStringAsync();

                return html.Substring(0, 2000);
            }
        }
        private static async Task DownloadSitesAsTextFile(string fileName, string[] urls)
        {
            var downloads = new List<Task<string>>();

            foreach (var url in urls)
            {
                downloads.Add(DownloadWebSiteContent(url));
            }

            var responses = await Task.WhenAll(downloads);

            var reponsesString = string.Join(
                Environment.NewLine + new String('-', 100), responses);

            //fix after lecture with Stamo
            await System.IO.File.WriteAllTextAsync(fileName, reponsesString);
        }


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

        //private static void DisplaySessionInfoAction
        //(Request request, Response response)
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
    }

  
}

