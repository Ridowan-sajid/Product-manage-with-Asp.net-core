using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace First.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        [DisplayName("Product Name")]
        public string Name { get; set; }
        [DisplayName("Product Size")]
        public int? Size { get; set; }
        [Required]
        [DisplayName("Product Price")]
        public double Price { get; set; }
        [DisplayName("Product Owner")]
        public string Owner { get; set; }

        //One to many relation=One category can have many product
        // (ModelState.IsValid) in CreateProduct it means it will validate evrything but
        //we dont't want to validate Category and ImageUrl that's why we use [ValidateNever]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        
        public Category? Category { get; set; }
        [ValidateNever]
        public string? ImageUrl { get; set; }
    }
}
