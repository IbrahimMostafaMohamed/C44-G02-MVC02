using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementDAL.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace GymManagementDAL.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(Phone), IsUnique = true)]
    public abstract class GymUser : BaseEntity
    {
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; } = null!;
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [MaxLength(11)]
        [Column(TypeName = "varchar(11)")]
        [RegularExpression(@"^(010|011|012|015)[0-9]{8}$",
         ErrorMessage = "Phone number must be a valid Egyptian mobile number.")]
        public string Phone { get; set; } = null!;

        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public Address Address { get; set; } = null!;
    }
    [Owned]
    public class Address
    {
        public int BuildingNumber { get; set; }
        [MaxLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string Street { get; set; } = null!;
        [MaxLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string City { get; set; } = null!;
    }
}
