using System;
using System.Collections.Generic;
using System.Text;

namespace HeraServices.ViewModels.EntitiesViewModels.Evaluacion
{
    public class RegistroCalificacionViewModel
    {
        public int CursoId { get; set; }
        public int EstudianteId { get; set; }

        public int DesafioId { get; set; }
        public string NombreDesafio { get; set; }

        public bool Iniciada { get; set; }
        public bool Terminada { get; set; }

        public bool Valorada { get; set; }

        public List<CalificacionViewModel> Calificaciones { get; set; }

    }
}
