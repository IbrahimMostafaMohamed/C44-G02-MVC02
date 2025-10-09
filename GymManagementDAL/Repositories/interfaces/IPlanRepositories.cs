using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.interfaces
{
    public interface IPlanRepository
    {
        // Get All
        IEnumerable<Plan> GetAll();
        // GetById
        Plan? GetById(int Id);
        // Add
        int Add(Plan member);
        // Update
        int Update(Plan plan);
        // Delete
        int Delete(int Id);
    }
}
