using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HikingApp.Data
{
    public class MyAppAuthContext : IdentityDbContext
    {
        public MyAppAuthContext(DbContextOptions<MyAppAuthContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "626267fd-b640-4f2d-8ea5-a68f9dd4dd8a";
            var writerRoeId = "8250dafd-8e91-42ab-bc30-b840a1973695";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },

                new IdentityRole
                {
                    Id = writerRoeId,
                    ConcurrencyStamp= writerRoeId,
                    Name ="Writer",
                    NormalizedName="Writer".ToUpper()
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
