using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace pumox.Controllers
{
    public class AuthorizedController : ControllerBase
    {
        protected T Using<T>() where T : class
        {
            var services = this.HttpContext.RequestServices;
            return (T)services.GetService(typeof(T));
        }
    }

}
