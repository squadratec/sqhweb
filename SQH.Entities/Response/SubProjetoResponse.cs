using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQH.Entities.Response
{
    public class SubProjetoResponse
    {
        public SubProjetoResponse()
        {
            this.subprojetos = new List<SelectListItem>();
        }
        public List<SelectListItem> subprojetos { get; set; }
        public string msg { get; set; }
    }
}
