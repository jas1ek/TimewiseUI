using System;
using System.Collections.Generic;

namespace GetStartedApp.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Type { get; set; } = null!;

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public override string ToString()
    {
        return $"UserId: {UserId}, Name: {Name}, Password: {Password}, Type: {Type}";
    }
}
