using Entities.Desafios;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeraServices.ViewModels.EntitiesViewModels.ProfesorCursos
{
    public class CursoDesafioViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string DirDesafioInicial { get; set; }
        public string DirSolucion { get; set; }

        public int ProfesorId { get; set; }

        public CursoDesafioViewModel(Desafio desafio)
        {
            Id = desafio.Id;
            Nombre = desafio.Nombre;
            Descripcion = desafio.Descripcion;
            DirDesafioInicial = desafio.DirDesafioInicial;
            DirSolucion = desafio.DirSolucion;
            ProfesorId = desafio.ProfesorId;
        }
    }
}
