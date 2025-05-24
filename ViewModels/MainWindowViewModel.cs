using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GetStartedApp.ViewModels;

/// <summary>
/// The main ViewModel controlling page navigation.
/// </summary>
public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase currentPage;

    public MainWindowViewModel()
    {
        // Initialize login and subscribe to login success event
        var loginViewModel = new LoginViewModel();
        loginViewModel.LoginSucceeded += OnLoginSucceeded;
        CurrentPage = loginViewModel;

        // Initialize commands for sidebar navigation
        ShowBoardCommand = new RelayCommand(ShowBoard);
        ShowTasksCommand = new RelayCommand(ShowTasks);
        ShowProjectsCommand = new RelayCommand(ShowProjects);
        ShowTimeLogsCommand = new RelayCommand(ShowTimeLogs);
        ShowReportsCommand = new RelayCommand(ShowReports);
    }

    // Public commands for binding to the sidebar
    public ICommand ShowBoardCommand { get; }
    public ICommand ShowTasksCommand { get; }
    public ICommand ShowProjectsCommand { get; }
    public ICommand ShowTimeLogsCommand { get; }
    public ICommand ShowReportsCommand { get; }

    // Called when login is successful
    private void OnLoginSucceeded(string username, string password, string type)
    {
        if (type == "admin" || type == "employee")
        {
            // Navigate to the board view with context
            CurrentPage = new SecondViewModel(username, password, this);
        }
        else
        {
            Console.WriteLine("❌ Unknown user type");
        }
    }

    // Navigation methods for each page
    private void ShowBoard()
    {
        CurrentPage = new SecondViewModel("admin", "admin", this);
    }

    private void ShowTasks()
    {
        CurrentPage = new TasksViewModel(this);
    }

    private void ShowProjects()
    {
        CurrentPage = new ProjectsViewModel(this);
    }

    private void ShowTimeLogs()
    {
        CurrentPage = new TimeLogsViewModel(this);
    }

    private void ShowReports()
    {
        CurrentPage = new ReportsViewModel(this);
    }
}
