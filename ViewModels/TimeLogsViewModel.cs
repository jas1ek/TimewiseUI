using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels
{
    public class TimeLogsViewModel : ViewModelBase
    {
        public MainWindowViewModel Main { get; }

        // Master list and filtered view
        private readonly List<TimeLog> _allLogs;
        public ObservableCollection<TimeLog> TimeLogs { get; }

        // Filters
        public ObservableCollection<string> ProjectsFilter { get; }
        public string SelectedProjectFilter { get; set; }

        public ObservableCollection<string> TasksFilter { get; }
        public string SelectedTaskFilter { get; set; }

        public DateTimeOffset? FromDate { get; set; }
        public DateTimeOffset? ToDate   { get; set; }

        public string SearchText { get; set; }

        // Group-By
        public ObservableCollection<string> GroupOptions { get; }
        public string SelectedGroupOption { get; set; }

        // Commands
        public ICommand ApplyFiltersCommand   { get; }
        public ICommand GenerateReportCommand { get; }

        public TimeLogsViewModel(MainWindowViewModel main)
        {
            Main = main ?? throw new ArgumentNullException(nameof(main));

            // Seed data
            _allLogs = new List<TimeLog>
            {
                new TimeLog { UserName = "User1", TaskName = "Prepare Client Report", ProjectName = "Project 1", Date = new DateTime(2025,5,10), TimeSpent = TimeSpan.FromMinutes(143), Description = "Finalized client project report with task and time summary." },
                new TimeLog { UserName = "User2", TaskName = "Fix Login Bug",         ProjectName = "Project 3", Date = new DateTime(2025,5,4),  TimeSpent = TimeSpan.FromHours(4).Add(TimeSpan.FromMinutes(10)), Description = "Resolved user session timeout issue." },
                new TimeLog { UserName = "User1", TaskName = "Update Dashboard UI",    ProjectName = "Project 2", Date = new DateTime(2025,5,3),  TimeSpent = TimeSpan.FromHours(12).Add(TimeSpan.FromMinutes(44)), Description = "Improved layout and added new charts." },
                new TimeLog { UserName = "User3", TaskName = "Client Report Log",     ProjectName = "Project 2", Date = new DateTime(2025,4,28), TimeSpent = TimeSpan.FromHours(2), Description = "Finalised project summary for delivery." },
                new TimeLog { UserName = "User2", TaskName = "Database Migration",    ProjectName = "Project 1", Date = new DateTime(2025,5,1),  TimeSpent = TimeSpan.FromHours(6).Add(TimeSpan.FromMinutes(30)), Description = "Migrated client DB to new cluster." }
            };

            TimeLogs = new ObservableCollection<TimeLog>(_allLogs);

            // Filters
            ProjectsFilter = new ObservableCollection<string> { "All" }.Concat(_allLogs.Select(t => t.ProjectName).Distinct().OrderBy(x => x)).ToObservableCollection();
            SelectedProjectFilter = "All";

            TasksFilter = new ObservableCollection<string> { "All" }.Concat(_allLogs.Select(t => t.TaskName).Distinct().OrderBy(x => x)).ToObservableCollection();
            SelectedTaskFilter = "All";

            // Initialize date-range
            FromDate = _allLogs.Min(t => t.Date);
            ToDate   = _allLogs.Max(t => t.Date);

            SearchText = string.Empty;

            // Group-By options (unused placeholder)
            GroupOptions = new ObservableCollection<string> { "Day", "Month", "Year" };
            SelectedGroupOption = GroupOptions[0];

            // Commands
            ApplyFiltersCommand   = new RelayCommand(ApplyFilters);
            GenerateReportCommand = new RelayCommand(GenerateReport);
        }

        private void ApplyFilters()
        {
            var filtered = _allLogs.AsEnumerable();

            if (SelectedProjectFilter != "All")
                filtered = filtered.Where(t => t.ProjectName == SelectedProjectFilter);

            if (SelectedTaskFilter != "All")
                filtered = filtered.Where(t => t.TaskName == SelectedTaskFilter);

            if (FromDate.HasValue)
                filtered = filtered.Where(t => t.Date >= FromDate.Value.Date);

            if (ToDate.HasValue)
                filtered = filtered.Where(t => t.Date <= ToDate.Value.Date);

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                var q = SearchText.Trim().ToLower();
                filtered = filtered.Where(t =>
                    (t.UserName?.ToLower().Contains(q) ?? false) ||
                    (t.TaskName?.ToLower().Contains(q) ?? false) ||
                    (t.ProjectName?.ToLower().Contains(q) ?? false) ||
                    (t.Description?.ToLower().Contains(q) ?? false)
                );
            }

            TimeLogs.Clear();
            foreach (var entry in filtered)
                TimeLogs.Add(entry);
        }

        private void GenerateReport()
        {
            // TODO: implement report logic
        }
    }

    // Extension helper for ObservableCollection creation
    public static class Extensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
            => new ObservableCollection<T>(source);
    }
}
