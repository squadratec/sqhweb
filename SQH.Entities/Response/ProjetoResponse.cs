using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQH.Entities.Response
{
    public class ProjetoResponse
    {
        public ProjetoResponse()
        {
            this.projetos = new List<SelectListItem>();
            this.motivos = new List<SelectListItem>();
        }
        public int idColaborador { get; set; }
        public List<SelectListItem> projetos { get; set; }
        public List<SelectListItem> motivos { get; set; }

        public string idProjeto { get; set; }
        public string idMotivo { get; set; }
    }
}
