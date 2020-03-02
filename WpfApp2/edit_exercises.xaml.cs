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

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for edit_exercises.xaml
    /// </summary>
    public partial class edit_exercises : Window
    {
        public edit_exercises()
        {
            InitializeComponent();
        }

        private void bottom_bench_main(object sender, RoutedEventArgs e)
        {
            ExerciseEdit window = new ExerciseEdit("bottom_bench_main");
            window.ShowDialog();
        }

        private void top_bench_main(object sender, RoutedEventArgs e)
        {
            ExerciseEdit window = new ExerciseEdit("top_bench_main");
            window.ShowDialog();
        }

        private void bottom_bench_assist(object sender, RoutedEventArgs e)
        {
            ExerciseEdit window = new ExerciseEdit("bottom_bench_assist");
            window.ShowDialog();
        }

        private void top_bench_assist(object sender, RoutedEventArgs e)
        {
            ExerciseEdit window = new ExerciseEdit("top_bench_assist");
            window.ShowDialog();
        }

        private void bench_supplement(object sender, RoutedEventArgs e)
        {
            ExerciseEdit window = new ExerciseEdit("bench_supplement");
            window.ShowDialog();
        }

        private void bottom_squat_main(object sender, RoutedEventArgs e)
        {
            ExerciseEdit window = new ExerciseEdit("bottom_squat_main");
            window.ShowDialog();
        }

        private void top_squat_main(object sender, RoutedEventArgs e)
        {
            ExerciseEdit window = new ExerciseEdit("top_squat_main");
            window.ShowDialog();
        }

        private void bottom_squat_assist(object sender, RoutedEventArgs e)
        {
            ExerciseEdit window = new ExerciseEdit("bottom_squat_assist");
            window.ShowDialog();
        }

        private void top_squat_assist(object sender, RoutedEventArgs e)
        {
            ExerciseEdit window = new ExerciseEdit("top_squat_assist");
            window.ShowDialog();
        }

        private void squat_supplement(object sender, RoutedEventArgs e)
        {
            ExerciseEdit window = new ExerciseEdit("squat_supplement");
            window.ShowDialog();
        }

        private void bottom_deadlift_main(object sender, RoutedEventArgs e)
        {
            ExerciseEdit window = new ExerciseEdit("bottom_deadlift_main");
            window.ShowDialog();
        }
        private void top_deadlift_main(object sender, RoutedEventArgs e)
        {
            ExerciseEdit window = new ExerciseEdit("top_deadlift_main");
            window.ShowDialog();
        }

        private void bottom_deadlift_assist(object sender, RoutedEventArgs e)
        {
            ExerciseEdit window = new ExerciseEdit("bottom_deadlift_assist");
            window.ShowDialog();
        }

        private void top_deadlift_assist(object sender, RoutedEventArgs e)
        {
            ExerciseEdit window = new ExerciseEdit("top_deadlift_assist");
            window.ShowDialog();
        }

        private void deadlift_supplement(object sender, RoutedEventArgs e)
        {
            ExerciseEdit window = new ExerciseEdit("deadlift_supplement");
            window.ShowDialog();
        }
    }
}
