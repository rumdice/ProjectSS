﻿using System;
using System.Collections.Generic;

namespace CoreDB.DBWebApp;

public partial class ItemSimpleEntity
{
    public long ItemUid { get; set; }

    public long? UserUid { get; set; }

    public long? ItemTid { get; set; }

    public string? Name { get; set; }

    public int? Grade { get; set; }
}
