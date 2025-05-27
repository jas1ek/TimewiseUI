using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GetStartedApp.Models
{
    public partial class TaskItem : ObservableObject
    {
        public TaskItem()
        {
            StatusOptions = new ObservableCollection<string> { "To Do", "In Progress", "Done" };
        }

        public string Title { get; set; } = string.Empty;
        public string Project { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [ObservableProperty]
        private string timeSpent = string.Empty;

        public string Deadline { get; set; } = string.Empty;
        public string AssignedTo { get; set; } = string.Empty;

        [ObservableProperty]
        private string status = "To Do";

        [ObservableProperty]
        private string newTimeEntry = string.Empty;

        /// <summary>
        /// Per-task dropdown options for status.
        /// </summary>
        public ObservableCollection<string> StatusOptions { get; }

        /// <summary>
        /// Wired up by the VM so each item can execute AddTime().
        /// </summary>
        public ICommand AddTimeCommand { get; set; } = null!;

        /// <summary>
        /// Wired up by the VM so each item can execute CycleStatus().
        /// </summary>
        public ICommand CycleStatusCommand { get; set; } = null!;
    }
}
