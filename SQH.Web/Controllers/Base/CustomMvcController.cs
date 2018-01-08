using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace SQH.Web.Controllers.Base
{
    public class CustomMvcController : Controller
    {
        public CookieContainer PacoteDeBiscoitos { get; set; }
    }
}