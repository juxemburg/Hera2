using System;
using System.Collections.Generic;
using System.Text;

namespace HeraServices.ViewModels.EntitiesViewModels.Desafios
{
    public class DesafioViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public float PuntuacionMax { get; set; }
        public bool Completado { get; set; }
        public float Valoracion { get; set; }
    }
}
