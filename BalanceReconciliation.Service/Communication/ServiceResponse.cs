using System;
using System.Collections.Generic;
using System.Text;

namespace BalanceReconciliation.Service.Communication
{
    public enum ErrorCodes
    {
        Success = 00,
        Exception = 99,
        Error = -11

    }
    public class ServiceResponse
    {
        public bool IsSuccess  => Code == ErrorCodes.Success ? true : false;
        public ErrorCodes Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public ServiceResponse()
        {

        }
        public ServiceResponse(string message, ErrorCodes code)
        {
            Message = message;
            Code = code;
        }
    }
}