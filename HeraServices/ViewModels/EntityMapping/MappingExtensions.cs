using Entities.Desafios;
using HeraServices.ViewModels.EntitiesViewModels.Desafios;
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
    }
}
