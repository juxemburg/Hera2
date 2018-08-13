using Entities.Desafios;
using Entities.Valoracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeraServices.ViewModels.EntitiesViewModels.EstudianteCurso
{
    public class DesafioCompletadoViewModel
    {
        public int CursoId { get; set; }
        

        public string Nombre { get; set; }

        public int ResultadoId { get; set; }
        public int NumScripts { get; set; }
        public int NumBloques { get; set; }
        public int DuplicateScriptsCount { get; set; }
        public int DeadCodeCount { get; set; }
        public int SiguienteDesafioId { get; set; }

        public DesafioCompletadoViewModel(int cursoId,
            ResultadoScratch resultado, Desafio siguienteDesafio)
        {
            this.CursoId = cursoId;

            this.ResultadoId = resultado.Id;
            this.NumScripts = resultado.NumScripts;
            this.NumBloques = resultado.NumBloques;
            this.DuplicateScriptsCount = resultado.DuplicateScriptsCount;
            this.DeadCodeCount = resultado.DeadCodeCount;
            this.Nombre = resultado.Nombre;
            
            this.SiguienteDesafioId = siguienteDesafio.Id;
        }
    }
}
