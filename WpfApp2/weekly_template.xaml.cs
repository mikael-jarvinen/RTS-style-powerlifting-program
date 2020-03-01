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
using System.Windows.Controls.Primitives;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for weekly_template.xaml
    /// </summary>
    public partial class weekly_template : Window
    {
        string weekly_template_file_path;
        List<List<string>> days;
        int exercise_count;
        List<string> current_day;
        public weekly_template()
        {
            InitializeComponent();
            weekly_template_file_path = "";
            days = new List<List<string>>(); days.Add(new List<string>()); days.Add(new List<string>()); days.Add(new List<string>()); days.Add(new List<string>()); days.Add(new List<string>()); days.Add(new List<string>()); days.Add(new List<string>());
            exercise_count = 0;
            current_day = new List<string>();
        }

        private void CheckExtraConditions(object sender, RoutedEventArgs e)
        {
            if (focus_empty_item.IsSelected == true && purpose_empty_item.IsSelected == true)
            {
                abs_combo.IsEnabled = true;
                lats_combo.IsEnabled = true;
                triceps_combo.IsEnabled = true;
                grip_combo.IsEnabled = true;
                text_combo.IsEnabled = true;
            }
            else
            {
                abs_combo.IsEnabled = false;
                lats_combo.IsEnabled = false;
                triceps_combo.IsEnabled = false;
                grip_combo.IsEnabled = false;
                text_combo.IsEnabled = false;
            }
        }

        private void add_exercise(object sender, RoutedEventArgs e)
        {
            string focus = exercise_focus_combo.Text;
            string exercise = main_exercise_combo.Text;
            string purpose = exercise_purpose_combo.Text;
            if (focus == "(leave empty)" && purpose == "(leave empty)")
            {
                current_day.Add(exercise);
            }
            else if (focus == "(leave empty)")
            {
                current_day.Add(exercise + '_' + purpose);
            }
            else
            {
                current_day.Add(focus + '_' + exercise + '_' + purpose);
                exercise_count++;
            }
            RefreshPanel();
        }

        private void EnableAddExerciseView()
        {
            exercise_focus_combo.Visibility = Visibility.Visible;
            main_exercise_combo.Visibility = Visibility.Visible;
            exercise_purpose_combo.Visibility = Visibility.Visible;
            add_exercise_button.Visibility = Visibility.Visible;
        }

        private void DisableAddExerciseView()
        {
            exercise_focus_combo.Visibility = Visibility.Hidden;
            main_exercise_combo.Visibility = Visibility.Hidden;
            exercise_purpose_combo.Visibility = Visibility.Hidden;
            add_exercise_button.Visibility = Visibility.Hidden;
        }

        private void RemoveEvent(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            WrapPanel panel = (WrapPanel)button.Parent;
            TextBlock text = (TextBlock)panel.Children[1];
            string exercise = text.Text;
            foreach(string element in current_day)
            {
                if (element == exercise)
                {
                    current_day.Remove(element);
                    if(exercise.Split('_').Length - 1 == 2)
                    {
                        exercise_count--;
                    }
                    break;
                }
            }
            day_view.Children.Remove((UIElement)button.Parent);
            RefreshPanel();
        }

        private void RefreshPanel()
        {
            day_view.Children.Clear();
            foreach(string exercise in current_day)
            {
                Button remove = new Button();
                remove.Width=20; remove.Height=15; remove.Click += RemoveEvent;
                TextBlock text = new TextBlock();
                text.Text = exercise;
                WrapPanel panel = new WrapPanel();
                panel.HorizontalAlignment = HorizontalAlignment.Center;
                panel.VerticalAlignment = VerticalAlignment.Center;
                panel.Children.Add(remove); panel.Children.Add(text);
                day_view.Children.Add(panel);
            }
        }

        private void load_template(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open weekly template file";
            openFileDialog.Filter = "RTS generator weekly template files (*.TEMPLATE)|*.TEMPLATE";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == openFileDialog.CheckFileExists)
            {
                weekly_template_file_path = openFileDialog.FileName;
            }
            else
            {
                return;
            }

            StreamReader sr = new StreamReader(weekly_template_file_path);
            string line = sr.ReadLine();
            List<List<string>> days = new List<List<string>>();days.Add(new List<string>());days.Add(new List<string>());days.Add(new List<string>());days.Add(new List<string>());days.Add(new List<string>());days.Add(new List<string>());days.Add(new List<string>());
            List<string> days_itr = new List<string>();
            while (line != null)
            {
                int s = 5;
                if (line == "mon" || line == "tue" || line == "wed" || line == "thu" || line == "fri" || line == "sat" || line == "sun")
                {
                    days_itr = days[DayToInt(line)];
                }
                else if (line == "X")
                {

                }
                else if (Int32.TryParse(line, out s))
                {
                    this.exercise_count = s;
                }
                else
                {
                    days_itr.Add(line);
                }
                //Read the next line
                line = sr.ReadLine();
            }
            sr.Close();
            this.days = days;

            int DayToInt(string day)
            {
                if (day == "mon")
                {
                    return 0;
                }
                else if (day == "tue")
                {
                    return 1;
                }
                else if (day == "wed")
                {
                    return 2;
                }
                else if (day == "thu")
                {
                    return 3;
                }
                else if (day == "fri")
                {
                    return 4;
                }
                else if (day == "sat")
                {
                    return 5;
                }
                else
                {
                    return 6;
                }
            }
        }

        private void save_template(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save weekly template file";
            saveFileDialog.Filter = "RTS generator weekly template files (*.TEMPLATE)|*.TEMPLATE";
            saveFileDialog.RestoreDirectory = true;
            string file_path = "";

            if (saveFileDialog.ShowDialog() == saveFileDialog.CheckPathExists)
            {
                file_path = saveFileDialog.FileName;
            }
            else
            {
                return;
            }

            File.Delete(file_path);
            FileStream new_file = File.Create(file_path);
            new_file.Close();
            StreamWriter file_writer = new StreamWriter(file_path);
            file_writer.WriteLine(this.exercise_count.ToString());

            int WeekDay = 0;
            foreach(List<string> day in this.days)
            {
                if (day.Count() == 0)
                {
                    WeekDay++;
                    continue;
                }
                file_writer.WriteLine(IntToDay(WeekDay));
                foreach(string exercise in day)
                {
                    file_writer.WriteLine(exercise);
                }
                file_writer.WriteLine("X");
                WeekDay++;
            }
            file_writer.Close();



            string IntToDay(int day)
            {
                if (day == 0)
                {
                    return "mon";
                }
                else if (day == 1)
                {
                    return "tue";
                }
                else if (day == 2)
                {
                    return "wed";
                }
                else if (day == 3)
                {
                    return "thu";
                }
                else if (day == 4)
                {
                    return "fri";
                }
                else if(day == 5)
                {
                    return "sat";
                }
                else
                {
                    return "sun";
                }
            }
        }

        private void mon_button(object sender, RoutedEventArgs e)
        {
            ToggleButton tgl_btn = (ToggleButton)sender;
            if (tgl_btn.IsChecked == true)
            {
                tue_tgl_btn.IsEnabled = false;
                wed_tgl_btn.IsEnabled = false;
                thu_tgl_btn.IsEnabled = false;
                fri_tgl_btn.IsEnabled = false;
                sat_tgl_btn.IsEnabled = false;
                sun_tgl_btn.IsEnabled = false;
                browse_button.IsEnabled = false;
                save_button.IsEnabled = false;
                current_day = this.days[0];
                EnableAddExerciseView();
                RefreshPanel();
            }
            else
            {
                tue_tgl_btn.IsEnabled = true;
                wed_tgl_btn.IsEnabled = true;
                thu_tgl_btn.IsEnabled = true;
                fri_tgl_btn.IsEnabled = true;
                sat_tgl_btn.IsEnabled = true;
                sun_tgl_btn.IsEnabled = true;
                browse_button.IsEnabled = true;
                save_button.IsEnabled = true;
                current_day = new List<string>();
                DisableAddExerciseView();
                RefreshPanel();
            }
        }

        private void tue_button(object sender, RoutedEventArgs e)
        {
            ToggleButton tgl_btn = (ToggleButton)sender;
            if (tgl_btn.IsChecked == true)
            {
                mon_tgl_btn.IsEnabled = false;
                wed_tgl_btn.IsEnabled = false;
                thu_tgl_btn.IsEnabled = false;
                fri_tgl_btn.IsEnabled = false;
                sat_tgl_btn.IsEnabled = false;
                sun_tgl_btn.IsEnabled = false;
                browse_button.IsEnabled = false;
                save_button.IsEnabled = false;
                current_day = this.days[1];
                EnableAddExerciseView();
                RefreshPanel();
            }
            else
            {
                mon_tgl_btn.IsEnabled = true;
                wed_tgl_btn.IsEnabled = true;
                thu_tgl_btn.IsEnabled = true;
                fri_tgl_btn.IsEnabled = true;
                sat_tgl_btn.IsEnabled = true;
                sun_tgl_btn.IsEnabled = true;
                browse_button.IsEnabled = true;
                save_button.IsEnabled = true;
                current_day = new List<string>();
                DisableAddExerciseView();
                RefreshPanel();
            }
        }

        private void wed_button(object sender, RoutedEventArgs e)
        {
            ToggleButton tgl_btn = (ToggleButton)sender;
            if (tgl_btn.IsChecked == true)
            {
                mon_tgl_btn.IsEnabled = false;
                tue_tgl_btn.IsEnabled = false;
                thu_tgl_btn.IsEnabled = false;
                fri_tgl_btn.IsEnabled = false;
                sat_tgl_btn.IsEnabled = false;
                sun_tgl_btn.IsEnabled = false;
                browse_button.IsEnabled = false;
                save_button.IsEnabled = false;
                current_day = this.days[2];
                EnableAddExerciseView();
                RefreshPanel();
            }
            else
            {
                mon_tgl_btn.IsEnabled = true;
                tue_tgl_btn.IsEnabled = true;
                thu_tgl_btn.IsEnabled = true;
                fri_tgl_btn.IsEnabled = true;
                sat_tgl_btn.IsEnabled = true;
                sun_tgl_btn.IsEnabled = true;
                browse_button.IsEnabled = true;
                save_button.IsEnabled = true;
                current_day = new List<string>();
                DisableAddExerciseView();
                RefreshPanel();
            }
        }

        private void thu_button(object sender, RoutedEventArgs e)
        {
            ToggleButton tgl_btn = (ToggleButton)sender;
            if (tgl_btn.IsChecked == true)
            {
                mon_tgl_btn.IsEnabled = false;
                tue_tgl_btn.IsEnabled = false;
                wed_tgl_btn.IsEnabled = false;
                fri_tgl_btn.IsEnabled = false;
                sat_tgl_btn.IsEnabled = false;
                sun_tgl_btn.IsEnabled = false;
                browse_button.IsEnabled = false;
                save_button.IsEnabled = false;
                current_day = this.days[3];
                EnableAddExerciseView();
                RefreshPanel();
            }
            else
            {
                mon_tgl_btn.IsEnabled = true;
                tue_tgl_btn.IsEnabled = true;
                wed_tgl_btn.IsEnabled = true;
                fri_tgl_btn.IsEnabled = true;
                sat_tgl_btn.IsEnabled = true;
                sun_tgl_btn.IsEnabled = true;
                browse_button.IsEnabled = true;
                save_button.IsEnabled = true;
                current_day = new List<string>();
                DisableAddExerciseView();
                RefreshPanel();
            }
        }

        private void fri_button(object sender, RoutedEventArgs e)
        {
            ToggleButton tgl_btn = (ToggleButton)sender;
            if (tgl_btn.IsChecked == true)
            {
                mon_tgl_btn.IsEnabled = false;
                tue_tgl_btn.IsEnabled = false;
                wed_tgl_btn.IsEnabled = false;
                thu_tgl_btn.IsEnabled = false;
                sat_tgl_btn.IsEnabled = false;
                sun_tgl_btn.IsEnabled = false;
                browse_button.IsEnabled = false;
                save_button.IsEnabled = false;
                current_day = this.days[4];
                EnableAddExerciseView();
                RefreshPanel();
            }
            else
            {
                mon_tgl_btn.IsEnabled = true;
                tue_tgl_btn.IsEnabled = true;
                wed_tgl_btn.IsEnabled = true;
                thu_tgl_btn.IsEnabled = true;
                sat_tgl_btn.IsEnabled = true;
                sun_tgl_btn.IsEnabled = true;
                browse_button.IsEnabled = true;
                save_button.IsEnabled = true;
                current_day = new List<string>();
                DisableAddExerciseView();
                RefreshPanel();
            }
        }

        private void sat_button(object sender, RoutedEventArgs e)
        {
            ToggleButton tgl_btn = (ToggleButton)sender;
            if (tgl_btn.IsChecked == true)
            {
                mon_tgl_btn.IsEnabled = false;
                tue_tgl_btn.IsEnabled = false;
                wed_tgl_btn.IsEnabled = false;
                thu_tgl_btn.IsEnabled = false;
                fri_tgl_btn.IsEnabled = false;
                sun_tgl_btn.IsEnabled = false;
                browse_button.IsEnabled = false;
                save_button.IsEnabled = false;
                current_day = this.days[5];
                EnableAddExerciseView();
                RefreshPanel();
            }
            else
            {
                mon_tgl_btn.IsEnabled = true;
                tue_tgl_btn.IsEnabled = true;
                wed_tgl_btn.IsEnabled = true;
                thu_tgl_btn.IsEnabled = true;
                fri_tgl_btn.IsEnabled = true;
                sun_tgl_btn.IsEnabled = true;
                browse_button.IsEnabled = true;
                save_button.IsEnabled = true;
                current_day = new List<string>();
                DisableAddExerciseView();
                RefreshPanel();
            }
        }

        private void sun_button(object sender, RoutedEventArgs e)
        {
            ToggleButton tgl_btn = (ToggleButton)sender;
            if (tgl_btn.IsChecked == true)
            {
                mon_tgl_btn.IsEnabled = false;
                tue_tgl_btn.IsEnabled = false;
                wed_tgl_btn.IsEnabled = false;
                thu_tgl_btn.IsEnabled = false;
                fri_tgl_btn.IsEnabled = false;
                sat_tgl_btn.IsEnabled = false;
                browse_button.IsEnabled = false;
                save_button.IsEnabled = false;
                current_day = this.days[6];
                EnableAddExerciseView();
                RefreshPanel();
            }
            else
            {
                mon_tgl_btn.IsEnabled = true;
                tue_tgl_btn.IsEnabled = true;
                wed_tgl_btn.IsEnabled = true;
                thu_tgl_btn.IsEnabled = true;
                fri_tgl_btn.IsEnabled = true;
                sat_tgl_btn.IsEnabled = true;
                browse_button.IsEnabled = true;
                save_button.IsEnabled = true;
                current_day = new List<string>();
                DisableAddExerciseView();
                RefreshPanel();
            }
        }
    }
}
