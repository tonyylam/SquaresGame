using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SquaresGame.Models
{
    public class SquareUser
    {
        public int Id {get;set;}
        [Display(Name = "User Name")]
        public string UserName {get;set;}
        [Display(Name = "User Password")]
        public string UserPassword {get;set;}
        public int IsAdmin {get;set;}
    }
}