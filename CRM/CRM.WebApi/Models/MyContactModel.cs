﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.EntityFramework;

namespace CRM.WebApi.Models
{
    public class MyContactModel
    {
        public MyContactModel()
        {

        }

        public MyContactModel(Contact c)
        {
            FullName = c.FullName;
            CompanyName = c.CompanyName;
            Position = c.Position;
            Country = c.Country;
            Email = c.Email;
            Guid = c.Guid;
            DateInserted = c.DateInserted;
            MailingLists = c.MailingLists.Select(x => x.MailingListName).ToList();
        }
        public int ContactId { get; set; }
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public Guid? Guid { get; set; }
        public DateTime? DateInserted { get; set; }
        public List<string> MailingLists { get; set; }
    }
}