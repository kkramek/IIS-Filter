﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filter
{
    interface IAnalyzer
    {
        bool IsRequestClean();
    }
}
