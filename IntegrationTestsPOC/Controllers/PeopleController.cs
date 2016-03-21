using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Http;
using IntegrationTestsPOC.DataAccess;

namespace IntegrationTestsPOC.Controllers
{
    public class PeopleController : ApiController
    {
        private readonly PocContext _context;

        public PeopleController(PocContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Person> Get()
        {
            try
            {
                var dbSet = _context.People.ToList();
                return dbSet;
            }
            catch (Exception ex)
            {
                
                throw;
            }

            return null;
        }

        [HttpGet]
        public Person Get(int id)
        {
            return _context.People.Find(id);
        }

        [HttpPost]
        public Person Post([FromBody]Person person)
        {
            var addedPerson = _context.People.Add(person);
            _context.SaveChanges();

            return addedPerson;
        }

        [HttpPut]
        public void Put(int id, [FromBody]Person person)
        {
            _context.People.AddOrUpdate(person);
            _context.SaveChanges();
        }

        [HttpDelete]
        public void Delete(int id)
        {
            var person = _context.People.Find(id);
            _context.People.Remove(person);

            _context.SaveChanges();
        }
    }
}
