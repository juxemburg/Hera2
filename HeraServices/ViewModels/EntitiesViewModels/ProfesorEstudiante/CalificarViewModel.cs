using System;
using Entities.Calificaciones;

namespace HeraServices.ViewModels.EntitiesViewModels.ProfesorEstudiante
{
    public class CreateCalificacionCualitativaViewModel
    {
        public int? Id { get; set; }
        public int Valoracion { get; set; }
        public bool Completada { get; set; }
        public string Descripcion { get; set; }
        public int? CalificacionId { get; set; }

        public CreateCalificacionCualitativaViewModel()
        {
            
        }

        public CreateCalificacionCualitativaViewModel(int id)
        {
            CalificacionId = id;
            Completada = false;
        }


        public CalificacionCualitativa Map()
        {
            return new CalificacionCualitativa
            {
                CalificacionId = CalificacionId.GetValueOrDefault(),
                FechaRegistro = DateTime.Now,
                Descripcion = Descripcion,
                Completada = Completada,
                Valoracion = Valoracion
            };
        }
    }
}
