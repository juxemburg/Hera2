using HeraServices.ViewModels.EntitiesViewModels.Chart;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeraServices.ViewModels.EntitiesViewModels.ProfesorCursos
{
    public class StudentTracesViewModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public ChartMultiLineViewModel GeneralTraces { get; set; }
        public List<ChallengeTraceViewModel> ChallengeTraces { get; set; }
    }

    public class ChallengeTraceViewModel
    {
        public int ChallengeId { get; set; }
        public string ChallengeName { get; set; }
        public ChartMultiLineViewModel ChartModel { get; set; }
    }
}
