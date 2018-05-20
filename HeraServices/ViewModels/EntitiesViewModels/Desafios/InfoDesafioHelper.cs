using Entities.Desafios;
using HeraServices.ViewModels.UtilityViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeraServices.ViewModels.EntitiesViewModels.Desafios
{
    public static class InfoDesafioHelper
    {
        public static List<SelectListItemViewModel> 
            AbstraccionItems { get; }
            = new List<SelectListItemViewModel>()
            {
                new SelectListItemViewModel
                { Value= ""+(int)Desafio_Abstraccion.Ninguno,
                    Text ="Ninguno"},
                new SelectListItemViewModel
                { Value= ""+(int)Desafio_Abstraccion.NoBloquesNoUsados,
                    Text ="No bloques no usados"},
                new SelectListItemViewModel
                { Value= ""+(int)Desafio_Abstraccion.CreacionBloquesPropios,
                    Text ="Creación de bloques propios"},
                new SelectListItemViewModel
                { Value= ""+(int)Desafio_Abstraccion.UsoDeClones,
                    Text ="Uso de clones"},
            };

        public static List<SelectListItemViewModel>
            PensamientoAlgoritmicoItems
        { get; }
            = new List<SelectListItemViewModel>()
            {
                new SelectListItemViewModel
                { Value= ""+(int)Desafio_PensamientoAlgoritmico.Ninguno,
                    Text ="Ninguno"},
                new SelectListItemViewModel
                { Value= ""+(int)Desafio_PensamientoAlgoritmico.UsoSecuencias,
                    Text ="Uso de Secuencias"}
            };

        public static List<SelectListItemViewModel>
             DescomposicionProblemasItems
        { get; }
        = new List<SelectListItemViewModel>()
        {
            new SelectListItemViewModel
            {
                Value = ""+(int)Desafio_DescomposicionProblemas.Ninguno,
                Text = "Ninguno"
            },
            new SelectListItemViewModel
            {
                Value = ""+(int)Desafio_DescomposicionProblemas.DosHilosPorSprite,
                Text = "Dos hilos por sprite"
            },
            new SelectListItemViewModel
            {
                Value = ""+(int)Desafio_DescomposicionProblemas.MultiplesEventosPorSprite,
                Text = "Múltiples eventos por sprite"
            }
        };

        public static List<SelectListItemViewModel>
             ParalelismoItems
        { get; }
        = new List<SelectListItemViewModel>()
        {
            new SelectListItemViewModel
            {
                Value = ""+(int)Desafio_Paralelismo.Ninguno,
                Text = "Ninguno"
            },
            new SelectListItemViewModel
            {
                Value = ""+(int)Desafio_Paralelismo.DosHilosBanderaVerde,
                Text = "Dos hilos con bandera verde"
            },
            new SelectListItemViewModel
            {
                Value = ""+(int)Desafio_Paralelismo.EnvioYRecepcionMensajes,
                Text = "Envío y recepción de mensajes"
            },
            new SelectListItemViewModel
            {
                Value = ""+(int)Desafio_Paralelismo.DosProgramasEnOtrosEventos,
                Text = "Dos programas en otros eventos"
            }
        };

        public static List<SelectListItemViewModel>
             ControlFlujoItems
        { get; }
        = new List<SelectListItemViewModel>()
        {
            new SelectListItemViewModel
            {
                Value = ""+(int)Desafio_ControlFlujo.Ninguno,
                Text = "Ninguno"
            },
            new SelectListItemViewModel
            {
                Value = ""+(int)Desafio_ControlFlujo.FlujoSimple,
                Text = "Uso de flujos simples"
            },
            new SelectListItemViewModel
            {
                Value = ""+(int)Desafio_ControlFlujo.FlujoComplejo,
                Text = "Uso de flujos complejos"
            },
            new SelectListItemViewModel
            {
                Value = ""+(int)Desafio_ControlFlujo.FlujoAnidado,
                Text = "Uso de flujos anidados"
            }
        };

        public static List<SelectListItemViewModel>
             InteraccionItems
        { get; }
        = new List<SelectListItemViewModel>()
        {
            new SelectListItemViewModel
            {
                Value = ""+(int)Desafio_Interaccion.Ninguno,
                Text = "Ninguno"
            },
            new SelectListItemViewModel
            {
                Value = ""+(int)Desafio_Interaccion.InputBasico,
                Text = "Uso de bloques básicos de interacción"
            },
            new SelectListItemViewModel
            {
                Value = ""+(int)Desafio_Interaccion.UsoVariables,
                Text = "Uso de variables (no propias)"
            },
            new SelectListItemViewModel
            {
                Value = ""+(int)Desafio_Interaccion.SensoresSprite,
                Text = "Uso de sensores de sprite"
            }
        };

        public static List<SelectListItemViewModel>
             RepresentacionItems
        { get; }
        = new List<SelectListItemViewModel>()
        {
            new SelectListItemViewModel
            {
                Value = ""+(int)Desafio_Representacion.Ninguno,
                Text = "Ninguno"
            },
            new SelectListItemViewModel
            {
                Value = ""+(int)Desafio_Representacion.CreacionVariables,
                Text = "Creación de variables (propias)"
            },
            new SelectListItemViewModel
            {
                Value = ""+(int)Desafio_Representacion.UsoCompartidoVariables,
                Text = "Uso de variables en múltiples sprites"
            },
            new SelectListItemViewModel
            {
                Value = ""+(int)Desafio_Representacion.UsoListas,
                Text = "Uso de listas"
            }
        };

        public static List<SelectListItemViewModel>
             AnalisisItems
        { get; }
        = new List<SelectListItemViewModel>()
        {
            new SelectListItemViewModel
            {
                Value = ""+(int)Desafio_Analisis.Ninguno,
                Text = "Ninguno"
            },
            new SelectListItemViewModel
            {
                Value = ""+(int)Desafio_Analisis.OperadoresBasicos,
                Text = "Uso de operadores básicos"
            },
            new SelectListItemViewModel
            {
                Value = ""+(int)Desafio_Analisis.OperadoresComplejos,
                Text = "Uso de operadores complejos"
            },
            new SelectListItemViewModel
            {
                Value = ""+(int)Desafio_Analisis.OperadoresAnidados,
                Text = "Uso de operadores anidados"
            }
        };
    }
}
