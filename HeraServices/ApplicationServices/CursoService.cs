using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Cursos;
using Entities.Notifications;
using HeraDAL.DataAcess;
using HeraServices.Services.UserServices;
using HeraServices.Services.UtilServices;
using HeraServices.ViewModels.ApiViewModels;
using HeraServices.ViewModels.ApiViewModels.Exceptions;
using HeraServices.ViewModels.EntitiesViewModels;
using HeraServices.ViewModels.EntitiesViewModels.Cursos;
using HeraServices.ViewModels.EntitiesViewModels.ProfesorCursos;
using Microsoft.EntityFrameworkCore;

namespace HeraServices.Services.ApplicationServices
{
    public class CursoService
    {
        private readonly IDataAccess _data;
        private readonly ColorService _clrService;
        private readonly UserService _usrService;

        public CursoService(IDataAccess data,
            ColorService clrService, UserService usrService)
        {
            _data = data;
            _clrService = clrService;
            _usrService = usrService;
        }

        public async Task<ApiResult<List<CursoListViewModel>>> Get_Cursos(string searchString = "", int skip = 0, int take = 0)
        {
            var res = await _data.GetAll_Cursos(searchString, skip, take)
                .Select(e => e.MapToViewModel())
                .ToListAsync();

            return ApiResult<List<CursoListViewModel>>.Initialize(res, true);
        }

        public async Task<Curso> Get_Curso(int id)
        {
            var model = await _data.Find_Curso(id);
            if (model == null)
                throw new ApplicationServicesException(
                    "Curso no encontrado");
            return model;
        }

        public async Task<Curso> Get_Curso(int profId, int cursoId)
        {
            if (!await Do_validateProfesor(profId, cursoId))
                throw new ApplicationServicesException();

            return await _data.Find_Curso(cursoId);
        }

        public async Task<EditCursoViewModel> Get_CursoEditViewModel(
            int profId, int cursoId)
        {
            var model = await Get_Curso(profId, cursoId);
            return new EditCursoViewModel(model);
        }

        public async Task<ApiResult<bool>> Create_Curso(int profId,
            CreateCursoViewModel model)
        {
            var result = ApiResult<bool>.Initialize(false);
            var desafio = await _data
                .Find_Desafio(model.DesafioId.GetValueOrDefault());
            if (desafio == null)
                result.AddError("", "Error en la creación del curso");

            _data.AddCurso(model.Map(profId, desafio,
                _clrService.RandomColor));
            var res = await _data.SaveAllAsync();
            result.Value = res;
            result.Success = res;
            return result;
        }

        public async Task<bool> Edit_Cruso(int profId,
            EditCursoViewModel model)
        {
            if (!await Do_validateProfesor(profId, model.Id))
                throw new ApplicationServicesException();

            var newCurso = await _data.Find_Curso(model.Id);
            newCurso.Nombre = model.Nombre;
            newCurso.Descripcion = model.Descripcion;
            newCurso.Password = model.Password;
            _data.Edit(newCurso);
            return await _data.SaveAllAsync();
        }

        public async Task<bool> Delete_Curso(int profId, int cursoId)
        {
            if (!await Do_validateProfesor(profId, cursoId))
                return false;

            await _data.Delete_Curso(cursoId);
            return await _data.SaveAllAsync();
        }

        public async Task<bool> Activate_Curso(int profId, int cursoId,
            bool value)
        {
            if (!await Do_validateProfesor(profId, cursoId))
                return false;

            var model = await _data.Find_Curso(cursoId);
            model.Activo = value;
            _data.Edit(model);
            return await _data.SaveAllAsync();

        }

        public async Task<ApiResult<CursoDesafioViewModel>> Add_DesafioCurso(int profId, int cursoId, int desafioId)
        {
            var result = ApiResult<CursoDesafioViewModel>.Initialize();

            if (!await Do_validateProfesor(profId, cursoId))
                throw new ApiUnauthorizedException("No tienes permisos para ésta solicitud");

            if (await _data.Exist_Desafio(desafioId, cursoId))
                throw new ApiBadRequestException("El desafío ya se encuentra en el curso");


            var desafio = await _data.Find_Desafio(desafioId);
            var profesor = await _data
                .Find_Profesor(desafio.ProfesorId);

            _data.AddDesafio(cursoId, desafio);

            if (profId != profesor.Id)
            {
                //TODO: llamar al notification service
                //_data.Do_PushNotification(
                //    NotificationType.NotificationDesafioUsado,
                //    profesor.UsuarioId, new Dictionary<string, string>()
                //    {
                //        ["IdDesafio"] = $"{desafio.Id}",
                //        ["NombreDesafio"] = $"{desafio.Nombre}"
                //    });
            }

            result.Success = await _data.SaveAllAsync();
            result.Value = new CursoDesafioViewModel(desafio);
            return result;
        }

        public async Task<bool> Remove_DesafioCurso(int profId,
            int desafioId, int cursoId)
        {
            try
            {
                if (!await Do_validateProfesor(profId, cursoId))
                    return false;


                await _data.Delete_Desafio(cursoId, desafioId);
                return await _data.SaveAllAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new ApplicationServicesException(
                    "Error en la eliminación de desafío", e);
            }
        }

        public async Task<bool> Edit_DesafioInicial(int profId,
            ChangeStarterViewModel model)
        {
            try
            {
                if (!await _data.Exist_Profesor_Curso(profId, model.CursoId))
                    return false;

                if (!await _data.Exist_Desafio(model.NewStarterId, model.CursoId)
                    && !await _data.Exist_Desafio(model.OldStarterId, model.CursoId))
                    throw new ApplicationServicesException(
                        "Error al cambiar el desafío");

                await _data.ChangeStarterDesafio(model.CursoId,
                    model.OldStarterId, model.NewStarterId);
                return await _data.SaveAllAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new ApplicationServicesException(
                    "Error en la eliminación de desafío", e);
            }
        }

        public async Task<bool> Remove_EstudianteCurso(int profId,
            int estudianteId, int cursoId)
        {
            if (!await Do_validateProfesor(profId, cursoId))
                return false;
            if (!await _data.Exist_Estudiante_Curso(estudianteId, cursoId))
                return false;

            var estUserId = (await _usrService
                    .Get_EstudianteUserId(estudianteId)).GetValueOrDefault();
            var curso = await _data.Find_Curso(cursoId);

            //LLamar al servicio de notificaciones
            //_data.Do_PushNotification(NotificationType.NotificationMatriculaAnulada,
            //    estUserId, new Dictionary<string, string>
            //    {
            //        ["NombreCurso"] = curso.Nombre
            //    });

            var rel = await _data
                .Find_Rel_CursoEstudiantes(cursoId, estudianteId);
            _data.Delete(rel);
            return await _data.SaveAllAsync();
        }

        private async Task<bool> Do_validateProfesor(int profId, int cursoId)
        {
            return await _data.Exist_Profesor_Curso(profId, cursoId);
        }
    }
}

