﻿using System;
using System.Collections.Generic;

namespace CoreDB.DBWebApp;

public partial class UserEntity
{
    public long UserUid { get; set; }

    public int? Level { get; set; }

    public string? Name { get; set; }
}