using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.interfaces;

namespace GymManagementBLL.Services.Classes
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var Members = _unitOfWork.GetRepository<Member>().GetAll();
            if (Members == null || !Members.Any()) return Enumerable.Empty<MemberViewModel>();
            var MemberViewModels = Members.Select(X => new MemberViewModel()
            {
                Id = X.Id,
                Name = X.Name,
                Email = X.Email,
                Photo = X.Photo,
                Phone = X.Phone,
                Gender = X.Gender.ToString()
            });
            return MemberViewModels;
        }

        public bool CreateMember(CreateMemberViewModel CreateMember)
        {
            try
            {

                if (IsEmailExists(CreateMember.Email) || IsPhoneExists(CreateMember.Phone)) return false;

                var member = new Member()
                {
                    Name = CreateMember.Name,
                    Email = CreateMember.Email,
                    Phone = CreateMember.Phone,
                    Gender = CreateMember.Gender,
                    DateOfBirth = CreateMember.DateOfBirth,
                    Address = new Address()
                    {
                        BuildingNumber = CreateMember.BuildingNumber,
                        City = CreateMember.City,
                        Street = CreateMember.Street
                    },
                    HealthRecord = new HealthRecord()
                    {
                        Height = CreateMember.HealthRecordViewModel.Height,
                        Weight = CreateMember.HealthRecordViewModel.Weight,
                        BloodType = CreateMember.HealthRecordViewModel.BloodType,
                        Notes = CreateMember.HealthRecordViewModel.Note
                    }

                };
                _unitOfWork.GetRepository<Member>().Add(member);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public MemberViewModel? GetMemberDetails(int MemberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(MemberId);
            if (member == null) return null;
            var ViewModel = new MemberViewModel()
            {
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                Gender = member.Gender.ToString(),
                DateOfBirth = member.DateOfBirth.ToShortDateString(),
                Address = $"{member.Address.BuildingNumber} - {member.Address.Street} - {member.Address.City}",
                Photo = member.Photo,
            };


            var ActiveMemberShip = _unitOfWork.GetRepository<Membership>().GetAll(X => X.MemberId == MemberId && X.Status == "Active")
                                                  .FirstOrDefault();
            if (ActiveMemberShip is not null)
            {
                ViewModel.MemberShipStartDate = ActiveMemberShip.CreatedAt.ToShortDateString();
                ViewModel.MemberShipEndDate = ActiveMemberShip.EndDate.ToShortDateString();

                var Plan = _unitOfWork.GetRepository<Plan>().GetById(ActiveMemberShip.PlanId);
                ViewModel.PlanName = Plan?.Name;

            }
            return ViewModel; 
        }

        public HealthRecordViewModel? GetMemberHealthRecordDetails(int MemberId)
        {
            var MemberHealthRecord = _unitOfWork.GetRepository<HealthRecord>().GetById(MemberId);
            if (MemberHealthRecord == null) return null;
            return new HealthRecordViewModel()
            {
                BloodType = MemberHealthRecord.BloodType,
                Height = MemberHealthRecord.Height,
                Weight = MemberHealthRecord.Weight,
                Note = MemberHealthRecord.Notes
            };

        }

        public MemberToUpdateViewModel? GetMemberToUpdate(int MemberId)
        {
            var Member = _unitOfWork.GetRepository<Member>().GetById(MemberId);
            if (Member == null) return null;
            return new MemberToUpdateViewModel()
            {
                Email = Member.Email,
                Name = Member.Name,
                Phone = Member.Phone,
                Photo = Member.Photo,
                BuildingNumber = Member.Address.BuildingNumber,
                City = Member.Address.City,
                Street = Member.Address.Street,
            };
        }

        public bool UpdateMemberDetails(int Id, MemberToUpdateViewModel UpdatedMember)
        {
            try
            {
                if (IsEmailExists(UpdatedMember.Email) || IsPhoneExists(UpdatedMember.Phone)) return false;
                var Member = _unitOfWork.GetRepository<Member>().GetById(Id);
                if (Member == null) return false;
                Member.Email = UpdatedMember.Email;
                Member.Phone = UpdatedMember.Phone;
                Member.Address.BuildingNumber = UpdatedMember.BuildingNumber;
                Member.Address.City = UpdatedMember.City;
                Member.Address.Street = UpdatedMember.Street;
                Member.UpdatedAt = DateTime.Now;
                _unitOfWork.GetRepository<Member>().Update(Member);
                return _unitOfWork.SaveChanges() > 0;

            }
            catch
            {
                return false;
            }
        }

        public bool RemoveMember(int MemberId)
        {
            var MemberRepo = _unitOfWork.GetRepository<Member>();
            var Member = MemberRepo.GetById(MemberId);
            if (Member == null) return false;
            var HasActiveMemberSessions = _unitOfWork.GetRepository<MemberSession>()
              .GetAll(x => x.MemberId == MemberId && x.Session.StartDate > DateTime.Now).Any();
            if (HasActiveMemberSessions) return false;

            var MemberShips = _unitOfWork.GetRepository<Membership>().GetAll(x => x.MemberId == MemberId);
            try
            {
                if (MemberShips.Any())
                {
                    foreach (var memberShip in MemberShips)
                    {

                        _unitOfWork.GetRepository<Membership>().Delete(memberShip.Id);
                    }
                }
                MemberRepo.Delete(Member.Id);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch { return false; }


        }

        #region Helper Method
        private bool IsEmailExists(string email)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(x => x.Email == email).Any();
        }

        private bool IsPhoneExists(string phone)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(x => x.Phone == phone).Any();
        }

        #endregion
    }
}
