using System;
using System.Collections.Generic;
using System.Text;

namespace HeraServices.ViewModels.ApiViewModels.Exceptions
{
    public class ApiNotFoundException : Exception
    {
        public ApiNotFoundException() { }

        public ApiNotFoundException(string message) : base(message) { }

        public ApiNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
