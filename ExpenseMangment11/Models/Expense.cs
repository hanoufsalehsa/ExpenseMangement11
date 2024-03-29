using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseMangement11.Models{
public class Expense {

    [Key]
     public int ExpenseID { get; set; }

    //user
    public int UserID { get; set; }

    [ForeignKey("UserID")]
    public required User User { get; set; }

    //Expense category
    public int CategoryID { get; set; }

    [ForeignKey("CategoryID")]
    public required ExpenseCategory Category { get; set; }



    public DateTime Date { get; set; } 
    public decimal Amount { get; set; }
    public required string Description { get; set; }
    public byte[] ReceiptImage { get; set; }

}
}