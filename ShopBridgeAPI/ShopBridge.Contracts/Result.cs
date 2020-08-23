using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridge.Contracts
{
    public class Result<T>
    {
        public string SuccessMessage { get; set; }

        public bool isSuccess { get; set; }
        public T ResultObject { get; set; }
        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }
        public string ErrorDetail { get; set; }
        public Result<T> GetResult(T resultObject)
        {
            if (resultObject == null)
            {
                //  ErrorCode = ErrorCodes.ResourceNotFound;
                // ErrorMessage = string.Format(Contracts.Utility.Constants.RecordNotFound, "Record");

            }
            else
            {
                isSuccess = true;
                ResultObject = resultObject;
            }
            return this;
        }
    }
    public class ErrorModel
    {
        public bool isSuccess { get; set; }
        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }
        public string ErrorDetail { get; set; }

    }
}
