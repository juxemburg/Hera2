﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HeraServices.ViewModels.ApiViewModels
{
    public class ApiResult<T>
    {
        public T Value { get; set; }
        public bool Success { get; set; }
        public Dictionary<string, string> ModelErrors { get; set; }

        public void AddError(string modelKey, string error)
        {
            ModelErrors.Add(modelKey, error);
        }

        public static ApiResult<T> Initialize()
        {
            return new ApiResult<T>()
            {
                ModelErrors = new Dictionary<string, string>(),
                Success = false
            };

        }

        public static ApiResult<T> Initialize(T val, bool success = false)
        {
            return new ApiResult<T>()
            {
                ModelErrors = new Dictionary<string, string>(),
                Success = success,
                Value = val
            };

         }
    }
}
