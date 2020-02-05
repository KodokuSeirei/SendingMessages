using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using SendingMessages.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace SendingMessages
{
    
    class PostRobot
    {
        MailAddress from;//Отправитель
        MailAddress to;//Получатель
        MailMessage m;//Сообщение
        List<string> notSentMails = new List<string>();
        string NotSentMailsForMessage="Cообщение не отправлено данным получателям:";
        //Отправка сообщения
        public async Task SendMessage(Sender sender, Message message, ObservableCollection<string> recipients )
        {
           
            from = new MailAddress(sender.EmailAddress);
                SmtpClient smtp = new SmtpClient("smtp." + sender.Provider, sender.Port)
                {
                    Credentials = new NetworkCredential(sender.EmailAddress, sender.Password),
                    EnableSsl = true,
                };
                for (int i = 0; i < recipients.Count; i++)
                {
                try
                {
                    to = new MailAddress(recipients[i]);
                    m = new MailMessage(from, to)
                    {
                        Subject = message.Subject,
                        Body = message.Body,
                        IsBodyHtml = message.IsBodyHtml
                    };
                    if (message.AttachmentQuantity != 0)
                    {
                        foreach (string s in message.AttachmentPaths)
                        {
                            m.Attachments.Add(new Attachment(s));
                        }
                    }
                    await smtp.SendMailAsync(m);
                }
                //Если не удалось отправить
                catch
                {              
                    notSentMails.Add(recipients[i]);
                }
                }
            if (notSentMails.Count > 0)
            {
                foreach (string s in notSentMails)
                    NotSentMailsForMessage += '\n' + s;
                MessageBox.Show(NotSentMailsForMessage);
                notSentMails.Clear();
                NotSentMailsForMessage = "Cообщение не отправлено данным получателям:";
            }
                smtp.Dispose();
        }
           
    }
}

