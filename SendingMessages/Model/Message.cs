using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendingMessages.Model
{
    //Сущность сообщения
    public class Message
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }
        public string[] AttachmentPaths { get; set; }
        public ushort AttachmentQuantity { get; set; } = 0;
        public Message(string subject,string body, bool isBodyHtml)
        {
            Subject = subject;
            Body = body;
            IsBodyHtml = isBodyHtml;            
        }
        public Message(string subject, string body, bool isBodyHtml, string[] attachmentPaths)
        {
            Subject = subject;
            Body = body;
            IsBodyHtml = isBodyHtml;
            AttachmentPaths = attachmentPaths;
            AttachmentQuantity = (ushort)attachmentPaths.Length;
        }
    

    }
}
