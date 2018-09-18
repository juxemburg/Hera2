using Entities.Calificaciones;
using Entities.Desafios;
using HeraServices.ViewModels.EntitiesViewModels.Evaluacion.Scratch;
using HeraServices.ViewModels.EntityMapping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HeraServices.ViewModels.EntitiesViewModels.Evaluacion
{
    public class CalificacionViewModel
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        public int EstudianteId { get; set; }
        public int DesafioId { get; set; }

        public DateTime? Tiempoinicio { get; set; }
        public DateTime? TiempoFinal { get; set; }

        public ResultadoScratchViewModel ResultadoGeneral { get; set; }
        public virtual List<ResultadoScratchViewModel> ResultadoSprites { get; set; }

        public int? CalificacionCualitativaId { get; set; }

        public TimeSpan Duracion { get; set; }
        public bool EnCurso { get; set; }

        public EvaluacionViewModel Evaluacion { get; set; }


        public CalificacionViewModel(Calificacion cal,
            InfoDesafio infoDesafio)
        {

            Id = cal.Id;
            CursoId = cal.CursoId;
            EstudianteId = cal.EstudianteId;
            DesafioId = cal.DesafioId;

            Tiempoinicio = cal.Tiempoinicio;
            TiempoFinal = cal.TiempoFinal;

            if (cal.CalificacionCualitativa != null)
                CalificacionCualitativaId = cal.CalificacionCualitativa.Id;

            ResultadoSprites = cal.Resultados.Where(item => !item.General)
                .Select(item => item.ToViewModel()).ToList();

            ResultadoGeneral = cal.ResultadoGeneral.ToViewModel();

            Evaluacion = new EvaluacionViewModel(cal.ResultadoGeneral,
                infoDesafio);
        }
    }
}
