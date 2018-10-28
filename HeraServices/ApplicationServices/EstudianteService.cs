using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Calificaciones;
using Entities.Cursos;
using HeraServices.Services.DesafiosServices;
using HeraScratch.Exceptions;
using Microsoft.EntityFrameworkCore;
using HeraServices.ViewModels.EntitiesViewModels.EstudianteCurso;
using HeraServices.ViewModels.EntitiesViewModels.EstudianteDesafio;
using HeraDAL.DataAcess;
using HeraServices.ViewModels.UtilityViewModels;
using HeraServices.ViewModels.EntitiesViewModels;
using HeraServices.ViewModels.ApiViewModels;
using HeraServices.ViewModels.EntitiesViewModels.Cursos;
using HeraServices.ViewModels.ApiViewModels.Exceptions;
using Entities.Valoracion;
using HeraServices.ViewModels.EntityMapping;
using HeraServices.ViewModels.EntitiesViewModels.Desafios;

namespace HeraServices.Services.ApplicationServices
{
    public class EstudianteService
    {
        private readonly IDataAccess _data;
        private readonly DesafioEstudianteService _desafioService;
        private readonly ScratchService _evaluator;

        public EstudianteService(IDataAccess data, ScratchService evaluator,
            DesafioEstudianteService desafioService)
        {
            _data = data;
            _desafioService = desafioService;
            _evaluator = evaluator;
        }

        public async Task<PaginationViewModel<Curso>> Search_Curso(int estId,
            string searchString, int skip, int take)
        {
            var model = await _data
                .Search_CursosEstudiante(estId, searchString)
                .ToListAsync();
            return new PaginationViewModel<Curso>(model, skip, take);
        }

        public async Task<ApiResult<PaginationViewModel<CursoListViewModel>>> GetAll_Curso(
            int estId, string searchString, int skip, int take, bool inverse = false)
        {
            var model = await _data.GetAll_CursosEstudiante(estId,
                searchString, inverse).Select(item => item.MapToViewModel()).ToListAsync();
            var res = ApiResult<PaginationViewModel<CursoListViewModel>>
                .Initialize(new PaginationViewModel<CursoListViewModel>(model, skip, take), true);
            return res;
        }

        public async Task<ApiResult<bool>> Do_MatricularEstudiante(int estId,
            AddEstudianteViewModel model)
        {
            var result = ApiResult<bool>.Initialize(true);
            try
            {
                var curso = await _data.Find_Curso(model.CursoId);
                var estudiante = await _data.Find_Estudiante(estId);
                var dataModel = model.Map(curso.Id, estudiante.Id);
                _data.Do_MatricularEstudiante(curso, estudiante,
                    ref dataModel, model.Password);
                result.Success = await _data.SaveAllAsync();

                if (!result.Success)
                    result.AddError("password", "Lo sentimos, la contraseña es inválida.");

                return result;
            }
            catch (Exception e)
            {
                //TODO: Log exception.
                result.AddError("", "El estudiante ya se ha matriculado en el curso.");
                result.Success = false;
                return result;
            }
        }

        public async Task<ApiResult<EstudianteCursoViewModel>> Get_Curso(int estId,
            int cursoId)
        {
            if (!await _data.Exist_Estudiante_Curso(estId, cursoId))
                throw new ApiNotFoundException("El estudiante no está matriculado en el curso");

            var model = await _desafioService
                .Get_RelEstudianteCurso(cursoId, estId);
            return ApiResult<EstudianteCursoViewModel>.Initialize(model, true);
        }

