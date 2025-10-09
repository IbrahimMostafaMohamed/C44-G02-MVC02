using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.PlanViewModels
{
    internal class UpdatePlanViewModel
    {
        [Required(ErrorMessage = "Plan Name is Required")]
        [StringLength(50 , ErrorMessage = "Plan Name Must be less Than 51 char")]
        public string PlanName { get; set; } = null!;
        [Required(ErrorMessage = "Description is Required")]
        [StringLength(200,MinimumLength =5, ErrorMessage = "Description Name Must be between 5 And 200 char")]
        public string Description  { get; set; } = null!;

        [Required(ErrorMessage = "Duration Days is Required")]
        [Range(1, 365, ErrorMessage = "Duration Days must be between 1 and 365")]
        public int DurationDays { get; set; }


        [Required(ErrorMessage = "Price Days is Required")]
        [Range(0.1, 10000, ErrorMessage = "Duration Days must be between 0.1 and 10000")]
        public decimal Price { get; set; }


    }
}
