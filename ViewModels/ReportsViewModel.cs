using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels
{
    public class ReportsViewModel : ViewModelBase
    {
        public MainWindowViewModel Main { get; }

        public ObservableCollection<ReportItem> Reports { get; }

        public ICommand SaveReportsToFileCommand { get; }

        // Default ctor—empty list
        public ReportsViewModel(MainWindowViewModel main)
            : this(main, Array.Empty<ReportItem>()) { }

        // Ctor for generated reports
        public ReportsViewModel(MainWindowViewModel main, IEnumerable<ReportItem> items)
        {
            Main = main ?? throw new ArgumentNullException(nameof(main));
            Reports = new ObservableCollection<ReportItem>(items);

            SaveReportsToFileCommand = new RelayCommand(SaveReportsToFile);
        }

        private void SaveReportsToFile()
        {
            try
            {
                // Prepare file path: e.g. "Reports_2024-06-12_16-25-30.txt"
                var fileName = $"Reports_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt";
                var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

                using (var writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("===== Reports =====");
                    foreach (var report in Reports)
                    {
                        writer.WriteLine($"Title: {report.Title}");
                        writer.WriteLine($"Summary: {report.Summary}");
                        writer.WriteLine($"Date Generated: {report.DateGenerated}");
                        writer.WriteLine(new string('-', 30));
                    }
                }

                // Optional: show a message box (Avalonia or log to console)
                Console.WriteLine($"Report saved to: {filePath}");
            }
            catch (Exception ex)
            {
                // Error handling (you can also show a message box in UI)
                Console.WriteLine("❌ Failed to save report: " + ex.Message);
            }
        }
    }
}
