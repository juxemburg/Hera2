using Entities.Desafios;
using HeraDAL.DataAcess;
using HeraServices.ViewModels.ApiViewModels;
using HeraServices.ViewModels.ApiViewModels.Exceptions;
using HeraServices.ViewModels.EntitiesViewModels.EstudianteCurso;
using HeraServices.ViewModels.EntityMapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeraServices.Services.DesafiosServices
{
    public class DesafioEstudianteService
    {
        private readonly IDataAccess _data;
        private readonly Random _random;

        public DesafioEstudianteService(IDataAccess data)
        {
            _data = data;
            _random = new Random();
        }

        /// <summary>
        /// Método que obtiene el modelo de la relación
        /// entre uun estudiante y curso
        /// </summary>
        /// <param name="idCurso">identificador del curso</param>
        /// <param name="idEstudiante">identificador del estudiante</param>
        /// <returns>Modelo EstudianteCursoViewModel</returns>
        public async Task<EstudianteCursoViewModel>
            Get_RelEstudianteCurso(int idCurso,
            int idEstudiante)
        {
            var model = await _data
                    .Find_Rel_CursoEstudiantes(idCurso, idEstudiante);
            var desafiosRealizados = model.Registros
                .Where(rel => rel.Terminada
                && rel.Desafio != null)
                .Select(rel => rel.Desafio.Map(rel.Valorada, rel.PuntuacionMaxima, rel.ValoracionMaxima))
                .ToList();
            var desafiosNoCompletados = model.Registros
                .Where(rel => !rel.Terminada
                && rel.Desafio != null)
                .Select(rel => rel.Desafio.Map(rel.Valorada, rel.PuntuacionMaxima, rel.ValoracionMaxima))
                .ToList();

            if (desafiosRealizados == null ||
                desafiosRealizados.Count == 0)
            {
                return new EstudianteCursoViewModel(model.Curso,
                    desafiosRealizados,
                    desafiosNoCompletados,
                    model.Curso.Desafio);
            }
            else
            {
                var desafio = await Get_SiguienteDesafio(idCurso,
                    idEstudiante);

                return new EstudianteCursoViewModel(model.Curso,
                    desafiosRealizados, desafiosNoCompletados, desafio);
            }
        }

        /// <summary>
        /// Método que retorna el siguiente desafío para un
        /// estudiante, dentro de un curso
        /// </summary>
        /// <param name="idCurso">identificador del curso</param>
        /// <param name="idEstudiante">identificador del estudiante</param>
        /// <returns></returns>
        public async Task<Desafio> Get_SiguienteDesafio(int idCurso,
            int idEstudiante)
        {
            var relEstCurso = await _data.Find_Rel_CursoEstudiantes(idCurso, idEstudiante);

            if (relEstCurso == null)
                throw new ApiNotFoundException();

            var siguienteDesafio = await _data.FindPure_Desafio(relEstCurso.SiguienteDesafioId);
            if (siguienteDesafio == null)
                siguienteDesafio = (await _data.Find_Curso(idCurso)).Desafio;

            return siguienteDesafio;
        }

        public async Task<ApiResult<List<Tuple<int, string>>>> Get_CourseStudents(int idCurso, int idEstudiante)
        {
            if (!await _data.Exist_Estudiante_Curso(idEstudiante, idCurso))
                throw new ApiNotFoundException("Recurso no encontrado");

            var estudiantes = (await _data.Find_Curso(idCurso))
                .Estudiantes.Where(rel => rel.EstudianteId != idEstudiante)
                .Select(rel => new Tuple<int,string>(rel.EstudianteId, rel.Estudiante.NombreCompleto))
                .ToList();
            return ApiResult<List<Tuple<int,string>>>.Initialize(estudiantes, true);
        }
    }
}
