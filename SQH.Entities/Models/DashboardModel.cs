using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SQH.Entities.Response;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SQH.Entities.Models
{
    public class DashboardModel
    {
        public DashboardModel()
        {
            this.projetos = new List<SelectListItem>();
            this.motivos = new List<SelectListItem>();
            this.resposta = new List<ApontamentoResponse>();
        }

        public List<ApontamentoResponse> resposta { get; set; }

        public int idColaborador { get; set; }

        public List<SelectListItem> projetos { get; set; }
        public List<SelectListItem> motivos { get; set; }

        [Required(ErrorMessage = "* É necessário selecionar um projeto.")]
        public string idProjeto { get; set; }

        [HiddenInput]
        public string textoProjeto { get; set; }

        [Required(ErrorMessage = "* É necessário selecionar um motivo.")]
        public string idMotivo { get; set; }

        [HiddenInput]
        public string textoMotivo { get; set; }

        [Required(ErrorMessage = "* É necessário selecionar um subprojeto.")]
        public string idSubProjeto { get; set; }

        [HiddenInput]
        public string textoSubProjeto { get; set; }

        [Required(ErrorMessage = "* É necessário selecionar um papel.")]
        public string idPapel { get; set; }

        [HiddenInput]
        public string textoPapel { get; set; }

        [Required(ErrorMessage = "* A descrição é obrigatória.")]
        public string descricao { get; set; }

        [Required(ErrorMessage = "* O período de datas é obrigatório.")]
        public string periodo { get; set; }
    }
}
