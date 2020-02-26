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
using System.ComponentModel;
using workoutmakerCsharp;
using Microsoft.Win32;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for variables_window.xaml
    /// </summary>
    public partial class AddBlock : Window
    {
        WorkoutMaker program;
        string weekly_template_file_path;
        public AddBlock()
        {
            InitializeComponent();
            program = new WorkoutMaker();
            weekly_template_file_path = "1_weekly_template.TEMPLATE";
        }

        public void SetProgram(WorkoutMaker program)
        {
            this.program = program;
        }

        void add_block_Closing(object sender, CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are the block parameters ok?", "Block parameters", MessageBoxButton.YesNoCancel);
            if ( result == MessageBoxResult.No)
            {
                e.Cancel = true;
                return;
            }
            else if (result == MessageBoxResult.Cancel)
            {
                return;
            }

            string block_type = training_block_box.Text;
            int week_count;
            Int32.TryParse(training_weeks_count.Text, out week_count);
            MainWorkouts main_workouts = new MainWorkouts(benchpress_main_focus.Text, deadlift_main_focus.Text, squat_main_focus.Text);
            this.program.AddTrainingBlock(new TrainingBlock(block_type, week_count, this.weekly_template_file_path, main_workouts));
        }

        private void weekly_template_browse(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open weekly template file";
            openFileDialog.Filter = "RTS generator weekly template files (*.TEMPLATE)|*.TEMPLATE";
            openFileDialog.RestoreDirectory = true;

            weekly_template_file_path = "";
            if (openFileDialog.ShowDialog() == openFileDialog.CheckFileExists)
            {
                if (openFileDialog.FileName != "")
                {
                    weekly_template_file_path = openFileDialog.FileName;
                }
            }
        }
    }
}
