using Microsoft.EntityFrameworkCore;
using SquaresGame.Models;

namespace SquaresGame.Data
{
    public class SquaresGameContext : DbContext
    {
        public SquaresGameContext (DbContextOptions<SquaresGameContext> options)
            : base(options)
            {
            }

            public DbSet<Square> Square {get;set;}
            public DbSet<SquareCard> SquareCard {get;set;}
            public DbSet<SquareGame> SquareGame {get;set;}
            public DbSet<SquareScore> SquareScore {get;set;}
            public DbSet<SquareUser> SquareUser {get;set;}
    }
}