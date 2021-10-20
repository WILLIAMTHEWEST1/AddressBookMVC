using AddressBookMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookMVC.Services.Interfaces
{
    public interface ICategoryService
    {

        Task AddContactToCategoryAsync(int categoryId, int contactId);

        Task RemoveContactFromCategoryAsync(int categoryId, int contactId);

        Task<ICollection<Category>> GetContactCategoriesAsync(int contactId);

        Task<int> GetContactCountForCategoryAsync(int categoryId);

        Task <bool> IsContactInCategory(int categoryId, int contactId);

    }
}
