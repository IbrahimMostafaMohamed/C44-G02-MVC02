using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GymManagementDAL.Entities
{
    
    internal class HealthRecord : BaseEntity
    {
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string BloodType { get; set; } = null!;
        public string? Notes { get; set; }

        ////  LastUpdate = UpdatedAt Of Member => Fluent Api 
        
        public Member Member { get; set; } = null!;

    }
}
