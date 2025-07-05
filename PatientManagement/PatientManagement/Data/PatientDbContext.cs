using Microsoft.EntityFrameworkCore;

namespace PatientManagement.Data
{
    public class PatientDbContext : DbContext
    {
        public PatientDbContext(DbContextOptions<PatientDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Patient> Patients { get; set; }
    }
}
