﻿namespace Schedule.Core.Models;

public class Surname
{
    public string Value { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}