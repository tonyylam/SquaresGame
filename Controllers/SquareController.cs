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
    public class SquareController : Controller
    {
        private readonly SquaresGameContext _context;

        public SquareController(SquaresGameContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int id)
        {
            //lookup the Game we are populating squares for
            SquareGame game = _context.SquareGame.Find(id);

            ViewData["GameId"] = id;
            ViewData["Year"] = game.Year;
            ViewData["Game"] = game.GameName;
            ViewData["Round"] = game.Round;

            //retrieve any already selected squares
            IEnumerable<Square> squares = _context.Square.Where (s => s.GameId == id);
            squares = squares.ToList();
            
            //retrieve any empty squares to determine if board is complete
            Square emptySquare = squares.FirstOrDefault(c => string.IsNullOrEmpty(c.PlayerName));
            
            //check if this board already has assigned score values
            IList<SquareCard> squareCards = await _context.SquareCard.Where(r => r.GameId == id).ToListAsync();
                    
            if (emptySquare == null && !squareCards.Any())
            {
                ViewData["Assign"] = 1;
            }
            else
            {
                ViewData["Assign"] = 0;
            }            
            
            if (squareCards.Any())
            {
                ViewData["SquareCards"] = squareCards;
            }

            return View(await _context.Square.Where(c => c.GameId == id).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Update(Square square, string playerName)
        {
            Square squareUpdate = _context.Square
                             .Where(s => s.Id == square.Id)
                             .FirstOrDefault();

            if (squareUpdate == null)
            {
                return NotFound();
            }
                
            try
            {
                squareUpdate.PlayerName = TempData["username"].ToString();
                _context.Update(squareUpdate);
                await _context.SaveChangesAsync();
                string url = string.Concat("/Square/Index/", squareUpdate.GameId);
                return Redirect(url);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SquareExists(square.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }                
        }

        private bool SquareExists(int id)
        {
            return _context.Square.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Assign(int gameId)
        {            
            for (int i = 1; i <= 10; i++)
            {
                int x = GenerateRandomXScore(gameId);
                int y = GenerateRandomYScore(gameId);   
                var squareCard = new SquareCard();
            
                squareCard.GameId = gameId;
                squareCard.SquareId = i;
                squareCard.xVall = x;
                squareCard.yVal = y;

                _context.Add(squareCard);
                await _context.SaveChangesAsync();            
            }            

            string url = string.Concat("/Square/Index/", gameId);
            return Redirect(url);
        }

        private int GenerateRandomXScore(int gameId)
        {
            Random random = new Random();
            int score = 0;

            do
            {
                score = random.Next(0,10);
                SquareCard card = _context.SquareCard
                                .Where(s => s.GameId == gameId && s.xVall == score)
                                .FirstOrDefault<SquareCard>();
                
                if (card == null)
                {
                    return score;
                }
            }while(true);            
        }

        private int GenerateRandomYScore(int gameId)
        {
            Random random = new Random();
            int score = 0;

            do
            {
                score = random.Next(0,10);
                SquareCard card = _context.SquareCard
                                .Where(s => s.GameId == gameId && s.yVal == score)
                                .FirstOrDefault<SquareCard>();
                
                if (card == null)
                {
                    return score;
                }
            }while(true);            
        }
    }
}
