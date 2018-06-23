using Entities.Usuarios;
using HeraServices.ViewModels.UtilityViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeraServices.ViewModels.EntitiesViewModels.Estudiantes
{
    public static class EstudianteInfoHelper
    {
        public static List<SelectListItemViewModel> EstudianteInfo_UsoPcList { get; }
            = new List<SelectListItemViewModel>()
            {
                new SelectListItemViewModel()
                {
                    Value = ""+(int)EstudianteInfo_UsoPc.TodosLosDias,
                    Text = "Todos los días"
                },
                new SelectListItemViewModel()
                {
                    Value = ""+(int)EstudianteInfo_UsoPc.UnaVezSemana,
                    Text = "Una vez a la semana"
                },
                new SelectListItemViewModel()
                {
                    Value = ""+(int)EstudianteInfo_UsoPc.RaraVez,
                    Text = "Rara Vez"
                },
                new SelectListItemViewModel()
                {
                    Value = ""+(int)EstudianteInfo_UsoPc.CasiNunca,
                    Text = "Casi nunca"
                }
            };

        public static List<SelectListItemViewModel> EstudianteInfo_ActividadesList { get; }
            = new List<SelectListItemViewModel>()
            {
                new SelectListItemViewModel()
                {
                    Value = ""+
                    (int)EstudianteInfo_ActividadesPc.RedesSociales,
                    Text= "Redes Sociales"
                },
                new SelectListItemViewModel()
                {
                    Value = ""+
                    (int)EstudianteInfo_ActividadesPc.Videos,
                    Text= "Ver vídeos"
                },
                new SelectListItemViewModel()
                {
                    Value = ""+
                    (int)EstudianteInfo_ActividadesPc.Peliculas_Series,
                    Text= "ver películas y/o series"
                },
                new SelectListItemViewModel()
                {
                    Value = ""+
                    (int)EstudianteInfo_ActividadesPc.BusquedaInformacion,
                    Text= "Buscar información"
                },
                new SelectListItemViewModel()
                {
                    Value = ""+
                    (int)EstudianteInfo_ActividadesPc.AprenderMas,
                    Text= "Aprender más"
                },
                new SelectListItemViewModel()
                {
                    Value = ""+
                    (int)EstudianteInfo_ActividadesPc.HacerTareas,
                    Text= "Hacer Tareas"
                },
                new SelectListItemViewModel()
                {
                    Value = ""+
                    (int)EstudianteInfo_ActividadesPc.Jugar,
                    Text= "Jugar Videojuegos"
                },
                new SelectListItemViewModel()
                {
                    Value = ""+
                    (int)EstudianteInfo_ActividadesPc.Comunicacion,
                    Text= "Comunicarme con familiares y amigos"
                },
                new SelectListItemViewModel()
                {
                    Value = ""+
                    (int)EstudianteInfo_ActividadesPc.Otros,
                    Text= "Otros"
                },
            };

        public static List<SelectListItemViewModel> EstudianteInfo_GeneroList
        { get; }
            = new List<SelectListItemViewModel>()
            {
                new SelectListItemViewModel()
                {
                    Value = ""+(int)Genero.Femenino,
                    Text = "Femenino"
                },
                new SelectListItemViewModel()
                {
                    Value = ""+(int)Genero.Masculino,
                    Text = "Masculino"
                }
            };

        public static List<SelectListItemViewModel>
            EstudianteInfo_MateriaFavoritaList
        { get; }
        = new List<SelectListItemViewModel>()
        {
            new SelectListItemViewModel()
            {
                Value = ""+(int)EstudianteInfo_MateriaFavorita.Matematicas,
                Text = "Matemáticas/Geometría"
            },
            new SelectListItemViewModel()
            {
                Value = ""+(int)EstudianteInfo_MateriaFavorita.Historia,
                Text = "Historia"
            },
            new SelectListItemViewModel()
            {
                Value = ""+(int)EstudianteInfo_MateriaFavorita.Geografia,
                Text = "Geografía"
            },
            new SelectListItemViewModel()
            {
                Value = ""+(int)EstudianteInfo_MateriaFavorita.Espanol,
                Text = "Español"
            },
            new SelectListItemViewModel()
            {
                Value = ""+(int)EstudianteInfo_MateriaFavorita.Ingles,
                Text = "Inglés"
            },
            new SelectListItemViewModel()
            {
                Value = ""+(int)EstudianteInfo_MateriaFavorita.Artes,
                Text = "Artes"
            },
            new SelectListItemViewModel()
            {
                Value = ""+(int)EstudianteInfo_MateriaFavorita.Informatica,
                Text = "Informática"
            },
            new SelectListItemViewModel()
            {
                Value = ""+(int)EstudianteInfo_MateriaFavorita.EdFisica,
                Text = "Educación Física"
            },
            new SelectListItemViewModel()
            {
                Value = ""+(int)EstudianteInfo_MateriaFavorita.Musica,
                Text = "Música"
            }
        };
    }
}
