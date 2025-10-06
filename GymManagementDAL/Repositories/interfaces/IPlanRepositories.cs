using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.interfaces
{
    internal interface IPlanRepositories
    {
        // Get All
        IEnumerable<Plan> GetAll();
        // GetById
        Plan? GetById(int Id);
        // Add
        int Add(Plan p);
        // Update
        int Update(Plan p);
        // Delete
        int Delete(int Id);
    }
}
