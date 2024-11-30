using System;
using System.Collections.Generic;

namespace CoreDB.DBLogApp;

public partial class AccountEntity
{
    public decimal Uid { get; set; }

    public string Aid { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Name { get; set; }
}
