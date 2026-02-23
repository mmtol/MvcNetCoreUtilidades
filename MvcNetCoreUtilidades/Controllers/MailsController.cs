using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace MvcNetCoreUtilidades.Controllers
{
    public class MailsController : Controller
    {
        private IConfiguration conf;

        public MailsController(IConfiguration conf)
        {
            this.conf = conf;
        }

        public IActionResult SendMail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMail(string to, string asunto, string mensaje)
        {
            string user = conf.GetValue<string>("MailSettings:Credentials:User");

            //objeto para la informacio del mail
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(user);
            mail.To.Add(to);
            mail.Subject = asunto;
            mail.Body = mensaje;
            //se puede interpretar un html con 
            mail.IsBodyHtml = true;
            //se le puede poner prioridad
            mail.Priority = MailPriority.Normal;

            //recuperamos los datos para el obj que manda el propio mail
            string password = conf.GetValue<string>("MailSettings:Credentials:Password");
            string host = conf.GetValue<string>("MailSettings:Server:Host");
            int port = conf.GetValue<int>("MailSettings:Server:Port");
            bool ssl = conf.GetValue<bool>("MailSettings:Server:Ssl");
            bool defaultCredentials = conf.GetValue<bool>("MailSettings:Server:DefaultCredentials");

            SmtpClient client = new SmtpClient();
            client.Host = host;
            client.Port = port;
            client.EnableSsl = ssl;
            client.UseDefaultCredentials = defaultCredentials;

            //credenciales para el mail
            NetworkCredential cred = new NetworkCredential(user, password);
            client.Credentials = cred;
            await client.SendMailAsync(mail);

            ViewData["Mensaje"] = "Email mandado correctamente !!";

            return View();
        }
    }
}
