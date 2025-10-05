using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.interfaces
{
    internal interface IMemberRepositories
    {
        // Get All
        IEnumerable<Member> GetAll();
        // GetById
        Member? GetById(int Id);
        // Add
        int Add(Member member);
        // Update
        int Update(Member member);
        // Delete
        int Delete(int Id);

    }
}
