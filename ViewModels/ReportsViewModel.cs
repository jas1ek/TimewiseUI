using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetStartedApp.Models;
using System.Linq;

namespace GetStartedApp.ViewModels
{
    public partial class ReportsViewModel : ViewModelBase
    {
        public MainWindowViewModel Main { get; }

        public ObservableCollection<ReportItem> Reports { get; }

        public ICommand SaveReportsToFileCommand { get; }

        [ObservableProperty]
        private string saveStatus = "";

        // Default constructor—empty list
        public ReportsViewModel(MainWindowViewModel main)
            : this(main, Array.Empty<ReportItem>()) { }

        // Constructor that accepts generated report items
        public ReportsViewModel(MainWindowViewModel main, System.Collections.Generic.IEnumerable<ReportItem> items)
        {
            Main = main ?? throw new ArgumentNullException(nameof(main));
            Reports = new ObservableCollection<ReportItem>(items);
            SaveReportsToFileCommand = new AsyncRelayCommand(SaveReportsToFileAsync);
        }

        private async System.Threading.Tasks.Task SaveReportsToFileAsync()
        {
            try
            {
                string filename = $"Timewise_Report_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), filename);

                using (var sw = new StreamWriter(path))
                {
                    foreach (var r in Reports)
                    {
                        await sw.WriteLineAsync($"Title: {r.Title}");
                        await sw.WriteLineAsync($"Summary: {r.Summary}");
                        await sw.WriteLineAsync($"Date Generated: {r.DateGenerated}");
                        await sw.WriteLineAsync(new string('-', 32));
                    }
                }

                SaveStatus = $"Saved to {filename}!";
            }
            catch (Exception ex)
            {
                SaveStatus = $"Error: {ex.Message}";
            }

            await System.Threading.Tasks.Task.Delay(2000);
            SaveStatus = "";
        }
    }
}
