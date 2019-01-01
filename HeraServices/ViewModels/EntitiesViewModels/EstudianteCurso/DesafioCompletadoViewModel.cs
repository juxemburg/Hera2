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


        public int ThreadCount { get; set; }
        public int CloneCount { get; set; }
        public int CloneRemovalCount { get; set; }
        public int SequentialLoopsCount { get; set; }

        public float Puntuacion { get; set; }

        //Assessment params
        public TipoEvaluacion AssessmentType { get; set; }
        public string Param1 { get; set; }
        public string Param2 { get; set; }
        public string Param3 { get; set; }
        public string Param4 { get; set; }

        public DesafioCompletadoViewModel(int cursoId,
            ResultadoScratch resultado, Desafio desafioCompletado, Desafio siguienteDesafio, float puntuacion)
        {
            CursoId = cursoId;

            ResultadoId = resultado.Id;
            NumScripts = resultado.NumScripts;
            NumBloques = resultado.NumBloques;
            DuplicateScriptsCount = resultado.DuplicateScriptsCount;
            DeadCodeCount = resultado.DeadCodeCount;
            Nombre = resultado.Nombre;

            ThreadCount = resultado.IInfoScratch_General.ThreadCount;
            CloneCount = resultado.IInfoScratch_General.CloneCount;
            CloneRemovalCount = resultado.IInfoScratch_General.CloneRemovalCount;
            SequentialLoopsCount = resultado.IInfoScratch_General.SequentialLoopsCount;

            AssessmentType = desafioCompletado.TipoEvaluacion;
            Param1 = desafioCompletado.Param1;
            Param2 = desafioCompletado.Param2;
            Param3 = desafioCompletado.Param3;
            Param4 = desafioCompletado.Param4;
            
            SiguienteDesafioId = siguienteDesafio.Id;
            Puntuacion = puntuacion;
        }
    }
}
