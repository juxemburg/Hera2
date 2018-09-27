using System;
using System.Collections.Generic;
using System.Text;

namespace HeraServices.ViewModels.EntitiesViewModels.Chart
{
    public class ChartLineViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<SingleValueSeriesViewModel> Values { get; set; }
    }
}
