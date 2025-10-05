using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementDAL.Entities.Enums;

namespace GymManagementDAL.Entities
{
    internal class Trainer : GymUser
    {
        // HireDate = CreatedAt From BaseEntity  => Fluent Api
        public Specialties Specialties { get; set; }
        public ICollection<Session>? Sessions { get; set; } 
        
    }
}
