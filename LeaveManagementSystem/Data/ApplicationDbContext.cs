using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id= "acbf0ea3-560d-4d28-bf8a-9531b31da52c", Name = "Administrator", NormalizedName = "ADMINISTRATOR" },
                new IdentityRole { Id = "6e6928d3-dbe9-4dc1-9454-2785cb29b2ab", Name = "User", NormalizedName = "USER" },
                new IdentityRole { Id = "3e048bc5-badd-49b0-8a49-64afffa5f24d", Name= "Viewer", NormalizedName = "USER" }
            );
            builder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = "d1b9c8e5-9c3a-4f0e-8a2b-1a2b3c4d5e6f",
                    UserName = "admin@localhost",
                    NormalizedUserName = "ADMIN@LOCALHOST",
                    Email = "admin@localhost",
                    NormalizedEmail = "ADMIN@LOCALHOST",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Admin123!")
                }
            );
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "d1b9c8e5-9c3a-4f0e-8a2b-1a2b3c4d5e6f",
                    RoleId = "acbf0ea3-560d-4d28-bf8a-9531b31da52c"
                }
            );
        }
        
        public DbSet<LeaveType> LeaveTypes { get; set; }
    }
}
