using Entities.Desafios;
using HeraServices.Services.ScratchServices;
using System;
using System.Linq;
using System.Collections.Generic;

namespace HeraServices.DesafiosServices
{
    public class CalificacionDesafioService
    {
        public float GetCalificacionDesafio(
            TipoEvaluacion tipoEvaluacion,
            string param1,
            string param2,
            string param3,
            string param4,
            IEnumerable<Valoration_Scatch> results)
        {
            switch (tipoEvaluacion)
            {
                case TipoEvaluacion.ParallelCars:
                    return GetParallelCarsValoration(param1, results);
                case TipoEvaluacion.FlowPipes:
                    break;
                case TipoEvaluacion.RepeatingDucks:
                    break;
                default:
                    break;
            }

            return 0;
        }

        public float GetParallelCarsValoration(string param1, IEnumerable<Valoration_Scatch> results)
        {
            var recomendedThreads = float.MinValue;
            float.TryParse(param1, out recomendedThreads);

            var generalValoration = results.FirstOrDefault(item => item.generalValoration);
            if (generalValoration == null || recomendedThreads <= 0 || recomendedThreads == int.MinValue)
                return 0;

            var threadCount = generalValoration.ScriptCount;
            var viableThreads = (threadCount <= recomendedThreads) ? threadCount : Math.Max(0, Math.Min(threadCount, 2*recomendedThreads) - (2 * recomendedThreads));
            return viableThreads / recomendedThreads * 5;
        }
    }
}
