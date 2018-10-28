using Entities.Calificaciones;
using Entities.Cursos;
using HeraServices.ViewModels.EntitiesViewModels.Chart;
using System;
using System.Collections.Generic;
using System.Linq;
using Entities.Usuarios;
using HeraServices.ViewModels.EntitiesViewModels.Desafios;
using Entities.Valoracion;

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
            List<RegistroCalificacion> registroCurso,
            ChartMultiLineViewModel traces)
        {
            Id = curso.Id;
            NombreCurso = curso.Nombre;
            DescripcionCurso = curso.Descripcion;
            PasswordCurso = curso.Password;

            Desafios = curso.Desafios
                .OrderBy(rel  => rel.Orden)
                .Select(item => new CursoDesafioViewModel(item.Desafio, item.Initial)).ToList();
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
                .Where(cal => cal.Tiempoinicio.HasValue && cal.Tiempoinicio > DateTime.Now.AddDays(-30) && cal.Duracion.CompareTo(TimeSpan.FromMinutes(30)) > 0)
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

            var blockFrequency = registroCurso
                .SelectMany(reg => reg.Calificaciones)
                .SelectMany(cal => cal.Resultados)
                .SelectMany(res => res.Bloques)
                .GroupBy(block => block.Nombre)
                .Select(grp => new SingleValueSeriesViewModel() { Label = grp.Key, Data = grp.Count() })
                .OrderByDescending(item => item.Data)
                .ToList();

            var avgByCourse = registroCurso
                .GroupBy(reg => reg.Rel_CursoEstudiantes.Estudiante)
                .Select(grp => new
                {
                    EstudianteId = grp.Key.Id,
                    Nombre = grp.Key.NombreCompleto,
                    Calificaciones = grp
                    .SelectMany(reg => reg.Calificaciones)
                    .Select(cal => cal.ResultadoGeneral)
                    .Where(cal => cal != null)
                    .Select(res => new
                    {
                        res.IInfoScratch_General.SpriteCount,
                        res.IInfoScratch_General.NonUnusedBlocks,
                        res.IInfoScratch_General.UserDefinedBlocks,
                        res.IInfoScratch_General.CloneUse,
                        res.IInfoScratch_General.SecuenceUse,
                        res.IInfoScratch_General.MultipleThreads,
                        res.IInfoScratch_General.TwoGreenFlagTrhead,
                        res.IInfoScratch_General.AdvancedEventUse,
                        res.IInfoScratch_General.UseSimpleBlocks,
                        res.IInfoScratch_General.UseMediumBlocks,
                        res.IInfoScratch_General.UseNestedControl,
                        res.IInfoScratch_General.BasicInputUse,
                        res.IInfoScratch_General.VariableUse,
                        res.IInfoScratch_General.SpriteSensing,
                        res.IInfoScratch_General.VariableCreation,
                        res.IInfoScratch_General.BasicOperators,
                        res.IInfoScratch_General.MediumOperators,
                        res.IInfoScratch_General.AdvancedOperators
                    })
                    .Aggregate(
                        new
                        {
                            Count = 1,
                            SpriteCount = 0,
                            NonUnusedBlocks = 0,
                            UserDefinedBlocks = 0,
                            CloneUse = 0,
                            SecuenceUse = 0,
                            MultipleThreads = 0,
                            TwoGreenFlagTrhead = 0,
                            AdvancedEventUse = 0,
                            UseSimpleBlocks = 0,
                            UseMediumBlocks = 0,
                            UseNestedControl = 0,
                            BasicInputUse = 0,
                            VariableUse = 0,
                            SpriteSensing = 0,
                            VariableCreation = 0,
                            BasicOperators = 0,
                            MediumOperators = 0,
                            AdvancedOperators = 0
                        },
                        (acc, item) => new
                        {
                            Count = acc.Count + 1,
                            SpriteCount = (acc.SpriteCount + item.SpriteCount) / acc.Count,
                            NonUnusedBlocks = (acc.NonUnusedBlocks + item.NonUnusedBlocks) / acc.Count,
                            UserDefinedBlocks = (acc.UserDefinedBlocks + item.UserDefinedBlocks) / acc.Count,
                            CloneUse = (acc.CloneUse + item.CloneUse) / acc.Count,
                            SecuenceUse = (acc.SecuenceUse + item.SecuenceUse) / acc.Count,
                            MultipleThreads = (acc.MultipleThreads + item.MultipleThreads) / acc.Count,
                            TwoGreenFlagTrhead = (acc.TwoGreenFlagTrhead + item.TwoGreenFlagTrhead) / acc.Count,
                            AdvancedEventUse = (acc.AdvancedEventUse + item.AdvancedEventUse) / acc.Count,
                            UseSimpleBlocks = (acc.UseSimpleBlocks + item.UseSimpleBlocks) / acc.Count,
                            UseMediumBlocks = (acc.UseMediumBlocks + item.UseMediumBlocks) / acc.Count,
                            UseNestedControl = (acc.UseNestedControl + item.UseNestedControl) / acc.Count,
                            BasicInputUse = (acc.BasicInputUse + item.BasicInputUse) / acc.Count,
                            VariableUse = (acc.VariableUse + item.VariableUse) / acc.Count,
                            SpriteSensing = (acc.SpriteSensing + item.SpriteSensing) / acc.Count,
                            VariableCreation = (acc.VariableCreation + item.VariableCreation) / acc.Count,
                            BasicOperators = (acc.BasicOperators + item.BasicOperators) / acc.Count,
                            MediumOperators = (acc.MediumOperators + item.MediumOperators) / acc.Count,
                            AdvancedOperators = (acc.AdvancedOperators + item.AdvancedOperators)
                        })
                }).ToList();


            Info = new InfoCursoViewModel()
            {
                GeneralTraces = traces,
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
                },
                BlockFrequency = new ChartLineViewModel()
                {
                    Name = "Desafíos fallidos",
                    Description = "Últimos 30 días",
                    Values = blockFrequency
                },
                AvgByStudent = avgByCourse
            };
        }
    }
}
