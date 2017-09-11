using RecuperacaoCustoAPI.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace RecuperacaoCustoAPI.Service
{
    public static class SendEmail
    {

        public static void Send(EmailDTO email)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.office365.com");

                mail.From = new MailAddress("igorago@algartech.com");
                foreach (string to in email.To)
                    mail.To.Add(to);

                mail.Subject = email.Subject;
                mail.IsBodyHtml = true;
                mail.Body = email.Message;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("igorago@algartech.com", "Iago-345");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

    }
}