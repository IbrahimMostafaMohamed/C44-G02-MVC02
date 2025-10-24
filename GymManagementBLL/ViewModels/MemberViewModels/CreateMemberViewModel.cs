using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementDAL.Entities.Enums;
using Microsoft.AspNetCore.Http;

namespace GymManagementBLL.ViewModels.MemberViewModels
{
    public class CreateMemberViewModel
    {
        [Required(ErrorMessage ="Profile Photo Is Required")]
        [Display(Name="Profile Photo")]
        public IFormFile PhotoFile { get; set; } = null!;
        [Required (ErrorMessage = "Name Is Required")]
        [StringLength(maximumLength:50 , MinimumLength =2 , ErrorMessage ="Name Must Be Between 2 And 50 Characters")]
        [RegularExpression(@"^[a-zA-Z\s]+$")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        [DataType(DataType.EmailAddress)]
        [StringLength(100,MinimumLength =5 ,ErrorMessage = "Email Must Be Between 5 And 100 Characters")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Phone Is Required")]
        [Phone(ErrorMessage = "Invalid Phone Format")]
        [RegularExpression(@"^(010|011|012|015)[0-9]{8}$" , ErrorMessage = "Phone Number Must be Valid Egyptian Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; } = null!;
        [Required(ErrorMessage = "DateOfBirth Is Required")]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender Is Required")]
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "Building Number Is Required")]
        [Range(1,9000 , ErrorMessage = "Building Number Must be Between 1 And 9000")]
        public int BuildingNumber { get; set; }
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Street Must Be Between 2 And 30 Characters")]
        [Required(ErrorMessage = "Street Is Required")]
        public string Street { get; set; } = null!;

        [StringLength(30, MinimumLength = 2, ErrorMessage = "Street Must Be Between 2 And 30 Characters")]
        [Required(ErrorMessage = "Street Is Required")]
        [RegularExpression(@"^[a-zA-Z\s]+$" , ErrorMessage ="City Can Contain Only Letters And Spaces")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "Health Record Is Required")]
        public HealthRecordViewModel HealthRecordViewModel { get; set; } = null!;

    }
}
