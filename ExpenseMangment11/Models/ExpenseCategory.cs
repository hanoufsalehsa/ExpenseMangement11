using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpenseMangement11.Models{
public class ExpenseCategory {

    [Key]
     public int CategoryID { get; set; }


    //[Column(TypeName = VARCHAR(255) NOT NULL)]
    [Required]
    public string CategoryName { get; set; }

    public string Description { get; set; }
    
    // Navigation property representing the relationship
    public ICollection<Expense> Expenses { get; set; }

}
}