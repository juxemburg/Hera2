using System;
using System.Collections.Generic;
using System.Text;

namespace HeraServices.ViewModels.ApiViewModels.Exceptions
{
    public class ApiBadRequestException : Exception
    {
        public ApiBadRequestException() { }

        public ApiBadRequestException(string message) : base(message) { }

        public ApiBadRequestException(string message, Exception innerException) : base(message, innerException) { }
    }
}
