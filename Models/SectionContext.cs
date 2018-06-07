using Microsoft.EntityFrameworkCore;

namespace ConferenceApi.Models
{
    public class SectionContext : DbContext
    {
        public SectionContext(DbContextOptions<SectionContext> options): base(options)
        {

        }

        public DbSet<SectionItem> Sections { get; set; }
    }
}
