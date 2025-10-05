using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    internal class Membership : BaseEntity
    {
        // StartDate = CreatedAt Of BaseEntity => Fluent Api

        public DateTime EndDate { get; set; }
        // Read Only Property
        public string Status
        {
            get
            {
                if (EndDate <= DateTime.Now)
                    return "Expired";
                else
                    return "Active";
            }
        }
        public Member Member { get; set; } = null!;
        public int MemberId { get; set; }
        public Plan Plan { get; set; } = null!;
        public int PlanId { get; set; }


    }
}
