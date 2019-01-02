using Entities.Calificaciones;
using Entities.Desafios;
using Entities.Valoracion;
using HeraServices.ViewModels.EntitiesViewModels.Desafios;
using HeraServices.ViewModels.EntitiesViewModels.EstudianteDesafio;
using HeraServices.ViewModels.EntitiesViewModels.Evaluacion;
using HeraServices.ViewModels.EntitiesViewModels.Evaluacion.Scratch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeraServices.ViewModels.EntityMapping
{
    public static class MappingExtensions
    {
        public static DesafioViewModel Map(this Desafio model, bool completada, float puntuacion, float valoracion)
        {
            return new DesafioViewModel()
            {
                Id = model.Id,
                Descripcion = model.Descripcion,
                Nombre = model.Nombre,
                PuntuacionMax = puntuacion,
                Completado = completada,
                Valoracion = valoracion
            };
        }

        public static CalificacionInfoViewModel ToViewModel(this Calificacion model)
        {
            return new CalificacionInfoViewModel()
            {
                Id = model.Id,
                CursoId = model.CursoId,
                DesafioId = model.DesafioId,
                EstudianteId = model.EstudianteId,
                Puntuacion = model.Puntuacion,

                Tiempoinicio = model.Tiempoinicio,
                TiempoFinal = model.TiempoFinal,

                DirArchivo = model.DirArchivo
            };
        }

        public static InfoScratchSpriteViewModel ToViewModel(this IInfoScratch_Sprite model)
        {
            return new InfoScratchSpriteViewModel()
            {
                Id = model.Id,
                ResultadoScratchId = model.ResultadoScratchId,
                NonUnusedBlocks = model.NonUnusedBlocks,
                UserDefinedBlocks = model.UserDefinedBlocks,
                CloneUse = model.CloneUse,
                SecuenceUse = model.SecuenceUse,
                MultipleThreads = model.MultipleThreads,
                TwoGreenFlagTrhead = model.TwoGreenFlagTrhead,
                AdvancedEventUse = model.AdvancedEventUse,
                UseSimpleBlocks = model.UseSimpleBlocks,
                UseMediumBlocks = model.UseMediumBlocks,
                UseNestedControl = model.UseNestedControl,
                BasicInputUse = model.BasicInputUse,
                VariableUse = model.VariableUse,
                SpriteSensing = model.SpriteSensing,
                VariableCreation = model.VariableCreation,
                BasicOperators = model.BasicOperators,
                MediumOperators = model.MediumOperators,
                AdvancedOperators = model.AdvancedOperators
            };
        }

        public static InfoScratchGeneralViewModel ToViewModel(this IInfoScratch_General model)
        {
            return new InfoScratchGeneralViewModel()
            {
                Id = model.Id,

                ResultadoScratchId = model.ResultadoScratchId,

                SpriteCount = model.SpriteCount,

                EventsUse = model.EventsUse,
                MessageUse = model.MessageUse,
                SharedVariables = model.SharedVariables,
                ListUse = model.ListUse,

                NonUnusedBlocks = model.NonUnusedBlocks,
                UserDefinedBlocks = model.UserDefinedBlocks,
                CloneUse = model.CloneUse,
                SecuenceUse = model.SecuenceUse,
                MultipleThreads = model.MultipleThreads,
                TwoGreenFlagTrhead = model.TwoGreenFlagTrhead,
                AdvancedEventUse = model.AdvancedEventUse,
                UseSimpleBlocks = model.UseSimpleBlocks,
                UseMediumBlocks = model.UseMediumBlocks,
                UseNestedControl = model.UseNestedControl,
                BasicInputUse = model.BasicInputUse,
                VariableUse = model.VariableUse,
                SpriteSensing = model.SpriteSensing,
                VariableCreation = model.VariableCreation,
                BasicOperators = model.BasicOperators,
                MediumOperators = model.MediumOperators,
                AdvancedOperators = model.AdvancedOperators
            };
        }

        public static ResultadoScratchViewModel ToViewModel(this ResultadoScratch model)
        {
            return new ResultadoScratchViewModel()
            {
                Id = model.Id,
                CalificacionId = model.CalificacionId,

                General = model.General,

                Nombre = model.Nombre,
                NumScripts = model.NumScripts,
                NumBloques = model.NumBloques,
                DuplicateScriptsCount = model.DuplicateScriptsCount,
                DeadCodeCount = model.DeadCodeCount,


                IInfoScratch_Sprite = model.IInfoScratch_Sprite != null ? model.IInfoScratch_Sprite.ToViewModel() : null,

                IInfoScratch_General = model.IInfoScratch_General != null ? model.IInfoScratch_General.ToViewModel() : null,
            };
        }

        public static RegistroCalificacionViewModel ToViewModel(this RegistroCalificacion model)
        {
            return new RegistroCalificacionViewModel()
            {
                CursoId = model.CursoId,
                EstudianteId = model.EstudianteId,

                DesafioId = model.DesafioId,
                NombreDesafio = model.Desafio.Nombre,

                Iniciada = model.Iniciada,
                Terminada = model.Terminada,

                Valorada = model.Valorada,

                Calificaciones = model.Calificaciones.Where(cal => cal != null && cal.ResultadoGeneral != null).Select(cal => new CalificacionViewModel(cal, model.Desafio.InfoDesafio)).ToList()
            };
        }
    }
}
