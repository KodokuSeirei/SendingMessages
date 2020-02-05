using Microsoft.EntityFrameworkCore;
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
using System.Windows.Shapes;

namespace SendingMessages.View
{
    /// <summary>
    /// Логика взаимодействия для AddRecipientWindow.xaml
    /// </summary>
    public partial class AddRecipientWindow : Window
    {
        public AddRecipientWindow()
        {
            InitializeComponent();
            EmailsLB.DataContext = emails;
        }
        public ObservableCollection<string> emails = new ObservableCollection<string>();
        private void GetEmailsButton_Click(object sender, RoutedEventArgs e)
        {
       
            foreach (string s in DataBaseInteraction.GetEmails(textServerAdress.Text, textDBName.Text, textTableName.Text, textColumnName.Text))
            { emails.Add(s); }
           
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            textServerAdress.Clear();
            textDBName.Clear();
            textTableName.Clear();
            textColumnName.Clear();
            textCondition.Clear();
            EmailsLB.Items.Clear();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
