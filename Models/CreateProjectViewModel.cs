using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels
{
    public partial class CreateProjectViewModel : ViewModelBase
    {
        [ObservableProperty] private string projectName = string.Empty;
        [ObservableProperty] private string projectDescription = string.Empty;
        [ObservableProperty] private string newAssignee = string.Empty;

        public ObservableCollection<string> AssignedUsers { get; } = new();

        private readonly MainWindowViewModel main;

        public CreateProjectViewModel(MainWindowViewModel main)
        {
            this.main = main ?? throw new ArgumentNullException(nameof(main));
            AddAssigneeCommand = new RelayCommand(AddAssignee);
            SaveCommand        = new RelayCommand(Save);
            CancelCommand      = new RelayCommand(GoBack);
        }

        public IRelayCommand AddAssigneeCommand { get; }
        public IRelayCommand SaveCommand        { get; }
        public IRelayCommand CancelCommand      { get; }

        private void AddAssignee()
        {
            var name = NewAssignee.Trim();
            if (!string.IsNullOrWhiteSpace(name) && !AssignedUsers.Contains(name))
            {
                AssignedUsers.Add(name);
                NewAssignee = string.Empty;
            }
        }

        private void Save()
        {
            if (string.IsNullOrWhiteSpace(ProjectName)) return;

            var p = new ProjectItem {
                Name          = ProjectName.Trim(),
                Description   = ProjectDescription.Trim(),
                AssignedUsers = new ObservableCollection<string>(AssignedUsers)
            };

            main.ProjectsViewModel ??= new ProjectsViewModel(main);
            main.ProjectsViewModel.Projects.Add(p);
            main.CurrentPage = main.ProjectsViewModel!;
        }

        private void GoBack()
        {
            main.CurrentPage = main.ProjectsViewModel!;
        }
    }
}
