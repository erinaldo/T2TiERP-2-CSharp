using System;
using System.Text;
using System.Collections.Generic;


namespace Servidor.Model {
    
    public class NfeDetalheImpostoPisDTO {
        public NfeDetalheImpostoPisDTO() { }
        public int Id { get; set; }
        public int IdNfeDetalhe { get; set; }
        public string CstPis { get; set; }
        public System.Nullable<decimal> QuantidadeVendida { get; set; }
        public System.Nullable<decimal> ValorBaseCalculoPis { get; set; }
        public System.Nullable<decimal> AliquotaPisPercentual { get; set; }
        public System.Nullable<decimal> AliquotaPisReais { get; set; }
        public System.Nullable<decimal> ValorPis { get; set; }
    }
}
