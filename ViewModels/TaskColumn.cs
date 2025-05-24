using System.Collections.ObjectModel;

namespace GetStartedApp.Models;

public class TaskColumn
{
    public string Name { get; set; } = "";
    public ObservableCollection<TaskItem> Tasks { get; set; } = new();
}
