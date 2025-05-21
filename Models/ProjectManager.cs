using System;
using System.Collections.Generic;

namespace GetStartedApp.Models;

public partial class ProjectManager
{
    public int ProjectManagerId { get; set; }

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;
}
