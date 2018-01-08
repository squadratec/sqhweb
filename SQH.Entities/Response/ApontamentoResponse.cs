using SQH.Entities.Models;
using System;

namespace SQH.Entities.Response
{
    public class ApontamentoResponse
    {
        public ApontamentoResponse() { this.apontamento = new DashboardModel(); }
        public DateTime data { get; set; }
        public bool sucesso { get; set; }
        public string mensagem { get; set; }
        public DashboardModel apontamento { get; set; }
    }
}
