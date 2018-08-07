using System;
using System.Collections.Generic;
using System.Text;

namespace HeraServices.ViewModels.EntitiesViewModels.Desafios
{
    public class CalificacionInfoViewModel
    {
        public int Id { get; set; }

        public int CursoId { get; set; }
        public int EstudianteId { get; set; }
        public int DesafioId { get; set; }

        public DateTime? Tiempoinicio { get; set; }
        public DateTime? TiempoFinal { get; set; }

        public string DirArchivo { get; set; }

        public TimeSpan Duracion => (TiempoFinal - Tiempoinicio).GetValueOrDefault();

        public bool EnCurso => TiempoFinal == null;
    }
}
