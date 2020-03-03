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
using System.IO;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for ExerciseEdit.xaml
    /// </summary>
    public partial class ExerciseEdit : Window
    {
        string path;
        List<string> exercises;
        public ExerciseEdit(string path)
        {
            InitializeComponent();
            this.path = path + ".WORKOUT";
            exercises = new List<string>();

            StreamReader sr = new StreamReader(this.path);
            string line = sr.ReadLine();

            while (line != null)
            {
                exercises.Add(line);
                WrapPanel new_panel = new WrapPanel();
                XButton remove = new XButton();
                remove.Width = 20; remove.btn.Width = 20;
                remove.Height = 20; remove.btn.Height = 20;
                remove.btn.Click += remove_click;
                TextBlock text = new TextBlock();
                text.Text = line;
                new_panel.Children.Add(remove);
                new_panel.Children.Add(text);
                View.Children.Add(new_panel);

                line = sr.ReadLine();
            }
            sr.Close();
        }

        private void remove_click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            XButton x = (XButton)button.Parent;
            WrapPanel parent = (WrapPanel)x.Parent;
            TextBlock text = (TextBlock)parent.Children[1];
            string exercise = text.Text;
            StackPanel elder = (StackPanel)parent.Parent;
            elder.Children.Remove(parent);
            exercises.Remove(exercise);

            File.Delete(this.path);
            FileStream new_file = File.Create(this.path);
            new_file.Close();

            StreamWriter sw = new StreamWriter(this.path);

            foreach(string element in this.exercises)
            {
                sw.WriteLine(element);
            }
            sw.Close();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            this.exercises.Add(exercise_box.Text);

            WrapPanel new_panel = new WrapPanel();
            XButton remove = new XButton();
            remove.Width = 20; remove.btn.Width = 20;
            remove.Height = 20;remove.btn.Height = 20;
            remove.btn.Click += remove_click;
            TextBlock text = new TextBlock();
            text.Text = exercise_box.Text;
            new_panel.Children.Add(remove);
            new_panel.Children.Add(text);
            View.Children.Add(new_panel);

            StreamWriter sw = new StreamWriter(path);
            foreach (string element in this.exercises)
            {
                sw.WriteLine(element);
            }
            sw.Close();
        }
    }
}
