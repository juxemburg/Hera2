using Entities.Cursos;
namespace Entities.Desafios
{
    public class Rel_DesafiosCursos
    {
        public bool Initial { get; set; }

        public int DesafioId { get; set; }
        public virtual Desafio Desafio { get; set; }

        public int CursoId { get; set; }
        public virtual Curso Curso { get; set; }
    }
}
