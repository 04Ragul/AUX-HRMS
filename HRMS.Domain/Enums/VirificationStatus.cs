﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Enums
{
    public enum VirificationStatus : byte
    {
        Success,
        InProgress,
        NotStarted,
        Failed
    }
}
