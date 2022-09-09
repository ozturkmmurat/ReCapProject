using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class Mail : IEntity
    {
        public string MailSender { get; set; }
        public string SenderSmtp { get; set; }
        public int SenderPort { get; set; }
        public string SenderPassword { get; set; }
        public List<string> MailRecipientList { get; set; }
        public string MailHtmlBody { get; set; }
        public string MailSubject { get; set; }

    }
}
