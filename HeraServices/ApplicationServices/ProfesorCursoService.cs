using HeraDAL.DataAcess;
using HeraServices.Services.UserServices;
using HeraServices.ViewModels.ApiViewModels;
using HeraServices.ViewModels.ApiViewModels.Exceptions;
using HeraServices.ViewModels.EntitiesViewModels.Evaluacion;
using HeraServices.ViewModels.EntitiesViewModels.ProfesorCursos;
using HeraServices.ViewModels.EntitiesViewModels.ProfesorEstudiante;
using HeraServices.ViewModels.EntityMapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraServices.ApplicationServices
{
    public class ProfesorCursoService
    {
        private readonly IDataAccess _data;
        private readonly UserService _usrService;

        public ProfesorCursoService(IDataAccess data, UserService usrService)
        {
            _usrService = usrService;
            _data = data;
        }

        public async Task<ApiResult<ProfesorCursoViewModel>> Get_Curso(int profId,
            int cursoId)
        {
            var model = await _data.Find_Curso(cursoId);
            if (model == null || model.ProfesorId != profId)
                throw new ApiNotFoundException("Curso no encontrado");

            var registros = await _data.GetAll_RegistroCalificacion(cursoId).ToListAsync();
            return ApiResult<ProfesorCursoViewModel>.Initialize(new ProfesorCursoViewModel(model, registros.Where(item => item.Terminada).ToList()), true);
        }

        public async Task<ApiResult<List<RegistroCalificacionViewModel>>> Get_RegistrosEstudiante(int profId,
            int cursoId, int estId)
        {
            if (!(await _data.Exist_Profesor_Curso(profId, cursoId)))
            {
                throw new ApiNotFoundException("Curso no encontrado");
            }

            var registros = await _data.GetAll_RegistroCalificacion(cursoId, estId).ToListAsync();

            return ApiResult<List<RegistroCalificacionViewModel>>.Initialize(registros.Select(item => item.ToViewModel()).ToList(), true);
        }

        public async Task<ApiResult<CreateCalificacionCualitativaViewModel>> Do_Calificar(int idProf, int idCurso,
            int idEstudiante, int idDesafio, CreateCalificacionCualitativaViewModel model)
        {

            if (!await _data.Exist_Desafio(idDesafio, idCurso, idProf))
            {
                throw new ApiNotFoundException("Recurso no encontrado");
            }

            var estUserId = (await _usrService
                .Get_EstudianteUserId(idEstudiante)).GetValueOrDefault();
            var desafio = await _data.Find_Desafio(idDesafio);
            var curso = await _data.Find_Curso(idCurso);

            _data.Add(model.Map());
            //Mover
            //_data.Do_PushNotification(
            //    NotificationType.NotificationDesafioCalificado, estUserId,
            //    new Dictionary<string, string>
            //    {
            //        ["IdCurso"] = $"{idCurso}",
            //        ["IdDesafio"] = $"{idDesafio}",
            //        ["NombreDesafio"] = desafio.Nombre,
            //        ["NombreCurso"] = curso.Nombre
            //    });
            var success = await _data.SaveAllAsync();
            return ApiResult<CreateCalificacionCualitativaViewModel>.Initialize(model, success);
        }




    }
}
