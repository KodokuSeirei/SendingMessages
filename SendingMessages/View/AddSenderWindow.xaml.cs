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
    /// Логика взаимодействия для AddSenderWindow.xaml
    /// </summary>
    public partial class AddSenderWindow : Window
    {
        public AddSenderWindow()
        {
            InitializeComponent();
        }
        public Sender NewSender { get; set; }
        //Добавление отправителя (в главное окно)
        private void AddSenderButton_Click(object sender, RoutedEventArgs e)
        {
            NewSender = new Sender(AdressTB.Text, PasswordTB.Text, (ushort)PortInteger.Value);
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
