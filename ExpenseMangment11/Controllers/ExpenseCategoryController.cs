using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseMangement11.Services;
using ExpenseMangement11.Models;


namespace ExpenseMangement11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExpenseCategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ApplicationDbContext _expenseCategoryService;


        public ExpenseCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // CRUD operations for categories...



       // public ExpenseCategoryController(ExpenseCategoryService expenseCategoryService)
       // {
           // _expenseCategoryService = expenseCategoryService;
       // }

        [HttpGet]
        public ActionResult<IEnumerable<ExpenseCategory>> GetExpenseCategories()
        {
            var categories = _expenseCategoryService.GetExpenseCategories();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public ActionResult<ExpenseCategory> GetExpenseCategory(int id)
        {
            var category = _expenseCategoryService.GetExpenseCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public ActionResult AddExpenseCategory([FromBody] ExpenseCategory category)
        {
            _expenseCategoryService.AddExpenseCategory(category);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateExpenseCategory(int id, [FromBody] ExpenseCategory category)
        {
            var existingCategory = _expenseCategoryService.GetExpenseCategory(id);
            if (existingCategory == null)
            {
                return NotFound();
            }

            category.CategoryID = id;
            _expenseCategoryService.UpdateExpenseCategory(category);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteExpenseCategory(int id)
        {
            var existingCategory = _expenseCategoryService.GetExpenseCategory(id);
            if (existingCategory == null)
            {
                return NotFound();
            }

            _expenseCategoryService.DeleteExpenseCategory(id);
            return Ok();
        }







    }
}