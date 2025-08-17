using System;
using System.Collections.Generic;

namespace CoreDB.DBLogApp;

public partial class AccountEntity
{
    public long Pid { get; set; }

    public string? Name { get; set; }

    public string? Password { get; set; }
}
