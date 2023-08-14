using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace First.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        [DisplayName("Category Name")] //can show it in label if we use asp-for="Name"
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display order must be between 1 to 100")]
        public int DisplayOrder { get; set; }
    }
}
