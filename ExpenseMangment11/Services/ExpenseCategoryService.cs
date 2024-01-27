using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using ExpenseMangement11.Models;
using System.Linq;
using ExpenseMangement11.Controllers;



namespace ExpenseMangement11.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryService>> GetCategories();
        Task<CategoryService> GetCategoryById(int id);
        Task<CategoryService> CreateCategory(CategoryService category);
        Task<CategoryService> UpdateCategory(int id, CategoryService category);
        Task<bool> DeleteCategory(int id);
    }


    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly ApplicationDbContext _dbContext;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }




        // Implementation of CRUD methods for categories...




        

       // public ExpenseCategoryService(ApplicationDbContext dbContext)
       // {
       //     _dbContext = dbContext;
        //}

        public IEnumerable<ExpenseCategory> GetExpenseCategories()
        {
            return _dbContext.ExpenseCategories.ToList();
        }

        public ExpenseCategory GetExpenseCategory(int categoryId)
        {
            return _dbContext.ExpenseCategories.Find(categoryId);
        }

        public void AddExpenseCategory(ExpenseCategory category)
        {
            _dbContext.ExpenseCategories.Add(category);
            _dbContext.SaveChanges();
        }

        public void UpdateExpenseCategory(ExpenseCategory category)
        {
            _dbContext.ExpenseCategories.Update(category);
            _dbContext.SaveChanges();
        }

        public void DeleteExpenseCategory(int categoryId)
        {
            var category = _dbContext.ExpenseCategories.Find(categoryId);
            if (category != null)
            {
                _dbContext.ExpenseCategories.Remove(category);
                _dbContext.SaveChanges();
            }
        }

        public Task<List<CategoryService>> GetCategories()
        {
            throw new NotImplementedException();
        }

        public Task<CategoryService> GetCategoryById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryService> CreateCategory(CategoryService category)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryService> UpdateCategory(int id, CategoryService category)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }

        Task<List<CategoryService>> ICategoryService.GetCategories()
        {
            throw new NotImplementedException();
        }

        Task<CategoryService> ICategoryService.GetCategoryById(int id)
        {
            throw new NotImplementedException();
        }

        
        }

        
    }

