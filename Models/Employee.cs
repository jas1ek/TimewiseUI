using System;
using System.Collections.Generic;

namespace GetStartedApp.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;
}
