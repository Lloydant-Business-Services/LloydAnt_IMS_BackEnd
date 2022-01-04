using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Infrastructure
{
    public class EmailSenderLogic<T> : IEmailSender
    {
        private string _templateFilePath;
        private T _model;
        public EmailSenderLogic(string templateFilePath, T model)
        {
            _templateFilePath = templateFilePath;
            _model = model;
        }
        public void Send(MailGunModel message)
        {
            try
            {
                useMailgun(message, _templateFilePath);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Send(List<MailGunModel> messages)
        {
            foreach (MailGunModel message in messages)
            {
                Send(message);
            }
        }

        private IRestResponse useMailgun(MailGunModel message, string body)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
                new HttpBasicAuthenticator("api",
                    "key-06b6db8ba1fa36f7dfddcff9d7c54040");
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "audme.dreamteam.com.ng", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";

            request.AddParameter("from", "NAU <portaladmin@audme.dreamteam.com.ng>");


            request.AddParameter("to", message.MessageTo);
            request.AddParameter("subject", message.Subject);
            request.AddParameter("html", body);
            request.Method = Method.POST;
            return client.Execute(request);
        }
    }
}
