using SendingMessages.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SendingMessages
{
    public static class XmlInteraction
    {
        public static void SerializeSenders(ObservableCollection<Sender> senders)
        {
            XDocument xdoc = new XDocument();
            // создаем первый элемент
            XElement sendersElem = new XElement("senders");
            // создаем атрибут
            foreach(Sender sender in senders)
            {
                XElement senderElem = new XElement("sender");
                senderElem.Add(new XAttribute("name", sender.EmailName));
                senderElem.Add(new XElement("provider", sender.Provider));
                senderElem.Add(new XElement("password", sender.Password));
                senderElem.Add(new XElement("port", sender.Port));
                sendersElem.Add(senderElem);
            }
            xdoc.Add(sendersElem);
            xdoc.Save("config.xml");
        }
        public static ObservableCollection<Sender> DeserializeSenders()
        {
            ObservableCollection<Sender> senders = new ObservableCollection<Sender>();
            XDocument xdoc = XDocument.Load("config.xml");
            foreach (XElement senderElem in xdoc.Element("senders").Elements("sender"))
            {
                XAttribute nameAttribute = senderElem.Attribute("name");
                XElement providerElem = senderElem.Element("provider");
                XElement passwordElem = senderElem.Element("password");
                XElement portElem = senderElem.Element("port");

                if (nameAttribute != null && providerElem != null && passwordElem != null && portElem != null)
                {
                    senders.Add(new Sender(nameAttribute.Value + '@' + providerElem.Value, passwordElem.Value, Convert.ToUInt16(portElem.Value)));
                }
                Console.WriteLine();
            }
            return senders;
        }
    }
}
