using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendingMessages
{
    static class FilesInteraction
    {
        
        //Получение адресов из файла txt/csv разделитель перевод на новую строку
        public static string[] GetAddressesFromFile()
        {
            string[] lines;
            //Узнаем путь к файлу
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                //Фильтр отображаемых файлов
                Filter = "text files(*.txt; *.csv)| *.txt; *.csv|All files (*.*)|*.*"                
            };
            openFileDialog.ShowDialog();
            //Создаем поток чтения файла 
            using (StreamReader readfile = new StreamReader(openFileDialog.FileName))
            {
                //Записываем строки в массив
                lines = readfile.ReadToEnd().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            }
           return lines;
        }
        //Получение путей к файлам
        public static string[] GetAttacmentPaths()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();        
            openFileDialog.ShowDialog();
          
            return openFileDialog.FileNames;
        }
    }
}
