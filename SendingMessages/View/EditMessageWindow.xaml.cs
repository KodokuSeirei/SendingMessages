using SendingMessages.Model;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace SendingMessages.View
{
    /// <summary>
    /// Логика взаимодействия для SendMessageWindow.xaml
    /// </summary>
    public partial class EditMessageWindow : Window
    {
        public EditMessageWindow()
        {
            InitializeComponent();
        }
        public Message SentMessage { get; set; }
        
        List<string> attachmentPaths=new List<string>();
       //Cохранение сообщения
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if(attachmentPaths.Count>0)
                SentMessage = new Message(ThemeMessageTB.Text, MessageTB.Text, HtmlRB.IsChecked.Value, attachmentPaths.ToArray());
            else
                SentMessage = new Message(ThemeMessageTB.Text, MessageTB.Text,HtmlRB.IsChecked.Value);

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            MessageTB.Clear();
            ThemeMessageTB.Clear();
            attachmentPaths.Clear();
        }

        private void AddAttacmentsButton_Click(object sender, RoutedEventArgs e)
        {
           foreach(string s in FilesInteraction.GetAttacmentPaths())
            { attachmentPaths.Add(s); MessageBox.Show(s); ; }
            QuantityAttacmentsLB.Content = attachmentPaths.Count();
        }
    }
}
