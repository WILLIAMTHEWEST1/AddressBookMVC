using AddressBookMVC.Data;
using AddressBookMVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookMVC.Services
{
    public class SearchService
    {
        private readonly ApplicationDbContext _context;

        public SearchService(ApplicationDbContext context)

        {
            _context = context;
        }

        public IEnumerable<Contact> SearchContacts(string searchString, string userId)
        {
            var result = _context.Contacts.Where(c => c.UserId == userId).AsQueryable();

            if (searchString is not null)
            {
                searchString = searchString.ToLower();

                result = result.Where(c => c.Address1.ToLower().Contains(searchString)
                || c.Address2.ToLower().Contains(searchString)
                || c.City.ToLower().Contains(searchString)
                || c.Email.ToLower().Contains(searchString)
                || c.FirstName.ToLower().Contains(searchString)
                || c.LastName.ToLower().Contains(searchString)
                );
            }
            return result.OrderByDescending(c => c.FirstName);
        }
    }
}
