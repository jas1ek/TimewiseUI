using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels;

public partial class TasksViewModel : ViewModelBase
{
    private readonly MainWindowViewModel? _main;
    public MainWindowViewModel? Main => _main;

    public ObservableCollection<TaskItem> Tasks { get; set; }

    // Constructor with MainWindowViewModel for command access
    public TasksViewModel(MainWindowViewModel main)
    {
        _main = main;

        Tasks = new ObservableCollection<TaskItem>
        {
            new TaskItem
            {
                Title = "Prepare Client Report",
                Description = "Draft summary, compile updates, finalize.",
                Project = "Project 1",
                TimeSpent = "1h 15m",
                Deadline = "13/05/25",
                AssignedTo = "John"
            },
            new TaskItem
            {
                Title = "Write Unit Tests for Login Flow",
                Description = "Implement unit tests using xUnit.",
                Project = "Project 2",
                TimeSpent = "2h 00m",
                Deadline = "16/05/25",
                AssignedTo = "Jane"
            }
        };
    }
}
