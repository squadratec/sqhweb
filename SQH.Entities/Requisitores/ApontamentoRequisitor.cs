namespace SQH.Entities.Requisitores
{
    public class ApontamentoRequisitor
    {
        public ApontamentoRequisitor()
        {
            this.PRJ_DESCRICAO = string.Empty;
            this.PRJ_ID = string.Empty;
            this.SUB_DESCRICAO = string.Empty;
            this.VG_ID = string.Empty;
            this.TPITEM_DESCRICAO = string.Empty;
            this.TSOL_ID = string.Empty;
            this.TSOL_DESC = string.Empty;
            this.SOL_OBS = string.Empty;
            this.SOL_DESC_ATV = string.Empty;
            this.DISC_DESCRICAO = string.Empty;
            this.DISC_ID = string.Empty;
            this.TAAP_ID = string.Empty;
            this.TAAP_DESCRICAO = string.Empty;
            this.ARRAY_SOLICITACAO = string.Empty;
        }
        public string PRJ_DESCRICAO { get; set; }
        public string PRJ_ID { get; set; }
        public string SUB_DESCRICAO { get; set; }
        public string VG_ID { get; set; }
        public string TPITEM_DESCRICAO { get; set; }
        public string TSOL_ID { get; set; }
        public string TSOL_DESC { get; set; }
        public string SOL_OBS { get; set; }
        public string SOL_DESC_ATV { get; set; }
        public string DISC_DESCRICAO { get; set; }
        public string DISC_ID { get; set; }
        public string TAAP_ID { get; set; }
        public string TAAP_DESCRICAO { get; set; }
        public string ARRAY_SOLICITACAO { get; set; }
    }
}
