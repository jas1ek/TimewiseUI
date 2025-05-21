using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    public event Action<string, string, string>? LoginSucceeded;

    [ObservableProperty]
    private string username = string.Empty;

    [ObservableProperty]
    private string password = string.Empty;

    public ObservableCollection<User> Users { get; } = new();

    [RelayCommand]
    private void Login()
    {
        // ✅ Replace database with hardcoded check
        if (Username == "admin" && Password == "admin")
        {
            Console.WriteLine("✅ Login success");

            var fakeUser = new User
            {
                Name = Username,
                Password = Password,
                Type = "employee" // or "admin" if needed
            };

            Users.Add(fakeUser);
            LoginSucceeded?.Invoke(fakeUser.Name, fakeUser.Password, fakeUser.Type);
        }
        else
        {
            Console.WriteLine("❌ Invalid credentials");
        }
    }
}
