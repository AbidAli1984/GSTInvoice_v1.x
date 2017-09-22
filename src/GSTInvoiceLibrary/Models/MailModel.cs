using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;

namespace GSTInvoiceData.Models
{
    public class MailModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public void SendMail(MailModel mailModel)
        {
            //Configuring webMail class to send emails  
            //gmail smtp server  
            WebMail.SmtpServer = "smtp.gmail.com";
            //gmail port to send emails  
            WebMail.SmtpPort = 587;
            WebMail.SmtpUseDefaultCredentials = true;
            //sending emails with secure protocol  
            WebMail.EnableSsl = true;
            //EmailId used to send emails from application  
            WebMail.UserName = "yajay9987@gmail.com";
            WebMail.Password = "peaceinfo321";

            //Sender email address.  
            WebMail.From = "yajay9987@gmail.com";

            //Send email  
            WebMail.Send(to: mailModel.To, subject: mailModel.Subject, body: mailModel.Body, isBodyHtml: true);
        }
    }
}