using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SquaresGame.Models;
using SquaresGame.Data;

namespace SquaresGame.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SquaresGameContext _context;
        public HomeController(ILogger<HomeController> logger, SquaresGameContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            TempData["username"] = null;            
            return View();                        
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            IQueryable<SquareUser> userQuery = from u in _context.SquareUser
                                where u.UserName.ToUpper() == username.ToUpper() && u.UserPassword == password
                                select u;

            try
            {
                SquareUser user = userQuery.SingleOrDefault();

                if (user == null)
                {
                    return RedirectToAction(nameof(Index));                    
                }
                
                TempData["username"] = user.UserName;
                return Redirect("/SquareGame/Index");
            }
            catch(Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
