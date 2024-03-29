﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;


namespace Core.Extension
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
