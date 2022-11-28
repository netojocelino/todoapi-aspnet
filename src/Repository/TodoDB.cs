using Microsoft.EntityFrameworkCore;

using Model;

namespace Repository
{
    class TodoDB : DbContext {
        public TodoDB(DbContextOptions<TodoDB> options)
            : base(options) { }
        public DbSet<Todo> Todos => Set<Todo>();
    }
}