using System.Data.Entity;

namespace IrregularVerbs
{
    public class VerbsDbContext : DbContext
    {
        public DbSet<Verb> Verbs { get; set; }
    }
}
