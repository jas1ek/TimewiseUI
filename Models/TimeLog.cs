using System;

namespace GetStartedApp.Models
{
    public class TimeLog
    {
        // EF properties
        public int TimeLogId   { get; set; }
        public DateTime Date   { get; set; }
        public bool IsApproved { get; set; }
        public int TaskId      { get; set; }
        public TimeSpan TimeSpent { get; set; }

        // EF navigation
        public Task Task { get; set; } = null!;

        // UI‐only:
        public string UserName    { get; set; } = "User1";
        public string ProjectName { get; set; } = "";
        public string TaskName    { get; set; } = "";
        public TimeSpan Duration  => TimeSpent;  // or compute EndTime-StartTime
        public string Description { get; set; } = "";
    }
}