        public async Task<ApiResult<CalificacionDesafioViewModel>> Get_Desafio(
            int estId, int cursoId, int desafioId)
        {
            var curso = await _data.Find_Curso(cursoId);
            if (curso == null || !curso.ContieneEstudiante(estId)
                || !curso.ContieneDesafio(desafioId))
                throw new ApiNotFoundException("El desafío al que intentas acceder no existe.");

            var model = await _data.Find_RegistroCalificacion(
                cursoId, estId, desafioId);

            var calificacionesColaborador = new List<Calificacion>();

            int? nextChallenge = null;
            if (model == null)
            {
                model = new RegistroCalificacion()
                {
                    CursoId = cursoId,
                    DesafioId = desafioId,
                    EstudianteId = estId,
                    Calificaciones = new List<Calificacion>()
                    {
                        new Calificacion()
                        {
                            Resultados = new List<ResultadoScratch>(),
                            CursoId = cursoId,
                            DesafioId = desafioId,
                            EstudianteId = estId,
                            DirArchivo = ""
                        }
                    }
                };
                _data.Add_RegistroCalificacion(model);
                await _data.SaveAllAsync();
            }
            else
            {
                var regEstudiante = await _data.Find_Rel_CursoEstudiantes(cursoId, estId);
                calificacionesColaborador = regEstudiante.Colaboraciones.Select(colab => colab.Calificacion)
                    .Where(cal => cal.DesafioId == desafioId)
                    .ToList();
                nextChallenge = await GetNextChallenge(cursoId, estId, desafioId);
            }
            
            return ApiResult<CalificacionDesafioViewModel>.Initialize(
                new CalificacionDesafioViewModel(model, calificacionesColaborador ,nextChallenge), true);
        }

        public async Task<ApiResult<CalificacionInfoViewModel>> Do_IniciarDesafio(int estId, int idCurso, int idDesafio, int idCalificacion, List<int> contributors)
        {
            var model = await _data.Find_Calificacion(idCalificacion, estId, idCurso, idDesafio);
            if (model == null)
                throw new ApiNotFoundException();
            if (model.Tiempoinicio != null)
                throw new ApiBadRequestException("El desafío ya ha comenzado");

            model.Tiempoinicio = DateTime.Now;
            _data.Edit(model);
            await AddContributorsAsync(estId, idCurso, idDesafio, model.Id, contributors);

            var success = await _data.SaveAllAsync();
            return ApiResult<CalificacionInfoViewModel>.Initialize(model.ToViewModel(), success);
        }

        public async Task<ApiResult<CalificacionInfoViewModel>> Do_CalificarDesafio(int estId,
            int idCurso, int idDesafio, int idCalificacion, string idProj)
        {
            var model = await _data.Find_Calificacion(idCalificacion, estId, idCurso, idDesafio);

            try
            {
                if (model == null)
                    throw new ApiNotFoundException();
                if (model.Tiempoinicio == null)
                    throw new ApiBadRequestException("Debes iniciar el desafío primero");
                if (model.TiempoFinal != null)
                    throw new ApiBadRequestException("El desafío ya ha terminado");


                var res = await _evaluator.Get_Evaluation(idProj);
                var est = await _data.Find_Estudiante(estId);
                var curso = await _data.Find_Curso(idCurso);
                var resultados = res.Select(val => val.Map(model.Id))
                    .ToList();

                _data.Do_TerminarCalificacion(curso,
                    est, model, resultados, idProj);
                var relEstudianteCurso = await _data.Find_Rel_CursoEstudiantes(idCurso, estId);
                relEstudianteCurso.SiguienteDesafioId = await GetNextChallenge(idCurso, estId, idDesafio);
                _data.Edit(relEstudianteCurso);
                var success = await _data.SaveAllAsync();
                return ApiResult<CalificacionInfoViewModel>.Initialize(model.ToViewModel(), success);
            }
            catch (EvaluationException)
            {
                throw new ApiBadRequestException("Id de desafío inválido");
            }
        }

