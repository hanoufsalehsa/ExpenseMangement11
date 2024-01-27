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
        public required string Username { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(255) NOT NULL")]
        public required string Password { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(255) NOT NULL")]
        public required string FirstName { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(255) NOT NULL")]
        public required string LastName { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(255) NOT NULL")]
        public required string Email { get; set; }

        public ICollection<Expense> Expenses { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        public required string Email { get; set; }
    }

    public class LoginModel
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        public required string Password { get; set; }
    }

    public class RefreshTokenModel
    {
        [Required]
        public required string Token { get; set; }
    }
}