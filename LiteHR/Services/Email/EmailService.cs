using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentEmail.Core;
using FluentEmail.Mailgun;
using LiteHR.Dtos;
using LiteHR.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LiteHR.Services.Email
{
    public class EmailService:IEmailService
    {
        string _template = "LiteHR.Services.Email.default.cshtml";
        string _templatePassword = "LiteHR.Services.Email.passwordReset.cshtml";

        private readonly IFluentEmail _email;
        private readonly IConfiguration _configuration;
        private readonly string frontEndUrl;

        public EmailService([FromServices]IFluentEmail email, IConfiguration configuration)
        {
            _email = email;

            _configuration = configuration;
            //var sender = new MailgunSender(_configuration.GetValue<string>("MailGun:domain"),
            //_configuration.GetValue<string>("MailGun:apiKey"));
            var sender = new MailgunSender("nrf.lloydant.com", "key-8540f3ef6a66cdaf8d9121f11c99aa6b");
            _email.Sender = sender;
            frontEndUrl = _configuration.GetValue<string>("Url:frontUrl");


        }

        public async Task SendMail(SendEmailDto sendEmailDto, string template)
        {
            if (!string.IsNullOrEmpty(template))
            {
                _template = template;
            }

            var parsedTemplate = ParseTemplate(_template);
            if (!string.IsNullOrEmpty(parsedTemplate))
            {
                try
                {
                    var sendStatus = await _email
                   .SetFrom(sendEmailDto.SenderEmail, sendEmailDto.SenderName)
                   .To(sendEmailDto.ReceiverEmail)
                   .Subject(sendEmailDto.Subject)
                   .UsingTemplate(parsedTemplate, sendEmailDto)
                   .SendAsync();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }

        private string ParseTemplate(string path)
        {
            string result;
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(path))
            using (var reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }
        public async Task EmailFormatter(SendEmailDto sendEmailDto)
        {
            switch (sendEmailDto.VerificationCategory)
            {
                case 1:
                    sendEmailDto.Body = string.Format("Hi " + sendEmailDto.ReceiverName + "!" + " You requested a password request. Click the Reset Password Button below to continue and confirm that the request came from you.");
                    sendEmailDto.Subject = "Job Notification";
                    //sendEmailDto.CTAUrl = localUrl + "VerifyEmail?guid=" + sendEmailDto.VerificationGuid;
                    sendEmailDto.CTAUrl = frontEndUrl + "VerifyEmail?guid=" + sendEmailDto.VerificationGuid + "&email=" + sendEmailDto.ReceiverEmail;
                    await SendMail(sendEmailDto, _template);
                    break;
                case 2:
                    sendEmailDto.Body = string.Format("Hi " + sendEmailDto.ReceiverName + "!" + " You requested a password request. Click the Reset Password Button below to continue and confirm that the request came from you.");
                    sendEmailDto.Subject = "Password Reset";
                    //sendEmailDto.CTAUrl = localUrl + "VerifyEmail?guid=" + sendEmailDto.VerificationGuid;
                    sendEmailDto.CTAUrl = frontEndUrl + "VerifyEmail?guid=" + sendEmailDto.VerificationGuid + "&email=" + sendEmailDto.ReceiverEmail;
                    await SendMail(sendEmailDto, _template);
                    break;
                default:
                    break;
            }
        }
    }
}
