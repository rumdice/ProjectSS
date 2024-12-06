using System;
using System.Collections.Generic;

namespace CoreDB.DBLogApp;

public partial class AccountEntity
{
    public long pid { get; set; }

    public string? name { get; set; }

    public string? password { get; set; }
}
