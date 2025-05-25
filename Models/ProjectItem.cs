// Models/ProjectItem.cs
using System.Collections.ObjectModel;

namespace GetStartedApp.Models;

public class ProjectItem
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ObservableCollection<string> AssignedUsers { get; set; } = new();
}
