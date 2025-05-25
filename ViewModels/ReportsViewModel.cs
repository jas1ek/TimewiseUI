using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels;

public partial class ReportsViewModel : ViewModelBase
{
    public ReportsViewModel(MainWindowViewModel main)
    {
        Main = main;

        Reports = new ObservableCollection<ReportItem>
        {
            new ReportItem { Title = "May Productivity", Summary = "Average work hours: 6.2/day", Date = "2025-05-22" },
            new ReportItem { Title = "Task Overview", Summary = "Completed 24/30 tasks", Date = "2025-05-22" }
        };
    }

    public MainWindowViewModel Main { get; }

    public ObservableCollection<ReportItem> Reports { get; set; }
}
