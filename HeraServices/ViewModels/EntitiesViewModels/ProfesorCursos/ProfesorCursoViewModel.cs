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

            Desafios = curso.Desafios.Select(item => new CursoDesafioViewModel(item.Desafio, item.Initial)).ToList();
            DesafioInicial = new CursoDesafioViewModel(curso.Desafio, true);

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

            var activity = registroCurso
                .SelectMany(reg => reg.Calificaciones)
                .Where(cal => cal.Tiempoinicio.HasValue && cal.Tiempoinicio > (DateTime.Now.AddDays(-30)))
                .GroupBy(cal => cal.Tiempoinicio.GetValueOrDefault().ToShortDateString())
                .Select(grp => new SingleValueSeriesViewModel() { Label = grp.Key, Data = grp.Count() })
                .ToList();

            var completedChallenges = registroCurso
                .SelectMany(reg => reg.Calificaciones)
                .Where(cal => cal.Tiempoinicio.HasValue && cal.Tiempoinicio > DateTime.Now.AddDays(-30) && cal.Duracion.CompareTo(TimeSpan.FromMinutes(30)) > 0 )
                .GroupBy(cal => cal.Tiempoinicio.GetValueOrDefault().ToShortDateString())
                .Select(grp => new SingleValueSeriesViewModel() { Label = grp.Key, Data = grp.Count() })
                .OrderByDescending(item => item.Data)
                .ToList();

            var failedChallenges = registroCurso
                .SelectMany(reg => reg.Calificaciones)
                .Where(cal => cal.Tiempoinicio.HasValue && cal.Tiempoinicio > DateTime.Now.AddDays(-30) && cal.Duracion.CompareTo(TimeSpan.FromMinutes(30)) < 0)
                .GroupBy(cal => cal.Tiempoinicio.GetValueOrDefault().ToShortDateString())
                .Select(grp => new SingleValueSeriesViewModel() { Label = grp.Key, Data = grp.Count() })
                .OrderByDescending(item => item.Data)
                .ToList();

            //var blockFrequency = registroCurso
            //    .SelectMany(reg => reg.Calificaciones)
            //    .SelectMany(cal => cal.Resultados)
            //    .SelectMany(res => res.Bloques)
            //    .GroupBy(block => block.Nombre)
            //    .Select(grp => new SingleValueSeriesViewModel() { Label = grp.Key, Data = grp.Count() })
            //    .OrderByDescending(item => item.Data)
            //    .ToList();


            Info = new InfoCursoViewModel()
            {
                SexDistribution = new List<SingleValueSeriesViewModel>()
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
                CourseActivity = new ChartLineViewModel()
                {
                    Name = "Actividad del curso",
                    Description = "Últimos 30 días",
                    Values = activity
                },
                CompletedChallenges = new ChartLineViewModel()
                {
                    Name = "Desafíos exitosos",
                    Description = "Últimos 30 días",
                    Values = completedChallenges
                },
                FailedChallenges = new ChartLineViewModel()
                {
                    Name = "Desafíos fallidos",
                    Description = "Últimos 30 días",
                    Values = failedChallenges
                }
            };
        }
    }
}
