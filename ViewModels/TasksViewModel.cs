using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels
{
    public partial class TasksViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _main;
        public MainWindowViewModel Main => _main;

        public ObservableCollection<TaskItem> AllTasks { get; } = new();
        [ObservableProperty] private ObservableCollection<TaskItem> tasks = new();
        [ObservableProperty] private string selectedStatusFilter = "All";
        [ObservableProperty] private string selectedSortOption = "Deadline";

        public ObservableCollection<string> StatusOptions { get; } =
            new ObservableCollection<string> { "All", "To Do", "In Progress", "Done" };
        public ObservableCollection<string> SortOptions { get; } =
            new ObservableCollection<string> { "Deadline", "Status" };

        public IRelayCommand<TaskItem?> AddTimeCommand     { get; }
        public IRelayCommand<TaskItem?> CycleStatusCommand { get; }

        public TasksViewModel(MainWindowViewModel main)
        {
            _main = main ?? throw new ArgumentNullException(nameof(main));

            // seed
            AllTasks.Add(new TaskItem {
                Title       = "Prepare Client Report",
                Project     = "Project 1",
                Description = "Draft summary, compile updates, finalize.",
                TimeSpent   = "1h 15m",
                Deadline    = "13/05/25",
                AssignedTo  = "John"
            });
            AllTasks.Add(new TaskItem {
                Title       = "Write Unit Tests",
                Project     = "Project 2",
                Description = "Implement unit tests using xUnit.",
                TimeSpent   = "2h 00m",
                Deadline    = "16/05/25",
                AssignedTo  = "Jane"
            });

            // commands
            AddTimeCommand     = new RelayCommand<TaskItem?>(AddTime);
            CycleStatusCommand = new RelayCommand<TaskItem?>(t =>
            {
                if (t is null) return;
                t.Status = t.Status switch
                {
                    "To Do"       => "In Progress",
                    "In Progress" => "Done",
                    _             => "To Do"
                };
            });

            // wire each item
            foreach (var itm in AllTasks)
            {
                itm.AddTimeCommand     = AddTimeCommand;
                itm.CycleStatusCommand = CycleStatusCommand;
            }

            // initial view
            Tasks = new ObservableCollection<TaskItem>(AllTasks);
        }

        partial void OnSelectedStatusFilterChanged(string _)  => ApplyFilter();
        partial void OnSelectedSortOptionChanged(string _)    => ApplySort();

        private void ApplyFilter()
        {
            Tasks = SelectedStatusFilter == "All"
                ? new ObservableCollection<TaskItem>(AllTasks)
                : new ObservableCollection<TaskItem>(
                    AllTasks.Where(t => t.Status == SelectedStatusFilter));
        }

        private void ApplySort()
        {
            if (SelectedSortOption == "Status")
            {
                Tasks = new ObservableCollection<TaskItem>(Tasks.OrderBy(t => t.Status));
            }
            else
            {
                Tasks = new ObservableCollection<TaskItem>(
                    Tasks.OrderBy(t =>
                        DateTime.TryParseExact(
                            t.Deadline, "dd/MM/yy",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out var dt)
                        ? dt
                        : DateTime.MaxValue));
            }
        }

        private void AddTime(TaskItem? task)
        {
            if (task is null || string.IsNullOrWhiteSpace(task.NewTimeEntry))
                return;

            int oldM = ParseDuration(task.TimeSpent);
            int addM = ParseDuration(task.NewTimeEntry);
            task.TimeSpent    = FormatDuration(oldM + addM);
            task.NewTimeEntry = string.Empty;
        }

        private int ParseDuration(string s)
        {
            var m = Regex.Match(s.Trim(), @"^(?:(\d+)\s*h)?\s*(?:(\d+)\s*m)?$", RegexOptions.IgnoreCase);
            if (!m.Success) return 0;
            int h   = m.Groups[1].Success ? int.Parse(m.Groups[1].Value) : 0;
            int ms  = m.Groups[2].Success ? int.Parse(m.Groups[2].Value) : 0;
            return h * 60 + ms;
        }

        private string FormatDuration(int total)
        {
            var h = total / 60;
            var m = total % 60;
            return h > 0
                ? (m > 0 ? $"{h}h {m}m" : $"{h}h")
                : $"{m}m";
        }
    }
}
