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
    }
}
