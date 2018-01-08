using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQH.Entities.Response
{
    public class PapelResponse
    {
        public PapelResponse()
        {
            this.papeis = new List<SelectListItem>();
        }
        public List<SelectListItem> papeis { get; set; }
    }
}
