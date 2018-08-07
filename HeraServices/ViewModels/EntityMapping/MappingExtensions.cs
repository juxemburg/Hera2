using Entities.Calificaciones;
using Entities.Desafios;
using HeraServices.ViewModels.EntitiesViewModels.Desafios;
using HeraServices.ViewModels.EntitiesViewModels.EstudianteDesafio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeraServices.ViewModels.EntityMapping
{
    public static class MappingExtensions
    {
        public static DesafioViewModel Map(this Desafio model)
        {
            return new DesafioViewModel()
            {
                Id = model.Id,
                Descripcion = model.Descripcion,
                Nombre = model.Nombre
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

                Tiempoinicio = model.Tiempoinicio,
                TiempoFinal = model.TiempoFinal,

                DirArchivo = model.DirArchivo
            };
        }
    }
}
