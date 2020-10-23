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
        public string Ano { get; set; }

        public string moeda { get; set; }

        private string hold1 = "https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata/CotacaoMoedaDia(moeda=@moeda,dataCotacao=@dataCotacao)?@moeda=%27";
        private string hold2 = "%27&@dataCotacao=%27";
        private string hold3 = "%27&$top=100&$format=json&$select=cotacaoCompra,cotacaoVenda,dataHoraCotacao";

        private string Url{
            get
            {
                string url = hold1 + moeda + hold2 + Mes + "-" + Dia + "-" + Ano + hold3;
                return url;
            }
        }

        public string ObterUrl()
        {
            return Url;
        }

        public void VerificaFimSemana(DateTime temp)
        {
            if (temp.DayOfWeek.ToString() == "Sunday")
            {
                temp = temp.AddDays(1);
                Dia = Convert.ToString(temp.Day, 10);
                Mes = Convert.ToString(temp.Month, 10);
                Ano = Convert.ToString(temp.Year, 10);

            }

            if (temp.DayOfWeek.ToString() == "Saturday")
            {
                temp = temp.AddDays(2);
                Dia = Convert.ToString(temp.Day, 10);
                Mes = Convert.ToString(temp.Month, 10);
                Ano = Convert.ToString(temp.Year, 10);

            }


        }



    }
}
