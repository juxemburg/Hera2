using HeraServices.ViewModels.EntitiesViewModels.Chart;
using System.Collections.Generic;
using HeraServices.ViewModels.UtilityViewModels;

namespace HeraServices.ViewModels.EntitiesViewModels.ProfesorCursos
{
    public class InfoCursoViewModel
    {
        public List<SingleValueSeriesViewModel> SexDistribution { get; set; }
        public ChartLineViewModel CourseActivity { get; set; }
        public ChartLineViewModel CompletedChallenges { get; set; }
        public ChartLineViewModel FailedChallenges { get; set; }
        public ChartLineViewModel BlockFrequency { get; set; }

        public object AvgByStudent { get; set; }



        public PieChartViewModel GetDistribucionSexo(string clss,
            string labelPosition, int labelOffset, bool showLabel = true)
        {
            return ChartUtil.GetPieChartViewModel(SexDistribution, "chart-sex",
                clss, labelPosition, labelOffset, showLabel);
        }

    }
}
