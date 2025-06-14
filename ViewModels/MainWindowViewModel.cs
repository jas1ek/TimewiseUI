﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels
{
    /// <summary>
    /// The main ViewModel controlling page navigation and state.
    /// </summary>
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ViewModelBase currentPage;

        public ProjectsViewModel? ProjectsViewModel { get; set; }

        // Persist generated reports here
        public List<ReportItem> GeneratedReports { get; set; } = new();

        public MainWindowViewModel()
        {
            // Initialize login and subscribe to login success event
            var loginViewModel = new LoginViewModel();
            loginViewModel.LoginSucceeded += OnLoginSucceeded;
            CurrentPage = loginViewModel;

            // Initialize navigation commands
            ShowBoardCommand = new RelayCommand(ShowBoard);
            ShowTasksCommand = new RelayCommand(ShowTasks);
            ShowProjectsCommand = new RelayCommand(ShowProjects);
            ShowTimeLogsCommand = new RelayCommand(ShowTimeLogs);
            ShowReportsCommand = new RelayCommand(ShowReports);
        }

        // 🔹 Public navigation commands
        public ICommand ShowBoardCommand { get; }
        public ICommand ShowTasksCommand { get; }
        public ICommand ShowProjectsCommand { get; }
        public ICommand ShowTimeLogsCommand { get; }
        public ICommand ShowReportsCommand { get; }

        // 🔹 Login success handler
        private void OnLoginSucceeded(string username, string password, string type)
        {
            if (type == "admin" || type == "employee")
            {
                CurrentPage = new SecondViewModel(username, password, this);
            }
            else
            {
                Console.WriteLine("❌ Unknown user type");
            }
        }

        // 🔹 Navigation methods
        private void ShowBoard()
        {
            CurrentPage = new SecondViewModel("admin", "admin", this);
        }

        private void ShowTasks()
        {
            CurrentPage = new TasksViewModel(this);
        }

        private void ShowProjects()
        {
            if (ProjectsViewModel is null)
                ProjectsViewModel = new ProjectsViewModel(this);

            CurrentPage = ProjectsViewModel;
        }

        private void ShowTimeLogs()
        {
            CurrentPage = new TimeLogsViewModel(this);
        }

        private void ShowReports()
        {
            // Always show the last generated report (even if from GenerateReport)
            CurrentPage = new ReportsViewModel(this, GeneratedReports);
        }

        /// <summary>
        /// Navigates to ReportsViewModel with custom report data (used for generated reports).
        /// </summary>
        /// <param name="reportItems">List of report items to display</param>
        public void ShowReportsWithData(IEnumerable<ReportItem> reportItems)
        {
            GeneratedReports = reportItems.ToList();
            CurrentPage = new ReportsViewModel(this, GeneratedReports);
        }
    }
}
