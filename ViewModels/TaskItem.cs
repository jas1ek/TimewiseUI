namespace GetStartedApp.Models;

public class TaskItem
{
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Project { get; set; } = "";
    public string TimeSpent { get; set; } = "";
    public string Deadline { get; set; } = "";
    public string AssignedTo { get; set; } = "";

    // 🔵 Optional enhancements for future use:
    public string Status { get; set; } = "";        // To Do, In Progress, Done
    public string Priority { get; set; } = "";      // Low, Medium, High
    public string AvatarUrl { get; set; } = "";     // URL or path to user icon
}
