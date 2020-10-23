using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Cotacao.Models.Services
{
    public class RequisicaoService
    {
        public string Dia { get; set; }
        public string Mes { get; set; }
        public string ano { get; set; }

        public string moeda { get; set; }

        private string hold1 = "https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata/CotacaoMoedaDia(moeda=@moeda,dataCotacao=@dataCotacao)?@moeda=%27";
        private string hold2 = "%27&@dataCotacao=%27";
        private string hold3 = "%27&$top=100&$format=json&$select=cotacaoCompra,cotacaoVenda,dataHoraCotacao";

        private string Url{
            get
            {
                string url = hold1 + moeda + hold2 + Mes + "-" + Dia + "-" + ano + hold3;
                return url;
            }
        }

        public void AtualizarUrl()
        {
            string url = hold1 + moeda + hold2 + Mes + "-" + Dia + "-" + ano + hold3;
            return;
        }

        public string ObterUrl()
        {
            return Url;
        }

        public void VerificaFimSemana(Models.Cotacao cotacao)
        {
            if (cotacao.Data.DayOfWeek.ToString() == "Sunday")
            {
                int calc = cotacao.Data.Day + 1;
                Dia = Convert.ToString(calc, 10);
                AtualizarUrl();
            }

            if (cotacao.Data.DayOfWeek.ToString() == "Saturday")
            {
                int calc = cotacao.Data.Day + 2;
                Dia = Convert.ToString(calc, 10);
                AtualizarUrl();
            }
        }



    }
}
