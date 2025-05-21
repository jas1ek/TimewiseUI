using System;
using System.Collections.Generic;

namespace GetStartedApp.Models;

public partial class Task
{
    public int TaskId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Type { get; set; } = null!;

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public int AssignedEmployeeId { get; set; }

    public int? ParentTaskId { get; set; }

    public int ProjectId { get; set; }

    public virtual User AssignedEmployee { get; set; } = null!;

    public virtual ICollection<Task> InverseParentTask { get; set; } = new List<Task>();

    public virtual Task? ParentTask { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual ICollection<TimeLog> TimeLogs { get; set; } = new List<TimeLog>();
}
