using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HeraServices.ViewModels.ApiViewModels.Exceptions
{
    public class ApiUnauthorizedException : Exception
    {
        public ApiUnauthorizedException()
        {
        }

        public ApiUnauthorizedException(string message) : base(message)
        {
        }

        public ApiUnauthorizedException(string message, Exception innerException) : base(message, innerException)
        {
        }
        
    }
}
