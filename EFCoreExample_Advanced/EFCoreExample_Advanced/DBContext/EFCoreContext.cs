using EFCoreExample_Advanced.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreExample_Advanced.DBContext {
  public class EFCoreContext : DbContext {
    protected override void OnConfiguring(DbContextOptionsBuilder options) {
      // connect to sqlite database
      options.UseSqlite("Data Source=Student.sqlite");
    }
    public DbSet<Student> Students { get; set; }
  }
}
