using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;

namespace IntegrationTestsPOC.DataAccess
{
    public class PocContext : DbContext
    {
        public PocContext(DbConnection dbConnection) : base(dbConnection, true)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Person> People { get; set; }
    }

    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual ICollection<Person> People { get; set; }
    }

    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}