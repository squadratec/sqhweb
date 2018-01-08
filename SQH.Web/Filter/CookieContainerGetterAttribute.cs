using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using SQH.Entities.Response;
using SQH.Web.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SQH.Web.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CookieContainerGetterAttribute : ActionFilterAttribute
    {
        private CookieContainer EmpacotarBiscoitos(string serializedCookieResponseCollection)
        {
            CookieContainer container = new CookieContainer();

            List<CookieResponse> cookies = JsonConvert.DeserializeObject<List<CookieResponse>>(serializedCookieResponseCollection);

            foreach (CookieResponse cookie in cookies)
            {
                Cookie c = new Cookie();
                c.Domain = cookie.Domain;
                c.Expires = cookie.Expires;
                c.Name = cookie.Name;
                c.Path = cookie.Path;
                c.Value = cookie.Value;
                container.Add(c);
            }

            return container;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(context.HttpContext.User.Identity.IsAuthenticated)
            {
                string action = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName;
                var parameters = context.ActionArguments;

                ClaimsIdentity claimsIdentity = context.HttpContext.User.Identity as ClaimsIdentity;

                var container = claimsIdentity.FindFirst("CookieContainer").Value;
                (context.Controller as CustomMvcController).PacoteDeBiscoitos = EmpacotarBiscoitos(container);
            }
        }
    }
}
