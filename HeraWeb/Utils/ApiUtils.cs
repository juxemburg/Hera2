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
        /// Función genérica para la inserción de un recurso
        /// </summary>
        /// <typeparam name="T">Tipo de dato del modelo</typeparam>
        /// <param name="model">Variable del modelo a ser insertada</param>
        /// <param name="modelstate">Estado del modelo a ser validado</param>
        /// <param name="fnInsert">Función de inserción del modelo</param>
        public static async Task<IActionResult> InsertModel<T>(this Controller controller,  T model,
            ModelStateDictionary modelState, Func<Task<bool>> fnInsert)
        {
            if(modelState.IsValid)
            {
                var result = await fnInsert();
                return controller.Ok();
            }
            else
            {
                return controller.BadRequest(modelState);
            }
        }   
    }
}
