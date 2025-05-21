using System;
using System.Collections.Generic;

namespace GetStartedApp.Models;

public partial class Project
{
    public int ProjectId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int ManagerId { get; set; }

    public virtual User Manager { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
