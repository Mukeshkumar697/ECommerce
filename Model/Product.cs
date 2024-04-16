using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Model
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required(ErrorMessage ="Please enter product name")]
   
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Please enter Description")]

        public string Description { get; set; }
        [Required(ErrorMessage = "Please enter Price")]


        public int Price { get; set; }
        [Required(ErrorMessage = "Please enter Stock")]

        public int Stock { get; set; }
        [Required(ErrorMessage = "Please enter CategoryId")]


        public int CategoryId { get; set; }

        //[ForeignKey("CategoryId")]
       // public virtual Category Category { get; set; }

        public enum StatusEnum
        {
            Active,
                Inactive
        }

        public StatusEnum Status { get; set; }  
        public DateTime DateTime { get; set; }  = DateTime.Now; 






    }
}
