﻿using System;
using System.Collections.Generic;

namespace CoreLibrary.Database;

public partial class UserEntity
{
    public long UserUid { get; set; }

    public int? Level { get; set; }

    public string? Name { get; set; }
}
