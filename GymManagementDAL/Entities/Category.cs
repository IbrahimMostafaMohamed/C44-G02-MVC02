using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    public class Category : BaseEntity
    {
        [MaxLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string CategoryName { get; set; } = null!;

        public ICollection<Session>? Sessions { get; set; }
    }
}
