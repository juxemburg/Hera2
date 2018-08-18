using System;
using System.Collections.Generic;
using System.Text;

namespace HeraServices.ViewModels.EntitiesViewModels.Evaluacion.Scratch
{
    public class ResultadoScratchViewModel
    {
        public int Id { get; set; }
        public int CalificacionId { get; set; }

        public bool General { get; set; }

        public string Nombre { get; set; }
        public int NumScripts { get; set; }
        public int NumBloques { get; set; }
        public int DuplicateScriptsCount { get; set; }
        public int DeadCodeCount { get; set; }
        

        public virtual InfoScratchSpriteViewModel IInfoScratch_Sprite { get; set; }
        
        public virtual InfoScratchGeneralViewModel IInfoScratch_General { get; set; }

    }
}
