using Microsoft.EntityFrameworkCore;

namespace MovieDBApp.Data{

    public class DataContext : DbContext{

        public DataContext(DbContextOptions<DataContext>options):base(options){}

        public DbSet<Bootcamp> Bootcamps => Set<Bootcamp>();
        public DbSet<Movie> Movies0 => Set<Movie>();
        public DbSet<MovieRegister> MovieRegisters => Set<MovieRegister>();
    }
}