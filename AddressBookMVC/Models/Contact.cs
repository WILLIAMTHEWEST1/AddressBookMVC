using AddressBookMVC.enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookMVC.Models
{
    public class Contact
    {                                                         
        //primary key
        public int Id { get; set; }

        //foreign key
        public string UserId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        [Required]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [Required]
        public string City { get; set; }

        public States State { get; set; }

        [DataType(DataType.PostalCode)]
        public int ZipCode { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        public DateTime Created { get; set; }

        [NotMapped]
        [DataType(DataType.Upload)]
        [Display(Name ="Contact Image")]
        public IFormFile ImageFile { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageType { get; set; }

        [NotMapped]
        public string FullName { get { return $"{FirstName} {LastName}"; } }

        public virtual AppUser User { get; set; }

        public virtual ICollection<Category> Categories { get; set; } = new HashSet<Category>();

    }
}
