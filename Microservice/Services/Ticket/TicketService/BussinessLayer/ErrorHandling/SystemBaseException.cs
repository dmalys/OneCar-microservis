using System;

namespace TicketService.BusinessLayer.ErrorHandling
{
    public class SystemBaseException : Exception
    {
        public SystemErrorCode errorCode { get; set; }
        
        public SystemBaseException(string message) : base(message)
        {
            errorCode = SystemErrorCode.SystemError;
        }

        public SystemBaseException(string message, SystemErrorCode exceptionCode) : base(message)
        {
            errorCode = exceptionCode;
        }

        public SystemBaseException(string message, Exception innerException) : base(message, innerException)
        {
            errorCode = SystemErrorCode.SystemError;
        }

        public SystemBaseException(string message, SystemErrorCode exceptionCode, Exception innerException) : base(message, innerException)
        {
            errorCode = exceptionCode;
        }
    }
}
