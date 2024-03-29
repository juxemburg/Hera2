﻿using Entities.Cursos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Usuarios
{
    public class Estudiante: IUsuario
    {
        public Estudiante()
        {
            Role = "Estudiante";
        }

        public int Edad { get; set; }
        
        [Range(1, 11)]
        public int Grado { get; set; }

        public Genero Genero { get; set; }

        public EstudianteInfo_MateriaFavorita MateriaFavorita { get; set; }

        public EstudianteInfo_ActividadesPc ActividadesPc { get; set; }

        public EstudianteInfo_UsoPc FrecuenciaPc { get; set; }

        [Range(1, 10)]
        public int ManejoComputador { get; set; }

        [Range(1, 10)]
        public int ConocimientoComputador { get; set; }

        public string UsuarioScratch { get; set; }

        public virtual List<Rel_CursoEstudiantes> Cursos { get; set; }
    }
}
