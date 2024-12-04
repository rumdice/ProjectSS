using System;
using System.Collections.Generic;

namespace CoreDB.DBWebApp;

public partial class UserEntity
{
    public long? pid { get; set; }

    public long? uid { get; set; }

    public string? name { get; set; }

    public int? level { get; set; }
}
