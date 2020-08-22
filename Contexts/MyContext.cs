using LizardPirates.Models;
using Microsoft.EntityFrameworkCore;


namespace LizardPirates.Contexts
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options){}
        public DbSet<Lizard> Lizards {get;set;}

        public DbSet<Pet> Pets {get;set;}
    }
}