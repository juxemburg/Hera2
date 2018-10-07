using System.Collections.Generic;
using System.Linq;
using HeraServices.ViewModels.EntitiesViewModels.Chart;
using Newtonsoft.Json;

namespace HeraServices.ViewModels.UtilityViewModels
{
    public class LineChartViewModel : IChartViewModel
    {
        public string Id { get; set; }
        public string Class { get; set; }
        public Dictionary<string, MultiValueSeriesViewModel>
            DataDictionary { get; set; }

        public float MaxValue => 0;

        public string ToJson =>
            JsonConvert.SerializeObject(
                new
                {
                   labels = DataDictionary.First().Value.Labels,
                   //series = DataDictionary.Values.Select(d => d.Data)

                });

        public string ToJsonOptions => "";
        public ChartFooterViewModel GetFooterViewModel  =>
            new ChartFooterViewModel()
            {
                Id = $"ct-footer-{Id}",
                LegendList = DataDictionary.First().Value.Labels
            };
    }
}
