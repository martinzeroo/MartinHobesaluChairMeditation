using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MartinHobesaluChairMeditation.Models;

namespace MartinHobesaluChairMeditation.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MartinHobesaluChairMeditation.Models.ChairMeditation> ChairMeditation { get; set; }
    }
}