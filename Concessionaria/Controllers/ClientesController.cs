using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Concessionaria.Models;
using Microsoft.Extensions.Hosting;

namespace Concessionaria.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ConcessionariaDbContext _context;

        public ClientesController(ConcessionariaDbContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var concessionariaDbContext = _context.Clientes.Include(c => c.Endereco);
            return View(await concessionariaDbContext.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(Tuple.Create(cliente, cliente.Endereco));
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            ViewData["EnderecoId"] = new SelectList(_context.Enderecos, "Id", "Id");
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente Item1, Endereco Item2)
        {
            //Item1.EnderecoId = 1;
            //if (ModelState.IsValid)
            //{
            _context.Add(Item2);
            //_context.Add(Item1);
            await _context.SaveChangesAsync();
            Item1.EnderecoId = Item2.Id;
            _context.Add(Item1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
            return View(Tuple.Create(Item1, Item2));
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            var endereco = await _context.Enderecos.FindAsync(cliente.EnderecoId);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewData["EnderecoId"] = new SelectList(_context.Enderecos, "Id", "Id", cliente.EnderecoId);
            return View(Tuple.Create(cliente, endereco));
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cliente Item1, Endereco Item2)
        {
            if (id != Item1.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(Item1);
                    _context.Update(Item2);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(Item1.Id))
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
            ViewData["EnderecoId"] = new SelectList(_context.Enderecos, "Id", "Id", Item1.EnderecoId);
            //return View(Tuple.Create(Item1, Item2));
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id, Cliente Item1, Endereco Item2)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }
            var endereco = await _context.Enderecos.FindAsync(cliente.EnderecoId);

            return View(Tuple.Create(cliente, endereco));
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, Cliente Item1, Endereco Item2)
        {
            if (_context.Clientes == null)
            {
                return Problem("Entity set 'ConcessionariaDbContext.Clientes'  is null.");
            }
            var cliente = await _context.Clientes.FindAsync(id);
            var endereco = await _context.Enderecos.FindAsync(cliente.EnderecoId);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
          return (_context.Clientes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
