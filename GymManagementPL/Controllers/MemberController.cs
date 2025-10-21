using System.Reflection;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        public IActionResult Index()
        {
            var members = _memberService.GetAllMembers();
            return View(members);
        }

        public IActionResult MemberDetails(int Id)
        {
            if (Id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Member Not be Zero Or Negative";
                return RedirectToAction(nameof(Index));
            }
            var Member = _memberService.GetMemberDetails(Id);
            if (Member == null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(Member);

        }
        public IActionResult HealthRecordDetails(int Id)
        {
            if (Id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Member Not be Zero Or Negative";
                return RedirectToAction(nameof(Index));
            }
            var HealthRecord = _memberService.GetMemberHealthRecordDetails(Id);
            if (HealthRecord == null)
            {
                TempData["ErrorMessage"] = "Health Record Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(HealthRecord);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateMember(CreateMemberViewModel CreatedMember)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Check Data And Missing Fields");
                return View(nameof(Create), CreatedMember);
            }
            bool Result = _memberService.CreateMember(CreatedMember);
            if (Result)
                TempData["SuccessMessage"] = "Member is Created Successfully";
            else
                TempData["ErrorMessage"] = "Member failed Create , Check Phone And Email";
            return RedirectToAction(nameof(Index));

        }
        public IActionResult MemberEdit(int Id)
        {
            if(Id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Member Not be Zero Or Negative";
                return RedirectToAction(nameof(Index));
            }
            var Member = _memberService.GetMemberToUpdate(Id);
            if (Member == null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(Member);
        }
        [HttpPost]
        public IActionResult MemberEdit([FromRoute] int id , MemberToUpdateViewModel MemberToEdit)
        {
            if(!ModelState.IsValid)
                return View(MemberToEdit);
            bool Result = _memberService.UpdateMemberDetails(id,MemberToEdit);
            if (Result)
                TempData["SuccessMessage"] = "Member Updated Successfully";
            else
                TempData["ErrorMessage"] = "Member Failed To Update";
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Member Not be Zero Or Negative";
                return RedirectToAction(nameof(Index));
            }
            var Member = _memberService.GetMemberDetails(id);
            if (Member == null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MemberId = id;
            ViewBag.MemberName = Member.Name;
            return View();
        }
        [HttpPost]
        public IActionResult DeleteConfirmed([FromForm] int id)
        {
            var Result = _memberService.RemoveMember(id);
            if (Result)
                TempData["SuccessMessage"] = "Member Deleted Successfully";
            else
                TempData["ErrorMessage"] = "Member Failed To Delete";
            return RedirectToAction(nameof(Index));

        }

    }
}
