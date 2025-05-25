using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels;

public partial class TimeLogsViewModel : ViewModelBase
{
    public TimeLogsViewModel(MainWindowViewModel main)
    {
        Main = main;

        Logs = new ObservableCollection<TimeLogItem>
        {
            new TimeLogItem { TaskName = "Fix login bug", Duration = "45m", Date = "2025-05-20", User = "Alice" },
            new TimeLogItem { TaskName = "Design onboarding", Duration = "1h 30m", Date = "2025-05-21", User = "Bob" }
        };
    }

    public MainWindowViewModel Main { get; }

    public ObservableCollection<TimeLogItem> Logs { get; set; }
}
