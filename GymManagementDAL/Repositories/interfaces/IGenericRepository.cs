using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity, new()
    {
        // Get All
        IEnumerable<T> GetAll(Func<T, bool>? condition = null);
        // Get By Id
        T? GetById(int id);
        // Add
        void Add(T entity);
        // Update
        void Update(T entity);
        // Delete
        void Delete(int id);

    }
}
