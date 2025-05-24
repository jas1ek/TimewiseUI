using CommunityToolkit.Mvvm.ComponentModel;

namespace GetStartedApp.ViewModels;

public partial class TimeLogsViewModel : ViewModelBase
{
    public TimeLogsViewModel(MainWindowViewModel main)
    {
        Main = main;
    }

    public MainWindowViewModel Main { get; }
}
