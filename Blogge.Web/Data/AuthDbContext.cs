using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Blogge.Web.Data

{
    public class AuthDbContext:IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options): base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // seed Role (Admin,User,SuperAdmin)
            var adminroleId = "cb3212c3-9bad-4a5d-8676-d5656dc6fa38";
            var superAdminroleId ="d517011d-ab41-4f89-b8b3-a0312f336298";
            var userRoleId = "2710dd59-b3e5-4f44-bdd4-fc2efb9e6d4c";
            var roles = new List<IdentityRole> {new IdentityRole
            {
                Name="Admin",
                NormalizedName="Admin",
                Id=adminroleId,
                ConcurrencyStamp=adminroleId

            },
            new IdentityRole
            {
                Name="SuperAdmin",
                NormalizedName="SuperAdmin",
                Id=superAdminroleId,
                ConcurrencyStamp =superAdminroleId
            },
            new IdentityRole
            {
                    Name="User",
                    NormalizedName="User",
                    Id=userRoleId,
                    ConcurrencyStamp=userRoleId
            }
                };
            builder.Entity<IdentityRole>().HasData(roles);
            //Seed SuperAdmin User
            var superAdminId = "b4c23002-2c46-4811-9dde-a0108090bd5c";
            var superAdminUser = new IdentityUser
            {
                UserName = "SuperAdmin@bloggie.com",
                Email = "SuperAdmin@bloggie.com",
                NormalizedUserName = "SuperAdmin@bloggie.com".ToUpper(),
                NormalizedEmail = "SuperAdmin@bloggie.com".ToUpper(),
                Id= superAdminId
 
            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "Superadmin@123");
            builder.Entity<IdentityUser>().HasData(superAdminUser);
            //Add all Role SuperAdmin User
            var superAdminRole = new List<IdentityUserRole<string>>{
                new IdentityUserRole<string>
                {
                    RoleId=adminroleId,
                    UserId=superAdminId
                },
                 new IdentityUserRole<string>
                {
                    RoleId=superAdminroleId,
                    UserId=superAdminId
                },
                  new IdentityUserRole<string>
                {
                    RoleId=userRoleId,
                    UserId=superAdminId
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRole);
        }
    }
}
