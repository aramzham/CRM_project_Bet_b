﻿using CRM.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CRM.WebApi.Infrastructure;
using CRM.WebApi.Models;

namespace CRM.WebApi.Controllers
{
    public class ContactsController : ApiController
    {
        private ApplicationManager appManager = new ApplicationManager();

        // GET: api/Contacts
        public async Task<List<ContactResponseModel>> GetContacts()
        {
            return await appManager.GetAllContacts();
        }

        // GET: api/Contacts/5
        [ResponseType(typeof(Contact))]
        public async Task<IHttpActionResult> GetContact(int id)
        {
            var contact = await appManager.GetContactById(id);
            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        // GET: api/Contacts/guid
        [ResponseType(typeof (ContactResponseModel))]
        public async Task<IHttpActionResult> GetContactByGuid([FromUri]string guid)
        {
            var contact = await appManager.GetContactByGuid(guid);
            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        // GET: api/Contacts/?start=1&numberOfRows=2&ascending=false
        [ResponseType(typeof(Contact))]
        public async Task<IHttpActionResult> GetContact(int start, int numberOfRows, bool ascending)
        {
            //start should be 1-based (f.e. if you want from first record, then type 1)
            var contacts = await appManager.GetByPage(start, numberOfRows, ascending);

            if (contacts == null)
            {
                return NotFound();
            }

            return Ok(contacts);
        }

        // PUT: api/Contacts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutContact(int id, [FromBody]Contact contact)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (id != contact.ID) return BadRequest();

            if (!await appManager.UpdateContact(id, contact)) return NotFound();
            else return StatusCode(HttpStatusCode.NoContent);
        }

        // PUT: api/Contacts/guid
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutContact(string guid, [FromBody]ContactRequestModel contact)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (contact.Guid == null || guid != contact.Guid.ToString()) return BadRequest();

            if (!await appManager.UpdateContact(guid, contact)) return NotFound();
            else return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Contacts
        [ResponseType(typeof(Contact))]
        public async Task<IHttpActionResult> PostContact([FromBody]Contact contact)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await appManager.AddContact(contact);

            return CreatedAtRoute("DefaultApi", new { id = contact.ID }, contact);
        }

        // POST: api/Contacts
        [ResponseType(typeof(Contact))]
        public async Task<IHttpActionResult> PostContact([FromBody]ContactRequestModel contact)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await appManager.AddContact(contact);

            return CreatedAtRoute("DefaultApi", new { id = contact.Guid }, contact); //do we need this?
        }

        // DELETE: api/Contacts/5
        [ResponseType(typeof(Contact))]
        public async Task<IHttpActionResult> DeleteContact(int id)
        {
            var contact = await appManager.RemoveContact(id);
            if (contact == null) return NotFound();
            else return Ok(contact);
        }

        // DELETE: api/Contacts/guid
        [ResponseType(typeof(ContactResponseModel))]
        public async Task<IHttpActionResult> DeleteContact(string guid)
        {
            var contact = await appManager.RemoveContact(guid);
            if (contact == null) return NotFound();
            else return Ok(contact);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                appManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
