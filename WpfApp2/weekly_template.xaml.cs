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
using Microsoft.Win32;
using System.IO;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for weekly_template.xaml
    /// </summary>
    public partial class weekly_template : Window
    {
        string weekly_template_file_path;
        public weekly_template()
        {
            InitializeComponent();
            weekly_template_file_path = "";
        }

        private void load_template(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open weekly template file";
            openFileDialog.Filter = "RTS generator weekly template files (*.TEMPLATE)|*.TEMPLATE";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == openFileDialog.CheckFileExists)
            {
                if (openFileDialog.FileName != "")
                {
                    weekly_template_file_path = openFileDialog.FileName;
                }
            }
            StreamReader sr = new StreamReader(weekly_template_file_path);

        }

        private void mon_button(object sender, RoutedEventArgs e)
        {

        }

        private void tue_button(object sender, RoutedEventArgs e)
        {

        }

        private void wed_button(object sender, RoutedEventArgs e)
        {

        }

        private void thu_button(object sender, RoutedEventArgs e)
        {

        }

        private void fri_button(object sender, RoutedEventArgs e)
        {

        }

        private void sat_button(object sender, RoutedEventArgs e)
        {

        }

        private void sun_button(object sender, RoutedEventArgs e)
        {

        }
    }
}
