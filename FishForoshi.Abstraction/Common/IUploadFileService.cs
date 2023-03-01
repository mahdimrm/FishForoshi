using FishForoshi.ViewModel.Common;
using Microsoft.AspNetCore.Http;

namespace FishForoshi.Abstraction.Common
{
    public interface IUploadFileService
    {
        Task<UploadFileResultModel> Upload(IFormFile file, string folder);
        Task<bool> CustomerFileUploader(IFormFile file, string filename);
        Task<string> UploadeToAppData(IFormFile file, string folderName);
        Task<DeleteFileResultStatus> DeleteFile(string file, string foldername);

    }
}
