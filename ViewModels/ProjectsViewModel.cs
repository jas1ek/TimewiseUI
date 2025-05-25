using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels;

public partial class ProjectsViewModel : ViewModelBase
{
    public MainWindowViewModel Main { get; }

    public ObservableCollection<ProjectItem> Projects { get; set; }

    public ProjectsViewModel(MainWindowViewModel main)
    {
        Main = main;

        Projects = new ObservableCollection<ProjectItem>
        {
            new ProjectItem { Name = "Redesign Website", Description = "Update company website to reflect new branding." },
            new ProjectItem { Name = "Internal Dashboard", Description = "Develop dashboard for employee productivity tracking." },
            new ProjectItem { Name = "Mobile App MVP", Description = "Prototype the core features of the new mobile app." }
        };
    }
}
