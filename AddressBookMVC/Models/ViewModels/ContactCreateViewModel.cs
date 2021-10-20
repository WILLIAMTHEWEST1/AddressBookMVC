using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookMVC.Models.ViewModels
{
    public class ContactCreateViewModel
    {
        public Contact Contact { get; set; } = new Contact();

        public SelectList CategoryList { get; set; }
    }
}
