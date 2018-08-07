using Entities.Calificaciones;
using Entities.Cursos;
using HeraServices.ViewModels.EntitiesViewModels.Desafios;
using HeraServices.ViewModels.EntityMapping;
using System.Collections.Generic;
using System.Linq;

namespace HeraServices.ViewModels.EntitiesViewModels.EstudianteDesafio
{
    public class CalificacionDesafioViewModel
    {
        public int CursoId { get; set; }
        public int EstudianteId { get; set; }

        public int DesafioId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        

        public virtual List<CalificacionInfoViewModel> Calificaciones { get; set; }

        public bool Iniciada
        {
            get
            {
                return (Calificaciones == null) || 
                    Calificaciones.Any(cal => cal.EnCurso);

            }
        }

        public bool Terminada
        {
            get
            {
                return (Calificaciones != null) 
                    && (Calificaciones.Any(cal => !cal.EnCurso)
                    && Calificaciones.Count > 0);
            }
        }

        public CalificacionInfoViewModel CalificacionPendiente
        {
            get
            {
                return Calificaciones
                    .FirstOrDefault(cal => cal.EnCurso);
            }
        }

        public CalificacionDesafioViewModel(RegistroCalificacion model)
        {
            CursoId = model.CursoId;
            EstudianteId = model.EstudianteId;
            DesafioId = model.Desafio.Id;
            Nombre = model.Desafio.Nombre;
            Descripcion = model.Desafio.Descripcion;

            Calificaciones = model.Calificaciones
                .Select(cal => cal.ToViewModel())
                .ToList();
        }
    }
}
