using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace SQH.Entities.Response
{
    public class AutenticacaoResponse
    {
        public AutenticacaoResponse()
        {
            this.Autenticado = false;
            this.CookieContainer = new List<CookieResponse>();
        }
        public bool Autenticado { get; set; }
        public List<CookieResponse> CookieContainer { get; set; }
    }

    public class AuthRetorno
    {
        public bool authenticated { get; set; }

        public ClaimsIdentity Claims { get; set; }

        public string message { get; set; }
    }
}
