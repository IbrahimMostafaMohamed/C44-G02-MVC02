using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementDAL.Entities;


namespace GymManagementDAL.Repositories.interfaces
{
    internal interface ITrainerRepositories
    {
        IEnumerable<Trainer> GetAll();
        Trainer? GetById(int Id);
        int Add(Trainer T);
        int Update(Trainer T);
        // Delete
        int Delete(int Id);
    }
}
