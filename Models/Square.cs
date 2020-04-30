using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SquaresGame.Models
{
    public class Square
    {
        public int Id {get;set;}
        public int GameId {get;set;}
        public int SquareId {get;set;}
        [Display(Name = "Player Name")]
        public string PlayerName {get;set;}
    }
}