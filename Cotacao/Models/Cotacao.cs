using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cotacao.Models
{
    public class Cotacao
    {
        [Key]
        public int CotacaoId { get; set; }

        public string MoedaOrigem { get; set; }

        public string MoedaDestino { get; set; }

        public string ValorCompra { get; set; }

        public string ValorVenda { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Data { get; set; }

        public string RetornarData()
        {
            return String.Format("{0}/{1}/{2}", Data.Day, Data.Month, Data.Year);
        }
        

    }
}
