using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.interfaces
{
    internal interface ISessionRepositories
    {
        // Get All
        IEnumerable<Session> GetAll();
        // GetById
        Session? GetById(int Id);
        // Add
        int Add(Session S);
        // Update
        int Update(Session S);
        // Delete
        int Delete(int Id);
    }
}
