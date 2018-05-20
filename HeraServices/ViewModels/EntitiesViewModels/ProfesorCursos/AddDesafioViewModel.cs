﻿using Entities.Desafios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HeraServices.ViewModels.EntitiesViewModels
{
    public class AddDesafioViewModel
    {
        public int DesafioId { get; set; }

        public int Id { get; set; }

        [Display(Name = "Nombre del Desafio")]
        public string Nombre { get; set; }

        public int Dificultad { get; set; }

        public string DirArchivo { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        public Desafio Map()
        {
            return new Desafio()
            {
                Nombre = this.Nombre,
                Descripcion = this.Descripcion,
                DirDesafioInicial = this.DirArchivo                               
            };
        }
    }
}
