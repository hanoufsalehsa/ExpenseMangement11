using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ExpenseMangement11.Controllers;
using ExpenseMangement11.Services;
namespace ExpenseMangement11.Models{
public class ExpenseCategory {

    [Key]
     public int CategoryID { get; set; }


    //[Column(TypeName = VARCHAR(255) NOT NULL)]
    [Required]
    public required string CategoryName { get; set; }

    public required string Description { get; set; }
    
    // Navigation property representing the relationship
    public required ICollection<Expense> Expenses { get; set; }

}
}