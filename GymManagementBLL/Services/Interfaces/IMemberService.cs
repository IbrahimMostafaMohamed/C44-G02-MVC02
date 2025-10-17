using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementBLL.ViewModels.MemberViewModels;

namespace GymManagementBLL.Services.Interfaces
{
    public interface IMemberService
    {
        
        IEnumerable<MemberViewModel> GetAllMembers();
        bool CreateMember(CreateMemberViewModel CreateMember);
        MemberViewModel? GetMemberDetails(int MemberId);

        HealthRecordViewModel? GetMemberHealthRecordDetails(int MemberId);
        MemberToUpdateViewModel? GetMemberToUpdate(int MemberId);

        bool UpdateMemberDetails(int Id, MemberToUpdateViewModel UpdatedMember);
        bool RemoveMember(int MemberId);

        

    }
}
