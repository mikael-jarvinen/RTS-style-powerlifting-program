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
using System.Windows.Navigation;
using System.Windows.Shapes;
using workoutmakerCsharp;
using Microsoft.Win32;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WorkoutMaker main_program;
        string current_file_path;

        public MainWindow()
        {
            InitializeComponent();
            main_program = new WorkoutMaker();
            string current_file_path = "";
        }

        public void UpdateProgramView()
        {
            block_scroll_view.Children.Clear();
            Image gear = new Image();
            gear.Source= new BitmapImage(new Uri("C:\\Users\\mikko\\source\\repos\\WpfApp2\\WpfApp2\\gear.png"));

            foreach (TrainingBlock block in main_program.GetTrainingBlocks())
            {
                TextBlock block_type = new TextBlock();
                block_type.FontSize = 20;
                block_type.Text = block.GetBlockType();
                StackPanel text_and_block = new StackPanel();
                text_and_block.Children.Add(block_type);
                block_scroll_view.Children.Add(text_and_block);

                int week_count = 1;
                foreach (TrainingWeek week in block.GetTrainingWeeks())
                {
                    TextBlock week_number = new TextBlock();
                    week_number.Text = "Week " + week_count;
                    week_number.FontSize = 16;
                    WrapPanel days_panel = new WrapPanel();
                    text_and_block.Children.Add(week_number);
                    text_and_block.Children.Add(days_panel);
                    foreach (TrainingDay day in week.GetTrainingDays())
                    {
                        StackPanel exercises_panel = new StackPanel(); exercises_panel.Margin = new Thickness(10.0);
                        TextBlock WeekDay = new TextBlock();
                        WeekDay.Text = day.GetWeekDay();
                        WeekDay.FontSize = 14;
                        exercises_panel.Children.Add(WeekDay);
                        days_panel.Children.Add(exercises_panel);

                        foreach(exercise exercise in day.GetExercises())
                        {
                            WrapPanel exercise_panel = new WrapPanel(); exercise_panel.Width = 300;
                            GearButton edit_button = new GearButton();
                            edit_button.Tag = day.GetExercises();
                            edit_button.btn.Tag = exercise;
                            edit_button.btn.Width = 15;
                            edit_button.btn.Height = 15;
                            edit_button.btn.Click += edit_exercise_click;

                            TextBlock exercise_text = new TextBlock();
                            exercise_text.Text = exercise.exerciseString();

                            exercise_panel.Children.Add(edit_button);
                            exercise_panel.Children.Add(exercise_text);
                            
                            exercises_panel.Children.Add(exercise_panel);
                        }
                        Button add_exercise = new Button();
                        add_exercise.Tag = day.GetExercises();
                        add_exercise.Width = 30;
                        add_exercise.Height = 20;
                        add_exercise.Content = "add";
                        add_exercise.HorizontalAlignment = HorizontalAlignment.Left;
                        add_exercise.Click += add_exercise_click;
                        exercises_panel.Children.Add(add_exercise);
                    }
                    week_count++;
                }
            }
        }

        private void add_exercise_click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            AddExercise window = new AddExercise((List<exercise>)button.Tag);
            window.ShowDialog();

            UpdateProgramView();
        }

        private void edit_exercise_click(object sender, RoutedEventArgs e)
        {
            Button edit_button = (Button)sender;
            GearButton parent = (GearButton)edit_button.Parent;

            EditExercise window = new EditExercise((exercise)edit_button.Tag, (List<exercise>)parent.Tag);
            window.ShowDialog();

            

            UpdateProgramView();
        }

        private void add_block_click(object sender, RoutedEventArgs e)
        {
            AddBlock variables_window = new AddBlock();
            variables_window.SetProgram(this.main_program);
            variables_window.ShowDialog();
            this.UpdateProgramView();
        }
        private void load_program_click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open RTS program";
            openFileDialog.Filter = "RTS generator files (*.rtsg)|*.rtsg";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == openFileDialog.CheckFileExists)
            {
                file_name.Text = openFileDialog.SafeFileName;
                current_file_path = openFileDialog.FileName;
            }
            else
            {
                return;
            }
            main_program.readFromFile(openFileDialog.FileName);
            UpdateProgramView();
        }
        private void save_program_click(object sender, RoutedEventArgs e)
        {
            if (main_program == null)
            {
                status_text.Text = "You have no program opened to save";
                return;
            }
            status_text.Text = "";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save RTS program";
            saveFileDialog.Filter = "RTS generator files (*.rtsg)|*.rtsg";
            saveFileDialog.RestoreDirectory = true;

            if(saveFileDialog.ShowDialog() == saveFileDialog.CheckPathExists)
            {
                file_name.Text = saveFileDialog.SafeFileName;
                current_file_path = saveFileDialog.FileName;
            }
            else
            {
                return;
            }
            main_program.writeToFile(saveFileDialog.FileName);
        }
        private void edit_weekly_template_click(object sender, RoutedEventArgs e)
        {
            weekly_template window = new weekly_template();
            window.Show();
        }
        private void edit_template_exercises_click(object sender, RoutedEventArgs e)
        {
            edit_exercises window = new edit_exercises();
            window.Show();

        }
    }
}
