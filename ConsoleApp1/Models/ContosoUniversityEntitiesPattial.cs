using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public partial class ContosoUniversityEntities : DbContext
    {
        public override int SaveChanges()
        {
            var entries = this.ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                var now = DateTime.Now;
                if (entry.Entity is Department && entry.State == EntityState.Modified)
                {
                    entry.CurrentValues.SetValues(new { ModifiedOn = now });
                    System.Console.WriteLine("ModifiedOn" + now);
                }
                else if (entry.Entity is Department && entry.State == EntityState.Added)
                {
                    entry.CurrentValues.SetValues(new { CreateOn = now });
                    System.Console.WriteLine("CreateOn" + now);
                }
                
            }
            return base.SaveChanges();
        }
    }
}
