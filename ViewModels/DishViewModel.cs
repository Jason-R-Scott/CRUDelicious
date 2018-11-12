using System;
using System.ComponentModel.DataAnnotations;

namespace CRUDelicious.ViewModels
{
    public class DishViewModel
    {
        [Required]
        public string DishName { get; set;}
        [Required]
        public string Chef { get; set; }
        [Required]
        [Range(1,5)]
        public int Tastiness { get; set; }
        [Required]
        [Range(1, Int16.MaxValue)]
        public int Calories { get; set; }
        public string Description { get; set; }
    }
}