using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using ExpenseMangement11.Models;
using ExpenseMangement11.Services;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace ExpenseMangement11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExpenseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ExpenseService _expenseService;
        private readonly UserService _userService;
        private int categoryId;

        public ExpenseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // CRUD operations for expenses...



        public ExpenseController(UserService userService, ExpenseService expenseService)
        {
            _userService = userService;
            _expenseService = expenseService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Expense>> GetExpenses()
        {
            try
            {
                var expenses = _expenseService.GetExpensesByCategory(categoryId);
                if (expenses.Any())
                {
                return Ok(expenses);
                }
                else
                {
                    return NotFound(new { Message = $"No expenses found for category with ID {categoryId}." });
                }
                }
            catch (ServiceException ex)
            {
                return StatusCode(500, new { Message = "Internal server error during fetching expenses.", Error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Expense> GetExpense(int id)
        {
            try
            {
                var expense = _expenseService.GetExpense(id);
                if (expense == null)
                {
                    return NotFound();
                }
                return Ok(expense);
            }
            catch (ServiceException ex)
            {
                return StatusCode(500, new { Message = $"Internal server error during fetching expense with ID {id}.", Error = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult AddExpense([FromBody] Expense expense)
        {
            try
            {
                _expenseService.AddExpense(expense);
                return Ok();
            }
            catch (ServiceException ex)
            {
                return StatusCode(500, new { Message = "Internal server error during adding a new expense.", Error = ex.Message });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Message = "Validation failed.", Error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateExpense(int id, [FromBody] Expense expense)
        {
            try
            {
                var existingExpense = _expenseService.GetExpense(id);
                if (existingExpense == null)
                {
                    return NotFound();
                }

                expense.ExpenseID = id;
                _expenseService.UpdateExpense(expense);
                return Ok();
            }
            catch (ServiceException ex)
            {
                return StatusCode(500, new { Message = $"Internal server error during updating expense with ID {id}.", Error = ex.Message });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Message = "Validation failed.", Error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteExpense(int id)
        {
            try
            {
                var existingExpense = _expenseService.GetExpense(id);
                if (existingExpense == null)
                {
                    return NotFound();
                }

                _expenseService.DeleteExpense(id);
                return Ok();
            }
            catch (ServiceException ex)
            {
                return StatusCode(500, new { Message = $"Internal server error during deleting expense with ID {id}.", Error = ex.Message });
            }
        }

    }
}
