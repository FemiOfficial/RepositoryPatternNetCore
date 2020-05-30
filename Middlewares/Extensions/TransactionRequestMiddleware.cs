using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace repopractise.Middlewares.Extensions
{
    public class TransactionRequestMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IConfiguration _configuration;

        public TransactionRequestMiddleware(RequestDelegate next) 
        {
            _next = next;
        }


    }
}