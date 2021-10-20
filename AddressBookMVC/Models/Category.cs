using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookMVC.Models
{
    public class Category
    {
        //primary key
        public int Id { get; set; }

        //foreign Key
        public string UserId { get; set; }

        [Required]
        [Display(Name ="Category Name")]
        public string Name { get; set; }

        public virtual AppUser User { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; } = new HashSet<Contact>();


    }
}
