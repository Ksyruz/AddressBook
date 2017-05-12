
using AddressBook.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace AddressBook.Web.Controllers
{
    public class AddressController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/DeliveryTracking
        [HttpGet]
        public IEnumerable<Contact> GetAddress(string search = null, string from = null, string to = null, int page = 0, int pagesize = 10)
        {
            var predicate = PredicateBuilder.True<Contact>();
            DateTime f, t = DateTime.Now;

            if (!string.IsNullOrWhiteSpace(search))
            {
                var lower = search.ToLower().ToLower();
                predicate = predicate.And((Contact p)
                    =>
                    p.EmailAddress.ToLower() == lower
                    || p.Forename.ToLower() == lower
                    || p.Surname.ToLower() == lower
                    || p.TwitterId.ToLower() == lower);
            }

            return db.Contacts.Where(predicate).AsNoTracking()
                .OrderByDescending(x => x.Forename)
                .Page(pagesize, page)
                .ToList();
        }

        // GET: api/DeliveryTracking/5
        [HttpGet]
        [ResponseType(typeof(Contact))]
        public async Task<IHttpActionResult> GetContact(int id)
        {
            var Contact = await db.Contacts.FirstOrDefaultAsync(c => c.ContactId == id);
            if (Contact == null)
            {
                return NotFound();
            }
            return Ok(Contact);

        }

        // PUT: api/DeliveryTracking/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutContact(int id, Contact model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Where(x => !string.IsNullOrWhiteSpace(x.ErrorMessage)).Select(e => e.ErrorMessage).ToList();
                var es = JsonConvert.SerializeObject(errors);
                return BadRequest(es);
            }

            if (id != model.ContactId)
            {
                var errors = new List<string> { "Record cannot be found for " + id };
                var es = JsonConvert.SerializeObject(errors);
                return BadRequest(es);
            }

            try
            {
                db.Contacts.AddOrUpdate(model);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException dbce)
            {
                if (!ContactExists(id))
                {
                    return NotFound();
                }
                else
                {
                    dbce.Log();
                    return BadRequest(JsonConvert.SerializeObject(new List<string> { "Database concurrency error" }));
                }
            }
            catch (Exception e)
            {
                e.Log();
                return BadRequest(JsonConvert.SerializeObject(new List<string> { "" }));
            }
            return Ok(); ;
        }

        // POST: api/DeliveryTracking
        [HttpPost]
        [ResponseType(typeof(Contact))]
        public async Task<IHttpActionResult> PostContact(Contact model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Where(x => !string.IsNullOrWhiteSpace(x.ErrorMessage)).Select(e => e.ErrorMessage).ToList().Concat(ModelState.Values.SelectMany(v => v.Errors).Where(x => !string.IsNullOrWhiteSpace(x.Exception.ToString())).Select(e => e.Exception.ToString()).ToList());

                var es = JsonConvert.SerializeObject(errors);
                return BadRequest(es);
            }
            try
            {
                db.Contacts.AddOrUpdate(model);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException dbce)
            {
                dbce.Log();
                return BadRequest(JsonConvert.SerializeObject(new List<string> { "Database concurrency error" }));
            }
            catch (Exception e)
            {
                e.Log();
                return BadRequest(JsonConvert.SerializeObject(new List<string> { "" }));
            }
            return Ok();
        }

        [HttpPost]
        [ResponseType(typeof(ICollection<Contact>))]
        [Route("api/DeliveryTracking/Batch")]
        public async Task<IHttpActionResult> PostContacts(ICollection<Contact> model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Where(x => !string.IsNullOrWhiteSpace(x.ErrorMessage)).Select(e => e.ErrorMessage).ToList().Concat(ModelState.Values.SelectMany(v => v.Errors).Where(x => !string.IsNullOrWhiteSpace(x.Exception.ToString())).Select(e => e.Exception.ToString()).ToList());

                var es = JsonConvert.SerializeObject(errors);
                return BadRequest(es);
            }
            try
            {
                db.Contacts.AddOrUpdate(model.ToArray());
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException dbce)
            {
                dbce.Log();
                return BadRequest(JsonConvert.SerializeObject(new List<string> { "Database concurrency error" }));
            }
            catch (Exception e)
            {
                e.Log();
                return BadRequest(JsonConvert.SerializeObject(new List<string> { "" }));
            }
            return Ok();
        }

        // DELETE: api/DeliveryTracking/5
        [HttpDelete]
        [ResponseType(typeof(Contact))]
        public async Task<IHttpActionResult> DeleteContact(int id)
        {
            Contact Contact = await db.Contacts.FindAsync(id);
            if (Contact == null)
            {
                return NotFound();
            }

            db.Contacts.Remove(Contact);
            await db.SaveChangesAsync();

            return Ok(Contact);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactExists(int id)
        {
            return db.Contacts.Count(e => e.ContactId == id) > 0;
        }
    }
}