using HeraServices.ViewModels.UtilityViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeraServices.ViewModels.AccountViewModels
{
    public class EstudianteRegistrationMetadataViewModel
    {
        public List<SelectListItemViewModel> GenderOptions { get; set; }
        public List<SelectListItemViewModel> FavoriteSubjectOptions { get; set; }
        public List<SelectListItemViewModel> PcActivitiesOptions { get; set; }
        public List<SelectListItemViewModel> PcFrecuencyOptions { get; set; }
    }
}
