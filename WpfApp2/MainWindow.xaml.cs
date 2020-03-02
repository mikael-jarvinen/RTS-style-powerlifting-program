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
            string new_text = "";
            foreach (TrainingBlock block in main_program.GetTrainingBlocks())
            {
                new_text = new_text + block.GetBlockType() + " block" + '\n';
                int x = 1;
                foreach (TrainingWeek week in block.GetTrainingWeeks())
                {
                    new_text = new_text + "Week " + x + '\n'; x++;
                    foreach (TrainingDay day in week.GetTrainingDays())
                    {
                        new_text = new_text + day.GetWeekDay() + '\n';
                        foreach (exercise exercise in day.GetExercises())
                        {
                            new_text = new_text + exercise.exerciseString() + '\n';
                        }
                    }
                }
            }
            program_text.Text = new_text;
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

            string new_text = "";
            foreach (TrainingBlock block in main_program.GetTrainingBlocks())
            {
                new_text = new_text + block.GetBlockType() + " block" + '\n';
                int x = 1;
                foreach (TrainingWeek week in block.GetTrainingWeeks())
                {
                    new_text = new_text + "Week " + x + '\n'; x++;
                    foreach (TrainingDay day in week.GetTrainingDays())
                    {
                        new_text = new_text + day.GetWeekDay() + '\n';
                        foreach (exercise exercise in day.GetExercises())
                        {
                            new_text = new_text + exercise.exerciseString() + '\n';
                        }
                    }
                }
            }
            program_text.Text = new_text;
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
