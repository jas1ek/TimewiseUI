using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels;

public partial class ProjectsViewModel : ViewModelBase
{
    public ObservableCollection<ProjectItem> Projects { get; set; } = new();

    public MainWindowViewModel Main { get; }

    public IRelayCommand OpenCreateProjectCommand { get; }

    public ProjectsViewModel(MainWindowViewModel main)
    {
        Main = main;
        OpenCreateProjectCommand = new RelayCommand(OpenCreateProject);
        LoadMockProjects();
    }

    private void LoadMockProjects()
    {
        Projects.Add(new ProjectItem { Name = "Project Alpha", Description = "Build Alpha features." });
        Projects.Add(new ProjectItem { Name = "Project Beta", Description = "Test and deploy Beta." });
    }

    private void OpenCreateProject()
    {
        Main.CurrentPage = new CreateProjectViewModel(Main);

    }
    
}
