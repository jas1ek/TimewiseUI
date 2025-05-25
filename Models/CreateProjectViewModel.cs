using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;

namespace GetStartedApp.ViewModels;

public partial class CreateProjectViewModel : ViewModelBase
{
    [ObservableProperty] private string projectName;
    [ObservableProperty] private string projectDescription;
    [ObservableProperty] private string newAssignee;

    public ObservableCollection<string> AssignedUsers { get; set; } = new();

    private readonly MainWindowViewModel main;

    public CreateProjectViewModel(MainWindowViewModel main)
    {
        this.main = main;
        SaveCommand = new RelayCommand(Save);
        CancelCommand = new RelayCommand(GoBack);
        AddAssigneeCommand = new RelayCommand(AddAssignee);
    }

    public IRelayCommand SaveCommand { get; }
    public IRelayCommand CancelCommand { get; }
    public IRelayCommand AddAssigneeCommand { get; }

    private void AddAssignee()
    {
        if (!string.IsNullOrWhiteSpace(NewAssignee))
        {
            AssignedUsers.Add(NewAssignee);
            NewAssignee = string.Empty;
        }
    }

    private void Save()
    {
        if (!string.IsNullOrWhiteSpace(ProjectName))
        {
            var newProject = new Models.ProjectItem
            {
                Name = ProjectName,
                Description = ProjectDescription,
                AssignedUsers = new ObservableCollection<string>(AssignedUsers)
            };


            main.ProjectsViewModel ??= new ProjectsViewModel(main);
            main.ProjectsViewModel.Projects.Add(newProject);
        }

        main.CurrentPage = main.ProjectsViewModel;
    }

    private void GoBack()
    {
        main.CurrentPage = main.ProjectsViewModel;
    }
}
