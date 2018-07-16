using Entities.Cursos;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeraServices.ViewModels.EntitiesViewModels.Cursos
{
    public static class CursoMapper
    {
        public static CursoListViewModel MapToViewModel(this Curso entity)
        {
            return new CursoListViewModel()
            {
                Id = entity.Id,
                Activo = entity.Activo,
                Color = entity.Color,
                Descripcion = entity.Descripcion,
                Nombre = entity.Nombre,
                ProfesorId = entity.Profesor.Id,
                ProfesorNombre = entity.Profesor.NombreCompleto
            };
        }

    }
}
