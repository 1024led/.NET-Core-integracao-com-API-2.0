using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cotacao.Models;
using CotacaoDatabase.Models;

namespace Cotacao.Controllers
{
    public class CotacaoController : Controller
    {
        private readonly CotacaoContext _context;

        public CotacaoController(CotacaoContext context)
        {
            _context = context;
        }

        // GET: Cotacao
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cotacao.ToListAsync());
        }

        // GET: Cotacao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cotacao = await _context.Cotacao
                .FirstOrDefaultAsync(m => m.CotacaoId == id);
            if (cotacao == null)
            {
                return NotFound();
            }

            return View(cotacao);
        }

        // GET: Cotacao/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cotacao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Data")] Cotacao.Models.Cotacao cotacao, string moeda)
        {

            Models.Services.RequisicaoService requisicao = new Models.Services.RequisicaoService();

            DateTime dataT = cotacao.Data;
            requisicao.Dia = Convert.ToString(cotacao.Data.Day, 10);
            requisicao.Mes = Convert.ToString(cotacao.Data.Month, 10);
            requisicao.Ano = Convert.ToString(cotacao.Data.Year, 10);

            requisicao.VerificaFimSemana(cotacao.Data);

            cotacao.Data = dataT;

            requisicao.moeda = moeda;

            Cotacao.Models.Services.SeletorMoedas seletor = new Cotacao.Models.Services.SeletorMoedas();

            string MoedaOrigem = seletor.DicMoedas[moeda];

            CotacaoService coletor = CotacaoService.Coletar(requisicao).Result;
     
            cotacao.MoedaOrigem = MoedaOrigem;
            cotacao.MoedaDestino = "Real";

            cotacao.ValorCompra = coletor.cotacaoCompra;
            cotacao.ValorVenda = coletor.cotacaoVenda;
           
            List<Models.Cotacao> lista = CotacaoExists(cotacao.MoedaOrigem);
            bool verify = false;

            foreach(Models.Cotacao temp in lista)
            {
                if(temp.Data.Date == cotacao.Data.Date)
                {
                    verify = true;
                }
            }

            if (!verify)
            {
                if ((ModelState.IsValid) & (cotacao.ValorCompra != null))
                {
                    _context.Add(cotacao);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View("errorData");
                    
                }
                
            }
            else
            {
                Console.WriteLine(cotacao.Data.ToString() + " " + cotacao.MoedaOrigem);
                return View("errorExist");
                
            }
        }


        // GET: Cotacao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cotacao = await _context.Cotacao
                .FirstOrDefaultAsync(m => m.CotacaoId == id);
            if (cotacao == null)
            {
                return NotFound();
            }

            return View(cotacao);
        }

        // POST: Cotacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cotacao = await _context.Cotacao.FindAsync(id);
            _context.Cotacao.Remove(cotacao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private List<Models.Cotacao> CotacaoExists(string moeda)
        {
            var existeMoeda = _context.Cotacao.Where(c => c.MoedaOrigem == moeda).Select(c => c.CotacaoId);
            var registros = _context.Cotacao.Where(c => existeMoeda.Contains(c.CotacaoId)).ToList();
            return registros;
        }

    }
}
