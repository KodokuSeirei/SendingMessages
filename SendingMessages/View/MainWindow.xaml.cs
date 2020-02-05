using SendingMessages.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SendingMessages.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                senders = XmlInteraction.DeserializeSenders();
            }
            catch
            {
                senders = new ObservableCollection<Sender>();
            }

            SendersBox.DataContext = senders;
            ListMails.DataContext = eMails;

            Closing += (s, e) =>
            {
                XmlInteraction.SerializeSenders(senders);
            };
            
        }

        PostRobot postRobot = new PostRobot();
        ObservableCollection<Sender> senders;
        ObservableCollection<string> eMails = new ObservableCollection<string>();
        
        List<string> sendersAddresses = new List<string>();
        public Message SentMessage { get; set; }
        //Получение адресов из файла
        private void GetMailsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                foreach (string s in FilesInteraction.GetAddressesFromFile())
                {
                    eMails.Add(s);
                }
                
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        //Вызов окна редактирования сообщения
        private void EditMessageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EditMessageWindow editMessageWindow = new EditMessageWindow();
                editMessageWindow.Owner = this;
                if (SentMessage != null)
                {
                    editMessageWindow.ThemeMessageTB.Text = SentMessage.Subject;
                    editMessageWindow.MessageTB.Text = SentMessage.Body;
                    editMessageWindow.HtmlRB.IsChecked = SentMessage.IsBodyHtml;
                }
                if (editMessageWindow.ShowDialog() == true)
                {
                    SentMessage = editMessageWindow.SentMessage;
                }
            }

            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        //Отправка сообщения адресатам 
        private async void SendMessageEveryoneButton_Click(object sender, RoutedEventArgs e)
        {           
            try
            {
                await postRobot.SendMessage(senders[SendersBox.SelectedIndex], SentMessage, eMails);         
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
        //Вызов окна добавления отправителя
        private void AddSenderButton_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                AddSenderWindow addSenderWindow = new AddSenderWindow();
                addSenderWindow.Owner = this;
                
                if (addSenderWindow.ShowDialog() == true)
                {
                    if (sendersAddresses.Contains(addSenderWindow.NewSender.EmailAddress))
                        MessageBox.Show("Адрес отправителя уже используется");
                    else
                    {
                        senders.Add(addSenderWindow.NewSender);
                        sendersAddresses.Add(addSenderWindow.NewSender.EmailAddress);
                        SendersBox.SelectedItem = senders[senders.Count - 1];
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        //Добавляем получателя из текстбокса
        private void AddRecipientButton_Click(object sender, RoutedEventArgs e)
        {
            if (RecipientTB.Text == "" || RecipientTB.Text.Contains("Адрес получателя")||!RecipientTB.Text.Contains("@"))
                MessageBox.Show("Введен некорректный адрес получателя");
            else
            {
                if (eMails.Contains((RecipientTB.Text)))
                    MessageBox.Show("Данный адрес получателя уже присутствует");
                else
                    eMails.Add(RecipientTB.Text);
            }
        }
    
        //Убираем подсказку с текстбокса
        private void RecipientTB_MouseEnter(object sender, MouseEventArgs e)
        {
            if(RecipientTB.Text=="Адрес получателя")
                RecipientTB.Clear();
        }
        //Если текстобкс пустой, возвращаем
        private void RecipientTB_MouseLeave(object sender, MouseEventArgs e)
        {
            if(RecipientTB.Text=="")
                RecipientTB.Text= "Адрес получателя";
        }
        //Получение адресов из базы данных
        private void AddRecipientFromDBButton_Click(object sender, RoutedEventArgs e)
        {
            AddRecipientWindow addRecipientWindow = new AddRecipientWindow();
            addRecipientWindow.Owner = this;
            if(addRecipientWindow.ShowDialog()==true)
            {
                foreach(string s in addRecipientWindow.emails)
                {
                    eMails.Add(s);
                }
            }       
        }
    }
}
