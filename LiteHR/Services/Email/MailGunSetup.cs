using Microsoft.Extensions.Configuration;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Services.Email
{
    public class MailGunSetup
    {
        private readonly IConfiguration _configuration;
        private static string apiKey;
        private static string domain;
        public MailGunSetup(IConfiguration configuration)
        {
            _configuration = configuration;
            // apiKey = _configuration.GetValue<string>("MailGun:apiKey");
            //domain = _configuration.GetValue<string>("MailGun:domain");
            apiKey = "key-8540f3ef6a66cdaf8d9121f11c99aa6b";
            domain = "nrf.lloydant.com";
        }
        public static IRestResponse SendEmail(string receiver, string subject, string body)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator = new HttpBasicAuthenticator("api", apiKey);
            RestRequest request = new RestRequest();
            request.AddParameter("domain", domain, ParameterType.UrlSegment);
            request.Resource = "{domain}/message";
            request.AddParameter("from", "Excited User<info@" + domain + ">");
            request.AddParameter("to", receiver);
            request.AddParameter("subject", subject);
            request.AddParameter("text", body);
            request.Method = Method.POST;
            return client.Execute(request);
        }
    }
}
