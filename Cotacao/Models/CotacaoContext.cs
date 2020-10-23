using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cotacao.Models;

namespace CotacaoDatabase.Models
{
    public class CotacaoContext:DbContext
    {
        public CotacaoContext(DbContextOptions<CotacaoContext> options):base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Cotacao.Models.Cotacao> Cotacao { get; set; }
       
    }
}
