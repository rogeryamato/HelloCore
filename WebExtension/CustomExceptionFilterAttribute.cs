using HelloCore.Common;
using HelloCore.DomainModel.Exceptions;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebExtension
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private ILog logger = LogManager.GetLogger(CommonConstant.LoggerRepository.Name, typeof(CustomExceptionFilterAttribute));

        public override void OnException(ExceptionContext context)
        {
            var ex = context.Exception;
            JsonResult jsonResult = null;
            if(ex is BusinessException)
            {
                jsonResult = new JsonResult(ex.Message)
                {
                    StatusCode = ex.HResult
                };
            }
            else
            {
                jsonResult = new JsonResult("Service error")
                {
                    StatusCode = 500
                };
            }
            logger.Error("Service error", ex);
            context.Result = jsonResult;
        }
    }
}
