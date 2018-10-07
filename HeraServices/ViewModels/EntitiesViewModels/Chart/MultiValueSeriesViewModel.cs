﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeraServices.ViewModels.EntitiesViewModels.Chart
{
    public class MultiValueSeriesViewModel
    {
        public List<List<float>> Values { get; set; }
        public List<string> Labels { get; set; }
        public string Name { get; set; }

    }
}
