using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeraServices.ViewModels.UtilityViewModels
{
    public interface IChartViewModel
    {
        string Id { get; set; }
        string ToJson { get; }
        string ToJsonOptions { get; }
        ChartFooterViewModel GetFooterViewModel { get; }

    }

}
