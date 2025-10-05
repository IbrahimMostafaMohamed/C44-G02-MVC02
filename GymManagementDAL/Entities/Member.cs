using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    internal class Member : GymUser
    {
        // JoinDate = CreatedAt Of BaseEntity => Fluent Api
        public string? Photo { get; set; }
        public HealthRecord HealthRecord { get; set; } = null!;
        public ICollection<Membership> Memberships { get; set; } = new List<Membership>();
        public ICollection<MemberSession> MemberSessions { get; set; } = new List<MemberSession>();

    }
}
