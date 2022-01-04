using LiteHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Interface
{
    public interface IEmailBodyService
    {
        Task<bool> SendEmailMessage(IEnumerable<EmailBody> emailBody);
    }
}
