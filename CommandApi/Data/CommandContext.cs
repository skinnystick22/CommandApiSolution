using CommandApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandApi.Data
{
    public class CommandContext : DbContext
    {
        public DbSet<Command> CommandItems { get; set; }
        
        public CommandContext(DbContextOptions<CommandContext> options) : base(options)
        {
            
        }
    }
}