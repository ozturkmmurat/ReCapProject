using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Core.Utilities.Helpers.MailHelper
{
    public interface IMailHelper
    {
        SmtpClient SendMail(Mail sendMail, UserMail userMail);

    }
}
