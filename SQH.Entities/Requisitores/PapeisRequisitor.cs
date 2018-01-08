namespace SQH.Entities.Requisitores
{
    public class PapeisRequisitor
    {
        public PapeisRequisitor()
        {

        }

        public PapeisRequisitor(string proj, string dt, string colabora, string subproj)
        {
            this.projeto = proj;
            this.data = dt;
            this.colaborador = colabora;
            this.subprojeto = subproj;
        }
        public string projeto { get; set; }
        public string data { get; set; }
        public string colaborador { get; set; }
        public string subprojeto { get; set; }
        public string opcao { get; set; }
    }
}
