using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace ExpenseMangement11.Models
{
public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships...

            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<Expense>()
            .HasOne(e => e.User)
            .WithMany(u => u.Expenses)
            .HasForeignKey(e => e.UserID);

            modelBuilder.Entity<Expense>()
            .HasOne(e => e.Category)
            .WithMany(c => c.Expenses)
            .HasForeignKey(e => e.CategoryID);

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){

         //optionsBuilder.UseNpgsql("DevConnection",providerOptions => { providerOptions.EnableRetryOnFailure(); });


     }

        internal void AddExpenseCategory(ExpenseCategory category)
        {
            throw new NotImplementedException();
        }

        internal void DeleteExpenseCategory(int id)
        {
            throw new NotImplementedException();
        }

        internal object GetExpenseCategories()
        {
            throw new NotImplementedException();
        }

        internal object GetExpenseCategory(int id)
        {
            throw new NotImplementedException();
        }

        internal void UpdateExpenseCategory(ExpenseCategory category)
        {
            throw new NotImplementedException();
        }
    }
}

    

//mine
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//options.UseNpgsql(builder.Configuration.GetConnectionString("DevConnection")));