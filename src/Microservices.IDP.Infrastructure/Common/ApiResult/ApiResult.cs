using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.IDP.Infrastructure.Common.ApiResult
{
    public class ApiResult<T> : IActionResult
    {

        public ApiResult()
        {
        }

        public ApiResult(bool isSucceeded, string? message = null)
        {
            Message = message;
            IsSucceeded = isSucceeded;
        }

        public ApiResult(bool isSucceeded, T result, string? message = null)
        {
            Result = result;
            Message = message;
            IsSucceeded = isSucceeded;
        }

        public bool IsSucceeded { get; set; }
        public string? Message { get; set; }
        public T Result { get; set; }


        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(this);

            await objectResult.ExecuteResultAsync(context);
        }
    }
}
