using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserRegistration.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int  UserId { get; set; }
        [Required (ErrorMessage ="Username is required")]
        public  string? UserName { get; set; }
        [Required (ErrorMessage ="First name is required")]
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required (ErrorMessage ="Mail ID is required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^(\d{10})$", 
                   ErrorMessage = "Entered phone format is not valid.")]
        public string? Mobile { get; set; }
      
       

        [Required(ErrorMessage = "Enter a password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*?[a-z])(?=.*?[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", 
            ErrorMessage = "Password must be between 8 and 15 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]

        public string? CreatePassword { get; set; }
        [Required(ErrorMessage ="Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("CreatePassword",ErrorMessage ="create password and confirm match should match")]
        public string? ConfirmPassword { get; set; }

        
    }
}
