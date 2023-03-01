using FishForoshi.Abstraction.Common;
using FishForoshi.ViewModel.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace FishForoshi.Implementation.Upload
{
    public class UploadFileService : IUploadFileService
    {
        [Obsolete]
        private readonly IHostingEnvironment hostingEnvironment;

        [Obsolete]
        public UploadFileService(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        [Obsolete]
        public async Task<UploadFileResultModel> Upload(IFormFile file, string folder)
        {
            var fileName = GetFileName(file);
            var relativePath = GetRelativePath(fileName, folder);
            var absolutePath = GetAbsolutePath(fileName, folder);
            using (var stream = File.Create(absolutePath))
            {
                await file.CopyToAsync(stream);
            }

            return new UploadFileResultModel
            {
                FileName = fileName,
                RelativePath = relativePath,
                AbsolutePath = absolutePath
            };
        }

        private string GetFileSuffix(IFormFile file)
        {
            return file.FileName.Substring(file.FileName.LastIndexOf('.') + 1);
        }

        private string GetFileName(IFormFile file)
        {
            var fileId = Guid.NewGuid().ToString().Replace("-", "");
            var fileSuffix = GetFileSuffix(file);
            var fileName = string.Format($"{fileId}.{fileSuffix}");

            return fileName;
        }

        private string GetRelativePath(string fileName, string folder)
        {
            return Path.Combine(folder, fileName);
        }

        [Obsolete]
        private string GetAbsolutePath(string fileName, string folder)
        {
            var root = hostingEnvironment.WebRootPath;
            var path = Path.Combine(root, folder, fileName);

            return path;
        }

        public async Task<bool> CustomerFileUploader(IFormFile file, string filename)
        {
            try
            {
                CreateDirectoryIfNotExist();
                var filePath = $"App_Data\\UserFiles\\Check\\{filename}";
                using (var stream = File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<string> UploadeToAppData(IFormFile file, string folderName)
        {
            try
            {
                string filename = GetFileName(file);

                var filePath = $"App_Data{Path.Combine(folderName, filename)}";
                using (var stream = File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }
                return filename;
            }
            catch (Exception)
            {
                return "خطا";
            }
        }

        private void CreateDirectoryIfNotExist()
        {
            var currentMonth = DateTime.Now.Date.ToString("yyyy-MM");
            var dir = $"App_Data\\UserFiles\\Check\\{currentMonth}\\";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        [Obsolete]
        public Task<DeleteFileResultStatus> DeleteFile(string file, string foldername)
        {
            var absolutePath = GetAbsolutePath(file, foldername);
            File.Delete(absolutePath);

            return Task.FromResult(DeleteFileResultStatus.Success);
        }
    }
}
