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
    public class SquareUserController : Controller
    {
        private readonly SquaresGameContext _context;

        public SquareUserController(SquaresGameContext context)
        {
            _context = context;
        }

        // GET: SquareUser
        public async Task<IActionResult> Index()
        {
            return View(await _context.SquareUser.ToListAsync());
        }

        // GET: SquareUser/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var SquareUser = await _context.SquareUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (SquareUser == null)
            {
                return NotFound();
            }

            return View(SquareUser);
        }

        // GET: SquareUser/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SquareUser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,UserPassword")] SquareUser SquareUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(SquareUser);
                await _context.SaveChangesAsync();
                return Redirect("/Home/Index");
            }
            return View(SquareUser);
        }

        // GET: SquareUser/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var SquareUser = await _context.SquareUser.FindAsync(id);
            if (SquareUser == null)
            {
                return NotFound();
            }
            return View(SquareUser);
        }

        // POST: SquareUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GameId,xScore,yScore")] SquareUser SquareUser)
        {
            if (id != SquareUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(SquareUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SquareUserExists(SquareUser.Id))
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
            return View(SquareUser);
        }

        // GET: SquareUser/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var SquareUser = await _context.SquareUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (SquareUser == null)
            {
                return NotFound();
            }

            return View(SquareUser);
        }

        // POST: SquareUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var SquareUser = await _context.SquareUser.FindAsync(id);
            _context.SquareUser.Remove(SquareUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SquareUserExists(int id)
        {
            return _context.SquareUser.Any(e => e.Id == id);
        }
    }
}
