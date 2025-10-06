using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.interfaces
{
    internal interface ICategoryRepositories
    {
        // Get All
        IEnumerable<Category> GetAll();
        // GetById
        Category? GetById(int Id);
        // Add
        int Add(Category c);
        // Update
        int Update(Category c);
        // Delete
        int Delete(int Id);
    }
}
