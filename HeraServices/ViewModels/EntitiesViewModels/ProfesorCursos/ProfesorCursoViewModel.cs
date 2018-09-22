using Entities.Calificaciones;
using Entities.Cursos;
using HeraServices.ViewModels.EntitiesViewModels.Chart;
using System;
using System.Collections.Generic;
using System.Linq;
using Entities.Usuarios;
using HeraServices.ViewModels.EntitiesViewModels.Desafios;

namespace HeraServices.ViewModels.EntitiesViewModels.ProfesorCursos
{
    public class ProfesorCursoViewModel
    {

        public int Id { get; set; }
        public string NombreCurso { get; set; }
        public string DescripcionCurso { get; set; }
        public string PasswordCurso { get; set; }
        public CursoDesafioViewModel DesafioInicial { get; set; }
        public List<CursoDesafioViewModel> Desafios { get; set; }

        public Dictionary<int, string> Estudiantes { get; set; }

        public InfoCursoViewModel Info { get; set; }

        public ProfesorCursoViewModel(
            Curso curso,
            List<RegistroCalificacion> registroCurso)
        {
            Id = curso.Id;
            NombreCurso = curso.Nombre;
            DescripcionCurso = curso.Descripcion;
            PasswordCurso = curso.Password;

            Desafios = curso.Desafios.Select(item => new CursoDesafioViewModel(item.Desafio)).ToList();
            DesafioInicial = new CursoDesafioViewModel(curso.Desafio);

            Estudiantes = curso.Estudiantes
                .ToDictionary(item => item.EstudianteId, item => item.Estudiante.NombreCompleto);

            

            var numM = curso.Estudiantes
                    .Count(rel =>
                    rel.Estudiante.Genero == Genero.Masculino);
            var numF =
                curso.Estudiantes
                    .Count(rel =>
                    rel.Estudiante.Genero == Genero.Femenino);
            var dates = Enumerable.Range(0, 7)
                .Select(d => DateTime.Now.AddDays((d * -1)));


            Info = new InfoCursoViewModel()
            {
                DistSexo = new List<SingleValueSeriesViewModel>()
                {
                    new SingleValueSeriesViewModel()
                    {
                        Data = numM,
                        Label = $"{ChartUtil.Percentage(numM, curso.Estudiantes.Count)}%",
                        Name = "Masculino"
                    },
                    new SingleValueSeriesViewModel()
                    {
                        Data = numF,
                        Label = $"{ChartUtil.Percentage(numF, curso.Estudiantes.Count)}%",
                        Name = "Femenino"
                    }
                },
                //ActividadCurso = new Dictionary<string, MultiValueSeriesViewModel>()
                //{

                //    ["Número de Calificaciones"]
                //        = new MultiValueSeriesViewModel()
                //        {
                //            Data = group.Select(grp => (float)grp.Count),
                //            Labels = group.Select(grp => grp.Key),
                //            Name = "Número de Calificaciones"
                //        }

                //}

            };
        }
    }
}
