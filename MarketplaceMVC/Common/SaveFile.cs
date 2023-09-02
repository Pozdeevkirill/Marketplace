using Marketplace.DAL.Models;

namespace MarketplaceMVC.Common
{
    public static class SaveFile
    {
        public static async Task<List<string>> SaveProductFile(IWebHostEnvironment appEnvironment,
                                                                List<IFormFile> files, 
                                                                string companyName, 
                                                                string productName)
        {
            List<string> result = new();
            foreach (var img in files)
            {
                //Путь к фото
                string path = $"/Files/Images/Products/{companyName}_{productName.Replace("/", " ").Replace("\"", " ").Replace("'", " ")}_{img.FileName}";

                //Сохранение фото
                using (var fs = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await img.CopyToAsync(fs);
                }
                result.Add(path);
            }

            return result;
        }
    }
}
