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
        public int CalificacionId { get; set; }
        public Calificacion Calificacion { get; set; }


        public int CursoId { get; set; }
        public int EstudianteId { get; set; }
        public Rel_CursoEstudiantes Rel_CursoEstudiante { get; set; }


    }
}
