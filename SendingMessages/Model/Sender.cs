using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SendingMessages.Model
{
    //Сущность отправителя
    public class Sender
    {

        public string EmailAddress { get { return EmailName + '@' + Provider; } private set { } }
        public string EmailName { get; set; }
        public string Provider { get; set; }   
        public string Password { get; set; }
        public ushort Port { get; set; }
        public Sender()
        { }
        public Sender(string emailAddres,string password, ushort port)
        {
            EmailName = emailAddres.Remove(emailAddres.IndexOf('@'));
            Provider = emailAddres.Remove(0, EmailName.Length+1);  
            Password = password;
            Port = port;
        }
    }
}
