using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using GetStartedApp.Models;


namespace GetStartedApp.ViewModels;

public partial class CreateProjectViewModel : ViewModelBase
{
    [ObservableProperty] private string projectName = string.Empty;
    [ObservableProperty] private string projectDescription = string.Empty;
    [ObservableProperty] private string newAssignee = string.Empty;

    public ObservableCollection<string> AssignedUsers { get; set; } = new();

    private readonly MainWindowViewModel main;

    public CreateProjectViewModel(MainWindowViewModel main)
    {
        this.main = main ?? throw new ArgumentNullException(nameof(main));
        SaveCommand = new RelayCommand(Save);
        CancelCommand = new RelayCommand(GoBack);
        AddAssigneeCommand = new RelayCommand(AddAssignee);
    }

    public IRelayCommand SaveCommand { get; }
    public IRelayCommand CancelCommand { get; }
    public IRelayCommand AddAssigneeCommand { get; }

    private void AddAssignee()
    {
        if (!string.IsNullOrWhiteSpace(NewAssignee) && !AssignedUsers.Contains(NewAssignee.Trim()))
        {
            AssignedUsers.Add(NewAssignee.Trim());
            NewAssignee = string.Empty;
        }
    }

    private void Save()
    {
        var name = ProjectName?.Trim() ?? string.Empty;
        var desc = ProjectDescription?.Trim() ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(name))
        {
            var newProject = new ProjectItem
            {
                Name = name,
                Description = desc,
                AssignedUsers = new ObservableCollection<string>(AssignedUsers)
            };

            main.ProjectsViewModel ??= new ProjectsViewModel(main);
            main.ProjectsViewModel.Projects.Add(newProject);
        }

        main.CurrentPage = main.ProjectsViewModel!;
    }

    private void GoBack()
    {
        main.CurrentPage = main.ProjectsViewModel!;
    }
}
