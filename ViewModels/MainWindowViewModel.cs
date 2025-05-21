using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GetStartedApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase currentPage;

    public MainWindowViewModel()
    {
        // ✅ Start with LoginViewModel
        var loginViewModel = new LoginViewModel();
        loginViewModel.LoginSucceeded += OnLoginSucceeded;
        CurrentPage = loginViewModel;
    }

    private void OnLoginSucceeded(string username, string password, string type)
    {
        if (type == "admin" || type == "employee")
        {
            CurrentPage = new SecondViewModel(username, password);
        }
        else
        {
            Console.WriteLine("❌ Unknown user type");
        }
    }
}
