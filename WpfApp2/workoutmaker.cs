using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace workoutmakerCsharp
{
    /// <summary>
    /// Holds information about main workouts, used for constructing BlockInitializer instances
    /// </summary>
    public struct MainWorkouts
    {
        public string bench_primary;
        public string bench_secondary;
        public string deadlift_primary;
        public string deadlift_secondary;
        public string squat_primary;
        public string squat_secondary;

        public MainWorkouts(string bench_primary, string deadlift_primary, string squat_primary)
        {
            if (bench_primary == "bottom")
            {
                this.bench_primary = "bottom";
                this.bench_secondary = "top";
            }
            else
            {
                this.bench_primary = "top";
                this.bench_secondary = "bottom";
            }

            if (deadlift_primary == "bottom")
            {
                this.deadlift_primary = "bottom";
                this.deadlift_secondary = "top";
            }
            else
            {
                this.deadlift_primary = "top";
                this.deadlift_secondary = "bottom";
            }

            if (squat_primary == "bottom")
            {
                this.squat_primary = "bottom";
                this.squat_secondary = "top";
            }
            else
            {
                this.squat_primary = "top";
                this.squat_secondary = "bottom";
            }
        }
    }

    /// <summary>
    /// Collects information for constructing TrainingBlock instances
    /// </summary>
    public class BlockInitializer
    {
        private string block_type;
        private int week_count;
        private string template_path;
        MainWorkouts main_workouts;

        public BlockInitializer(string block_type, int week_count, string template_path, MainWorkouts main_workouts)
        {
            this.block_type = block_type;
            this.week_count = week_count;
            this.template_path = template_path;
            this.main_workouts = main_workouts;
        }

        public string GetBlockType() => block_type;
        public int GetWeekCount() => week_count;
        public string GetTemplatePath() => template_path;
        public MainWorkouts GetMainWorkouts() => main_workouts;
    }

    /// <summary>
    /// Class that holds all the training blocks, the RTS program type
    /// </summary>
    public class WorkoutMaker
    {
        private List<TrainingBlock> training_blocks;

        public WorkoutMaker(List<BlockInitializer> training_blocks_initializer)
        {
            this.training_blocks = new List<TrainingBlock>();
            foreach(BlockInitializer element in training_blocks_initializer)
            {
                training_blocks.Add(new TrainingBlock(element.GetBlockType(), element.GetWeekCount(), element.GetTemplatePath(), element.GetMainWorkouts()));
            }
        }

        public WorkoutMaker()
        {
            this.training_blocks = new List<TrainingBlock>();
        }

        /// <summary>
        /// writes the program to a file, if the file does not exist, creates it
        /// </summary>
        /// <param name="path"></param>
        public void writeToFile(string path)
        {
            if (!File.Exists(path))
            {
                FileStream file= File.Create(path);
                file.Close();
            }
            File.Delete(path);
            FileStream new_file = File.Create(path);
            new_file.Close();
            StreamWriter file_writer = new StreamWriter(path);
            
            foreach(TrainingBlock block in this.training_blocks)
            {
                file_writer.WriteLine(block.GetBlockType());
                int week_count = 1;
                foreach(TrainingWeek week in block.GetTrainingWeeks())
                {
                    file_writer.WriteLine("Week " + week_count + " " + week.GetFatique());
                    week_count++;
                    foreach(TrainingDay day in week.GetTrainingDays())
                    {
                        file_writer.WriteLine(day.GetWeekDay());
                        foreach(exercise exercise in day.GetExercises())
                        {
                            file_writer.WriteLine(exercise.exerciseString());
                        }
                        file_writer.WriteLine("day_end");
                    }
                    file_writer.WriteLine("week_end");
                }
                file_writer.WriteLine("block_end");
            }
            file_writer.Close();
        }

        /// <summary>
        /// Loads a program from a file, if file doesn't exist returns bool
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool readFromFile(string path)
        {
            if (!File.Exists(path))
            {
                return false;
            }

            StreamReader file_reader = new StreamReader(path);
            string line = file_reader.ReadLine();

            TrainingBlock new_block = new TrainingBlock();
            string block_type = "";
            TrainingWeek new_week = new TrainingWeek();
            TrainingDay new_day = new TrainingDay();

            while (line != null)
            {
                if(line == "Volume" || line == "Intensity" || line == "Transition")
                {
                    block_type = line;
                    new_block.SetBlockType(block_type);
                    line = file_reader.ReadLine();
                }

                while (line != "block_end")
                {
                    if (line.Contains("Week"))
                    {
                        string fatique_string = line.Substring(line.LastIndexOf(' '));
                        int week_fatique;
                        Int32.TryParse(fatique_string, out week_fatique);
                        new_week.SetFatique(week_fatique);
                        new_week.SetBlockType(block_type);
                        line = file_reader.ReadLine();
                        while (line != "week_end")
                        {
                            if (line == "mon" || line == "tue" || line == "wed" || line == "thu" || line == "fri" || line == "sat" || line == "sun")
                            {
                                new_day.SetWeekDay(line);
                                line = file_reader.ReadLine();
                                while (line != "day_end")
                                {
                                    char[] separators = { ' ' };
                                    string[] strings = line.Split(separators, 20);
                                    int strings_last_index = strings.Length - 1;

                                    if (strings[strings_last_index] == "ld" || strings[strings_last_index] == "ldg")
                                    {
                                        double RPE;
                                        double.TryParse(strings[strings_last_index - 2], out RPE);

                                        int fatique;
                                        Int32.TryParse(strings[strings_last_index - 1].Substring(0, 1), out fatique);

                                        int reps;
                                        Int32.TryParse(strings[strings_last_index - 3].Substring(1, 1), out reps);

                                        string name = "";
                                        for (int i = 0; i < strings_last_index - 3; i++)
                                        {
                                            name = name + strings[i] + " ";
                                        }
                                        name = name.Substring(0, name.Length - 1);
                                        new_day.AddExercise(new exercise(name, reps, fatique, RPE, strings[strings_last_index]));
                                    }
                                    else if (strings[strings_last_index - 1].Substring(0, 1) == "x")
                                    {
                                        double RPE;
                                        double.TryParse(strings[strings_last_index], out RPE);

                                        int sets;
                                        Int32.TryParse(strings[strings_last_index - 1].Substring(2, 1), out sets);

                                        int reps;
                                        Int32.TryParse(strings[strings_last_index - 1].Substring(0, 1), out reps);

                                        string name = "";
                                        for (int i = 0; i < strings_last_index - 1; i++)
                                        {
                                            name = name + strings[i] + " ";
                                        }
                                        name = name.Substring(0, name.Length - 1);
                                        new_day.AddExercise(new exercise(name, reps, sets, RPE));
                                    }
                                    else
                                    {
                                        int sets;
                                        Int32.TryParse(strings[strings_last_index].Substring(0, 1), out sets);

                                        int reps;
                                        Int32.TryParse(strings[strings_last_index].Substring(2, 1), out reps);

                                        string name = "";
                                        for (int i = 0; i < strings_last_index; i++)
                                        {
                                            name = name + strings[i] + " ";
                                        }
                                        name = name.Substring(0, name.Length - 1);
                                        new_day.AddExercise(new exercise(name, reps, sets));
                                    }

                                    line = file_reader.ReadLine();
                                }
                                new_week.AddTrainingDay(new_day);
                                new_day = new TrainingDay();
                            }
                            line = file_reader.ReadLine();
                        }
                        new_block.AddTrainingWeek(new_week);
                        new_week = new TrainingWeek();
                    }
                    line = file_reader.ReadLine();
                }
                this.training_blocks.Add(new_block);
                new_block = new TrainingBlock();
                line = file_reader.ReadLine();
            }

            file_reader.Close();
            return true;
        }

        public List<TrainingBlock> GetTrainingBlocks() => training_blocks;
        public void AddTrainingBlock(TrainingBlock block) => this.training_blocks.Add(block);
    }

    public class TrainingBlock
    {
        private string block_type;
        private List<TrainingWeek> training_weeks;

        public TrainingBlock(string block_type, int week_count, string template_path, MainWorkouts main_workouts)
        {
            this.training_weeks = new List<TrainingWeek>();
            List<int> starting_fatique = new List<int>(); starting_fatique.Add(24); starting_fatique.Add(40); starting_fatique.Add(56);
            List<int> available_fatiques = new List<int>(); available_fatiques.Add(24); available_fatiques.Add(40); available_fatiques.Add(56); available_fatiques.Add(72);
            this.block_type = block_type;

            training_weeks.Add(new TrainingWeek(template_path, block_type, starting_fatique[TrainingDay.random.Next(starting_fatique.Count)], main_workouts));
            for (int i=1; i<week_count; i++)
            {
                training_weeks.Add(new TrainingWeek(template_path, block_type, available_fatiques[TrainingDay.random.Next(available_fatiques.Count)], main_workouts));
            }
        }

        public TrainingBlock()
        {
            block_type = "";
            training_weeks = new List<TrainingWeek>();
        }

        public void AddTrainingWeek(TrainingWeek training_week) => this.training_weeks.Add(training_week);
        public void SetBlockType(string block_type) => this.block_type = block_type;
        public List<TrainingWeek> GetTrainingWeeks() => training_weeks;
        public string GetBlockType() => block_type;
    }

    public class TrainingWeek
    {
        private int fatique;
        private List<TrainingDay> training_days;
        private string block_type;


        public TrainingWeek(string template_path, string block_type, int fatique, MainWorkouts main_workouts)
        {
            this.fatique = fatique;
            this.block_type = block_type;
            this.training_days = new List<TrainingDay>();
            

            StreamReader sr = new StreamReader(template_path);
            string line = sr.ReadLine();
            List<string> template_exercises = new List<string>();
            int training_days_count;  Int32.TryParse(line, out training_days_count);
            line = sr.ReadLine();

            string week_day = "";

            while (line != null)
            {
                if (line == "mon" || line == "tue" || line == "wed" || line == "thu" || line == "fri" || line == "sat" || line == "sun")
                {
                    week_day = line;
                }
                else if (line == "X")
                {
                    training_days.Add(new TrainingDay(week_day, template_exercises, block_type, fatique/(2*training_days_count), main_workouts));
                    template_exercises.Clear();
                }
                else
                {
                    template_exercises.Add(line);
                }
                //Read the next line
                line = sr.ReadLine();
            }
            sr.Close();
        }

        public TrainingWeek()
        {
            this.fatique = 0;
            training_days = new List<TrainingDay>();
            block_type = "";
        }

        public void AddTrainingDay(TrainingDay training_day) => this.training_days.Add(training_day);
        public void SetBlockType(string block_type) => this.block_type = block_type;
        public List<TrainingDay> GetTrainingDays() => training_days;
        public int GetFatique() => fatique;
        public void SetFatique(int fatique) => this.fatique = fatique;
    }

    public class TrainingDay
    {
        private string week_day;
        private List<exercise> exercises;
        public static Random random = new Random();

        public TrainingDay(string week_day, List<string> template_exercises, string block_type, int fatique, MainWorkouts main_workouts)
        {
            this.exercises = new List<exercise>();
            this.week_day = week_day;
            List<string> available_exercises = new List<string>();

            int rep_min, rep_max;
            List<double> RPE = new List<double>();
            List<string> protocols = new List<string>();

            if (block_type == "Volume")
            {
                RPE.Add(8.0);RPE.Add(8.5);RPE.Add(9.0);
                rep_min = 3;
                rep_max = 5;
                protocols.Add("ld");
                protocols.Add("ld");
                protocols.Add("ldg");
            }
            else if (block_type == "Intensity")
            {
                RPE.Add(9.0);RPE.Add(9.5);RPE.Add(10.0);
                rep_min = 1;
                rep_max = 3;
                protocols.Add("ldg");
                protocols.Add("ldg");
                protocols.Add("ld");
            }
            else
            {
                RPE.Add(8.0);RPE.Add(8.5);RPE.Add(9.0);RPE.Add(9.5);RPE.Add(10.0);
                rep_min = 2;
                rep_max = 8;
                protocols.Add("ldg");
                protocols.Add("ld");
            }

            foreach (string element in template_exercises)
            {
                string file = "";
                if(element.Split('_').Length - 1 == 1)
                {
                    file = element;
                    goto file_read;
                }
                string exercise_target = element.Substring(0, element.IndexOf('_'));
                string exercise = element.Substring(element.IndexOf('_') + 1);
                if (element.Contains("bench"))
                {
                    if (exercise_target == "primary")
                    {
                        file = main_workouts.bench_primary + '_' + exercise;
                    }
                    else
                    {
                        file = main_workouts.bench_secondary + '_' + exercise;
                    }
                }
                else if (element.Contains("deadlift"))
                {
                    if (exercise_target == "primary")
                    {
                        file = main_workouts.deadlift_primary + '_' + exercise;
                    }
                    else
                    {
                        file = main_workouts.deadlift_secondary + '_' + exercise;
                    }
                }
                else
                {
                    if (exercise_target == "primary")
                    {
                        file = main_workouts.deadlift_primary + '_' + exercise;
                    }
                    else
                    {
                        file = main_workouts.deadlift_secondary + '_' + exercise;
                    }
                }

                file_read:
                StreamReader sr = new StreamReader(file+".WORKOUT");
                string line = sr.ReadLine();

                while (line != null)
                {
                    available_exercises.Add(line);
                    line = sr.ReadLine();
                }

                if (file.Contains("supplement"))
                {
                    this.exercises.Add(new exercise(available_exercises[random.Next(available_exercises.Count)], random.Next(5, 8), random.Next(4, 7)));
                }
                else
                {
                    this.exercises.Add(new exercise(available_exercises[random.Next(available_exercises.Count)], random.Next(rep_min, rep_max + 1), fatique, RPE[random.Next(RPE.Count)], protocols[random.Next(protocols.Count)]));
                }
                available_exercises = new List<string>();

                sr.Close();
            }
        }

        public TrainingDay()
        {
            this.week_day = "";
            this.exercises = new List<exercise>();
        }

        public void AddExercise(exercise exercise)
        {
            this.exercises.Add(exercise);
        }

        public void SetWeekDay(string week_day) => this.week_day = week_day;
        public List<exercise> GetExercises() => exercises;
        public string GetWeekDay() => week_day;
    }

    public class exercise
    {
        int fatique;
        double RPE;
        int reps;
        int sets;
        string name;
        string protocol;
        public static int[] fatiques = { 24, 40, 56, 72 };

        public exercise(string name, int reps, int sets)
        {
            this.name = name;
            this.reps = reps;
            this.sets = sets;
            this.protocol = "blank";
        }

        public exercise(string name, int reps, int fatique, double RPE, string protocol)
        {
            this.name = name;
            this.reps = reps;
            this.fatique = fatique;
            this.RPE = RPE;
            this.protocol = protocol;
        }

        public exercise(string name, int reps, int sets, double RPE)
        {
            this.name = name;
            this.reps = reps;
            this.sets = sets;
            this.RPE = RPE;
            this.protocol = "sets";
        }

        public string exerciseString()
        {
            const string s = " ";
            string returnable = this.name;
            if (this.protocol == "blank")
            {
                returnable += s + this.sets + "x" + this.reps;
                return returnable;
            }
            else if(this.protocol == "sets")
            {
                returnable += s + this.sets + "x" + this.reps + "@RPE " + this.RPE;
                return returnable;
            }
            returnable += s + "x" + this.reps + "@RPE " + this.RPE + s + this.fatique + "% " + this.protocol;
            return returnable;
        }
    }
}