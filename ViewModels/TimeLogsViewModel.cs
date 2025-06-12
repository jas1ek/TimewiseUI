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

        // Full set and filtered view
        private readonly List<TimeLog> _allLogs;
        public ObservableCollection<TimeLog> TimeLogs { get; }

        // Filters
        public ObservableCollection<string> ProjectsFilter { get; }
        public string SelectedProjectFilter { get; set; }

        public ObservableCollection<string> TasksFilter { get; }
        public string SelectedTaskFilter { get; set; }

        public DateTimeOffset? FromDate { get; set; }
        public DateTimeOffset? ToDate { get; set; }
        public string SearchText { get; set; }

        // Commands
        public ICommand ApplyFiltersCommand { get; }
        public ICommand GenerateReportCommand { get; }

        public TimeLogsViewModel(MainWindowViewModel main)
        {
            Main = main ?? throw new ArgumentNullException(nameof(main));

            // Seed logs
            _allLogs = new List<TimeLog>
            {
                new TimeLog { UserName="User1", TaskName="Prepare Client Report", ProjectName="Project 1", Date=new DateTime(2025,5,10), TimeSpent=TimeSpan.FromMinutes(143), Description="Finalized client project report." },
                new TimeLog { UserName="User2", TaskName="Fix Login Bug", ProjectName="Project 3", Date=new DateTime(2025,5,4), TimeSpent=TimeSpan.FromHours(4).Add(TimeSpan.FromMinutes(10)), Description="Resolved timeout issue." },
                new TimeLog { UserName="User1", TaskName="Update Dashboard UI", ProjectName="Project 2", Date=new DateTime(2025,5,3), TimeSpent=TimeSpan.FromHours(12).Add(TimeSpan.FromMinutes(44)), Description="Added new charts." },
                new TimeLog { UserName="User3", TaskName="Client Report Log", ProjectName="Project 2", Date=new DateTime(2025,4,28), TimeSpent=TimeSpan.FromHours(2), Description="Finalised summary." },
                new TimeLog { UserName="User2", TaskName="Database Migration", ProjectName="Project 1", Date=new DateTime(2025,5,1), TimeSpent=TimeSpan.FromHours(6).Add(TimeSpan.FromMinutes(30)), Description="Migrated DB." }
            };

            TimeLogs = new ObservableCollection<TimeLog>(_allLogs);

            // Filters
            ProjectsFilter = new ObservableCollection<string> { "All" }
                .Concat(_allLogs.Select(l => l.ProjectName).Distinct().OrderBy(x => x))
                .ToObservableCollection();
            SelectedProjectFilter = "All";

            TasksFilter = new ObservableCollection<string> { "All" }
                .Concat(_allLogs.Select(l => l.TaskName).Distinct().OrderBy(x => x))
                .ToObservableCollection();
            SelectedTaskFilter = "All";

            // Dates
            FromDate = _allLogs.Min(l => l.Date);
            ToDate = _allLogs.Max(l => l.Date);

            SearchText = string.Empty;

            // Commands
            ApplyFiltersCommand = new RelayCommand(ApplyFilters);
            GenerateReportCommand = new RelayCommand(GenerateReport);
        }

        private void ApplyFilters()
        {
            var filtered = _allLogs.AsEnumerable();

            if (SelectedProjectFilter != "All")
                filtered = filtered.Where(l => l.ProjectName == SelectedProjectFilter);

            if (SelectedTaskFilter != "All")
                filtered = filtered.Where(l => l.TaskName == SelectedTaskFilter);

            if (FromDate.HasValue)
                filtered = filtered.Where(l => l.Date >= FromDate.Value.Date);

            if (ToDate.HasValue)
                filtered = filtered.Where(l => l.Date <= ToDate.Value.Date);

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                var q = SearchText.Trim().ToLower();
                filtered = filtered.Where(l =>
                    (l.UserName ?? "").ToLower().Contains(q) ||
                    (l.TaskName ?? "").ToLower().Contains(q) ||
                    (l.ProjectName ?? "").ToLower().Contains(q) ||
                    (l.Description ?? "").ToLower().Contains(q));
            }

            TimeLogs.Clear();
            foreach (var e in filtered)
                TimeLogs.Add(e);
        }

        private void GenerateReport()
        {
            var selectedLogs = TimeLogs.Where(l => l.IsSelected).ToList();
            if (selectedLogs.Count == 0)
                selectedLogs = TimeLogs.ToList();

            var reportItems = selectedLogs.Select(l => new ReportItem
            {
                Title = $"{l.Date:d} — {l.ProjectName}",
                Summary = $"{l.TaskName}: {l.TimeSpent.Hours}h {l.TimeSpent.Minutes}m",
                DateGenerated = DateTimeOffset.Now.ToString("dd/MM/yyyy")
            }).ToList();

            Main.ShowReportsWithData(reportItems);
        }
    }

    // Helper extension
    public static class Extensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> src)
            => new ObservableCollection<T>(src);
    }
}