        public async Task<DesafioProgresoViewModel> Get_DesafioProgreso(
            int estId, int cursoId, int desafioId)
        {
            var registro = await _data.Find_RegistroCalificacion(cursoId,
                    estId, desafioId);
            if (registro == null)
                throw new ApplicationServicesException("");

            var desafio = await _data.Find_Desafio(desafioId);
            var curso = await _data.Find_Curso(cursoId);

            return new DesafioProgresoViewModel
            {
                Calificaciones = registro.Calificaciones
                    .Where(c => !c.EnCurso)
                    .ToList(),
                CursoId = cursoId,
                NombreCurso = curso.Nombre,
                NombreDesafio = desafio.Nombre
            };
        }



        public async Task<ApiResult<DesafioCompletadoViewModel>>
            Get_DesafioCompletadoViewModel(int idEst, int idCurso,
                int idDesafio, int idCalificacion)
        {
            if (!await _data.Exist_Estudiante_Curso(idEst, idCurso))
                throw new ApiNotFoundException();

            var desafio = await _desafioService
                .Get_SiguienteDesafio(idCurso, idEst);

            var resultado = await _data
                .Find_ResultadoScratchGeneral(idCalificacion);

            if(resultado == null)
                throw new ApiNotFoundException();
            

            return ApiResult<DesafioCompletadoViewModel>
                .Initialize(new DesafioCompletadoViewModel(idCurso, resultado, desafio), true);
        }

        public async Task<ApiResult<CalificacionInfoViewModel>> Do_AddCalificacion
            (int idEst, int idCurso,
            int idDesafio)
        {
            var registroCalificacion = await _data.Find_RegistroCalificacion(idCurso, idEst, idDesafio);

            if (registroCalificacion == null)
                throw new ApiNotFoundException("Recurso no encontrado");
            if (registroCalificacion.Iniciada)
                throw new ApiBadRequestException("Debes terminar la calificación actual para poder registrar una nueva.");

            var model = new Calificacion()
            {
                Tiempoinicio = null,
                CursoId = idCurso,
                EstudianteId = idEst,
                DesafioId = idDesafio
            };
            _data.Add(model);

            var success = await _data.SaveAllAsync();
            return ApiResult<CalificacionInfoViewModel>.Initialize(model.ToViewModel(), success);
        }

        private async Task<int> GetNextChallenge(int idCurso, int idEstudiante, int desafioId)
        {
            var relEstCurso = await _data.Find_Rel_CursoEstudiantes(idCurso, idEstudiante);

            if (relEstCurso == null)
                throw new ApiNotFoundException("Recurso no encontrado");

            if(!await _data.Exist_Desafio(desafioId, idCurso))
                throw new ApiNotFoundException("Recurso no encontrado");

            var curso = await _data.Find_Curso(idCurso);
            

            if (!await _data.Exist_Desafio(desafioId, idCurso))
                throw new ApiNotFoundException("Recurso no encontrado");

            var relDesafioCurso = await _data.Find_Rel_DesafiosCursos(desafioId, idCurso);
            var siguientesDesafios = _data.GetAll_Rel_DesafiosCursos(idCurso, relDesafioCurso.Orden).ToList();

            return siguientesDesafios.Count > 0 ? siguientesDesafios.First().DesafioId : curso.Desafio.Id;
        }

        private async Task AddContributorsAsync(int idEst, int idCurso,
            int idDesafio, int idCalificacion, List<int> contributors)
        {
            if (contributors == null)
                contributors = new List<int>();

            if (contributors.Any(item => item.Equals(idEst)))
                throw new ApiBadRequestException("El estudiante y el contribuidor deben ser diferentes");

            if (contributors.GroupBy(item => item).Any(grp => grp.Count() > 1))
                throw new ApiBadRequestException("No puedes repetir contribuidores");

            foreach (var estId in contributors)
            {
                if (!await _data.Exist_Estudiante_Curso(estId, idCurso))
                    throw new ApiBadRequestException("El estudiante no existe en el curso");

                _data.Add(new RegistrosColaborador()
                {
                    CalificacionId = idCalificacion,
                    CursoId = idCurso,
                    EstudianteId = estId
                });
            }
        }
    }
}

