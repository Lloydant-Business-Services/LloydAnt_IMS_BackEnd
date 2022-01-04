using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Infrastructure
{
    interface IEmailSender
    {
        void Send(MailGunModel message);
        void Send(List<MailGunModel> messages);
    }
}
