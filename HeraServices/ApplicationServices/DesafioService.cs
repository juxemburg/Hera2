using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeraDAL.DataAcess;
using HeraServices.Services.ScratchServices;
using HeraServices.ViewModels.ApiViewModels;
using HeraServices.ViewModels.EntitiesViewModels;
using HeraServices.ViewModels.EntitiesViewModels.Desafios;
using HeraServices.ViewModels.EntitiesViewModels.Ratings;
using HeraServices.ViewModels.ServicesViewModels.Valoration;
using HeraServices.ViewModels.UtilityViewModels;
using Microsoft.EntityFrameworkCore;

namespace HeraServices.Services.ApplicationServices
{
    public class DesafioService
    {
        private readonly IDataAccess _data;
        private readonly ScratchService _scratchService;

        public DesafioService(IDataAccess data, ScratchService scratchService)
        {
            _data = data;
            _scratchService = scratchService;
        }

        public async Task<ApiResult<List<AutocompleteResultDesafioViewModel>>>
            AutocompleteDesafios(string searchString)
        {
            var resultList = await _data.Autocomplete_Desafios(searchString)
                .Select(r => new AutocompleteResultDesafioViewModel
                {
                    Id = r.Id,
                    Nombre = r.Nombre,
                    Descripcion = r.Descripcion,
                    Autor = r.Profesor.NombreCompleto
                }).ToListAsync();
            return ApiResult<List<AutocompleteResultDesafioViewModel>>
                .Initialize(resultList, true);
        }

        public async Task<ApiResult<PaginationViewModel<DesafioDetailsViewModel>>>
            GetAll_Desafios(int profId, SearchDesafioViewModel searchModel,
                int skip, int take)
        {
            var searchString = searchModel.SearchString;
            var model = _data.GetAll_Desafios(null, null, searchString,
                    searchModel.Map(), searchModel.EqualSearchModel,
                    searchModel.MinValoration)
                .AsNoTracking();

            if (profId > 0)
            {
                model = model.Where(d => d.ProfesorId != profId);
            }

            var list = await model.Select(m =>
                    new DesafioDetailsViewModel(m))
                .ToListAsync();

            return ApiResult<PaginationViewModel<DesafioDetailsViewModel>>
                .Initialize(new PaginationViewModel<DesafioDetailsViewModel>(list, skip, take), true);
        }

        public async Task<ApiResult<GeneralValorationViewModel>> GetValoration(string projectId)
        {
            var res = await _scratchService.Get_GeneralEvaluation(projectId);
            var model = new GeneralValorationViewModel((GeneralInfo)res.AdditionalInfo);
            return ApiResult<GeneralValorationViewModel>.Initialize(model, true);
        }

        public async Task<DesafioDetailsViewModel> Get_Desafio(int id)
        {
            var model = await _data.Find_Desafio(id);

            return (model == null)
                ? null
                : new DesafioDetailsViewModel(model);
        }

        public async Task<ApiResult<bool>> Create_Desafio(int profId,
            CreateDesafioViewModel model)
        {
            var result = ApiResult<bool>.Initialize(false);
            try
            {
                _data.AddDesafio(model.Map(profId));
                result.Value = await _data.SaveAllAsync();
                result.Success = result.Value;
                return result;

            }
            catch (Exception e)
            {
                result.AddError("", "Error en la creación de desafío");
                return result;
            }
        }

        public async Task<bool> Do_CalificarDesafio(int profId,
            int desafioId, RateViewModel model)
        {
            if (!await _data.Exist_Desafio(desafioId))
                return false;
            var desafio = await _data.Find_Desafio(desafioId);
            var profesor = await _data.Find_Profesor(desafio.ProfesorId);
            if (profId != profesor.Id)
            {
                //TODO: llamar al servicio de notificaciones
                //_data.Do_PushNotification(
                //    NotificationType.NotificationDesafioCalificado,
                //    profesor.UsuarioId, new Dictionary<string, string>()
                //    {
                //        ["IdDesafio"] = $"{desafioId}",
                //        ["NombreDesafio"] = $"{desafio.Nombre}"
                //    });
            }


            await _data.Calificar_Desafio(desafioId, profId,
                model.Rate);
            return await _data.SaveAllAsync();
        }


        public async Task<EditDesafioViewModel> Get_DesafioEdit(int profId,
            int desafioId)
        {
            if (!await _data.Exist_DesafioP(desafioId, profId))
                throw new ApplicationServicesException();

            var model = await _data.Find_Desafio(desafioId);
            return new EditDesafioViewModel(model);
        }

        public async Task<bool> Edit_Desafio(int profId,
            EditDesafioViewModel model)
        {
            if (!await _data.Exist_DesafioP(model.Id, profId))
                throw new ApplicationServicesException();

            var desafio = await _data.Find_Desafio(model.Id);
            desafio.Nombre = model.Nombre;
            desafio.Descripcion = model.Descripcion;
            desafio.DirDesafioInicial = model.UrlEscenarioInicial;

            _data.Edit(desafio);
            return await _data.SaveAllAsync();

        }

        public async Task<bool> Delete_Desafio(int profId, int desafioId)
        {
            if (!await _data.Exist_DesafioP(desafioId, profId))
                throw new ApplicationServicesException();

            await _data.Delete_Desafio(desafioId);
            return await _data.SaveAllAsync();
        }
    }
}
