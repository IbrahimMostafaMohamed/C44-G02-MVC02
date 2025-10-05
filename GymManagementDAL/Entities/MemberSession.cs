using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    internal class MemberSession : BaseEntity
    {
        // BookingDate = CreatedAt Of BaseEntity => Fluent Api
        public bool IsAttended { get; set; }
        public Member Member { get; set; } = null!;
        public int MemberId { get; set; }
        public Session Session { get; set; } = null!;
        public int SessionId { get; set; }
    }
}
