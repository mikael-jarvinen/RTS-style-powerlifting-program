using Microsoft.Win32;
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
using workoutmakerCsharp;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for AddWeek.xaml
    /// </summary>
    public partial class AddWeek : Window
    {
        TrainingBlock trainingBlock;
        string template_path;
        public AddWeek(TrainingBlock trainingBlock)
        {
            InitializeComponent();
            this.trainingBlock = trainingBlock;
            block_type_text.Text = trainingBlock.GetBlockType() + " Block";
            template_path = "templates//1_weekly_template.TEMPLATE";
        }

        private void LoadTemplate(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open weekly template";
            openFileDialog.Filter = "Weekly template files (*.TEMPLATE)|*.TEMPLATE";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == openFileDialog.CheckFileExists)
            {
                template_path = openFileDialog.FileName;
            }
            else
            {
                return;
            }
        }

        private void ConfirmClose(object sender, RoutedEventArgs e)
        {
            string text = ((ComboBoxItem)fatique_combo.SelectedItem).Content.ToString();
            int fatique;
            if (text == "low")
            {
                fatique = 24;
            }
            else if (text == "medium")
            {
                fatique = 40;
            }
            else if (text == "high")
            {
                fatique = 56;
            }
            else
            {
                fatique=72;
            }

            string squat_primary= ((ComboBoxItem)squat_primary_combo.SelectedItem).Content.ToString();
            string bench_primary= ((ComboBoxItem)bench_primary_combo.SelectedItem).Content.ToString();
            string deadlift_primary= ((ComboBoxItem)deadlift_primary_combo.SelectedItem).Content.ToString();

            MainWorkouts main_workouts = new MainWorkouts(bench_primary, deadlift_primary, squat_primary);

            TrainingWeek new_week = new TrainingWeek(template_path, trainingBlock.GetBlockType(), fatique, main_workouts);
            trainingBlock.GetTrainingWeeks().Add(new_week);
            this.Close();
        }
    }
}
