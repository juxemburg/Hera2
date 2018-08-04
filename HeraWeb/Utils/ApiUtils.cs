using HeraDAL.Exceptions;
using HeraServices.ViewModels.ApiViewModels;
using HeraServices.ViewModels.ApiViewModels.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeraWeb.Utils
{
    public static class CrudController
    {
        /// <summary>
        /// Función genérica para obtener un recurso
        /// </summary>
        /// <typeparam name="T">Tipo de dato del recurso a obtener</typeparam>
        /// <param name="fnGetModel">Función que obtiene el modelo</param>
        /// <returns></returns>
        public static async Task<IActionResult> Get<T>(this Controller controller, Func<Task<ApiResult<T>>> fnGetModel)
        {
            try
            {
                var result = await fnGetModel();
                //return result.Success ? (IActionResult) controller.Ok(result.Value) : controller.NotFound();
                if (result.Success)
                    return controller.Ok(result.Value);
                else
                    return result.Value == null ? (IActionResult) controller.NotFound(): controller.BadRequest(result.ModelErrors);
            }
            catch (ApiNotFoundException e)
            {
                return controller.NotFound(e);
            }
            catch (ApiBadRequestException e)
            {
                return controller.BadRequest(e);
            }
            catch (ApiUnauthorizedException)
            {
                return controller.Unauthorized();
            }
            catch(DataAccessException e)
            {
                //Log error
                return controller.StatusCode(500, e);
            }
            catch (Exception err)
            {
                //Log error.
                return controller.StatusCode(500, err);
            }
        }

        /// <summary>
        /// Función genérica para la inserción de un recurso
        /// </summary>
        /// <typeparam name="T">Tipo de dato del modelo</typeparam>
        /// <param name="model">Variable del modelo a ser insertada</param>
        /// <param name="modelstate">Estado del modelo a ser validado</param>
        /// <param name="fnInsert">Función de inserción del modelo</param>
        public static async Task<IActionResult> Post<T, U>(this Controller controller,  T model,
            ModelStateDictionary modelState, Func<Task<ApiResult<U>>> fnInsert)
        {
            if(modelState.IsValid)
            {
                
                var result = await fnInsert();
                addModelErrors(modelState, result.ModelErrors);

                return result.Success ? (IActionResult)controller.Ok(result.Value) : controller.BadRequest(modelState);
            }
            else
            {
                return controller.BadRequest(modelState);
            }
        }
        

        private static void addModelErrors(ModelStateDictionary modelState, Dictionary<string, string> errDictionary)
        {
            if (errDictionary == null)
                return;

            foreach (var key in errDictionary.Keys)
            {
                modelState.AddModelError(key, errDictionary[key]);
            }
        }
    }
}
