using System;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels;

public partial class NewUserViewModel : ViewModelBase
{
    public NewUserViewModel()
    {
        RegisterUserCommand = new RelayCommand(RegisterUser);
    }

    public string Username
    {
        get;
        set => SetProperty(ref field, value);
    } = "";

    public string Password
    {
        get;
        set => SetProperty(ref field, value);
    } = "";

    public string UserType
    {
        get;
        set => SetProperty(ref field, value);
    } = "employee";

    public ICommand RegisterUserCommand { get; }

    private void RegisterUser()
    {
        using var context = new TimeTrackingContext();

        var newUser = new User
        {
            Name = $"{Username}",
            Password = $"{Password}",
            Type = "admin",
        };

        var userWithSameName = context.Users.SingleOrDefault(u => u.Name == newUser.Name);

        if (userWithSameName != null)
        {
            Console.WriteLine($"User with the provided name already exists");
            return;
        }

        context.Users.Add(newUser);
        context.SaveChanges();

        Console.WriteLine($"User registered");
    }
}