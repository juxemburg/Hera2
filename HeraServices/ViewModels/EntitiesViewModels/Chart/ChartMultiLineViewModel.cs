using System;
using System.Collections.Generic;
using System.Text;

namespace HeraServices.ViewModels.EntitiesViewModels.Chart
{
    public class ChartMultiLineViewModel
    {
        public List<List<float>> Values { get; set; }
        public List<string> Labels { get; set; }
        public string Name { get; set; }
    }
}
