using Entities.Calificaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeraServices.ViewModels.EntitiesViewModels.ProfesorCursos
{
    public class CalificacionEstudianteViewModel
    {
        public int CursoId { get; set; }
        public int EstudianteId { get; set; }
        public int DesafioId { get; set; }
        public bool Valorada { get; set; }

        public CalificacionEstudianteViewModel(RegistroCalificacion registro)
        {
            this.CursoId = registro.CursoId;
            this.EstudianteId = registro.EstudianteId;
            this.DesafioId = registro.DesafioId;
            this.Valorada = registro.Valorada;
        }
    }
}
