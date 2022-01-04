using LiteHR.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Interface
{
    public interface IEmailService
    {
        Task SendMail(SendEmailDto sendEmailDto, string template);
        Task EmailFormatter(SendEmailDto sendEmailDto);
    }
}
