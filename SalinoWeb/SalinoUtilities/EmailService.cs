using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace SalinoMvc5.SalinoUtilities
{
    public static class EmailService
    {
        public static void  EmailSender(string textComment,string passwordEmail,string fromEmail,string toEmail)
        {
            
         
            //____________فرستادن کد به ایمیل


            try
            {
                MailMessage MyEmail = new MailMessage();
                MyEmail.From = new MailAddress(fromEmail, "فروشگاه سالینو");
                MyEmail.To.Add(toEmail);
                MyEmail.Subject = " پاسخ به نظرشما :";
                MyEmail.Body = "<div style='border: 1px solid;padding: 33px 192px;background: gainsboro;font-family: IRANSans'><h1 style='text-align:center;font-family: IRANSans'>فروشگاه سالینو</h3></hr><p style='text-align:right;direction:rtl;font-family: IRANSans'>" + textComment + " </p></div>";
                MyEmail.IsBodyHtml = true;
                MyEmail.Priority = MailPriority.High;

                SmtpClient mysmtp = new SmtpClient();
                mysmtp.Host = "smtp.gmail.com";
                mysmtp.UseDefaultCredentials = true;
                mysmtp.EnableSsl = true;
                mysmtp.Port = 587;
                mysmtp.Credentials = new NetworkCredential(fromEmail, passwordEmail);
                mysmtp.Send(MyEmail);
            }
            catch (Exception x)
            {
                x.Message.ToString();
               
            }
            //_________________________
        }

    }
}