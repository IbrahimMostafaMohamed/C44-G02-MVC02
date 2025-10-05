using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.interfaces
{
    internal interface IMembershipRepositories
    {
        // Get All
        IEnumerable<Membership> GetAll();
        // GetById
        Membership? GetById(int Id);
        // Add
        int Add(Membership member);
        // Update
        int Update(Membership member);
        // Delete
        int Delete(int Id);
    }
}
