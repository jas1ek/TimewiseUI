using CommunityToolkit.Mvvm.ComponentModel;

namespace GetStartedApp.ViewModels;

public partial class ReportsViewModel : ViewModelBase
{
    public ReportsViewModel(MainWindowViewModel main)
    {
        Main = main;
    }

    public MainWindowViewModel Main { get; }
}
