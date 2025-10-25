
using Microsoft.AspNetCore.Http;

namespace GymManagementBLL.Services.AttachmentsServices
{
    public interface IAttachmentsServices
    {
        //public string? Upload(string folderName , IFormFile file);
        Task<string>? Upload(string FolderName , IFormFile file);
        bool Delete(string FolderName , string FileName); 
    }
}
