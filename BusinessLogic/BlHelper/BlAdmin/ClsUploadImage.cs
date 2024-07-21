using Microsoft.AspNetCore.Http;

namespace BusinessLogic.BlHelper.BlAdmin
{
    public class ClsUploadImage
    {
        public static async Task<string> UploadImage(List<IFormFile> Files ,string folderName) 
        {
            foreach (var file in Files) 
            {
                if (file.Length > 0)
                {
                    var ImageName = Guid.NewGuid().ToString() + DateTime.Now.Ticks.ToString() + ".jpg";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/Admin/Uploads/Images/" + folderName ,ImageName);

                    using (var stream = File.Create(filePath)) 
                    {
                        await file.CopyToAsync(stream);
                        return ImageName;
                    }
                }
            }

            return "";
        }
    }
}
