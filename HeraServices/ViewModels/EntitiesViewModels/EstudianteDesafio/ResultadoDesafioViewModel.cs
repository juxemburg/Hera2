using Entities.Calificaciones;
using Entities.Valoracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeraServices.ViewModels.EntitiesViewModels.EstudianteDesafio
{
    public class ResultadoDesafioViewModel
    {
        public int Id { get; set; }

        public DateTime? Tiempoinicio { get; set; }
        public DateTime? TiempoFinal { get; set; }
    
        public int RegistroCalificacionId { get; set; }

        public string DirArchivo { get; set; }


        public TimeSpan Duracion { get { return (TiempoFinal - Tiempoinicio).GetValueOrDefault(); } }
        public bool EnCurso
        {
            get
            {
                return TiempoFinal == null;
            }
        }
        public void TerminarCalificacion(string dirArchivo)
        {
            this.DirArchivo = dirArchivo;
            TiempoFinal = DateTime.Now;
        }

        public ResultadoDesafioViewModel(Calificacion model)
        {
            Id = model.Id;
            TiempoFinal = model.TiempoFinal;
            Tiempoinicio = model.Tiempoinicio;
            DirArchivo = model.DirArchivo;           
        }

    }
}
