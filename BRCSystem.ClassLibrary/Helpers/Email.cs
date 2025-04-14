using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using BRCSystem.ClassLibrary.Authentication.Entities;

namespace BRCSystem.ClassLibrary.Helpers
{
    public class Email
    {
        private readonly IParametersService _parametersService;
        public string Domain { get; set; }
        public int Port { get; set; }
        public string EmailAccount { get; set; }
        public string Password { get; set; }
        
        public Email(IParametersService parametersService)
        {
            this._parametersService = parametersService;
            Parameters? parameters = parametersService.GetAllWithPassword().FirstOrDefault();
            this.EmailAccount = parameters.EmailAccount;
            this.Password = parameters.EmailPassword;
            this.Domain = parameters.EmailDomain;
            this.Port = parameters.EmailPort;
        }

        public void EnviarEmail(string titulo, string corpo, string recipient){
            List<string> listRecipient = new List<string>();
            listRecipient.Add(recipient);
            EnviarEmail(titulo, corpo, listRecipient);
        }

        public void EnviarEmail(string titulo, string corpo, List<string> recipients){
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(EmailAccount); //IMPORTANT: This must be same as your smtp authentication address.
            foreach(string recipient in recipients){
                mail.To.Add(recipient);
            }
            //set the content
            mail.Subject = titulo;
            mail.Body = String.Format("<h2>{0}</h2>" +
                "<br/><br/>" +
                "{1}" +
                "<br/><br/><br/><br/><p style='color: gray; font - size: 13px; '><i>Mensagem automática, não é necessário responder</i></p>", titulo, corpo);
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient(Domain, Port);
            NetworkCredential Credentials = new NetworkCredential(EmailAccount, PasswordEncryption.DecryptPassword(Password));
            smtp.Credentials = Credentials;
            smtp.EnableSsl = true;
            smtp.SendAsync(mail, null);
        }
    }
}