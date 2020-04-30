using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SquaresGame.Data;
using SquaresGame.Models;

namespace SquaresGame.Controllers
{
    public class SquareCardController : Controller
    {
        private readonly SquaresGameContext _context;

        public SquareCardController(SquaresGameContext context)
        {
            _context = context;
        }

        // GET: SquareCard
        public async Task<IActionResult> Index()
        {
            return View(await _context.SquareCard.ToListAsync());
        }

        // GET: SquareCard/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var squareCard = await _context.SquareCard
                .FirstOrDefaultAsync(m => m.Id == id);
            if (squareCard == null)
            {
                return NotFound();
            }

            return View(squareCard);
        }

        // GET: SquareCard/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SquareCard/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GameId,SquareId,xVall,yVal")] SquareCard squareCard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(squareCard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(squareCard);
        }

        // GET: SquareCard/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var squareCard = await _context.SquareCard.FindAsync(id);
            if (squareCard == null)
            {
                return NotFound();
            }
            return View(squareCard);
        }

        // POST: SquareCard/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GameId,SquareId,xVall,yVal")] SquareCard squareCard)
        {
            if (id != squareCard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(squareCard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SquareCardExists(squareCard.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(squareCard);
        }

        // GET: SquareCard/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var squareCard = await _context.SquareCard
                .FirstOrDefaultAsync(m => m.Id == id);
            if (squareCard == null)
            {
                return NotFound();
            }

            return View(squareCard);
        }

        // POST: SquareCard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var squareCard = await _context.SquareCard.FindAsync(id);
            _context.SquareCard.Remove(squareCard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SquareCardExists(int id)
        {
            return _context.SquareCard.Any(e => e.Id == id);
        }
    }
}
