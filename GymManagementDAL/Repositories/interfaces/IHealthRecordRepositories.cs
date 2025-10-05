using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.interfaces
{
    internal interface IHealthRecordRepositories
    {
        // Get All
        IEnumerable<HealthRecord> GetAll();
        // GetById
        HealthRecord? GetById(int Id);
        // Add
        int Add(HealthRecord H);
        // Update
        int Update(HealthRecord H);
        // Delete
        int Delete(int Id);
    }
}
