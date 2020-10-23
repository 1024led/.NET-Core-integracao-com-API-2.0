using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cotacao.Models.Services
{
    public class SeletorMoedas
    {
        public Dictionary<string, string> DicMoedas { 
            get{

                Dictionary<string, string> dic = new Dictionary<string, string>()
             {
                {"USD","Dólar Americano"},
                {"AUD","Dólar Australiano"},
                {"CAD", "Dólar Canadense"},
                {"CHF", "Franco suíço"},
                {"DKK", "Coroa Dinamarquesa"},
                {"EUR","Euro"},
                {"GBP","Libra Esterlina"},
                {"JPY","Iene"},
                {"NOK","Coroa Norueguesa"},
                {"SEK","Coroa Sueca"}
              };
                return dic;
            } 
        }
    }
}
