using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    public class Session : BaseEntity
    {
        public string Description { get; set; } = null!;
        [Range(1, 25, ErrorMessage = "Capacity must be between 1 and 25")]
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Category Category { get; set; } = null!; 
        public int CategoryId { get; set; }

        public Trainer Trainer { get; set; } = null!;
        public int TrainerId { get;set; }
        public ICollection<MemberSession> MemberSessions { get; set; } = new List<MemberSession>();

    }
}
