using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExpenseMangement11.Controllers;
using ExpenseMangement11.Models;
using System.Linq;
using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseMangement11.Services
{
    public interface IExpenseService
    {
        //Task<List<Expense>> GetExpenses();
        //Task<Expense> GetExpenseById(int id);
        //Task<Expense> CreateExpense(Expense expense);
        //Task<Expense> UpdateExpense(int id, Expense expense);
        //Task<bool> DeleteExpense(int id);
        Task<List<Expense>> GetExpensesByCategory(int categoryId);
    }


    public class ExpenseService : IExpenseService
    {
        private readonly ApplicationDbContext _context;

        public ExpenseService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Implementation of CRUD methods for expenses...



        public IEnumerable<Expense> GetExpensesByCategory(int categoryId)
        {
            try
            {
                return _context.Expenses
                .Where(e => e.CategoryID == categoryId)
                .ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceException("Error occurred while fetching expenses.", ex);
            }
        }

        public Expense GetExpense(int expenseId)
        {
            try
            {
                return _context.Expenses.Find(expenseId);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Error occurred while fetching expense with ID {expenseId}.", ex);
            }
        }

        public void AddExpense(Expense expense)
        {
            ValidateExpense(expense); // Call the validation method

            try
            {
                _context.Expenses.Add(expense);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ServiceException("Error occurred while adding a new expense.", ex);
            }
        }

        public void UpdateExpense(Expense expense)
        {
            ValidateExpense(expense); // Call the validation method

            try
            {
                _context.Expenses.Update(expense);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Error occurred while updating expense with ID {expense.ExpenseID}.", ex);
            }
        }

        public void DeleteExpense(int expenseId)
        {
            try
            {
                var expense = _context.Expenses.Find(expenseId);
                if (expense != null)
                {
                    _context.Expenses.Remove(expense);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Error occurred while deleting expense with ID {expenseId}.", ex);
            }
        }

        // Other CRUD operations...

        private void ValidateExpense(Expense expense)
        {
            // Validate date format
            if (!IsValidDateFormat(expense.Date))
            {
                throw new ValidationException("Invalid date format. Please use the correct format.");
            }

            // Validate category existence
            if (!CategoryExists(expense.CategoryID))
            {
                throw new ValidationException($"Expense category with ID {expense.CategoryID} does not exist.");
            }

            // Add additional validations as needed...
        }

       // private bool IsValidDateFormat(DateTime date)
        //{
            // Implement your date format validation logic here
            //return true; // Replace with your validation logic
        private bool CategoryExists(int categoryID)
        {
            throw new NotImplementedException();
            
        }

        private bool IsValidDateFormat(DateTime date)
        {
            throw new NotImplementedException();
        }

        internal object GetExpensesByCategory(object categoryId)
        {
            throw new NotImplementedException();
        }

        Task<List<Expense>> IExpenseService.GetExpensesByCategory(int categoryId)
        {
            throw new NotImplementedException();
        }
    

        //private bool CategoryExists(int categoryId)
        //{
            // Check if the category with the given ID exists in the database
        //    return _context.ExpenseCategories.Any(c => c.CategoryID == categoryId);
       // }

        // Implement additional expense-related methods as needed...

    
    }




 }
    

