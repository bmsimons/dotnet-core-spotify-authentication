using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Extensions;

namespace dotnet_core_spotify_authentication.Controllers
{
    class SpotifyAuthentication
    {
        public string clientID     = "27adc1ba81634561b6f2b93e581bf3cc";
        public string clientSecret = "4f885fa66efa4243b9cdfe0f8e33b0de";
        public string redirectURL  = "http://localhost:5000/callback";
    }

    [Route("")]
    public class SpotifyController : Controller
    {
        SpotifyAuthentication sAuth = new SpotifyAuthentication();

        [HttpGet]
        public ContentResult Get()
        {   
            var qb = new QueryBuilder();
            qb.Add("response_type", "code");
            qb.Add("client_id", sAuth.clientID);
            qb.Add("scope", "user-read-private user-read-email");
            qb.Add("redirect_uri", sAuth.redirectURL);

            return new ContentResult {
                ContentType = "text/html",
                Content = @"
                    <!DOCTYPE html>
                    <html>
                        <head>
                            <meta charset=""utf-8"">
                            <title>Spotify Auth Example</title>
                        </head>

                        <body>
                            <a href=""https://accounts.spotify.com/authorize/" + qb.ToQueryString().ToString() + @"""><button>Authenticate at Spotify</button></a>
                        </body>
                    </html>
                "
            };
        }

        [Route("/callback")]
        public ContentResult Get(string code)
        {
            string responseString = "";

            if (code.Length > 0)
            {
                using (HttpClient client = new HttpClient())
                {
                    Console.WriteLine(Environment.NewLine+"Your basic bearer: "+Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(sAuth.clientID+":"+sAuth.clientSecret)));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(sAuth.clientID+":"+sAuth.clientSecret)));

                    FormUrlEncodedContent formContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("code", code),
                        new KeyValuePair<string, string>("redirect_uri", sAuth.redirectURL),
                        new KeyValuePair<string, string>("grant_type", "authorization_code"),
                    });

                    var response = client.PostAsync("https://accounts.spotify.com/api/token", formContent).Result;

                    var responseContent = response.Content;
                    responseString = responseContent.ReadAsStringAsync().Result;
                }
            }

            return new ContentResult {
                ContentType = "application/json",
                Content = responseString
            };
        }
    }
}
