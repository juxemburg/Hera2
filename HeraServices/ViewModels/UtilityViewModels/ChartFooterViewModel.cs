﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace HeraServices.ViewModels.UtilityViewModels
{
    public class ChartFooterViewModel
    {
        public string Id { get; set; }
        public IEnumerable<string> LegendList { get; set; }

        public string ToLegendJson =>
            JsonConvert.SerializeObject(new { legend = LegendList });
    }
}