using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Project.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage ="Please Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]
        [EmailAddress(ErrorMessage = "Please Enter Valid Email")]
        [Remote("IsExist","Place",ErrorMessage ="Email Already Exist !")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Please Enter Password")]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage ="Your Password is Wrong Please Enter Correct Password")]
        public string ConfirmPassword {  get; set; }

        [Required(ErrorMessage ="Please enter mobile number =")]
        [RegularExpression(@"^[9,8,7,6]{1}\d{9}$",ErrorMessage ="Please enter valid mobile number")]
        public string PhoneNumber { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage ="Please enter address")]
        public string Address {  get; set; }
        public string Role { get; }
       
        
        
     
    }
}
