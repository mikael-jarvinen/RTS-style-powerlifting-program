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
    /// Interaction logic for EditExercise.xaml
    /// </summary>
    public partial class AddExercise : Window
    {
        List<exercise> exercises;
        public AddExercise(List<exercise> exercises)
        {
            InitializeComponent();
            this.exercises = exercises;
        }

        private void CloseWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                exercise exercise = new exercise(name_box.Text);
                exercises.Add(exercise);
                string protocol = ((ComboBoxItem)protocol_combo.SelectedItem).Content.ToString();
                exercise.name = name_box.Text;
                if (protocol == "fatique")
                {
                    string fatique_protocol = ((ComboBoxItem)fatique_protocol_combo.SelectedItem).Content.ToString();
                    if (fatique_protocol == "load drop(ld)")
                    {
                        exercise.protocol = "ld";
                    }
                    else
                    {
                        exercise.protocol = "ldg";
                    }

                    int reps;
                    if (!Int32.TryParse(reps_box.Text, out reps))
                    {
                        return;
                    }
                    exercise.reps = reps;

                    double RPE;
                    Double.TryParse(((ComboBoxItem)RPE_combo.SelectedItem).Content.ToString(), out RPE);
                    exercise.RPE = RPE;

                    int fatique;
                    Int32.TryParse(((ComboBoxItem)fatique_combo.SelectedItem).Content.ToString().Substring(0, 1), out fatique);
                    exercise.fatique = fatique;
                }
                else if (protocol == "basic, RPE")
                {
                    exercise.protocol = "sets";

                    int sets;
                    Int32.TryParse(((ComboBoxItem)sets_combo.SelectedItem).Content.ToString(), out sets);
                    exercise.sets = sets;

                    int reps;
                    if (!Int32.TryParse(reps_box.Text, out reps))
                    {
                        return;
                    }
                    exercise.reps = reps;

                    double RPE;
                    Double.TryParse(((ComboBoxItem)RPE_combo.SelectedItem).Content.ToString(), out RPE);
                    exercise.RPE = RPE;
                }
                else if (protocol == "extra")
                {
                    exercise.protocol = "extra";
                    exercise.name = name_box.Text;
                }
                else if (protocol == "basic")
                {
                    exercise.protocol = "blank";

                    int sets;
                    Int32.TryParse(((ComboBoxItem)sets_combo.SelectedItem).Content.ToString(), out sets);
                    exercise.sets = sets;

                    int reps;
                    if (!Int32.TryParse(reps_box.Text, out reps))
                    {
                        return;
                    }
                    exercise.reps = reps;
                }
                else
                {
                    return;
                }
            }
            catch
            {
                return;
            }
        }

        private void protocol_change(object sender, RoutedEventArgs e)
        {
            string text = ((ComboBoxItem)protocol_combo.SelectedItem).Content.ToString();
            name_box.IsEnabled = false;
            sets_combo.IsEnabled = false;
            reps_box.IsEnabled = false;
            RPE_combo.IsEnabled = false;
            fatique_combo.IsEnabled = false;
            fatique_protocol_combo.IsEnabled = false;
            if (text == "fatique")
            {
                name_box.IsEnabled = true;
                reps_box.IsEnabled = true;
                RPE_combo.IsEnabled = true;
                fatique_combo.IsEnabled = true;
                fatique_protocol_combo.IsEnabled = true;
            }
            else if (text == "basic, RPE")
            {
                name_box.IsEnabled = true;
                sets_combo.IsEnabled = true;
                reps_box.IsEnabled = true;
                RPE_combo.IsEnabled = true;
            }
            else if (text == "extra")
            {
                name_box.IsEnabled = true;
            }
            else if (text == "basic")
            {
                name_box.IsEnabled = true;
                sets_combo.IsEnabled = true;
                reps_box.IsEnabled = true;
            }
            else
            {
                return;
            }
        }
    }
}
