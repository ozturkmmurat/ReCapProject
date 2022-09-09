using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Core.Utilities.Helpers.MailHelper
{
    public class MailHelper : IMailHelper
    {
        public SmtpClient SendMail(Mail sendMail, UserMail userMail)
        {
            SmtpClient sc = new SmtpClient(sendMail.SenderSmtp, sendMail.SenderPort);
            sc.UseDefaultCredentials = false;
            sc.Credentials = new NetworkCredential(sendMail.MailSender, sendMail.SenderPassword);
            sc.EnableSsl = false;
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(sendMail.MailSender, userMail.FirstNameLastName);
            foreach (var item in sendMail.MailRecipientList)
            {
                mail.To.Add(item);
            }
            mail.Subject = sendMail.MailSubject;
            mail.IsBodyHtml = true;
            string htmlString = sendMail.MailHtmlBody;
            mail.Body = htmlString;
            mail.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
            mail.SubjectEncoding = System.Text.Encoding.GetEncoding("utf-8");
            sc.Send(mail);

            return null;
        }
    }
}
