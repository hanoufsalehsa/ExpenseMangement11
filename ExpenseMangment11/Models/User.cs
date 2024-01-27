// User.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ExpenseMangement11.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(255) NOT NULL")]
        public string Username { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(255) NOT NULL")]
        public string Password { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(255) NOT NULL")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(255) NOT NULL")]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(255) NOT NULL")]
        public string Email { get; set; }

        public ICollection<Expense> Expenses { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }
    }

    public class LoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class RefreshTokenModel
    {
        [Required]
        public string Token { get; set; }
    }
}