﻿using System.Collections.Generic;
using System.Linq;
using HeraServices.ViewModels.EntitiesViewModels.Chart;
using Newtonsoft.Json;

namespace HeraServices.ViewModels.UtilityViewModels
{
    public class PieChartViewModel : IChartViewModel
    {
        public string Id { get; set; }
        public string Class { get; set; }

        public List<SingleValueSeriesViewModel> Models { get; set; }

        //options
        public bool ShowLabel { get; set; }
        public string LabelPosition { get; set; }
        public int LabelOffset { get; set; }

        public string ToJson =>
            JsonConvert.SerializeObject(
                new
                {
                    labels = Models.Select(m => m.Label),
                    series = Models.Select(m => m.Data)
                });

        public string ToJsonOptions =>
            JsonConvert.SerializeObject(
                new
                {
                    showLabel = ShowLabel,
                    labelPosition = LabelPosition,
                    labelOffset = LabelOffset
                });

        public ChartFooterViewModel GetFooterViewModel =>
            new ChartFooterViewModel()
            {
                Id = $"ct-footer-{Id}",
                LegendList = Models.Select(m => m.Name)
            };
    }
}
