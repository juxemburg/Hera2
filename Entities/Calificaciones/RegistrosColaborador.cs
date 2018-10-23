using Entities.Cursos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Calificaciones
{
    public class RegistrosColaborador
    {
        public int Id { get; set; }

        //Colaborador principal
        public int CursoId { get; set; }
        public int EstudianteId { get; set; }
        public int DesafioId { get; set; }
        public RegistroCalificacion Calificacion { get; set; }

        
        public int ColaboradorId { get; set; }
        public Rel_CursoEstudiantes Rel_CursoEstudiante { get; set; }


    }
}
