using Entities.Desafios;
using HeraServices.Services.ScratchServices;
using System;
using System.Linq;
using System.Collections.Generic;
using Entities.Valoracion;

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
            IEnumerable<ResultadoScratch> results)
        {
            switch (tipoEvaluacion)
            {
                case TipoEvaluacion.ParallelCars:
                    return GetParallelCarsValoration(param1, results);
                case TipoEvaluacion.FlowPipes:
                    break;
                case TipoEvaluacion.RepeatingRains:
                    return GetRepeatingRainsValoration(param1, param2, param3, results);
                case TipoEvaluacion.CloningTroubles:
                    return GetCloningTroubleValoration(param1, param2, param3, results);
                default:
                    break;
            }
            return 0;
        }

        public float GetParallelCarsValoration(string param1, IEnumerable<ResultadoScratch> results)
        {
            var recomendedThreads = float.MinValue;
            float.TryParse(param1, out recomendedThreads);

            var generalValoration = results.FirstOrDefault(item => item.General);
            if (generalValoration == null || recomendedThreads <= 0 || recomendedThreads == int.MinValue)
                return 0;

            var threadCount = generalValoration.NumScripts;
            var viableThreads = (threadCount <= recomendedThreads) ? threadCount : Math.Max(0, Math.Min(threadCount, 2 * recomendedThreads) - (2 * recomendedThreads));
            return viableThreads / recomendedThreads * 5;
        }

        public float GetCloningTroubleValoration(string param1, string param2, string param3, IEnumerable<ResultadoScratch> results)
        {
            var generalValoration = results.FirstOrDefault(item => item.General);
            var cs = float.MinValue;
            var cmax = float.MinValue;

            float.TryParse(param1, out cs);
            float.TryParse(param1, out cmax);

            if (generalValoration == null || cs < 0 || cmax < 0)
                return 0;

            if (generalValoration.IInfoScratch_General.CloneCount > cmax)
                return 0;

            var cz = Math.Max(0, generalValoration.IInfoScratch_General.CloneCount - generalValoration.IInfoScratch_General.CloneRemovalCount);
            var c1 = Math.Clamp(generalValoration.IInfoScratch_General.CloneCount - cz, 0, cs);
            var c2 = Math.Max(0, generalValoration.IInfoScratch_General.CloneCount - c1);

            var result = 5 * (1 - (cs - c1) / (cs + c2 + cz));
            return Math.Max(0, result);
        }

        public float GetRepeatingRainsValoration(string param1, string param2, string param3, IEnumerable<ResultadoScratch> results)
        {
            var generalValoration = results.FirstOrDefault(item => item.General);
            var _is = float.MinValue;
            var nMax = float.MinValue;
            var nMin = float.MinValue;

            float.TryParse(param1, out _is);
            float.TryParse(param2, out nMax);
            float.TryParse(param3, out nMin);

            if (generalValoration == null || _is < 0 || nMax < 0 || nMin < 0)
                return 0;

            var spriteValoration = generalValoration.IInfoScratch_General;
            var n1 = Math.Min(nMax, spriteValoration.SequentialLoopsCount);
            var n2 = Math.Max(0, Math.Max(nMin - spriteValoration.SequentialLoopsCount, spriteValoration.SequentialLoopsCount - nMax));
            var s = generalValoration.DuplicateScriptsCount;

            var res = Math.Max(0, 1 - ((Math.Pow(n1 - _is, 2) * (1 + s)) / (_is * _is)));

            return (float)(5 * res);
        }
    }
}
