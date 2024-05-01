using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NadinSoft.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "required")]
        [MaxLength(30)]
        public required string Name { get; set; }

        [Display(Name = "Produce Date")]
        public DateTime ProduceDate { get; set; }

        [Display(Name = "Manufacture Phone")]
        [Required(ErrorMessage = "required")]
        [MaxLength(15)]
        public required string ManufacturePhone { get; set; }

        [Display(Name = "Manufacture Email")]
        [Required(ErrorMessage = "required")]
        [MaxLength(30)]
        public required string ManufactureEmail { get; set; }
        public bool IsAvailable { get; set; }
        public Guid UserId { get; set; }
    }
}