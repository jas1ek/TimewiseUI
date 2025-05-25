using System.Collections.ObjectModel;

namespace GetStartedApp.Models;

public class ProjectItem
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ObservableCollection<string> AssignedUsers { get; set; } = new();
}
