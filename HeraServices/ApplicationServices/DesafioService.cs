﻿using System;
using System.Linq;
using System.Threading.Tasks;
using HeraDAL.DataAcess;
using HeraServices.ViewModels.EntitiesViewModels;
using HeraServices.ViewModels.EntitiesViewModels.Desafios;
using HeraServices.ViewModels.EntitiesViewModels.Ratings;
using HeraServices.ViewModels.UtilityViewModels;
using Microsoft.EntityFrameworkCore;

namespace HeraServices.Services.ApplicationServices
{
    public class DesafioService
    {
        private readonly IDataAccess _data;

        public DesafioService(IDataAccess data)
        {
            _data = data;
        }

        public async Task<PaginationViewModel<DesafioDetailsViewModel>>
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
            return new PaginationViewModel<DesafioDetailsViewModel>(
                list, skip, take);
        }

        public async Task<DesafioDetailsViewModel> Get_Desafio(int id)
        {
            var model = await _data.Find_Desafio(id);

            return (model == null)
                ? null
                : new DesafioDetailsViewModel(model);
        }

        public async Task<bool> Create_Desafio(int profId,
            CreateDesafioViewModel model)
        {
            try
            {
                _data.AddDesafio(model.Map(profId));
                return await _data.SaveAllAsync();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new ApplicationServicesException(
                    "Error en la creación de desafío");
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
