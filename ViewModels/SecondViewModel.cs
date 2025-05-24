using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels;

public partial class SecondViewModel : ViewModelBase
{
    private readonly MainWindowViewModel? _main;
    public MainWindowViewModel? Main => _main;

    // ✅ Constructor used after login
    public SecondViewModel(string username, string password)
    {
        Username = $"Username: {username}";
        Password = $"Password: {password}";
        Console.WriteLine($"🟢 Entered SecondViewModel with: {username}, {password}");
        LoadMockData();
    }

    // ✅ Constructor used when navigating with command
    public SecondViewModel(string username, string password, MainWindowViewModel main)
        : this(username, password)
    {
        _main = main;
    }

    public string Username { get; }
    public string Password { get; }

    public ObservableCollection<TaskColumn> Columns { get; set; } = new();

    private void LoadMockData()
    {
        var toDoTasks = new ObservableCollection<TaskItem>
        {
            new TaskItem { Title = "Design login screen", Description = "Mock the login UI" },
            new TaskItem { Title = "Set up project", Description = "Initialize with Avalonia MVVM" }
        };

        var inProgressTasks = new ObservableCollection<TaskItem>
        {
            new TaskItem { Title = "Implement board layout", Description = "Match Figma design" }
        };

        var doneTasks = new ObservableCollection<TaskItem>
        {
            new TaskItem { Title = "Mock login logic", Description = "admin/admin login implemented" }
        };

        Columns.Add(new TaskColumn
        {
            Name = $"TO DO {toDoTasks.Count}",
            Tasks = toDoTasks
        });

        Columns.Add(new TaskColumn
        {
            Name = $"IN PROGRESS {inProgressTasks.Count}",
            Tasks = inProgressTasks
        });

        Columns.Add(new TaskColumn
        {
            Name = $"DONE {doneTasks.Count}",
            Tasks = doneTasks
        });
    }
}
