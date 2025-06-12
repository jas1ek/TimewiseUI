using System;

namespace GetStartedApp.Models
{
    public class TimeLog
    {
        // Database properties
        public int TimeLogId { get; set; }
        public DateTime Date { get; set; }
        public bool IsApproved { get; set; }
        public int TaskId { get; set; }
        public TimeSpan TimeSpent { get; set; }

        // Navigation property (if you use EF)
        public Task? Task { get; set; }

        // UI-only
        public string UserName { get; set; } = "User1";
        public string ProjectName { get; set; } = "";
        public string TaskName { get; set; } = "";
        public TimeSpan Duration => TimeSpent;
        public string Description { get; set; } = "";
        public bool IsSelected { get; set; }   // <-- For checkbox selection!
    }
}
