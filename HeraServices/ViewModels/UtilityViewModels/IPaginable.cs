﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeraServices.ViewModels.UtilityViewModels
{
    public interface IPaginable
    {
        List<Tuple<int, string>> Pages { get; set; }
        int Take { get; set; }
    }
}
