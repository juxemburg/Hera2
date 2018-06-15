using System;
using System.Collections.Generic;
using System.Text;

namespace HeraServices.ViewModels.ApiViewModels
{
    public class ApiResult<T>
    {
        public T Value { get; set; }
        public Dictionary<string, string> ModelErrors { get; set; }
    }
}
