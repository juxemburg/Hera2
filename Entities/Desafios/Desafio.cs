using Entities.Calificaciones;
using Entities.Usuarios;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Desafios
{
    public class Desafio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string DirDesafioInicial { get; set; }
        public string DirSolucion { get; set; }

        //Input evaluación
        public TipoEvaluacion TipoEvaluacion { get; set; }
        public string Param1 { get; set; }
        public string Param2 { get; set; }
        public string Param3 { get; set; }
        public string Param4 { get; set; }

        public int ProfesorId { get; set; }
        public virtual Profesor Profesor { get; set; }

        public InfoDesafio InfoDesafio { get; set; }
        public virtual List<RegistroCalificacion> Calificaciones { get; set; }

        public virtual List<Rel_Rating> Ratings { get; set; }
        public virtual List<Rel_DesafiosCursos> Cursos { get; set; }


        public float AverageRating
        {
            get
            {
                if (Ratings == null || Ratings.Count == 0)
                    return 0f;
                return (float)Ratings.Average(r => r.Rating);
            }
        }
        public int RatingCount
        {
            get
            {
                if (Ratings == null)
                    return 0;
                return Ratings.Count;
            }
        }
        public int Popularity { get => (Cursos == null) ? 0 : Cursos.Count; }
    }
}
