﻿using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Mpagopay.Application.Exceptions;

namespace Mpagopay.Api.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                var tokne = await context.GetTokenAsync("access_token");
                await _next(context);
            }
            catch(Exception ex)
            {
                await ConvertException(context, ex);
            }
        }

        private static Task ConvertException(HttpContext context, Exception ex)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            var result = string.Empty;

            switch (ex)
            {
                case ValidationException validationException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(validationException.ValidationErrors);
                    break;
                case BadRequestException badRequestException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = badRequestException.Message;
                    break;
                case NotFoundException:
                    httpStatusCode = HttpStatusCode.NotFound;
                    break;
                case Exception:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    break;

            }

            context.Response.StatusCode = (int)httpStatusCode;

            if(result == string.Empty)
            {
                result = JsonSerializer.Serialize(new ExceptionMessage { Message = ex.Message });
            }


            return context.Response.WriteAsync(result);
        }
    }
}
