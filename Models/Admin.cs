using System;
using System.Collections.Generic;

namespace GetStartedApp.Models;

public partial class Admin
{
    public int AdminId { get; set; }

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;
}
