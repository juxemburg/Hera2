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
        public ResultadoScratch Resultado { get; set; }
        public int SiguienteDesafioId { get; set; }

        public DesafioCompletadoViewModel(int cursoId,
            ResultadoScratch resultado, Desafio siguienteDesafio)
        {
            this.CursoId = cursoId;
            this.Resultado = resultado;
            this.SiguienteDesafioId = siguienteDesafio.Id;
        }
    }
}
