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
    public class SquareGameController : Controller
    {
        private readonly SquaresGameContext _context;

        public SquareGameController(SquaresGameContext context)
        {
            _context = context;
        }

        // GET: SquareGame
        public async Task<IActionResult> Index()
        {
            return View(await _context.SquareGame.ToListAsync());
        }

        // GET: SquareGame/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SquareGame/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GameName,GameGenre,Year,Round,Payout")] SquareGame squareGame)
        {
            if (ModelState.IsValid)
            {
                _context.Add(squareGame);
                await _context.SaveChangesAsync();
                CreateNewSquares(squareGame.Id);
                
                return RedirectToAction(nameof(Index));                
            }
            return View(squareGame);
        }

        // GET: SquareGame/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var squareGame = await _context.SquareGame.FindAsync(id);
            if (squareGame == null)
            {
                return NotFound();
            }
            return View(squareGame);
        }

        // POST: SquareGame/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GameName,GameGenre,Year,Round,Payout")] SquareGame squareGame)
        {
            if (id != squareGame.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(squareGame);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SquareGameExists(squareGame.Id))
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
            return View(squareGame);
        }

        // GET: SquareGame/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var squareGame = await _context.SquareGame
                .FirstOrDefaultAsync(m => m.Id == id);
            if (squareGame == null)
            {
                return NotFound();
            }

            return View(squareGame);
        }

        // POST: SquareGame/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var squareGame = await _context.SquareGame.FindAsync(id);
            _context.SquareGame.Remove(squareGame);
            await _context.SaveChangesAsync();
            DeleteSquareCardAndSquares(id);
            return RedirectToAction(nameof(Index));
        }

        private bool SquareGameExists(int id)
        {
            return _context.SquareGame.Any(e => e.Id == id);
        }

        private async void CreateNewSquares(int gameId)
        {
            for (int i = 1; i<=100; i++)
            {
                var square = new Square();
            
                square.GameId = gameId;
                square.SquareId = i;

                _context.Add(square);
                await _context.SaveChangesAsync();
            }          
        }

        private async void DeleteSquareCardAndSquares(int id)
        {
            IQueryable<SquareCard> squareCardQuery = from s in _context.SquareCard
                                where s.GameId == id
                                select s;
            
            _context.SquareCard.RemoveRange(await squareCardQuery.ToListAsync());

            IQueryable<Square> squareQuery = from s in _context.Square
                                where s.GameId == id
                                select s;

            _context.Square.RemoveRange(await squareQuery.ToListAsync());
            
            await _context.SaveChangesAsync();
        }

    }
}
