using Entities.Colors;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeraServices.ViewModels.EntitiesViewModels.Cursos
{
    public class CursoListViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }

        public Color Color { get; set; }
        public string ColorName => ColorHelper.Get_ColorName(Color);
        

        public int ProfesorId { get; set; }
        public string ProfesorNombre { get; set; }
    }
}
