using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
            return _context.People;
        }

        [HttpGet]
        public Person Get(int id)
        {
            return _context.People.Find(id);
        }

        [HttpPost]
        public void Post([FromBody]Person person)
        {
            _context.People.Add(person);
            _context.SaveChanges();
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
