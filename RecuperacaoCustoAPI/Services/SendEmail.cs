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

        public static void Send(object obj)
        {
            EmailDTO email = (EmailDTO)obj;
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.office365.com");

                mail.From = new MailAddress("marissaar@algartech.com");
                foreach (string to in email.To)
                    mail.To.Add(to);

                mail.Subject = email.Subject;
                mail.IsBodyHtml = true;
                mail.Body = email.Message;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("marissaar@algartech.com", "#Liberdade3");
                //SmtpServer.Credentials = new System.Net.NetworkCredential("rec.custos.algar@gmail.com", "rec.custos");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }


        public static void Send(string to, string subject, string message)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.office365.com");

                mail.From = new MailAddress("marissaar@algartech.com");
                mail.To.Add(to);
                //mail.To.Add("igorago@algartech.com");
                mail.Bcc.Add("igorago@algartech.com");
                //mail.Bcc.Add("marissaar@algartech.com");

                mail.Subject = subject;
                mail.IsBodyHtml = true;
                mail.Body = message;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("marissaar@algartech.com", "#Liberdade2");
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