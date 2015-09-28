using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace LiraTaxi.Data.Access.App
{
    public interface ISendMail
    {
        Task<bool> ActivationMail(EmailModel model);
        Task<int> SendSMS(string UserId, string MobileNo);
    }
    public class SendMail : BaseSendMail,ISendMail
    {
        public async Task<bool> ActivationMail(EmailModel model)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("<% UserName %>", model.UserName);
            dic.Add("<% Subject %>", model.Subject);
            dic.Add("<% Summary %>", model.Summary);
            dic.Add("<% ModuleLink %>", model.ModuleLink);
            dic.Add("<% LeadName %>", model.LeadName);
            var st = await SendEMail(model.EmailTo, dic, model.Subject, "Estimation.html", "");
            return st;
        }
        
        public async Task<int> SendSMS(string UserId,string MobileNo)
        {
            int code= GetRandomCode();
            // SMS Send API.
            return code;
        }
        private int GetRandomCode()
        {
            Random rnd = new Random();
            int dice = rnd.Next(1, 5);
            return 1234;
        }
    }
    public class BaseSendMail
    {

        public async Task<bool> SendEMail(string Emailto, Dictionary<string, string> data, string Subject, string template, string attachment)
        {
            try
            {
                MailMessage email = new MailMessage();
                email.From = new MailAddress(ConfigurationManager.AppSettings["tomail"], ConfigurationManager.AppSettings["SenderName"].ToString());

                email.Subject = Subject;
                email.IsBodyHtml = true;
                StringBuilder Message = new StringBuilder();
                string path = HostingEnvironment.MapPath(@"~\MailTemplates\" + template);
                using (StreamReader reader = new StreamReader(path))
                {
                    Message = new StringBuilder(reader.ReadToEnd());
                }

                DateTime dt = DateTime.Now;
                foreach (KeyValuePair<string, string> s in data)
                {
                    Message.Replace(s.Key, s.Value);
                }
                email.Body = Convert.ToString(Message);
                string from_email = ConfigurationManager.AppSettings["email"];
                string password = ConfigurationManager.AppSettings["password"];
                string host = ConfigurationManager.AppSettings["host"];
                int port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
                SmtpClient smtp = new SmtpClient(host, port);
                smtp.UseDefaultCredentials = false;
                NetworkCredential nc = new NetworkCredential(from_email, password);
                smtp.Credentials = nc;
                smtp.EnableSsl = true;
                email.IsBodyHtml = true;
                foreach (string s in Emailto.Split(';'))
                {
                    email.To.Add(s);
                }
                if (attachment != "")
                {
                    Attachment attm = new Attachment(attachment);
                    email.Attachments.Add(attm);
                }
                object ob = (object)DateTime.Now.ToString();
                await smtp.SendMailAsync(email);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                // logger.Error("SP=" + Procedure + " Exception= " + ex.ToString());
            }
        }
    }

    public class EmailModel
    {
        public string UserName { get; set; }
        public string Subject { get; set; }
        public string Summary { get; set; }
        public string ModuleLink { get; set; }
        public string EmailTo { get; set; }
        public string LeadName { get; set; }

    }
}
