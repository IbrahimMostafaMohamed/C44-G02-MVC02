using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    internal class Plan : BaseEntity
    {
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; } = null!;
        [MaxLength(200)]
        [Column(TypeName = "varchar(200)")]
        public string Description { get; set; } = null!;
        [Range(1, 365, ErrorMessage = "Duration must be between 1 and 365 days")]
        public int DurationDays { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Membership> Memberships { get; set; } = new List<Membership>();

    }
}
