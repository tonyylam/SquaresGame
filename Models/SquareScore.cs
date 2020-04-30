using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SquaresGame.Models
{
    public class SquareScore
    {
        public int Id {get;set;}
        public int GameId {get;set;}
        [Display(Name = "X Score")]
        public int xScore {get;set;}
        [Display(Name = "Y Score")]
        public int yScore {get;set;}
    }
}