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
            List<Desafio> desafiosRealizados,
            List<Desafio> desafioNoCompletados,
            Desafio desafioPendiente)
        {
            this.Id = model.Id;
            this.Nombre = model.Nombre;
            this.Descripcion = model.Descripcion;
            this.NombreProfesor = model.Profesor.NombreCompleto;
            this.DesafiosNoCompletados = desafioNoCompletados.Select(item => item.Map()).ToList();
            this.DesafiosRealizados =
                desafiosRealizados != null ? desafiosRealizados.Select(item => item.Map()).ToList()
                : new List<DesafioViewModel>();
            this.DesafioPendiente = desafioPendiente.Map();
            
        }
    }
}
