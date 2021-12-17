using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI.Core.Model;

namespace WebAPI.DAL
{
    public class AppDBContext : IdentityDbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seeding a  'Administrator' role to AspNetRoles table
            modelBuilder.Entity<IdentityRole>()
                .HasData(new IdentityRole
                {
                    Id = "1c5e174e-3b0e-446f-86af-483d56fd7210",
                    Name = "admin",
                    NormalizedName = "ADMIN".ToUpper()
                }, new IdentityRole
                {
                    Id = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    Name = "user",
                    NormalizedName = "USER".ToUpper()
                }
                );

            // seed database
            var password = new PasswordHasher<User>();
            User user = new User
            {
                UserName = "admin"
            };
            var hashed = password.HashPassword(user, "secret");

            modelBuilder.Entity<User>()
                .HasData(new User
                {
                     UserName = "admin",
                     PasswordHash= hashed

                }
             );


        }
    }
}
