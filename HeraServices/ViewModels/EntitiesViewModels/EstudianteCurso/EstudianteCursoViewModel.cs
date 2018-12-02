using Entities.Cursos;
using Entities.Desafios;
using Entities.Usuarios;
using HeraServices.ViewModels.EntitiesViewModels.Desafios;
using HeraServices.ViewModels.EntityMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeraServices.ViewModels.EntitiesViewModels.EstudianteCurso
{
    public class EstudianteCursoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public string NombreProfesor { get; set; }


        public List<DesafioViewModel> DesafiosRealizados { get; set; }
        public List<DesafioViewModel> DesafiosNoCompletados { get; set; }
        public DesafioViewModel DesafioPendiente { get; set; }

        public EstudianteCursoViewModel(Curso model,
            List<DesafioViewModel> desafiosRealizados,
            List<DesafioViewModel> desafioNoCompletados,
            Desafio desafioPendiente)
        {
            Id = model.Id;
            Nombre = model.Nombre;
            Descripcion = model.Descripcion;
            NombreProfesor = model.Profesor.NombreCompleto;
            DesafiosNoCompletados = desafioNoCompletados;
            DesafiosRealizados =
                desafiosRealizados != null ? desafiosRealizados : new List<DesafioViewModel>();
            DesafioPendiente = desafioPendiente.Map(0);

        }
    }
}
