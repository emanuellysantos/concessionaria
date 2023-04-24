using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Concessionaria.Models;

namespace Concessionaria.Controllers
{
    public class VendasController : Controller
    {
        private readonly ConcessionariaDbContext _context;

        public VendasController(ConcessionariaDbContext context)
        {
            _context = context;
        }

        // GET: Vendas
        public async Task<IActionResult> Index()
        {
            var concessionariaDbContext = _context.Venda.Include(v => v.Cliente).Include(v => v.Veiculo).Include(v => v.Vendedor);
            return View(await concessionariaDbContext.ToListAsync());
        }

        //public async Task<IActionResult> Index()
        //{
        //    return _context.Vendedors != null ?
        //                View(await _context.Venda.ToListAsync()) :
        //                Problem("Entity set 'ConcessionariaDbContext.Vendas'  is null.");
        //}

        // GET: Vendas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Venda == null)
            {
                return NotFound();
            }

            var venda = await _context.Venda
                .Include(v => v.Cliente)
                .Include(v => v.Veiculo)
                .Include(v => v.Vendedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (venda == null)
            {
                return NotFound();
            }

            return View(venda);
        }

        // GET: Vendas/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome");
            ViewData["VeiculoId"] = new SelectList(_context.Veiculo, "Id", "Modelo");
            ViewData["VendedorId"] = new SelectList(_context.Vendedors, "Id", "Nome");
            return View();
        }

        // POST: Vendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VeiculoId,VendedorId,ClienteId")] Venda venda)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(venda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", venda.ClienteId);
            ViewData["VeiculoId"] = new SelectList(_context.Veiculo, "Id", "Id", venda.VeiculoId);
            ViewData["VendedorId"] = new SelectList(_context.Vendedors, "Id", "Id", venda.VendedorId);
            return View(venda);
        }

        // GET: Vendas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Venda == null)
            {
                return NotFound();
            }

            var venda = await _context.Venda.FindAsync(id);
            if (venda == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome", venda.ClienteId);
            ViewData["VeiculoId"] = new SelectList(_context.Veiculo, "Id", "Modelo", venda.VeiculoId);
            ViewData["VendedorId"] = new SelectList(_context.Vendedors, "Id", "Nome", venda.VendedorId);
            return View(venda);
        }

        // POST: Vendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VeiculoId,VendedorId,ClienteId")] Venda venda)
        {
            if (id != venda.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(venda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendaExists(venda.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome", venda.ClienteId);
            ViewData["VeiculoId"] = new SelectList(_context.Veiculo, "Id", "Modelo", venda.VeiculoId);
            ViewData["VendedorId"] = new SelectList(_context.Vendedors, "Id", "Nome", venda.VendedorId);
            return View(venda);
        }

        // GET: Vendas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Venda == null)
            {
                return NotFound();
            }

            var venda = await _context.Venda
                .Include(v => v.Cliente)
                .Include(v => v.Veiculo)
                .Include(v => v.Vendedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (venda == null)
            {
                return NotFound();
            }

            return View(venda);
        }

        // POST: Vendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Venda == null)
            {
                return Problem("Entity set 'ConcessionariaDbContext.Venda'  is null.");
            }
            var venda = await _context.Venda.FindAsync(id);
            if (venda != null)
            {
                _context.Venda.Remove(venda);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Relatorios()
        {
            ViewData["RelatorioVeiculo"] = new SelectList(_context.Venda, "Id", "VeiculoId");

            var lista = _context.Venda.Include(v => v.Veiculo);

            Dictionary<int, int> frequencies = new Dictionary<int, int>();

            foreach (var item in lista)
            {
                if (frequencies.ContainsKey(item.VeiculoId))
                {
                    frequencies[item.VeiculoId]++;
                }
                else
                {
                    frequencies[item.VeiculoId] = 1;
                }
            }

            int maxFrequency = 0;
            int mostFrequentItem = new int();

            foreach (KeyValuePair<int, int> pair in frequencies)
            {
                if (pair.Value > maxFrequency)
                {
                    maxFrequency = pair.Value;
                    mostFrequentItem = pair.Key;
                }
            }

            ViewData["MostFrequentItem"] = mostFrequentItem;

            //var venda = await _context.Venda.FindAsync(mostFrequentItem);
            var veiculo = await _context.Veiculo.FindAsync(mostFrequentItem);

            return View(veiculo);
        }

        public async Task<IActionResult> RelatorioVendedor()
        {
            ViewData["RelatorioVendedor"] = new SelectList(_context.Venda, "Id", "VendedorId");

            var lista = _context.Venda.Include(v => v.Vendedor);

            Dictionary<int, int> frequencies = new Dictionary<int, int>();

            foreach (var item in lista)
            {
                if (frequencies.ContainsKey(item.VendedorId))
                {
                    frequencies[item.VendedorId]++;
                }
                else
                {
                    frequencies[item.VendedorId] = 1;
                }
            }

            int maxFrequency = 0;
            int mostFrequentItem = new int();

            foreach (KeyValuePair<int, int> pair in frequencies)
            {
                if (pair.Value > maxFrequency)
                {
                    maxFrequency = pair.Value;
                    mostFrequentItem = pair.Key;
                }
            }

            ViewData["MostFrequentItem"] = mostFrequentItem;

            var vendedor = await _context.Vendedors.FindAsync(mostFrequentItem);

            return View(vendedor);
        }

        private bool VendaExists(int id)
        {
          return (_context.Venda?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
