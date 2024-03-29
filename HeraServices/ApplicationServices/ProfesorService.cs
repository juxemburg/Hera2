﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Calificaciones;
using Entities.Cursos;
using Entities.Desafios;
using HeraDAL.DataAcess;
using HeraServices.Services.UserServices;
using HeraServices.ViewModels.ApiViewModels;
using HeraServices.ViewModels.ApiViewModels.Exceptions;
using HeraServices.ViewModels.EntitiesViewModels.Cursos;
using HeraServices.ViewModels.EntitiesViewModels.Desafios;
using HeraServices.ViewModels.EntitiesViewModels.EstudianteDesafio;
using HeraServices.ViewModels.EntitiesViewModels.Evaluacion;
using HeraServices.ViewModels.EntitiesViewModels.ProfesorCursos;
using HeraServices.ViewModels.EntitiesViewModels.ProfesorEstudiante;
using HeraServices.ViewModels.UtilityViewModels;
using Microsoft.EntityFrameworkCore;

namespace HeraServices.Services.ApplicationServices
{
    public class ProfesorService
    {
        private readonly IDataAccess _data;
        private readonly UserService _usrService;

        public ProfesorService(IDataAccess data, UserService usrService)
        {
            _usrService = usrService;
            _data = data;
        }

        public async Task<ApiResult<PaginationViewModel<CursoListViewModel>>>
            GetAll_Cursos(int profId, string searchString, int skip,
                int take)
        {
            var model = (string.IsNullOrWhiteSpace(searchString))
                ? _data.GetAll_Cursos(profId) :
                _data.Autocomplete_Cursos(searchString, profId);

            return ApiResult<PaginationViewModel<CursoListViewModel>>.Initialize(
                new PaginationViewModel<CursoListViewModel>(
                    await model.Select(item => item.MapToViewModel()).ToListAsync(), skip, take), true);
        }

        public async Task<ApiResult<List<Curso>>>
            GetAll_CursosList(int profId, string searchString)
        {
            var model = (string.IsNullOrWhiteSpace(searchString))
                ? _data.GetAll_Cursos(profId) :
                _data.Autocomplete_Cursos(searchString, profId);

            var list = await model.ToListAsync();
            return ApiResult<List<Curso>>.Initialize(list, true);
        }

        public async Task<ApiResult<PaginationViewModel<Curso>>>
            GetAll_CursosI(int profId, string searchString, int skip,
                int take)
        {
            var model = (string.IsNullOrWhiteSpace(searchString))
                ? _data.GetAll_Cursos(profId, false) :
                _data.Autocomplete_CursosI(searchString, profId);
            return ApiResult<PaginationViewModel<Curso>>.Initialize(
                new PaginationViewModel<Curso>(await model.ToListAsync(), skip, take), true);
        }


        public async Task<PaginationViewModel<DesafioDetailsViewModel>>
            GetAll_Desafios(int profId, SearchDesafioViewModel searchModel,
            int skip, int take = 10)
        {
            var model = await _data.GetAll_Desafios(null, profId,
                    searchModel.SearchString, searchModel.Map(),
                    searchModel.EqualSearchModel, searchModel.MinValoration)
                .AsNoTracking()
                .Select(m =>
                    new DesafioDetailsViewModel(m))
                .ToListAsync();
            return new PaginationViewModel<DesafioDetailsViewModel>(
                model, skip, take);
        }

        public async Task<IEnumerable<Desafio>> GetAll_Desafios(int profId,
            int cursoId)
        {
            if (await _data.Exist_Profesor_Curso(profId, cursoId))
            {
                var curso = await _data.Find_Curso(cursoId);
                return curso.Desafios
                    .Select(d => d.Desafio);
            }
            throw new ApplicationServicesException("El desafío no existe");
        }

        public async Task<IEnumerable<SelectListItemViewModel>>
            GetAll_DesafiosSelectList(int profId, int cursoId)
        {
            return (await GetAll_Desafios(profId, cursoId))
                .Select(d =>
                    new SelectListItemViewModel()
                    {
                        Value = d.Id.ToString(),
                        Text = d.Nombre
                    });
        }


        

        public async Task<DesafioCursoViewModel> Get_Desafio(int profId,
            int cursoId, int desafioId)
        {
            if (!await _data.Exist_Desafio(desafioId, cursoId, profId))
                throw new ApplicationServicesException("Desafío no encontrado");

            var desafio = await _data.Find_Desafio(desafioId);
            var curso = await _data.Find_Curso(cursoId);

            return new DesafioCursoViewModel(desafio, curso);
        }

        public async Task<EstudianteCalificacionViewModel>
            Get_Estudiante(int profId, int cursoId, int estudianteId)
        {
            var model = await _data.Find_Estudiante(estudianteId,
                cursoId, profId);
            if (model == null)
                throw new ApplicationServicesException("Estudiante no encontrado");
            return new EstudianteCalificacionViewModel(model);
        }


        public async Task<CalificacionesViewModel>
            Get_CalificacionModel(int idProf, int idCurso,
                int idEstudiante, int idDesafio)
        {
            var model = await _data.Find_RegistroCalificacion(idCurso,
                idEstudiante, idDesafio, idProf);

            var desafio = await _data.Find_Desafio(idDesafio);

            if (model == null || desafio == null)
            {
                return null;
            }
            var calificacionList = model.Calificaciones
                .Select(c =>
                    new CalificacionViewModel(c, desafio.InfoDesafio))
                    .ToList();

            var est = await _data.Find_Estudiante(model.EstudianteId);
            var curso = await _data.Find_Curso(model.CursoId);

            var resultModel = new CalificacionesViewModel(curso.Nombre,
                est.NombreCompleto, desafio.Nombre, calificacionList,
                model);

            return resultModel;

        }

        

        public async Task<ResultadosScratchViewModel> Get_EvaluacionModel(
            int idProf, int idCurso, int idEstudiante, int idDesafio,
            int idCalificacion)
        {
            if (!await Do_validationEstudiante(idProf, idCurso,
                idEstudiante))
                return null;

            var cal = await _data.Find_Calificacion(idCalificacion);
            return cal == null ? null
                : new ResultadosScratchViewModel(cal.Resultados);
        }

        public async Task<bool> Do_EditCalificar(int profId, int cursoId,
            int estudianteId, int desafioId, CalificacionCualitativa model)
        {
            if (!await Do_validationEstudiante(profId, cursoId,
                estudianteId))
                return false;

            var entity = await _data
                .Find_CalificacionCualitativa(estudianteId, cursoId,
                desafioId);
            if (entity == null)
                return false;

            entity.Completada = model.Completada;
            entity.Descripcion = model.Descripcion;

            _data.Edit(entity);

            return await _data.SaveAllAsync();
        }

        #region private methods

        private async Task<bool> Do_validationEstudiante(int idProf,
            int idCurso, int idEstudiante)
        {
            return await _data.Exist_Profesor_Curso(idProf, idCurso) &&
                   await _data.Exist_Estudiante_Curso(idEstudiante, idCurso);
        }

        #endregion

    }


}
