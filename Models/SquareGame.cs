using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SquaresGame.Models
{
    public class SquareGame
    {
        public int Id {get;set;}
        [Display(Name = "Name of Game")]
        public string GameName {get;set;}
        [Display(Name = "Game Category")]
        public string GameGenre {get;set;}
        public int Year {get;set;}
        public int Round {get;set;}
        public decimal Payout {get;set;}
    }
}